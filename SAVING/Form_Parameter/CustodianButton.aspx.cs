using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAVING.Form_Parameter
{
    public partial class CustodianButton : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
            }
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustDetail.location.href = 'CustodianProduct.aspx?ID=" + LB_ID.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustDetail.location.href = 'CustodianAttribute.aspx?ID=" + LB_ID.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustDetail.location.href = 'CustodianBank.aspx?ID=" + LB_ID.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.CustDetail.location.href = 'CustodianReport.aspx?ID=" + LB_ID.Text + "';</script>");
        }
    }
}