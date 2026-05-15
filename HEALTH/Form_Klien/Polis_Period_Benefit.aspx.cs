using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Benefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select * FROM dbo.V_POLICY_PERIOD_BENEFIT WHERE POLICY_PERIOD_ID='" + LB_PERIOD.Text + "' ORDER BY ORDER_NO";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();

            DGR.DataSource = dt;
            DGR.DataBind();
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                TextBox txt_benefit_pct = (TextBox)DGR.Items[x].FindControl("TXT_BENEFIT_PCT");
                TextBox txt_discount_pct = (TextBox)DGR.Items[x].FindControl("TXT_DISCOUNT_PCT");
                CheckBox cb_provider = (CheckBox)DGR.Items[x].FindControl("CB_PROVIDER");
                CheckBox cb_reimburse = (CheckBox)DGR.Items[x].FindControl("CB_REIMBURSE");
                CheckBox cb_kapitasi = (CheckBox)DGR.Items[x].FindControl("CB_KAPITASI");
                CheckBox cb_aso = (CheckBox)DGR.Items[x].FindControl("CB_ASO");
                TextBox txt = (TextBox)DGR.Items[x].FindControl("TXT_REMARK");

                txt_benefit_pct.Text = dt.Rows[x]["BENEFIT_PCT"].ToString();
                txt_discount_pct.Text = dt.Rows[x]["DISCOUNT_PCT"].ToString();
                cb_provider.Checked = (dt.Rows[x]["PROVIDER"].ToString() == "True") ? true : false;
                cb_reimburse.Checked = (dt.Rows[x]["REIMBURSE"].ToString() == "True") ? true : false;
                cb_kapitasi.Checked = (dt.Rows[x]["KAPITASI"].ToString() == "True") ? true : false;
                cb_aso.Checked = (dt.Rows[x]["ASO"].ToString() == "True") ? true : false;
                txt.Text = dt.Rows[x]["REMARK"].ToString();


            }
        }


        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            for (int x = 0; x < DGR.Items.Count; x++)
            {
                TextBox txt_benefit_pct = (TextBox)DGR.Items[x].FindControl("TXT_BENEFIT_PCT");
                TextBox txt_discount_pct = (TextBox)DGR.Items[x].FindControl("TXT_DISCOUNT_PCT");
                CheckBox cb_provider = (CheckBox)DGR.Items[x].FindControl("CB_PROVIDER");
                CheckBox cb_reimburse = (CheckBox)DGR.Items[x].FindControl("CB_REIMBURSE");
                CheckBox cb_kapitasi = (CheckBox)DGR.Items[x].FindControl("CB_KAPITASI");
                CheckBox cb_aso = (CheckBox)DGR.Items[x].FindControl("CB_ASO");
                TextBox txt = (TextBox)DGR.Items[x].FindControl("TXT_REMARK");

                string prov, reimb, aso;
                prov = "0";
                reimb = "0";
                aso = "0";
                if (cb_provider.Checked)
                    prov = "1";
                if (cb_reimburse.Checked)
                    reimb = "1";
                if (cb_aso.Checked)
                    aso = "1";

                try
                {
                    conn.QueryString = "update POLICY_PERIOD_BENEFIT set " +
                                        "BENEFIT_PCT = '" + txt_benefit_pct.Text.Trim().Replace(",", "") + "'," +
                                        "DISCOUNT_PCT = '" + txt_discount_pct.Text.Trim().Replace(",", "") + "'," +
                                        "PROVIDER = " + prov + "," +
                                        "REIMBURSE = " + reimb + "," +
                                        "ASO = " + aso + "," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "ID = '" + DGR.Items[x].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }

                try
                {
                    conn.QueryString = "delete from POLICY_PERIOD_BENEFIT_REMARK where PERIOD_BENEFIT_ID = '" + DGR.Items[x].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }

                try
                {
                    conn.QueryString = "insert into POLICY_PERIOD_BENEFIT_REMARK select " +
                                        "NEWID()," +
                                        "'" + DGR.Items[x].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "GETDATE()";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }
        }
    }
}