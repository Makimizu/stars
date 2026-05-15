using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Claim
{
    public partial class ClaimBenefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();

                Setup();
                LoadBenefit();
                ShowRiskBenefit();
                ShowINVBenefit();
                LoadCharge();
                LoadSavingAlert();

                try
                {
                    SetMode(Request.QueryString["mode"]);
                }
                catch
                {
                    SetMode("RISK");
                }
            }
        }

        protected void ShowINVBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_SUMM '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_INV.DataSource = dt;
            DGR_BENEFIT_INV.DataBind();

            for (int i = 0; i < DGR_BENEFIT_INV.Items.Count; i++)
            {
                if (DGR_BENEFIT_INV.Items[i].Cells[0].Text == "D")
                {
                    DGR_BENEFIT_INV.Items[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void Setup()
        {
            bDone = TrackDone();
            conn.QueryString = "select " +
                                "REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PAYDI = 1 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                BT_INV.Enabled = false;
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void LoadBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_BENEFIT '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();


            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_AMOUNT");
                txtAMOUNT.Text = DGR_BENEFIT.Items[i].Cells[1].Text;

                if (bDone || Request.QueryString["readonly"] == "1")
                    txtAMOUNT.ReadOnly = true;
            }


            conn.QueryString = "exec SP_APPLICATION_CLAIM_BENEFIT_PAYOR '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_BENEFIT_PAYOR.DataSource = dt;
                DGR_BENEFIT_PAYOR.DataBind();

                for (int i = 0; i < DGR_BENEFIT_PAYOR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_BENEFIT_PAYOR.Items[i].FindControl("CB");
                    if (DGR_BENEFIT_PAYOR.Items[i].Cells[1].Text == "1")
                        cb.Checked = true;
                }
            }
        }

        protected void LoadCharge()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_CHARGE_COMPONENT '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CHARGE.DataSource = dt;
            DGR_CHARGE.DataBind();


            for (int i = 0; i < DGR_CHARGE.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_CHARGE");
                txtAMOUNT.Text = DGR_CHARGE.Items[i].Cells[1].Text;

                if (DGR_CHARGE.Items[i].Cells[2].Text == "0")
                {
                    txtAMOUNT.BackColor = System.Drawing.Color.Pink;
                }

                if (bDone || Request.QueryString["readonly"] == "1")
                    txtAMOUNT.ReadOnly = true;

                if (DGR_CHARGE.Items[i].Cells[3].Text == "1")
                    txtAMOUNT.ReadOnly = true;
            }
        }


        protected void DGR_CHARGE_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                e.Item.Cells[4].Text = "TOTAL APPROVED";

                conn.QueryString = "exec SP_APPLICATION_CLAIM_CHARGE_COMPONENT_TOTAL '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
                conn.ExecuteQuery();
                e.Item.Cells[5].Text = conn.GetFieldValue("TOTAL").ToString();
            }
        }


        protected void LoadSavingAlert()
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_SAVING_ALERT '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                LB_SAVING_ALERT.Text = conn.GetFieldValue(0, 0).ToString();
            }
            catch { }
        }

        protected void ShowRiskBenefit()
        {
            SetMode("RISK");
        }

        protected void BT_RISK_Click(object sender, EventArgs e)
        {
            ShowRiskBenefit();
        }

        protected void BT_INV_Click(object sender, EventArgs e)
        {
            SetMode("INV");
        }

        protected void SetMode(string mode)
        {
            if (mode == "RISK")
            {
                LB_TITLE.Text = BT_RISK.Text;
                TR_RISK.Visible = true;
                TR_INV.Visible = false;
                ShowBeneficiary("RISK");
            }
            else
            {
                LB_TITLE.Text = BT_INV.Text;
                TR_RISK.Visible = false;
                TR_INV.Visible = true;
                ShowBeneficiary("INV");
            }
        }

        protected void ShowBeneficiary(string mode)
        {
            //conn.QueryString = "exec SP_APPLICATION_PAYABLE " +
            //                    "'" + LB_REGNO.Text + "'," +
            //                    LB_SEQ.Text + "," +
            //                    "'" + mode + "'";
            //conn.ExecuteQuery();
            //DataTable dt;
            //dt = new DataTable();
            //dt = conn.GetDataTable().Copy();
            //DGR_BENEFICIARY.DataSource = dt;
            //DGR_BENEFICIARY.DataBind();

            //if (DGR_BENEFICIARY.Items.Count > 1)
            //{
            //    DGR_BENEFICIARY.Items[DGR_BENEFICIARY.Items.Count - 1].Font.Bold = true;
            //    DGR_BENEFICIARY.Items[DGR_BENEFICIARY.Items.Count - 1].BackColor = System.Drawing.Color.Gainsboro;
            //}

            bDone = TrackDone();
            conn.QueryString = "exec SP_APPLICATION_CLAIM_PAYABLE " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + mode + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PAYABLE.DataSource = dt;
            DGR_PAYABLE.DataBind();

            for (int i = 0; i < DGR_PAYABLE.Items.Count; i++)
            {
                Button bt = (Button)DGR_PAYABLE.Items[i].FindControl("BT_SET");
                if (DGR_PAYABLE.Items[i].Cells[2].Text.Replace("&nbsp;", "").Trim() != "")
                {
                    bt.Visible = true;
                }

                if (bDone)
                {
                    bt.Visible = false;
                }

                if (DGR_PAYABLE.Items[i].Cells[0].Text == "ZZZ")
                {
                    DGR_PAYABLE.Items[i].BackColor = System.Drawing.Color.Gainsboro;
                }

                if (DGR_PAYABLE.Items[i].Cells[1].Text == "D")
                {
                    DGR_PAYABLE.Items[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void DGR_PAYABLE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Set")
            {
                if (e.Item.Cells[2].Text.Substring(0, 4) != "exec")
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitbody.location.href = '" + e.Item.Cells[2].Text + "';</script>");
                }
                else
                {
                    conn.QueryString = e.Item.Cells[2].Text;
                    conn.ExecuteNonQuery();
                    if (TR_RISK.Visible)
                        ShowBeneficiary("RISK");
                    else
                        ShowBeneficiary("INV");
                }
            }
        }
    }
}