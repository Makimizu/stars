using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class AGENT_HOLD_REMUN_STOP : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conn.QueryString = "exec SP_M_AGENT_RELEASE_REMUN " +
                                    "'" + Request.QueryString["CODE"].ToString() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("ENDORSEMENT_HISTORY.aspx?mode=7");
            }
        }
    }
}