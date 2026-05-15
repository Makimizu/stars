using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductTC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["CODE"].ToString();
                FillDGR();
                LoadFirstTC();
            }
        }

        protected void LoadFirstTC()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[0].Text == "1")
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProductTCbody.location.href = " + DGR.Items[i].Cells[1].Text + "';</script>");
                    return;
                }
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TC '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[0].Text == "1")
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
                    if (!((CheckBox)sender).Checked)
                    {
                        conn.QueryString = "delete from PARAM_PRODUCT_MASTER_TC where PRODUCT_CODE = '" + LB_ID.Text + "' and TC_ID = '" + DGR.Items[i].Cells[3].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    else
                    {
                        conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TC_INSERT '" + LB_ID.Text + "','" + DGR.Items[i].Cells[3].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    FillDGR();
                    return;
                }
            }
        }

    }
}