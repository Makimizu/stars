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
    public partial class ClaimBatchInquiry : System.Web.UI.Page
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
            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery(120);
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            LB_SQL.Text = "";
            string where = "";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and DOC_NO like '%" + TXT_DOCNO.Text.Trim() + "%' ";

            if (TXT_DOCMEMONO.Text.Trim() != "")
                where = where + " and isnull(REKAPID,'') like '%" + TXT_DOCMEMONO.Text.Trim() + "%' ";

            if (TXT_PROVCOM.Text.Trim() != "")
                where = where + " and INSTITUTION like '%" + TXT_PROVCOM.Text.Trim() + "%' ";
                        
            if (TXT_DATE.Text.Trim() != "")
                where = where + " and datediff(day,'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "',a.TGL_DOC) >= 0 ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + " and datediff(day,a.TGL_DOC,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "') >= 0 ";

            if (TXT_TGLKLM1.Text.Trim() != "")
                where = where + " and datediff(day,'" + GlobalUse.GlobalDateFormat(TXT_TGLKLM1.Text.Trim(), "d/M/yyyy") + "',a.TGL_KLAIM) >= 0 ";

            if (TXT_TGLKLM2.Text.Trim() != "")
                where = where + " and datediff(day,a.TGL_KLAIM,'" + GlobalUse.GlobalDateFormat(TXT_TGLKLM2.Text.Trim(), "d/M/yyyy") + "') >= 0 ";

            if (DDL_OUTS.SelectedValue != "")
                where = where + " " + DDL_OUTS.SelectedValue + " ";

            if (TXT_AGING1.Text.Trim() != "")
                where = where + " and AGING >= " + TXT_AGING1.Text.Trim() + " ";

            if (TXT_AGING2.Text.Trim() != "")
                where = where + " and AGING <= " + TXT_AGING2.Text.Trim() + " ";

            string SQL1 = "select " +
                        "BATCH_ID, " +
                        "DOC_NO, " +
                        "TGL_DOC = convert(varchar(20),a.TGL_DOC,106), " +
                        "TGL_KLAIM = convert(varchar(20),a.TGL_KLAIM,106), " +
                        "INSTITUTION, " +
                        "BILLED = replace(convert(varchar(100),convert(money,BILLED),1),'.00',''), " +
                        "REJECT = replace(convert(varchar(100),convert(money,REJECT),1),'.00',''), " +
                        "PAID = replace(convert(varchar(100),convert(money,PAID),1),'.00',''), " +
                        "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                        "LAST_PAIDDATE = convert(varchar(20),a.LAST_PAIDDATE,106), " +
                        "CNT, " +
                        "AGING, " +
                        "REKAPID, " +
                        "REKAPID_URL, " +
                        "URL_REKAP	= c.URL + '&rc:Parameters=False&DOCNO=' + a.BATCH_ID, " +
                        "URL_DETAIL	= b.URL + '&rc:Parameters=False&DOCNO=' + a.BATCH_ID, " +
                        "URL_PAID	= d.URL + '&rc:Parameters=False&DOCNO=' + a.BATCH_ID ";
            string SQL1b = "select " +
                        "BATCH_ID, " +
                        "DOC_NO, " +
                        "TGL_DOC = convert(varchar(20),a.TGL_DOC,106), " +
                        "TGL_KLAIM = convert(varchar(20),a.TGL_KLAIM,106), " +
                        "INSTITUTION, " +
                        "BILLED = BILLED, " +
                        "REJECT = REJECT, " +
                        "PAID = PAID, " +
                        "OUTSTANDING = OUTSTANDING, " +
                        "LAST_PAIDDATE = convert(varchar(20),a.LAST_PAIDDATE,106), " +
                        "CNT, " +
                        "AGING, " +
                        "REKAPID ";
            string SQL2 = "from V_CLM_CLAIM_BATCH_REKAP a " +                                
                        "inner join V_LINK_SC_REPORT_LIST b on b.CODE='294' " +
                        "left join V_LINK_SC_REPORT_LIST c on c.CODE='295' " +
                        "left join V_LINK_SC_REPORT_LIST d on d.CODE='296' " +
                        "where " +
                        "a.PR = '" + DDL_PR.SelectedValue + "' and a.BILLED > 0 " + where +
                        "order by a.TGL_DOC";

            conn.QueryString = SQL1+SQL2;
            conn.ExecuteQuery(120);

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            LB_RECORDS.Text = " : " + conn.GetRowCount().ToString() + " Batch";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "BILLED = replace(convert(varchar(100),convert(money,SUM(BILLED)),1),'.00',''), " +
                                "PAID = replace(convert(varchar(100),convert(money,SUM(PAID)),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,SUM(OUTSTANDING)),1),'.00','') " +
                                "from V_CLM_CLAIM_BATCH_REKAP a  " +
                                "where " +
                                "a.PR = '" + DDL_PR.SelectedValue + "' and a.BILLED > 0 " + where;
            conn.ExecuteQuery();

            LB_BILLED.Text = " : " + conn.GetFieldValue("BILLED").ToString();
            LB_PAID.Text = " : " + conn.GetFieldValue("PAID").ToString();
            LB_OUTSTANDING.Text = " : " + conn.GetFieldValue("OUTSTANDING").ToString();

            if (conn.GetRowCount() > 0)
            {
                BT_XLS.Visible = true;
                LB_SQL.Text = SQL1b + SQL2;
            }

            for(int i=0; i<DGR.Items.Count;i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                lbREKAPID.Text = DGR.Items[i].Cells[15].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "REKAPID")
            {
                if (e.Item.Cells[15].Text.Replace("&nbsp;","") != "0")
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[16].Text + "','MEMO','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "Rekap")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[11].Text + "','DETAIL','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "Detail")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[12].Text + "','DETAIL','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "Paid")
            {
                if (e.Item.Cells[8].Text != "0")
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[13].Text + "','DETAIL','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = LB_SQL.Text;
            conn.ExecuteQuery(120);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "DATA INQUIRY BATCH CLAIM", true);
        }
    }
}