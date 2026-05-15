using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace FINANCE.Form_Bank
{
    public partial class UploadInvoiceSettle : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string _fullpath, _path;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from PARAM_NOTA_TYPE a " +
                                "inner join V_LINK_SC_M_APPS b on a.APP_ID = b.CODE " +
                                "order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.BATCH_ID, " +
                                "REMARK, " +
                                "USERBY, " +
                                "CNT, " +
                                "USERDATE, " +
                                "ENABLE_DELETE = (case when datediff(day, a.USERDATE, GETDATE()) = 0 then 1 else 0 end) " +
                                "from INVOICE_BATCH_SETTLE a " +
                                "left join (select BATCH_ID, CNT = count(BATCH_ID) from INVOICE_BATCH_SETTLE_RAW where LEN(LTRIM(RTRIM(isnull(INVOICENO collate database_default,'') + isnull(REGNO collate database_default,'')))) > 0 group by BATCH_ID) b on a.BATCH_ID = b.BATCH_ID " +
                                "where APPID = '" + DDL_APP.SelectedValue + "' " +
                                "order by " +
                                "a.USERDATE desc";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDel = (Button)DGR.Items[i].FindControl("BT_DEL");

                if (DGR.Items[i].Cells[1].Text != "1")
                    btDel.Visible = false;
                else
                    btDel.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }


        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (TXT_FILE_UPLOAD.Value == "")
                return;

            try
            {
                UploadFile();
                FillDGR();
            }
            catch (Exception er)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = er.Message + "<BR><BR>";
                return;
            }

            if (LB_ERR.Text == "")
            {
                LB_ERR.ForeColor = System.Drawing.Color.Black;
                LB_ERR.Text = "Upload success";
            }
        }

        private void UploadFile()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                filename = Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);
                ProcessSpecialTemplate(filename, _fullpath);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
            string BATCH_ID;
            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                "set @BATCH_ID = NEWID() " +
                                "insert into INVOICE_BATCH_SETTLE select " +
                                "@BATCH_ID," +
                                "'" + DDL_APP.SelectedValue + "'," +
                                "''," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery();

            BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            try
            {

                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into INVOICE_BATCH_SETTLE_RAW select " +
                                    "'" + BATCH_ID + "'," +
                                    "'" + IDX.ToString() + "',";

                    for (int i = 0; i < 24; i++)
                    {
                        string val = "";
                        try
                        {
                            val = myRow[i].ToString().Trim().Replace("'", "`");
                        }
                        catch { }

                        sql = sql + "'" + val + "',";
                    }
                    conn.QueryString = sql + "''";
                    conn.ExecuteNonQuery();
                }

                conn.QueryString = "exec SP_INVOICE_BATCH_SETTLE_PROCESS " +
                                    "'" + BATCH_ID + "'";
                conn.ExecuteNonQuery();

                con.Close();
            }
            catch (System.Exception ex)
            {
                con.Close();
                LB_ERR.Text = ex.Message + "<BR><BR>";

            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERR.Text = "";

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_INVOICE_BATCH_SETTLE_ROLLBACK '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = ex.Message + "<BR><BR>";
                    return;
                }
            }
        }

        protected void DDL_APP_SelectedIndexChanged1(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void LBT_TEMPLATE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rs:Format=EXCEL&APPID=' from V_LINK_SC_REPORT_LIST where REPORT_NAME = 'RPT_INVOICE_SETTLE_TEMPLATE'";
            conn.ExecuteQuery();
            Response.Redirect(conn.GetFieldValue("URL").ToString() + DDL_APP.SelectedValue);
        }
    }
}