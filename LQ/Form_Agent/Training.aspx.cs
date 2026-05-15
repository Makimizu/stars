using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Agent
{
    public partial class Training : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGRUpcoming();
                FillDGRPast();
            }
        }

        protected void FillDGRUpcoming()
        {
            conn.QueryString = "exec SP_LINK_MARKETING_TRAINING_SCHEDULE '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UPCOMING.DataSource = dt;
            DGR_UPCOMING.DataBind();
        }

        protected void FillDGRPast()
        {
            conn.QueryString = "exec SP_LINK_MARKETING_TRAINING_SCHEDULE '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PAST.DataSource = dt;
            DGR_PAST.DataBind();
        }
    }
}