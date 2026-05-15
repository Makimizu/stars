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
    public partial class MemoType : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "exec SP_LINK_FINANCE_SETTLEMENT";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    LB_CARDS.Text = LB_CARDS.Text + conn.GetFieldValue(i, 0).ToString();
            }
        }
    }
}