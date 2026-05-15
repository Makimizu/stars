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
    public partial class Polis_Endorsement_Package : System.Web.UI.Page
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
            FillDGRPackage();
            FillDGRBenefit();
        }

        protected void FillDGRPackage()
        {
            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE '" + LB_ID.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                TextBox txtDESCR = (TextBox)DGR.Items[i].FindControl("TXT_PACKAGE");
                txtDESCR.Text = DGR.Items[i].Cells[3].Text;

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                if (DGR.Items[i].Cells[0].Text == "OLD")
                {
                    btDEL.Visible = false;
                }
                else
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                }

                for (int j = 4; j < 24; j++)
                {
                    Label lbPLAN = (Label)DGR.Items[i].FindControl("LB_PLAN" + (j - 3).ToString());
                    TextBox txtPLAN = (TextBox)DGR.Items[i].FindControl("TXT_PLAN" + (j - 3).ToString());

                    if (DGR.Items[i].Cells[j].Text.Replace("&nbsp;", "") != "")
                    {
                        if (DGR.Items[i].Cells[0].Text == "OLD")
                        {
                            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PLAN_ISNEW " +
                                                "'" + DGR.Items[i].Cells[2].Text + "'," +
                                                (j - 3).ToString();
                            conn.ExecuteQuery();

                            if (conn.GetFieldValue("RESULT").ToString() == "0")
                            {
                                lbPLAN.Text = DGR.Items[i].Cells[j].Text.Replace("&nbsp;", "");
                                txtPLAN.Visible = false;
                            }
                            else
                            {
                                txtPLAN.Text = DGR.Items[i].Cells[j].Text.Replace("&nbsp;", "");
                                txtPLAN.BackColor = System.Drawing.Color.Aqua;
                                lbPLAN.Visible = false;
                            }
                        }
                        else
                        {
                            txtPLAN.Text = DGR.Items[i].Cells[j].Text.Replace("&nbsp;", "");
                            txtPLAN.BackColor = System.Drawing.Color.Aqua;
                            lbPLAN.Visible = false;
                        }
                    }
                    else
                    {
                        lbPLAN.Visible = false;
                        txtPLAN.Visible = true;
                    }
                }
            }
        }

        protected void FillDGRBenefit()
        {
            DGR_BEN.Visible = false;
            BT_SAVEBEN.Visible = false;

            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_BENEFIT '" + LB_ID.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BEN.DataSource = dt;
            DGR_BEN.DataBind();

            if (conn.GetRowCount() > 0)
            {
                DGR_BEN.Visible = true;
                BT_SAVEBEN.Visible = true;
            }

            for (int i = 0; i < DGR_BEN.Items.Count; i++)
            {
                TextBox txtBEN = (TextBox)DGR_BEN.Items[i].FindControl("TXT_BENPCT");
                TextBox txtDISC = (TextBox)DGR_BEN.Items[i].FindControl("TXT_DISCPCT");
                TextBox txtREMARK = (TextBox)DGR_BEN.Items[i].FindControl("TXT_REMARK");
                CheckBox cbP = (CheckBox)DGR_BEN.Items[i].FindControl("CB_P");
                CheckBox cbR = (CheckBox)DGR_BEN.Items[i].FindControl("CB_R");
                CheckBox cbASO = (CheckBox)DGR_BEN.Items[i].FindControl("CB_ASO");


                txtBEN.Text = DGR_BEN.Items[i].Cells[3].Text;
                txtDISC.Text = DGR_BEN.Items[i].Cells[4].Text;
                txtREMARK.Text = DGR_BEN.Items[i].Cells[8].Text.Replace("&nbsp;", "");

                if (DGR_BEN.Items[i].Cells[5].Text == "1")
                    cbP.Checked = true;
                if (DGR_BEN.Items[i].Cells[6].Text == "1")
                    cbR.Checked = true;
                if (DGR_BEN.Items[i].Cells[7].Text == "1")
                    cbASO.Checked = true;

            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                conn.QueryString = "select ORDER_NO,ID from PARAM_ACT_BENEFIT order by ORDER_NO";
                conn.ExecuteQuery();

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    Label lbBEN = (Label)e.Item.FindControl("LB_BEN" + conn.GetFieldValue(j, 0).ToString());
                    lbBEN.Text = conn.GetFieldValue(j, 1).ToString();
                }


                for (int i = 24; i < 44; i++)
                {
                    Label lbBEN = (Label)e.Item.FindControl("LB_BEN" + (i - 23).ToString());
                    if (lbBEN.Text == "")
                    {
                        try
                        {
                            DGR.Columns[i + 1].Visible = false;
                        }
                        catch { }
                    }
                }

            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_ROLLBACK'" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "null," +
                                    "''," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            FillDGR();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            SaveDGRPackage();
            FillDGR();
        }

        protected void SaveDGRPackage()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtPACKAGE = (TextBox)DGR.Items[i].FindControl("TXT_PACKAGE");
                if (DGR.Items[1].Cells[0].Text == "OLD")
                {
                    conn.QueryString = "update POLICY_PERIOD_PACKAGE set " +
                                        "DESCR = '" + txtPACKAGE.Text.Trim() + "' " +
                                        "where " +
                                        "ID = '" + DGR.Items[i].Cells[2].Text + "'";
                }
                else
                {
                    conn.QueryString = "update ALTER_POLICY_PERIOD_PACKAGE set " +
                                        "DESCR = '" + txtPACKAGE.Text.Trim() + "' " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + LB_ID.Text + "' " +
                                        "and SEQ_ID = " + LB_SEQ.Text + " " +
                                        "and ID = '" + DGR.Items[i].Cells[2].Text + "'";
                }

                try
                {
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + " - " + ex.Message + "<BR>";
                }

                for (int j = 1; j < 21; j++)
                {
                    TextBox txtPLAN = (TextBox)DGR.Items[i].FindControl("TXT_PLAN" + j.ToString());
                    if (txtPLAN.Visible && txtPLAN.Text.Trim() != "")
                    {
                        try
                        {
                            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PLAN_UPSERT " +
                                                "'" + LB_ID.Text + "'," +
                                                LB_SEQ.Text + "," +
                                                j.ToString() + "," +
                                                "'" + DGR.Items[i].Cells[2].Text + "'," +
                                                txtPLAN.Text.Trim() + "," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERROR.Text = LB_ERROR.Text + " - " + ex.Message + "<BR>";
                        }
                    }

                    if (txtPLAN.Visible && txtPLAN.Text.Trim() == "")
                    {
                        try
                        {
                            conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_PACKAGE_PLAN_ROLLBACK " +
                                                "'" + LB_ID.Text + "'," +
                                                LB_SEQ.Text + "," +
                                                j.ToString() + "," +
                                                "'" + DGR.Items[i].Cells[2].Text + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERROR.Text = LB_ERROR.Text + " - " + ex.Message + "<BR>";
                        }
                    }
                }
            }
        }

        protected void SaveDGRBenefit()
        {
            for (int i = 0; i < DGR_BEN.Items.Count; i++)
            {
                TextBox txtBEN = (TextBox)DGR_BEN.Items[i].FindControl("TXT_BENPCT");
                TextBox txtDISC = (TextBox)DGR_BEN.Items[i].FindControl("TXT_DISCPCT");
                TextBox txtREMARK = (TextBox)DGR_BEN.Items[i].FindControl("TXT_REMARK");
                CheckBox cbP = (CheckBox)DGR_BEN.Items[i].FindControl("CB_P");
                CheckBox cbR = (CheckBox)DGR_BEN.Items[i].FindControl("CB_R");
                CheckBox cbASO = (CheckBox)DGR_BEN.Items[i].FindControl("CB_ASO");

                string P = "1";
                string R = "1";
                string ASO = "0";

                if (!cbP.Checked)
                    P = "0";
                if (!cbR.Checked)
                    R = "0";
                if (cbASO.Checked)
                    ASO = "1";

                try
                {
                    conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_BENEFIT_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + DGR_BEN.Items[i].Cells[1].Text + "'," +
                                        (float.Parse(txtBEN.Text.Trim())/100).ToString() + "," +
                                        (float.Parse(txtDISC.Text.Trim())/ 100).ToString() + "," +
                                        "0," +
                                        P + "," +
                                        R + "," +
                                        ASO + "," +
                                        "0," +
                                        "'" + txtREMARK.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";


                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + " - " + ex.Message + "<BR>";
                }

            }
        }

        protected void BT_SAVEBEN_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            SaveDGRBenefit();
            FillDGR();
        }
    }
}