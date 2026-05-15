using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;

namespace ReportViewer
{
    public partial class Viewer : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*
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
                */
                Setup();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString.Count == 0)
                return;

            NameValueCollection parameters = Request.QueryString;
            ReportParameter[] prm;

            conn.QueryString = "select " +
                                "URL = replace(b.URL, '/Pages/ReportViewer.aspx?%2f' + b.FOLDER + '%2f' + b.REPORT_NAME + '&rs:Command=Render',''),  " +
                                "PATH = '/' + b.FOLDER + '/' + b.REPORT_NAME, " +
                                "ParameterName = a.ParameterName  " +
                                "from REPORT_LIST b " +
                                "left join V_REPORT_PARAMETER a on a.APP_ID = b.APP_ID and a.CODE = b.CODE " +
                                "where " +
                                "b.APP_ID = '" + Request.QueryString["APPID"] + "' " +
                                "and b.CODE = " + Request.QueryString["CODE"];
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString();
            string PATH = conn.GetFieldValue("PATH").ToString();
            prm = new ReportParameter[conn.GetRowCount()];

            int paramseq = 0;
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                for (int j = 0; j < parameters.Count; j++)
                {
                    if (parameters.GetKey(j) == conn.GetFieldValue(i, "ParameterName").ToString())
                    {
                        prm[paramseq] = new ReportParameter(parameters.GetKey(j), parameters.GetValues(j)[0]);
                        paramseq = paramseq + 1;
                        continue;
                    }
                }
            }

            RV.ServerReport.ReportServerCredentials = new ReportServerCredentials();


            RV.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = RV.ServerReport;
            serverReport.ReportServerUrl = new Uri(URL);
            serverReport.ReportPath = PATH;

            if (paramseq > 0)
            {
                RV.ShowParameterPrompts = false;
                RV.ServerReport.SetParameters(prm);
            }
        }
    }
}