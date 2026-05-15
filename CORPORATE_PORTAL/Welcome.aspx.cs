using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace CORPORATE_PORTAL
{
    public partial class Welcome : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                LoadPolicy(GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            }
        }

        protected void LoadPolicy(string ID)
        {

            conn.QueryString = "exec SP_LINK_HO_GOOGLECHARTCOMBO_POLICY " +
                                ID + "," +
                                "'comboclaimmtd'," +
                                "'MONTHLY CLAIM'," +
                                "'MTD'," +
                                "'YTD'," +
                                "0";
            conn.ExecuteQuery();
            LB_CLAIMCOMBO.Text = conn.GetFieldValue("RESULT").ToString();

            conn.QueryString = "exec SP_LINK_HO_GOOGLECHARTCOMBO_POLICY " +
                                ID + "," +
                                "'comboclaimprov'," +
                                "'TOP 10 HOSPITAL & CLINIC'," +
                                "'Amount'," +
                                "'Case'," +
                                "1";
            conn.ExecuteQuery();
            LB_PROVCOMBO.Text = conn.GetFieldValue("RESULT").ToString();

            conn.QueryString = "exec SP_LINK_HO_GOOGLECHARTPIE_POLICY " +
                                ID + "," +
                                "'membercompos'," +
                                "'MEMBER COMPOSITION'," +
                                "0";
            conn.ExecuteQuery();
            LB_MEMBERCOMPOS.Text = conn.GetFieldValue("RESULT").ToString();

            conn.QueryString = "select ID = max(ID) from V_LINK_HO_POLICY_PERIOD where POLICY_ID = " + ID;
            conn.ExecuteQuery();
            string period = conn.GetFieldValue("ID").ToString();

            conn.QueryString = "exec SP_LINK_HO_RPT_POLICY_UR_ICD '" + period + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ICD.DataSource = dt;
            DGR_ICD.DataBind();

            conn.QueryString = "exec SP_LINK_HO_RPT_POLICY_UR_CLAIM_BYMEMBER '" + period + "',0";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CLM_CASE.DataSource = dt;
            DGR_CLM_CASE.DataBind();
        }
    }
}