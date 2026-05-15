using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimReport : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_REPORTS '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT");
                bt.Text = DGR.Items[i].Cells[0].Text.ToUpper();
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ClaimReportBody.location.href = '" + e.Item.Cells[1].Text + "';</script>");
            }
        }
    }
}