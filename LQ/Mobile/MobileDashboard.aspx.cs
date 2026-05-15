using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Mobile
{
    public partial class MobileDashboard : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["s"] == null)
                Response.Redirect("logout.aspx");

            GenerateCharts();
        }

        protected void GenerateCharts()
        {
            conn.QueryString = "exec SP_GOOGLECHARTCOMBO '" + Session["s"].ToString() + "',0,'chart1'";
            conn.ExecuteQuery();
            LB_CHART1.Text = conn.GetFieldValue("RESULT").ToString();
        }
    }
}