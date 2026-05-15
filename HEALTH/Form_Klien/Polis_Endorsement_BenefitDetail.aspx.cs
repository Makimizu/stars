using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement_BenefitDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"];
                LB_SEQ.Text = Request.QueryString["SEQ"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PLAN " +
                                "'" + LB_ID.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                string id = conn.GetFieldValue(i, "ID").ToString();
                string descr = conn.GetFieldValue(i, "PACKAGE_DESCR").ToString() + " :   " + conn.GetFieldValue(i, "PLAN_CODE").ToString();
                LB_PLAN.Items.Add(new ListItem(descr, id));
            }
        }

        protected void LB_PLAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowBenefit(LB_PLAN.SelectedValue);
        }

        protected void ShowBenefit(string planid)
        {
            TBL_PACKAGE.Visible = true;

            conn.QueryString = "select " +
                                "ID, " +
                                "BENEFIT_ID, " +
                                "PACKAGE_DESCR, " +
                                "PLAN_CODE " +
                                "from V_ALTER_POLICY_PERIOD_PACKAGE_PLAN " +
                                "where " +
                                "ID = '" + planid + "'";
            conn.ExecuteQuery();

            LB_PLANCODEID.Text = LB_PLAN.SelectedValue;
            LB_BENEFITID.Text = conn.GetFieldValue("BENEFIT_ID").ToString();
            LB_PLANCODE.Text = "<table>" +
                                "<tr><td>PAKET</td><td>:</td><td><B>" + conn.GetFieldValue("PACKAGE_DESCR").ToString() + "</B></td></tr>" +
                                "<tr><td>PLAN</td><td>:</td><td><B>" + conn.GetFieldValue("PLAN_CODE").ToString() + "</B></td></tr>" +
                                "</table>";


            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL_NOTEXIST '" + LB_PLANCODEID.Text + "'";
            conn.ExecuteQuery();
            DDL_BENEFIT.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            BT_PLANCOPY.Visible = false;
            DDL_PLANBENEFIT.Visible = false;
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_PLAN_BENEFIT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_BENEFITID.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                BT_PLANCOPY.Visible = true;
                DDL_PLANBENEFIT.Visible = true;
                DDL_PLANBENEFIT.Items.Clear();
                for (int x = 0; x < conn.GetRowCount(); x++)
                    DDL_PLANBENEFIT.Items.Add(new ListItem(conn.GetFieldValue(x, 1).ToString(), conn.GetFieldValue(x, 0).ToString()));
            }

            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL " +
                                    "'" + LB_PLANCODEID.Text + "'";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_DETAIL.DataSource = dt;
            DGR_DETAIL.DataBind();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn2.QueryString = "select CODE,DESCR from PARAM_ACT_BENEFIT_UNIT_LIMIT";
            conn2.ExecuteQuery();

            for (int j = 0; j < DGR_DETAIL.Items.Count; j++)
            {
                Button btDel = (Button)DGR_DETAIL.Items[j].FindControl("BT_DEL");
                TextBox txtMAX = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_MAX");
                DropDownList ddlUNIT = (DropDownList)DGR_DETAIL.Items[j].FindControl("DDL_UNIT");
                TextBox txtP = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_UPP");
                TextBox txtR = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_UPR");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                for (int a = 0; a < conn2.GetRowCount(); a++)
                {
                    ddlUNIT.Items.Add(new ListItem(conn2.GetFieldValue(a, 1).ToString(), conn2.GetFieldValue(a, 0).ToString()));
                }

                txtMAX.Text = DGR_DETAIL.Items[j].Cells[2].Text;
                txtP.Text = DGR_DETAIL.Items[j].Cells[4].Text;
                txtR.Text = DGR_DETAIL.Items[j].Cells[5].Text;
                try
                {
                    ddlUNIT.SelectedValue = DGR_DETAIL.Items[j].Cells[3].Text;
                }
                catch { }
            }
        }

        protected void DGR_DETAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERR.Text = "";
                try
                {
                    conn.QueryString = "delete from ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    ShowBenefit(LB_PLANCODEID.Text);
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }
        }

        protected void BT_BENEFITADD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL_ADD " +
                                    "'" + LB_PLANCODEID.Text + "'," +
                                    "'" + DDL_BENEFIT.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                ShowBenefit(LB_PLANCODEID.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
            }

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < DGR_DETAIL.Items.Count; j++)
            {
                TextBox txtMAX = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_MAX");
                DropDownList ddlUNIT = (DropDownList)DGR_DETAIL.Items[j].FindControl("DDL_UNIT");
                TextBox txtP = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_UPP");
                TextBox txtR = (TextBox)DGR_DETAIL.Items[j].FindControl("TXT_UPR");

                try
                {
                    conn.QueryString = "update ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL set " +
                                        "FREQ_ID = '" + ddlUNIT.SelectedValue + "', " +
                                        "KUNJUNGAN = '" + txtMAX.Text.Trim().Replace(",", "") + "', " +
                                        "UP_P = '" + txtP.Text.Trim().Replace(",", "") + "', " +
                                        "UP_R = '" + txtR.Text.Trim().Replace(",", "") + "', " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "ID = '" + DGR_DETAIL.Items[j].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }

            ShowBenefit(LB_PLANCODEID.Text);
        }

        protected void BT_PLANCOPY_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL_COPY " +
                                    "'" + DDL_PLANBENEFIT.SelectedValue + "'," +
                                    "'" + LB_PLANCODEID.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                ShowBenefit(LB_PLANCODEID.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
            }
        }
    }
}