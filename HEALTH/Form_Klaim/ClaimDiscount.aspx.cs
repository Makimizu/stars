using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimDiscount : System.Web.UI.Page
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
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();

            if (LB_TRACK.Text == "3" || LB_TRACK.Text == "4")
            {
                BT_SAVE.Visible = false;
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_CLM_CLAIM_DISKON '" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_TIPE_DISCOUNT";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                DropDownList ddlTYPE = (DropDownList)DGR.Items[i].FindControl("DDL_TIPE");

                txtAMOUNT.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTYPE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
                try
                {
                    ddlTYPE.SelectedValue = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "delete from CLAIM_DISCOUNT where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_AMOUNT");
                DropDownList ddlTYPE = (DropDownList)DGR.Items[i].FindControl("DDL_TIPE");

                try
                {
                    float discount = float.Parse(txtAMOUNT.Text.Trim().Replace(",", ""));
                    if (discount == 0)
                        continue;
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "- " + ex.Message + "<BR>";
                    continue;
                }

                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_DISKON_INSERT " +
                                        "'" + LB_CLAIMNO.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + ddlTYPE.SelectedValue + "'," +
                                        "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "- " + ex.Message + "<BR>";
                }

            }

            FillDGR();
        }
    }
}