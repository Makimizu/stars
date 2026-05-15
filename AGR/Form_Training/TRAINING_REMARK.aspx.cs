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
    public partial class TRAINING_REMARK : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TRAINING_CODE.Text = Request.QueryString["TRAINING_CODE"];
                FillDGR();

            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_MASTER_REMARK '" + LB_TRAINING_CODE.Text + "' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");
                txt.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txt.Attributes.Add("placeholder", "REMARK " + DGR.Items[i].Cells[0].Text + " ...");

                if (DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") != "")
                    txt.BackColor = System.Drawing.Color.LightYellow;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                try
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");
                    conn.QueryString = "exec SP_TRAINING_MASTER_REMARK_UPSERT " +
                                        "'" + LB_TRAINING_CODE.Text + "'," +
                                        DGR.Items[i].Cells[0].Text + "," +
                                        "'" + txt.Text.Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    
                }
                catch { }
            }

            FillDGR();
        }
    }
}