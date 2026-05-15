using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Form_Health
{
    public partial class Claim_List : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from V_LINK_HO_PR_CLAIM_DOC_SOURCE order by 1";
            conn.ExecuteQuery();

            DDL_SOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select ID, DESCR from V_LINK_HO_PARAM_ACT_BENEFIT order by ORDER_NO";
            conn.ExecuteQuery();

            DDL_BENEFIT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select SEQ, DESCR from V_LINK_HO_PARAM_TRACK where TIPE_CODE = 'CLMMASTER' order by SEQ desc";
            conn.ExecuteQuery();

            DDL_TRACK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRACK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "", agentjoin = "";

            if (TXT_CLAIMNO.Text.Trim() != "")
                where = where + " and a.CLAIM_NO = '" + TXT_CLAIMNO.Text.Trim() + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and a.DOC_NO like '%" + TXT_DOCNO.Text.Trim() + "%' ";

            if (TXT_PROVIDER.Text.Trim() != "")
                where = where + " and a.PROVIDER like '%" + TXT_PROVIDER.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (DDL_SOURCE.SelectedValue != "")
                where = where + " and a.DOC_SOURCE = '" + DDL_SOURCE.SelectedValue + "' ";

            if (DDL_PR.SelectedValue != "")
                where = where + " and a.PR = '" + DDL_PR.SelectedValue + "' ";

            if (DDL_BENEFIT.SelectedValue != "")
                where = where + " and a.BENEFIT_CODE = '" + DDL_BENEFIT.SelectedValue + "' ";

            if (DDL_TRACK.SelectedValue != "")
                where = where + " and a.LAST_TRACK = '" + DDL_TRACK.SelectedValue + "' ";

            if (TXT_DOCDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_DOC) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOCDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DOCDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_DOC) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOCDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_CLAIMDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_KLAIM) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_CLAIMDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_KLAIM) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATECARE1.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_RAWAT_DARI) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATECARE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATECARE2.Text.Trim() != "")
                where = where + " and convert(date,a.TGL_RAWAT_DARI) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATECARE2.Text.Trim(), "d/M/yyyy") + "') ";


            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
            {
                string downliners = "";

                conn.QueryString = "exec SP_LINK_MR_AGENT_DOWNLINER '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    downliners = downliners + ",'" + conn.GetFieldValue(i, 0).ToString() + "'";
                }

                agentjoin = "inner join V_LINK_HO_POLICY_PERIOD d on a.POLICY_PERIOD_ID = d.ID and d.AGENT_CODE in ('" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'" + downliners + ") ";
            }

            conn.QueryString = "select " +
                                "a.CLAIM_NO, " +
                                "a.LAST_TRACK_DESCR, " +
                                "a.BENEFIT_DESCR, " +
                                "a.NAMA, " +
                                "a.COMPANY_NAME, " +
                                "a.PROVIDER, " +
                                "a.PR_DESCR, " +
                                "TGL_KLAIM = convert(varchar(20),a.TGL_KLAIM,106), " +
                                "TGL_RAWAT = convert(varchar(20),a.TGL_RAWAT_DARI,106) + ' - ' + convert(varchar(20),a.TGL_RAWAT_SAMPAI,106), " +
                                "TGL_TRANSFER = convert(varchar(20),c.POST_DATE,106), " +
                                "REPORT_URL	= b.URL + '&rc:Parameters=False&CLAIM_NO=' + a.CLAIM_NO + '&rc:Zoom=Page20%Width', " +
                                "DOCNO	= e.REKAPID " +
                                "from V_LINK_HO_CLM_CLAIM_MASTER a " + agentjoin + " " +
                                "inner join V_LINK_SC_REPORT_LIST b on b.APP_ID='HO' and b.CODE='288' " +
                                "left join V_LINK_FN_SETTLEMENT_DETAIL_PAID c on c.APP_ID='HO' and c.TIPE_SETTLEMENT in ('1','1a','5','5a') and c.DOCNO = a.CLAIM_NO collate database_default " +
                                "left join finance.dbo.SETTLEMENT_DETAIL e on e.docno = a.claim_no "+
                                "where " +
                                "1 = 1 " + where +
                                "order by a.TGL_KLAIM";
            conn.ExecuteQuery(150000);

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                lbID.Text = DGR.Items[i].Cells[1].Text;
                lbID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text + "','CLAIM_DETAIL','height=400,width=800,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
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