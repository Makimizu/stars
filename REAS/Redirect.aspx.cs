using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace REAS
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string s = Request.QueryString["s"];

                //string s = Guid.NewGuid().ToString("N");

                string menu = Request.QueryString["menu"];
                string param = Request.QueryString["param"];
                Session.Remove("s");
                Session.Add("s", s);

                if (menu != null)
                {
                    Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
                    conn.QueryString = "select " +
                                        "PATH = (case when b.APP_SVR like 'https://%' then '' else 'http://' end) + b.APP_SVR + '/' + b.APP_PATH + '/' + a.MENU_PATH " +
                                        "from M_MENU a " +
                                        "inner join M_APPS b on a.APP_CODE=b.CODE " +
                                        "where " +
                                        "a.APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                        "and a.MENU_CODE='" + menu + "'";
                    conn.ExecuteQuery();

                    if (param != null)
                        param = Crypto.DecryptStringAES(param);
                    else
                        param = "";

                    string path = conn.GetFieldValue("PATH").ToString() + param;
                    Response.Redirect(path);
                }
                else
                {

                    Response.Redirect("Standard/main.aspx");

                }
            }
        }
    }
}