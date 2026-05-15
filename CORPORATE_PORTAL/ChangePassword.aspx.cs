using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CORPORATE_PORTAL
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
            }
        }

        protected void BT_PASSWORD_Click(object sender, EventArgs e)
        {
            LB_PASSWORD_RESULT.Text = "";

            conn.QueryString = "select PASSWORD from V_LINK_SC_M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            string password = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());

            if (password != TXT_CURRPWD.Text.Trim())
            {
                LB_PASSWORD_RESULT.ForeColor = System.Drawing.Color.Red;
                LB_PASSWORD_RESULT.Text = "Old Password is incorrect";
                return;
            }

            if (TXT_NEWPWD.Text.Trim().Length < 8)
            {
                LB_PASSWORD_RESULT.ForeColor = System.Drawing.Color.Red;
                LB_PASSWORD_RESULT.Text = "New Password should be minimum 8 characters long";
                return;
            }

            if (TXT_NEWPWD.Text.Trim() != TXT_NEWPWD2.Text.Trim())
            {
                LB_PASSWORD_RESULT.ForeColor = System.Drawing.Color.Red;
                LB_PASSWORD_RESULT.Text = "New Password verification is not the same";
                return;
            }

            try
            {
                conn.QueryString = "update SECURITY.dbo.M_USERS set PASSWORD = '" + Crypto.EncryptStringAES(TXT_NEWPWD.Text.Trim()) + "' where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                LB_PASSWORD_RESULT.ForeColor = System.Drawing.Color.Blue;
                LB_PASSWORD_RESULT.Text = "Password is changed succesfully";
            }
            catch (SystemException ex)
            {
                LB_PASSWORD_RESULT.ForeColor = System.Drawing.Color.Red;
                LB_PASSWORD_RESULT.Text = ex.Message;
            }
        }
    }
}