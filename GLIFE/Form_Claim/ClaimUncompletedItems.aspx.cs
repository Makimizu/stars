using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Claim
{
    public partial class ClaimUncompletedItems : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string regno = Request.QueryString["REGNO"].ToString();
                string seq = Request.QueryString["SEQ"].ToString();
                FillDGR(regno, seq);
            }
        }

        protected void FillDGR(string regno, string seq)
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_APV_VALIDATION " +
                                "'" + regno + "'," +
                                seq + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }
    }
}