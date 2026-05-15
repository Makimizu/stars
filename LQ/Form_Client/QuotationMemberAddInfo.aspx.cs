using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationPolicySendAddress : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_MEMBERID.Text = Request.QueryString["MEMBERID"].ToString();

                FillDDLAddress();

                string visible = "0";
                conn.QueryString = "select " +
                                        "VISIBLE = case when PRODUCT_CODE = '059' then 0 " +
                                        "               when PRODUCT_GROUP = 'ITL' then 1 " +
                                        "               when PRODUCT_GROUP = 'IED' then 1 " +
                                        "               when PRODUCT_GROUP = 'IPA' then 1 " +
				                        "               when PAYDI = 0 then 0 "+
				                        "               else 0 " +
			                            "               end " +
                                        "from V_APPLICATION_MASTER " +
                                        "where REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                visible = conn.GetFieldValue("VISIBLE").ToString();
                if (visible == "1")
                {
                    FillDGRMedicalHistory();
                    TBL_MEDICAL_HISTORY.Visible = true;
                }
                else
                {
                    TBL_MEDICAL_HISTORY.Visible = false;
                }
                
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER_POLICY_ADDRESS_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_MEMBERID.Text + "'," +
                                "'" + DDL_POLICY_ADDRESS.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            TXT_POLICY_ADDRESS.Text = "";

            FillDDLAddress();
        }

        protected void FillDDLAddress()
        {
            string selected = "";

            DDL_POLICY_ADDRESS.Items.Clear();

            conn.QueryString = "exec SP_APPLICATION_MEMBER_POLICY_ADDRESS " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_MEMBERID.Text + "'," +
                                "'" + TXT_POLICY_ADDRESS.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_POLICY_ADDRESS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                if (conn.GetFieldValue(i, 2).ToString() == "1")
                {
                    selected = conn.GetFieldValue(i, 0).ToString();
                }
            }

            if (selected != "")
                DDL_POLICY_ADDRESS.SelectedValue = selected;
        }

        protected void TXT_POLICY_ADDRESS_TextChanged(object sender, EventArgs e)
        {
            FillDDLAddress();
        }

        protected void DGR_MEDICAL_HISTORY_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void FillDGRMedicalHistory()
        {
            string alive = "1";
            conn.QueryString = "exec SP_APPLICATION_FAMILY_MEDICAL_HISTORY " +
                                "'" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DGR_MEDICAL_HISTORY.DataSource = conn.GetDataTable().Copy();
            DGR_MEDICAL_HISTORY.DataBind();

            for (int j = 0; j < DGR_MEDICAL_HISTORY.Items.Count; j++)
            {
                DropDownList ddlAlive = (DropDownList)DGR_MEDICAL_HISTORY.Items[j].FindControl("DDL_ALIVE");
                TextBox txtAge = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_AGE");
                TextBox txtHealthCondition = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_HEALTH_CONDITION");
                TextBox txtAgeAtDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_AGE_AT_DEATH");
                TextBox txtYearAtDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_YEAR_OF_DEATH");
                TextBox txtCauseOfDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_CAUSE_OF_DEATH");

                if (DGR_MEDICAL_HISTORY.Items[j].Cells[3].Text.Replace("&nbsp;", "") == "True"){
                    alive = "1";
                }else{
                    alive = "0";
                }

                ddlAlive.SelectedValue = alive;
                txtAge.Text = DGR_MEDICAL_HISTORY.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                txtHealthCondition.Text = DGR_MEDICAL_HISTORY.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                txtAgeAtDeath.Text = DGR_MEDICAL_HISTORY.Items[j].Cells[6].Text.Replace("&nbsp;", "");
                txtYearAtDeath.Text = DGR_MEDICAL_HISTORY.Items[j].Cells[7].Text.Replace("&nbsp;", "");
                txtCauseOfDeath.Text = DGR_MEDICAL_HISTORY.Items[j].Cells[8].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_SAVE_MEDICAL_HISTORY_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < DGR_MEDICAL_HISTORY.Items.Count; j++)
            {
                DropDownList ddlAlive = (DropDownList)DGR_MEDICAL_HISTORY.Items[j].FindControl("DDL_ALIVE");
                TextBox txtAge = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_AGE");
                TextBox txtHealthCondition = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_HEALTH_CONDITION");
                TextBox txtAgeAtDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_AGE_AT_DEATH");
                TextBox txtYearAtDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_YEAR_OF_DEATH");
                TextBox txtCauseOfDeath = (TextBox)DGR_MEDICAL_HISTORY.Items[j].FindControl("TXT_CAUSE_OF_DEATH");

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_FAMILY_MEDICAL_HISTORY_UPSERT " +
                                        "'" + LB_REGNO.Text.Trim() + "'," +
                                        "'" + DGR_MEDICAL_HISTORY.Items[j].Cells[2].Text.Replace("&nbsp;", "") + "'," +
                                        "'" + ddlAlive.SelectedValue + "'," + 
                                        "'" + txtAge.Text.Trim() + "'," +
                                        "'" + txtHealthCondition.Text.Trim() + "'," +
                                        "'" + txtAgeAtDeath.Text.Trim() + "'," +
                                        "'" + txtYearAtDeath.Text.Trim() + "'," +
                                        "'" + txtCauseOfDeath.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRMedicalHistory();
        }
    }
}