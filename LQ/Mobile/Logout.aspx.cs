using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LQ.Mobile
{
    public partial class Logout : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Request.QueryString["s"];
                    GetOut(s);
                }
                catch { }

                Response.Redirect("../login.aspx");
            }
        }

        protected void GetOut(string s)
        {
            Session.Remove("s");
            conn.QueryString = "update SECURITY.dbo.USER_LOG_HISTORY set LOGOUT=GETDATE() where ROWID='" + s + "'";
            conn.ExecuteNonQuery();
        }
    }
}