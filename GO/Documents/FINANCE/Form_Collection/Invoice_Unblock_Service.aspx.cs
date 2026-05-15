using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace FINANCE.Form_Collection
{
    public partial class Invoice_Unblock_Service : System.Web.UI.Page
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
        }

        protected void FillDGR()
        {
            string where = "";

            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "INVOICE_TYPE_DESCR, " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "AGING, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106) " +
                                "from V_INVOICE_BLOCKING_SERVICE a " +
                                "where 1=1 " + where;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
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