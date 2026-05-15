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

namespace LIFE.Form_Tools
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
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
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
        }

        protected void FillDGR()
        {
            conn.QueryString = "select BATCH_ID, MODE, USER_BY, USER_DATE, RECORDS, PROCESSED from V_BATCH_MASTER " +
                                "where " +
                                "MODE = '" + LB_MODE.Text + "' " +
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

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            Process(filename, fullpath);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            if (LB_ERR.Text.Trim() == "")
                Response.Redirect("UploadBatch.aspx?mode=" + LB_MODE.Text);
        }

        protected void Process(string filename, string FullPath)
        {
            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                "set @BATCH_ID = NEWID() " +
                                "insert into BATCH_MASTER select " +
                                "@BATCH_ID," +
                                "'" + LB_MODE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery();
            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();

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
                                    "'" + LB_MODE.Text + "' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                                    "'" + BATCH_ID + "'," +
                                    "'" + LB_MODE.Text + "'," +
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

                Task.Run(() => ProcessBatch(BATCH_ID));
            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
            }

        }

        protected void ProcessBatch(string batchid)
        {
            conn.QueryString = "exec SP_BATCH_MASTER_UPLOAD '" + batchid + "'";
            conn.ExecuteQuery(500000);
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
                conn.QueryString = "exec SP_APPLICATION_DATA_RAW '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_RAW.DataSource = dt;
                DGR_RAW.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
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
            GlobalUse.DataGridToExcel(this, DGR_RAW);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }
    }
}