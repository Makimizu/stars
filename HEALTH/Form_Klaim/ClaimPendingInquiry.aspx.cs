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
    public partial class ClaimPendingInquiry : System.Web.UI.Page
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
                where = where + " and a.DOCNO like '%" + TXT_CLAIMNO.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (DDL_PR.SelectedValue != "ALL")
                where = where + " and a.PR like '" + DDL_PR.SelectedValue + "' ";

            if (DDL_STS.SelectedValue != "ALL")
                where = where + " and a.STS_CLAIM like '%" + DDL_STS.SelectedValue + "%' ";

            if (DDL_STR.SelectedValue == "NEW")
                where = where + " and (a.REMIND1 is null and a.REMIND2 is null) ";
            else if (DDL_STR.SelectedValue == "R1")
                where = where + " and (a.REMIND1 is not null and a.REMIND2 is null) ";
            else if (DDL_STR.SelectedValue == "R2")
                where = where + " and (a.REMIND1 is not null and a.REMIND2 is not null) ";

            if (TXT_APRDATE1.Text.Trim() != "")
                where = where + "and convert(date,a.LAST_PENDINGDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_APRDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.LAST_PENDINGDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_APRDATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "a.DOCNO, " +
                                "a.NAMA, " +
                                "a.POLICY_NO, " +
                                "a.COMPANY, " +
                                "a.PR_DESCR, " +
                                "TGL_PENDING= convert(varchar(20),a.DOCDATE,106), " +
                                "TGL_PENDING_AWAL= convert(varchar(20),a.FIRST_PENDINGDATE,106), " +
                                "a.FIRST_PENDINGBY, " +
                                "TGL_PENDING_AKHIR= convert(varchar(20),a.LAST_PENDINGDATE,106), " +
                                "a.LAST_PENDINGBY, " +
                                "TGL_REMIND1= convert(varchar(20),a.REMIND1,106), " +
                                "TGL_REMIND2= convert(varchar(20),a.REMIND2,106), " +
                                "TGL_REJECT= convert(varchar(20),a.REJECTDATE,106), " +
                                "a.AGING, " +
                                "a.AMOUNT, " +
                                "a.STS_CLAIM " +
                                "from V_CLAIM_PENDING_REMINDER a " +
                                "where 1 = 1 " + where +
                                "order by a.DOCDATE desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCLAIMNO = (LinkButton)DGR.Items[i].FindControl("LB_CLAIMNO");
                //Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                //btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");
                lbCLAIMNO.Text = DGR.Items[i].Cells[2].Text;
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
            //if (e.CommandName == "Delete")
            //{
            //    try
            //    {
            //        conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UNAPPRV '" + e.Item.Cells[2].Text + "'";
            //        conn.ExecuteNonQuery();
            //    }
            //    catch(System.Exception ex)
            //    {
            //        LB_ERR.Text = ex.Message;
            //        return;
            //    }

            //    try
            //    {                    
            //        FillDGR();
            //    }
            //    catch 
            //    {
            //        DGR.CurrentPageIndex = 0;
            //        FillDGR();
            //    }
            //}

            if (e.CommandName == "Detail")
            {
                Response.Redirect("ClaimHeader.aspx?CLAIM_NO=" + e.Item.Cells[2].Text);
            }
        }


    }
}