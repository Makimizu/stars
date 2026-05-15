using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace CORPORATE_PORTAL
{
    public partial class ClaimHistory : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
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
            //conn.QueryString = "select SEQ, DESCR from V_LINK_HO_PARAM_TRACK where TIPE_CODE = 'CLMMASTER' and SEQ in (3,4) order by 2";
            conn.QueryString = "select SEQ, DESCR from V_LINK_HO_PARAM_TRACK where TIPE_CODE = 'CLMMASTER' order by 1 desc";
            conn.ExecuteQuery();
            DDL_STATUS.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            conn.QueryString = "select distinct " +
                                "e.ID, " +
                                "e.DESCR, " +
                                "e.ORDER_NO " +
                                "from V_LINK_HO_POLICY a " +
                                "inner join V_LINK_HO_POLICY_PERIOD b on a.ID = b.POLICY_ID " +
                                "inner join V_LINK_HO_POLICY_PERIOD_BENEFIT c on b.ID = c.POLICY_PERIOD_ID " +
                                "inner join V_LINK_HO_POLICY_PERIOD_PACKAGE_PLAN d on c.ID = d.POLICY_PERIOD_BENEFIT_ID " +
                                "inner join V_LINK_HO_PARAM_ACT_BENEFIT e on c.BENEFIT_ID = e.ID " +
                                "where " +
                                "a.ID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                "order by e.ORDER_NO";
            conn.ExecuteQuery();
            DDL_BENEFIT.Items.Clear();
            DDL_BENEFIT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_BENEFIT.SelectedValue != "")
                where = where + " and e.BENEFIT_CODE = '" + DDL_BENEFIT.SelectedValue + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and c.NAMA like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_DATE1.Text.Trim() != "")
                where = where + " and convert(date, b.TGL_RAWAT_DARI) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + " and convert(date, b.TGL_RAWAT_DARI) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "b.CLAIM_NO, " +
                                "c.NAMA, " +
                                "b.PR, " +
                                "BENEFIT_DESCR = f.DESCR, " +
                                "ACT_DATE = convert(varchar(20), b.TGL_RAWAT_DARI, 106) + ' - ' + convert(varchar(20), b.TGL_RAWAT_SAMPAI, 106),  " +
                                "INCURRED = replace(convert(varchar(20), convert(money, e.INCURRED), 1), '.00', ''),  " +
                                "REJECTED = replace(convert(varchar(20), convert(money, e.REJECT), 1), '.00', ''),  " +
                                "APPROVED = replace(convert(varchar(20), convert(money, e.APPROVED), 1), '.00', ''),  " +
                                "STAT = b.LAST_TRACK_DESCR " +
                                "from V_LINK_HO_CLM_CLAIM_MASTER b " +
                                "inner join V_LINK_HO_PESERTA_MASTER c on b.REGNO = c.REGNO " +
                                "left join(select " +
                                "                CLAIM_NO, " +
                                "                BENEFIT_CODE = min(c.BENEFIT_CODE), " +
                                "                INCURRED    = SUM(AMOUNT_PENGAJUAN), " +
                                "                REJECT      = SUM(isnull(AMOUNT_UNPAID, 0) + isnull(AMOUNT_EXCESS, 0)), " +
                                "                APPROVED    = SUM(AMOUNT_BAYAR) " +
                                "                from V_LINK_HO_CLAIM_BENEFIT a " +
                                "                inner join V_LINK_HO_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID = b.ID " +
                                "                inner join V_LINK_HO_PARAM_ACT_BENEFIT_DETAIL c on b.MORBIDITY_ID = c.ID " +
                                "                group by CLAIM_NO " +
                                "                ) e on b.CLAIM_NO = e.CLAIM_NO " +
                                "left join V_LINK_HO_PARAM_ACT_BENEFIT f on e.BENEFIT_CODE = f.ID " +
                                "where " +
                                "b.LAST_TRACK = " + DDL_STATUS.SelectedValue + " " +
                                "and c.POLICY_ID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " + where + " " +
                                "order by b.TGL_RAWAT_DARI desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_VIEW");
                Label lbIncurred = (Label)DGR.Items[i].FindControl("LBL_INCURRED");
                Label lbReject = (Label)DGR.Items[i].FindControl("LBL_REJECT");
                Label lbApproved = (Label)DGR.Items[i].FindControl("LBL_APPROVED");

                lb.Text = "<table style='border-spacing:0px;'><tr><td style='width:100px;'>" + DGR.Items[i].Cells[0].Text + "</td><td>" + DGR.Items[i].Cells[1].Text + "</td></tr></table>";
                lbIncurred.Text = DGR.Items[i].Cells[6].Text;
                lbReject.Text = DGR.Items[i].Cells[7].Text;
                lbApproved.Text = DGR.Items[i].Cells[8].Text;

                if (DGR.Items[i].Cells[6].Text == "0")
                    lbApproved.Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("ClaimDetail.aspx?CLAIM_NO=" + e.Item.Cells[0].Text);
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_STAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
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