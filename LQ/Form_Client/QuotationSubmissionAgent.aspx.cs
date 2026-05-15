using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationSubmissionAgent : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
                if (role == "99")
                {
                    Response.Redirect("QuotationSubmission.aspx?code=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                }

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "d.CODE, " +
                                "d.DESCR " +
                                "from		UWBOX.dbo.V_PARAM_PRODUCT_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER_CHANNEL_DISTRIBUTION b on a.PRODUCT_CODE = b.PRODUCT_CODE " +
                                "inner join	MARKETING.dbo.PARAM_SUB_CHANNEL_DISTRIBUTION c on b.SUBCD = c.SUB_CODE collate database_default " +
                                "inner join	MARKETING.dbo.PR_MARKET_SEGMENT d on c.MARKET_SEGMENT = d.CODE " +
                                "where " +
                                "a.SEGMENT = 0 " +
                                "order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLLevel();
            FillDGR();
        }

        protected void FillDDLLevel()
        {
            DDL_LEVEL.Items.Clear();
            conn.QueryString = "select SUB_CODE, DESCR from MARKETING.dbo.PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_AGENT_SUBMISSION " +
                                "'" + DDL_LEVEL.SelectedValue + "', " +
                                "'%" + TXT_CODE.Text.Trim() + "%', " +
                                "'%" + TXT_NAME.Text.Trim() + "%', " +
                                "'%" + TXT_AGENCY.Text.Trim() + "%' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lb.Text = DGR.Items[i].Cells[1].Text;

                Label lbWarn = (Label)DGR.Items[i].FindControl("LB_WARN");

                if (DGR.Items[i].Cells[7].Text.Trim() == "0")
                {
                    lbWarn.Text = "LICENSE EXPIRED!";
                    DGR.Items[i].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("QuotationSubmission.aspx?code=" + e.Item.Cells[1].Text);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_UPLINER_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
        }

        protected void DDL_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

    }
}