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
    public partial class Invoice_ListHO : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string APPID = "HO";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDDLInvoice();
            }
        }

        protected void FillDDLInvoice()
        {
            conn.QueryString = "select INVOICE_TYPE,DESCR from PARAM_INVOICE_TYPE where APP_ID = '" + APPID + "'";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLInvoice();
        }

        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.INVOICE_TYPE='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_ARDATE.Text.Trim() != "")
                where = where + " and convert(date,a.AR_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_ARDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_ARDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.AR_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_ARDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_AGE1.Text.Trim() != "")
                where = where + " and a.AGING >= '" + TXT_AGE1.Text.Trim() + "' ";

            if (TXT_AGE2.Text.Trim() != "")
                where = where + " and a.AGING <= '" + TXT_AGE2.Text.Trim() + "' ";


            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AR_DATE = convert(varchar(20),AR_DATE,106), " +
                                "AGING, " +
                                "INVOICE_TYPE_DESCR, " +
                                "BILLED = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "NOTA_AMOUNT = replace(convert(varchar(100),convert(money,NOTA_AMOUNT),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,PAYMENT_AMOUNT),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "REPORT_URL = replace(REPORT_URL,INVOICENO,'NOLOGO' + INVOICENO), " +
                                "REPORT_URL_ENG = replace(REPORT_URL_ENG,INVOICENO,'NOLOGO' + INVOICENO), " +
                                "RECEIPT_URL, " +
                                "ENABLE_PAID, " +
                                "ENABLE_WO, " +
                                "COLOR = (case	when OUTSTANDING > 0 and (AGING between 15 and 21) then 'Yellow' when OUTSTANDING > 0 and AGING > 21 then 'Pink' else '' end) " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "a.STAT='1' " +
                                "and a.APP_ID = '" + APPID + "' " +
                                "and a.OUTSTANDING " + DDL_OUTS.SelectedValue + " " + where +
                                "order by " +
                                DDL_ORDERBY.SelectedValue + " " + DDL_ORDERSHORT.SelectedValue;
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
                BT_XLS.Visible = true;

            string result = "<TABLE style='border-spacing:0px;'><TR><TD>Total</TD><TD>:</TD><TD style='width:120px; text-align:right;'>" + conn.GetRowCount().ToString() + " Records</TD>";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "BILLED = replace(convert(varchar(100),convert(money,SUM(AMOUNT)),1),'.00',''), " +
                                "NOTA_AMOUNT = replace(convert(varchar(100),convert(money,SUM(NOTA_AMOUNT)),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,SUM(PAYMENT_AMOUNT)),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,SUM(OUTSTANDING)),1),'.00','') " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "a.STAT='1' " +
                                "and a.APP_ID = '" + APPID + "' " +
                                "and a.OUTSTANDING " + DDL_OUTS.SelectedValue + " " +
                                where;
            conn.ExecuteQuery();

            result = result +
                        "<TR><TD>Billed</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 0).ToString() + "</TD></TR>" +
                        "<TR><TD>C/D Note</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 1).ToString() + "</TD></TR>" +
                        "<TR><TD>Payment</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 2).ToString() + "</TD></TR>" +
                        "<TR><TD>Outstanding</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 3).ToString() + "</TD></TR>" +
                        "</TABLE>";
            LB_RESULT.Text = result;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbINVOICENO = (LinkButton)DGR.Items[i].FindControl("LB_INVOICENO");
                Button btRECEIPT = (Button)DGR.Items[i].FindControl("BT_RECEIPT");
                Button btSTL = (Button)DGR.Items[i].FindControl("BT_STL");
                Button btWO = (Button)DGR.Items[i].FindControl("BT_WO");
                Button btHST = (Button)DGR.Items[i].FindControl("BT_HST");

                lbINVOICENO.Text = DGR.Items[i].Cells[1].Text;
                lbINVOICENO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[12].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btHST.Attributes.Add("onclick", "window.open('Invoice_History.aspx?INVOICENO=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[14].Text.Trim().Replace("&nbsp;", "") != "")
                {
                    btRECEIPT.Visible = true;
                    btRECEIPT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[14].Text.Replace("&nbsp;", "") + "','RECEIPT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }

                if (DGR.Items[i].Cells[15].Text.Trim().Replace("&nbsp;", "") == "1")
                    btSTL.Visible = true;
                if (DGR.Items[i].Cells[16].Text.Trim().Replace("&nbsp;", "") == "1")
                {
                    btWO.Visible = true;
                    btWO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk WRITE OFF ?')){return false;};");
                }

                if (DGR.Items[i].Cells[18].Text.Replace("&nbsp;", "") != "")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.FromName(DGR.Items[i].Cells[18].Text);
                }
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Note")
            {
                Response.Redirect("Note_Manual.aspx?NOTENO=&INVOICENO=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Settle")
            {
                Response.Redirect("Invoice_Settle.aspx?MODE=INV&CODE=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "WriteOff")
            {
                try
                {
                    conn.QueryString = "exec SP_INVOICE_WRITEOFF " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }

            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.INVOICE_TYPE='" + DDL_TIPE.SelectedValue + "' ";

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
                                "DUEDATE = convert(varchar(20),DUEDATE,106), " +
                                "AGING, " +
                                "INVOICE_TYPE_DESCR, " +
                                "BILLED = AMOUNT, " +
                                "NOTA_AMOUNT = NOTA_AMOUNT, " +
                                "PAYMENT_AMOUNT = PAYMENT_AMOUNT, " +
                                "OUTSTANDING = OUTSTANDING " +
                                "from V_INVOICE_MASTER a " +
                                "where " +
                                "a.STAT='1' " +
                                "and a.APP_ID = '" + APPID + "' " +
                                "and a.OUTSTANDING " + DDL_OUTS.SelectedValue + " " +
                                where + " order by a.INVOICE_DATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "INVOICE", true);
        }
    }
}