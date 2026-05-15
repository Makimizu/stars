using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_MASTER_REMUN_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "exec SP_PARAM_REMUN_MENU '" + Request.QueryString["mode"].ToString() + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    LB_CARDS.Text = LB_CARDS.Text + conn.GetFieldValue(i, 0).ToString();
            }
        }
    }
}