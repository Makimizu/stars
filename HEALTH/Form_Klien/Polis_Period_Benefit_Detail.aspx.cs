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
    public partial class Polis_Period_Benefit_Detail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["PolicyPeriod"]))
                    Response.Redirect("~/Login.aspx");
                else
                    LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];

                Setup();
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


            Fill_DGR_PAKET_BENEFIT();

            conn.QueryString = "select " +
                                "REPORT_URL = (case	when b.FORMAT_FILE <> 'TXT' then c.URL + '&rs:Format=' + b.FORMAT_FILE + '&POLICY_PERIOD_ID=' + a.ID " +
					            "else 'exec ' + replace(c.REPORT_NAME,'_'+b.DESCR,'') + ' ''' +a.ID+ ''','''+a.TPA+'''' end) " +
                                "from POLICY_PERIOD a " +
                                "inner join PARAM_ACT_TPA b on a.TPA=b.CODE " +
                                "inner join V_LINK_SC_REPORT_LIST c on b.REPORT_POLICY_ENROLLMENT = c.CODE " +
                                "where " +
                                "isnull(b.REPORT_POLICY_ENROLLMENT,'') <> '' " +
                                "and a.ID='" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                BT_TPA_PLAN_IMPORT.Visible = true;
                LB_REPORT_URL.Text = conn.GetFieldValue("REPORT_URL").ToString();
            }
        }

        private void Fill_DGR_PAKET_BENEFIT()
        {
            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn2.QueryString = "select CODE,DESCR from PARAM_ACT_BENEFIT_UNIT_LIMIT";
            conn2.ExecuteQuery();

            conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_PLAN " +
                                "'" + LB_PERIOD.Text + "'," +
                                "'" + DDL_PAKET.SelectedValue + "'";
            conn.ExecuteQuery();


            DataTable dt = new DataTable();
            dt = conn.GetDataTable();

            DGR_PAKET_BENEFIT.DataSource = dt;
            DGR_PAKET_BENEFIT.DataBind();

            for (int i = 0; i < DGR_PAKET_BENEFIT.Items.Count; i++)
            {
                Label lb = (Label)DGR_PAKET_BENEFIT.Items[i].FindControl("LB_BENEFIT");
                DataGrid dgr = (DataGrid)DGR_PAKET_BENEFIT.Items[i].FindControl("DGR_DETAIL");
                DropDownList ddl = (DropDownList)DGR_PAKET_BENEFIT.Items[i].FindControl("DDL_BENEFIT");

                conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL_NOTEXIST " +
                                    "'" + DGR_PAKET_BENEFIT.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString() + " - " + conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                lb.Text = "<BR>" + DGR_PAKET_BENEFIT.Items[i].Cells[1].Text + " :";

                conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL " +
                                    "'" + DGR_PAKET_BENEFIT.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                dt = conn.GetDataTable();
                dgr.DataSource = dt;
                dgr.DataBind();


                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    Button btDel = (Button)dgr.Items[j].FindControl("BT_DEL");
                    TextBox txtMAX = (TextBox)dgr.Items[j].FindControl("TXT_MAX");
                    DropDownList ddlUNIT = (DropDownList)dgr.Items[j].FindControl("DDL_UNIT");
                    TextBox txtP = (TextBox)dgr.Items[j].FindControl("TXT_UPP");
                    TextBox txtR = (TextBox)dgr.Items[j].FindControl("TXT_UPR");

                    btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                    for (int a = 0; a < conn2.GetRowCount(); a++)
                    {
                        ddlUNIT.Items.Add(new ListItem(conn2.GetFieldValue(a, 1).ToString(), conn2.GetFieldValue(a, 0).ToString()));
                    }

                    txtMAX.Text = dgr.Items[j].Cells[2].Text;
                    txtP.Text = dgr.Items[j].Cells[4].Text;
                    txtR.Text = dgr.Items[j].Cells[5].Text;
                    try
                    {
                        ddlUNIT.SelectedValue = dgr.Items[j].Cells[3].Text;
                    }
                    catch { }
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            for (int i = 0; i < DGR_PAKET_BENEFIT.Items.Count; i++)
            {
                DataGrid dgr = (DataGrid)DGR_PAKET_BENEFIT.Items[i].FindControl("DGR_DETAIL");
                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    TextBox txtMAX = (TextBox)dgr.Items[j].FindControl("TXT_MAX");
                    DropDownList ddlUNIT = (DropDownList)dgr.Items[j].FindControl("DDL_UNIT");
                    TextBox txtP = (TextBox)dgr.Items[j].FindControl("TXT_UPP");
                    TextBox txtR = (TextBox)dgr.Items[j].FindControl("TXT_UPR");

                    try
                    {
                        conn.QueryString = "update POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL set " +
                                            "FREQ_ID = '" + ddlUNIT.SelectedValue + "', " +
                                            "KUNJUNGAN = '" + txtMAX.Text.Trim().Replace(",", "") + "', " +
                                            "UP_P = '" + txtP.Text.Trim().Replace(",", "") + "', " +
                                            "UP_R = '" + txtR.Text.Trim().Replace(",", "") + "', " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "ID = '" + dgr.Items[j].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                    }
                }
            }

            Fill_DGR_PAKET_BENEFIT();
        }
        protected void DDL_PAKET_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill_DGR_PAKET_BENEFIT();
        }

        protected void DGR_PAKET_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERR.Text = "";
            DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_BENEFIT");

            if (e.CommandName == "Add")
            {
                try
                {
                    conn.QueryString = "exec SP_UW_POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL_ADD " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    Fill_DGR_PAKET_BENEFIT();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }
        }

        protected void DGR_DETAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERR.Text = "";
                try
                {
                    conn.QueryString = "delete from POLICY_PERIOD_PACKAGE_BENEFIT_DETAIL where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    Fill_DGR_PAKET_BENEFIT();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }
        }

        protected void BT_TPA_PLAN_IMPORT_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_POLICY_PERIOD_NBRN_ACTIVITY_INSERT " +
                                    "'" + LB_PERIOD.Text + "'," +
                                    "2," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            if (LB_REPORT_URL.Text.Substring(0, 4) != "exec")
            {
                Response.Redirect(LB_REPORT_URL.Text);
            }
            else
            {
                conn.QueryString = LB_REPORT_URL.Text;
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                GlobalUse.ToCSV(dt, this, LB_PERIOD.Text, false, "\"", ",");
            }
        }
    }
}