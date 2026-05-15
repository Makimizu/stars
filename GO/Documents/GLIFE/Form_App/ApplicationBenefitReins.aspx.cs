using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationBenefitReins : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                Setup();
                LoadShare();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select SEQ = MAX(SEQ) from TRACK_DATA where TIPE_CODE='UW' and OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            //if (conn.GetRowCount() == 0)
            //    result = false;
            if (int.Parse(conn.GetFieldValue("SEQ").ToString()) < 3)
                result = false;

            return result;
        }

        protected void Setup()
        {
            if (bDone || Request.QueryString["readonly"] == "1")
            {
                TR_REINSADD.Visible = false;
            }
            else
            {
                conn.QueryString = "select CODE, DESCR from V_LINK_REINS_PR_REINS_TYPE order by 1 desc";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

                FillDDLReinsurance();
            }
        }

        protected void FillDDLReinsurance()
        {
            conn.QueryString = "select ID, DESCR from V_LINK_REINS_TC_MASTER where TYPE = '" + DDL_TYPE.SelectedValue + "' order by 2 desc";
            conn.ExecuteQuery();
            DDL_REINS.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REINS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadShare()
        {
            conn.QueryString = "exec SP_APPLICATION_SUMINS_SHARE '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbAMOUNT = (Label)DGR.Items[i].FindControl("LB_SHAREAMOUNT");
                TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_SHAREAMOUNT");
                Label lbRATE = (Label)DGR.Items[i].FindControl("LB_SHARERATE");
                TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_SHARERATE");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_SHAREDEL");
                Button btEMAIL = (Button)DGR.Items[i].FindControl("BT_EMAIL");

                txtAMOUNT.Text = DGR.Items[i].Cells[1].Text;
                lbAMOUNT.Text = DGR.Items[i].Cells[1].Text;
                txtRATE.Text = DGR.Items[i].Cells[2].Text;
                lbRATE.Text = DGR.Items[i].Cells[2].Text;

                if (DGR.Items[i].Cells[3].Text == "REINS")
                {
                    if (bDone || Request.QueryString["readonly"] == "1")
                    {
                        txtAMOUNT.Visible = false;
                        lbAMOUNT.Visible = true;
                        txtRATE.Visible = false;
                        lbRATE.Visible = true;
                        btDEL.Visible = false;
                        btEMAIL.Visible = false;
                    }
                    else
                    {
                        txtAMOUNT.Visible = true;
                        lbAMOUNT.Visible = false;
                        txtRATE.Visible = true;
                        lbRATE.Visible = false;
                        btDEL.Visible = true;
                        btEMAIL.Visible = true;
                    }
                }
                else
                {
                    txtAMOUNT.Visible = false;
                    btDEL.Visible = false;
                    lbAMOUNT.Visible = true;
                    btEMAIL.Visible = false;
                    txtRATE.Visible = false;
                    lbRATE.Visible = false;

                    if (DGR.Items[i].Cells[3].Text == "OWN")
                    {
                        DGR.Items[i].ForeColor = System.Drawing.Color.Green;
                        DGR.Items[i].Font.Bold = true;
                    }

                    if (DGR.Items[i].Cells[3].Text == "TOTAL")
                    {
                        DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                        DGR.Items[i].Font.Bold = true;
                    }
                }

                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }

            if (bDone || Request.QueryString["readonly"] == "1")
            {
                DGR.Columns[8].Visible = false;
                DGR.Columns[11].Visible = false;
            }
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLReinsurance();
        }

        protected void BT_REINSADD_Click(object sender, EventArgs e)
        {
            if (DDL_REINS.SelectedValue != "")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_REINSURANCE_INSERT_NEW " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DDL_REINS.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    LoadShare();
                }
                catch { }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Email")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_REINSURANCE_EMAIL '" + LB_REGNO.Text + "'," + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APPLICATION_REINSURANCE where REGNO = '" + LB_REGNO.Text + "' and SEQ=" + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                    LoadShare();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_SHAREAMOUNT");
                    TextBox txtRATE = (TextBox)DGR.Items[i].FindControl("TXT_SHARERATE");
                    if (txtAMOUNT.Visible)
                    {
                        conn.QueryString = "update APPLICATION_REINSURANCE set " +
                                            "AMOUNT = " + txtAMOUNT.Text.Replace(",", "") + ", " +
                                            "RATE = " + txtRATE.Text.Replace(",", "") + ", " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "REGNO = '" + LB_REGNO.Text + "' " +
                                            "and SEQ = " + DGR.Items[i].Cells[0].Text;
                        conn.ExecuteNonQuery();
                    }
                }

                /*
                conn.QueryString = "delete from APPLICATION_REINSURANCE " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteNonQuery();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_SHAREAMOUNT");
                    if (txtAMOUNT.Visible)
                    {
                        try
                        {
                            conn.QueryString = "exec SP_APPLICATION_REINSURANCE_ADD " +
                                                "'" + LB_REGNO.Text + "'," +
                                                "'" + DGR.Items[i].Cells[DGR.Columns.Count - 1].Text.Replace("&nbsp;", "") + "'," +
                                                txtAMOUNT.Text.Trim().Replace(",", "") + "," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }
                */

                /*
                float totalshare = 0;
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_SHAREAMOUNT");
                    if (txtAMOUNT.Visible)
                    {
                        try
                        {
                            totalshare = totalshare + float.Parse(txtAMOUNT.Text.Trim().Replace(",", ""));
                        }
                        catch { }
                    }
                }

                conn.QueryString = "select REGNO from V_APPLICATION_MASTER " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and OWN_SUMINS >= " + totalshare.ToString();
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    LoadShare();
                    return;
                }

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtAMOUNT = (TextBox)DGR.Items[i].FindControl("TXT_SHAREAMOUNT");
                    if (txtAMOUNT.Visible)
                    {
                        try
                        {
                            conn.QueryString = "update APPLICATION_REINSURANCE set " +
                                                "AMOUNT = " + txtAMOUNT.Text.Trim().Replace(",", "") + " " +
                                                "where " +
                                                "REGNO = '" + LB_REGNO.Text + "' " +
                                                "and SEQ = " + DGR.Items[i].Cells[0].Text;
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }
                */

                LoadShare();
            }
        }
    }
}