using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Data
{
    public partial class DataRemuneration : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_YEAR.Text = Request.QueryString["YEAR"].ToString();
                LB_MONTH.Text = Request.QueryString["MONTH"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select MON = DATENAME(MONTH,'" + LB_MONTH.Text + "/1/" + LB_YEAR.Text + "')";
            conn.ExecuteQuery();
            LB_TITLE.Text = "REMUNERATION DATA : <B>" + conn.GetFieldValue("MON").ToString() + " " + LB_YEAR.Text + "</B>";

            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                TR_AGENTNAME.Visible = false;
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_LINK_MARKETING_PERIOD_DETAIL " +
                                LB_YEAR.Text + "," +
                                LB_MONTH.Text + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "'" + TXT_AGENTNAME.Text.Trim() + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void TXT_AGENTNAME_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}