using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Premi : System.Web.UI.Page
    {
        #region PrivateVariables

        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                Setup();
                FillDGRPLAN();
            }
        }

        protected void FillDGRPLAN()
        {
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_PLAN " +
                                "'" + LB_PERIOD.Text + "'," +
                                "'" + DDL_PAKET.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrPREMIUM = (DataGrid)DGR.Items[i].FindControl("DGR_PREMIUM");
                Label lbPLAN = (Label)DGR.Items[i].FindControl("LB_PLAN");
                lbPLAN.Text = "<BR>" + DGR.Items[i].Cells[1].Text + " :<BR>";

                conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_PREMIUM " +
                                    "'" + LB_PERIOD.Text + "'," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                dt = conn.GetDataTable();
                dgrPREMIUM.DataSource = dt;
                dgrPREMIUM.DataBind();

                for (int j = 0; j < dgrPREMIUM.Items.Count; j++)
                {
                    TextBox txtPREMIUM = (TextBox)dgrPREMIUM.Items[j].FindControl("TXT_PREMIUM");
                    txtPREMIUM.Text = dgrPREMIUM.Items[j].Cells[4].Text;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "SELECT ID,DESCR FROM dbo.POLICY_PERIOD_PACKAGE " +
                                    "WHERE POLICY_PERIOD_ID='" + LB_PERIOD.Text + "' " +
                                    "ORDER BY SEQ";
            conn.ExecuteQuery();
            DDL_PAKET.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PAKET.Items.Add(new ListItem(conn.GetFieldValue(i, 1), conn.GetFieldValue(i, 0)));
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrPREMIUM = (DataGrid)DGR.Items[i].FindControl("DGR_PREMIUM");
                for (int j = 0; j < dgrPREMIUM.Items.Count; j++)
                {
                    TextBox txtPREMIUM = (TextBox)dgrPREMIUM.Items[j].FindControl("TXT_PREMIUM");

                    try
                    {
                        conn.QueryString = "update POLICY_PERIOD_PACKAGE_PREMIUM set " +
                                            "PREMIUM = '" + txtPREMIUM.Text.Trim().Replace(",", "") + "', " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "ID = '" + dgrPREMIUM.Items[j].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                    }
                }
            }

            FillDGRPLAN();
        }

        protected void DDL_PAKET_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRPLAN();
        }
    }
}