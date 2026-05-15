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
    public partial class Customer_Block_Service_Detail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APP.Text = Request.QueryString["APPID"];
                LB_NO.Text = Request.QueryString["NO"];
                ShowInfo();
                FillDGR();
            }
        }

        protected void ShowInfo()
        {
            try
            {
                conn.QueryString = "select " +
                                    "b.APP_NAME, " +
                                    "a.CUSTOMER_NAME, " +
                                    "OUTSTANDING = replace(convert(varchar(100),convert(money,a.OUTSTANDING),1),'.00','') " +
                                    "from V_INVOICE_CUSTOMER_OVERDUE a " +
                                    "inner join V_LINK_SC_M_APPS b on a.APP_ID=b.CODE " +
                                    "where " +
                                    "a.APP_ID='" + LB_APP.Text + "' " +
                                    "and a.CUSTOMER_CODE='" + LB_NO.Text + "'";
                conn.ExecuteQuery();

                LB_APPDESCR.Text = conn.GetFieldValue("APP_NAME").ToString();
                LB_COMPANY.Text = conn.GetFieldValue("CUSTOMER_NAME").ToString();
                LB_OUTSTANDING.Text = conn.GetFieldValue("OUTSTANDING").ToString();
            }
            catch { }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "INVOICE_TYPE_DESCR, " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,PAYMENT_AMOUNT),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AGING " +
                                "from V_INVOICE_CUSTOMER_OVERDUE_DETAIL a " +
                                "where " +
                                "a.APP_ID='" + LB_APP.Text + "' " +
                                "and a.CUSTOMER_CODE='" +LB_NO.Text+ "' " +
                                "order by " +
                                "a.INVOICE_DATE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }
    }
}