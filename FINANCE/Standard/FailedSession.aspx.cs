using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace FINANCE.Standard
{
    public partial class FailedSession : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "select LOGO = BIG_LOGO from SECURITY.dbo.SC_COMPANY where CODE='1'";
                //conn.ExecuteQuery();
                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn, conn.QueryString, "LOGO");
            }
        }
    }
}