using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Member
{
    public partial class GPA_Reg : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                string BATCH_ID = Request.QueryString["BATCH_ID"];
                LoadRecord(BATCH_ID);
            }
        }

        protected void Setup()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PR_TIPE_ENDORSEMENT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DDL_SOURCE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PR_DOCUMENT_SOURCE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DateTime date = DateTime.Now;
            TXT_DATE.Text = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();

            string hour, minute;
            hour = date.Hour.ToString();
            minute = date.Minute.ToString();

            if (hour.Length == 1)
                hour = "0" + hour;
            if (minute.Length == 1)
                minute = "0" + minute;

            DDL_HOUR.SelectedValue = hour;
            DDL_MINUTE.SelectedValue = minute;

            BT_GOTO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES ?')){return false;};");

        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", "<", ">"));
                string date = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
                conn.QueryString = "exec SP_GPA_REG_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + LB_PERIODID.Text + "'," +
                                    "'" + regex.Replace(TXT_DOCNO.Text.Trim(), string.Empty) + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    "'" + DDL_SOURCE.SelectedValue + "'," +
                                    "'" + date + " " + DDL_HOUR.SelectedValue + ":" + DDL_MINUTE.SelectedValue + ":00'," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();
                LB_ID.Text = conn.GetFieldValue("BATCH_ID").ToString();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            if (LB_ERROR.Text != "")
                return;

            Response.Redirect("GPA_Reg.aspx?BATCH_ID=" + LB_ID.Text);
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Setup();

            LB_ID.Text = "";
            TXT_DOCNO.Text = "";
            LB_ERROR.Text = "";
            DDL_TIPE.Enabled = true;

            ShowControlOnRecord(false);
        }

        protected void ShowControlOnRecord(bool bShow)
        {
            DDL_GOTO.Visible = bShow;
            BT_GOTO.Visible = bShow;
            BT_ARSIP.Visible = bShow;
            BT_REMARK.Visible = bShow;
            I1.Visible = bShow;
            I2.Visible = bShow;
        }

        protected void BT_GOTO_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_GPA_TRACK_GOTO_PROSES @BATCH_ID='" + LB_ID.Text.Trim() + "',@NEXT_TRACK = " + DDL_GOTO.SelectedValue + ", @USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            Response.Redirect("../Form_Tools/InquiryScreen.aspx?CODE=009");
        }

        protected void BT_ARSIP_Click(object sender, EventArgs e)
        {
            if (LB_ID.Text.Trim() != "")
            {
                string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_GPA", LB_ID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                I1.Attributes.Add("src", URL);
            }
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            if (LB_ID.Text.Trim() != "")
                I1.Attributes.Add("src", "../Form_Tools/Remark.aspx?tipe=GPA&owner=" + LB_ID.Text);
        }

        protected void LoadRecord(string BATCH_ID)
        {
            LB_ID.Text = BATCH_ID;

            ShowControlOnRecord(true);

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], "GPA", LB_ID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            I1.Attributes.Add("src", URL);
            I2.Attributes.Add("src", "../Form_Tools/Track.aspx?tipe=GPA&owner=" + LB_ID.Text);

            conn.QueryString = "select " +
                                "COMPANY_NAME		= d.COMPANY_NAME, " +
                                "PERIODID           = b.ID, " +
                                "PERIOD				= CONVERT(VARCHAR(11), b.START_DATE, 13) + ' - ' + CONVERT(VARCHAR(11), b.END_DATE, 13), " +
                                "EMAIL_PIC			= a.EMAIL_PIC, " +
                                "DOCNO				= a.DOCNO, " +
                                "DOC_SOURCE			= a.DOC_SOURCE, " +
                                "TIPE_ENDORS		= a.TIPE_ENDORS, " +
                                "TGL_BATCH			= a.TGL_BATCH " +
                                "from		ENDORSEMENT_BATCH a " +
                                "inner join	POLICY_PERIOD b on a.POLICY_PERIOD_ID = b.ID " +
                                "inner join	POLICY c on b.POLICY_ID = c.ID " +
                                "inner join	COMPANY d on d.COMPANY_CODE = d.COMPANY_CODE " +
                                "where " +
                                "a.BATCH_ID = '" + BATCH_ID + "'";
            conn.ExecuteQuery();

            DDL_TIPE.SelectedValue = conn.GetFieldValue("TIPE_ENDORS").ToString();
            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_PERIODID.Text = conn.GetFieldValue("PERIODID").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();


            TXT_DOCNO.Text = conn.GetFieldValue("DOCNO").ToString();
            DDL_SOURCE.SelectedValue = conn.GetFieldValue("DOC_SOURCE").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL_PIC").ToString();

            DateTime date = DateTime.Parse(conn.GetFieldValue("TGL_BATCH").ToString());
            TXT_DATE.Text = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();

            string hour, minute;
            hour = date.Hour.ToString();
            minute = date.Minute.ToString();

            if (hour.Length == 1)
                hour = "0" + hour;
            if (minute.Length == 1)
                minute = "0" + minute;

            DDL_HOUR.SelectedValue = hour;
            DDL_MINUTE.SelectedValue = minute;

            //DDL_POLID.Enabled = false;
            DDL_TIPE.Enabled = false;
            //BT_CARIPOLIS.Enabled = false;

            conn.QueryString = "exec SP_GPA_TRACK_GOTO_LIST @BATCH_ID = '" + LB_ID.Text.Trim() + "' ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GOTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            BTN_INFO_TC.Attributes.Remove("onclick");
            BTN_INFO_TC.Attributes.Add("onclick", "window.open('../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + LB_PERIODID.Text + "','PESERTA','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }
    }
}