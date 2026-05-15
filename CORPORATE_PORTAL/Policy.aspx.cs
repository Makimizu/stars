using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using Microsoft.Reporting.WebForms;

namespace CORPORATE_PORTAL
{
    public partial class Policy : System.Web.UI.Page
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
                Setup();
                LoadPeriod();
            }
        }

        protected void Setup()
        {
            LB_APPID.Text = Request.QueryString["APPID"];
            LB_REPORTCODE.Text = Request.QueryString["CODE"];

            conn.QueryString = "select ID,DESCR = CONVERT(VARCHAR(11), START_DATE, 13)+' - '+CONVERT(VARCHAR(11), END_DATE, 13) " +
                                "FROM dbo.V_LINK_HO_POLICY_PERIOD " +
                                "WHERE POLICY_ID='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ORDER BY ORDER_PERIOD DESC";
            conn.ExecuteQuery();
            DDL_PERIOD.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPeriod();
        }

        protected void LoadPeriod()
        {
            conn.QueryString = "select " +
                                "POLICY_NO, " +
                                "TIPE_DESCR, " +
                                "MOP_DESCR, " +
                                "PRODUCT = upper(PRODUCT), " +
                                "TPA_DESCR, " +
                                "AGENT_NAME = upper(b.FRONT_NAME), " +
                                "b.EMAIL, " +
                                "PROCESSDATE " +
                                "from V_LINK_HO_POLICY_PERIOD a " +
                                "inner join V_LINK_MR_M_AGENTS b on a.AGENT_CODE = b.CODE  " +
                                "where " +
                                "a.ID = '" + DDL_PERIOD.SelectedValue + "'";
            conn.ExecuteQuery();

            LB_MOP.Text = conn.GetFieldValue("MOP_DESCR").ToString();
            LB_POLNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PROCDATE.Text = conn.GetFieldValue("PROCESSDATE").ToString();
            LB_PROD.Text = conn.GetFieldValue("PRODUCT").ToString();
            LB_TPA.Text = conn.GetFieldValue("TPA_DESCR").ToString();
            LB_TYPE.Text = conn.GetFieldValue("TIPE_DESCR").ToString();


            conn.QueryString = "select " +
                                "URL = 'http://localhost/reportserver', " +
                                "PATH = '/' + a.FOLDER + '/' + a.REPORT_NAME " +
                                "from V_LINK_SC_REPORT_LIST a " +
                                "where " +
                                "a.APP_ID = '" + LB_APPID.Text + "' " +
                                "and a.CODE = " + LB_REPORTCODE.Text;
            conn.ExecuteQuery();

            RV1.ProcessingMode = ProcessingMode.Remote;
            ServerReport serverReport = RV1.ServerReport;
            serverReport.ReportServerUrl = new Uri(conn.GetFieldValue("URL").ToString());
            serverReport.ReportPath = conn.GetFieldValue("PATH").ToString();
            ReportParameter prm = new ReportParameter();
            prm.Name = "POLICY_PERIOD_ID";
            prm.Values.Add(DDL_PERIOD.SelectedValue);
            RV1.ServerReport.SetParameters(new ReportParameter[] { prm });
        }
    }
}