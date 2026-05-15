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
    public partial class Invoice_Email_Paid_Detail : System.Web.UI.Page
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

                FillDGR(Request.QueryString["CUSTOMERCODE"], Request.QueryString["STLDATE"]);
                //FillDGR(Request.QueryString["INVOICE_DATE"], Request.QueryString["CUSTOMER_NAME"]);
                //FillDGR(Request.QueryString["INVOICE_DATE"]);
                //FillDGR(Request.QueryString["CUSTOMER_NAME"]);
            }
        }

        protected void FillDGR(string customercode, string stldate)
        {
            conn.QueryString = "exec SP_INVOICE_EMAIL_PAID_HISTORY '" + customercode + "','" + stldate + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        //protected void FillDGR(string invdate, string cusname)
        //{
        //    conn.QueryString = "exec SP_INVOICE_EMAIL_PAID_DETAIL '" + invdate + "''" + cusname + "'";
        //    conn.ExecuteQuery();

        //    DataTable dt;
        //    dt = new DataTable();
        //    dt = conn.GetDataTable().Copy();
        //    DGR.DataSource = dt;
        //    DGR.DataBind();
        //}
    }
}