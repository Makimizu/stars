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
    public partial class Polis_Period_Loading : System.Web.UI.Page
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

                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];

                FillDGR();
                FillDGR_TABARRU();
                FillDGRCommission();
            }
        }
        protected void FillDGR()
        {
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_LOADING " +
                                    "'" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                txt.Text = DGR.Items[i].Cells[1].Text;
            }
            conn.QueryString = "exec SP_APPROVAL_UJROH_VALIDATION '" + LB_PERIOD.Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
            conn.ExecuteQuery();
            if (conn.GetRowCount() < 1)
            {
                BT_SAVE.Visible = false;
                //BT_SAVE_TABARRU.Visible = false;
                BT_SAVE_AGENT.Visible = false;
            }
        }
        /*
        protected void FillDGR()
        {
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_LOADING " +
                                    "'" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                txt.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[2].Text == "1")
                {
                    txt.Enabled = false;
                }
            }
        }*/

        protected void FillDGRCommission()
        {
            conn.QueryString = "exec SP_POLICY_PERIOD_AGENT " +
                                    "'" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DGR_AGENT.DataSource = conn.GetDataTable();
            DGR_AGENT.DataBind();

            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtVAL = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMM");

                txtAGENTCODE.Text = DGR_AGENT.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");
                txtVAL.Text = DGR_AGENT.Items[i].Cells[2].Text.Trim();
            }
        }


        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_UW_POLICY_PERIOD_LOADING_TOTAL " +
                                    "'" + LB_PERIOD.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[4].Text = conn.GetFieldValue("VAL").ToString();
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                if (!txt.Enabled)
                    continue;

                try
                {
                    /*conn.QueryString = "update POLICY_PERIOD_LOADING set " +
                                        "VAL = convert(float," + txt.Text + ")/100, " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                        "and LOADING_CODE = '" + DGR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();*/

                     
                    conn.QueryString = "exec SP_APPROVAL_UJROH_VALIDATION '" + LB_PERIOD.Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
                    conn.ExecuteQuery();
                    if (conn.GetRowCount() < 1)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Anda Tidak Berhak Mengubah Loading! Silahkan Hubungi Manager Anda ')", true);
                    }
                    else
                    {
                        conn.QueryString = "update POLICY_PERIOD_LOADING set " +
                                        "VAL = convert(float," + txt.Text + ")/100, " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                        "and LOADING_CODE = '" + DGR.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();                       
                    }
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                }
            }

            FillDGR();
        }

        protected void BT_SAVE_AGENT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtVAL = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMM");

                try
                {
                    conn.QueryString = "exec SP_POLICY_PERIOD_AGENT_UPSERT " +
                                        "'" + LB_PERIOD.Text + "'," +
                                        "'" + DGR_AGENT.Items[i].Cells[0].Text + "'," +
                                        "'" + txtAGENTCODE.Text.Trim() + "'," +
                                        txtVAL.Text.Trim().Replace(",", "") + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGR();
            FillDGRCommission();
        }
        protected void FillDGR_TABARRU()
        {
            conn.QueryString = "exec SP_UW_POLICY_PERIOD_LOADING_TABARRU " +
                                    "'" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            DGR_TABARRU.DataSource = conn.GetDataTable();
            DGR_TABARRU.DataBind();
            for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                txt.Text = DGR_TABARRU.Items[i].Cells[1].Text;
                if (DGR_TABARRU.Items[i].Cells[2].Text == "1")
                {
                    txt.Enabled = false;
                }
            }
        }
        protected void DGR_TABARRU_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_UW_POLICY_PERIOD_LOADING_TOTAL_TABARRU " +
                                    "'" + LB_PERIOD.Text + "'";
                conn.ExecuteQuery();
                e.Item.Cells[3].Text = conn.GetFieldValue("VAL").ToString();
                LB_TOTAL_TABARU.Text = conn.GetFieldValue("VAL").ToString();
            }
        }

        protected void BT_SAVE_TABARRU_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            float ujroh = 0, tabarru = 0;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtujroh = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                //if (!txtujroh.Enabled)
                //    continue;
                ujroh += float.Parse(txtujroh.Text);
            }
            for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
            {
                TextBox txttabarru = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                if (!txttabarru.Enabled)
                    continue;
                tabarru += float.Parse(txttabarru.Text);
            }
            if (ujroh + tabarru == 100)
            {
                //CEK
                /*
                        conn.QueryString = "exec SP_APPROVAL_UJROH_VALIDATION '" + LB_PERIOD.Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
                        conn.ExecuteQuery();
                        if (conn.GetRowCount() < 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Anda Tidak Berhak Mengubah Loading! Silahkan Hubungi Manager Anda ')", true);
                        }
                        else
                        {*/
                //TABARRU
                for (int i = 0; i < DGR_TABARRU.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_TABARRU.Items[i].FindControl("TXT_LOADING_TABARRU");
                    if (!txt.Enabled)
                        continue;
                    try
                    {
                        conn.QueryString = "update POLICY_PERIOD_LOADING_TABARRU set " +
                                            "VAL = convert(float," + txt.Text + ")/100, " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                            "and LOADING_CODE = '" + DGR_TABARRU.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                        conn.QueryString = "INSERT INTO POLICY_PERIOD_LOADING_HISTORY " +
                                           "SELECT '" + LB_PERIOD.Text + "'," +
                                           "       VAL VAL_BEFORE, " +
                                           "       LASTCHANGEBY LASTCHANGEBY_BEFORE, " +
                                           "       LASTCHANGEDATE LASTCHANGEDATE_BEFORE, " +
                                           "       convert(float," + txt.Text + ")/100 VAL_NEW, " +
                                           "       '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' LASTCHANGEBY_NEW, " +
                                           "       GETDATE() LASTCHANGEDATE_NEW, " +
                                           "       'TABARRU' FUND " +
                                           "FROM POLICY_PERIOD_LOADING " +
                                           "WHERE POLICY_PERIOD_ID = '1113_202103' AND LOADING_CODE = '001'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                    }
                }
                //UJROH
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_LOADING");
                    try
                    {
                        conn.QueryString = "update POLICY_PERIOD_LOADING set " +
                                        "VAL = convert(float," + txt.Text + ")/100, " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                        "and LOADING_CODE = '" + DGR.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                        conn.QueryString = "INSERT INTO POLICY_PERIOD_LOADING_HISTORY " +
                                           "SELECT '" + LB_PERIOD.Text + "'," +
                                           "       VAL VAL_BEFORE, " +
                                               "       LASTCHANGEBY LASTCHANGEBY_BEFORE, " +
                                               "       LASTCHANGEDATE LASTCHANGEDATE_BEFORE, " +
                                           "       convert(float," + txt.Text + ")/100 VAL_NEW, " +
                                           "       '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' LASTCHANGEBY_NEW, " +
                                               "       GETDATE() LASTCHANGEDATE_NEW, " +
                                               "       'UJROH' FUND " +
                                           "FROM POLICY_PERIOD_LOADING " +
                                           "WHERE POLICY_PERIOD_ID = '1113_202103' AND LOADING_CODE = '001'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + "<BR>- " + ex.Message;
                    }
                }
                //}
                FillDGR();
                FillDGR_TABARRU();
            }
            else
            {
                Response.Write("<script>alert('Jumlah UJROH dan TABARRU tidak 100%')</script>");
            }
        }
    }
}