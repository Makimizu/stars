using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR
{
    public partial class REPORTHEADER : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "exec SP_REPORT_LIST";
                conn.ExecuteQuery();
                //LB_CARDS.Text = "<table style=\"border-spacing:0px;width:95%;\">";
                for (int i = 0; i < conn.GetRowCount(); i++)
                    LB_CARDS.Text = LB_CARDS.Text + conn.GetFieldValue(i, 0).ToString();
                //LB_CARDS.Text = LB_CARDS.Text + "</table>";
            }
        }
    }
}