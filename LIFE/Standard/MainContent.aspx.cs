using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LIFE.Standard
{
    public partial class MainContent : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string session = Session["s"].ToString();
            }
            catch
            {
                Logout();
            }
        }

        protected void Logout()
        {
            Session.Clear();

            conn.QueryString = "select URL = (case when APP_SVR like 'https://%' then '' else 'http://' end) + APP_SVR + '/' from M_APPS where CODE='0'";
            conn.ExecuteQuery();

            try
            {
                string frameScript = "<script language='javascript'>window.top.location.href='" + conn.GetFieldValue("URL").ToString() + "logout.aspx?s=" + Session["s"].ToString() + "';</script>";
                Response.Write(frameScript);
            }
            catch
            {
                string frameScript = "<script language='javascript'>window.top.location.href='" + conn.GetFieldValue("URL").ToString() + "login.aspx';</script>";
                Response.Write(frameScript);
            }
        }
    }
}