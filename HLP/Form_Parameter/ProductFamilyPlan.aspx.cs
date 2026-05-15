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
    public partial class ProductFamilyPlan : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                LB_CODE.Text = Request.QueryString["code"];
                LB_BENEFITID.Text = Request.QueryString["benefitid"];
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN '" + LB_CODE.Text + "','" + LB_BENEFITID.Text + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNSELECTED.DataSource = dt;
            DGR_UNSELECTED.DataBind();
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN '" + LB_CODE.Text + "','" + LB_BENEFITID.Text + "',1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
        }

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_SET '" + e.Item.Cells[0].Text + "',1";
                conn.ExecuteNonQuery();
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_SET '" + e.Item.Cells[0].Text + "',0";
                conn.ExecuteNonQuery();
                FillDGRUnselected();
                FillDGRSelected();
            }

            if (e.CommandName == "Benefit")
            {
                conn.QueryString = "select DESCR from PR_BENEFIT where CODE = '" + LB_BENEFITID.Text + "'";
                conn.ExecuteQuery();
                TBL_FAMILYPLAN.Visible = true;
                LB_BENEFITPLAN.Text = conn.GetFieldValue("DESCR").ToString();
                LB_PLANCODE.Text = e.Item.Cells[0].Text;
                LB_PLANVALUE.Text = e.Item.Cells[1].Text;

                TR_BENEFIT.Visible = true;
                TR_PREMIUM.Visible = false;
                ShowBenefit();
            }

            if (e.CommandName == "Premium")
            {
                conn.QueryString = "select DESCR from PR_BENEFIT where CODE = '" + LB_BENEFITID.Text + "'";
                conn.ExecuteQuery();
                TBL_FAMILYPLAN.Visible = true;
                LB_BENEFITPLAN.Text = conn.GetFieldValue("DESCR").ToString();
                LB_PLANCODE.Text = e.Item.Cells[0].Text;
                LB_PLANVALUE.Text = e.Item.Cells[1].Text;

                TR_BENEFIT.Visible = false;
                TR_PREMIUM.Visible = true;
                ShowPremium();
            }
        }

        protected void ShowBenefit()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_BENEFIT '" + LB_PLANCODE.Text + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_UNSELECTED.DataSource = dt;
            DGR_BENEFIT_UNSELECTED.DataBind();

            conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_BENEFIT '" + LB_PLANCODE.Text + "',1";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT_SELECTED.DataSource = dt;
            DGR_BENEFIT_SELECTED.DataBind();
        }

        protected void ShowPremium()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_PREMIUM '" + LB_PLANCODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PREMIUM.DataSource = dt;
            DGR_PREMIUM.DataBind();

            for (int i = 0; i < DGR_PREMIUM.Items.Count; i++)
            {
                TextBox txtPREMIUM = (TextBox)DGR_PREMIUM.Items[i].FindControl("TXT_PREMIUM");
                TextBox txtSUMINS = (TextBox)DGR_PREMIUM.Items[i].FindControl("TXT_SUMINS");

                txtPREMIUM.Text = DGR_PREMIUM.Items[i].Cells[2].Text;
                txtSUMINS.Text = DGR_PREMIUM.Items[i].Cells[3].Text;
            }
        }

        protected void DGR_BENEFIT_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_BENEFIT_SET '" + LB_PLANCODE.Text + "','" + e.Item.Cells[0].Text + "',1";
                conn.ExecuteNonQuery();
                ShowBenefit();
            }
        }

        protected void DGR_BENEFIT_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_BENEFIT_SET '" + LB_PLANCODE.Text + "','" + e.Item.Cells[0].Text + "',0";
                conn.ExecuteNonQuery();
                ShowBenefit();
            }
        }

        protected void BT_SAVE_PREMIUM_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PREMIUM.Items.Count; i++)
            {
                TextBox txtPREMIUM = (TextBox)DGR_PREMIUM.Items[i].FindControl("TXT_PREMIUM");
                TextBox txtSUMINS = (TextBox)DGR_PREMIUM.Items[i].FindControl("TXT_SUMINS");

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_FAMILY_PLAN_PREMIUM_SET " +
                                        "'" + LB_PLANCODE.Text + "'," +
                                        "'" + DGR_PREMIUM.Items[i].Cells[0].Text + "'," +
                                        txtPREMIUM.Text.Trim().Replace(",", "") + "," +
                                        txtSUMINS.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            ShowPremium();
        }


    }
}