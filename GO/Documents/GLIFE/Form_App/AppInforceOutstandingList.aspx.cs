using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
namespace GLIFE.Form_App
{
    public partial class AppInforceOutstandingList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillDGR();

            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_INVOICE.Text.Trim() != "")
                where = where + " and INVOICENO like '%" + TXT_INVOICE.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_AGING1.Text.Trim() != "")
                where = where + " and AGING >= " + TXT_AGING1.Text.Trim() + " ";

            if (TXT_AGING2.Text.Trim() != "")
                where = where + " and AGING <= " + TXT_AGING2.Text.Trim() + " ";


            conn.QueryString = "select " +
                                "REGNO,  " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "POLICY_NO,  " +
                                "COMPANY_NAME = '<span style=\"color:black;\">' + COMPANY_NAME + '</span>' + '<BR><i>' + TC_DESCR + '</i>',  " +
                                "BRANCH_CODE,  " +
                                "OUTSTANDING = replace(convert(varchar(100), convert(money,OUTSTANDING),1), '.00',''),  " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "START_DATE = convert(varchar(100), START_DATE, 106),  " +
                                "END_DATE = convert(varchar(100), END_DATE, 106), " +
                                "INVOICENO, " +
                                "AGING " +
                                "from V_LINK_FINANCE_APPLICATION_INVOICE_OUTSTANDING a " +
                                "where " +
                                "1=1 " + where + " " +
                                " order by a.AGING desc";
            conn.ExecuteQuery(10000);

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
                lbCODE.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Rollback")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_APPLICATION_MASTER_CANCEL " +
                                                "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteQuery();
                        }
                        catch { }
                    }
                }

                FillDGR();
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

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Button btROLLBACK = (Button)e.Item.FindControl("BT_ROLLBACK");
                btROLLBACK.Attributes.Add("onclick", "if(!confirm('Are you sure to CANCEL ?')){return false;};");
            }
        }
    }
}