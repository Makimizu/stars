using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GO
{
    public partial class EmailAccount : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BT_SAVE.Attributes.Add("onclick", "if(!confirm('Are you sure to SAVE ?')){return false;};");
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "EXEC msdb.dbo.sysmail_help_account_sp";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbMODE = (Label)DGR.Items[i].FindControl("LBL_MODE");
                TextBox txtDISPLAY = (TextBox)DGR.Items[i].FindControl("TXT_DISPLAYNAME");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");
                TextBox txtREPLY = (TextBox)DGR.Items[i].FindControl("TXT_REPLYTO");
                TextBox txtSERVER = (TextBox)DGR.Items[i].FindControl("TXT_SERVER");
                TextBox txtPORT = (TextBox)DGR.Items[i].FindControl("TXT_PORT");
                TextBox txtUID = (TextBox)DGR.Items[i].FindControl("TXT_USERID");
                TextBox txtPWD = (TextBox)DGR.Items[i].FindControl("TXT_PASSWORD");

                lbMODE.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;","");
                txtDISPLAY.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtEMAIL.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtREPLY.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtSERVER.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtPORT.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
                txtUID.Text = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

        }
    }
}