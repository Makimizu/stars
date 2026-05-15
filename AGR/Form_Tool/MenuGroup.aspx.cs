using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Tool
{
    public partial class MenuGroup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "exec SP_MENU_GROUP " + Request.QueryString["CODE"].ToString();
                conn.ExecuteQuery();
                LB_MENU.Text = conn.GetFieldValue(0, 0).ToString();
            }
        }
    }
}