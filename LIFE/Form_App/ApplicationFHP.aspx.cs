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
    public partial class ApplicationFHP : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=BT_SEARCH]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);
            }
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";

            conn.QueryString = "exec SP_APPLICATION_FHP_CURRENT_PERIOD '" + DDL_CR.SelectedValue + "','" + TXT_FULLNAME.Text.Trim() + "'";
            conn.ExecuteQuery(500000);

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btREJECT = (Button)DGR.Items[i].FindControl("BT_REJECT");
                btREJECT.Attributes.Add("onclick", "if(!confirm('Are you sure to REJECT ?')){return false;};");
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Reject")
            {
                TextBox txtREMARK = (TextBox)e.Item.FindControl("TXT_REMARK");
                if (txtREMARK.Text.Trim() == "")
                    return;

                conn.QueryString = e.Item.Cells[0].Text.Replace("@REMARK", "'" + txtREMARK.Text.Replace("'", "`") + "'").Replace("@USERBY", "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'");
                conn.ExecuteNonQuery();

                DGR.CurrentPageIndex = 0;
                FillDGR();
                return;
            }
        }
    }
}