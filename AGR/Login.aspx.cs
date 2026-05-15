using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using DMS.DBConnection;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Services;
using AGR.Apps.Core.UnitOfWorks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AGR
{
    public partial class Login : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));

        static string sharedSecret = System.Configuration.ConfigurationManager.AppSettings["enckey"];
        static IEncryptionService encryptionService = EncryptionService.CreateEncryptionService(sharedSecret);
        static string stringEnc = System.Configuration.ConfigurationManager.AppSettings["conn"];
        static string connString = encryptionService.Decrypt(stringEnc);
        private readonly IUnitOfWork unitOfWork = new UnitOfWork(connString, sharedSecret);
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_MENUCODE.Text = !string.IsNullOrEmpty(Session["MENUCODE"] as string) ? Request.QueryString["MENUCODE"].ToString() : (!string.IsNullOrEmpty(Request.QueryString["MENUCODE"] as string) ? Request.QueryString["MENUCODE"].ToString() : "");
                    Lbl_SysCode.Text = !string.IsNullOrEmpty(Request.QueryString["SysCode"] as string) ? Request.QueryString["SysCode"].ToString() : "";
                }
                catch { }

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
                TXT_UID.Focus();
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
            GenerateToken(session);
            Proceed(session);
        }

        protected void Proceed(string s)
        {
            Session.Remove("s");
            Session.Add("s", s);
            
            if (LB_MENUCODE.Text != "")
            {
                conn.QueryString = "select MENU_CODE from SECURITY.dbo.M_MENU where APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' and MENU_CODE = '" + LB_MENUCODE.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    Session.Remove("MENUCODE");
                    Session.Add("MENUCODE", LB_MENUCODE.Text);
                }
            }

            if(Lbl_SysCode.Text != "")
            {
                Session.Remove("SysCode");
                Session.Add("SysCode", Lbl_SysCode.Text);
            }

            

            Response.Redirect("Standard/main.aspx");

            /*
            if (menu != null)
            {
                Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
                conn.QueryString = "select " +
                                    "PATH = (case when b.APP_SVR like 'https://%' then '' else 'http://' end) + b.APP_SVR + '/' + b.APP_PATH + '/' + a.MENU_PATH " +
                                    "from M_MENU a " +
                                    "inner join M_APPS b on a.APP_CODE=b.CODE " +
                                    "where " +
                                    "a.APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                    "and a.MENU_CODE='" + menu + "'";
                conn.ExecuteQuery();

                if (param != null)
                    param = Crypto.DecryptStringAES(param);
                else
                    param = "";

                string path = conn.GetFieldValue("PATH").ToString() + param;
                Response.Redirect(path);
            }
            else
            {
                Response.Redirect("Standard/main.aspx");
            }
            */
        }

        private void GenerateToken(string sessionid)
        {
            using (var client = new HttpClient())
            {
                var data = unitOfWork.IdentityService.GenerateTokenBySessionid(sessionid);
                if (data.IsSuccess && data.Data.Token != null) {
                    Session.Remove("validtoken");
                    Session.Add("validtoken", data.Data.Token);
                }
                //example call api
                /*var urlService = unitOfWork.SystemConfigurationService.GetSysconfigValue("EMAIL", "JOBSMAIL", "URLMAIL");
                
                var request = new HttpRequestMessage(HttpMethod.Get, urlService);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", data.Data.Token);
                HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string apiString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Session.Remove("validtoken");
                    Session.Add("validtoken", data.Data.Token);
                }*/
            }
        }

        private async Task GetDataFromApi()
        {
            string apiUrl = "http://remunservice.takaful.com/api/Mail/SendMail"; // Replace with your API URL
            var data = unitOfWork.IdentityService.GenerateTokenBySessionid(Session["s"].ToString());
            string bearerToken = data.Data.Token; // Replace with your actual Bearer token

            using (HttpClient client = new HttpClient())
            {
                // Set Authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Use the responseData (e.g., display or bind to UI)
                        Response.Write("<pre>" + Server.HtmlEncode(responseData) + "</pre>");
                    }
                    else
                    {
                        Response.Write("Error: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Exception: " + ex.Message);
                }
            }
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