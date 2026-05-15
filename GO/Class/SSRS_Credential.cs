using System;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;
using DMS.DBConnection;


public class ReportServerCredentials : IReportServerCredentials
{
    public WindowsIdentity ImpersonationUser
    {
        get
        {
            // Use the default Windows user.  Credentials will be
            // provided by the NetworkCredentials property.
            return null;
        }
    }

    public ICredentials NetworkCredentials
    {
        get
        {
            // Read the user information from the Web.config file.  
            // By reading the information on demand instead of 
            // storing it, the credentials will not be stored in 
            // session, reducing the vulnerable surface area to the
            // Web.config file, which can be secured with an ACL.

            // User name
            Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
            conn.QueryString = "select " +
                                "DOMAIN = (select VALUE from SC_GENERAL_SET where APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"]  + "' and PARAMETER = 'REPORT_IMPERSONATE_DOMAIN'), " +
                                "USERID = (select VALUE from SC_GENERAL_SET where APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' and PARAMETER = 'REPORT_IMPERSONATE_USERID'), " +
                                "PASSWORD = (select VALUE from SC_GENERAL_SET where APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' and PARAMETER = 'REPORT_IMPERSONATE_PASSWORD')";
            conn.ExecuteQuery();

            string userName = conn.GetFieldValue("USERID").ToString();
            string password = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());
            string domain = conn.GetFieldValue("DOMAIN").ToString();

            return new NetworkCredential(userName, password, domain);
        }
    }

    public bool GetFormsCredentials(out Cookie authCookie,
                out string userName, out string password,
                out string authority)
    {
        authCookie = null;
        userName = null;
        password = null;
        authority = null;

        // Not using form credentials
        return false;
    }
}


