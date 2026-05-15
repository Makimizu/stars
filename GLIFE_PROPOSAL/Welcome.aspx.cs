using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class Welcome : System.Web.UI.Page
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
                GenerateCards();
            }
        }

        protected void GenerateCards()
        {
            conn.QueryString = "exec SP_LINK_SC_GETMENU_CARDS '" + Session["s"].ToString() + "'";
            conn.ExecuteQuery();
            LB_CARDS.Text = conn.GetFieldValue("CARDS").ToString();
        }
    }
}