using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR
{
    public partial class TRAINING_PARTICIPANT_CHANNEL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["SCHEDULE_CODE"].ToString();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUB_CHANNEL_DISPLAY '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    string check = "0";
                    if (((CheckBox)sender).Checked)
                        check = "1";

                    conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUB_CHANNEL_UPSERT " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        check + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    return;
                }
            }

            FillDGR();
        }
    }
}