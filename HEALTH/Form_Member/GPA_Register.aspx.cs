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
    public partial class GPA_Register : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
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

            SearchCompany();
            FillDDLPeriod();

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

            BT_SUBMIT.Attributes.Add("onclick", "if(!confirm('Are you sure to SUBMIT ?')){return false;};");
        }

        protected void DDL_POLID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPeriod();
        }

        protected void FillDDLPeriod()
        {
            conn.QueryString = "select top 3 " +
                                "ID, " +
                                "DESCR = CONVERT(VARCHAR(11), START_DATE, 13)+' - '+CONVERT(VARCHAR(11), END_DATE, 13) " +
                                "FROM POLICY_PERIOD " +
                                "WHERE POLICY_ID='" + DDL_POLID.SelectedValue + "' ORDER BY START_DATE DESC";
            conn.ExecuteQuery();
            DDL_POLICY_PERIOD.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLICY_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

            conn.QueryString = "select " +
                                "b.COMPANY_EMAIL " +
                                "from		POLICY a " +
                                "inner join	COMPANY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "WHERE a.ID='" + DDL_POLID.SelectedValue + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                TXT_EMAIL.Text = conn.GetFieldValue(0, 0);

            BTN_INFO_TC.Attributes.Remove("onclick");
            BTN_INFO_TC.Attributes.Add("onclick", "window.open('../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "','PESERTA','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void TXT_POLID_SEARCH_TextChanged(object sender, EventArgs e)
        {
            SearchCompany();
            FillDDLPeriod();
        }

        protected void SearchCompany()
        {
            DDL_POLID.Items.Clear();
            conn.QueryString = "select " +
                                "a.ID, " +
                                "COMPANY_NAME = LEFT(b.COMPANY_NAME,60) " +
                                "from		POLICY a " +
                                "inner join	COMPANY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where b.COMPANY_NAME like '%" + TXT_POLID_SEARCH.Text.Trim() + "%' order by 2";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLID.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            string ID = "";

            try
            {
                Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", "<", ">"));
                string date = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
                conn.QueryString = "exec SP_GPA_REG_UPSERT " +
                                    "''," +
                                    "'" + DDL_POLICY_PERIOD.SelectedValue + "'," +
                                    "'" + regex.Replace(TXT_DOCNO.Text.Trim(), string.Empty) + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    "'" + DDL_SOURCE.SelectedValue + "'," +
                                    "'" + date + " " + DDL_HOUR.SelectedValue + ":" + DDL_MINUTE.SelectedValue + ":00'," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();

                ID = conn.GetFieldValue("BATCH_ID").ToString();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            if (LB_ERROR.Text != "")
                return;

            Response.Redirect("GPA_Reg.aspx?BATCH_ID=" + ID);
        }

        protected void DDL_POLICY_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTN_INFO_TC.Attributes.Remove("onclick");
            BTN_INFO_TC.Attributes.Add("onclick", "window.open('../Form_Klien/Polis_Period_TC.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "','PESERTA','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }
    }
}