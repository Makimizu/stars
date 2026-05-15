using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Tools
{
    public partial class V_LINK_SC_REPORT_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
            }
        }

        protected void Setup()
        {
            DDL_REPORT.Items.Add("");
            conn.QueryString = "select CODE, DESCR " +
                                "from V_LINK_SC_REPORT_LIST a " +
                                "where " +
                                "SHARE = 1 " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REPORT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_REPORT_SelectedIndexChanged(object sender, EventArgs e)
        {
            LB_GO.Text = "";
            if (DDL_REPORT.SelectedValue == "")
                return;

            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='" + DDL_REPORT.SelectedValue + "'";
            conn.ExecuteQuery();

            ClientScript.RegisterStartupScript(GetType(), "", "<script>parent.reportcontent.location.href='" + conn.GetFieldValue(0, 0).ToString() + "';</script>");
        }
    }
}