using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace UWBOX.Form_TC
{
    public partial class ProductHealth : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                ShowPackage();
                ShowPlan();
                ShowPlanDetail();
                ShowBenefitDetail();
                ShowTPA();
                ShowDvPackage();
                ShowBenefitNote();
            }
        }

        protected void Setup()
        {
            LB_CODE.Text = Request.QueryString["CODE"].ToString();
            conn.QueryString = "select b.CODE, DESCR = b.CODE + ' - ' + b.DESCR from PARAM_PRODUCT_MASTER_TC a inner join TC_MASTER b on a.TC_ID = b.CODE where a.PRODUCT_CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }

        protected void ShowPackage()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_HEALTH_PACKAGE " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PACKAGE.DataSource = dt;
            DGR_PACKAGE.DataBind();

            for (int i = 0; i < DGR_PACKAGE.Items.Count; i++)
            {
                TextBox txtPackage = (TextBox)DGR_PACKAGE.Items[i].FindControl("TXT_PACKAGE");
                Button btDEL = (Button)DGR_PACKAGE.Items[i].FindControl("BT_DEL_PACKAGE");

                txtPackage.Text = DGR_PACKAGE.Items[i].Cells[1].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
            }
        }

        protected void ShowPlan()
        {
            conn.QueryString = "select " +
                                "URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) + '&PRODUCT_CODE=" + LB_CODE.Text + "&TC_ID=" + DDL_TC.SelectedValue + "' " +
                                "from SECURITY.dbo.REPORT_LIST " +
                                "where " +
                                "APP_ID = 'UW' " +
                                "and REPORT_NAME = 'RPT_PARAM_HEALTH_BENEFIT_PLAN'";
            conn.ExecuteQuery();
            IF_PLAN.Src = conn.GetFieldValue("URL").ToString();
        }

        protected void ShowPlanDetail()
        {
        }

        protected void ShowBenefitDetail()
        {
            conn.QueryString = "select " +
                                "URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) + '&PRODUCT_CODE=" + LB_CODE.Text + "&TC_ID=" + DDL_TC.SelectedValue + "' " +
                                "from SECURITY.dbo.REPORT_LIST " +
                                "where " +
                                "APP_ID = 'UW' " +
                                "and REPORT_NAME = 'RPT_PARAM_PRODUCT_MASTER_HEALTH_BENEFIT_DETAIL'";
            conn.ExecuteQuery();
            IF_BENEFIT_DETAIL.Src = conn.GetFieldValue("URL").ToString();
        }

        protected void ShowTPA()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TPA " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TPA.DataSource = dt;
            DGR_TPA.DataBind();

            for (int i = 0; i < DGR_TPA.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TPA.Items[i].FindControl("CB");
                TextBox txtCHARGE = (TextBox)DGR_TPA.Items[i].FindControl("TXT_CHARGE");
                txtCHARGE.Text = DGR_TPA.Items[i].Cells[2].Text;
                if (DGR_TPA.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void ShowBenefitNote()
        {
            conn.QueryString = "select " +
                                "URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) + '&PRODUCT_CODE=" + LB_CODE.Text + "&TC_ID=" + DDL_TC.SelectedValue + "' " +
                                "from SECURITY.dbo.REPORT_LIST " +
                                "where " +
                                "APP_ID = 'UW' " +
                                "and REPORT_NAME = 'RPT_PARAM_PRODUCT_MASTER_HEALTH_NOTE'";
            conn.ExecuteQuery();
            IF_NOTE.Src = conn.GetFieldValue("URL").ToString();
        }

        protected void ShowDvPackage()
        {
            LB_TITLE.Text = BT_PACKAGE.Text;

            DV_PACKAGE.Visible = true;
            DV_PLAN.Visible = false;
            DV_PLAN_DETAIL.Visible = false;
            DV_BENEFIT_DETAIL.Visible = false;
            DV_TPA.Visible = false;
            DV_NOTE.Visible = false;
        }

        protected void DDL_TC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPackage();
            ShowPlan();
            ShowPlanDetail();
            ShowBenefitDetail();
            ShowDvPackage();
            ShowTPA();
            ShowBenefitNote();
        }

        protected void BT_PACKAGE_Click(object sender, EventArgs e)
        {
            ShowDvPackage();
        }

        protected void BT_PLAN_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_PACKAGE.Visible = false;
            DV_PLAN.Visible = true;
            DV_PLAN_DETAIL.Visible = false;
            DV_BENEFIT_DETAIL.Visible = false;
            DV_TPA.Visible = false;
            DV_NOTE.Visible = false;
        }

        protected void BT_PLAN_DETAIL_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_PACKAGE.Visible = false;
            DV_PLAN.Visible = false;
            DV_PLAN_DETAIL.Visible = true;
            DV_BENEFIT_DETAIL.Visible = false;
            DV_TPA.Visible = false;
            DV_NOTE.Visible = false;

            FillDDLPackage();
            FillDGRPackageDetail();
        }

        protected void BT_BENEFIT_DETAIL_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_PACKAGE.Visible = false;
            DV_PLAN.Visible = false;
            DV_PLAN_DETAIL.Visible = false;
            DV_BENEFIT_DETAIL.Visible = true;
            DV_TPA.Visible = false;
            DV_NOTE.Visible = false;
        }

        protected void BT_TPA_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_PACKAGE.Visible = false;
            DV_PLAN.Visible = false;
            DV_PLAN_DETAIL.Visible = false;
            DV_BENEFIT_DETAIL.Visible = false;
            DV_TPA.Visible = true;
            DV_NOTE.Visible = false;
        }

        protected void BT_NOTE_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_PACKAGE.Visible = false;
            DV_PLAN.Visible = false;
            DV_PLAN_DETAIL.Visible = false;
            DV_BENEFIT_DETAIL.Visible = false;
            DV_TPA.Visible = false;
            DV_NOTE.Visible = true;
        }

        protected void BT_SAVE_PACKAGE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PACKAGE.Items.Count; i++)
            {
                TextBox txtPackage = (TextBox)DGR_PACKAGE.Items[i].FindControl("TXT_PACKAGE");

                conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_UPSERT " +
                                    "'" + LB_CODE.Text + "', " +
                                    "'" + DDL_TC.SelectedValue + "', " +
                                    "'" + DGR_PACKAGE.Items[i].Cells[0].Text + "', " +
                                    "'" + txtPackage.Text.Trim() + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            ShowPackage();
        }

        protected void BT_NEW_PACKAGE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_UPSERT " +
                                    "'" + LB_CODE.Text + "', " +
                                    "'" + DDL_TC.SelectedValue + "', " +
                                    "NULL, " +
                                    "'', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            ShowPackage();
        }

        protected void DGR_PACKAGE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_PRODUCT_MASTER_HEALTH_PACKAGE " +
                    "where " +
                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                    "and TC_ID = '" + DDL_TC.SelectedValue + "' " +
                    "and PACKAGE_ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                ShowPackage();
            }
        }

        protected void BT_SAVE_TPA_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_TPA.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TPA.Items[i].FindControl("CB");
                TextBox txtCHARGE = (TextBox)DGR_TPA.Items[i].FindControl("TXT_CHARGE");

                string taken = "0";
                string charge = "0";
                if (cb.Checked)
                    taken = "1";
                charge = txtCHARGE.Text.Trim().Replace(",", "");

                conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_TPA_UPSERT " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_TC.SelectedValue + "'," +
                                    "'" + DGR_TPA.Items[i].Cells[0].Text + "'," +
                                    taken + "," +
                                    charge + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            ShowTPA();
        }

        protected void DDL_PACKAGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRPackageDetail();
        }

        protected void DGR_PACKAGE_DETAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if(e.CommandName == "Save")
            {
                DropDownList ddlPlanValue = (DropDownList)e.Item.FindControl("DDL_PLAN_VALUE");

                conn.QueryString = "update PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_DETAIL set " +
                                    "PLAN_VALUE = '" + ddlPlanValue.SelectedValue + "' " +
                                    "where " +
                                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "and TC_ID = '" + DDL_TC.SelectedValue + "' " +
                                    "and PACKAGE_ID = '" + DDL_PACKAGE.SelectedValue + "' " +
                                    "and BENEFIT_ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
            }

            if(e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_DETAIL " +
                    "where " +
                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                    "and TC_ID = '" + DDL_TC.SelectedValue + "' " +
                    "and PACKAGE_ID = '" + DDL_PACKAGE.SelectedValue + "'" +
                    "and BENEFIT_ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRPackageDetail();
        }

        protected void FillDGRPackageDetail()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_DETAIL " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'," +
                                "'" + DDL_PACKAGE.SelectedValue + "'," +
                                "1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PACKAGE_DETAIL.DataSource = dt;
            DGR_PACKAGE_DETAIL.DataBind();

            for (int i = 0; i < DGR_PACKAGE_DETAIL.Items.Count; i++)
            {
                DropDownList ddlBenefit = (DropDownList)DGR_PACKAGE_DETAIL.Items[i].FindControl("DDL_BENEFIT");
                DropDownList ddlPlanValue= (DropDownList)DGR_PACKAGE_DETAIL.Items[i].FindControl("DDL_PLAN_VALUE");
                //TextBox txtValue = (TextBox)DGR_PACKAGE_DETAIL.Items[i].FindControl("TXT_PLAN_VALUE");
                Button btDEL = (Button)DGR_PACKAGE_DETAIL.Items[i].FindControl("BT_DEL_PACKAGE_DETAIL");

                conn.QueryString = "select CODE = BENEFIT_ID, DESCR = DESCR from PARAM_HEALTH_BENEFIT order by SEQ";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlBenefit.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                ddlBenefit.SelectedValue = DGR_PACKAGE_DETAIL.Items[i].Cells[0].Text;

                conn.QueryString = "select	distinct PLAN_VALUE " +
                                    "from PARAM_PRODUCT_MASTER_HEALTH_PLAN a " +
                                    "where " +
                                    "a.PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "and a.TC_ID = '" + DDL_TC.SelectedValue + "' " +
                                    "and a.BENEFIT_ID = '" + ddlBenefit.SelectedValue + "'";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlPlanValue.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));

                ddlPlanValue.SelectedValue = DGR_PACKAGE_DETAIL.Items[i].Cells[1].Text;
                //txtValue.Text = DGR_PACKAGE_DETAIL.Items[i].Cells[1].Text;

                btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
            }
        }

        protected void FillDDLPackage()
        {
            DDL_PACKAGE.Items.Clear();
            LB_CODE.Text = Request.QueryString["CODE"].ToString();
            conn.QueryString = "select CODE = PACKAGE_ID, DESCR = PACKAGE_DESCR from PARAM_PRODUCT_MASTER_HEALTH_PACKAGE where PRODUCT_CODE = '" + LB_CODE.Text + "' and TC_ID = '" + DDL_TC.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PACKAGE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            DDL_BENEFIT_ADD.Items.Clear();
            conn.QueryString = "select CODE = BENEFIT_ID, DESCR = DESCR from PARAM_HEALTH_BENEFIT order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BENEFIT_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLPlanValueAdd();
        }

        protected void BT_ADD_PACKAGE_DETAIL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_HEALTH_PACKAGE_DETAIL_INSERT " +
                                    "'" + LB_CODE.Text + "', " +
                                    "'" + DDL_TC.SelectedValue + "', " +
                                    "'" + DDL_PACKAGE.SelectedValue + "', " +
                                    "'" + DDL_BENEFIT_ADD.SelectedValue + "', " +
                                    "'" + DDL_PLAN_VALUE_ADD.SelectedValue + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            FillDGRPackageDetail();
        }

        protected void DDL_BENEFIT_ADD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPlanValueAdd();
        }

        protected void FillDDLPlanValueAdd()
        {
            DDL_PLAN_VALUE_ADD.Items.Clear();
            conn.QueryString = "select	distinct PLAN_VALUE " +
                                "from PARAM_PRODUCT_MASTER_HEALTH_PLAN a " +
                                "where " +
                                "a.PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                "and a.TC_ID = '" + DDL_TC.SelectedValue + "' " +
                                "and a.BENEFIT_ID = '" + DDL_BENEFIT_ADD.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PLAN_VALUE_ADD.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }
    }
}