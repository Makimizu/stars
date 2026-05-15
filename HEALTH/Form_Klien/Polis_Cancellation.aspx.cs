using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Cancellation : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR(DDL_CANCEL.SelectedValue);
            }
        }

        protected void Setup()
        {
            BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");
            BT_REMARKSAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk BATAL ?')){return false;};");
        }

        protected void FillDGR(string mode)
        {
            string where = "";

            DGR.Visible = false;
            DGR_CANCELED.Visible = false;

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO = '" + TXT_POLICYNO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (mode == "0")
            {
                DGR.Visible = true;

                conn.QueryString = "select " +
                                    "ID, " +
                                    "POLICY_ID, " +
                                    "POLICY_NO, " +
                                    "COMPANY_NAME, " +
                                    "START_DATE = convert(varchar(20),START_DATE,106), " +
                                    "END_DATE = convert(varchar(20),END_DATE,106), " +
                                    "TIPE_DESCR, " +
                                    "MOP_DESCR, " +
                                    "PRODUCT, " +
                                    "TIPE_KOMISI, " +
                                    "TPA_DESCR, " +
                                    "AGENT_NAME = b.FRONT_NAME, " +
                                    "PROCESSDATE " +
                                    "from V_POLICY_PERIOD_NOTRX a " +
                                    "left join POLICY_PERIOD_CANCEL c on a.ID = c.POLICY_PERIOD_ID " +
                                    "left join V_LINK_MARKETING_M_AGENTS b on a.AGENT_CODE = b.CODE collate database_default " +
                                    "where c.POLICY_PERIOD_ID is null " + where +
                                    "order by " +
                                    "a.START_DATE";
                conn.ExecuteQuery();

                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();

                LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";


                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    LinkButton bt = (LinkButton)DGR.Items[i].FindControl("LB");
                    bt.Text = DGR.Items[i].Cells[3].Text;
                }
            }
            else
            {
                DGR_CANCELED.Visible = true;

                conn.QueryString = "select " +
                                    "ID, " +
                                    "POLICY_ID, " +
                                    "POLICY_NO, " +
                                    "COMPANY_NAME, " +
                                    "START_DATE = convert(varchar(20),START_DATE,106), " +
                                    "END_DATE = convert(varchar(20),END_DATE,106), " +
                                    "CANCEL_DATE = convert(varchar(20), c.CANCEL_DATE,106), " +
                                    "c.USERBY, " +
                                    "c.REMARK " +
                                    "from POLICY_PERIOD_CANCEL c " +
                                    "inner join V_POLICY_PERIOD a on c.POLICY_PERIOD_ID = a.ID " +
                                    "left join V_LINK_MARKETING_M_AGENTS b on a.AGENT_CODE = b.CODE collate database_default " +
                                    "where 1=1 " + where +
                                    "order by " +
                                    "c.CANCEL_DATE";
                conn.ExecuteQuery();

                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_CANCELED.DataSource = dt;
                DGR_CANCELED.DataBind();

                LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

                for (int i = 0; i < DGR_CANCELED.Items.Count; i++)
                {
                    LinkButton bt = (LinkButton)DGR_CANCELED.Items[i].FindControl("LB0");
                    bt.Text = DGR_CANCELED.Items[i].Cells[3].Text;
                }
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {   
            FillDGR(DDL_CANCEL.SelectedValue);
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("PolisFrame.aspx?ID=" + e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Cancel")
            {
                ShowCancellation(e.Item.Cells[1].Text, e.Item.Cells[3].Text, e.Item.Cells[4].Text, e.Item.Cells[5].Text + " - " + e.Item.Cells[6].Text);
            }
        }

        protected void ShowCancellation(string periodid, string policyno, string company, string period)
        {
            LB_ERROR.Text = "";
            LB_PERIODID.Text = periodid;
            LB_POLICYNO.Text = policyno;
            LB_COMPANY.Text = company;
            LB_PERIODDATE.Text = period;

            conn.QueryString = "select convert(varchar(20),GETDATE(),103)";
            conn.ExecuteQuery();
            TXT_DATE.Text = conn.GetFieldValue(0, 0).ToString();
            TXT_REMARK.Text = "";
   
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DDL_CANCEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR(DDL_CANCEL.SelectedValue);
        }

        protected void DGR_CANCELED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("PolisFrame.aspx?ID=" + e.Item.Cells[2].Text);
            }
        }

        protected void BT_REMARKSAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_CANCEL_INSERT " +
                                    "'" + LB_PERIODID.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text, "d/M/yyyy") + "'," +
                                    "'" + TXT_REMARK.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR(DDL_CANCEL.SelectedValue);
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'none';", true);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }
    }
}