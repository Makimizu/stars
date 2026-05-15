using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class InvoiceOSEmailBancass_Hist : System.Web.UI.Page
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

                FillDGR(Request.QueryString["AGENTCODE"], Request.QueryString["MONTHLYAGING"]);
            }
        }

        protected void FillDGR(string agentcode, string monthlyaging)
        {
            conn.QueryString = "select AGENT_CODE = a.AGENT_CODE, " +
		                       "          AGENT_NAME = b.FULLNAME, " +
		                       "          MONTHLY_AGING = case when a.MONTHLY_AGING = '4' then 'More Than 3 Months' else a.MONTHLY_AGING + ' Month' end, " +
		                       "          RECIPIENT = isnull(a.RECIPIENT,b.EMAIL), " +
		                       "          REQ_DATE = LEFT(convert(varchar(20), a.REQUEST_DATE, 113),20), " +
		                       "          REQ_BY = a.REQUEST_BY, " +
                               "          SEND_DATE = isnull(LEFT(convert(varchar(20), a.SEND_DATE, 113),20), 'HAS NOT BEEN SENT') " +
		                       "     from FINANCE.dbo.EMAIL_INVOICE_OUTSTANDING_REMINDER_BANCASS a " +
                               "inner join MARKETING.dbo.V_M_AGENTS b on a.AGENT_CODE = b.CODE " +
                               "where a.AGENT_CODE = '" + agentcode + "' and a.MONTHLY_AGING = '" + monthlyaging + "' order by a.REQUEST_DATE desc";
            conn.ExecuteQuery(500000);

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }
    }
}