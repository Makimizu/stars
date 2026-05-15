using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationPrintHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text =  Request.QueryString["quotno"];
                LB_VERNO.Text = Request.QueryString["verno"];
                Setup();
                ShowReport();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_SC_REPORT_LIST where SHARE='1'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REPORT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void ShowReport()
        {
            string URL = "&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + LB_VERNO.Text + "&rc:Parameters=False";
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='" + DDL_REPORT.SelectedValue + "'";
            conn.ExecuteQuery();

            URL = conn.GetFieldValue("URL").ToString() + URL;

            //System.Threading.Thread.Sleep(1000);            
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write("<script language='javascript'>parent.printbody.location.href = '" + URL + "';</script>");            
        }

        protected void DDL_REPORT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReport();
        }

        protected void BT_EXPORT_Click(object sender, EventArgs e)
        {
            string URL = "&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + LB_VERNO.Text + "&rs:Format=" + DDL_FORMAT.SelectedValue;
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='" + DDL_REPORT.SelectedValue + "'";
            conn.ExecuteQuery();
            URL = conn.GetFieldValue("URL").ToString() + URL;
                        
            Response.Redirect(URL);            
        }
    }
}