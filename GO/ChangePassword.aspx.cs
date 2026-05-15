using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace GO
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
                LB_SES.Text = Request.QueryString["s"];
                Setup();
            }
        }

        protected void Setup()
        {
            try
            {
                conn.QueryString = "select " +
                                    "NAME = UPPER(ISNULL(a.FRONT_NAME,'') + REPLACE(' ' + ISNULL(MID_NAME,'') + ' ','  ',' ') + ISNULL(a.LAST_NAME,'')), " +
                                    "a.PHOTO " +
                                    "from M_USERS a " +
                                    "inner join USER_LOG_HISTORY b on a.CODE=b.USER_CODE " +
                                    "where " +
                                    "b.ROWID='" + LB_SES.Text + "'";
                conn.ExecuteQuery();

                LB_NAME.Text = conn.GetFieldValue("NAME").ToString();
                IMG1.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "PHOTO");
                IMG1.Height = 100;
            }
            catch
            {
                return;
            }
        }

        protected bool CekOldPassword()
        {
            conn.QueryString = "select PASSWORD from M_USERS a " +
                                    "inner join USER_LOG_HISTORY b on a.CODE=b.USER_CODE " +
                                    "where b.ROWID='" + LB_SES.Text + "'";
            conn.ExecuteQuery();

            string pwd = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());
            if (TXT_PWD_OLD.Text != pwd)
            {
                LB_ERROR.Text = "Old Password is not valid";
                return false;
            }

            return true;
        }

        protected bool CekNewPassword()
        {
            if (TXT_PWD_NEW1.Text != TXT_PWD_NEW2.Text)
            {
                LB_ERROR.Text = "New Passwords are not matched";
                return false;
            }

            return true;
        }

        protected bool CekPasswordCriteria()
        {
            conn.QueryString = "select RESULT=dbo.UFN_CHECK_PASSWORD_REGULATION('" + TXT_PWD_NEW1.Text + "')";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("RESULT").ToString() == "0")
            {
                LB_ERROR.Text = "New Passwords are not matched criteria.<BR>" +
                                "Password Length should be minimum 8 characters.<BR>" +
                                "Password should be consisted by Alphanumeric (A-Z, a-z, 0-9) and Special Characters.";
                return false;
            }
            return true;
        }

        protected bool CekPasswordHistory()
        {
            conn.QueryString = "select top 3 PASSWORD " +
                                "from USER_PASSWORD_HISTORY a " +
                                "inner join USER_LOG_HISTORY b on a.USER_CODE=b.USER_CODE " +
                                "where " +
                                "b.ROWID='" + LB_SES.Text + "' " +
                                "order by CREATEDATE desc";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 1)
            {
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    string pwd = Crypto.DecryptStringAES(conn.GetFieldValue(i, "PASSWORD").ToString());
                    if (pwd == TXT_PWD_NEW1.Text)
                    {
                        LB_ERROR.Text = "Password should not be the same with the last 3 ones";
                        return false;
                    }
                }
            }

            return true;
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                if (!CekOldPassword())
                    return;

                if (!CekNewPassword())
                    return;

                if (!CekPasswordCriteria())
                    return;

                if (!CekPasswordHistory())
                    return;

                string pwd = Crypto.EncryptStringAES(TXT_PWD_NEW1.Text);
                conn.QueryString = "exec SSP_CHANGE_PASSWORD '" + LB_SES.Text + "','" + pwd + "'";
                conn.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }
        }
    }
}