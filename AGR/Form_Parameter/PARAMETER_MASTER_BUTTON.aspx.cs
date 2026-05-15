using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace AGR
{
    public partial class PARAMETER_MASTER_BUTTON : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                conn.QueryString = "select DESCR from PR_REMUN_TYPE where CODE = '" + LB_MODE.Text + "'";
                conn.ExecuteQuery();
                LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
            }
        }
    }
}