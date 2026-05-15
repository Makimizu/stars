using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductOtherSetup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["CODE"].ToString();
                Setup();
                if (DDL_TC.Items.Count == 0)
                {
                    TBL_PARAMETERS.Visible = false;
                }
                else
                {
                    LoadTC();
                    ShowOtherSettings();
                }
            }
        }

        protected void LoadTC()
        {
            FillDGRFund();
            FillDGR_ITEM();
            FillDGRFOP();
            FillDGRFinancing();
            FillDGRSavingFinTerm();
            FillDGRSurplus();
            FillDGRMinmaxPrd();
            FillDGRMinmaxAge();
            FillDGRLienCondition();
            FillDGROtherCorpShare();
            FillDDLBenefitClaim();
            FillDGRClaimLapse();
            FillDGRRider();
        }

        protected void FillDGRRider()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TC_BENEFIT '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_RIDER.DataSource = dt;
            DGR_RIDER.DataBind();

            for (int i = 0; i < DGR_RIDER.Items.Count; i++)
            {
                Label lbBENEFIT = (Label)DGR_RIDER.Items[i].FindControl("LB_BENEFIT");
                TextBox txtLOADING = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_LOADING");
                TextBox txtMINPCT = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_PCT");
                TextBox txtMIN = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN");
                TextBox txtMAXPCT = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX_PCT");
                TextBox txtMAX = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX");
                TextBox txtMINAGE = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_AGE");
                TextBox txtMAXAGE = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX_AGE");
                TextBox txtMINPP = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_PP");
                TextBox txtXN = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_XN");

                lbBENEFIT.Text = DGR_RIDER.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtLOADING.Text = DGR_RIDER.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtMINPCT.Text = DGR_RIDER.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtMIN.Text = DGR_RIDER.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtMAXPCT.Text = DGR_RIDER.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtMAX.Text = DGR_RIDER.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtMINAGE.Text = DGR_RIDER.Items[i].Cells[7].Text.Replace("&nbsp;", "");
                txtMAXAGE.Text = DGR_RIDER.Items[i].Cells[8].Text.Replace("&nbsp;", "");
                txtMINPP.Text = DGR_RIDER.Items[i].Cells[9].Text.Replace("&nbsp;", "");
                txtXN.Text = DGR_RIDER.Items[i].Cells[10].Text.Replace("&nbsp;", "");
            }
        }

        protected void FillDGRClaimLapse()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_CLAIM_LAPSE '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "','" + DDL_BENEFIT_CLAIM.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_CLAIM.DataSource = dt;
            DGR_BENEFIT_CLAIM.DataBind();

            for (int i = 0; i < DGR_BENEFIT_CLAIM.Items.Count; i++)
            {
                DropDownList ddlLAPSE = (DropDownList)DGR_BENEFIT_CLAIM.Items[i].FindControl("DDL_LAPSE");
                ddlLAPSE.SelectedValue = DGR_BENEFIT_CLAIM.Items[i].Cells[1].Text;
                if (DGR_BENEFIT_CLAIM.Items[i].Cells[1].Text == "0")
                {
                    DGR_BENEFIT_CLAIM.Items[i].BackColor = System.Drawing.Color.LightYellow;
                    DGR_BENEFIT_CLAIM.Items[i].BorderColor = System.Drawing.Color.Gainsboro;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select b.CODE, DESCR = b.CODE + ' - ' + b.DESCR from PARAM_PRODUCT_MASTER_TC a inner join TC_MASTER b on a.TC_ID = b.CODE where a.PRODUCT_CODE = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select PAYDI, UNITIZE from V_PARAM_PRODUCT_MASTER where PRODUCT_CODE = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("PAYDI").ToString() != "1")
            {
                BT_FUND.Visible = false;
                BT_FINTERM.Visible = false;
            }
            //if (conn.GetFieldValue("UNITIZE").ToString() != "1")
            //{
            //    BT_FUND.Visible = false;
            //}
        }

        protected void FillDGROtherCorpShare()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "','CLM'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_OTHERCORP_CLM.DataSource = dt;
            DGR_OTHERCORP_CLM.DataBind();
            for (int i = 0; i < DGR_OTHERCORP_CLM.Items.Count; i++)
            {
                Button btSAVE = (Button)DGR_OTHERCORP_CLM.Items[i].FindControl("BT_SAVE");
                Button btX = (Button)DGR_OTHERCORP_CLM.Items[i].FindControl("BT_X");
                TextBox txt = (TextBox)DGR_OTHERCORP_CLM.Items[i].FindControl("TXT_PCT_CLM");
                txt.Text = DGR_OTHERCORP_CLM.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (txt.Text.Trim() != "")
                {
                    txt.Enabled = false;
                    btSAVE.Visible = false;
                }
                else
                {
                    btX.Visible = false;
                }
            }

            conn.QueryString = "exec SP_PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "','INV'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_OTHERCORP_INV.DataSource = dt;
            DGR_OTHERCORP_INV.DataBind();
            for (int i = 0; i < DGR_OTHERCORP_INV.Items.Count; i++)
            {
                Button bt = (Button)DGR_OTHERCORP_INV.Items[i].FindControl("BT_SAVE_INV");
                Button btX = (Button)DGR_OTHERCORP_INV.Items[i].FindControl("BT_X_INV");
                TextBox txt = (TextBox)DGR_OTHERCORP_INV.Items[i].FindControl("TXT_PCT_INV");
                txt.Text = DGR_OTHERCORP_INV.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (txt.Text.Trim() != "")
                {
                    txt.Enabled = false;
                    bt.Visible = false;
                }
                else
                {
                    btX.Visible = false;
                }
            }
        }

        protected void FillDGRSurplus()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_SURPLUS_UNDERWRITING '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SURPLUS.DataSource = dt;
            DGR_SURPLUS.DataBind();

            for (int i = 0; i < DGR_SURPLUS.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_SURPLUS.Items[i].FindControl("TXT_SURPLUS");
                txt.Text = DGR_SURPLUS.Items[i].Cells[2].Text;
            }
        }

        protected void FillDGRFOP()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FOP '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FOP.DataSource = dt;
            DGR_FOP.DataBind();

            for (int i = 0; i < DGR_FOP.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FOP.Items[i].FindControl("TXT_LOADING");
                TextBox txtPr = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MIN_PREMIUM");
                TextBox txtTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MIN_PREMIUM_TOPUP");
                TextBox txtMAXTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MAX_PREMIUM_TOPUP");
                CheckBox cbEnabled = (CheckBox)DGR_FOP.Items[i].FindControl("CB_ENABLED");
                txt.Text = DGR_FOP.Items[i].Cells[2].Text;
                txtPr.Text = dt.Rows[i][3].ToString();
                txtTOPUP.Text = dt.Rows[i][4].ToString();
                txtMAXTOPUP.Text = dt.Rows[i][5].ToString();
                if (DGR_FOP.Items[i].Cells[7].Text == "True")
                {
                    cbEnabled.Checked = true;
                }
            }
        }

        protected void FillDGRFund()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FUND '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FUND.DataSource = dt;
            DGR_FUND.DataBind();

            conn.QueryString = "select CODE, DESCR from PR_POLICY_AGREEMENT order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_FUND.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_FUND.Items[i].FindControl("CB");
                Button btD = (Button)DGR_FUND.Items[i].FindControl("BT_D");
                DropDownList ddl = (DropDownList)DGR_FUND.Items[i].FindControl("DDL_AGREEMENT");

                if (DGR_FUND.Items[i].Cells[3].Text == "1")
                {
                    cb.Checked = true;
                    btD.Visible = true;
                }

                TextBox txt = (TextBox)DGR_FUND.Items[i].FindControl("TXT_DESCRIPTION");
                txt.Text = dt.Rows[i][3].ToString();

                TextBox txtLow1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_LOW1");
                txtLow1.Text = dt.Rows[i][5].ToString();
                TextBox txtMed1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_MID1");
                txtMed1.Text = dt.Rows[i][6].ToString();
                TextBox txtHigh1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_HIGH1");
                txtHigh1.Text = dt.Rows[i][7].ToString();
                TextBox txtLow2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_LOW2");
                txtLow2.Text = dt.Rows[i][8].ToString();
                TextBox txtMed2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_MED2");
                txtMed2.Text = dt.Rows[i][9].ToString();
                TextBox txtHigh2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_HIGH2");
                txtHigh2.Text = dt.Rows[i][10].ToString();

                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddl.SelectedValue = DGR_FUND.Items[i].Cells[1].Text;
                }
                catch { }
            }
        }

        protected void FillDGRFinancing()
        {
            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn2.QueryString = "select CODE, DESCR from PR_SCHOOL_GRADE";
            conn2.ExecuteQuery();

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FINANCING1.DataSource = dt;
            DGR_FINANCING1.DataBind();

            for (int i = 0; i < DGR_FINANCING1.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FINANCING1.Items[i].FindControl("TXT_PCT1");
                DropDownList ddlGrade = (DropDownList)DGR_FINANCING1.Items[i].FindControl("DDL_SCHOOL_GRADE1");
                txt.Text = DGR_FINANCING1.Items[i].Cells[2].Text;

                ddlGrade.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn2.GetRowCount(); j++)
                    ddlGrade.Items.Add(new ListItem(conn2.GetFieldValue(j, 1).ToString(), conn2.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlGrade.SelectedValue = DGR_FINANCING1.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }

                if (txt.Text != "0")
                    DGR_FINANCING1.Items[i].BackColor = System.Drawing.Color.Yellow;
            }

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "',1";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FINANCING2.DataSource = dt;
            DGR_FINANCING2.DataBind();

            for (int i = 0; i < DGR_FINANCING2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FINANCING2.Items[i].FindControl("TXT_PCT2");
                DropDownList ddlGrade = (DropDownList)DGR_FINANCING2.Items[i].FindControl("DDL_SCHOOL_GRADE2");
                txt.Text = DGR_FINANCING2.Items[i].Cells[2].Text;

                ddlGrade.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn2.GetRowCount(); j++)
                    ddlGrade.Items.Add(new ListItem(conn2.GetFieldValue(j, 1).ToString(), conn2.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlGrade.SelectedValue = DGR_FINANCING2.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }

                if (txt.Text != "0")
                    DGR_FINANCING2.Items[i].BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void FillDGRSavingFinTerm()
        {

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_SAVING_PLAN_FINTERM '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRSAVTERM1.DataSource = dt;
            DGRSAVTERM1.DataBind();

            for (int i = 0; i < DGRSAVTERM1.Items.Count; i++)
            {
                TextBox txtpct = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_PCT1");
                TextBox txtprd = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_PERIOD");
                TextBox txtmin = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_MINAGE");
                TextBox txtmax = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_MAXAGE");
                DropDownList ddlGrade = (DropDownList)DGRSAVTERM1.Items[i].FindControl("DDL_SCHOOL_GRADE1");
                txtpct.Text = DGRSAVTERM1.Items[i].Cells[2].Text;
                txtprd.Text = DGRSAVTERM1.Items[i].Cells[3].Text;
                txtmin.Text = DGRSAVTERM1.Items[i].Cells[4].Text;
                txtmax.Text = DGRSAVTERM1.Items[i].Cells[5].Text;

                if (txtpct.Text != "0")
                    DGRSAVTERM1.Items[i].BackColor = System.Drawing.Color.LightSkyBlue;
            }

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_SAVING_PLAN_FINTERM '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "',1";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRSAVTERM2.DataSource = dt;
            DGRSAVTERM2.DataBind();

            for (int i = 0; i < DGRSAVTERM2.Items.Count; i++)
            {
                TextBox txtpct = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_PCT1");
                TextBox txtprd = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_PERIOD");
                TextBox txtmin = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_MINAGE");
                TextBox txtmax = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_MAXAGE");
                DropDownList ddlGrade = (DropDownList)DGRSAVTERM2.Items[i].FindControl("DDL_SCHOOL_GRADE1");
                txtpct.Text = DGRSAVTERM2.Items[i].Cells[2].Text;
                txtprd.Text = DGRSAVTERM2.Items[i].Cells[3].Text;
                txtmin.Text = DGRSAVTERM2.Items[i].Cells[4].Text;
                txtmax.Text = DGRSAVTERM2.Items[i].Cells[5].Text;

                if (txtpct.Text != "0")
                    DGRSAVTERM2.Items[i].BackColor = System.Drawing.Color.Pink;
            }
        }

        protected void FillDGRMinmaxPrd()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_PERIOD_MINMAX '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MINMAX_PRD.DataSource = dt;
            DGR_MINMAX_PRD.DataBind();

            for (int i = 0; i < DGR_MINMAX_PRD.Items.Count; i++)
            {
                TextBox txtMin = (TextBox)DGR_MINMAX_PRD.Items[i].FindControl("TXT_MIN");
                txtMin.Text = dt.Rows[i][2].ToString(); //DGR_MINMAX_PRD.Items[i].Cells[2].Text;
                TextBox txtMax = (TextBox)DGR_MINMAX_PRD.Items[i].FindControl("TXT_MAX");
                txtMax.Text = dt.Rows[i][3].ToString(); //DGR_MINMAX_PRD.Items[i].Cells[3].Text;
            }
        }

        protected void FillDGRMinmaxAge()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MEMBER_AGE_MINMAX '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MINMAX_AGE.DataSource = dt;
            DGR_MINMAX_AGE.DataBind();

            for (int i = 0; i < DGR_MINMAX_AGE.Items.Count; i++)
            {
                TextBox txtMin = (TextBox)DGR_MINMAX_AGE.Items[i].FindControl("TXT_MIN");
                txtMin.Text = dt.Rows[i][2].ToString(); //DGR_MINMAX_PRD.Items[i].Cells[2].Text;
                TextBox txtMax = (TextBox)DGR_MINMAX_AGE.Items[i].FindControl("TXT_MAX");
                txtMax.Text = dt.Rows[i][3].ToString(); //DGR_MINMAX_PRD.Items[i].Cells[3].Text;
            }
        }

        protected void BT_SAVE_FUND_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_FUND.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_FUND.Items[i].FindControl("CB");
                DropDownList ddl = (DropDownList)DGR_FUND.Items[i].FindControl("DDL_AGREEMENT");
                string taken = "0";
                if (cb.Checked)
                    taken = "1";
                TextBox txt = (TextBox)DGR_FUND.Items[i].FindControl("TXT_DESCRIPTION");

                TextBox txtLow1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_LOW1");
                if (txtLow1.Text.Trim() == "") { txtLow1.Text = "0"; }
                TextBox txtMed1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_MID1");
                if (txtMed1.Text.Trim() == "") { txtMed1.Text = "0"; }
                TextBox txtHigh1 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_HIGH1");
                if (txtHigh1.Text.Trim() == "") { txtHigh1.Text = "0"; }
                TextBox txtLow2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_LOW2");
                if (txtLow2.Text.Trim() == "") { txtLow2.Text = "0"; }
                TextBox txtMed2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_MED2");
                if (txtMed2.Text.Trim() == "") { txtMed2.Text = "0"; }
                TextBox txtHigh2 = (TextBox)DGR_FUND.Items[i].FindControl("TXT_HIGH2");
                if (txtHigh2.Text.Trim() == "") { txtHigh2.Text = "0"; }

                try
                {
                    string agreement = "null";
                    if (ddl.SelectedValue != "")
                        agreement = "'" + ddl.SelectedValue + "'";

                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FUND_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_FUND.Items[i].Cells[0].Text + "'," +
                                        taken + "," +
                                        "'" + txt.Text.Trim() + "'," +
                                        agreement + "," +
                                        txtLow1.Text.Trim() + "," +
                                        txtMed1.Text.Trim() + "," +
                                        txtHigh1.Text.Trim() + "," +
                                        txtLow2.Text.Trim() + "," +
                                        txtMed2.Text.Trim() + "," +
                                        txtHigh2.Text.Trim() + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRFund();
        }

        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_OTHER_SETTING " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'," +
                                "'PRO'";
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
                            break;
                        case "INT": txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Attributes.Add("text-align", "right");
                            txtVAL.Width = 40;
                            break;
                        case "FLO":
                            txtVAL.Attributes.Add("text-align", "right");
                            txtVAL.Width = 100;
                            try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
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

        protected void DGR_ITEM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR_ITEM.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                    DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");

                    string val = txt.Text.Trim();
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    if (DGR_ITEM.Items[i].Cells[1].Text == "INT" || DGR_ITEM.Items[i].Cells[1].Text == "FLO")
                    {
                        val = val.Replace(",", "");
                    }

                    conn.QueryString = "exec SP_PARAM_OTHER_SETTING_UPSERT " +
                                        "'" + LB_ID.Text + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                        "'" + val + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();



                }

                FillDGR_ITEM();
            }
        }

        protected void BT_SAVE_FOP_Click(object sender, EventArgs e)
        {
            int enabled = 0;
            for (int i = 0; i < DGR_FOP.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FOP.Items[i].FindControl("TXT_LOADING");
                TextBox txtPr = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MIN_PREMIUM");
                TextBox txtTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MIN_PREMIUM_TOPUP");
                TextBox txtMAXTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_MAX_PREMIUM_TOPUP");
                CheckBox cbEnabled = (CheckBox)DGR_FOP.Items[i].FindControl("CB_ENABLED");

                try
                {
                    if (cbEnabled.Checked == true)
                    {
                        enabled = 1;
                    }
                    else
                    {
                        enabled = 0;
                    }

                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FOP_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_FOP.Items[i].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtPr.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtTOPUP.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtMAXTOPUP.Text.Trim().Replace(",", "") + "'," +
                                        enabled + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRFOP();
        }

        protected void DDL_TC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTC();
        }

        protected void FillDDLBenefitClaim()
        {
            DDL_BENEFIT_CLAIM.Items.Clear();
            conn.QueryString = "select " +
                                "b.CODE, " +
                                "b.DESCR " +
                                "from		TC_BENEFITS a " +
                                "inner join	PR_BENEFIT_MASTER b on a.BENEFIT_CODE = b.CODE " +
                                "where " +
                                "a.TC_CODE = '" + DDL_TC.SelectedValue + "' " +
                                "order by b.CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BENEFIT_CLAIM.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SAVE_FINANCING_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_FINANCING1.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FINANCING1.Items[i].FindControl("TXT_PCT1");
                DropDownList ddlGrade = (DropDownList)DGR_FINANCING1.Items[i].FindControl("DDL_SCHOOL_GRADE1");

                string pct = "null";
                string grade = "null";
                if (txt.Text.Trim() != "")
                    pct = txt.Text.Trim().Replace(",", "");
                if (ddlGrade.SelectedValue != "")
                    grade = "'" + ddlGrade.SelectedValue + "'";

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        DGR_FINANCING1.Items[i].Cells[0].Text + "," +
                                        DGR_FINANCING1.Items[i].Cells[1].Text + "," +
                                        pct + "," +
                                        grade + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();


                }
                catch { }
            }

            for (int i = 0; i < DGR_FINANCING2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_FINANCING2.Items[i].FindControl("TXT_PCT2");
                DropDownList ddlGrade = (DropDownList)DGR_FINANCING2.Items[i].FindControl("DDL_SCHOOL_GRADE2");

                string pct = "null";
                string grade = "null";
                if (txt.Text.Trim() != "")
                    pct = txt.Text.Trim().Replace(",", "");
                if (ddlGrade.SelectedValue != "")
                    grade = "'" + ddlGrade.SelectedValue + "'";

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        DGR_FINANCING2.Items[i].Cells[0].Text + "," +
                                        DGR_FINANCING2.Items[i].Cells[1].Text + "," +
                                        pct + "," +
                                        grade + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();


                }
                catch { }
            }

            FillDGRFinancing();
        }

        protected void BT_SAVE_SAVINGFINTERM_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGRSAVTERM1.Items.Count; i++)
            {
                TextBox txtpct = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_PCT1");
                TextBox txtprd = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_PERIOD");
                TextBox txtmin = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_MINAGE");
                TextBox txtmax = (TextBox)DGRSAVTERM1.Items[i].FindControl("TXT_MAXAGE");

                string pct = "null";
                string prd = "null";
                string min = "null";
                string max = "null";
                if (txtpct.Text.Trim() != "")
                    pct = txtpct.Text.Trim().Replace(",", ".");
                if (txtprd.Text.Trim() != "")
                    prd = txtprd.Text.Trim().Replace(",", ".");
                if (txtmin.Text.Trim() != "")
                    min = txtmin.Text.Trim().Replace(",", ".");
                if (txtmax.Text.Trim() != "")
                    max = txtmax.Text.Trim().Replace(",", ".");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_SAVING_PLAN_FINTERM_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        DGRSAVTERM1.Items[i].Cells[6].Text + "," +
                                        DGRSAVTERM1.Items[i].Cells[0].Text + "," +
                                        prd + "," +
                                        pct + "," +
                                        min + "," +
                                        max + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();


                }
                catch { }
            }

            for (int i = 0; i < DGRSAVTERM2.Items.Count; i++)
            {
                TextBox txtpct = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_PCT1");
                TextBox txtprd = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_PERIOD");
                TextBox txtmin = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_MINAGE");
                TextBox txtmax = (TextBox)DGRSAVTERM2.Items[i].FindControl("TXT_MAXAGE");

                string pct = "null";
                string prd = "null";
                string min = "null";
                string max = "null";
                if (txtpct.Text.Trim() != "")
                    pct = txtpct.Text.Trim().Replace(",", ".");
                if (txtprd.Text.Trim() != "")
                    prd = txtprd.Text.Trim().Replace(",", ".");
                if (txtmin.Text.Trim() != "")
                    min = txtmin.Text.Trim().Replace(",", ".");
                if (txtmax.Text.Trim() != "")
                    max = txtmax.Text.Trim().Replace(",", ".");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_SAVING_PLAN_FINTERM_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        DGRSAVTERM2.Items[i].Cells[6].Text + "," +
                                        DGRSAVTERM2.Items[i].Cells[0].Text + "," +
                                        prd + "," +
                                        pct + "," +
                                        min + "," +
                                        max + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();


                }
                catch { }
            }

            FillDGRSavingFinTerm();
        }

        protected void BT_SAVE_SURPLUS_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_SURPLUS.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_SURPLUS.Items[i].FindControl("TXT_SURPLUS");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_SURPLUS_UNDERWRITING_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_SURPLUS.Items[i].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRSurplus();
        }

        protected void DGR_FUND_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                TR_1_FUND.Visible = false;
                TR_1_INSTRUMENT.Visible = true;

                LB_FUNDCODE.Text = e.Item.Cells[0].Text;
                LB_FUNDNAME.Text = e.Item.Cells[1].Text;
                conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FUND_INSTRUMENT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_TC.SelectedValue + "'," +
                                    "'" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_INSTRUMENT.DataSource = dt;
                DGR_INSTRUMENT.DataBind();
                for (int i = 0; i < DGR_INSTRUMENT.Items.Count; i++)
                {
                    TextBox txtMinPCT = (TextBox)DGR_INSTRUMENT.Items[i].FindControl("TXT_MIN_INSPCT");
                    txtMinPCT.Text = DGR_INSTRUMENT.Items[i].Cells[3].Text;
                    TextBox txtPCT = (TextBox)DGR_INSTRUMENT.Items[i].FindControl("TXT_INSPCT");
                    txtPCT.Text = DGR_INSTRUMENT.Items[i].Cells[4].Text;
                }
            }
        }

        protected void BT_SAVE_INSTRUMENT_Click(object sender, EventArgs e)
        {
            SaveFundInstrument();

            TR_1_FUND.Visible = true;
            TR_1_INSTRUMENT.Visible = false;
        }

        protected void SaveFundInstrument()
        {
            for (int i = 0; i < DGR_INSTRUMENT.Items.Count; i++)
            {
                TextBox txtMinPCT = (TextBox)DGR_INSTRUMENT.Items[i].FindControl("TXT_MIN_INSPCT");
                TextBox txtMaxPCT = (TextBox)DGR_INSTRUMENT.Items[i].FindControl("TXT_INSPCT");
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_FUND_INSTRUMENT_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DGR_INSTRUMENT.Items[i].Cells[1].Text + "'," +
                                        "'" + DGR_INSTRUMENT.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR_INSTRUMENT.Items[i].Cells[2].Text + "'," +
                                        "'" + txtMinPCT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtMaxPCT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
        }

        protected void BT_SAVE_MINMAX_PRD_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MINMAX_PRD.Items.Count; i++)
            {
                TextBox txtMin = (TextBox)DGR_MINMAX_PRD.Items[i].FindControl("TXT_MIN");
                TextBox txtMax = (TextBox)DGR_MINMAX_PRD.Items[i].FindControl("TXT_MAX");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_PERIOD_MINMAX_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_MINMAX_PRD.Items[i].Cells[0].Text + "'," +
                                        "'" + txtMin.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtMax.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRMinmaxPrd();
        }

        protected void BT_SAVE_MINMAX_AGE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MINMAX_AGE.Items.Count; i++)
            {
                TextBox txtMin = (TextBox)DGR_MINMAX_AGE.Items[i].FindControl("TXT_MIN");
                TextBox txtMax = (TextBox)DGR_MINMAX_AGE.Items[i].FindControl("TXT_MAX");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MEMBER_AGE_MINMAX_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_MINMAX_AGE.Items[i].Cells[0].Text + "'," +
                                        "'" + txtMin.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtMax.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRMinmaxAge();
        }

        protected void FillDGRLienCondition()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_LIEN_CONDITION '" + LB_ID.Text + "','" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LIENCONDITION.DataSource = dt;
            DGR_LIENCONDITION.DataBind();

            for (int i = 0; i < DGR_LIENCONDITION.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LIENCONDITION.Items[i].FindControl("TXT_PCTCLAIM");
                txt.Text = dt.Rows[i][2].ToString();
            }
        }

        protected void DGR_LIENCONDITION_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_PRODUCT_LIEN_CONDITION where " +
                                    "PRODUCT_CODE = '" + LB_ID.Text + "' " +
                                    "and TC_ID = '" + DDL_TC.SelectedValue + "' " +
                                    "and START_AGE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRLienCondition();
            }

            if (e.CommandName == "Save")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_PCTCLAIM");

                conn.QueryString = "exec SP_PARAM_PRODUCT_LIEN_CONDITION_UPSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'," +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "'" + e.Item.Cells[1].Text + "'," +
                                "'" + txt.Text.Trim().Replace(",", "") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRLienCondition();
            }
        }

        protected void BT_LIEN_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_LIEN_CONDITION_UPSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'," +
                                "'" + TXT_STARTAGE.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_ENDAGE.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_PCTCLAIM.Text.Trim().Replace(",", "") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGRLienCondition();
        }

        protected void DGR_OTHERCORP_CLM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txt = (TextBox)e.Item.FindControl("TXT_PCT_CLM");

            if (e.CommandName == "Delete")
            {
                //try
                //{
                conn.QueryString = "delete from PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE " +
                                    "where " +
                                    "PRODUCT_CODE	= '" + LB_ID.Text + "' " +
                                    "and TC_ID		= '" + DDL_TC.SelectedValue + "' " +
                                    "and CLAIM_MODE	= '" + e.Item.Cells[0].Text + "' " +
                                    "and PCT        = " + txt.Text.Trim().Replace(",", "");
                conn.ExecuteNonQuery();
                FillDGROtherCorpShare();
                //}
                //catch { }
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE_INSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "" + txt.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGROtherCorpShare();
                }
                catch { }
            }
        }

        protected void DGR_OTHERCORP_INV_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txt = (TextBox)e.Item.FindControl("TXT_PCT_INV");

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE " +
                                        "where " +
                                        "PRODUCT_CODE	= '" + LB_ID.Text + "' " +
                                        "and TC_ID		= '" + DDL_TC.SelectedValue + "' " +
                                        "and CLAIM_MODE	= '" + e.Item.Cells[0].Text + "' " +
                                        "and PCT        = " + txt.Text.Trim().Replace(",", "");
                    conn.ExecuteNonQuery();
                    FillDGROtherCorpShare();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_OTHERCORP_CLAIM_SHARE_INSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "" + txt.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGROtherCorpShare();
                }
                catch { }
            }
        }

        protected void DDL_BENEFIT_CLAIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRClaimLapse();
        }

        protected void BT_SAVE_BENEFITCLAIM_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT_CLAIM.Items.Count; i++)
            {
                DropDownList ddlLAPSE = (DropDownList)DGR_BENEFIT_CLAIM.Items[i].FindControl("DDL_LAPSE");
                conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_CLAIM_LAPSE_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_TC.SelectedValue + "'," +
                                    "'" + DGR_BENEFIT_CLAIM.Items[i].Cells[0].Text + "'," +
                                    "'" + DDL_BENEFIT_CLAIM.SelectedValue + "'," +
                                    ddlLAPSE.SelectedValue + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            FillDGRClaimLapse();
        }

        protected void ShowOtherSettings()
        {
            LB_TITLE.Text = BT_OTHER.Text;
            DV_OTHER.Visible = true;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_OTHER_Click(object sender, EventArgs e)
        {
            ShowOtherSettings();
        }

        protected void BT_FOP_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = true;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_LIEN_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = true;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_MINMAX_PERIOD_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = true;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_MINMAX_AGE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = true;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_CLAIM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = true;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_SURPLUS_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = true;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_FUND_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = true;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_FINTERM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;

            conn.QueryString = "select PRODUCT_GROUP from UWBOX.dbo.PARAM_PRODUCT_MASTER where PRODUCT_CODE = '" + LB_ID.Text + "' and PRODUCT_GROUP = 'ISP'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DV_FINTERM.Visible = false;
                DV_FINTERMSAVING.Visible = true;
            }
            else
            {
                DV_FINTERM.Visible = true;
                DV_FINTERMSAVING.Visible = false;
            }

            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = false;
        }

        protected void BT_WAKAF_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = true;
            DV_RIDER.Visible = false;
        }

        protected void BT_RIDER_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_OTHER.Visible = false;
            DV_FOP.Visible = false;
            DV_LIEN.Visible = false;
            DV_MINMAX_PERIOD.Visible = false;
            DV_MINMAX_AGE.Visible = false;
            DV_CLAIM.Visible = false;
            DV_SURPLUS.Visible = false;
            DV_FUND.Visible = false;
            DV_FINTERM.Visible = false;
            DV_FINTERMSAVING.Visible = false;
            DV_WAKAF.Visible = false;
            DV_RIDER.Visible = true;
        }


        protected void BT_SAVE_RIDER_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_RIDER.Items.Count; i++)
            {
                TextBox txtLOADING = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_LOADING");
                TextBox txtMINPCT = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_PCT");
                TextBox txtMIN = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN");
                TextBox txtMAXPCT = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX_PCT");
                TextBox txtMAX = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX");
                TextBox txtMINAGE = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_AGE");
                TextBox txtMAXAGE = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MAX_AGE");
                TextBox txtMINPP = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_MIN_PP");
                TextBox txtXN = (TextBox)DGR_RIDER.Items[i].FindControl("TXT_XN");

                string LOADING, MINPCT, MINSUMINS, MAXPCT, MAXSUMINS, MINAGE, MAXAGE, MINPP, XN;
                LOADING = MINPCT = MINSUMINS = MAXPCT = MAXSUMINS = MINAGE = MAXAGE = MINPP = XN = "null";

                if (txtLOADING.Text.Trim() != "")
                    LOADING = txtLOADING.Text.Trim().Replace(",", "");

                if (txtMINPCT.Text.Trim() != "")
                    MINPCT = txtMINPCT.Text.Trim().Replace(",", "");

                if (txtMIN.Text.Trim() != "")
                    MINSUMINS = txtMIN.Text.Trim().Replace(",", "");

                if (txtMAXPCT.Text.Trim() != "")
                    MAXPCT = txtMAXPCT.Text.Trim().Replace(",", "");

                if (txtMAX.Text.Trim() != "")
                    MAXSUMINS = txtMAX.Text.Trim().Replace(",", "");

                if (txtMINAGE.Text.Trim() != "")
                    MINAGE = txtMINAGE.Text.Trim().Replace(",", "");

                if (txtMAXAGE.Text.Trim() != "")
                    MAXAGE = txtMAXAGE.Text.Trim().Replace(",", "");

                if (txtMINPP.Text.Trim() != "")
                    MINPP = txtMINPP.Text.Trim().Replace(",", "");

                if (txtXN.Text.Trim() != "")
                    XN = txtXN.Text.Trim().Replace(",", "");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TC_BENEFIT_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_TC.SelectedValue + "'," +
                                        "'" + DGR_RIDER.Items[i].Cells[0].Text + "'," +
                                        LOADING + "," +
                                        MINPCT + "," +
                                        MINSUMINS + "," +
                                        MAXPCT + "," +
                                        MAXSUMINS + "," +
                                        MINAGE + "," +
                                        MAXAGE + "," +
                                        MINPP + "," +
                                        XN + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRRider();
        }
    }
}