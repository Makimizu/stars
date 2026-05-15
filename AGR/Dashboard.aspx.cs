using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace AGR
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                GoMenu();
                GenerateChart();
                //GenerateCards();
            }
        }

        protected void GoMenu()
        {
            string MenuCode = "";
            try
            {
                MenuCode = !string.IsNullOrEmpty(Session["MENUCODE"] as string) ? Session["MENUCODE"].ToString() : ""; ;
                Session.Remove("MENUCODE");
                conn.QueryString = "select MENU_PATH from SECURITY.dbo.M_MENU where APP_CODE = '" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' and MENU_CODE = '" + MenuCode + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    Response.Redirect(conn.GetFieldValue("MENU_PATH").ToString());
                }
            }
            catch
            {
                return;
            }
        }

        protected void GenerateChart()
        {
            conn.QueryString = "exec SP_GOOGLECHART  " +
                                "2";
            conn.ExecuteQuery();
            LB_CHART1.Text = conn.GetFieldValue("RESULT").ToString();

            conn.QueryString = "exec SP_GOOGLECHART  " +
                                "0";
            conn.ExecuteQuery();
            LB_CHART2.Text = conn.GetFieldValue("RESULT").ToString();

            conn.QueryString = "exec SP_GOOGLECHART  " +
                                "1";
            conn.ExecuteQuery();
            LB_CHART3.Text = conn.GetFieldValue("RESULT").ToString();


            conn.QueryString = "exec SP_YTD_COMMISSION_BYTYPE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_COMMTYPE.DataSource = dt;
            DGR_COMMTYPE.DataBind();

            conn.QueryString = "exec SP_YTD_TOP_AGENT_BYLEVEL";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TOPAGENT.DataSource = dt;
            DGR_TOPAGENT.DataBind();
        }

        protected void GenerateCards()
        {
            conn.QueryString = "exec SP_LINK_SC_GETMENU_CARDS '" + Session["s"].ToString() + "'";
            conn.ExecuteQuery();
            LB_CARDS.Text = conn.GetFieldValue("CARDS").ToString();
        }
    }
}