using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyChannel : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillDGRChannel();
            }
        }

        protected void FillDGRChannel()
        {
            conn.QueryString = "exec SP_POLICY_CHANNEL_DISTRIBUTION " + LB_ID.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CHANNEL.DataSource = dt;
            DGR_CHANNEL.DataBind();

            for (int i = 0; i < DGR_CHANNEL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_CHANNEL.Items[i].FindControl("CB");
                if (DGR_CHANNEL.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;

                if (Request.QueryString["readonly"].ToString() == "1")
                    cb.Enabled = false;
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            conn.QueryString = "delete from POLICY_CHANNEL_DISTRIBUTION where ID = " + LB_ID.Text;
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_CHANNEL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_CHANNEL.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "insert into POLICY_CHANNEL_DISTRIBUTION select " + LB_ID.Text + ",'" + DGR_CHANNEL.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRChannel();
        }
    }
}