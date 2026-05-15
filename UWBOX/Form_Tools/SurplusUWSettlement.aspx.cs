using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace UWBOX.Form_Tools
{
    public partial class SurplusUWSettlement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS_FINANCE_SETTLEMENT '" + Session["s"].ToString() + "'";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT_MEMO");
                bt.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.settlementbody.location.href = '" + e.Item.Cells[0].Text + "&s=" + Session["s"].ToString() + "';</script>");
            }
        }
    }
}