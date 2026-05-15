using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace HEALTH
{
    public partial class Statistic : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGRICD();
                FillDGRClaim();
                //FillDGRClaimCase();
                FillDGRNBRN();
            }
        }

        protected void FillDGRICD()
        {
            conn.QueryString = "exec SP_STATISTIC 1";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_ICD.DataSource = dt;
            DGR_ICD.DataBind();
        }

        protected void FillDGRClaim()
        {
            conn.QueryString = "exec SP_STATISTIC 2";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_CLAIM.DataSource = dt;
            DGR_CLAIM.DataBind();
        }

        protected void FillDGRClaimCase()
        {
            conn.QueryString = "exec SP_STATISTIC 3";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_CLAIM_CASE.DataSource = dt;
            DGR_CLAIM_CASE.DataBind();
        }

        protected void FillDGRNBRN()
        {
            conn.QueryString = "exec SP_STATISTIC 4";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_NBRN.DataSource = dt;
            DGR_NBRN.DataBind();
        }
    }
}