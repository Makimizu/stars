using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQR.Mobile
{
    public partial class MobileDashboard : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
                    Response.Redirect("../SessionExpired.aspx");
                }

                LB_REGNO.Text = Session["s"].ToString();
                LoadRecord();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appsignature.location.href = 'ApplicationAgreement.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
            }
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "a.MEMBERID, " +
                                "a.FULLNAME, " +
                                "DOB			        = isnull(convert(varchar(20), a.DOB, 106), '') + (case when a.DOB is not null then ' - ' else '' end) + (case when a.GENDER='M' then 'MALE' else 'FEMALE' end), " +
                                "PRODUCT_DESCR          = UPPER(a.PRODUCT_DESCR), " +
                                "PRODUCT_GROUP_DESCR    = UPPER(a.PRODUCT_GROUP_DESCR), " +
                                "a.START_AGE, " +
                                "POLICY_PERIOD	        = convert(varchar(20), a.START_DATE, 106) + ' - ' + isnull(convert(varchar(20), a.END_DATE, 106), ''), " +
                                "PAYMENT_PERIOD	        = convert(varchar(20), a.START_PAYMENT_DATE, 106) + ' - ' + isnull(convert(varchar(20), a.END_PAYMENT_DATE, 106), ''), " +
                                "SUMINS		            = replace(convert(varchar(100), convert(money, a.SUMINS),1), '.00', ''), " +
                                "UW_CODE_DESCR, " +
                                "AGENT_CODE, " +
                                "AGENT_NAME, " +
                                "AGENT_CHANNEL, " +
                                "AGENT_LEVEL, " +
                                "AGENCY_NAME = isnull(AGENCY_NAME, '') + ' - ' + isnull(AGENCY_BRANCH, '') " +
                                "from V_APPLICATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_PRODUCTGROUP.Text = conn.GetFieldValue("PRODUCT_GROUP_DESCR").ToString();
            LB_PRODUCTNAME.Text = conn.GetFieldValue("PRODUCT_DESCR").ToString();
            LB_STARTAGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_CODE_DESCR").ToString();

            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_POLICY_PERIOD.Text = conn.GetFieldValue("POLICY_PERIOD").ToString();
            LB_PAYMENT_PERIOD.Text = conn.GetFieldValue("PAYMENT_PERIOD").ToString();

            LB_AGENT_CODE.Text = conn.GetFieldValue("AGENT_CODE").ToString();
            LB_AGENT_NAME.Text = conn.GetFieldValue("AGENT_NAME").ToString();
            LB_CHANNEL.Text = conn.GetFieldValue("AGENT_CHANNEL").ToString();
            LB_LEVEL.Text = conn.GetFieldValue("AGENT_LEVEL").ToString();
            LB_AGENCY.Text = conn.GetFieldValue("AGENCY_NAME").ToString();



        }
    }
}