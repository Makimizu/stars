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
    public partial class SuspendAccountList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=BT_LOAD]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void BT_REFRESH_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_JOB_APPLICATION_BANK_STATEMENT";
                conn.ExecuteQuery(500000);
            }
            catch { }
            Response.Redirect("SuspendAccountList.aspx");
        }

        protected void BT_LOAD_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(15000);
            FillDGRSumm();
        }

        protected void FillDGRSumm()
        {
            conn.QueryString = "exec SP_LINK_FINANCE_REKENING_JURNAL_SUSPEND_SUMMARY";
            conn.ExecuteQuery(500000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SUMM.DataSource = dt;
            DGR_SUMM.DataBind();

            for (int i = 0; i < DGR_SUMM.Items.Count; i++)
            {
                LinkButton btIDN = (LinkButton)DGR_SUMM.Items[i].FindControl("BT_IDENTIFIED");
                LinkButton btUDN = (LinkButton)DGR_SUMM.Items[i].FindControl("BT_UNIDENTIFIED");
                LinkButton btS = (LinkButton)DGR_SUMM.Items[i].FindControl("BT_S");

                if (DGR_SUMM.Items[i].Cells[1].Text != "0")
                {
                    btS.Visible = true;
                    btS.Attributes.Add("onclick", "if(!confirm('Are you sure to AUTO SETTLE ?')){return false;};");
                    btIDN.Text = DGR_SUMM.Items[i].Cells[1].Text;
                }
                else
                    btIDN.Visible = false;

                if (DGR_SUMM.Items[i].Cells[2].Text != "0")
                    btUDN.Text = DGR_SUMM.Items[i].Cells[2].Text;
                else
                    btUDN.Visible = false;
            }
        }

        protected void DGR_SUMM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "IDENTIFIED")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.SuspendBankStatementList.location.href = 'SuspendBankStatement.aspx?mode=I&ACCNO=" + e.Item.Cells[0].Text + "';</script>");
            }

            if (e.CommandName == "UNIDENTIFIED")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.SuspendBankStatementList.location.href = 'SuspendBankStatement.aspx?mode=U&ACCNO=" + e.Item.Cells[0].Text + "';</script>");
            }

            if (e.CommandName == "AutoSettle")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'SuspendAutoSettle.aspx?ACCNO=" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}