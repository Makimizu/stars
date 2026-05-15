using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CUSTOMERS.Form_Client
{
    public partial class CompanyAgent : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_CODE.Text = Request.QueryString["code"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_COMPANY_AGENT_HISTORY '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
        }
    }
}