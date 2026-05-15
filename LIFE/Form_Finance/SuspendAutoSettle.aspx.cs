using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Finance
{
    public partial class SuspendAutoSettle : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=BT_LOAD]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void BT_LOAD_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            conn.QueryString = "exec SP_LINK_FINANCE_INVOICE_SETTLE_AUTO '" + Request.QueryString["ACCNO"].ToString() + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(500000);
            Response.Redirect("SuspendFrame.aspx");
        }
    }
}