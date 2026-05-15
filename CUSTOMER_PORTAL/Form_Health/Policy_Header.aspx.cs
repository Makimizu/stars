using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace CUSTOMER_PORTAL.Form_Health
{
    public partial class Policy_Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                Setup();
                ShowPolicy();
                ShowTC();
            }  
        }

        protected void Setup()
        {
            conn.QueryString = "select ID,DESCR = CONVERT(VARCHAR(11), START_DATE, 13)+' - '+CONVERT(VARCHAR(11), END_DATE, 13) " +
                                "FROM V_LINK_HO_POLICY_PERIOD " +
                                "WHERE POLICY_ID='" + Request.QueryString["ID"] + "' ORDER BY ORDER_PERIOD DESC";
            conn.ExecuteQuery();
            DDL_POLICY_PERIOD.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLICY_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

            DDL_POLICY_PERIOD.BackColor = Color.Bisque;
            DDL_POLICY_PERIOD.Font.Bold = true;
        }

        protected void ShowPolicy()
        {
            conn.QueryString = "select " +
                                "a.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "a.TIPE_DESCR, " +
                                "a.MOP_DESCR, " +
                                "a.PRODUCT, " +
                                "a.REGFORM_NO, " +
                                "a.TIPE_KOMISI, " +
                                "a.TPA_DESCR, " +
                                "a.AGENT_CODE, " +
                                "AGENT_NAME = LTRIM(RTRIM(isnull(b.FRONT_NAME,'') + ' ' + isnull(b.LAST_NAME,''))), " +
                                "SUB_CHANNEL = c.DESCR, " +
                                "PROCESSDATE = convert(varchar(100), a.PROCESSDATE, 113) " +
                                "from V_LINK_HO_POLICY_PERIOD a " +
                                "left join V_LINK_MR_M_AGENTS b on a.AGENT_CODE=b.CODE collate database_default " +
                                "left join V_LINK_MR_PARAM_SUB_CHANNEL_DISTRIBUTION c on b.SUB_CODE = c.SUB_CODE " +
                                "WHERE A.ID = '" + DDL_POLICY_PERIOD.SelectedValue + "'";

            conn.ExecuteQuery();

            LB_COMPANYNAME.Text = conn.GetFieldValue("COMPANY_NAME");
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO");
            LB_REGFORMNO.Text = conn.GetFieldValue("REGFORM_NO");
            LB_POLICY_TYPE.Text = conn.GetFieldValue("TIPE_DESCR");
            LB_MOP.Text = conn.GetFieldValue("MOP_DESCR");
            LB_PRODUCT.Text = conn.GetFieldValue("PRODUCT");
            LB_KOMISI.Text = conn.GetFieldValue("TIPE_KOMISI");
            LB_TPA.Text = conn.GetFieldValue("TPA_DESCR");
            LB_AGENTCODE.Text = conn.GetFieldValue("AGENT_CODE");
            LB_AGENT.Text = conn.GetFieldValue("AGENT_NAME");
            LB_CHANNEL.Text = conn.GetFieldValue("SUB_CHANNEL");
            LB_PROCESSDATE.Text = conn.GetFieldValue("PROCESSDATE");
        }


        protected void ShowReport(string appid, string reportcode, string param)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where APP_ID='" +appid+ "' and CODE='" +reportcode+ "'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() + "&rc:Parameters=False&POLICY_PERIOD_ID=" + param;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = '" + URL + "';</script>");
        }

        protected void ShowTC()
        {
            LB_TITLE.Text = BTN_TC.Text;
            ShowReport("HO", "314", DDL_POLICY_PERIOD.SelectedValue);
        }

        protected void BTN_TC_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BTN_BENEFIT_DETAIL_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ShowReport("HO", "280", DDL_POLICY_PERIOD.SelectedValue);
        }
        
        protected void BTN_PREMI_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;            
            ShowReport("HO", "281", DDL_POLICY_PERIOD.SelectedValue);
        }

        protected void BTN_CLAIM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Policy_Claim.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_FINANCE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Policy_Finance.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_PRINT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ShowReport("HO", "289", DDL_POLICY_PERIOD.SelectedValue);
        }

        protected void BTN_ARSIP_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            conn.QueryString = "select a.POLICY_NO " +
                                "from V_LINK_HO_POLICY_PERIOD a " +
                                "where a.ID = '" + DDL_POLICY_PERIOD.SelectedValue + "'";
            conn.ExecuteQuery();

            string URL = GlobalUse.GetArsipURL("HO", "HO_1", conn.GetFieldValue("POLICY_NO"), "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = '" + URL + "';</script>");
        }

        protected void DDL_POLICY_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPolicy();
            ShowTC();
        }

        protected void BT_UR_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ShowReport("HO", "327", DDL_POLICY_PERIOD.SelectedValue);
        }
    }
}