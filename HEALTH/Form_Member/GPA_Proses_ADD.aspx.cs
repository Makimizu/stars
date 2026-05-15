using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Proses_ADD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string _dbip, _fullpath, _path, _ftprootpath, _ftpuid, _ftppwd;
        private int _port;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_BATCH_ID.Text = Request.QueryString["BATCH_ID"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "c.COMPANY_CODE " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join POLICY c on b.POLICY_ID=c.ID " +
                                "where a.BATCH_ID='" + LB_BATCH_ID.Text + "'";
            conn.ExecuteQuery();
            LB_COMPANYCODE.Text = conn.GetFieldValue(0, 0).ToString();

            conn.QueryString = "select CODE,BANK from FINANCE.dbo.PARAM_TBL_BANK order by BANK";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select d.BRANCH_CODE, d.NAMA_CABANG " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join POLICY c on b.POLICY_ID=c.ID " +
                                "inner join BRANCH d on c.COMPANY_CODE=d.COMPANY_CODE " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCH_ID.Text + "' " +
                                "order by " +
                                "d.NAMA_CABANG";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_GENDER where CODE not in ('0','C') order by CREATEDATE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GENDER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_FAMILY_GROUP";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GRUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select c.ID, c.DESCR " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join POLICY_PERIOD_PACKAGE c on b.ID=c.POLICY_PERIOD_ID " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCH_ID.Text + "' " +
                                "order by " +
                                "c.DESCR";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PACKAGE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillREGNOEMP();

            conn.QueryString = "select CODE,DESCR from dbo.PR_JENIS_ID";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TIPEID.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            BTN_CARI_REGNO_EMP.Attributes.Add("onclick", "window.open('../Form_Tools/SearchEmployeCompany.aspx?company_code=" + LB_COMPANYCODE.Text + "&parent=0&target=DDL_REGNO_EMP','PESERTA','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void FillREGNOEMP()
        {
            DDL_REGNO_EMP.Items.Clear();
            if (DDL_GRUP.SelectedValue == "1E")
                return;

            conn.QueryString = "select c.REGNO, c.NAMA " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join PESERTA_MASTER c on b.POLICY_ID=c.POLICY_ID " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCH_ID.Text + "' and c.STAT='1' and c.FAMILY_GROUP = '1E' " +
                                "union all " +
                                "select REGNO, NAMA " +
                                "from PESERTA_MASUK_TEMP " +
                                "where " +
                                "BATCH_ID =  '" + LB_BATCH_ID.Text + "' " +
                                "and FAMILY_GROUP = '1E' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REGNO_EMP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillREGNOEMP_Info();
        }



        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            string accbank = "null";
            string accname = "null";
            string accno = "null";

            if (DDL_BANK.SelectedValue != "" && TXT_ACCNO.Text.Trim() != "" && TXT_ACCNAMA.Text.Trim() != "")
            {
                accbank = "'" + DDL_BANK.SelectedValue + "'";
                accname = "'" + TXT_ACCNAMA.Text.Trim() + "'";
                accno = "'" + TXT_ACCNO.Text.Trim() + "'";
            }

            try
            {
                conn.QueryString = "exec SP_GPA_PESERTA_ADDITION_PROSES_ENTRY " +
                                    "'" + LB_BATCH_ID.Text + "'," +
                                    "'" + DDL_REGNO_EMP.SelectedValue + "'," +
                                    "''," +
                                    "'" + DDL_BRANCH.SelectedValue + "'," +
                                    "'" + TXT_NAMA.Text.Trim() + "'," +
                                    "'" + DDL_GENDER.SelectedValue + "'," +
                                    "'" + DDL_GRUP.SelectedValue + "'," +
                                    "'" + DDL_VIP.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_TIPEID.SelectedValue + "'," +
                                    "'" + TXT_NOID.Text.Trim() + "'," +
                                    "'" + TXT_PHONE.Text.Trim() + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    "'" + DDL_PACKAGE.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLMASUK.Text.Trim(), "d/M/yyyy") + "'," +
                                    accbank + "," +
                                    accno + "," +
                                    accname + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString().Trim() != "")
                {
                    LB_ERR.Text = "<TABLE style='border-spacing:0px;'>" + conn.GetFieldValue("RESULT").ToString() + "</TABLE>";
                    return;
                }
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
                return;
            }


            Response.Write("<script language='javascript'>parent.gpaaddbody.location.href = '../Form_Member/GPA_Proses_ADD_List.aspx?BATCH_ID=" + LB_BATCH_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpaaddheader.location.href = '../Form_Member/GPA_Proses_ADD.aspx?BATCH_ID=" + LB_BATCH_ID.Text + "';</script>");

        }



        protected void DDL_GRUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_BRANCH.Enabled = true;
            DDL_PACKAGE.Enabled = true;
            FillREGNOEMP();
        }

        protected void DDL_REGNO_EMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillREGNOEMP_Info();
        }

        protected void FillREGNOEMP_Info()
        {
            DDL_BRANCH.Enabled = false;
            //DDL_PACKAGE.Enabled = false;

            try
            {
                conn.QueryString = "exec SP_GPA_PESERTA_REGNO_EMP '" + DDL_REGNO_EMP.SelectedValue + "'";
                conn.ExecuteQuery();
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
                DDL_PACKAGE.SelectedValue = conn.GetFieldValue("PACKAGE").ToString();
            }
            catch { }
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_MODE.SelectedValue == "INSERT")
            {
                TR_INSERT.Visible = true;
                TR_UPLOAD.Visible = false;
            }
            else
            {
                TR_INSERT.Visible = false;
                TR_UPLOAD.Visible = true;
            }
        }

        protected void BT_EXCEL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='14'";
            conn.ExecuteQuery();
            string URL = conn.GetFieldValue("URL").ToString() + "&rs:Format=EXCEL&BATCH_ID=" + LB_BATCH_ID.Text;
            Response.Redirect(URL);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
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
            conn.QueryString = "select " +
                                "TBL = replace(DESCR,' ','') " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD_PACKAGE b on a.POLICY_PERIOD_ID=b.POLICY_PERIOD_ID " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCH_ID.Text + "' " +
                                "order by b.SEQ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            string[] Paket = new string[conn.GetRowCount()];
            for (int i = 0; i < conn.GetRowCount(); i++)
                Paket[i] = conn.GetFieldValue(i, "TBL").ToString();

            conn.QueryString = "delete from ENDORSEMENT_BATCH_DATAUPLOAD " +
                                "where  " +
                                "BATCH_ID = '" + LB_BATCH_ID.Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < Paket.Count(); i++)
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("select [FAMILY GROUP], [REGNO EMP], [BRANCH CODE],NAMA,SEX,DOB, [TGL MASUK], [IDENTITY NO],PHONE, [ACC BANK], [ACC NO], [ACC NAMA],EMAIL,VIP from [" + Paket[i] + "$] where NAMA <> ''", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string values = "";
                    foreach (DataColumn myCol in dt.Columns)
                    {
                        string val = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                        values = values + "'" + val + "',";
                    }


                    values = "'" + LB_BATCH_ID.Text + "'," +
                                "'" + Paket[i] + "'," +
                                "'" + IDX.ToString() + "'," +
                                values +
                                "null";

                    conn.QueryString = "insert into ENDORSEMENT_BATCH_DATAUPLOAD " +
                                        "(BATCH_ID,WORKSHEET,SEQ,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15) " +
                                        "select " +
                                        values;

                    conn.ExecuteNonQuery();
                }

                con.Close();
            }

            conn.QueryString = "exec SP_GPA_PESERTA_ADDITION_PROSES_FILEUPLOAD " +
                                "'" + LB_BATCH_ID.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            Response.Write("<script language='javascript'>parent.gpaaddbody.location.href = '../Form_Member/GPA_Proses_ADD_List.aspx?BATCH_ID=" + LB_BATCH_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpaaddheader.location.href = '../Form_Member/GPA_Proses_ADD.aspx?BATCH_ID=" + LB_BATCH_ID.Text + "';</script>");
        }
    }
}