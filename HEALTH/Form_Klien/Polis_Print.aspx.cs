using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DMS.DBConnection;
using System.Data;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Print : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LB_ID.Text = Request.QueryString["PolicyPeriod"];
            LoadReport();
        }

        protected void LoadReport()
        {
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("../RPT/RPT_UW_POLICY_SECTION_1.rpt"));

            string uid = GlobalUse.GetAppAttribute(System.Configuration.ConfigurationManager.AppSettings["appid"], "APP_DBUID");
            string pwd = Crypto.DecryptStringAES(GlobalUse.GetAppAttribute(System.Configuration.ConfigurationManager.AppSettings["appid"], "APP_DBPWD"));

            rpt.SetDatabaseLogon(uid, pwd);
            rpt.SetParameterValue("@POLICY_PERIOD_ID", LB_ID.Text);
            CRV.ReportSource = rpt;
            CRV.DataBind();
            CRV.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
        }
    }
}