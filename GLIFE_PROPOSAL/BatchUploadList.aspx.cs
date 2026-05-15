using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class BatchUploadList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select STARTMON = '1/'+convert(varchar(4),MONTH(GETDATE()))+'/'+convert(varchar(4),YEAR(GETDATE()))";
            conn.ExecuteQuery(1500000);
            TXT_REGDATE1.Text = conn.GetFieldValue("STARTMON").ToString();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date,USERDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date,USERDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "BATCH_ID		= BATCH_ID, " +
                                "POLICY_NO		= POLICY_NO, " +
                                "TC_DESCR       = TC_DESCR, " +
                                "COMPANY_NAME	= COMPANY_NAME + '<BR>' + BRANCH_DESCR, " +
                                "TOTAL			= convert(varchar(10), TOTAL_RAW)  + ' / ' + convert(varchar(10), TOTAL_EXP), " +
                                "FORMAT_DESCR	= FORMAT_DESCR, " +
                                "UPLOADBY		= USERBY  + '<BR>' + convert(varchar(50),USERDATE) " +
                                "from V_BATCH_MASTER_UPLOAD a " +
                                "where " +
                                "(POLICY_NO like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or COMPANY_NAME like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or BRANCH_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or TC_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%') " + where + " " +
                                "order by a.USERDATE desc";
            conn.ExecuteQuery(1500000);

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_POLICY.DataSource = dt;
            DGR_POLICY.DataBind();

            for (int i = 0; i < DGR_POLICY.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_POLICY.Items[i].FindControl("LBT_POLICY");

                lbCODE.Text = DGR_POLICY.Items[i].Cells[1].Text + "<BR>" + DGR_POLICY.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_POLICY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_POLICY.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                DV_LIST.Visible = false;
                DV_SHOW.Visible = true;

                LB_BATCHID.Text = e.Item.Cells[0].Text;
                LB_POLICYNO.Text = e.Item.Cells[1].Text + " - " + e.Item.Cells[2].Text;
                LB_COMPANY.Text = e.Item.Cells[4].Text.Replace("<BR>", " - ");

                conn.QueryString = "select " +
                                    "URLAPP = e.URLAPP + '&BATCH_ID=' + convert(varchar(100), a.ID), " +
                                    "EMAIL = MIN(isnull(d.EMAIL,'')) + ',' + MIN(isnull(c.PIC_EMAIL,'')) collate database_default " +
                                    "from BATCH_MASTER a " +
                                    "inner join QUOTATION_MASTER b on a.ID = b.BATCH_ID " +
                                    "left join V_LINK_CB_BRANCH_CORRESPONDENCE c on b.BRANCH_CODE = c.BRANCH_CODE and c.TIPE = '3' " +
                                    "left join V_LINK_SC_M_USERS d on b.USERBY = d.CODE collate database_default " +
                                    "inner join V_LINK_SC_REPORT_LIST e on e.CODE = 1 " +
                                    "where a.ID = '" + LB_BATCHID.Text + "' " +
                                    "group by " +
                                    "a.ID, " +
                                    "e.URLAPP";
                conn.ExecuteQuery(1500000);
                TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
                IF.Src = conn.GetFieldValue("URLAPP").ToString();
            }

            if (e.CommandName == "Raw")
            {
                conn.QueryString = "select " +
                                    "a.ERROR_REMARK, " +
                                    "REGNO	= isnull(b.REGNO,'NOT EXPORTED'), " +
                                    "F01,F02,F03,F04,F05,F06,F07,F08,F09,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30, " +
                                    "F31,F32,F33,F34,F35,F36,F37,F38,F39,F40,F41,F42,F43,F44,F45,F46,F47,F48,F49,F50,F51,F52,F53,F54,F55,F56,F57,F58,F59,F60 " +
                                    "from QUOTATION_DATA_RAW a " +
                                    "left join QUOTATION_MASTER b on a.REGNO = b.REGNO " +
                                    "where a.BATCH_ID = '" + e.Item.Cells[0].Text + "' " +
                                    "order by a.REGNO";

                conn.ExecuteQuery(1500000);
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                GlobalUse.ExportDataSetToExcel(dt, this, "QUOTATION_RAW_DATA", false);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from BATCH_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                FillDGR();
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_POLICY.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_BACK_Click(object sender, EventArgs e)
        {
            DV_LIST.Visible = true;
            DV_SHOW.Visible = false;
        }

        protected void BT_EMAIL_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_JOB_EMAIL_INVOICE_BATCH " +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'";
                conn.ExecuteNonQuery();
            }
            catch { }
        }
    }
}