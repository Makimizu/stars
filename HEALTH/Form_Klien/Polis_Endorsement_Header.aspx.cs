using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement_Header : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                LB_SEQ.Text = Request.QueryString["SEQ"];
                LoadRecord();
            }
        }

        protected void LoadRecord()
        {
            conn.QueryString = "select " +
                                "ID, " +
                                "SEQ, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "PERIOD = convert(varchar(20),START_DATE,106) + ' - ' + convert(varchar(20),END_DATE,106), " +
                                "CREATEBY, " +
                                "CREATEDATE " +
                                "from V_ALTER_POLICY_PERIOD a " +
                                "where " +
                                "ID = '" + LB_ID.Text + "' " +
                                "and SEQ = " + LB_SEQ.Text;
            conn.ExecuteQuery();

            LB_POLICY.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DATEREG.Text = conn.GetFieldValue("CREATEDATE").ToString();
            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_USERREG.Text = conn.GetFieldValue("CREATEBY").ToString();

            LoadPackage();
        }

        protected void LoadPackage()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisendorsbody.location.href = 'Polis_Endorsement_Package.aspx?ID=" + LB_ID.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {            
            LoadPackage();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_POL_END", LB_ID.Text + "-" + LB_SEQ.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisendorsbody.location.href = '" + URL + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisendorsbody.location.href = 'Polis_Endorsement_Premium.aspx?ID=" + LB_ID.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.polisendorsbody.location.href = 'Polis_Endorsement_BenefitDetail.aspx?ID=" + LB_ID.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }
    }
}