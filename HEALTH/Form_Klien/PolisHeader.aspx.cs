using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace HEALTH.Form_Klien
{
    public partial class PolisHeader : System.Web.UI.Page
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
                ShowTC();
            }            
        }

        protected void Setup()
        {
            try
            {
                BT_ROLLBACK.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");
                FillDdlPolicyPeriod();
                GetPolicyInfo();
            }
            catch (Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void FillDdlPolicyPeriod()
        {
            conn.QueryString = "SELECT ID = b.ID, DESCR = CONVERT(VARCHAR(11), b.START_DATE, 13)+' - '+CONVERT(VARCHAR(11), b.END_DATE, 13) " +
                                "FROM POLICY a INNER JOIN dbo.POLICY_PERIOD b ON a.ID = b.POLICY_ID " +
                                "WHERE a.ID='" + Request.QueryString["ID"] + "' ORDER BY b.START_DATE DESC";
            conn.ExecuteQuery();
            DDL_POLICY_PERIOD.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLICY_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));

            DDL_POLICY_PERIOD.BackColor = Color.Bisque;
            DDL_POLICY_PERIOD.Font.Bold = true;
        }

        protected void GetPolicyInfo()
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
                                "PROCESSDATE = convert(varchar(100), a.PROCESSDATE, 113), " +
                                "CR = convert(varchar(100),convert(money, d.CLAIM_RATIO * 100),1) " +
                                "from V_POLICY_PERIOD a " +
                                "left join V_LINK_MARKETING_M_AGENTS b on a.AGENT_CODE=b.CODE collate database_default " +
                                "left join V_LINK_MARKETING_PARAM_SUB_CHANNEL_DISTRIBUTION c on b.SUB_CODE = c.SUB_CODE " +
                                "left join V_POLICY_PERIOD_PRERENEWAL_NEW d on a.ID = d.ID " +
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
            LB_CR.Text = conn.GetFieldValue("CR");
            LB_AGENTCODE.Text = conn.GetFieldValue("AGENT_CODE");
            LB_AGENT.Text = conn.GetFieldValue("AGENT_NAME");
            LB_CHANNEL.Text = conn.GetFieldValue("SUB_CHANNEL");
            LB_PROCESSDATE.Text = conn.GetFieldValue("PROCESSDATE");

            /*
            switch (company_ranco)
            {
                case "R":
                    I2.Visible = true;
                    DDL_POLICY_PERIOD.Enabled = false;
                    I2.Attributes.Add("src", "../Form_Tools/Track.aspx?tipe=UWRENEWAL&owner=" + DDL_POLICY_PERIOD.SelectedValue);
                    break;
                case "N":
                    I2.Visible = true;
                    DDL_POLICY_PERIOD.Enabled = false;
                    I2.Attributes.Add("src", "../Form_Tools/Track.aspx?tipe=UWNB&owner=" + DDL_POLICY_PERIOD.SelectedValue);
                    break;
                default:
                    I2.Visible = false;
                    DDL_POLICY_PERIOD.Enabled = true;
                    break;
            }
            */
        }

        protected void ShowTC()
        {
            LB_TITLE.Text = BTN_TC.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_TC.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_TC_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BTN_BENEFIT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;       
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Benefit.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_PREMI_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Premi.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void DDL_POLICY_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPolicyInfo();
            BTN_TC_Click(null, EventArgs.Empty);
        }

        protected void BTN_BENEFIT_DETAIL_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Benefit_Detail.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_TPA_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_TPA.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_ARSIP_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            conn.QueryString = "select b.POLICY_NO " +
                                "from POLICY_PERIOD a " +
                                "inner join POLICY b on a.POLICY_ID=b.ID " +
                                "where a.ID = '" + DDL_POLICY_PERIOD.SelectedValue + "'";
            conn.ExecuteQuery();

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_1", conn.GetFieldValue("POLICY_NO"), "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = '" + URL + "';</script>");
        }

        protected void BTN_FINANCE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Finance_Frame.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_REMARK_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = '../Form_Tools/Remark.aspx?tipe=ENDPOL&owner=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_LOADING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Loading.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BTN_BIAYA_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Biaya.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_PRINT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Print_Frame.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        protected void BT_ROLLBACK_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_LINK_HEALTHPROPOSAL_NBRN_ROLLBACK '" + DDL_POLICY_PERIOD.SelectedValue + "'";
                conn.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '../Form_Tools/InquiryScreen.aspx?CODE=006';</script>");
            }
            catch { }
        }

        protected void BTN_CLAIM_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisbody.location.href = 'Polis_Period_Claim.aspx?PolicyPeriod=" + DDL_POLICY_PERIOD.SelectedValue + "';</script>");
        }

        
    }
}