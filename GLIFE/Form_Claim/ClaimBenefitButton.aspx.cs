using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Claim
{
    public partial class ClaimBenefitButton : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    LB_READONLY.Text = "&readonly=1";
                }
                ShowDiagnose();
            }
        }

        protected void ShowDiagnose()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitbuttoncontent.location.href = 'ClaimBenefitICD.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowDiagnose();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimbenefitbuttoncontent.location.href = '../Form_App/ApplicationBenefitSeries.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }
    }
}