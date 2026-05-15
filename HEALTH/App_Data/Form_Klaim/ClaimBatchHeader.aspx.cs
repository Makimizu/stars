using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimBatchHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "select URL, DESCR from V_LINK_SC_REPORT_LIST where CODE in ('297','298')";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_MODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                ShowBody();
            }
        }

        protected void ShowBody()
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbatchbody.location.href = '" + DDL_MODE.SelectedValue + "';</script>");
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowBody();
        }
    }
}