using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Print_Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["PolicyPeriod"];
                Setup();
                ShowReport();
            }
        }

        protected void Setup()
        {
            //conn.QueryString = "select URL = URL + '&rc:Parameters=False&POLICY_PERIOD_ID=" + LB_ID.Text + "', DESCR from V_LINK_SC_REPORT_LIST where CODE in ('289','280','281','282','327','332','336') order by DESCR";
            conn.QueryString = "select " +
                                "URL = URLAPP + '&POLICY_PERIOD_ID=" + LB_ID.Text + "', " +
                                "DESCR = a.DESCR " +
                                "from V_LINK_SC_REPORT_LIST a " +
                                "inner join V_LINK_SC_M_APPS b on b.CODE = '0' " +
                                "where a.CODE in ('289','280','281','282','327','332','370','378','379') order by a.DESCR";
            conn.ExecuteQuery();

            //DDL_PRINT.Items.Add(new ListItem("POLIS - IKHTISAR", "Polis_Print.aspx?PolicyPeriod=" + LB_ID.Text));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRINT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_PRINT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReport();
        }

        protected void ShowReport()
        {
            Response.Write("<script language='javascript'>parent.polisprintbody.location.href = '" + DDL_PRINT.SelectedValue + "';</script>");
        }
    }
}