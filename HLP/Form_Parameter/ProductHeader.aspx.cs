using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LB_CODE.Text = Request.QueryString["code"];
                conn.QueryString = "select " +
                                    "DESCR, " +
                                    "START_DATE = convert(varchar(20),START_DATE,103), " +
                                    "END_DATE = convert(varchar(20),END_DATE,103) " +
                                    "from PARAM_PRODUCT " +
                                    "where " +
                                    "CODE='" +LB_CODE.Text+ "'";
                conn.ExecuteQuery();

                TXT_PRODUCT.Text = conn.GetFieldValue("DESCR").ToString();
                TXT_TGLSTART.Text = conn.GetFieldValue("START_DATE").ToString();
                TXT_TGLEND.Text = conn.GetFieldValue("END_DATE").ToString();

                ShowPlan();
            }
            catch { }
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowPlan();
        }

        protected void ShowPlan()
        {
            LBL_TITLE.Text = BT1.Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductPlan.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductBenefit.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductRate.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductUP.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductTC.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductFactor.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT_7_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductLoading.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT_8_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            Response.Write("<script language='javascript'>parent.productbody.location.href = 'ProductReinsurance.aspx?code=" + LB_CODE.Text + "';</script>");
        }

    }
}