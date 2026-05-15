using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIFE.Form_POS
{
    public partial class EndorsementBenefitCyclePayoutButton : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();
            }
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.EndorsementPayoutheader.location.href = 'EndorsementBenefitCyclePayout.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + LB_TYPE.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.EndorsementPayoutheader.location.href = 'EndorsementPayout.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + LB_TYPE.Text + "';</script>");
        }
    }
}