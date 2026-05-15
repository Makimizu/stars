using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Drawing;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimBenTrx : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                Setup();
                FillDGR();
            }
        }

        protected string CheckStatus()
        {
            string stat = "";
            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            stat = conn.GetFieldValue("LAST_TRACK").ToString();
            return stat;
        }

        protected void Setup()
        {
            string stat = CheckStatus();
            if (stat == "3" || stat == "4")
            {
                TR_ADDBEN.Visible = false;
                DGR.Columns[18].Visible = false;
                return;
            }

            conn.QueryString = "exec SP_CLM_BENEFIT '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            DDL_BENEFIT.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {   
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            if (DDL_BENEFIT.Items.Count > 0)
            {
                FillBenefitDetail();
            }
            
        }

        protected void FillBenefitDetail()
        {
            conn.QueryString = "exec SP_CLM_BENEFIT_DETAIL '" + LB_CLAIMNO.Text + "','" + DDL_BENEFIT.SelectedValue + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                DVBENEFIT.Visible = false;
                return;
            }
            else
            {
                DVBENEFIT.Visible = true;
            }

            DataTable dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                LinkButton lbDETAIL = (LinkButton)DGR_BENEFIT.Items[i].FindControl("LB_BENEFITDETAIL");
                lbDETAIL.Text = DGR_BENEFIT.Items[i].Cells[2].Text;
            }
        }

        protected void LB_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBenefitDetail();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_CLM_CLAIM_BENEFIT '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                Label lbCODE = (Label)DGR.Items[i].FindControl("LB_CODE");
                Label lbDESCR = (Label)DGR.Items[i].FindControl("LB_DESCR");
                Label lbFREQ = (Label)DGR.Items[i].FindControl("LB_FREQ");

                TextBox txtREMARK = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");
                TextBox txtPENGAJUAN = (TextBox)DGR.Items[i].FindControl("TXT_PENGAJUAN");
                TextBox txtCASH = (TextBox)DGR.Items[i].FindControl("TXT_CASH");
                TextBox txtFREQ = (TextBox)DGR.Items[i].FindControl("TXT_FREQ");

                TextBox txtCOVERED = (TextBox)DGR.Items[i].FindControl("TXT_COVERED");
                TextBox txtUNPAID = (TextBox)DGR.Items[i].FindControl("TXT_UNPAID");
                TextBox txtEXCESS = (TextBox)DGR.Items[i].FindControl("TXT_EXCESS");
                TextBox txtREFUND = (TextBox)DGR.Items[i].FindControl("TXT_REFUND");
                TextBox txtPAID = (TextBox)DGR.Items[i].FindControl("TXT_PAID");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                lbCODE.Text = DGR.Items[i].Cells[1].Text + " - " + DGR.Items[i].Cells[2].Text;
                lbDESCR.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                lbFREQ.Text = DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "");

                txtREMARK.Text = DGR.Items[i].Cells[13].Text.Replace("&nbsp;", "");
                txtPENGAJUAN.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtCASH.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtFREQ.Text = DGR.Items[i].Cells[12].Text.Replace("&nbsp;", "");

                txtCOVERED.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtUNPAID.Text = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                txtEXCESS.Text = DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                txtREFUND.Text = DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                txtPAID.Text = DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label LBPENGAJUAN = (Label)e.Item.FindControl("LB_PENGAJUAN_TOTAL");
                Label LBCASH = (Label)e.Item.FindControl("LB_CASH_TOTAL");

                Label LBUNPAID = (Label)e.Item.FindControl("LB_UNPAID_TOTAL");
                Label LBEXCESS = (Label)e.Item.FindControl("LB_EXCESS_TOTAL");
                Label LBREFUND = (Label)e.Item.FindControl("LB_REFUND_TOTAL");
                Label lbDISCOUNT = (Label)e.Item.FindControl("LB_DISCOUNT");
                Label LBPAID = (Label)e.Item.FindControl("LB_PAID_TOTAL");

                conn.QueryString = "exec SP_CLM_CLAIM_BENEFIT_TOTAL '" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();

                LBPENGAJUAN.Text = conn.GetFieldValue("AMOUNT_PENGAJUAN").ToString();
                LBCASH.Text = conn.GetFieldValue("AMOUNT_CASH").ToString();
                LBUNPAID.Text = conn.GetFieldValue("AMOUNT_UNPAID").ToString();
                LBEXCESS.Text = conn.GetFieldValue("AMOUNT_EXCESS").ToString();
                LBREFUND.Text = conn.GetFieldValue("AMOUNT_REFUND").ToString();
                lbDISCOUNT.Text = conn.GetFieldValue("AMOUNT_DISCOUNT").ToString();
                LBPAID.Text = conn.GetFieldValue("AMOUNT_BAYAR").ToString();
            }
        }

        protected void AddBenefit(string ID)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "SELECT * FROM dbo.CLAIM_ICD WHERE CLAIM_NO='" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() == 0)
                {
                    GlobalTools.popMessage(this, "Diagnosa belum dipilih !");
                    return;
                }
                conn.QueryString = "exec SP_CLM_CLAIM_BENEFIT_INSERT '" + LB_CLAIMNO.Text + "','" + ID + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_BENEFIT_DELETE '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbCODE = (Label)DGR.Items[i].FindControl("LB_CODE");
                Label lbDESCR = (Label)DGR.Items[i].FindControl("LB_DESCR");
                Label lbFREQ = (Label)DGR.Items[i].FindControl("LB_FREQ");

                TextBox txtREMARK = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");
                TextBox txtPENGAJUAN = (TextBox)DGR.Items[i].FindControl("TXT_PENGAJUAN");
                TextBox txtCASH = (TextBox)DGR.Items[i].FindControl("TXT_CASH");
                TextBox txtFREQ = (TextBox)DGR.Items[i].FindControl("TXT_FREQ");

                TextBox txtUNPAID = (TextBox)DGR.Items[i].FindControl("TXT_UNPAID");
                TextBox txtEXCESS = (TextBox)DGR.Items[i].FindControl("TXT_EXCESS");
                TextBox txtREFUND = (TextBox)DGR.Items[i].FindControl("TXT_REFUND");
                TextBox txtPAID = (TextBox)DGR.Items[i].FindControl("TXT_PAID");

                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_BENEFIT_UPDATE " +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + txtFREQ.Text.Trim() + "'," +
                                        "'" + txtPENGAJUAN.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtCASH.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtUNPAID.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtREMARK.Text.Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "<BR>- " + ex.Message;
                }
            }

            FillDGR();
        }

        protected void BT_PRINT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rc:Parameters=False&CLAIM_NO=" + LB_CLAIMNO.Text + "' from V_LINK_SC_REPORT_LIST where CODE='288'";
            conn.ExecuteQuery();
            Response.Redirect(conn.GetFieldValue("URL").ToString());
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                AddBenefit(e.Item.Cells[1].Text);
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillBenefitDetail();
        }
    }
}