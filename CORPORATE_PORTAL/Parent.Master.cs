using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CORPORATE_PORTAL
{
    public partial class Parent : System.Web.UI.MasterPage
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
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
                    //GoMenu(menuid);
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
                conn.QueryString = "select top 1 " +
                                    "LOGO       = a.SMALL_LOGO, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from V_LINK_SC_COMPANY a ";
                conn.ExecuteQuery();
                Page.Title = conn.GetFieldValue("COMPANY").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
                Page.Header.Controls.Add(link);

                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO");


                //conn.QueryString = "exec SP_GETMENU " +
                //                "'" + Session["s"].ToString() + "'";
                //conn.ExecuteQuery();
                //LB_MENU.Text = conn.GetFieldValue("MENU").ToString();
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
                                "NAME = b.FRONT_NAME " +
                                "from V_LINK_SEC_USER_LOG_HISTORY a " +
                                "inner join V_LINK_SC_M_USERS b on a.USER_CODE = b.CODE collate database_default " +
                                "where " +
                                "a.ROWID = '" + Session["s"] + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                Response.Redirect("logout.aspx");

            LB_USER.Text = conn.GetFieldValue("NAME").ToString();
            LB_USERNAME.Text = conn.GetFieldValue("NAME").ToString();
        }
    }
}