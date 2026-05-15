using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductTPA : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                FillGrid();
            }
        }

        protected void FillGrid()
        {
            DGR.Visible = true;
            conn.QueryString = "exec SP_PARAM_PRODUCT_TPA '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt1 = (TextBox)DGR.Items[i].FindControl("TXT_VAL1");
                TextBox txt2 = (TextBox)DGR.Items[i].FindControl("TXT_VAL2");
                TextBox txt3 = (TextBox)DGR.Items[i].FindControl("TXT_VAL3");
                
                txt1.Text = DGR.Items[i].Cells[2].Text;
                txt2.Text = DGR.Items[i].Cells[3].Text;
                txt3.Text = DGR.Items[i].Cells[4].Text;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt1 = (TextBox)DGR.Items[i].FindControl("TXT_VAL1");
                TextBox txt2 = (TextBox)DGR.Items[i].FindControl("TXT_VAL2");
                TextBox txt3 = (TextBox)DGR.Items[i].FindControl("TXT_VAL3");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_TPA_UPSERT " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + txt1.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txt2.Text.Trim().Replace(",", "") + "'," +
                                        "'" + txt3.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillGrid();
        }


    }
}