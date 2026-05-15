using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimPayoutForPremium : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_MODE.Text = Request.QueryString["MODE"].ToString();
                LB_INVOICE_TYPE.Text = Request.QueryString["INVOICE_TYPE"].ToString();

                Setup();
                FillDGRList();
            }

        }

        protected void Setup()
        {
            switch (LB_MODE.Text)
            {
                case "RISK": LB_TITLE.Text = "PREMIUM PAYMENT from <B>RISK BENEFIT</B>"; break;
                case "INV": LB_TITLE.Text = "PREMIUM PAYMENT from <B>SAVING BENEFIT</B>"; break;
            }

            conn.QueryString = "select CURRENCY from V_APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery(1500000);
            LB_CURR.Text = conn.GetFieldValue("CURRENCY").ToString();
        }

        protected void FillDGRList()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYOUT_FOR_PREMIUM " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_MODE.Text + "'";
            conn.ExecuteQuery(1500000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LIST.DataSource = dt;
            DGR_LIST.DataBind();
        }

        protected void FillDGRSearch()
        {
            if (TXT_SEARCH.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_APPLICATION_PREMIUM_PERFORMANCE_SEARCH '" + TXT_SEARCH.Text.Trim() + "','" + LB_CURR.Text + "'";
            conn.ExecuteQuery(1500000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SEARCH.DataSource = dt;
            DGR_SEARCH.DataBind();

            for (int i = 0; i < DGR_SEARCH.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_SEARCH.Items[i].FindControl("LB_SELECT");
                DropDownList ddl = (DropDownList)DGR_SEARCH.Items[i].FindControl("DDL_TOP");

                lb.Text = DGR_SEARCH.Items[i].Cells[1].Text;

                try
                {
                    conn.QueryString = "select SEQ from SC_SEQ where SEQ <= " + DGR_SEARCH.Items[i].Cells[5].Text;
                    conn.ExecuteQuery(1500000);
                    ddl.Items.Add(new ListItem("", ""));
                    for (int j = 0; j < conn.GetRowCount(); j++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
                catch { }
            }
        }

        protected void FillDGRSearchPTIR()
        {
            if (TXT_SEARCH.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_APPLICATION_PTIR_SEARCH '" + TXT_SEARCH.Text.Trim() + "','" + LB_CURR.Text + "'";
            conn.ExecuteQuery(1500000);


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SEARCH_PTIR.DataSource = dt;
            DGR_SEARCH_PTIR.DataBind();

            for (int i = 0; i < DGR_SEARCH_PTIR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_SEARCH_PTIR.Items[i].FindControl("LB_SELECT");
                lb.Text = DGR_SEARCH_PTIR.Items[i].Cells[1].Text;
            }
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            TR_LIST.Visible = true;
            TR_SEARCH.Visible = false;
            FillDGRList();
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            TR_LIST.Visible = false;
            TR_SEARCH.Visible = true;

            if (LB_INVOICE_TYPE.Text != "PTIR")
            {
                FillDGRSearch();
            }
            else
            {
                FillDGRSearchPTIR();
            }
        }

        protected void DGR_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYOUT_FOR_PREMIUM_FOR_PREMIUM_DELETE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_MODE.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRList();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitheader.location.href = 'ClaimBenefit.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&MODE=" + LB_MODE.Text + "';</script>");
            }

            if (e.CommandName == "DeleteAll")
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYOUT_FOR_PREMIUM_FOR_PREMIUM_DELETE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_MODE.Text + "'," +
                                    "null";
                conn.ExecuteNonQuery();
                FillDGRList();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitheader.location.href = 'ClaimBenefit.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&MODE=" + LB_MODE.Text + "';</script>");
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            if (LB_INVOICE_TYPE.Text != "PTIR")
            {
                FillDGRSearch();
            }
            else
            {
                FillDGRSearchPTIR();
            }
        }

        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_TOP");
                if (ddl.SelectedValue == "")
                    return;

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYOUT_FOR_PREMIUM_INSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + LB_MODE.Text + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        ddl.SelectedValue + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(500000);
                }
                catch { }

                TR_LIST.Visible = true;
                TR_SEARCH.Visible = false;
                FillDGRList();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitheader.location.href = 'ClaimBenefit.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&MODE=" + LB_MODE.Text + "';</script>");
            }
        }

        protected void DGR_SEARCH_PTIR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYOUT_FOR_PREMIUM_PTIR_INSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + LB_MODE.Text + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(500000);
                }
                catch { }

                TR_LIST.Visible = true;
                TR_SEARCH.Visible = false;
                FillDGRList();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitheader.location.href = 'ClaimBenefit.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&MODE=" + LB_MODE.Text + "';</script>");
            }
        }

        protected void DGR_SEARCH_PTIR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_SEARCH_PTIR.CurrentPageIndex = e.NewPageIndex;
            FillDGRSearchPTIR();
        }
    }
}