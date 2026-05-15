using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationAgentInfo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadAgent();
                LoadAgentQuestion();
            }

        }

        protected void LoadAgentQuestion()
        {
            string memberid = "";
            conn.QueryString = "select MEMBER_ID from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            memberid = conn.GetFieldValue("MEMBER_ID").ToString();

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'QuotationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=99-AGN&MEMBERID=" + memberid + "&URL=';</script>");
        }

        protected void LoadAgent()
        {
            conn.QueryString = "exec SP_APPLICATION_AGENT_INFO '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_CODE.Text = conn.GetFieldValue("AGENT_CODE").ToString();
            LB_AGENCY.Text = conn.GetFieldValue("AGENCY").ToString();
            LB_FULLNAME.Text = conn.GetFieldValue("AGENT_NAME").ToString();
            LB_LEVEL.Text = conn.GetFieldValue("LEVEL").ToString();
            LB_LICENCENO.Text = conn.GetFieldValue("LICENCE_NO").ToString();
            LB_MARKETSEGMENT.Text = conn.GetFieldValue("MARKET_SEGMENT").ToString();
            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_STATUS.Text = conn.GetFieldValue("STATUS").ToString();
        }

        protected void LB_FULLNAME_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'QuotationAgent.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }
    }
}