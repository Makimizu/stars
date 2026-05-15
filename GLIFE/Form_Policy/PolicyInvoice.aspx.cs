using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE.Form_Policy
{
    public partial class PolicyInvoice : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillDGRInvoice();
                LoadReserve();
            }
        }

        protected void LoadReserve()
        {
            conn.QueryString = "exec SP_POLICY_INVOICE '" + LB_ID.Text + "',0," + DDL_MODE.SelectedValue;
            conn.ExecuteQuery();

            LB_INVOICE.Text = conn.GetFieldValue("CNT").ToString();
            LB_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();
            LB_CREDIT.Text = conn.GetFieldValue("NK_AMOUNT").ToString();
            LB_DEBET.Text = conn.GetFieldValue("ND_AMOUNT").ToString();
            LB_PAYMENT.Text = conn.GetFieldValue("PAYMENT_AMOUNT").ToString();
            LB_OUSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
        }

        protected void FillDGRInvoice()
        {
            conn.QueryString = "exec SP_POLICY_INVOICE '" + LB_ID.Text + "',1," + DDL_MODE.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btINVOICE = (Button)DGR.Items[i].FindControl("BT_INVOICE");
                Button btRECEIPT = (Button)DGR.Items[i].FindControl("BT_RECEIPT");

                btINVOICE.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[DGR.Columns.Count - 2].Text.Replace("&nbsp;", "").Trim() + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[DGR.Columns.Count - 1].Text.Replace("&nbsp;", "").Trim() != "")
                {
                    btRECEIPT.Visible = true;
                    btRECEIPT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[DGR.Columns.Count - 1].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {

            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGRInvoice();
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGRInvoice();
            LoadReserve();
        }
    }
}