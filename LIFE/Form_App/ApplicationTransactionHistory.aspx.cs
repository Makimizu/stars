using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationTransactionHistory : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                FillDGR();
                Show(DGR.Items[0].Cells[1].Text);
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_TRANSACTION_HISTORY_REPORT '" + LB_REGNO.Text + "'";
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
                Show(e.Item.Cells[1].Text);
            }
        }

        protected void Show(string URL)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.premiumhistory.location.href = '" + URL + "';</script>");
        }
    }
}