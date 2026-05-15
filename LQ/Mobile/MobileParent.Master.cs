using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LQ.Mobile
{
    public partial class MobileParent : System.Web.UI.MasterPage
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Login.aspx");
                }

                conn.QueryString = "select top 1 " +
                                    "LOGO       = a.BIG_LOGO, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SECURITY.dbo.SC_COMPANY a ";
                conn.ExecuteQuery();

                Page.Title = conn.GetFieldValue("COMPANY").ToString();
                //IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO");

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
                Page.Header.Controls.Add(link);

                conn.QueryString = "select " +
                                    "FULLNAME	= UPPER(LTRIM(RTRIM(isnull(a.FRONT_NAME, '') + ' ' + isnull(a.MID_NAME, '') + ' ' +  isnull(a.LAST_NAME, '')))), " +
                                    "PHOTO		= a.PHOTO " +
                                    "from		MARKETING.dbo.V_M_AGENTS a " +
                                    "inner join	SECURITY.dbo.USER_LOG_HISTORY b on a.CODE = b.USER_CODE collate database_default and b.ROWID = '" + Session["s"].ToString() + "'";
                conn.ExecuteQuery();

                LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
                try
                {
                    IMG_PHOTO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO");
                }
                catch { }
            }
        }
    }
}