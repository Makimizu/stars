using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ
{
    public partial class Welcome : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["s"] == null)
                Response.Redirect("logout.aspx");

            Setup();
            GenerateCards();
            GenerateCharts();
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "NAME = LTRIM(RTRIM(isnull(b.FRONT_NAME,'') + ' ' + isnull(b.LAST_NAME,''))) " +
                                "from V_LINK_SC_USER_LOG_HISTORY a " +
                                "inner join V_LINK_SC_M_USERS b on a.USER_CODE = b.CODE collate database_default " +
                                "where " +
                                "a.ROWID = '" + Session["s"] + "'";
            conn.ExecuteQuery();
            LB_USER.Text = "Hi, " + conn.GetFieldValue("NAME").ToString();
        }

        protected void GenerateCards()
        {
            conn.QueryString = "exec SP_LINK_SC_GETMENU_CARDS '" + Session["s"].ToString() + "'";
            conn.ExecuteQuery();
            LB_CARDS.Text = conn.GetFieldValue("CARDS").ToString();
        }

        protected void GenerateCharts()
        {
            conn.QueryString = "exec SP_GOOGLECHARTCOMBO '" + Session["s"].ToString() + "',0,'chart1'";
            conn.ExecuteQuery();
            LB_CHART1.Text = conn.GetFieldValue("RESULT").ToString();
        }
    }
}