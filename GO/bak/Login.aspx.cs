using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using DMS.DBConnection;

namespace GO
{
    public partial class Login : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "select top 1 " +
                                    "LOGO       = a.BIG_LOGO, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SC_COMPANY a ";
                conn.ExecuteQuery();
                Page.Title = conn.GetFieldValue("COMPANY").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
                Header.Controls.Add(link);

                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO");

                //conn.QueryString = "select DISCLAIMER = replace(replace(DISCLAIMER,char(13),'<BR>'),'<BR><BR>','<BR>') from SC_COMPANY";
                conn.QueryString = "select DISCLAIMER = '' from SC_COMPANY";
                conn.ExecuteQuery();
                LB_NDA.Text = conn.GetFieldValue("DISCLAIMER").ToString();
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_MSG.Text = "";


            try
            {
                conn.QueryString = "select PASSWORD, LOCKED=isnull(LOCKED,0), NOEXP=dbo.UFN_CHECK_PASSWORD_EXPIRATION(CODE) from M_USERS where CODE='" + TXT_UID.Text.Trim() + "' and isnull(ACTIVE,0)>0";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_MSG.Text = "<BR><BR><BR>" + ex.Message;
                return;
            }

            if (conn.GetRowCount() == 0)
            {
                LB_MSG.Text = "<BR><BR><BR>User ID is not available";
                return;
            }

            string EXP = conn.GetFieldValue("NOEXP").ToString();

            if (conn.GetFieldValue("LOCKED") != "0")
            {
                LB_MSG.Text = "<BR><BR><BR>User ID is LOCKED";
                return;
            }

            string PWD = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());
            if (PWD != TXT_PWD.Text.Trim())
            {
                LB_MSG.Text = "<BR><BR><BR>Password is invalid";
                SSP_LOG_ATTEMPT(TXT_UID.Text.Trim(), "0");
                return;
            }

            LB_MSG.Text = "<BR><BR><BR>Authorization succed !";
            SSP_LOG_ATTEMPT(TXT_UID.Text.Trim(), "1");

            string result = SSP_LOG_HISTORY_IN(TXT_UID.Text.Trim());
            if (result.Length > 0)
            {
                LB_MSG.Text = result;
                return;
            }

            string session = GetSession(TXT_UID.Text.Trim());
            if (session == "")
                return;

            if (EXP == "0")
            {
                LB_MSG.Text = "<BR><BR><BR>Password is expired !";
                string linkAction = GlobalUse.PopupCenterWindow("ChangePassword.aspx?s=" + session, "Change_Password", 400, 300);
                Response.Write(linkAction);
                return;
            }

            Response.Redirect("Modul.aspx?s=" + session);
        }

        protected string GetSession(string UID)
        {
            string result = "";

            try
            {
                conn.QueryString = "select top 1 ROWID from USER_LOG_HISTORY where USER_CODE='" + UID + "' and LOGOUT is null order by LOGIN desc";
                conn.ExecuteQuery();
                result = conn.GetFieldValue("ROWID").ToString();
            }
            catch { }

            return result;
        }

        protected void SSP_LOG_ATTEMPT(string UserID, string Success)
        {
            try
            {
                conn.QueryString = "exec SSP_LOG_ATTEMPT '" + UserID + "'," + Success;
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected string SSP_LOG_HISTORY_IN(string UserID)
        {
            string result = "";
            string stTerminalIP = Request.ServerVariables["REMOTE_ADDR"];

            try
            {
                conn.QueryString = "exec SSP_LOG_HISTORY_IN '" + UserID + "','" + stTerminalIP + "'";
                conn.ExecuteQuery();

                result = conn.GetFieldValue("RESULT").ToString();
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}