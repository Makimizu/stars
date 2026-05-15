using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationFrame : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string QUOTNO = Request.QueryString["QUOTNO"];
            conn.QueryString = "select PRODUCT_GROUP from QUOTATION where QUOTNO = '" + QUOTNO + "'";
            conn.ExecuteQuery();

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationHeader.location.href = 'Quotation" + conn.GetFieldValue("PRODUCT_GROUP").ToString() + ".aspx?QUOTNO=" + QUOTNO + "';</script>");
            */
        }
    }
}