using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationReportHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadReportList();
            }
        }

        protected void LoadReportList()
        {
            //LB_LIST.Text = "";
            //conn.QueryString = "exec SP_REPORT_LIST '" + LB_REGNO.Text + "'";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    LB_LIST.Text = LB_LIST.Text + conn.GetFieldValue(i, 0).ToString();
            //}


            conn.QueryString = "exec SP_QUOTATION_REPORT_LIST '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DGR_BUTTON.DataSource = conn.GetDataTable().Copy();
            DGR_BUTTON.DataBind();

            for (int i = 0; i < DGR_BUTTON.Items.Count; i++)
            {
                Button bt = (Button)DGR_BUTTON.Items[i].FindControl("BT_PRINT");
                bt.Text = DGR_BUTTON.Items[i].Cells[1].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_BUTTON_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                //string URL = "";
                //URL = e.Item.Cells[0].Text;
                //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ReportBody.location.href = '" + URL + "';</script>");

                
                conn.QueryString = "exec SP_APPLICATION_PRINT_VALIDATION_LOG '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ReportBody.location.href = 'QuotationValidationLog.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
                }
                else
                {
                    string URL = "";
                    URL = e.Item.Cells[0].Text;
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ReportBody.location.href = '" + URL + "';</script>");
                }
                
            }
        }
    }
}