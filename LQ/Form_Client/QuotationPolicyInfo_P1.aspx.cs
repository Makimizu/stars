using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationPolicyInfo_P1 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_UNITIZE.Text = Request.QueryString["U"].ToString();

                ShowForm();
                FillDGRIrreguler();
                FillFormNo();
                FillDGR_ITEM();
                FillDGRFOP();
                //FillDGRBenefit();
                //FillDGRBenefitHealthPlan();
                FillDGRFund();
                FillClaimOrganization();
                FillDGRBenefitCycle();
                SpecificSet();
                IF_BENEFIT.Attributes.Remove("onload");
                IF_BENEFIT.Attributes.Add("onload", " resizeIframe(this)");
                IF_BENEFIT.Src = "QuotationBenefit.aspx?REGNO=" + LB_REGNO.Text;
            }
        }

        protected void FillDGRBenefitCycle()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                BT_BENCYCLE.Visible = false;
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_CYCLE.DataSource = dt;
            DGR_BENEFIT_CYCLE.DataBind();

            for (int i = 0; i < DGR_BENEFIT_CYCLE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT_CYCLE.Items[i].FindControl("CB");
                if (DGR_BENEFIT_CYCLE.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void FillDDLACCBANK()
        {
            DDL_ACCBANK.Items.Clear();
            conn.QueryString = "select KODE, BANK = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' and BANK like '%" + TXT_ACCBANK.Text.Trim() + "%' order by 1";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillClaimOrganization()
        {
            FillDDLACCBANK();

            conn.QueryString = "select * from V_APPLICATION_ORG_BENEFICIARY where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_ORG.Text = conn.GetFieldValue("ORG_NAME").ToString();
                TXT_CERNO.Text = conn.GetFieldValue("CERNO").ToString();
                TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
                TXT_ACCNAME.Text = conn.GetFieldValue("ACCNAME").ToString();

                try
                {
                    DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
                }
                catch { }

                string PCT_CLM = conn.GetFieldValue("PCT_RISK").ToString();
                string PCT_CLM_SQL = conn.GetFieldValue("PCT_RISK_SQL").ToString();
                string PCT_INV = conn.GetFieldValue("PCT_NONRISK").ToString();
                string PCT_INV_SQL = conn.GetFieldValue("PCT_NONRISK_SQL").ToString();

                DDL_PCTCLM.Items.Clear();
                conn.QueryString = PCT_CLM_SQL;
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PCTCLM.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

                try
                {
                    DDL_PCTCLM.SelectedValue = PCT_CLM;
                }
                catch { }

                DDL_PCTINV.Items.Clear();
                conn.QueryString = PCT_INV_SQL;
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_PCTINV.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

                try
                {
                    DDL_PCTINV.SelectedValue = PCT_INV;
                }
                catch { }
            }
            else
            {
                BT_ORGBEN.Visible = false;
            }
        }

        protected void SpecificSet()
        {
            string productGroup = "";
            string productCode = "";
            string enableTopup = "";
            conn.QueryString = "select " +
                                "PRODUCT_CODE       = a.PRODUCT_CODE, " +
                                "PRODUCT_GROUP      = b.PRODUCT_GROUP, " +
                                "ENABLE_TOPUPREG	= (case when isnull(d.MAX_TOPUP_MULT,0) > 0 then 1 else 0 end) " + 
                                "from		        APPLICATION_MASTER a " +
                                "inner join	        V_LINK_UW_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE " +
                                "left join	        APPLICATION_PERIOD_INFO c on a.REGNO = c.REGNO " +
                                "left join	        UWBOX.dbo.PARAM_PRODUCT_MASTER_FOP d on a.PRODUCT_CODE = d.PRODUCT_CODE and isnull(c.FOP,1) = d.FOP_CODE " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                productCode = conn.GetFieldValue("PRODUCT_CODE").ToString();
                productGroup = conn.GetFieldValue("PRODUCT_GROUP").ToString();
                enableTopup = conn.GetFieldValue("ENABLE_TOPUPREG").ToString();
            }

            string edplan;
            edplan = "0";

            conn.QueryString = "select distinct " +
                                "a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM b on a.PRODUCT_CODE = b.PRODUCT_CODE and a.TC_ID = b.TC_ID and b.DEATH = 0 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                edplan = "1";

            for (int i = 0; i < DGR_FOP.Items.Count; i++)
            {
                DropDownList ddlPOL = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_POLICY_PERIOD");
                DropDownList ddlPAY = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_PAYMENT_PERIOD");
                DropDownList ddlFOP = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_FOP");
                TextBox txtPREMIUM = (TextBox)DGR_FOP.Items[i].FindControl("TXT_PREMIUM");
                TextBox txtTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_TOPUP");
                HtmlTableRow trTOPUP = (HtmlTableRow)DGR_FOP.Items[i].FindControl("TR_TOPUP_REGULER");

                if (edplan == "1")
                {
                    ddlPOL.Enabled = false;
                    ddlPAY.Enabled = false;
                }

                //if (productGroup == "IED" || productCode == "118")
                if (enableTopup == "0")
                {
                    trTOPUP.Visible = false;
                }
            }
        }

        protected void FillDGRIrreguler()
        {
            string ProductCode = "";
            try
            {

                conn.QueryString = "select PRODUCT_CODE from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                ProductCode = conn.GetFieldValue("PRODUCT_CODE").ToString();
            }
            catch { }
            
            //if (LB_UNITIZE.Text != "1" && ProductCode != "117")
            //{
            //    BT_IRG.Visible = false;
            //    return;
            //}

            conn.QueryString = "exec SP_APPLICATION_IRREGULER_TRX '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_IRREGULER.DataSource = dt;
            DGR_IRREGULER.DataBind();

            for (int i = 0; i < DGR_IRREGULER.Items.Count; i++)
            {
                TextBox txtTIR = (TextBox)DGR_IRREGULER.Items[i].FindControl("TXT_TIR");
                TextBox txtWDW = (TextBox)DGR_IRREGULER.Items[i].FindControl("TXT_WDW");

                txtTIR.Text = DGR_IRREGULER.Items[i].Cells[2].Text;
                txtWDW.Text = DGR_IRREGULER.Items[i].Cells[3].Text;
            }
        }

        protected void FillDGRFOP()
        {
            string productGroup = "";
            conn.QueryString = "select " +
                                "PRODUCT_GROUP, " +
                                "START_DATE	= convert(varchar(20), a.START_DATE, 103) " +
                                "from V_APPLICATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            productGroup = conn.GetFieldValue("PRODUCT_GROUP").ToString();
            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();

            conn.QueryString = "exec SP_APPLICATION_PERIOD_INFO '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection conn3 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            //conn1.QueryString = "select CODE, DESCR from V_LINK_UW_PR_FREQUENCY_OF_PAYMENT order by 1";
            conn1.QueryString = "select CODE, DESCR " +
                                "from V_LINK_UW_PR_FREQUENCY_OF_PAYMENT a " +
                                "inner join APPLICATION_MASTER b on b.REGNO = '" + LB_REGNO.Text + "' " +
                                "inner join UWBOX.dbo.PARAM_PRODUCT_MASTER_FOP c on a.CODE = c.FOP_CODE and c.PRODUCT_CODE = b.PRODUCT_CODE and c.TC_ID = b.TC_ID " +
                                "order by 1";
            conn1.ExecuteQuery();
            conn2.QueryString = "exec SP_APPLICATION_TENOR_FACTOR '" + LB_REGNO.Text + "', 'IP'";
            conn2.ExecuteQuery();
            conn3.QueryString = "exec SP_APPLICATION_TENOR_FACTOR '" + LB_REGNO.Text + "', 'PP'";
            conn3.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FOP.DataSource = dt;
            DGR_FOP.DataBind();

            for (int i = 0; i < DGR_FOP.Items.Count; i++)
            {
                DropDownList ddlPOL = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_POLICY_PERIOD");
                DropDownList ddlPAY = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_PAYMENT_PERIOD");
                DropDownList ddlFOP = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_FOP");
                TextBox txtPREMIUM = (TextBox)DGR_FOP.Items[i].FindControl("TXT_PREMIUM");
                TextBox txtTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_TOPUP");
                

                ddlFOP.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn1.GetRowCount(); j++)
                    ddlFOP.Items.Add(new ListItem(conn1.GetFieldValue(j, 1).ToString(), conn1.GetFieldValue(j, 0).ToString()));

                ddlPOL.Items.Add(new ListItem("", "0"));
                ddlPAY.Items.Add(new ListItem("", "0"));
                for (int k = 0; k < conn2.GetRowCount(); k++)
                {
                    ddlPOL.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                }
                for (int k = 0; k < conn3.GetRowCount(); k++)
                {
                    ddlPAY.Items.Add(new ListItem(conn3.GetFieldValue(k, 1).ToString(), conn3.GetFieldValue(k, 0).ToString()));
                }


                txtPREMIUM.Text = DGR_FOP.Items[i].Cells[5].Text;
                txtTOPUP.Text = DGR_FOP.Items[i].Cells[6].Text;

                try
                {
                    ddlFOP.SelectedValue = DGR_FOP.Items[i].Cells[2].Text;
                }
                catch { }

                try
                {
                    ddlPOL.SelectedValue = DGR_FOP.Items[i].Cells[3].Text;
                }
                catch { }

                try
                {
                    ddlPAY.SelectedValue = DGR_FOP.Items[i].Cells[4].Text;
                }
                catch { }

                //try
                //{
                //    if (productGroup == "ISP")
                //    {
                //        ddlPAY.Enabled = false;
                //    }
                //}
                //catch { }
            }
        }

        //protected void FillDGRBenefit()
        //{
        //    conn.QueryString = "exec SP_APPLICATION_BENEFIT '" + LB_REGNO.Text + "'";
        //    conn.ExecuteQuery();

        //    Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

        //    conn1.QueryString = "select " +
        //                        "ID			= b.ID, " +
        //                        "FULLNAME	= c.DESCR + ' : ' + b.FULLNAME " +
        //                        "from		APPLICATION_MEMBER a  " +
        //                        "inner join	V_LINK_CB_MEMBER_MASTER b on a.MEMBER_ID = b.ID " +
        //                        "inner join	UWBOX.dbo.PR_MEMBER_TYPE c on a.MEMBER_TYPE = c.CODE " +
        //                        "where " +
        //                        "a.REGNO = '" + LB_REGNO.Text + "' " +
        //                        "order by convert(int, c.CODE)";
        //    conn1.ExecuteQuery();

        //    DataTable dt;
        //    dt = new DataTable();
        //    dt = conn.GetDataTable().Copy();
        //    DGR_BENEFIT.DataSource = dt;
        //    DGR_BENEFIT.DataBind();

        //    for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
        //    {
        //        Label lbBASIC = (Label)DGR_BENEFIT.Items[i].FindControl("LB_BASIC");
        //        Label lbBENEFIT = (Label)DGR_BENEFIT.Items[i].FindControl("LB_BENEFIT_DESCR");
        //        CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_BENEFIT");
        //        DropDownList ddlINSURED = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_INSUREDPERSON");
        //        TextBox txtSUMINSURED = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_SUMINS");
        //        TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_RATE");
        //        TextBox txtENDDATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_ENDDATE");
        //        Label lbMIN = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MIN");
        //        Label lbMAX = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MAX");
        //        System.Web.UI.HtmlControls.HtmlTableRow trSUMINS = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_SUMINS");
        //        System.Web.UI.HtmlControls.HtmlTableRow trSUMINSLIMIT = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_SUMINS_LIMIT");
        //        System.Web.UI.HtmlControls.HtmlTableRow trENDDATE = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_ENDDATE");
        //        System.Web.UI.HtmlControls.HtmlTableRow trMAXAGE = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_BENEFIT.Items[i].FindControl("TR_MAXAGE");
        //        DropDownList ddlMAXAGE = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_MAXAGE");

        //        lbBENEFIT.Text = DGR_BENEFIT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
        //        txtSUMINSURED.Text = DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "");
        //        txtENDDATE.Text = DGR_BENEFIT.Items[i].Cells[5].Text.Replace("&nbsp;", "");
        //        lbMIN.Text = DGR_BENEFIT.Items[i].Cells[9].Text.Replace("&nbsp;", "");
        //        lbMAX.Text = DGR_BENEFIT.Items[i].Cells[10].Text.Replace("&nbsp;", "");
        //        //txtRATE.Text = DGR_BENEFIT.Items[i].Cells[4].Text.Replace("&nbsp;", "");

        //        if (DGR_BENEFIT.Items[i].Cells[7].Text == "1")
        //            cb.Checked = true;

        //        if (DGR_BENEFIT.Items[i].Cells[8].Text == "1")
        //        {
        //            cb.Enabled = false;
        //            lbBENEFIT.Font.Bold = true;
        //            DGR_BENEFIT.Items[i].BackColor = System.Drawing.Color.FromArgb(222, 243, 222);
        //        }
        //        else
        //        {
        //            lbBASIC.Text = "RIDER";
        //        }

        //        ddlINSURED.Items.Add(new ListItem("", ""));
        //        for (int j = 0; j < conn1.GetRowCount(); j++)
        //            ddlINSURED.Items.Add(new ListItem(conn1.GetFieldValue(j, 1).ToString(), conn1.GetFieldValue(j, 0).ToString()));

        //        try
        //        {
        //            ddlINSURED.SelectedValue = DGR_BENEFIT.Items[i].Cells[6].Text;
        //        }
        //        catch { }

        //        if (DGR_BENEFIT.Items[i].Cells[11].Text == "1")
        //        {
        //            trMAXAGE.Visible = true;
        //            trENDDATE.Visible = false;
        //            trSUMINS.Visible = false;
        //            trSUMINSLIMIT.Visible = false;

        //            conn.QueryString = DGR_BENEFIT.Items[i].Cells[12].Text;
        //            conn.ExecuteQuery();
        //            for (int py = 0; py < conn.GetRowCount(); py++)
        //                ddlMAXAGE.Items.Add(new ListItem(conn.GetFieldValue(py, 0).ToString(), conn.GetFieldValue(py, 0).ToString()));
        //        }
        //        else
        //        {
        //            trMAXAGE.Visible = false;
        //            trENDDATE.Visible = true;
        //            trSUMINS.Visible = true;
        //            trSUMINSLIMIT.Visible = true;
        //        }
        //    }
        //}

        //protected void FillDGRBenefitHealthPlan()
        //{
        //    conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN '" + LB_REGNO.Text + "'";
        //    conn.ExecuteQuery();

        //    if (conn.GetRowCount() == 0)
        //    {
        //        DGR_BENEFIT_HEALTHPLAN.Visible = false;
        //        return;
        //    }

        //    DataTable dt;
        //    dt = new DataTable();
        //    dt = conn.GetDataTable().Copy();
        //    DGR_BENEFIT_HEALTHPLAN.DataSource = dt;
        //    DGR_BENEFIT_HEALTHPLAN.DataBind();

        //    for (int i = 0; i < DGR_BENEFIT_HEALTHPLAN.Items.Count; i++)
        //    {
        //        DropDownList ddlPLAN = (DropDownList)DGR_BENEFIT_HEALTHPLAN.Items[i].FindControl("DDL_PLAN");
        //        ddlPLAN.Items.Add("");
        //        conn.QueryString = DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[2].Text;
        //        conn.ExecuteQuery();
        //        for (int j = 0; j < conn.GetRowCount(); j++)
        //        {
        //            ddlPLAN.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));
        //        }

        //        try
        //        {
        //            ddlPLAN.SelectedValue = DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[1].Text;
        //        }
        //        catch { }
        //    }
        //}

        protected void FillDGRFund()
        {
            if (LB_UNITIZE.Text == "0")
            {
                BT_FUND.Visible = false;
                return;
            }

            conn.QueryString = "exec SP_APPLICATION_FUND '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_FUND.DataSource = dt;
            DGR_FUND.DataBind();

            conn.QueryString = "select SEQ = (SEQ-1)*10 from SC_SEQ where SEQ <= 11";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_FUND.Items.Count; i++)
            {
                DropDownList ddlPCT = (DropDownList)DGR_FUND.Items[i].FindControl("DDL_PCT_FUND");

                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlPCT.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlPCT.SelectedValue = DGR_FUND.Items[i].Cells[2].Text;
                }
                catch { }
            }
        }

        protected void BT_SAVE_FOP_Click(object sender, EventArgs e)
        {
            SaveOtherItems();

            for (int i = 0; i < DGR_FOP.Items.Count; i++)
            {
                DropDownList ddlPOL = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_POLICY_PERIOD");
                DropDownList ddlPAY = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_PAYMENT_PERIOD");
                DropDownList ddlFOP = (DropDownList)DGR_FOP.Items[i].FindControl("DDL_FOP");
                TextBox txtPREMIUM = (TextBox)DGR_FOP.Items[i].FindControl("TXT_PREMIUM");
                TextBox txtTOPUP = (TextBox)DGR_FOP.Items[i].FindControl("TXT_TOPUP");

                if (ddlFOP.SelectedValue == "" || txtPREMIUM.Text.Trim() == "0" || txtPREMIUM.Text.Trim() == "")
                    continue;

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_PERIOD_INFO_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + DGR_FOP.Items[i].Cells[0].Text + "'," +
                                        "'" + ddlFOP.SelectedValue + "'," +
                                        "'" + ddlPOL.SelectedValue + "'," +
                                        "'" + ddlPAY.SelectedValue + "'," +
                                        "'" + txtPREMIUM.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txtTOPUP.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            //conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_UPSERT '" + LB_REGNO.Text + "'";
            //conn.ExecuteNonQuery();

            SaveFormNo();

            //FillDGRFOP();
            //FillDGRBenefit();
            //FillDGR_ITEM();
            //FillFormNo();
            //SpecificSet();
            ShowQuotationHeader();
            FillDGRBenefitCycle();
        }

        protected void SaveFormNo()
        {
            conn.QueryString = "exec SP_APPLICATION_SUBMISSION_FORM_UPSERT '" + LB_REGNO.Text + "','" + TXT_FORMNO.Text.Trim() + "'";
            conn.ExecuteNonQuery();
        }

        //protected void BT_SAVE_BENEFIT_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
        //    {
        //        Label lbMIN = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MIN");
        //        Label lbMAX = (Label)DGR_BENEFIT.Items[i].FindControl("LB_SUMINS_MAX");
        //        CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB_BENEFIT");
        //        DropDownList ddlINSURED = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_INSUREDPERSON");
        //        TextBox txtSUMINSURED = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_SUMINS");
        //        TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_RATE");
        //        TextBox txtENDDATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_ENDDATE");

        //        //if (ddlINSURED.SelectedValue == "" || txtENDDATE.Text.Trim() == "" || txtSUMINSURED.Text.Trim() == "0")
        //        if (ddlINSURED.SelectedValue == "" || txtENDDATE.Text.Trim() == "")
        //            continue;

        //        //try
        //        //{
        //        string stat = "1";
        //        if (!cb.Checked)
        //            stat = "0";

        //        conn.QueryString = "update APPLICATION_BENEFIT set " +
        //                            "MEMBER_ID = '" + ddlINSURED.SelectedValue + "'," +
        //                            "SUMINS = '" + txtSUMINSURED.Text.Trim().Replace(",", "") + "'," +
        //                            "END_DATE = '" + GlobalUse.GlobalDateFormat(txtENDDATE.Text.Trim(), "d/M/yyyy") + "', " +
        //                            "STAT = " + stat + ", " +
        //                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
        //                            "LASTCHANGEDATE = GETDATE() " +
        //                            "where " +
        //                            "REGNO = '" + LB_REGNO.Text + "' " +
        //                            "and BENEFIT_CODE = '" + DGR_BENEFIT.Items[i].Cells[1].Text + "' " +
        //                            "and TC_ID = '" + DGR_BENEFIT.Items[i].Cells[0].Text + "' " +
        //                            "and " + txtSUMINSURED.Text.Trim().Replace(",", "") + " between " + lbMIN.Text.Trim().Replace(",", "") + " and " + lbMAX.Text.Trim().Replace(",", "") + " " +
        //                            "exec SP_APPLICATION_BENEFIT_ADJUST " +
        //                            "'" + LB_REGNO.Text + "'," +
        //                            "'" + DGR_BENEFIT.Items[i].Cells[1].Text + "'," +
        //                            txtSUMINSURED.Text.Trim().Replace(",", "") + "," +
        //                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //        conn.ExecuteNonQuery();
        //        //}
        //        //catch { }
        //    }

        //    if (DGR_BENEFIT_HEALTHPLAN.Visible)
        //    {
        //        for (int i = 0; i < DGR_BENEFIT_HEALTHPLAN.Items.Count; i++)
        //        {
        //            string PLAN = "null";
        //            DropDownList ddlPLAN = (DropDownList)DGR_BENEFIT_HEALTHPLAN.Items[i].FindControl("DDL_PLAN");
        //            if (ddlPLAN.SelectedValue != "")
        //                PLAN = "'" + ddlPLAN.SelectedValue + "'";

        //            conn.QueryString = "exec SP_APPLICATION_BENEFIT_HEALTH_PLAN_UPSERT " +
        //                                "'" + LB_REGNO.Text + "'," +
        //                                "'" + DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[0].Text + "'," +
        //                                "'" + DGR_BENEFIT_HEALTHPLAN.Items[i].Cells[5].Text + "'," +
        //                                PLAN + "," +
        //                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //            conn.ExecuteNonQuery();
        //        }
        //    }


        //    conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_UPSERT '" + LB_REGNO.Text + "'";
        //    conn.ExecuteNonQuery();

        //    //FillDGRBenefit();
        //    //FillDGRBenefitHealthPlan();
        //    //FillFormNo();
        //    //SpecificSet();
        //    ShowQuotationHeader();
        //}

        protected void BT_SAVE_FUND_Click(object sender, EventArgs e)
        {
            float TotalPCT = 0;
            for (int i = 0; i < DGR_FUND.Items.Count; i++)
            {
                DropDownList ddlPCT = (DropDownList)DGR_FUND.Items[i].FindControl("DDL_PCT_FUND");
                TotalPCT = TotalPCT + float.Parse(ddlPCT.SelectedValue);
            }


            if (TotalPCT == 100)
            {

                for (int i = 0; i < DGR_FUND.Items.Count; i++)
                {
                    DropDownList ddlPCT = (DropDownList)DGR_FUND.Items[i].FindControl("DDL_PCT_FUND");

                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_FUND_UPSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DGR_FUND.Items[i].Cells[1].Text + "'," +
                                            "'" + ddlPCT.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_UPSERT '" + LB_REGNO.Text + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRFund();
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
                TextBox txtVALDATE = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VALDATE");

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
                        case "DATE": txtVALDATE.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Visible = false;
                            ddl.Visible = false;
                            txtVALDATE.Visible = true;
                            break;

                    }
                }
            }

        }

        protected void SaveOtherItems()
        {
            for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");
                TextBox txtValDate = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VALDATE");

                string val = txt.Text.Trim();
                if (ddl.Visible)
                    val = ddl.SelectedValue;

                if (txtValDate.Visible && txtValDate.Text.Trim() != "")
                    val = GlobalUse.GlobalDateFormat(txtValDate.Text.Trim(), "d/M/yyyy");

                conn.QueryString = "exec SP_APPLICATION_OTHER_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR_ITEM();
        }

        protected void FillFormNo()
        {
            conn.QueryString = "exec SP_APPLICATION_SUBMISSION_FORM '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            TXT_FORMNO.Text = conn.GetFieldValue("FORMNO").ToString();
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

        protected void BT_IRREGULER_Click(object sender, EventArgs e)
        {
            string FOP = "";
            conn.QueryString = "select FOP from APPLICATION_PERIOD_INFO where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            FOP = conn.GetFieldValue("FOP").ToString();

            conn.QueryString = "delete from APPLICATION_IRREGULER_TRX where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_IRREGULER.Items.Count; i++)
            {
                TextBox txtTIR = (TextBox)DGR_IRREGULER.Items[i].FindControl("TXT_TIR");
                TextBox txtWDW = (TextBox)DGR_IRREGULER.Items[i].FindControl("TXT_WDW");

                if (txtTIR.Text.Trim() != "" && txtTIR.Text.Trim() != "0")
                {
                    conn.QueryString = "insert into APPLICATION_IRREGULER_TRX select " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DGR_IRREGULER.Items[i].Cells[1].Text + "'," +
                                        "'PTIR_" + FOP + "'," +
                                        "'C'," +
                                        "'" + txtTIR.Text.Trim().Replace(",", "") + "'";
                    conn.ExecuteNonQuery();
                }

                if (txtWDW.Text.Trim() != "" && txtWDW.Text.Trim() != "0")
                {
                    conn.QueryString = "insert into APPLICATION_IRREGULER_TRX select " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DGR_IRREGULER.Items[i].Cells[1].Text + "'," +
                                        "'WDW'," +
                                        "'D'," +
                                        "'" + txtWDW.Text.Trim().Replace(",", "") + "'";
                    conn.ExecuteNonQuery();
                }
            }

            conn.QueryString = "exec SP_APPLICATION_SAVING_TRX_UPSERT '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            FillFormNo();
            FillDGRIrreguler();
        }

        protected void ShowQuotationHeader()
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationHeader.location.href = 'QuotationHeader.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT_ORG_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_ORG_BENEFICIARY_UPSERT" +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + TXT_ORG.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_CERNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + TXT_ACCNO.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_ACCBANK.SelectedValue + "'," +
                                "'" + TXT_ACCNAME.Text.Trim().Replace("'", "`") + "'," +
                                "'" + DDL_PCTCLM.SelectedValue + "'," +
                                "'" + DDL_PCTINV.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillClaimOrganization();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT_CYCLE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT_CYCLE.Items[i].FindControl("CB");
                string tobepaid = "0";
                if (cb.Checked)
                    tobepaid = "1";

                conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    DGR_BENEFIT_CYCLE.Items[i].Cells[0].Text + "," +
                                    DGR_BENEFIT_CYCLE.Items[i].Cells[2].Text + "," +
                                    tobepaid + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRBenefitCycle();
        }

        protected void ShowForm()
        {
            LB_TITLE.Text = BT_FORM.Text;
            TBL_FORM.Visible = true;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_FORM_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        protected void BT_BENEFIT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = true;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_IRG_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = true;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_FUND_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = true;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_ORGBEN_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = true;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_BENCYCLE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = true;
            TBL_PAYMENT.Visible = false;
        }

        protected void BT_PAYMENT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_FORM.Visible = false;
            TBL_BENEFIT.Visible = false;
            TBL_IRG.Visible = false;
            TBL_FUND.Visible = false;
            TBL_ORGBEN.Visible = false;
            TBL_BENCYCLE.Visible = false;
            TBL_PAYMENT.Visible = true;
        }

        protected void DDL_REFF_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveOtherItems();
            FillDGR_ITEM();
        }

        protected void TXT_ACCBANK_TextChanged(object sender, EventArgs e)
        {
            FillDDLACCBANK();
        }
    }
}