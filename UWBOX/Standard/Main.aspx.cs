using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace UWBOX.Standard
{
    public partial class Main : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "select top 1 " +
                                    "APP_NAME	= b.APP_NAME, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SC_COMPANY a " +
                                    "inner join M_APPS b on b.CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"].ToString() + "'";
                conn.ExecuteQuery();
                Page.Title = conn.GetFieldValue("APP_NAME").ToString() + " - " + conn.GetFieldValue("COMPANY").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn, conn.QueryString, "ICON"));
                Header.Controls.Add(link);
            }
        }
    }
}