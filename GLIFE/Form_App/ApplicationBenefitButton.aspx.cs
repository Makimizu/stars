using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationBenefitButton : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    LB_READONLY.Text = "&readonly=1";
                }
                ShowCoinsReins();
            }
        }

        protected void ShowCoinsReins()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appchild.location.href = 'ApplicationBenefitReins.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowCoinsReins();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appchild.location.href = 'ApplicationBenefitSeries.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }
    }
}