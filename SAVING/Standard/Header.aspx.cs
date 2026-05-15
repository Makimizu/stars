using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace SAVING.Standard
{
    public partial class Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Authenticate()
        {
            try
            {
                string session = Session["s"].ToString();

                conn.QueryString = "select " +
                                        "PATH, " +
                                        "APP_NAME  " +
                                        "from V_ROLE_APPS a " +
                                        "inner join M_USERS b on a.ROLE_CODE=b.ROLE_CODE " +
                                        "inner join USER_LOG_HISTORY c on c.USER_CODE=b.CODE " +
                                        "where " +
                                        "a.APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                        "and c.ROWID = '" + session + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() == 0)
                {
                    conn.QueryString = "select URL='http://' + APP_SVR + '/' + APP_PATH + '/' from M_APPS where CODE='0'";
                    conn.ExecuteQuery();
                    string frameScript = "<script language='javascript'>window.top.location.href='" + conn.GetFieldValue("URL").ToString() + "Modul.aspx?s=" + session + "';</script>";
                    Response.Write(frameScript);
                }

            }
            catch
            {
                Logout();
            }
        }

        protected void Setup()
        {
            Authenticate();

            try
            {
                string session = Session["s"].ToString();
            }
            catch
            {
                Logout();
            }

            try
            {
                LB_SES.Text = Session["s"].ToString();

                conn.QueryString = "select " +
                                    "PATH, " +
                                    "APP_NAME  " +
                                    "from V_ROLE_APPS a " +
                                    "inner join M_USERS b on a.ROLE_CODE=b.ROLE_CODE " +
                                    "inner join USER_LOG_HISTORY c on c.USER_CODE=b.CODE " +
                                    "where " +
                                    "a.APP_CODE <> 'ZZZZ' " +
                                    "and a.APP_CODE <> '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                    "and c.ROWID = '" + LB_SES.Text + "' " +
                                    "order by 2";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    DDL_APP.Items.Clear();
                    DDL_APP.Items.Add(new ListItem("GO TO APPLICATION :", ""));
                    for (int i = 0; i < conn.GetRowCount(); i++)
                    {
                        DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                    }
                }
                else
                    DDL_APP.Visible = false;

                conn.QueryString = "select " +
                                    "NAME = UPPER(ISNULL(a.FRONT_NAME,'') + REPLACE(' ' + ISNULL(MID_NAME,'') + ' ','  ',' ') + ISNULL(a.LAST_NAME,'')),  " +
                                    "ROLE_DESCR = c.DESCR,  " +
                                    "LOGIN = convert(varchar(30),b.LOGIN,113), " +
                                    "PHOTO = isnull(a.PHOTO,(select top 1 DEFAULT_PROFILE_PIC from SC_COMPANY)), " +
                                    "LOGO = (select top 1 SMALL_LOGO from SC_COMPANY) " +
                                    "from M_USERS a  " +
                                    "inner join USER_LOG_HISTORY b on a.CODE=b.USER_CODE  " +
                                    "inner join M_ROLES c on a.ROLE_CODE=c.CODE " +
                                    "where  " +
                                    "b.ROWID='" + Session["s"].ToString() + "'";
                conn.ExecuteQuery();

                LBL_USER.Text = conn.GetFieldValue("NAME").ToString() +
                                " (" +
                                conn.GetFieldValue("ROLE_DESCR").ToString() +
                                ")<BR>Login Since: " +
                                conn.GetFieldValue("LOGIN").ToString();

                IMG1.ImageUrl = GlobalUse.GetStringImageURL(conn, conn.QueryString, "PHOTO");
                IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn, conn.QueryString, "LOGO");

                conn.QueryString = "select URL = (case when APP_SVR not like 'https%' then 'http://' else '' end) + APP_SVR + '/' + APP_PATH + '/ChangePix.aspx?userid=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' from M_APPS where CODE = '0'";
                conn.ExecuteQuery();
                IMG1.Attributes.Add("onclick", "window.open('" + conn.GetFieldValue("URL").ToString() + "','PHOTO','height=130px,width=300px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
            catch { }
        }

        protected void LB_LOGOUT_Click(object sender, EventArgs e)
        {
            Logout();
        }

        protected void Logout()
        {
            Session.Clear();

            conn.QueryString = "select URL = (case when APP_SVR like 'https://%' then '' else 'http://' end) + APP_SVR + '/' from M_APPS where CODE='0'";
            conn.ExecuteQuery();

            try
            {
                string frameScript = "<script language='javascript'>window.top.location.href='" + conn.GetFieldValue("URL").ToString() + "logout.aspx?s=" + Session["s"].ToString() + "';</script>";
                Response.Write(frameScript);
            }
            catch
            {
                string frameScript = "<script language='javascript'>window.top.location.href='" + conn.GetFieldValue("URL").ToString() + "login.aspx';</script>";
                Response.Write(frameScript);
            }
        }

        protected void TM_SESSION_Tick(object sender, EventArgs e)
        {
            try
            {
                string s = Session["s"].ToString();
            }
            catch
            {
                Logout();
            }
        }

        protected void LB_PASSWORD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL='http://' + APP_SVR + '/' + APP_PATH + '/' from M_APPS where CODE='0'";
            conn.ExecuteQuery();

            string linkAction = GlobalUse.PopupCenterWindow(conn.GetFieldValue("URL").ToString() + "ChangePassword.aspx?s=" + LB_SES.Text, "Change_Password", 400, 300);
            Response.Write(linkAction);
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_APP.SelectedValue != "")
            {
                string frameScript = "<script language='javascript'>window.top.location.href='" + DDL_APP.SelectedValue + "?s=" + LB_SES.Text + "';</script>";
                Response.Write(frameScript);
            }
        }
    }
}