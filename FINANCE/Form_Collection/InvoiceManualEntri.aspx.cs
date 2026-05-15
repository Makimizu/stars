using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class InvoiceManualEntri : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                TXT_NO.Text = Request.QueryString["INVOICENO"];
                Setup();
                LoadRecord();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select b.CODE, b.APP_NAME from PARAM_INVOICE_TYPE a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default " +
                                "where a.INVOICE_TYPE = '999' order by 2";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLCustomer();
        }

        protected void FillDDLCustomer()
        {
            DDL_CUSTOMER.Items.Clear();
            conn.QueryString = "select " +
                                "a.POLICY_NO, b.COMPANY_NAME  " +
                                "from V_LINK_CB_POLICY_GROUP_MASTER a " +
                                "inner join V_LINK_CB_COMPANY b on a.COMPANY_CODE=b.COMPANY_CODE " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CUSTOMER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLBranch();
        }

        protected void FillDDLBranch()
        {
            DDL_BRANCH.Items.Clear();
            conn.QueryString = "select b.BRANCH_CODE, b.NAMA_CABANG " +
                                "from V_LINK_CB_POLICY_GROUP_MASTER a " +
                                "inner join V_LINK_CB_BRANCH b on a.COMPANY_CODE=b.COMPANY_CODE " +
                                "where a.POLICY_NO = '" + DDL_CUSTOMER.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLCustomer();
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "APP_ID, " +
                                "CUSTOMER_CODE, " +
                                "BRANCH_CODE " +
                                "from INVOICE_MASTER " +
                                "where " +
                                "INVOICENO = '" + TXT_NO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_DATE.Text = conn.GetFieldValue("INVOICE_DATE").ToString();
                DDL_APP.SelectedValue = conn.GetFieldValue("APP_ID").ToString();
                DDL_CUSTOMER.SelectedValue = conn.GetFieldValue("CUSTOMER_CODE").ToString();
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
            }

            FillGrid();
        }

        protected void FillGrid()
        {
            conn.QueryString = "exec SP_INVOICE_MANUAL_DETAIL '" + TXT_NO.Text + "'";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtDescr = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                TextBox txtAmount = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");

                txtDescr.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtAmount.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }


        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00','') from V_INVOICE_MASTER where INVOICENO = '" + TXT_NO.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[4].Text = conn.GetFieldValue("OUTSTANDING").ToString();
            }
        }

        protected void DDL_CUSTOMER_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLBranch();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            try
            {
                conn.QueryString = "exec SP_INVOICE_MANUAL_UPSERT " +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + TXT_NO.Text + "'," +
                                    "'" + DDL_CUSTOMER.SelectedValue + "'," +
                                    "'" + DDL_BRANCH.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                TXT_NO.Text = conn.GetFieldValue("INVOICENO").ToString();

                conn.QueryString = "delete from INVOICE_DETAIL_MANUAL where INVOICENO='" + TXT_NO.Text + "'";
                conn.ExecuteNonQuery();

                int SEQ = 1;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtDescr = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                    TextBox txtAmount = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");

                    if (txtAmount.Text.Trim() != "")
                    {
                        try
                        {
                            conn.QueryString = "insert into INVOICE_DETAIL_MANUAL select " +
                                                "'" + TXT_NO.Text + "'," +
                                                "'" + SEQ.ToString() + "'," +
                                                "'" + txtDescr.Text.Trim().Replace("'", "`") + "'," +
                                                "'" + txtAmount.Text.Trim().Replace(",", "") + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "GETDATE()";
                            conn.ExecuteNonQuery();

                            SEQ++;
                        }
                        catch { }
                    }
                }

                LoadRecord();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
            }
        }
    }
}