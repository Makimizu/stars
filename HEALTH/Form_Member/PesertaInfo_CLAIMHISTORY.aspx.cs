using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class PesertaInfo_CLAIMHISTORY : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"];
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            DGR_CLAIM.Visible = true;
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','5','" + LB_PERIOD.Text + "',null";
            conn.ExecuteQuery(150000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CLAIM.DataSource = dt;
            DGR_CLAIM.DataBind();
        }
    }
}