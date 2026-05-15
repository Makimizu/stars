using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Claim
{
    public partial class ClaimSendEmail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TRACK.Text = Request.QueryString["seq"].ToString();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_SENDED.SelectedValue != "")
                where = where + "  ";

            if (DDL_STATUS.SelectedValue != "")
                where = where + " and (case when g.PENDING_CODE is not null and g.CLOSINGDATE is null then 'PENDING' else a.LAST_TRACK_DESCR end) = '" + DDL_STATUS.SelectedValue + "' ";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and f.DOCNO = '" + TXT_DOCNO.Text.Trim() + "' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO = '" + TXT_REGNO.Text.Trim() + "' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_CLAIMDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CLAIMDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString =  "select a.REGNO, " +
                                "       f.DOCNO, " +
                                "		a.SEQ, " +
                                "		a.FULLNAME, " +
                                "		DOB = convert(varchar(20), a.DOB, 106), " +
                                "		CLAIM_DATE = convert(varchar(20), a.CLAIM_DATE, 106), " +
                                "		a.TC_DESCR, " +
                                "		a.POLICY_NO, " +
                                "		a.COMPANY_NAME, " +
                                "       e.BRANCH_PIC, " +
                                "       c.BRANCH_CODE, " +
                                "		d.PIC_NAMA," +
                                "		d.PIC_EMAIL," +
                                "		INCURRED = replace(convert(varchar(100), convert(money, b.BENEFIT_AMOUNT),1), '.00',''), " +
                                "		ADDITION = replace(convert(varchar(100), convert(money, b.CREDIT_AMOUNT),1), '.00',''), " +
                                "		DEDUCTION = replace(convert(varchar(100), convert(money, b.DEBET_AMOUNT),1), '.00',''), " +
                                "		TOTAL = replace(convert(varchar(100), convert(money, b.TOTAL_AMOUNT),1), '.00',''), " +
                                "		(case when g.PENDING_CODE is not null and g.CLOSINGDATE is null then 'PENDING' else a.LAST_TRACK_DESCR end) as LAST_TRACK_DESCR," +
                                "       a.LAST_TRACK_DESCR," +
                                "		a.CLAIM_TYPE_DESCR," +
                                "       g.PENDING_CODE," +
                                "       g.PENDINGDATE," +
                                "       g.CLOSINGDATE " +
                                "from V_APPLICATION_CLAIM_MASTER a " +
                                "inner join V_APPLICATION_CLAIM_AMOUNT_SUMM b on a.REGNO = b.REGNO and a.SEQ = b.SEQ " +
                                "inner join APPLICATION_MASTER c on c.REGNO = a.REGNO " +
                                "left join CLIENT_BASE.dbo.BRANCH_CORRESPONDENCE d on d.BRANCH_CODE = c.BRANCH_CODE and d.TIPE_KORESPONDEN='CLM' " +
                                "left join CLIENT_BASE.dbo.BRANCH e on e.BRANCH_CODE = c.BRANCH_CODE " +
                                "LEFT JOIN APPLICATION_CLAIM_DOCUMENT_LETTER f ON f.REGNO = a.REGNO AND f.CODE = '033' " +
                                "left join APPLICATION_MASTER_PENDING g on g.REGNO = a.REGNO and g.PENDINGDATE = (select max(PENDINGDATE) from APPLICATION_MASTER_PENDING where REGNO = g.REGNO)" +
                                "where a.LAST_TRACK in (1,2,3,4,5) " + where + " ";
            conn.ExecuteQuery();

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREGNO = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                lbREGNO.Text = DGR.Items[i].Cells[1].Text;

                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                txtEMAIL.Text = DGR.Items[i].Cells[11].Text.Trim().Replace("&nbsp;", "");

            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string ReadOnly = "";
                if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
                    ReadOnly = "&readonly=1";

                Response.Redirect("ClaimAppFrame.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + e.Item.Cells[2].Text + ReadOnly);
            }

            if (e.CommandName == "Process")
            {
                try
                {
                    TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
                    conn.QueryString = "exec CLIENT_BASE.dbo.SP_BRANCH_CORRESPONDENCE_UPSERT '" + e.Item.Cells[7].Text + "','CLM','" + e.Item.Cells[8].Text + "','0','','','" + txtEMAIL.Text.Trim() + "','system' ";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}