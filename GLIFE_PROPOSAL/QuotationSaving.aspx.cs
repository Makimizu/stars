using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationSaving : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                try
                {
                    LB_QUOTNO.Text = Request.QueryString["quotno"];
                    if (LB_QUOTNO.Text != "")
                    {   
                        LoadQuotation(LB_QUOTNO.Text);
                        Setup();
                    }
                    else
                    {
                        DGR_COMPANY.CurrentPageIndex = 0;
                        FillDGRCompany();
                    }
                }
                catch { }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select URLAPP, DESCR from V_LINK_SC_REPORT_LIST where SHARE = '1'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REPORT.DataSource = dt;
            DGR_REPORT.DataBind();

            for (int i = 0; i < DGR_REPORT.Items.Count; i++)
            {
                Button bt = (Button)DGR_REPORT.Items[i].FindControl("BT_REPORT");
                bt.Text = DGR_REPORT.Items[i].Cells[1].Text.ToUpper();
            }
        }

        protected void LoadQuotation(string quotno)
        {
            DV_COMPANY.Visible = false;
            DV_QUOT.Visible = true;
            TR_VERNO.Visible = true;
            TR_BTSAVE.Visible = false;
            DV_QUOTVER.Visible = true;

            conn.QueryString = "select VERNO from QUOTATION_VERSION_SAVING where QUOTNO = '" + quotno + "' order by 1 desc";
            conn.ExecuteQuery();
            DDL_VERNO.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_VERNO.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (DDL_VERNO.Items.Count == 0)
                return;

            conn.QueryString = "select * from V_QUOTATION where QUOTNO = '" + quotno + "'";
            conn.ExecuteQuery();

            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_COMPANYCODE.Text = conn.GetFieldValue("COMPANY_CODE").ToString();

            LoadVersion(DDL_VERNO.SelectedValue);
        }

        protected void LoadPackage()
        {
            LBL_TITLE.Text = BT_PACKAGE.Text;
            IF.Src = "QuotationSavingParameter.aspx?quotno=" + LB_QUOTNO.Text + "&verno=" + DDL_VERNO.SelectedValue;
        }

        protected void LoadVersion(string verno)
        {
            TR_QUOTNO.Visible = true;

            LoadVersionSummary();
            LoadPackage();
        }

        protected void LoadVersionSummary()
        {
            try
            {
                conn.QueryString = "select " +
                                    "MEMBER, " +
                                    "LAST_BALANCE = replace(convert(varchar(100),convert(money,LAST_BALANCE),1), '.00','') " +
                                    "from V_QUOTATION_VERSION_SAVING " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = " + DDL_VERNO.SelectedValue;
                conn.ExecuteQuery();
                LB_MEMBER.Text = conn.GetFieldValue("MEMBER").ToString();
                LB_BALANCE.Text = conn.GetFieldValue("LAST_BALANCE").ToString();
            }
            catch { }
        }

        protected void DDL_VERNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVersion(DDL_VERNO.SelectedValue);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            /*
            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_UPSERT " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "'" + DDL_COMPANY.SelectedValue + "'," +
                                "'" + DDL_AGENT.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            Response.Redirect("QuotationSaving.aspx?quotno=" + conn.GetFieldValue("QUOTNO").ToString());
            */
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_UPSERT " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "null," +
                                    "'" + DateTime.Now.ToShortDateString() + "'," +
                                    "null," +
                                    "56," +
                                    "0," +
                                    "10000," +
                                    "0," +
                                    "0," +
                                    "'M'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationSaving.aspx?quotno=" + LB_QUOTNO.Text);
            }
            catch { }
        }

        protected void BT_COPY_Click(object sender, EventArgs e)
        {

        }

        protected void BT_DEL_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "delete from QUOTATION_VERSION_SAVING where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + DDL_VERNO.SelectedValue;
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationSaving.aspx?quotno=" + LB_QUOTNO.Text);
            }
            catch { }
        }

        protected void BT_PACKAGE_Click(object sender, EventArgs e)
        {
            LoadVersionSummary();
            LoadPackage();
        }

        protected void BT_MEMBER_Click(object sender, EventArgs e)
        {
            LoadVersionSummary();
            LBL_TITLE.Text = ((Button)sender).Text;
            IF.Src = "QuotationSavingMember.aspx?quotno=" + LB_QUOTNO.Text + "&verno=" + DDL_VERNO.SelectedValue;
        }

        protected void BT_BRANCH_Click(object sender, EventArgs e)
        {
            LoadVersionSummary();
            LBL_TITLE.Text = ((Button)sender).Text;
            IF.Src = "CompanyBranch.aspx?ID=" + LB_COMPANYCODE.Text;
        }

        protected void ARCHIEVE_Click(object sender, EventArgs e)
        {
            LoadVersionSummary();
            LBL_TITLE.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", LB_QUOTNO.Text + "-" + DDL_VERNO.SelectedValue, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            IF.Src = URL;
        }

        protected void ACTUARY_Click(object sender, EventArgs e)
        {
            LoadVersionSummary();
            LBL_TITLE.Text = ((Button)sender).Text;
            //IF.Src = "QuotationBatchClosing.aspx?quotno=" + LB_QUOTNO.Text + "&verno=" + DDL_VERNO.SelectedValue;
        }

        protected void TXT_COMPANYSEARCH_TextChanged(object sender, EventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = 0;
            FillDGRCompany();
        }

        protected void FillDGRCompany()
        {
            string where = "";
            if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99")
                where = " and AGENT = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";

            conn.QueryString = "select " +
                                "a.COMPANY_CODE, " +
                                "a.COMPANY_NAME " +
                                "from V_LINK_CB_COMPANY a " +
                                "where " +
                                "LTRIM(RTRIM(a.COMPANY_NAME)) <> '' " + where +
                                "and a.COMPANY_NAME like '%" + TXT_COMPANYSEARCH.Text.Trim() + "%' " +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_COMPANY.DataSource = dt;
            DGR_COMPANY.DataBind();

            for (int i = 0; i < DGR_COMPANY.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_COMPANY.Items[i].FindControl("LBT_COMPANY");

                lbCODE.Text = DGR_COMPANY.Items[i].Cells[1].Text; ;
            }
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadVersionSummary();

                conn.QueryString = "exec SP_QUOTATION_INSERT " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'SP'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                Response.Redirect("QuotationSaving.aspx?QUOTNO=" + conn.GetFieldValue("QUOTNO").ToString());
            }
        }

        protected void DGR_POLICY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = e.NewPageIndex;
            FillDGRCompany();
        }

        protected void DGR_REPORT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LBL_TITLE.Text = e.Item.Cells[1].Text;
                IF.Src = e.Item.Cells[0].Text + "&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + DDL_VERNO.SelectedValue;
            }
        }
    }
}