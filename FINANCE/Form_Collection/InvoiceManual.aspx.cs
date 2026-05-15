using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class InvoiceManual : System.Web.UI.Page
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

                Setup();
                FillDGR();
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

            
        }

        protected void FillDGR()
        {
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_AGE1.Text.Trim() != "")
                where = where + " and a.AGING >= '" + TXT_AGE1.Text.Trim() + "' ";

            if (TXT_AGE2.Text.Trim() != "")
                where = where + " and a.AGING <= '" + TXT_AGE2.Text.Trim() + "' ";


            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AGING, " +
                                "INVOICE_TYPE_DESCR, " +
                                "BILLED = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "NOTA_AMOUNT = replace(convert(varchar(100),convert(money,NOTA_AMOUNT),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,PAYMENT_AMOUNT),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "REPORT_URL, " +
                                "REPORT_URL_ENG, " +
                                "RECEIPT_URL, " +
                                "ENABLE_PAID, " +
                                "ENABLE_WO " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "a.STAT='0' and a.INVOICE_TYPE='999' " +
                                "and a.APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                where + " order by a.INVOICE_DATE desc";
            conn.ExecuteQuery();

            string result = "<TABLE style='border-spacing:0px;'><TR><TD>Total</TD><TD>:</TD><TD style='width:120px; text-align:right;'>" + conn.GetRowCount().ToString() + " Records</TD>";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,SUM(OUTSTANDING)),1),'.00','') " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "a.STAT='1' and a.INVOICE_TYPE='999' " +
                                "and a.APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                where;
            conn.ExecuteQuery();

            result = result + "<TR><TD>Outstanding</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 0).ToString() + "</TD></TABLE>";
            LB_RESULT.Text = result;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbINVOICENO = (LinkButton)DGR.Items[i].FindControl("LB_INVOICENO");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btAPPR = (Button)DGR.Items[i].FindControl("BT_APPROVE");

                lbINVOICENO.Text = DGR.Items[i].Cells[1].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
                btAPPR.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk APPROVE ?')){return false;};");

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

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("InvoiceManualEntri.aspx");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {
                Response.Redirect("InvoiceManualEntri.aspx?INVOICENO=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_INVOICE_MANUAL_DELETE " +
                                        "'" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteQuery();
                    FillDGR();
                }
                catch 
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }

            if (e.CommandName == "Approve")
            {
                try
                {
                    conn.QueryString = "exec SP_INVOICE_MANUAL_APPROVAL " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }
        }
    }
}