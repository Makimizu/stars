using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GO
{
    public partial class Title : System.Web.UI.Page
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
                    string URL = Request.QueryString["URL"];

                    conn.QueryString = "select MENU_DESCR, MENU_PATH from M_MENU where MENU_CODE=" + URL;
                    conn.ExecuteQuery();
                    LBL_TITLE.Text = conn.GetFieldValue("MENU_DESCR").ToString().ToUpper();

                    string path = conn.GetFieldValue("MENU_PATH").ToString();
                    if (path.IndexOfAny(new char[] { '?' }) > -1)
                        path = path + "&menucode=" + URL;
                    else
                        path = path + "?menucode=" + URL;

                    string frameScript = "<script language='javascript'>window.parent.frames['content'].location.href='" + path + "';</script>";
                    Response.Write(frameScript);

                }
                catch
                {
                    try
                    {
                        conn.QueryString = "select APP_NAME from M_APPS where CODE=" + System.Configuration.ConfigurationManager.AppSettings["appid"];
                        conn.ExecuteQuery();
                        LBL_TITLE.Text = conn.GetFieldValue("APP_NAME").ToString().ToUpper();
                    }
                    catch { }
                }
            }
        }

    }
}