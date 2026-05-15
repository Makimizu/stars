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
    public partial class ClaimUnapprove : System.Web.UI.Page
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

        }

        protected void FillDGR()
        {
            string where = "";
            LB_RESULT.Text = "";

            if (TXT_CLAIMNO.Text.Trim() != "")
                where = where + " and a.CLAIM_NO like '%" +TXT_CLAIMNO.Text.Trim()+ "%' ";

            if (TXT_STLDOC.Text.Trim() != "")
                where = where + " and c.REKAPID like '%" + TXT_STLDOC.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.NAMA like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (DDL_PR.SelectedValue != "")
                where = where + " and a.PR like '" + DDL_PR.SelectedValue + "' ";

            if (TXT_APRDATE1.Text.Trim() != "")
                where = where + "and convert(date,a.USER_ENDDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_APRDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.USER_ENDDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "a.CLAIM_NO, " +
                                "a.NAMA, " +
                                "a.COMPANY_NAME, " +
                                "a.POLICY_NO, " +
                                "a.PR_DESCR, " +
                                "TGL_RAWAT = convert(varchar(20),a.TGL_RAWAT_DARI,106) + ' - ' + convert(varchar(20),a.TGL_RAWAT_SAMPAI,106), " +
                                "TGL_KLAIM = convert(varchar(20),a.TGL_KLAIM,106), " +
                                "TGL_APPROVE = convert(varchar(20),a.USER_ENDDATE,106), " +
                                "a.BENEFIT_DESCR, " +
                                "c.REKAPID, " +
                                "c.URL " +
                                "from V_CLM_CLAIM_MASTER a " +
                                "inner join V_LINK_FINANCE_SETTLEMENT_DETAIL b on a.CLAIM_NO=b.DOCNO and b.TIPE_SETTLEMENT in ('1','1a','5','5a') " +
                                "inner join V_LINK_FINANCE_SETTLEMENT_MASTER c on b.REKAPID=c.REKAPID and c.APPROVALBY is null " +
                                "where " +
                                "a.LAST_TRACK = '4' " + where +
                                "order by a.USER_ENDDATE";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCLAIMNO = (LinkButton)DGR.Items[i].FindControl("LB_CLAIMNO");
                LinkButton lbSTL = (LinkButton)DGR.Items[i].FindControl("LB_STLDOC");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");
                lbSTL.Attributes.Add("onclick", "window.open('" +DGR.Items[i].Cells[11].Text+ "','SETTLEMENT DOC','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");

                lbCLAIMNO.Text = DGR.Items[i].Cells[2].Text;
                lbSTL.Text = DGR.Items[i].Cells[10].Text;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
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
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UNAPPRV '" + e.Item.Cells[2].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch(System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                    return;
                }

                try
                {                    
                    FillDGR();
                }
                catch 
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }

            if (e.CommandName == "Detail")
            {
                Response.Redirect("ClaimHeader.aspx?CLAIM_NO=" + e.Item.Cells[2].Text);
            }
        }


    }
}