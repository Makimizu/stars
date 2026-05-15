using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace SALESMARKET.Form_Comm
{
    public partial class EarnedComm : System.Web.UI.Page
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
            conn.QueryString = "select distinct " +
                                "a.CODE, a.APP_NAME " +
                                "APP_NAME from SECURITY.dbo.M_APPS a " +
                                "inner join FINANCE.dbo.V_INVOICE_MASTER_COMISSION_DATA b on a.CODE=b.APP_ID collate database_default " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            LB_AMOUNT.Text = "";
            string where = "";

            if (DDL_APP.SelectedValue != "")
                where = where + " and a.APP_ID='" + DDL_APP.SelectedValue + "' ";

            if (TXT_AGENT.Text.Trim() != "")
                where = where + " and a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' ";

            if (TXT_PRCDATE.Text.Trim() != "")
                where = where + " and convert(date,a.SETTLEDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_PRCDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_PRCDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.SETTLEDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_PRCDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CUSTOMER.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_CUSTOMER.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and a.PRODUCT_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "ROWID, " +
                                "SETTLEDATE = convert(varchar(20),a.SETTLEDATE,106), " +
                                "DC, " +
                                "AGENT_NAME, " +
                                "SUB_CODE_DESCR, " +
                                "COMISSIONAMT = replace(convert(varchar(100),convert(money,a.COMISSIONAMT),1),'.00',''), " +
                                "PRODUCT_DESCR, " +
                                "INVOICENO, " +
                                "INVOICE_TYPE_DESCR, " +
                                "POLICY_NO, " +
                                "CUSTOMER_NAME, " +
                                "BANKPOSTDATE = convert(varchar(20),a.BANKPOSTDATE,106), " +
                                "PAIDAMOUNT = replace(convert(varchar(100),convert(money,a.PAIDAMOUNT),1),'.00',''), " +
                                "COMMISIONVAL = a.COMMISIONVAL * 100, " +
                                "DISCOUNTVAL = a.DISCOUNTVAL * 100 " +
                                "from FINANCE.dbo.V_INVOICE_MASTER_COMISSION_DATA a " +
                                "where  " +
                                "1=1 " + where +
                                "order by a.SETTLEDATE";
            conn.ExecuteQuery();

            LB_RECORDS.Text = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[2].Text == "C")
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
            }

            conn.QueryString = "select " +
                                "COMISSIONAMT = replace(convert(varchar(100),convert(money,SUM(a.COMISSIONAMT)),1),'.00','') " +
                                "from FINANCE.dbo.V_INVOICE_MASTER_COMISSION_DATA a " +
                                "where  " +
                                "1=1 " + where;
            conn.ExecuteQuery();
            LB_AMOUNT.Text = conn.GetFieldValue("COMISSIONAMT").ToString();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}