using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationBenefitCycle : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                FillDGRBenefitCycle();
            }
        }

        protected void FillDGRBenefitCycle()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_CYCLE.DataSource = dt;
            DGR_BENEFIT_CYCLE.DataBind();

            for (int i = 0; i < DGR_BENEFIT_CYCLE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT_CYCLE.Items[i].FindControl("CB");
                if (DGR_BENEFIT_CYCLE.Items[i].Cells[0].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {            
            for (int i = 0; i < DGR_BENEFIT_CYCLE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_BENEFIT_CYCLE.Items[i].FindControl("CB");

                if (cb == (CheckBox)sender)
                {
                    string tobepaid = "0";
                    if (cb.Checked)
                        tobepaid = "1";

                    conn.QueryString = "exec SP_APPLICATION_BENEFIT_CYCLE_UPDATE " +
                                        "'" + LB_REGNO.Text + "'," +
                                        DGR_BENEFIT_CYCLE.Items[i].Cells[1].Text + "," +
                                        tobepaid + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGRBenefitCycle();
                    return;
                }
            }            
        }
    }
}