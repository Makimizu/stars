using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;


namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement_Premium : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void Setup()
        {

        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PLAN '" + LB_ID.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrM = (DataGrid)DGR.Items[i].FindControl("DGR_M");
                DataGrid dgrF = (DataGrid)DGR.Items[i].FindControl("DGR_F");
                DataGrid dgrC = (DataGrid)DGR.Items[i].FindControl("DGR_C");
                DataGrid dgrD = (DataGrid)DGR.Items[i].FindControl("DGR_D");

                FillDGRDetail(dgrM, DGR.Items[i].Cells[0].Text, "M");
                FillDGRDetail(dgrF, DGR.Items[i].Cells[0].Text, "F");
                FillDGRDetail(dgrC, DGR.Items[i].Cells[0].Text, "C");
                FillDGRDetail(dgrD, DGR.Items[i].Cells[0].Text, "D");
            }
        }

        protected void FillDGRDetail(DataGrid dgr, string planID, string groupid)
        {
            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PREMIUM  " +
                                    "'" + planID + "'," +
                                    "'" + groupid + "'";
            conn.ExecuteQuery();
            dgr.DataSource = conn.GetDataTable().Copy();
            dgr.DataBind();

            for (int j = 0; j < dgr.Items.Count; j++)
            {
                TextBox txtMIN = (TextBox)dgr.Items[j].FindControl("TXT_MINAGE");
                TextBox txtMAX = (TextBox)dgr.Items[j].FindControl("TXT_MAXAGE");
                TextBox txtPRM = (TextBox)dgr.Items[j].FindControl("TXT_PREMIUM");
                Button btADD = (Button)dgr.Items[j].FindControl("BT_ADD");
                Button btDEL = (Button)dgr.Items[j].FindControl("BT_DEL");

                txtMIN.Text = dgr.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                txtMAX.Text = dgr.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                txtPRM.Text = dgr.Items[j].Cells[3].Text.Replace("&nbsp;", "");

                if (dgr.Items[j].Cells[0].Text.Replace("&nbsp;", "") == "")
                {
                    txtMIN.BackColor = System.Drawing.Color.Yellow;
                    txtMAX.BackColor = System.Drawing.Color.Yellow;
                    txtPRM.BackColor = System.Drawing.Color.Yellow;
                    //dgr.Items[j].BackColor = System.Drawing.Color.Yellow;
                    btADD.Visible = true;
                    btDEL.Visible = false;
                }
                else
                {
                    txtMIN.BackColor = System.Drawing.Color.Gainsboro;
                    txtMAX.BackColor = System.Drawing.Color.Gainsboro;
                    txtPRM.BackColor = System.Drawing.Color.Gainsboro;
                    txtMIN.ReadOnly = true;
                    txtMAX.ReadOnly = true;
                    txtPRM.ReadOnly = true;

                    btADD.Visible = false;
                    btDEL.Visible = true;
                }
            }
        }

        protected void DGR_M_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DGRDetailCommand(e, "M");
        }

        protected void DGR_F_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DGRDetailCommand(e, "F");
        }

        protected void DGR_D_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DGRDetailCommand(e, "D");
        }

        protected void DGR_C_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DGRDetailCommand(e, "C");
        }

        protected void DGRDetailCommand(DataGridCommandEventArgs e, string groupid)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from ALTER_POLICY_PERIOD_PACKAGE_PREMIUM where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGR();
            }

            if (e.CommandName == "Add")
            {
                TextBox txtMIN = (TextBox)e.Item.FindControl("TXT_MINAGE");
                TextBox txtMAX = (TextBox)e.Item.FindControl("TXT_MAXAGE");
                TextBox txtPRM = (TextBox)e.Item.FindControl("TXT_PREMIUM");

                try
                {
                    conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PREMIUM_INSERT " +
                                        "'" + e.Item.Cells[4].Text + "'," +
                                        "'" + groupid + "'," +
                                        "'" + txtMIN.Text.Replace(",", "") + "'," +
                                        "'" + txtMAX.Text.Replace(",", "") + "'," +
                                        "'" + txtPRM.Text.Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }
    }
}