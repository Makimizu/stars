using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGR
{
    public partial class DATA_ALLOWANCE_HEADER : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowProcess();
            }
        }

        protected void ShowProcess()
        {
            LB_TITLE.Text = BT3.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AllowanceBody.location.href = 'DATA_ALLOWANCE_SUMMARY.ASPX';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AllowanceBody.location.href = 'DATA_ALLOWANCE_DETAIL_FRAME.ASPX';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            ShowProcess();
        }
    }
}