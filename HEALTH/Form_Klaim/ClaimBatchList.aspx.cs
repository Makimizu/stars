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
    public partial class ClaimBatchList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_DATE.Attributes.Add("readonly", "readonly");
                TXT_DATE2.Attributes.Add("readonly", "readonly");
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_PROVIDER_TYPE";
            conn.ExecuteQuery();
            DDL_PR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE,DESCR from PR_CLAIM_DOC_SOURCE order by CODE";
            conn.ExecuteQuery();
            DDL_DOC_SOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DOC_SOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            string date1 = "1 jan 1980";
            string date2 = "31 dec 2090";

           

            if (DDL_DOC_SOURCE.SelectedValue != "")
                where = where + " and DOC_SOURCE='" + DDL_DOC_SOURCE.SelectedValue + "' ";

            if (DDL_PR.SelectedValue != "")
                where = where + " and PR='" + DDL_PR.SelectedValue + "' ";

            if (DDL_CNT.SelectedValue != "")
                where = where + " and CLAIM_CNT " + DDL_CNT.SelectedValue + " ";

            if (TXT_BATCH.Text.Trim() != "")
                where = where + " and BATCH_ID like '%" + TXT_BATCH.Text.Trim() + "%' ";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and DOC_NO like '%" + TXT_DOCNO.Text.Trim() + "%' ";

            if (TXT_PROVIDER.Text.Trim() != "")
                where = where + " and PROVIDER like '%" + TXT_PROVIDER.Text.Trim() + "%' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            //-- remark GAS
            //if (TXT_DATE.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            //{
            //    if (TXT_DATE.Text.Trim() != "")
            //        date1 = GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy");
            //    if (TXT_DATE2.Text.Trim() != "")
            //        date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

            //    where = where + " and (a.TGL_DOC between '" + date1 + "' and '" + date2 + "') ";
            //}

            if (TXT_DATE.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                DateTime dates1 = DateTime.Parse(TXT_DATE.Text, null);
                DateTime dates2 = DateTime.Parse(TXT_DATE2.Text, null);

                where = where + " and (a.TGL_DOC between '" + dates1.ToString("MM/dd/yyyy") + "' and '" + dates2.ToString("MM/dd/yyyy") + "') ";

            }

            conn.QueryString = "select TOP 1 ROLE_CODE from SECURITY.dbo.M_USERS where CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' and ROLE_CODE in (select code from PR_USER_ACCESS_POLICYNO)";
            conn.ExecuteQuery(120);
            string ROLECODE = conn.GetFieldValue("ROLE_CODE").ToString();
            if (!String.IsNullOrEmpty(ROLECODE))
            {
                where = where + " and POLICY_NO in (select [DESCR] from [PR_USER_ACCESS_POLICYNO] where CODE = '" + ROLECODE + "') ";
            }

            conn.QueryString = "select " +
                                "BATCH_ID, " +
                                "DOC_NO, " +
                                "DOC_SOURCE_NAME, " +
                                "CLAIM_CNT, " +
                                "TGL_DOC = CONVERT(varchar(20),a.TGL_DOC,106), " +
                                "PR_DESCR, " +
                                "PROVIDER, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "REPORT_URL " +
                                "from V_CLM_CLAIM_MASTER_BATCH a " +
                                "where " +
                                "1=1 " + where +
                                "order by a.TGL_DOC desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbDocNo = (Label)DGR.Items[i].FindControl("LB_DOC_NO");
                lbDocNo.Text = "<a href='#'>" + DGR.Items[i].Cells[10].Text;
                lbDocNo.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[11].Text + "','BATCH','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes')");
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
            if (e.CommandName == "Select")
            {
                Response.Redirect("ClaimReg.aspx?BATCH_ID=" + e.Item.Cells[1].Text);
            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClaimReg.aspx?BATCH_ID=");
        }
    }
}