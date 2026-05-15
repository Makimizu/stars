using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationPremiumTerm : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                FillDGR();
                FillDGRBenefit();
                ShowReport();
                ShowPremium();
                ShowCOI();
                ShowPremiumStatistic();
                CheckTrack();
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") > 3)
            {
                DGR_BENEFIT.Enabled = false;
            }
            else
            {
                BT_COI.Visible = false;
                BT_TRX.Visible = false;
                BT_STAT.Visible = false;
            }
        }

        protected void ShowReport()
        {
            conn.QueryString = "select REPORT_URL = (case when REPORT_URL2 <> '' then '../..' else '../Standard/default.html' end) + REPORT_URL2 from V_APPLICATION_MASTER_REPORT where MODE = 'TRX' and REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            IF.Src = conn.GetFieldValue(0, 0).ToString();
        }

        protected void ShowCOI()
        {
            conn.QueryString = "select " +
                                "a.REGNO " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PAYDI = 1 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                BT_COI.Visible = false;
                return;
            }

            conn.QueryString = "exec SP_APPLICATION_COI_CYCLE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_COI.DataSource = dt;
            DGR_COI.DataBind();
        }


        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_PAYMENT_CYCLE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[DGR.Columns.Count - 2].Text.Replace("&nbsp;", "") != "")
                {
                    DGR.Items[i].Cells[DGR.Columns.Count - 2].ForeColor = System.Drawing.Color.Green;
                    DGR.Items[i].Cells[DGR.Columns.Count - 3].ForeColor = System.Drawing.Color.Green;
                    DGR.Items[i].Cells[DGR.Columns.Count - 2].BackColor = System.Drawing.Color.LightGreen;
                    DGR.Items[i].Cells[DGR.Columns.Count - 3].BackColor = System.Drawing.Color.LightGreen;
                }

                if (DGR.Items[i].Cells[DGR.Columns.Count - 1].Text.Replace("&nbsp;", "") != "")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void FillDGRBenefit()
        {
            //conn.QueryString = "select " +
            //                    "a.REGNO " +
            //                    "from		APPLICATION_MASTER a " +
            //                    "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER_FINANCING_PLAN_TERM b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default " +
            //                    "where " +
            //                    "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.QueryString = "select " +
                                "a.REGNO " +
                                "from		APPLICATION_BENEFIT_CYCLE a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
            {
                BT_BENEFIT.Visible = false;
                return;
            }

            conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE " +  
                                        "'" + LB_REGNO.Text + "'," + 
                                        "0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
                if (DGR_BENEFIT.Items[i].Cells[5].Text == "1")
                    cb.Checked = true;

                if (DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 1].Text.Replace("&nbsp;", "") != "")
                {
                    DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 1].ForeColor = System.Drawing.Color.Red;
                    DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 2].ForeColor = System.Drawing.Color.Red;
                    DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                    DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 2].BackColor = System.Drawing.Color.Pink;
                }
            }

            FillDGRBenefitDeath();
        }

        protected void FillDGRBenefitDeath()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_DEAD.DataSource = dt;
            DGR_BENEFIT_DEAD.DataBind();

            for (int i = 0; i < DGR_BENEFIT_DEAD.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT_DEAD.Items[i].FindControl("CB");
                if (DGR_BENEFIT_DEAD.Items[i].Cells[5].Text == "1")
                    cb.Checked = true;

                if (DGR_BENEFIT_DEAD.Items[i].Cells[DGR_BENEFIT_DEAD.Columns.Count - 1].Text.Replace("&nbsp;", "") != "")
                {
                    DGR_BENEFIT_DEAD.Items[i].Cells[DGR_BENEFIT_DEAD.Columns.Count - 1].ForeColor = System.Drawing.Color.Red;
                    DGR_BENEFIT_DEAD.Items[i].Cells[DGR_BENEFIT_DEAD.Columns.Count - 2].ForeColor = System.Drawing.Color.Red;
                    DGR_BENEFIT_DEAD.Items[i].Cells[DGR_BENEFIT_DEAD.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                    DGR_BENEFIT_DEAD.Items[i].Cells[DGR_BENEFIT_DEAD.Columns.Count - 2].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void ShowPremiumStatistic()
        {
            conn.QueryString = "exec SP_APPLICATION_PREMIUM_PERFORMANCE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_LOM.Text = conn.GetFieldValue("LOM").ToString();
            LB_TENOR.Text = conn.GetFieldValue("TENOR").ToString();
            LB_ANNUAL_PREMIUM.Text = conn.GetFieldValue("ANNUAL_PREMIUM_EXPECTED").ToString();
            LB_REMAINING.Text = conn.GetFieldValue("REMAINING_PERIOD").ToString();

            LB_MTD_TERM_EXPECTED.Text = conn.GetFieldValue("MTD_TERM_EXPECTED").ToString();
            LB_MTD_AMOUNT_EXPECTED.Text = conn.GetFieldValue("MTD_PREMIUM_EXPECTED").ToString();
            LB_MTD_TERM_PAID.Text = conn.GetFieldValue("MTD_TERM_PAID").ToString();
            LB_MTD_AMOUNT_PAID.Text = conn.GetFieldValue("MTD_PREMIUM_PAID").ToString();
            LB_MTD_TERM_OUTSTANDING.Text = conn.GetFieldValue("MTD_TERM_OUTSTANDING").ToString();
            LB_MTD_AMOUNT_OUTSTANDING.Text = conn.GetFieldValue("MTD_PREMIUM_OUTSTANDING").ToString();

            LB_TOTAL_TERM_EXPECTED.Text = conn.GetFieldValue("TOTAL_TERM_EXPECTED").ToString();
            LB_TOTAL_AMOUNT_EXPECTED.Text = conn.GetFieldValue("TOTAL_PREMIUM_EXPECTED").ToString();
            LB_TOTAL_TERM_PAID.Text = conn.GetFieldValue("TOTAL_TERM_PAID").ToString();
            LB_TOTAL_AMOUNT_PAID.Text = conn.GetFieldValue("TOTAL_PREMIUM_PAID").ToString();
            LB_TOTAL_TERM_INCOMPLETED.Text = conn.GetFieldValue("TOTAL_TERM_INCOMPLETED").ToString();
            LB_TOTAL_AMOUNT_INCOMPLETED.Text = conn.GetFieldValue("TOTAL_PREMIUM_INCOMPLETED").ToString();
        }

        protected void ShowPremium()
        {
            LB_TITLE.Text = BT_PREMIUM.Text;
            DGR.Visible = true;
            TBL_BENEFIT.Visible = false;
            IF.Visible = false;
            DGR_COI.Visible = false;
            TBL_STAT.Visible = false;
        }

        protected void BT_PREMIUM_Click(object sender, EventArgs e)
        {
            ShowPremium();
        }

        protected void BT_BENEFIT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DGR.Visible = false;
            TBL_BENEFIT.Visible = true;
            IF.Visible = false;
            DGR_COI.Visible = false;
            TBL_STAT.Visible = false;
        }

        protected void BT_TRX_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DGR.Visible = false;
            TBL_BENEFIT.Visible = false;
            IF.Visible = true;
            DGR_COI.Visible = false;
            TBL_STAT.Visible = false;
        }

        protected void BT_COI_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DGR.Visible = false;
            TBL_BENEFIT.Visible = false;
            IF.Visible = false;
            DGR_COI.Visible = true;
            TBL_STAT.Visible = false;
        }

        protected void BT_STAT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DGR.Visible = false;
            TBL_BENEFIT.Visible = false;
            IF.Visible = false;
            DGR_COI.Visible = false;
            TBL_STAT.Visible = true;
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
                string tobepaid = "0";
                if (cb.Checked)
                    tobepaid = "1";

                conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    DGR_BENEFIT.Items[i].Cells[8].Text + "," +
                                    DGR_BENEFIT.Items[i].Cells[9].Text + "," +
                                    tobepaid + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRBenefit();
        }


    }
}