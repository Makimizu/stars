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
    public partial class TRAINING_PARTICIPANT_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["SCHEDULE_CODE"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_SUBMISSION_DISPLAY  " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + TXT_CHANNEL.Text.Trim() + "'," +
                                "'" + TXT_LEVEL.Text.Trim() + "'," +
                                "'" + TXT_FULLNAME.Text.Trim() + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DELETE");
                if (DGR.Items[i].Cells[1].Text == "0")
                    btDELETE.Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from TRAINING_SUBMISSION " +
                                    "where " +
                                    "SCHEDULE_CODE = '" + LB_CODE.Text + "' " +
                                    "and AGENT_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void TXT_FULLNAME_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_CHANNEL_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_LEVEL_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

    }
}