using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimHistory : System.Web.UI.Page
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
            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'CLM' and SEQ in (4,5,6)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_TYPE order by 1";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and PRODUCT_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_CLAIMDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CLAIMDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.CLAIM_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CLAIMDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.CLAIM_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.SEQ, " +
                                "a.FULLNAME, " +
                                "DOB = convert(varchar(20), a.DOB, 106), " +
                                "CLAIM_DATE = convert(varchar(20), a.CLAIM_DATE, 106), " +
                                "a.PRODUCT_NAME, " +
                                "a.POLICY_NO, " +
                                "INCURRED = replace(convert(varchar(100), convert(money, b.BENEFIT_AMOUNT),1), '.00',''), " +
                                "ADDITION = replace(convert(varchar(100), convert(money, b.CREDIT_AMOUNT),1), '.00',''), " +
                                "DEDUCTION = replace(convert(varchar(100), convert(money, b.DEBET_AMOUNT),1), '.00',''), " +
                                "TOTAL = replace(convert(varchar(100), convert(money, b.TOTAL_AMOUNT),1), '.00',''), " +
                                "a.CLAIM_TYPE_DESCR " +
                                "from V_APPLICATION_CLAIM_MASTER a " +
                                "inner join V_APPLICATION_CLAIM_AMOUNT_SUMM b on a.REGNO = b.REGNO and a.SEQ = b.SEQ " +
                                "where " +
                                "a.LAST_TRACK = " + DDL_STAT.SelectedValue + " " + where + " " +
                                "order by a.CLAIM_DATE desc";
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
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                Button btSUBMIT = (Button)DGR.Items[i].FindControl("BT_SUBMIT");

                lbCODE.Text = DGR.Items[i].Cells[3].Text;
                btSUBMIT.Attributes.Add("onclick", "if(!confirm('Are you sure to REVISE ?')){return false;};");
            }

            if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ClaimAppFrame.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Revision")
            {
                Response.Redirect("ClaimApp.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=");
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