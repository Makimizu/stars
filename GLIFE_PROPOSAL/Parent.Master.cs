using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE_PROPOSAL
{
    public partial class Parent : System.Web.UI.MasterPage
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected Connection connsec = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string menuid = "";
                try
                {
                    menuid = Request.QueryString["menuid"];
                }
                catch { }

                try
                {
                    Setup();
                    CheckSession();
                }
                catch
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        protected void Logout()
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void Setup()
        {
            try
            {
                conn.QueryString = "exec SP_LINK_SC_GETMENU " +
                                "'" + Session["s"].ToString() + "'";
                conn.ExecuteQuery();
                LB_MENU.Text = conn.GetFieldValue("MENU").ToString();


                connsec.QueryString = "select top 1 " +
                                        "LOGO       = a.SMALL_LOGO, " +
                                        "COMPANY_NAME	= a.NAME, " +
                                        "ICON		= a.ICON " +
                                        "from SECURITY.dbo.SC_COMPANY a ";
                connsec.ExecuteQuery();

                Page.Title = connsec.GetFieldValue("COMPANY_NAME").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(connsec.QueryString, "ICON"));
                Page.Header.Controls.Add(link);

                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(connsec.QueryString, "LOGO");
            }
            catch { }

            if (Request.Browser.IsMobileDevice)
            {
                MobileMode();
            }
        }

        protected void MobileMode()
        {
            DV_CONTAINER_FLUID.Attributes.Remove("class");
        }

        protected void CheckSession()
        {

            conn.QueryString = "select " +
                                "NAME = LTRIM(RTRIM(isnull(b.FRONT_NAME,'') + ' ' + isnull(b.LAST_NAME,''))) " +
                                "from V_LINK_SC_USER_LOG_HISTORY a " +
                                "inner join V_LINK_SC_M_USERS b on a.USER_CODE = b.CODE collate database_default " +
                                "where " +
                                "a.ROWID = '" + Session["s"] + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                Response.Redirect("logout.aspx");

            LB_USER.Text = conn.GetFieldValue("NAME").ToString();

        }
    }
}