using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Policy_Period_Plan : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_POLICY_PERIOD_PACKAGE_PLAN_XTAB '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
        }
    }
}