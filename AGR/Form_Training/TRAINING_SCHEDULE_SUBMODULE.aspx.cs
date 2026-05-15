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
    public partial class TRAINING_SCHEDULE_SUBMODULE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_SCHEDULE_CODE.Text = Request.QueryString["SCHEDULE_CODE"];
                FillDGR();

            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUBMODULE '" + LB_SCHEDULE_CODE.Text + "' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") == "1")
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
                    string flag = "0";
                    if (cb.Checked)
                        flag = "1";

                    conn.QueryString = "exec SP_TRAINING_SCHEDULE_SUBMODULE_FLAG " +
                                        "'" + LB_SCHEDULE_CODE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR.Items[i].Cells[1].Text + "'," +
                                        flag;
                    conn.ExecuteQuery();
                    return;
                }
            }

            FillDGR();
        }
    }
}