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
    public partial class AppMCUInvoiceList : System.Web.UI.Page
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
            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE='MCU' order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "and a.LAST_TRACK = " + DDL_STATUS.SelectedValue + " ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_INVOICENO.Text.Trim() != "")
                where = where + " and INVOICENO like '%" + TXT_INVOICENO.Text.Trim() + "%' ";

            if (TXT_INVOICEDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVOICEDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVOICEDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVOICEDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "a.BATCH_ID, " +
                                "a.INVOICENO, " +
                                "INVOICE_DATE = convert(varchar(20), a.INVOICE_DATE, 106), " +
                                "COMPANY_NAME = UPPER(a.COMPANY_NAME), " +
                                "a.MEMBER, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "ESTIMATED_CHARGE = replace(convert(varchar(100),convert(money,ESTIMATED_CHARGE),1),'.00',''), " +
                                "DIFF = replace(convert(varchar(100),convert(money,isnull(a.AMOUNT,0) - isnull(a.ESTIMATED_CHARGE,0)),1),'.00',''), " +
                                "a.CREATEDATE, " +
                                "a.LAST_TRACK, " +
                                "a.LAST_TRACK_DESCR " +
                                "from V_MEDICAL_LAB_INVOICE a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by " +
                                "a.CREATEDATE";
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
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_INVOICENO");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                
                lbCODE.Text = DGR.Items[i].Cells[2].Text;
                if (DGR.Items[i].Cells[3].Text == "2")
                    btDEL.Visible = false;
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationMCUInvoice.aspx?batchid=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from MEDICAL_LAB_INVOICE where BATCH_ID = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
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

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationMCUInvoice.aspx?batchid=");
        }
    }
}