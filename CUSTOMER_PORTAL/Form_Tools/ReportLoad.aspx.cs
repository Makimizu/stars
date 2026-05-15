using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Form_Tools
{
    public partial class ReportLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string appid = Request.QueryString["APPID"];
                string code = Request.QueryString["CODE"];

                Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
                conn.QueryString = "select URL from REPORT_LIST where APP_ID='" + appid + "' and CODE='" + code + "'";
                conn.ExecuteQuery();

                Response.Redirect(conn.GetFieldValue("URL").ToString());
            }
        }
    }
}