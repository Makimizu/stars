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
    public partial class Report_Setup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillReports();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from SECURITY.dbo.M_ROLES";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ROLES.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillReports()
        {
            conn.QueryString = "select CODE, DESCR " +
                                "from V_LINK_SC_REPORT_LIST " +
                                "where CODE not in " +
                                "(select CODE from REPORT_ROLES where ROLES=" + DDL_ROLES.SelectedValue + ") and isnull(URL,'')<>'' order by 2";
            conn.ExecuteQuery();
            LB1.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB1.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select b.CODE, DESCR " +
                                "from REPORT_ROLES a " +
                                "inner join V_LINK_SC_REPORT_LIST b on a.CODE=b.CODE " +
                                "where a.ROLES=" + DDL_ROLES.SelectedValue + "  order by 2";
            conn.ExecuteQuery();
            LB2.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB2.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_ROLES_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReports();
        }

        protected void BT_INSERT_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into REPORT_ROLES select " + LB1.SelectedValue + "," + DDL_ROLES.SelectedValue;
                conn.ExecuteNonQuery();
                FillReports();
            }
            catch { }
        }

        protected void BT_DELETE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "delete from REPORT_ROLES where CODE=" + LB2.SelectedValue + " and ROLES=" + DDL_ROLES.SelectedValue;
                conn.ExecuteNonQuery();
                FillReports();
            }
            catch { }
        }
    }
}