using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class ADMEDIKA_export : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }

        }

        protected void Setup()
        {
            conn.QueryString = "select TODAY = convert(varchar(20),GETDATE(),103)";
            conn.ExecuteQuery();
            TXT_APRDATE1.Text = conn.GetFieldValue("TODAY").ToString();
            TXT_APRDATE2.Text = conn.GetFieldValue("TODAY").ToString();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_APRDATE1.Text.Trim() != "")
                where = where + "and convert(date,a.USER_ENDDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE1.Text.Trim(), "d/M/yyyy") + "') ";
            if (TXT_APRDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.USER_ENDDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "a.CLAIM_NO, " +
                                "APPROVALDATE = convert(varchar(20),a.USER_ENDDATE,106), " +
                                "a.NAMA, " +
                                "a.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "INCURRED		= replace(convert(varchar(100),convert(money,b.AMOUNT_PENGAJUAN),1),'.00',''), " +
                                "REJECTED		= replace(convert(varchar(100),convert(money,b.JUMLAH_TOLAK),1),'.00',''), " +
                                "APPROVED		= replace(convert(varchar(100),convert(money,b.AMOUNT_BAYAR),1),'.00','') " +
                                "from V_CLM_CLAIM_MASTER a " +
                                "inner join V_CLM_CLAIM_BENEFIT_SUM b on a.CLAIM_NO=b.CLAIM_NO " +
                                "where " +
                                "a.LAST_TRACK = '4' " + 
                                "and a.PR = '" +DDL_PR.SelectedValue+"' " + where +
                                "order by a.USER_ENDDATE desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();
            if (conn.GetRowCount() > 0)
            {
                BT_REPORT.Visible = true;
                DDL_REPORT.Visible = true;
            }

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = true;
            }
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_REPORT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select " +                                
                                "PAYORID, " +
                                "CORPORATEID, " +
                                "POLICYNUMBER, " +
                                "MEMBERID, " +
                                "NIK, " +
                                "BRANCHCODE, " +
                                "CARDNO, " +
                                "MEMBERNAME, " +
                                "CLAIMID, " +
                                "CLAIMTYPE, " +
                                "CLAIMSTATUS, " +
                                "PROVIDERCODE, " +
                                "ADMISSION_DATE, " +
                                "DISCHARGE_DATE, " +
                                "DURATION, " +
                                "COVERAGEID, " +
                                "PLANID, " +
                                "DISABILITYNO, " +
                                "DIAGNOSISCODE, " +
                                "SECONDARYDIAGNOSISCODE, " +
                                "AMOUNT_INCURRED, " +
                                "AMOUNT_APPROVED, " +
                                "AMOUNT_NOT_APPROVED, " +
                                "AMOUNT_ASO_APPROVED, " +
                                "HIGH_PLAN, " +
                                "REMARKS, " +
                                "PROVIDEREXCESSPAID, " +
                                "PAYORINVOICEID, " +
                                "HOSPITALINVOICEDATE, " +
                                "HOSPITALINVOICENO, " +
                                "RECEIVEDDATE, " +
                                "SUBMISSIONDATE, " +
                                "VERIFYBY " +
                                "from V_CLM_SMART_EXPORT_HEADER where CLAIMID in (";
            if(DDL_REPORT.SelectedValue == "DETAIL")
                conn.QueryString = "select * from V_CLM_SMART_EXPORT_DETAIL where CLAIMID in (";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                    conn.QueryString = conn.QueryString + "'" + DGR.Items[i].Cells[1].Text + "',";
            }

            conn.QueryString = conn.QueryString + "'') order by CLAIMID";
            if (DDL_REPORT.SelectedValue == "DETAIL")
                conn.QueryString = conn.QueryString + ",BENEFITCODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            string filename = DDL_PR.SelectedItem.Text + "_" + DDL_REPORT.SelectedValue + "_" + TXT_APRDATE1.Text.Trim() + "-" + TXT_APRDATE2.Text.Trim() + ".TXT";

            GlobalUse.ToCSV(dt, this, filename, false, "\"", ",");
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            bool bCheck = ((CheckBox)sender).Checked;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = bCheck;
            }
        }
    }
}