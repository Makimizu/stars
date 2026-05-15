using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace LIFE.Form_App
{
    public partial class ApplicationOtherInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();

                Setup();
                FillDGRUWInfo();
                FillDGROtherPolicy();
                FillDGR_ITEM();
                FillDGROtherInsurance();
                FillDGRMemberDisease();
                FillDGRMedicalHistory();
                FillDGRUClaim();
                FillDGR();
                FillDGRICDExcluded();
                FillDGRQuotation();
                ShowPaymentStatus();
                ShowOtherExclusion();
                CheckTrack();
                FillDGRBlackList();

                ShowSubmission();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select * from LQ.dbo.V_APPLICATION_QUESTION_MODULAR_GROUP where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
            {
                BT_QUESTIONFORM.Enabled = false;
            }

            conn.QueryString = "select ENABLE = dbo.UFN_PENDING_ENABLE('" + LB_REGNO.Text + "','','UW')";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("ENABLE").ToString() == "0")
            {
                BT_PENDING.Enabled = false;
            }

            conn.QueryString = "select VISIBLE = 0 from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "' and PRODUCT_CODE = '059'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                if (conn.GetFieldValue("VISIBLE").ToString() == "0")
                {
                    BT_DISEASE.Visible = false;
                }
            }
        }

        protected void ShowOtherExclusion()
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION '" + LB_REGNO.Text + "','WORK'";
            conn.ExecuteQuery();
            TXT_JOB.Text = conn.GetFieldValue("REMARK").ToString();

            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION '" + LB_REGNO.Text + "','HOBBY'";
            conn.ExecuteQuery();
            TXT_HOBBY.Text = conn.GetFieldValue("REMARK").ToString();

            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION '" + LB_REGNO.Text + "','DISEASE'";
            conn.ExecuteQuery();
            TXT_DISEASE.Text = conn.GetFieldValue("REMARK").ToString();
        }

        protected void FillDGRQuotation()
        {
            conn.QueryString = "exec SP_LINK_LQ_QUOTATION '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DGR_QUOTATION.DataSource = conn.GetDataTable().Copy();
            DGR_QUOTATION.DataBind();
            for (int i = 0; i < DGR_QUOTATION.Items.Count; i++)
            {
                Button bt = (Button)DGR_QUOTATION.Items[i].FindControl("BT_QUOTATION");
                bt.Text = DGR_QUOTATION.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGRUWInfo()
        {
            conn.QueryString = "exec SP_APPLICATION_UW_INFO '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_UW_INFO.DataSource = conn.GetDataTable().Copy();
            DGR_UW_INFO.DataBind();
        }

        protected void FillDGRBlackList()
        {
            conn.QueryString = "exec SP_LINK_CB_MEMBER_BLACKLIST_VERIFY '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_BLACKLIST.DataSource = conn.GetDataTable().Copy();
            DGR_BLACKLIST.DataBind();
        }

        protected void FillDGRUClaim()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_HISTORY '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_CLAIM.DataSource = conn.GetDataTable().Copy();
            DGR_CLAIM.DataBind();
        }

        protected void FillDGROtherInsurance()
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_INSURANCE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_OTHERINS.DataSource = conn.GetDataTable().Copy();
            DGR_OTHERINS.DataBind();
        }

        protected void FillDGRMemberDisease()
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER_DISEASE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_DISEASE.DataSource = conn.GetDataTable().Copy();
            DGR_DISEASE.DataBind();
        }

        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_INFO " +
                                "'" + LB_REGNO.Text + "'," +
                                "'UW'";
            conn.ExecuteQuery();

            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");

                if (DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_ITEM.Items[j].Cells[1].Text)
                    {
                        case "STR": txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Width = Unit.Percentage(100);
                            break;
                        case "INT": txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Attributes.Add("text-align", "right");
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Attributes.Add("text-align", "right");
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }
            }

        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") > 2)
            {
                TBL_FIRST_PAYMENT.Visible = false;
                TXT_ICD.Visible = false;
                DGR_ICD_AVAIL.Visible = false;
                DGR_ICD_EXCLUDED.Enabled = false;
                DGR.Enabled = false;
                BT_SAVE.Visible = false;
                DGR_ITEM.Enabled = false;
                BT_ITEM_SAVE.Visible = false;
            }
        }

        protected bool IsPAYDI()
        {
            conn.QueryString = "select a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PAYDI = 1 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                return true;

            return false;
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_TC_BENEFIT '" + LB_REGNO.Text + "', 0";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbBENEFIT = (Label)DGR.Items[i].FindControl("LB_BENEFIT");
                Label lbINSURED = (Label)DGR.Items[i].FindControl("LB_INSURED");
                Label lbENDDATE = (Label)DGR.Items[i].FindControl("LB_ENDDATE");
                Label lbSUMINS = (Label)DGR.Items[i].FindControl("LB_SUMINS");
                Label lbSUMINS1 = (Label)DGR.Items[i].FindControl("LB_SUMINS1");
                Label lbSUMINS2 = (Label)DGR.Items[i].FindControl("LB_SUMINS2");
                Label lbBASICPREM = (Label)DGR.Items[i].FindControl("LB_BASICPREMIUM");
                Label lbPREMIUM1 = (Label)DGR.Items[i].FindControl("LB_PREMIUM1");
                Label lbPREMIUM2 = (Label)DGR.Items[i].FindControl("LB_PREMIUM2");
                Label lbPREMIUM3 = (Label)DGR.Items[i].FindControl("LB_PREMIUM3");


                TextBox TXT_RATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                TextBox TXT_EP_RATE = (TextBox)DGR.Items[i].FindControl("TXT_EP_RATE");
                TextBox TXT_EM_RATE = (TextBox)DGR.Items[i].FindControl("TXT_EM_RATE");
                TextBox TXT_EP_REMARK = (TextBox)DGR.Items[i].FindControl("TXT_EP_REMARK");
                TextBox TXT_EM_REMARK = (TextBox)DGR.Items[i].FindControl("TXT_EM_REMARK");


                lbBENEFIT.Text = DGR.Items[i].Cells[3].Text;
                lbINSURED.Text = DGR.Items[i].Cells[4].Text;
                lbENDDATE.Text = DGR.Items[i].Cells[5].Text;
                lbSUMINS.Text = DGR.Items[i].Cells[6].Text;
                lbSUMINS1.Text = DGR.Items[i].Cells[6].Text;
                lbSUMINS2.Text = "(" + DGR.Items[i].Cells[6].Text + "/(1 - <B>" + DGR.Items[i].Cells[15].Text + "%</B>))";
                lbBASICPREM.Text = DGR.Items[i].Cells[10].Text;
                lbPREMIUM1.Text = DGR.Items[i].Cells[10].Text;
                lbPREMIUM2.Text = DGR.Items[i].Cells[11].Text;
                lbPREMIUM3.Text = DGR.Items[i].Cells[12].Text;

                TXT_RATE.Text = DGR.Items[i].Cells[7].Text;
                TXT_EP_RATE.Text = DGR.Items[i].Cells[8].Text;
                TXT_EM_RATE.Text = DGR.Items[i].Cells[9].Text;
                TXT_EP_REMARK.Text = DGR.Items[i].Cells[13].Text.Trim().Replace("&nbsp;", "");
                TXT_EM_REMARK.Text = DGR.Items[i].Cells[14].Text.Trim().Replace("&nbsp;", "");
            }

        }

        protected void FillDGROtherPolicy()
        {
            conn.QueryString = "exec SP_APPLICATION_SAME_MEMBER '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DGR_OTHERPOLICY.DataSource = conn.GetDataTable().Copy();
            DGR_OTHERPOLICY.DataBind();

            conn.QueryString = "exec SP_APPLICATION_SUMINS_ACCUMULATION_BENEFIT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DGR_BENEFIT.DataSource = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataBind();
        }

        protected void ShowSubmission()
        {
            LB_TITLE.Text = BT_SUBMISSION.Text;
            TBL_SUBMISSION.Visible = true;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_SUBMISSION_Click(object sender, EventArgs e)
        {
            ShowSubmission();
        }

        protected void BT_OTHERPOLICY_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = true;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_CLAIM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = true;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_DISEASE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = true;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_QUESTIONFORM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = true;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_EXTRAPREMIUM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = true;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_MEDICALDOC_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = true;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_PENDING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = true;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_EXCLUSION_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = true;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_PAYMENT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = true;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = false;
        }

        protected void BT_REINSURANCE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_SUBMISSION.Visible = false;
            TBL_OTHERPOLICY.Visible = false;
            TBL_CLAIM.Visible = false;
            TBL_DISEASE.Visible = false;
            TBL_QUESTIONFORM.Visible = false;
            TBL_EXTRAPREMIUM.Visible = false;
            TBL_MEDICALDOC.Visible = false;
            TBL_PENDING.Visible = false;
            TBL_EXCLUSION.Visible = false;
            TBL_PAYMENT.Visible = false;
            TBL_QUOTATION.Visible = false;
            TBL_REINSURANCE.Visible = true;
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox TXT_RATE = (TextBox)DGR.Items[i].FindControl("TXT_RATE");
                TextBox TXT_EP_RATE = (TextBox)DGR.Items[i].FindControl("TXT_EP_RATE");
                TextBox TXT_EM_RATE = (TextBox)DGR.Items[i].FindControl("TXT_EM_RATE");
                TextBox TXT_EP_REMARK = (TextBox)DGR.Items[i].FindControl("TXT_EP_REMARK");
                TextBox TXT_EM_REMARK = (TextBox)DGR.Items[i].FindControl("TXT_EM_REMARK");

                if (TXT_EP_RATE.Text.Trim() != "")
                {
                    try
                    {
                        conn.QueryString = "update APPLICATION_TC_BENEFIT set " +
                                            "EP_RATE = '" + TXT_EP_RATE.Text + "', " +
                                            "EP_REMARK = '" + TXT_EP_REMARK.Text + "' " +
                                            "where " +
                                            "REGNO = '" + LB_REGNO.Text + "' " +
                                            "and TC_ID = '" + DGR.Items[i].Cells[0].Text + "' " +
                                            "and BENEFIT_CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                if (TXT_EM_RATE.Text.Trim() != "")
                {
                    try
                    {
                        conn.QueryString = "update APPLICATION_TC_BENEFIT set " +
                                            "EM_RATE = '" + TXT_EM_RATE.Text + "', " +
                                            "EM_REMARK = '" + TXT_EM_REMARK.Text + "' " +
                                            "where " +
                                            "REGNO = '" + LB_REGNO.Text + "' " +
                                            "and TC_ID = '" + DGR.Items[i].Cells[0].Text + "' " +
                                            "and BENEFIT_CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            //Task.Run(() => ProcessCOI_EMEP());
            ProcessCOI_EMEP();
            FillDGR();
        }

        protected void ProcessCOI_EMEP()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_EMEP_INSDEL '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_APPLICATION_PREMIUM_COI_UPDATE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(500000);
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_TC_BENEFIT '" + LB_REGNO.Text + "', 1";
                    conn.ExecuteQuery();

                    e.Item.Cells[DGR.Columns.Count - 2].Text = "TOTAL";
                    e.Item.Cells[DGR.Columns.Count - 1].Text = conn.GetFieldValue(0, 0).ToString();
                }
                catch { }
            }
        }

        protected void TXT_ICD_TextChanged(object sender, EventArgs e)
        {
            FillDGRICD();
        }

        protected void FillDGRICD()
        {
            if (TXT_ICD.Text.Trim().Length < 4)
                return;

            conn.QueryString = "select " +
                                "a.CODE,  " +
                                "a.DESCR  " +
                                "from		UWBOX.dbo.PR_ICD a " +
                                "left join	APPLICATION_EXCLUDED_ICD b on a.CODE = b.ICD_CODE collate database_default and b.REGNO = '" + LB_REGNO.Text + "' " +
                                "where  " +
                                "a.DESCR like '%" + TXT_ICD.Text.Trim() + "%'  " +
                                "and b.REGNO is null " +
                                "order by 2";
            conn.ExecuteQuery();

            DGR_ICD_AVAIL.DataSource = conn.GetDataTable().Copy();
            DGR_ICD_AVAIL.DataBind();
            for (int i = 0; i < DGR_ICD_AVAIL.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_ICD_AVAIL.Items[i].FindControl("LB_SELECT");
                lb.Text = DGR_ICD_AVAIL.Items[i].Cells[0].Text;
            }
        }

        protected void FillDGRICDExcluded()
        {
            conn.QueryString = "select " +
                                "b.CODE, " +
                                "b.DESCR " +
                                "from		APPLICATION_EXCLUDED_ICD a " +
                                "inner join	UWBOX.dbo.PR_ICD b on a.ICD_CODE = b.CODE collate database_default " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();

            DGR_ICD_EXCLUDED.DataSource = conn.GetDataTable().Copy();
            DGR_ICD_EXCLUDED.DataBind();

            for (int i = 0; i < DGR_ICD_EXCLUDED.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_ICD_EXCLUDED.Items[i].FindControl("LB_DELETE");
                DataGrid dgr = (DataGrid)DGR_ICD_EXCLUDED.Items[i].FindControl("DGR_ICD_BENEFIT");
                lb.Text = DGR_ICD_EXCLUDED.Items[i].Cells[0].Text;

                conn.QueryString = "exec SP_APPLICATION_EXCLUDED_ICD_BENEFIT '" + LB_REGNO.Text + "','" + DGR_ICD_EXCLUDED.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                dgr.DataSource = conn.GetDataTable().Copy();
                dgr.DataBind();
                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    CheckBox cb = (CheckBox)dgr.Items[j].FindControl("CB");
                    if (dgr.Items[j].Cells[0].Text == "1")
                        cb.Checked = true;
                }
            }
        }

        protected void DGR_ICD_AVAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_APPLICATION_EXCLUDED_ICD_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                FillDGRICD();
                FillDGRICDExcluded();
            }
        }

        protected void DGR_ICD_EXCLUDED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from APPLICATION_EXCLUDED_ICD where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and ICD_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                FillDGRICD();
                FillDGRICDExcluded();
            }
        }

        protected void ShowPaymentStatus()
        {
            conn.QueryString = "exec SP_APPLICATION_FIRST_PAYMENT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_VACCNO.Text = conn.GetFieldValue("VACCNO").ToString();
            LB_BANK.Text = conn.GetFieldValue("BANK").ToString();
            LB_PREMIUM.Text = conn.GetFieldValue("PREMIUM").ToString();
            LB_PAYMENT_AMOUNT.Text = conn.GetFieldValue("PAYMENT_AMOUNT").ToString();
            LB_PAYMENT_DATE.Text = conn.GetFieldValue("PAYMENT_DATE").ToString();
            LB_PAYMENT_NOTE.Text = conn.GetFieldValue("PAYMENT_DESCR").ToString();
            LB_POLICY_CHG.Text = conn.GetFieldValue("POLICY_CHG").ToString();
            LB_STAMP_CHG.Text = conn.GetFieldValue("STAMP_CHG").ToString();
            LB_TOTAL_CHG.Text = conn.GetFieldValue("TOTAL").ToString();
        }

        protected void DGR_OTHERPOLICY_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_APPLICATION_SAME_MEMBER_TOTAL '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[DGR_OTHERPOLICY.Columns.Count - 2].Text = "TOTAL";
                e.Item.Cells[DGR_OTHERPOLICY.Columns.Count - 1].Text = conn.GetFieldValue(0, 0).ToString();
            }
        }

        protected void BT_ITEM_SAVE_Click(object sender, EventArgs e)
        {
            SaveDGRITEM();
        }

        protected void SaveDGRITEM()
        {
            for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");
                //TextBox txtValDate = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VALDATE");

                string val = txt.Text.Trim();
                if (ddl.Visible)
                    val = ddl.SelectedValue;

                //if (txtValDate.Visible && txtValDate.Text.Trim() != "")
                //    val = GlobalUse.GlobalDateFormat(txtValDate.Text.Trim(), "d/M/yyyy");

                conn.QueryString = "exec SP_APPLICATION_OTHER_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR_ITEM();
            FillDGRUWInfo();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_ICD_EXCLUDED.Items.Count; i++)
            {
                DataGrid dgr = (DataGrid)DGR_ICD_EXCLUDED.Items[i].FindControl("DGR_ICD_BENEFIT");
                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    CheckBox cb = (CheckBox)dgr.Items[j].FindControl("CB");
                    if (cb == (CheckBox)sender)
                    {
                        if (cb.Checked)
                        {
                            conn.QueryString = "insert into APPLICATION_EXCLUDED_ICD_BENEFIT " +
                                                "select " +
                                                "REGNO			= '" + LB_REGNO.Text + "'," +
                                                "ICD_CODE		= '" + DGR_ICD_EXCLUDED.Items[i].Cells[0].Text + "'," +
                                                "BENEFIT_CODE	= '" + dgr.Items[j].Cells[1].Text + "'," +
                                                "CREATEBY		= '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "CREATEDATE		= GETDATE()," +
                                                "LASTCHANGEBY	= '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "LASTCHANGEDATE	= GETDATE()";
                            conn.ExecuteNonQuery();
                        }
                        else
                        {
                            conn.QueryString = "delete APPLICATION_EXCLUDED_ICD_BENEFIT " +
                                                "where " +
                                                "REGNO			    = '" + LB_REGNO.Text + "' " +
                                                "and ICD_CODE	    = '" + DGR_ICD_EXCLUDED.Items[i].Cells[0].Text + "' " +
                                                "and BENEFIT_CODE	= '" + dgr.Items[j].Cells[1].Text + "' " +
                                                "delete APPLICATION_EXCLUDED_ICD where REGNO + ICD_CODE not in (select REGNO + ICD_CODE from APPLICATION_EXCLUDED_ICD_BENEFIT)";
                            conn.ExecuteNonQuery();
                        }

                        FillDGRICDExcluded();
                        return;
                    }
                }
            }
        }

        protected void DGR_QUOTATION_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Quotation")
            {
                LB_TITLE.Text = e.Item.Cells[1].Text.ToUpper();
                TBL_SUBMISSION.Visible = false;
                TBL_OTHERPOLICY.Visible = false;
                TBL_CLAIM.Visible = false;
                TBL_DISEASE.Visible = false;
                TBL_QUESTIONFORM.Visible = false;
                TBL_EXTRAPREMIUM.Visible = false;
                TBL_MEDICALDOC.Visible = false;
                TBL_PENDING.Visible = false;
                TBL_EXCLUSION.Visible = false;
                TBL_PAYMENT.Visible = false;
                TBL_QUOTATION.Visible = true;
                TBL_REINSURANCE.Visible = false;

                IF_QUOTATION.Src = e.Item.Cells[0].Text;
            }
        }

        protected void FillDGRMedicalHistory()
        {
            string visible = "0";
            conn.QueryString = "select " +
                                    "VISIBLE = case when b.PRODUCT_GROUP = 'IED' then 1 " +
                                    "               when b.PAYDI = 0 then 1 " +
                                    "               else 0 " +
                                    "               end " +
                                    "from V_APPLICATION_MASTER a " +
                                    "inner join UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE " +
                                    "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            visible = conn.GetFieldValue("VISIBLE").ToString();
            if (visible == "1")
            {
                conn.QueryString = "exec SP_APPLICATION_FAMILY_MEDICAL_HISTORY " +
                                "'" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                DGR_MEDICAL_HISTORY.DataSource = conn.GetDataTable().Copy();
                DGR_MEDICAL_HISTORY.DataBind();

                LB_MEDICAL_HISTORY.Visible = true;
                DGR_MEDICAL_HISTORY.Visible = true;
            }
            else
            {
                LB_MEDICAL_HISTORY.Visible = false;
                DGR_MEDICAL_HISTORY.Visible = false;
            }


        }

        protected void BT_SAVE_OTHER_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'WORK'," +
                                "'" + TXT_JOB.Text.Trim().Replace("'", "`") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'HOBBY'," +
                                "'" + TXT_HOBBY.Text.Trim().Replace("'", "`") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_APPLICATION_OTHER_EXCLUSION_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'DISEASE'," +
                                "'" + TXT_DISEASE.Text.Trim().Replace("'", "`") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_APPLICATION_EXCLUDED_WORK_INSERT '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            ShowOtherExclusion();
        }


    }
}