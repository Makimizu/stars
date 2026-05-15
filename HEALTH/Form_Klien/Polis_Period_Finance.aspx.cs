using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Finance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGRInvoice();
                LoadReserve();
            }
        }

        protected void LoadReserve()
        {
            conn.QueryString = "exec SP_POLICY_PERIOD_RESERVE '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            LB_BALANCE.Text = conn.GetFieldValue("BALANCE").ToString();
            LB_CLAIM.Text = conn.GetFieldValue("CLAIM").ToString();
            LB_LOADINGAMT.Text = conn.GetFieldValue("LOADING_AMT").ToString();
            LB_LOADINGPCT.Text = conn.GetFieldValue("LOADING").ToString();
            LB_REFUNDPRM.Text = conn.GetFieldValue("REFUND_PREMIUM").ToString();
            LB_RSV_EARNED.Text = conn.GetFieldValue("RESERVE_EARNED").ToString();
            LB_TABARRUAMT.Text = conn.GetFieldValue("TABBARU_AMT").ToString();
            LB_TABARRUPCT.Text = conn.GetFieldValue("TABARRU").ToString();
        }

        protected void FillDGRInvoice()
        {
            conn.QueryString = "select " +
                                "a.INVOICENO, " +
                                "INVOICE_DATE = convert(varchar(20),a.INVOICE_DATE,106), " +
                                "a.AGING, " +
                                "a.INVOICE_TYPE_DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "NK_AMOUNT = replace(convert(varchar(100),convert(money,a.NK_AMOUNT),1),'.00',''), " +
                                "ND_AMOUNT = replace(convert(varchar(100),convert(money,a.ND_AMOUNT),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,a.PAYMENT_AMOUNT),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,a.OUTSTANDING),1),'.00',''), " +
                                "a.URL_INVOICE, " +
                                "a.URL_RECEIPT " +
                                "from V_LINK_FINANCE_INVOICE_MASTER a " +                                
                                "inner join V_POLICY_PERIOD_INVOICE b on a.INVOICENO = isnull(b.INVOICENO,'') " +
                                "where " +
                                "a.APP_ID = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                "and b.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                "order by " +
                                "a.INVOICE_DATE";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btINVOICE = (Button)DGR.Items[i].FindControl("BT_INVOICE");
                Button btRECEIPT = (Button)DGR.Items[i].FindControl("BT_RECEIPT");

                btINVOICE.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "").Trim() + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "").Trim() != "")
                {
                    btRECEIPT.Visible = true;
                    btRECEIPT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {

            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,SUM(a.AMOUNT)),1),'.00',''), " +
                                "NK_AMOUNT = replace(convert(varchar(100),convert(money,SUM(a.NK_AMOUNT)),1),'.00',''), " +
                                "ND_AMOUNT = replace(convert(varchar(100),convert(money,SUM(a.ND_AMOUNT)),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,SUM(a.PAYMENT_AMOUNT)),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,SUM(a.OUTSTANDING)),1),'.00','') " +
                                "from V_LINK_FINANCE_INVOICE_MASTER a " +
                                "inner join V_POLICY_PERIOD_INVOICE b on a.INVOICENO = isnull(b.INVOICENO,'') " +
                                "where " +
                                "a.APP_ID = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' " +
                                "and b.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[5].Text = conn.GetFieldValue("AMOUNT").ToString();
                e.Item.Cells[6].Text = conn.GetFieldValue("NK_AMOUNT").ToString();
                e.Item.Cells[7].Text = conn.GetFieldValue("ND_AMOUNT").ToString();
                e.Item.Cells[8].Text = conn.GetFieldValue("PAYMENT_AMOUNT").ToString();
                e.Item.Cells[9].Text = conn.GetFieldValue("OUTSTANDING").ToString();
            }
        }
    }
}