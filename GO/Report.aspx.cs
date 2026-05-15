using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GO
{
    public partial class Report : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APP.Text = Request.QueryString["APPID"];
                Setup();
                if(DDL_REPORT.Items.Count > 0)
                    ShowReport();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "b.URL, " +
                                "b.DESCR " +
                                "from REPORT_ROLES a " +
                                "inner join REPORT_LIST b on a.APP_ID=b.APP_ID and a.CODE=b.CODE " +
                                "where " +
                                "a.ROLES = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "' " +
                                "and isnull(b.URL,'') <> '' " +
                                "and a.APP_ID = '" +LB_APP.Text+ "' " +
                                "order by 2";
            conn.ExecuteQuery();

            DDL_REPORT.Items.Add(new ListItem("-- SELECT REPORT --", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REPORT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_REPORT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReport();
        }

        protected void ShowReport()
        {
            string URL = "default.html";
            if (DDL_REPORT.SelectedValue.Trim() != "")
            {
                URL = DDL_REPORT.SelectedValue.Trim();
                LBL_TITLE.Text = DDL_REPORT.SelectedItem.Text;
            }
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.reportcontent.location.href = '" + URL + "';</script>");
        }
    }
}