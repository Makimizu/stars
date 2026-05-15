using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using Microsoft.Reporting.WebForms;


namespace LIFE.Form_Tools
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
                LB_APPID.Text = Request.QueryString["APPID"];
                LB_REPORTCODE.Text = Request.QueryString["CODE"];
                LB_PARAM.Text = Request.QueryString["PARAM"];
                LB_VALUE.Text = Request.QueryString["VALUE"];

                LoadReport();
            }
        }

        protected void LoadReport()
        {
            conn.QueryString = "select " +
                                "URL = replace(URL, '/Pages/ReportViewer.aspx?%2f' + FOLDER + '%2f' + REPORT_NAME + '&rs:Command=Render',''), " +
                                "PATH = '/' + a.FOLDER + '/' + a.REPORT_NAME " +
                                "from REPORT_LIST a " +
                                "where " +
                                "a.APP_ID = '" + LB_APPID.Text + "' " +
                                "and a.CODE = " + LB_REPORTCODE.Text;
            conn.ExecuteQuery();

            RV1.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = RV1.ServerReport;
            serverReport.ReportServerUrl = new Uri(conn.GetFieldValue("URL").ToString());
            serverReport.ReportPath = conn.GetFieldValue("PATH").ToString();
            ReportParameter prm = new ReportParameter();
            prm.Name = LB_PARAM.Text;
            prm.Values.Add(LB_VALUE.Text);
            RV1.ServerReport.SetParameters(new ReportParameter[] { prm });
        }
    }
}