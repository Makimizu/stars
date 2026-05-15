using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Finance
{
    public partial class ApplicationPremiumStatistic : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                ShowPremiumStatistic();
            }
        }

        protected void ShowPremiumStatistic()
        {
            conn.QueryString = "exec SP_APPLICATION_PREMIUM_PERFORMANCE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_LOM.Text = conn.GetFieldValue("LOM").ToString();
            LB_TENOR.Text = conn.GetFieldValue("TENOR").ToString();
            LB_ANNUAL_PREMIUM.Text = conn.GetFieldValue("ANNUAL_PREMIUM_EXPECTED").ToString();
            LB_REMAINING.Text = conn.GetFieldValue("REMAINING_PERIOD").ToString();

            LB_MTD_TERM_EXPECTED.Text = conn.GetFieldValue("MTD_TERM_EXPECTED").ToString();
            LB_MTD_AMOUNT_EXPECTED.Text = conn.GetFieldValue("MTD_PREMIUM_EXPECTED").ToString();
            LB_MTD_TERM_PAID.Text = conn.GetFieldValue("MTD_TERM_PAID").ToString();
            LB_MTD_AMOUNT_PAID.Text = conn.GetFieldValue("MTD_PREMIUM_PAID").ToString();
            LB_MTD_TERM_OUTSTANDING.Text = conn.GetFieldValue("MTD_TERM_OUTSTANDING").ToString();
            LB_MTD_AMOUNT_OUTSTANDING.Text = conn.GetFieldValue("MTD_PREMIUM_OUTSTANDING").ToString();

            LB_TOTAL_TERM_EXPECTED.Text = conn.GetFieldValue("TOTAL_TERM_EXPECTED").ToString();
            LB_TOTAL_AMOUNT_EXPECTED.Text = conn.GetFieldValue("TOTAL_PREMIUM_EXPECTED").ToString();
            LB_TOTAL_TERM_PAID.Text = conn.GetFieldValue("TOTAL_TERM_PAID").ToString();
            LB_TOTAL_AMOUNT_PAID.Text = conn.GetFieldValue("TOTAL_PREMIUM_PAID").ToString();
            LB_TOTAL_TERM_INCOMPLETED.Text = conn.GetFieldValue("TOTAL_TERM_INCOMPLETED").ToString();
            LB_TOTAL_AMOUNT_INCOMPLETED.Text = conn.GetFieldValue("TOTAL_PREMIUM_INCOMPLETED").ToString();
        }
    }
}