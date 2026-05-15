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
    public partial class QuotationGTL : System.Web.UI.Page
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
                Setup();
                try
                {
                    LB_QUOTNO.Text = Request.QueryString["quotno"];
                    if (LB_QUOTNO.Text != "")
                    {
                        FillDDLAgent();
                        LoadQuotation(LB_QUOTNO.Text);
                        LoadPackage();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
        }

        protected void Setup()
        {

        }

        protected void FillDDLAgent()
        {
            conn.QueryString = "select " +
                                "c.CODE, " +
                                "FULLNAME = UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,''))) + ' - ' + c.code " +
                                "from QUOTATION a " +
                                "inner join V_LINK_GLIFE_POLICY_CHANNEL_DISTRIBUTION b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_MARKETING_M_AGENTS c on b.SUBCD = c.SUBCD " +
                                "where " +
                                "a.QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Clear();
            DDL_AGENT.Items.Add(new ListItem("", ""));
        }

        protected void LoadQuotation(string quotno)
        {
            conn.QueryString = "select VERNO from QUOTATION_VERSION where QUOTNO = '" + quotno + "' order by 1 desc";
            conn.ExecuteQuery();
            DDL_VERNO.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_VERNO.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (DDL_VERNO.Items.Count == 0)
                return;

            DDL_AGENT.Enabled = false;
            TR_BTSAVE.Visible = false;


            conn.QueryString = "select * from V_QUOTATION where QUOTNO = '" + quotno + "'";
            conn.ExecuteQuery();

            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_POLICYID.Text = conn.GetFieldValue("POLICY_ID").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_COMPANYCODE.Text = conn.GetFieldValue("COMPANY_CODE").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("PRODUCT").ToString();

            try
            {
                DDL_AGENT.SelectedValue = conn.GetFieldValue("AGENT_CODE").ToString();
            }
            catch { }

            LoadVersion(DDL_VERNO.SelectedValue);
        }


        protected void FillDGRReport()
        {
            DGR_REPORT.Visible = true;

            conn.QueryString = "exec SP_QUOTATION_VERSION_REPORT '" + LB_QUOTNO.Text + "'," + DDL_VERNO.SelectedValue;
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

        protected void LoadPackage()
        {
            LBL_TITLE.Text = BT_PACKAGE.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'PolicyTCFrame.aspx?ID=" + LB_POLICYID.Text + "';</script>");
        }

        protected void LoadVersion(string verno)
        {
            conn.QueryString = "select " +
                                "MEMBER, " +
                                "PREMIUM = replace(convert(varchar(100), convert(money, round(PREMIUM, 0)), 1), '.00', ''), " +
                // "PREMIUM_AVG = replace(convert(varchar(100), convert(money, round(PREMIUM_AVG, 0)), 1), '.00', ''), " +
                                "START_DATE = convert(varchar(20), START_DATE, 103), " +
                                "END_DATE = convert(varchar(20), END_DATE, 103) " +
                                "from V_QUOTATION_VERSION " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + DDL_VERNO.SelectedValue;
            conn.ExecuteQuery();

            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();
            LB_MEMBER.Text = conn.GetFieldValue("MEMBER").ToString();
            LB_PREMIUM.Text = conn.GetFieldValue("PREMIUM").ToString();

            TR_QUOTNO.Visible = true;
            FillDGRReport();
            LoadPackage();
        }

        protected void DDL_VERNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVersion(DDL_VERNO.SelectedValue);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_QUOTATION_UPSERT " +
                                "'" + LB_QUOTNO.Text + "'," +
                                "null," +
                                "null," +
                                "'" + DDL_AGENT.SelectedValue + "'";
            conn.ExecuteQuery();

            Response.Redirect("QuotationGTL.aspx?quotno=" + conn.GetFieldValue("QUOTNO").ToString());
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_INSERT '" + LB_QUOTNO.Text + "',null,null,'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationBatch.aspx?quotno=" + LB_QUOTNO.Text);
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
                conn.QueryString = "exec SP_QUOTATION_VERSION_DELETE '" + LB_QUOTNO.Text + "'," + DDL_VERNO.SelectedValue;
                conn.ExecuteNonQuery();
                Response.Redirect("QuotationBatch.aspx?quotno=" + LB_QUOTNO.Text);
            }
            catch { }
        }

        protected void BT_PACKAGE_Click(object sender, EventArgs e)
        {
            LoadPackage();
        }

        protected void BT_MEMBER_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationBatchMember.aspx?ID=" + LB_POLICYID.Text + "&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + DDL_VERNO.SelectedValue + "';</script>");
        }

        protected void BT_BRANCH_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'CompanyBranch.aspx?ID=" + LB_COMPANYCODE.Text + "';</script>");
        }

        protected void ARCHIEVE_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_GTL", LB_QUOTNO.Text + "-" + DDL_VERNO.SelectedValue, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = '" + URL + "';</script>");
        }

        protected void ACTUARY_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationBatchClosing.aspx?quotno=" + LB_QUOTNO.Text + "&verno=" + DDL_VERNO.SelectedValue + "';</script>");
        }

        protected void DGR_REPORT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LBL_TITLE.Text = e.Item.Cells[1].Text;
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = '" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}