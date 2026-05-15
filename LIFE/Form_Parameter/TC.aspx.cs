using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Parameter
{
    public partial class TC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                LoadTC(LB_CODE.Text);
            }
        }

        protected void LoadTC(string code)
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR " +
                                "from V_LINK_UB_TC_MASTER " +
                                "where code = '" + code + "'";
            conn.ExecuteQuery();
            LB_NAME.Text = conn.GetFieldValue("DESCR").ToString();


            conn.QueryString = "exec SP_LINK_UB_TC_SPECIFICATIONS " +
                                "'" + code + "',1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SPEC.DataSource = dt;
            DGR_SPEC.DataBind();

            conn.QueryString = "exec SP_LINK_UB_TC_SPECIFICATIONS " +
                                "'" + code + "',2";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_BENEFIT.Items[i].FindControl("LBT_RATE");
                if (DGR_BENEFIT.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lbt.Visible = true;
                    lbt.Text = DGR_BENEFIT.Items[i].Cells[1].Text;
                }
            }

            try
            {
                conn.QueryString = "exec SP_LINK_UB_PARAM_UW_MATRIX_DETAIL " +
                                    "'" + code + "'";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_UW.DataSource = dt;
                DGR_UW.DataBind();
            }
            catch { }
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Rate")
            {
                ShowPopUp("PREMIUM RATE : " + e.Item.Cells[1].Text, "1", e.Item.Cells[0].Text);
            }
        }

        protected void ShowPopUp(string title, string mode, string code)
        {
            DGR.Visible = false;

            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

            if (mode == "1")
            {
                LB_RATECODE.Text = code;
                DDL_GENDER.SelectedValue = "M";
                LoadDGRRate(code, "M");
            }
        }

        protected void LoadDGRRate(string code, string gender)
        {
            conn.QueryString = "exec SP_LINK_UB_PARAM_PREMIUM_RATE_DETAIL '" + code + "','" + gender + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DGR.Visible = true;
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();
            }
        }

        protected void DDL_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDGRRate(LB_RATECODE.Text, DDL_GENDER.SelectedValue);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_LINK_UB_PARAM_PREMIUM_RATE_DETAIL '" + LB_RATECODE.Text + "','" + DDL_GENDER.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, LB_TITLE.Text.Replace("PREMIUM RATE : ", ""), true);
        }
    }
}