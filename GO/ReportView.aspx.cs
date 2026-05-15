using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;

namespace GO
{
    public partial class ReportView : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "select top 1 " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON, " +
                                    "WALLPAPER  = a.WALLPAPER " +
                                    "from SC_COMPANY a ";
                conn.ExecuteQuery();
                Page.Title = conn.GetFieldValue("COMPANY").ToString();

                System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                link.Attributes.Add("type", "image/vnd.microsoft.icon");
                link.Attributes.Add("rel", "icon");
                link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
                Header.Controls.Add(link);

                Setup();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString.Count == 0)
                return;

            

            NameValueCollection parameters = Request.QueryString;
            ReportParameter[] prm;            

            conn.QueryString = "select ParameterName from V_REPORT_PARAMETER " +
                                "where " +
                                "APP_ID = '" + Request.QueryString["APPID"] + "' " +
                                "and CODE = " + Request.QueryString["CODE"];
            conn.ExecuteQuery();
            prm = new ReportParameter[conn.GetRowCount()];

            int paramseq = 0;

            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters.GetKey(i) != "APPID" && parameters.GetKey(i) != "CODE")
                {
                    prm[paramseq] = new ReportParameter(parameters.GetKey(i),parameters.GetValues(i)[0]);
                    paramseq = paramseq + 1;
                }
            }

            
            RV.ServerReport.ReportServerCredentials = new ReportServerCredentials();

            conn.QueryString = "select " +
                                "URL = replace(URL, '/Pages/ReportViewer.aspx?%2f' + FOLDER + '%2f' + REPORT_NAME + '&rs:Command=Render',''), " +
                                "PATH = '/' + a.FOLDER + '/' + a.REPORT_NAME " +
                                "from REPORT_LIST a " +
                                "where " +
                                "a.APP_ID = '" + Request.QueryString["APPID"] + "' " +
                                "and a.CODE = " + Request.QueryString["CODE"];
            conn.ExecuteQuery();
            
            RV.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = RV.ServerReport;
            serverReport.ReportServerUrl = new Uri(conn.GetFieldValue("URL").ToString());
            serverReport.ReportPath = conn.GetFieldValue("PATH").ToString();

            if (paramseq > 0)
            {
                RV.ShowParameterPrompts = false;
                RV.ServerReport.SetParameters(prm);
            }
        }
    }
}