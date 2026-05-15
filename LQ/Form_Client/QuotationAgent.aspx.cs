using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationAgent : System.Web.UI.Page
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
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_AGENT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtCODE = (TextBox)DGR.Items[i].FindControl("TXT_AGENTCODE");
                txtCODE.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                if (DGR.Items[i].Cells[0].Text == "COMM")
                {
                    txtCODE.Enabled = false;
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtCODE = (TextBox)DGR.Items[i].FindControl("TXT_AGENTCODE");
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_AGENT_UPSERT" +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + txtCODE.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGR();
        }
    }
}