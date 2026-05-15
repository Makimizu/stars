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
    public partial class Invoice_Corporate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_INVOICE_MASTER_SUMMARY 'HO'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_HO.DataSource = dt;
            DGR_HO.DataBind();

            conn.QueryString = "exec SP_INVOICE_MASTER_SUMMARY 'GL'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_GL.DataSource = dt;
            DGR_GL.DataBind();
        }

        protected void BT_HO_Click(object sender, EventArgs e)
        {
            Response.Redirect("Invoice_ListHO.aspx");
        }

        protected void BT_GL_Click(object sender, EventArgs e)
        {
            Response.Redirect("Invoice_ListGL.aspx");
        }
    }
}