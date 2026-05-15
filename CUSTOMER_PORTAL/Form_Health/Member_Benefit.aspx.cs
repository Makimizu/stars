using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Form_Health
{
    public partial class Member_Benefit : System.Web.UI.Page
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
            DGR_CLAIMBENEFIT.Visible = true;
            conn.QueryString = "exec SP_LINK_HO_UW_PESERTA_INFO '" + LB_REGNO.Text + "','5b','" + LB_PERIOD.Text + "',null";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CLAIMBENEFIT.DataSource = dt;
            DGR_CLAIMBENEFIT.DataBind();
        }
    }
}