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
    public partial class QuotationNew : System.Web.UI.Page
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
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_UW_PR_PRODUCT_GROUP where CODE in ('GTL') order by 2 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            FillDGRCompany();
        }

        protected void FillDGRCompany()
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "COMPANY_NAME = UPPER(a.COMPANY_NAME), " +
                                "a.POLICY_NO, " +
                                "TC_DESCR = replace(a.DESCR, a.TC_ID + '-', '') " +
                                "from	V_LINK_GLIFE_POLICY a " +
                                "where " +
                                "LTRIM(RTRIM(a.COMPANY_NAME)) <> '' " +
                                "and a.PRODUCT_GROUP = '" + DDL_PRODUCT.SelectedValue + "' " +
                                "and a.COMPANY_NAME like '%" + TXT_COMPANYSEARCH.Text.Trim() + "%' " +
                                "and a.DESCR like '%" + TXT_PRODUCTSEARCH.Text.Trim() + "%' " +
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

        protected void TXT_COMPANYSEARCH_TextChanged(object sender, EventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = 0;
            FillDGRCompany();
        }

        protected void DGR_POLICY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = e.NewPageIndex;
            FillDGRCompany();
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_QUOTATION_UPSERT " +
                                    "null," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + DDL_PRODUCT.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();


                string URL = Crypto.EncryptStringAES("QuotationFrame.aspx?quotno=" + conn.GetFieldValue("QUOTNO").ToString());
                while (URL.IndexOf("/") >= 0 || URL.IndexOf("\\") >= 0 || URL.IndexOf("+") >= 0)
                {
                    URL = Crypto.EncryptStringAES("QuotationFrame.aspx?quotno=" + conn.GetFieldValue("QUOTNO").ToString());
                }

                Response.Redirect("Frame.aspx?URL=" + URL);
            }
        }

        protected void DDL_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = 0;
            FillDGRCompany();
        }

        protected void TXT_PRODUCTSEARCH_TextChanged(object sender, EventArgs e)
        {
            DGR_COMPANY.CurrentPageIndex = 0;
            FillDGRCompany();
        }
    }
}