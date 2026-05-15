using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Standard
{
    public partial class BodyMenu : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["URL"] == "")
                {
                    SetupMenu();
                    return;
                }
                else
                {
                    try
                    {
                        string menucode = Request.QueryString["URL"].ToString();
                        if (menucode == "Logout")
                        {
                            return;
                        }

                        string frameScript = "<script language='javascript'>window.parent.frames['headercontent'].location='title.aspx?URL=" + menucode + "';</script>";
                        Response.Write(frameScript);


                    }
                    catch { }
                }
            }
        }


        protected void SetupMenu()
        {
            try
            {
                conn.QueryString = "exec SP_MENUROLES_REMOTE '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "'," + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
                conn.ExecuteQuery();
            }
            catch (SystemException ex)
            {
                LB_MENU.Text = ex.Message;
            }

            try
            {
                if (conn.GetRowCount() > 0)
                {
                    LB_MENU.Text = "<TABLE>";
                    for (int i = 0; i < conn.GetRowCount(); i++)
                        LB_MENU.Text = LB_MENU.Text + conn.GetFieldValue(i, 0).ToString();
                    LB_MENU.Text = LB_MENU.Text +
                                    "<TR><TD></TD></TR>" +
                                    "</TABLE>";

                }
            }
            catch
            {

            }
        }
    }
}