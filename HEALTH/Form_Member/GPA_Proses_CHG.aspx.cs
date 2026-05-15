using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.IO;
using WebCamService;
using System.Data.OleDb;

namespace HEALTH.Form_Member
{
    public partial class GPA_Peserta_ChangePlan : System.Web.UI.Page
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
                LB_ID.Text = Request.QueryString["BATCH_ID"];
                Setup();
                DisableButton(LB_ID.Text);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "NAMA = LEFT(a.NAMA,30) " +
                                "from PESERTA_MASTER a " +
                                "inner join POLICY_PERIOD b on a.POLICY_ID=b.POLICY_ID " +
                                "inner join ENDORSEMENT_BATCH c on b.ID=c.POLICY_PERIOD_ID " +
                                "where " +
                                "a.STAT='1' " +
                                "and c.BATCH_ID = '" + LB_ID.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_REGNO.Items.Clear();
            DDL_REGNO.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REGNO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.DESCR " +
                                "from POLICY_PERIOD_PACKAGE a " +
                                "inner join ENDORSEMENT_BATCH b on a.POLICY_PERIOD_ID=b.POLICY_PERIOD_ID and b.TIPE_ENDORS='CHG' " +
                                "where b.BATCH_ID='" + LB_ID.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PLAN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "c.COMPANY_CODE " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join POLICY c on b.POLICY_ID=c.ID " +
                                "where a.BATCH_ID='" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            LB_COMPANYCODE.Text = conn.GetFieldValue(0, 0).ToString();

            BTN_CARI_REGNO_EMP.Attributes.Add("onclick", "window.open('../Form_Tools/SearchEmployeCompany.aspx?company_code=" + LB_COMPANYCODE.Text + "&parent=0&target=DDL_REGNO','PESERTA','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "if exists(select USER_ENDDATE from TRACK_DATA where TIPE_CODE='GPA' AND OWNER='" + LB_ID.Text.Trim() + "' AND SEQ=2 and USER_ENDDATE is not null) " +
                                    "select isEnd = 1, RESULT = '' " +
                                    "else " +
                                    "select isEnd = 0, RESULT = '' ";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("isEnd").Equals("1") && BT_ADD.Enabled.Equals(true))
                {
                    DisableButton(LB_ID.Text.Trim());
                    return;
                }
                conn.QueryString = "exec SP_GPA_PESERTA_MUTASI_PROSES " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_REGNO.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_PLAN.SelectedValue + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString().Trim() != "")
                {
                    LB_ERROR.Text = "<TABLE style='border-spacing:0px;'>" + conn.GetFieldValue("RESULT").ToString() + "</TABLE>";
                    return;
                }
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            Response.Write("<script language='javascript'>parent.gpachgbody.location.href = '../Form_Member/GPA_Proses_CHG_List.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpachgheader.location.href = '../Form_Member/GPA_Proses_CHG.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
        }



        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LBL_STATUS.Text = "Begin Process";
            if (TXT_FILE_UPLOAD.Value == "")
            {
                LBL_STATUS.Text = "";
                return;
            }
            try
            {
                uploadfile();
            }
            catch (Exception er)
            {
                LBL_STATUS.ForeColor = System.Drawing.Color.Red;
                LBL_STATUS.Text = er.Message;
                return;
            }

            LBL_STATUS.ForeColor = System.Drawing.Color.Blue;
            LBL_STATUS.Text = "Upload success";
        }

        private void uploadfile()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                filename = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID").Replace(".", "") + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                try
                {
                    if (File.Exists(_fullpath))
                        File.Delete(_fullpath);
                }
                catch
                {
                    filename = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID").Replace(".", "") + "_" + filename;
                    _fullpath = _path + filename;

                    if (File.Exists(_fullpath))
                        File.Delete(_fullpath);
                }

                userPostedFile.SaveAs(_fullpath);
                ProcessFile(_fullpath, filename);


                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        private void ProcessFile(string fullpath, string filename)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + ";Extended Properties=Excel 8.0");

            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select REGNO,TGL_MUTASI,PLAN from [PESERTA_CHANGEPLAN$] where REGNO<>''", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                string regno = "", tgl_mutasi = "", plan = "";
                DateTime? tglmutasi = null;
                foreach (DataColumn myCol in dt.Columns)
                {
                    if (myCol.ColumnName == "REGNO")
                        regno = myRow[myCol].ToString().Replace(" ", "");
                    if (myCol.ColumnName == "TGL_MUTASI")
                        //tgl_mutasi = myRow[myCol].ToString();
                        tglmutasi = Convert.ToDateTime(myRow[myCol].ToString(), new System.Globalization.CultureInfo("id-ID", true));
                    if (myCol.ColumnName == "PLAN")
                        plan = myRow[myCol].ToString().Replace(" ", "");
                }

                conn.QueryString = "exec SP_GPA_PESERTA_MUTASI_PROSES " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + regno + "'," +
                                    "'" + String.Format("{0:yyyy/MM/dd}", tglmutasi) + "'," +
                                    "null," +
                                    "'" + plan + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                /*
                try
                { conn.ExecuteQuery(150000); }
                catch { }
                */
                //LB_ERROR.Text = conn.QueryString;
                conn.ExecuteQuery(150000);
            }

            con.Close();


            Response.Write("<script language='javascript'>parent.gpachgbody.location.href = '../Form_Member/GPA_Proses_CHG_List.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpachgheader.location.href = '../Form_Member/GPA_Proses_CHG.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
        }

        protected void TXT_DATE_TextChanged(object sender, EventArgs e)
        {
            LB_TGLMUTASI.Text = "";

            try
            {
                string date = GlobalUse.GlobalDateFormat(TXT_DATE.Text, "d/M/yyyy");
                conn.QueryString = "select diffday = ABS(datediff(day,'" + date + "',GETDATE()))";
                conn.ExecuteQuery();
                int diffday = int.Parse(conn.GetFieldValue("diffday").ToString());

                if (diffday > 30)
                    LB_TGLMUTASI.Text = "TGL MUTASI lebih dari range 30 hari !!!<BR>";
            }
            catch { }
        }

        protected void DisableButton(string BATCH_ID)
        {
            conn.QueryString = "SELECT * FROM DBO.V_GPA_ENDORSEMENT_BATCH WHERE BATCH_ID='" + BATCH_ID + "' AND PROS_END IS NOT NULL";
            conn.ExecuteQuery();
            int sa = conn.GetRowCount();
            if (conn.GetRowCount() > 0)
            {
                BT_UPLOAD.Enabled = false;
                BT_ADD.Enabled = false;
            }
        }

        protected void DDL_REGNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPlan();
        }

        protected void DDL_REGNO_TextChanged(object sender, EventArgs e)
        {
            CheckPlan();
        }

        protected void BT_PLAN_Click(object sender, EventArgs e)
        {
            CheckPlan();
        }

        protected void CheckPlan()
        {
            if (DDL_REGNO.SelectedValue != "")
            {
                conn.QueryString = "SELECT a.DESCR FROM POLICY_PERIOD_PACKAGE a JOIN PESERTA_MASTER b on a.ID = b.PACKAGE where b.REGNO = '" + DDL_REGNO.SelectedValue + "'";
                conn.ExecuteQuery();
                TXT_OLD_PLAN.Text = conn.GetFieldValue("DESCR");
            }
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
    }
}