using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CORPORATE_PORTAL
{
    public partial class ContactUs : System.Web.UI.Page
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
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "company = a.ADDRESS1 + '<BR>' + RTRIM(isnull(a.ADDRESS2, '')) + '<BR>' + a.ZIPCODE + '<BR>' + a.COUNTRY ," +
                                "phone = a.PHONE," +
                                "fax = a.FAX," + 
                                "email = '<a href=\"mailto:' + a.EMAIL + ' ?subject=\">' + a.EMAIL + '</a>' " +
                                "from V_LINK_SC_COMPANY a " +
                                "where " +
                                "CODE = '1'";
            conn.ExecuteQuery();
            LB_COMPANY.Text = conn.GetFieldValue("company").ToString();
            LB_COMPANYPHONE.Text = conn.GetFieldValue("phone").ToString();
            LB_COMPANYFAX.Text = conn.GetFieldValue("fax").ToString();
            LB_COMPANYEMAIL.Text = conn.GetFieldValue("email").ToString();

            conn.QueryString = "select " +
                                "agent = UPPER(RTRIM(isnull(b.FRONT_NAME, '') + LTRIM(' ' + isnull(b.MID_NAME, '')) + ' ' + isnull(b.LAST_NAME, ''))), " +
                                "phone = ''," +
                                "email = '<a href=\"mailto:' + b.EMAIL + ' ?subject=\">' + b.EMAIL + '</a>' " +
                                "from V_LINK_HO_POLICY a " +
                                "inner join V_LINK_MR_M_AGENTS b on a.AGENT_CODE = b.CODE " +
                                "where " +
                                "a.ID = " + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            conn.ExecuteQuery();
            LB_AGENT.Text = conn.GetFieldValue("agent").ToString();
            LB_AGENTPHONE.Text = conn.GetFieldValue("phone").ToString();
            LB_AGENTEMAIL.Text = conn.GetFieldValue("email").ToString();
        }
    }
}