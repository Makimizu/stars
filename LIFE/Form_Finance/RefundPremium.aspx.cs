using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class RefundPremium : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGRSummary();
                FillDGR();
            }
        }

        protected void FillDGRSummary()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_REJECT_CANCEL_SUMMARY";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SUMMARY.DataSource = dt;
            DGR_SUMMARY.DataBind();

            for (int i = 0; i < DGR_SUMMARY.Items.Count; i++)
            {
                Button bt = (Button)DGR_SUMMARY.Items[i].FindControl("BT_POLSTAT");
                bt.Text = DGR_SUMMARY.Items[i].Cells[1].Text;

                if (DGR_SUMMARY.Items[i].Cells[3].Text == "0")
                {
                    bt.Enabled = false;
                }
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_REJECT_CANCEL " +
                                "'" + LB_POLSTAT.Text + "'," +
                                "'" + TXT_REGNO.Text.Trim() + "'," +
                                "'" + TXT_FULLNAME.Text.Trim() + "'";
            conn.ExecuteQuery(500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select BICODE, BANK from V_FINANCE_BANK order by BICODE";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtACCNO = (TextBox)DGR.Items[i].FindControl("TXT_ACCNO");
                TextBox txtACCNAME = (TextBox)DGR.Items[i].FindControl("TXT_ACCNAME");
                DropDownList ddlACCBANK = (DropDownList)DGR.Items[i].FindControl("DDL_ACCBANK");
                Button bt = (Button)DGR.Items[i].FindControl("BT_PROCESS");

                ddlACCBANK.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlACCBANK.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtACCNO.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtACCNAME.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                try
                {
                    ddlACCBANK.SelectedValue = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }

                bt.Attributes.Add("onclick", "if(!confirm('Are you sure to REFUND ?')){return false;};");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Refund")
            {
                TextBox txtACCNO = (TextBox)e.Item.FindControl("TXT_ACCNO");
                TextBox txtACCNAME = (TextBox)e.Item.FindControl("TXT_ACCNAME");
                DropDownList ddlACCBANK = (DropDownList)e.Item.FindControl("DDL_ACCBANK");

                if (txtACCNO.Text.Trim() == "" || txtACCNAME.Text.Trim() == "" || ddlACCBANK.SelectedValue.Trim() == "")
                    return;

                conn.QueryString = "exec SP_APPLICATION_BANK_STATEMENT_REFUND " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + txtACCNO.Text.Trim() + "'," +
                                    "'" + txtACCNAME.Text.Trim() + "'," +
                                    "'" + ddlACCBANK.SelectedValue.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            FillDGRSummary();
            DV_SUMMARY.Visible = true;
            DV_DETAIL.Visible = false;
        }

        protected void DGR_SUMMARY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                LB_TITLE.Text = e.Item.Cells[1].Text;
                LB_POLSTAT.Text = e.Item.Cells[0].Text;
                TXT_FULLNAME.Text = "";
                TXT_REGNO.Text = "";
                FillDGR();

                DV_SUMMARY.Visible = false;
                DV_DETAIL.Visible = true;
            }
        }
    }
}