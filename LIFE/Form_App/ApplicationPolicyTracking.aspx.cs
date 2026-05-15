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

namespace LIFE.Form_App
{
    public partial class ApplicationPolicyTracking : System.Web.UI.Page
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
        }

        protected void FillDGR()
        {
            conn.QueryString = "select	" +
                                "BATCH_ID = UPPER(convert(varchar(255), a.ID))," +
                                "MODE = a.TYPE," +
                                "USER_BY = isnull(c.FRONT_NAME + ' ' + isnull(c.LAST_NAME, ''), a.USERBY)," +
                                "USER_DATE = a.USERDATE," +
                                "RECORDS = isnull(b.CNT, 0) " +
                                "from       BATCH_MASTER a " +
                                "left join  (   select " +
                                "               BATCH_ID = a.BATCH_ID, " +
                                "               CNT = count(a.REGNO) " +
                                "               from APPLICATION_DATA_RAW a " +
                                "               where ISDATE(a.F01) > 0 " +
                                "               group by " +
                                "               a.BATCH_ID " +
                                "           ) b on a.ID = b.BATCH_ID " +
                                "left join   V_LINK_SC_M_USERS c on a.USERBY = c.CODE collate database_default " +
                                "where " +
                                "a.TYPE = 'NB' " +
                                "order by a.USERDATE desc";
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
            ProcessExcel(filename, fullpath);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            if (LB_ERR.Text.Trim() == "")
                Response.Redirect("ApplicationPolicyTracking.aspx");
        }

        protected void ProcessExcel(string filename, string FullPath)
        {
            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                "set @BATCH_ID = NEWID() " +
                                "insert into BATCH_MASTER select " +
                                "@BATCH_ID," +
                                "'NB'," +
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
                                    "'NB' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                                    "'" + BATCH_ID + "'," +
                                    "'NB'," +
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
            conn.ExecuteNonQuery();
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

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            //GlobalUse.DataGridToExcel(this, DGR_RAW);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DGR_RAW_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_RAW.CurrentPageIndex = e.NewPageIndex;
            FillDGRRaw(LB_BATCHID.Text);
        }
    }
}