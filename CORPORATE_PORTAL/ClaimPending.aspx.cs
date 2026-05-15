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
    public partial class ClaimPending : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";

            conn.QueryString = "select " +
                                "a.CLAIM_NO, " +
                                "a.NAMA, " +
                                "a.BENEFIT_DESCR, " +
                                "a.PR, " +
                                "ACT_DATE = convert(varchar(20), a.TGL_RAWAT_DARI, 106) + '<BR>' + convert(varchar(20), a.TGL_RAWAT_SAMPAI, 106), " +
                                "INCURRED = replace(convert(varchar(20),convert(money,d.AMOUNT_PENGAJUAN),1),'.00',''), " +
                                "REJECTED = replace(convert(varchar(20),convert(money,d.JUMLAH_TOLAK + d.EKSES),1),'.00',''), " +
                                "APPROVED = replace(convert(varchar(20),convert(money,d.AMOUNT_BAYAR),1),'.00',''), " +
                                "PROVIDER = a.PROVIDER, " +
                                "STAT = a.LAST_TRACK_DESCR, " +
                                "TP = isnull(aa.TP, 0) " +
                                "from V_LINK_HO_CLM_CLAIM_MASTER a " +
                                "inner join V_LINK_HO_CLAIM_TP aa on a.CLAIM_NO = aa.DOCNO and aa.TP = 2 " +
                                "inner join V_LINK_HO_PESERTA_MASTER b on a.REGNO = b.REGNO " +
                                "left join V_LINK_HO_CLM_CLAIM_BENEFIT_SUM d on a.CLAIM_NO = d.CLAIM_NO " +
                                "where " +
                                "a.LAST_TRACK in (1,2) " +
                                "and a.POLICY_ID = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                "order by a.TGL_RAWAT_DARI desc";
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

                lb.Text = DGR.Items[i].Cells[0].Text + "<BR><B>" + DGR.Items[i].Cells[1].Text + "</B>";
                lbIncurred.Text = DGR.Items[i].Cells[7].Text;
                lbReject.Text = DGR.Items[i].Cells[8].Text;
                lbApproved.Text = DGR.Items[i].Cells[9].Text;

                if (DGR.Items[i].Cells[7].Text == "0")
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
    }
}