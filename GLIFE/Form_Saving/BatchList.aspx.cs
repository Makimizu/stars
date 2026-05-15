using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace GLIFE.Form_Saving
{
    public partial class BatchList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select STARTMON = '1/'+convert(varchar(4),MONTH(GETDATE()))+'/'+convert(varchar(4),YEAR(GETDATE()))";
            conn.ExecuteQuery();
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
                                "BATCH_ID		= a.BATCH_ID, " +
                                "POLICY_NO		= POLICY_NO, " +
                                "TC_DESCR       = TC_DESCR, " +
                                "COMPANY_NAME	= COMPANY_NAME + '<BR>' + BRANCH_DESCR, " +
                                "TOTAL			= convert(varchar(10), TOTAL_RAW)  + ' / ' + convert(varchar(10), TOTAL_EXP), " +
                                "FORMAT_DESCR	= FORMAT_DESCR, " +
                                "UPLOADBY		= USERBY  + '<BR>' + convert(varchar(50),USERDATE), " +
                                "CONTRIB		= replace(convert(varchar(100), convert(money, isnull(b.AMOUNT,0)),1), '.00', ''), " +
                                "INBOUND_TRX    = a.INBOUND_TRX " +
                                "from V_BATCH_MASTER_UPLOAD a " +
                                "left join V_BATCH_MASTER_SAVING_AMOUNT b on a.BATCH_ID = b.BATCH_ID " +
                                "where " +
                                "PRODUCT_GROUP = 'SP' and a.BATCH_TYPE not like 'SV_%' " +
                                "and (POLICY_NO like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or COMPANY_NAME like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or BRANCH_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or TC_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%') " + where + " " +
                                "order by a.USERDATE desc";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_POLICY.DataSource = dt;
            DGR_POLICY.DataBind();

            for (int i = 0; i < DGR_POLICY.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_POLICY.Items[i].FindControl("LBT_POLICY");
                Button btRK = (Button)DGR_POLICY.Items[i].FindControl("BT_RK");

                lbCODE.Text = DGR_POLICY.Items[i].Cells[1].Text + "<BR>" + DGR_POLICY.Items[i].Cells[2].Text;

                if (DGR_POLICY.Items[i].Cells[3].Text == "1")
                    btRK.Visible = true;
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
                Response.Redirect("BatchDetail.aspx?ID=" + e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Raw")
            {
                conn.QueryString = "select " +
                                    "a.ERROR_REMARK, " +
                                    "REGNO	= isnull(b.REGNO,'NOT EXPORTED'), " +
                                    "F01,F02,F03,F04,F05,F06,F07,F08,F09,F10,F11,F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30, " +
                                    "F31,F32,F33,F34,F35,F36,F37,F38,F39,F40,F41,F42,F43,F44,F45,F46,F47,F48,F49,F50,F51,F52,F53,F54,F55,F56,F57,F58,F59,F60 " +
                                    "from APPLICATION_DATA_RAW a " +
                                    "left join APPLICATION_MASTER b on a.REGNO = b.REGNO " +
                                    "where a.BATCH_ID = '" + e.Item.Cells[0].Text + "' " +
                                    "order by a.REGNO";

                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                GlobalUse.ExportDataSetToExcel(dt, this, "QUOTATION_RAW_DATA", false);
            }

            if (e.CommandName == "Delete")
            {
                //try
                //{
                conn.QueryString = "delete from TRACK_DATA where TIPE_CODE in ('NB','UW') and OWNER collate database_default in (select REGNO from APPLICATION_MASTER where BATCH_ID = '" + e.Item.Cells[0].Text + "') " +
                                    "delete from APPLICATION_MASTER where BATCH_ID = '" + e.Item.Cells[0].Text + "' " +
                                    "delete from BATCH_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                //}
                //catch { }

                FillDGR();
            }

            if (e.CommandName == "RK")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
                ShowBankStatement(e.Item.Cells[0].Text, e.Item.Cells[1].Text + " - " + e.Item.Cells[2].Text, e.Item.Cells[7].Text);
            }
        }

        protected void ShowBankStatement(string batchid, string policy, string amount)
        {
            LB_BATCHID.Text = batchid;
            LB_DATA_AMOUNT.Text = amount;
            LB_POLICY.Text = policy;

            conn.QueryString = "select START_DATE = convert(varchar(20), dateadd(month,-6,GETDATE()), 103)";
            conn.ExecuteQuery();

            TXT_POSTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_POSTDATE2.Text = "";

            conn.QueryString = "select NOREK, BANK from V_LINK_FINANCE_REKENING_MASTER where NOREK not in ('COLLDEP') order by 2";
            conn.ExecuteQuery();
            DDL_NOREK.Items.Clear();
            DDL_NOREK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDGRSelected();
            FillDGRBankStatement();
        }

        protected void FillDGRBankStatement()
        {
            string where = "";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_NOREK.SelectedValue != "")
                where = where + "and a.NOREK = '" + DDL_NOREK.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "a.TRXID, " +
                                "POST_DATE	= convert(varchar(20), a.POST_DATE, 106), " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, a.BALANCE),1), '.00',''), " +
                                "DESCR = LEFT(a.DESCR, 100), " +
                                "a.BOOK_NAME " +
                                "from V_LINK_FINANCE_REKENING_JURNAL a " +
                                //"left join BATCH_MASTER_BANK_STATEMENT b on a.TRXID = b.TRXID " +
                                "where " +
                                //"b.TRXID is null and TIPE_VALIDASI = '001' and a.NOREK not in ('COLLDEP') and ROUND(a.BALANCE,0) > 0 " + where +
                                "TIPE_VALIDASI = '001' and a.NOREK not in ('COLLDEP') and ROUND(a.BALANCE,0) > 0 " + where +
                                "order by a.POST_DATE";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_POLICY.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("BatchUpload.aspx");
        }

        protected void BT_RK_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGRBankStatement();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Settle")
            {
                conn.QueryString = "exec SP_BATCH_MASTER_BANK_STATEMENT_INSERT " +
                                    "'" + LB_BATCHID.Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRSelected();
                FillDGRBankStatement();
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "select " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, b.BALANCE),1), '.00',''), " +
                                "READY		= (case when isnull(a.AMOUNT, 0) <= isnull(b.BALANCE, 0) then 1 else 0 end) " +
                                "from		V_BATCH_MASTER_SAVING_AMOUNT a " +
                                "left join	(	select  " +
                                "                BALANCE = SUM(b.BALANCE) " +
                                "                from		BATCH_MASTER_BANK_STATEMENT a " +
                                "                inner join	V_LINK_FINANCE_REKENING_JURNAL b on a.TRXID = b.TRXID " +
                                "                where " +
                                "                a.BATCH_ID = '" + LB_BATCHID.Text + "' " +
                                "                ) b on 1=1" +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCHID.Text + "'";
            conn.ExecuteQuery();
            LB_BANK_AMOUNT.Text = conn.GetFieldValue("BALANCE").ToString();

            BT_EXECUTE.Visible = false;
            if (conn.GetFieldValue("READY").ToString() == "1")
                BT_EXECUTE.Visible = true;

            conn.QueryString = "select " +
                                "a.ID, " +
                                "POST_DATE	= convert(varchar(20), b.POST_DATE, 106),  " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, b.BALANCE),1), '.00',''),  " +
                                "b.BOOK_NAME " +
                                "from		BATCH_MASTER_BANK_STATEMENT a " +
                                "inner join	V_LINK_FINANCE_REKENING_JURNAL b on a.TRXID = b.TRXID " +
                                "where " +
                                "a.BATCH_ID = '" + LB_BATCHID.Text + "'";
            conn.ExecuteQuery();

            DGR_SELECTED.DataSource = conn.GetDataTable().Copy();
            DGR_SELECTED.DataBind();
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from BATCH_MASTER_BANK_STATEMENT where ID = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRSelected();
                FillDGRBankStatement();
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void BT_EXECUTE_Click(object sender, EventArgs e)
        {   
            //Task.Run(() => ProcessBatch(LB_BATCHID.Text));
            ProcessBatch(LB_BATCHID.Text);
            FillDGR();
        }

        protected void ProcessBatch(string batchid)
        {
            conn.QueryString = "exec SP_BATCH_MASTER_BANK_STATEMENT_INVOICE_INSERT " +
                                "'" + batchid + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(50000);
        }
    }
}