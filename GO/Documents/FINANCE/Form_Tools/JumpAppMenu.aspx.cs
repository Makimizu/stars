using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Tools
{
    public partial class JumpAppMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string appid = Request.QueryString["appid"];
                string menu = Request.QueryString["menu"];
                string param = Request.QueryString["param"];

                Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
                conn.QueryString = "select " +
                                    "PATH = (case when APP_SVR like 'https://%' then '' else 'http://' end) + APP_SVR + '/' + APP_PATH + '/Redirect.aspx?s=' " +
                                    "from M_APPS  " +
                                    "where " +
                                    "CODE = '" + appid + "'";
                conn.ExecuteQuery();

                string path = conn.GetFieldValue("PATH").ToString() + Session["s"] + "&menu=" + menu + "&param=" + param;
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.content.location.href = '" + path + "';</script>");
            }
        }
    }
}