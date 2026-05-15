using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE_PROPOSAL
{
    public partial class Login : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Setup();
        }

        protected void Setup()
        {

            try
            {
                conn.QueryString = "select top 1 " +
                                    "LOGO       = a.BIG_LOGO, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SECURITY.dbo.SC_COMPANY a ";
                conn.ExecuteQuery();
                Page.Title = conn.GetFieldValue("COMPANY").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
                Header.Controls.Add(link);

                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO");


            }
            catch { }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_MSG.Text = "";
            SubmitClick();
        }

        public void SubmitClick()
        {
            try
            {
                conn.QueryString = "select PASSWORD, ACTIVE from V_LINK_SC_M_USERS where CODE='" + TXT_UID.Text.Trim() + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_MSG.Text = ex.Message;
                return;
            }

            if (conn.GetRowCount() == 0)
            {
                LB_MSG.Text = "User ID is not available";
                return;
            }

            string EXP = conn.GetFieldValue("ACTIVE").ToString();

            if (conn.GetFieldValue("ACTIVE") != "1")
            {
                LB_MSG.Text = "User ID is NOT ACTIVE";
                return;
            }

            string PWD = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());
            if (PWD != TXT_PWD.Text.Trim())
            {
                LB_MSG.Text = "Password is invalid";
                SSP_LOG_ATTEMPT(TXT_UID.Text.Trim(), "0");
                return;
            }

            LB_MSG.Text = "Authorization succeed !";
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


            Session.Remove("s");
            Session.Add("s", session);
            Response.Redirect("Welcome.aspx");
        }

        protected string GetSession(string UID)
        {
            string result = "";

            try
            {
                conn.QueryString = "select top 1 ROWID from V_LINK_SEC_USER_LOG_HISTORY where USER_CODE='" + UID + "' and LOGOUT is null order by LOGIN desc";
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
                conn.QueryString = "exec SP_LINK_SEC_LOG_ATTEMPT '" + UserID + "'," + Success;
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
                conn.QueryString = "exec SP_LINK_SEC_LOG_HISTORY_IN '" + UserID + "','" + stTerminalIP + "'";
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