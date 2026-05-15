using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using DMS.DBConnection;

namespace GO
{
    public partial class Email_Detail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        private string _dbip, _fullpath, _path, _ftprootpath, _ftpuid, _ftppwd;
        private int _port;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();

                DDL_APP.SelectedValue = Request.QueryString["APPID"];
                LB_CODE.Text = Request.QueryString["CODE"];

                conn.QueryString = "select DESCR from PARAM_EMAIL where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text;
                conn.ExecuteQuery();
                LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();

                modeBODY();
            }
        }

        protected void Setup()
        {
            DDL_APP.Items.Clear();
            conn.QueryString = "select a.CODE, a.APP_NAME from  M_APPS a  order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_VIEW_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_VIEW.SelectedValue == "0")
            {
                modeBODY();
            }

            if (DDL_VIEW.SelectedValue == "1")
            {
                modeATTACHMENT();
            }

            if (DDL_VIEW.SelectedValue == "2")
            {
                modeSQL();
            }
        }

        protected void modeBODY()
        {
            TR_BODY.Visible = true;
            TR_EMAIL.Visible = false;

            LB_BODY.Text = "";
            conn.QueryString = "select BODY = isnull(BODY,'') from PARAM_EMAIL where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text;
            conn.ExecuteQuery();
            LB_BODY.Text = conn.GetFieldValue("BODY").ToString();
        }

        protected void FillDDLReport()
        {
            LB1.Items.Clear();

            conn.QueryString = "select a.CODE, a.DESCR " +
                                "from REPORT_LIST a " +
                                "left join PARAM_EMAIL_ATTACHMENT b on a.APP_ID=b.APP_ID and a.CODE=b.REPORT_CODE and b.CODE='" + LB_CODE.Text + "' " +
                                "where " +
                                "a.APP_ID='" + DDL_APP.SelectedValue + "' " +
                                "and b.CODE is null " +
                                "and a.DESCR like '%" + TXT_SEARCH_REPORT.Text.Trim() + "%' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                LB1.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void modeATTACHMENT()
        {
            TR_BODY.Visible = false;
            TR_EMAIL.Visible = true;

            FillDDLReport();

            conn.QueryString = "select a.REPORT_CODE, b.DESCR, a.FORMAT " +
                                "from PARAM_EMAIL_ATTACHMENT a " +
                                "inner join REPORT_LIST b on a.APP_ID=b.APP_ID and a.REPORT_CODE=b.CODE " +
                                "where " +
                                "a.APP_ID='" + DDL_APP.SelectedValue + "' " +
                                "and a.CODE='" + LB_CODE.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select CODE, DESCR from PR_EXPORT_FORMAT";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlFORMAT = (DropDownList)DGR.Items[i].FindControl("DDL_FORMAT");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlFORMAT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlFORMAT.SelectedValue = DGR.Items[i].Cells[2].Text;
                }
                catch { }
            }
        }

        protected void BT_BODY_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (TXT_FILE_UPLOAD.Value == "")
                return;

            try
            {
                UploadFile();

            }
            catch (Exception er)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = er.Message;
                return;
            }

            LB_ERR.ForeColor = System.Drawing.Color.Black;
            LB_ERR.Text = "Upload success";
        }

        private void UploadFile()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);
                ProcessFile(_fullpath);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessFile(string FullPath)
        {
            string HTML = "";
            using (StreamReader sr = new StreamReader(FullPath))
            {
                HTML = sr.ReadToEnd(); ;
            }

            conn.QueryString = "update PARAM_EMAIL set BODY = '" + HTML.Replace("'", "''") + "' " +
                                "where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text;
            conn.ExecuteNonQuery();
            modeBODY();
        }

        protected void BT_ON_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into PARAM_EMAIL_ATTACHMENT select " +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + LB1.SelectedValue + "'," +
                                    "'PDF'";
                conn.ExecuteNonQuery();
                modeATTACHMENT();
            }
            catch (System.Exception ex)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = ex.Message;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                DropDownList ddlFORMAT = (DropDownList)e.Item.FindControl("DDL_FORMAT");

                try
                {
                    conn.QueryString = "update PARAM_EMAIL_ATTACHMENT set " +
                                        "FORMAT = '" + ddlFORMAT.SelectedValue + "' " +
                                        "where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text + " and REPORT_CODE='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    modeATTACHMENT();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = ex.Message;
                }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_EMAIL_ATTACHMENT " +
                                        "where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text + " and REPORT_CODE='" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    modeATTACHMENT();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = ex.Message;
                }
            }
        }

        protected void TXT_SEARCH_REPORT_TextChanged(object sender, EventArgs e)
        {
            FillDDLReport();
        }


        protected void modeSQL()
        {
            TR_BODY.Visible = false;
            TR_EMAIL.Visible = false;
            TR_SQL.Visible = true;

            conn.QueryString = "select * from PARAM_EMAIL_SQL " +
                                "where APP_ID='" + DDL_APP.SelectedValue + "' and CODE=" + LB_CODE.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_REPORT.Text = conn.GetFieldValue("REPORT_FIELD").ToString();
                TXT_BODY.Text = conn.GetFieldValue("BODY_FIELD").ToString();
                TXT_CUSTEMAIL.Text = conn.GetFieldValue("CUSTOMER_EMAIL").ToString();
                TXT_CUSTNAME.Text = conn.GetFieldValue("CUSTOMER_NAME").ToString();
                TXT_CUSTPIC.Text = conn.GetFieldValue("CUSTOMER_PIC").ToString();
                TXT_DOCDATE.Text = conn.GetFieldValue("DOCDATE").ToString();
                TXT_DOCNO.Text = conn.GetFieldValue("DOCNO").ToString();
                TXT_TABLENAME.Text = conn.GetFieldValue("TABLENAME").ToString();
            }
        }

        protected void BT_SQL_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_PARAM_EMAIL_SQL_UPSERT " +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + LB_CODE.Text + "'," +
                                    "'1'," +
                                    "'" + TXT_TABLENAME.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_DOCNO.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_DOCDATE.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_CUSTNAME.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_CUSTPIC.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_CUSTEMAIL.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_REPORT.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_BODY.Text.Trim().Replace("'", "") + "'";
                conn.ExecuteNonQuery();
                modeSQL();
            }
            catch (System.Exception ex)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = ex.Message;
            }
        }
    }
}