using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace SAVING.Form_Trx
{
    public partial class UploadBatch : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from V_CUSTODIAN_MASTER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLMode();

            conn.QueryString = "select " +
                                "YEAR		= YEAR(GETDATE()) - a.SEQ + 1 " +
                                "from		SC_SEQ a " +
                                "where " +
                                "a.SEQ <= 5 " +
                                "order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select MONTH(GETDATE())";
            conn.ExecuteQuery();
            DDL_MONTH.SelectedValue = conn.GetFieldValue(0, 0).ToString();

            try
            {
                DDL_MODE.SelectedValue = Request.QueryString["mode"].ToString();
            }
            catch { }
        }

        protected void FillDDLMode()
        {
            DDL_MODE.Items.Clear();
            conn.QueryString = "select " +
                                "CODE		= COMPANY_CODE + '-' + MODE_CODE, " +
                                "DESCR		= DESCR " +
                                "from		CUSTODIAN_MASTER_UPLOAD_MODE " +
                                "where " +
                                "COMPANY_CODE = '" + DDL_COMPANY.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            conn.QueryString = "select BATCH_ID, MODE, USER_BY, USER_DATE, RECORDS, PROCESSED from V_BATCH_MASTER " +
                                "where " +
                                "MODE = '" + DDL_MODE.SelectedValue + "' " +
                                "and YEAR(USER_DATE) = " + DDL_YEAR.SelectedValue + " " +
                                "and MONTH(USER_DATE) = " + DDL_MONTH.SelectedValue + " " +
                                "order by USER_DATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT_DELETE");
                bt.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (!FU.HasFile)
                return;

            conn.QueryString = "select VALUE from SECURITY.dbo.SC_GENERAL_SET where APP_CODE = '0' and PARAMETER = 'UPLOAD_FOLDER'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = conn.GetFieldValue(0, 0).ToString() + "\\" + Session["s"] + filename;
            //string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            Process(filename, fullpath);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            if (LB_ERR.Text.Trim() == "")
                Response.Redirect("UploadBatch.aspx?mode=" + DDL_MODE.SelectedValue);
        }

        protected void Process(string filename, string FullPath)
        {
            conn.QueryString = "select FILE_FORMAT, DELIMITER from CUSTODIAN_MASTER_UPLOAD_MODE where COMPANY_CODE + '-' + MODE_CODE = '" + DDL_MODE.SelectedValue + "'";
            conn.ExecuteQuery();
            string FORMAT = conn.GetFieldValue("FILE_FORMAT").ToString();
            string DELIMITER = conn.GetFieldValue("DELIMITER").ToString();

            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                "set @BATCH_ID = NEWID() " +
                                "insert into BATCH_MASTER select " +
                                "@BATCH_ID," +
                                "'" + DDL_MODE.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery();
            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();


            switch (FORMAT)
            {
                case "EXCEL": ProcessExcel(filename, FullPath, BATCH_ID); break;
                case "TXT": ProcessTXT(filename, FullPath, BATCH_ID, DELIMITER); break;
            }

            Task.Run(() => ProcessBatch(BATCH_ID));
        }

        protected void ProcessExcel(string filename, string FullPath, string BATCH_ID)
        {
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            try
            {
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into APPLICATION_DATA_RAW select " +
                                    "'" + DDL_MODE.SelectedValue + "' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                                    "'" + BATCH_ID + "'," +
                                    "'" + DDL_MODE.SelectedValue + "'," +
                                    "'',";

                    for (int i = 0; i < 70; i++)
                    {
                        string val = "";
                        try
                        {
                            val = myRow[i].ToString().Trim().Replace("'", "`");
                        }
                        catch { }

                        sql = sql + "'" + val + "',";
                    }

                    conn.QueryString = sql + "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', GETDATE()";
                    conn.ExecuteNonQuery();
                }


                con.Close();

                conn.QueryString = "delete from APPLICATION_DATA_RAW where BATCH_ID = '" + BATCH_ID + "' and isnull(F01, '') = ''";
                conn.ExecuteNonQuery();

                //Task.Run(() => ProcessBatch(BATCH_ID));
                //ProcessBatchAsync(BATCH_ID);

            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
            }
        }

        protected void ProcessTXT(string filename, string FullPath, string BATCH_ID, string DELIMITER)
        {
            conn.QueryString = "exec SP_BATCH_PROCESS_TXT " +
                                "'" + BATCH_ID + "'," +
                                "'" + FullPath + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
                                "'" + DELIMITER + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            /*
            string USERID = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

            conn.QueryString = "select ID = dbo.UFN_GET_NEWID()";
            conn.ExecuteQuery();
            Int64 seq = Int64.Parse(conn.GetFieldValue("ID").ToString());

            StreamReader reader = File.OpenText(FullPath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                seq++;

                string values = "";
                int nfield = 0;
                string[] splits = line.Split(new string[] { DELIMITER, }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string value in splits)
                {
                    nfield++;
                    string val = value.Replace("\"", "").Replace("'", "`");
                    if (val.Length > 255)
                        val = value.Substring(0, 255);

                    values = values + ",'" + val + "'";
                }

                int cntfields = 70;
                for (int i = 0; i < cntfields - nfield; i++)
                {
                    values = values + ",null";
                }

                values = "'" + seq.ToString() + "'," +
                         "'" + BATCH_ID + "'," +
                         "'" + DDL_MODE.SelectedValue.Replace(DDL_COMPANY.SelectedValue + "-", "") + "'," +
                         "''" +
                         values + "," +
                         "'" + USERID + "'," +
                         "GETDATE()";

                try
                {
                    conn.QueryString = "insert into APPLICATION_DATA_RAW select " + values;
                    conn.ExecuteNonQuery();

                    conn.QueryString = "delete from APPLICATION_DATA_RAW where BATCH_ID = '" + BATCH_ID + "' and isnull(F01, '') = ''";
                    conn.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }

            reader.Close();
            */
        }

        protected void ProcessBatch(string batchid)
        {
            conn.QueryString = "exec SP_BATCH_MASTER_UPLOAD '" + batchid + "'";
            conn.ExecuteNonQuery();
        }

        protected async Task ProcessBatchAsync(string batchid)
        {
            await Task.Run(() =>
            {
                conn.QueryString = "exec SP_BATCH_MASTER_UPLOAD '" + batchid + "'";
                conn.ExecuteNonQuery();
            });
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Raw")
            {
                DGR_RAW.CurrentPageIndex = 0;
                FillDGRRaw(e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from BATCH_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                FillDGR();
            }
        }

        protected void FillDGRRaw(string BATCH_ID)
        {
            LB_BATCHID.Text = BATCH_ID;
            conn.QueryString = "exec SP_APPLICATION_DATA_RAW '" + LB_BATCHID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_RAW.DataSource = dt;
            DGR_RAW.DataBind();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            //GlobalUse.DataGridToExcel(this, DGR_RAW);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DDL_COMPANY_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLMode();
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_RAW_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_RAW.CurrentPageIndex = e.NewPageIndex;
            FillDGRRaw(LB_BATCHID.Text);
        }
    }
}