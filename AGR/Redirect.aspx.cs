using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGR.Apps.Core.UnitOfWorks;
using DMS.DBConnection;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Services;

namespace AGR
{
    public partial class Redirect : System.Web.UI.Page
    {
        #region PrivateVariables
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
                string s = Request.QueryString["s"];
                string menu = Request.QueryString["menu"];
                string param = Request.QueryString["param"];
                Session.Remove("s");
                Session.Add("s", s);

                GenerateToken(s);

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

                //use for uat only redirect pointing to db starsfix
                //Session.Remove("s");
                //Response.Redirect("Login.aspx");
            }
        }

        private void GenerateToken(string sessionid)
        {
            using (var client = new HttpClient())
            {
                var data = unitOfWork.IdentityService.GenerateTokenBySessionid(sessionid);
                if (data.IsSuccess && data.Data.Token != null)
                {
                    Session.Remove("validtoken");
                    Session.Add("validtoken", data.Data.Token);
                }
            }
        }
    }
}