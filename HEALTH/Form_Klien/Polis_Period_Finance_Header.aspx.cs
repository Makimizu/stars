using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Finance_Header : System.Web.UI.Page
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
            conn.QueryString = "exec SP_POLICY_PERIOD_FINANCE '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_URL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_URL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReport();
        }

        protected void ShowReport()
        {
            Response.Write("<script language='javascript'>parent.polisfinancebody.location.href = '" + DDL_URL.SelectedValue + "';</script>");
        }
    }
}