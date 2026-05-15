using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQR.Mobile
{
    public partial class MobileQuotation : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LQ"));
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
                    Response.Redirect("../SessionExpired.aspx");
                }

                LB_REGNO.Text = Session["s"].ToString();
                conn.QueryString = "exec SP_REPORT_LIST '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                IF.Src = conn.GetFieldValue("URL").ToString();
            }
        }
    }
}