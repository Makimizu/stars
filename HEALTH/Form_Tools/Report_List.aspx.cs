using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
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
                Setup();
            }
        }

        protected void Setup()
        {




            DDL_REPORT.Items.Add("");
            conn.QueryString = "select b.CODE, DESCR " +
                                "from REPORT_ROLES a " +
                                "inner join V_LINK_SC_REPORT_LIST b on a.CODE=b.CODE " +
                                "where a.ROLES=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "  order by 2";
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

            Response.Write("<script>parent.reportcontent.location.href='" + conn.GetFieldValue(0, 0).ToString() + "';</script>");

            //LB_GO.Text = "<a href='" + conn.GetFieldValue(0, 0).ToString() + "'  target='reportcontent'><B>TAMPILKAN</B></a>";
        }
    }
}