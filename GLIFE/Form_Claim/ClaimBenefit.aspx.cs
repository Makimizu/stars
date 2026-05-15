using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Claim
{
    public partial class ClaimBenefit : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                bDone = TrackDone();
                LoadBenefit();
                LoadCharge();
                LoadSavingAlert();

                if (bDone || Request.QueryString["readonly"] == "1")
                {
                    BT_SAVE.Visible = false;
                }
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void LoadBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_BENEFIT '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();


            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_AMOUNT");
                txtAMOUNT.Text = DGR_BENEFIT.Items[i].Cells[1].Text;

                if (bDone || Request.QueryString["readonly"] == "1")
                    txtAMOUNT.ReadOnly = true;
            }
        }

        protected void LoadCharge()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_CHARGE_COMPONENT '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CHARGE.DataSource = dt;
            DGR_CHARGE.DataBind();


            for (int i = 0; i < DGR_CHARGE.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_CHARGE");
                txtAMOUNT.Text = DGR_CHARGE.Items[i].Cells[1].Text;

                if (DGR_CHARGE.Items[i].Cells[2].Text == "0")
                {
                    txtAMOUNT.BackColor = System.Drawing.Color.Pink;
                }

                if (bDone || Request.QueryString["readonly"] == "1")
                    txtAMOUNT.ReadOnly = true;
            }
        }


        protected void DGR_CHARGE_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                e.Item.Cells[3].Font.Bold = true;
                e.Item.Cells[3].Text = "TOTAL APPROVED";

                conn.QueryString = "exec SP_APPLICATION_CLAIM_CHARGE_COMPONENT_TOTAL '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
                conn.ExecuteQuery();
                e.Item.Cells[4].Text = conn.GetFieldValue("TOTAL").ToString();
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_AMOUNT");

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_CLAIM_BENEFIT_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text + "'," +
                                        "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[5].Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            for (int i = 0; i < DGR_CHARGE.Items.Count; i++)
            {
                TextBox txtAMOUNT = (TextBox)DGR_CHARGE.Items[i].FindControl("TXT_CHARGE");

                try
                {
                    conn.QueryString = "exec SP_APPLICATION_CLAIM_CHARGE_COMPONENT_UPSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + DGR_CHARGE.Items[i].Cells[0].Text + "'," +
                                        "'" + txtAMOUNT.Text.Trim().Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            try
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_SHARE_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            LoadBenefit();
            LoadCharge();
        }

        protected void LoadSavingAlert()
        {
            try
            {
                conn.QueryString = "exec SP_APPLICATION_CLAIM_MASTER_SAVING_ALERT '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                LB_SAVING_ALERT.Text = conn.GetFieldValue(0, 0).ToString();
            }
            catch { }
        }
    }
}