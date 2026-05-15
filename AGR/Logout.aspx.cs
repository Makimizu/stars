using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace AGR
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

                //Response.Redirect("Login.aspx");
                conn.QueryString = "select URL = (case when APP_SVR like 'https://%' then '' else 'http://' end) + APP_SVR + '/' from M_APPS where CODE='0'";
                conn.ExecuteQuery();
                Response.Redirect(conn.GetFieldValue("URL").ToString() + "login.aspx");
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