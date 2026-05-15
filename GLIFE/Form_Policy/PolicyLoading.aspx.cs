using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyLoading : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{
                //    string s = Session["s"].ToString();
                //}
                //catch
                //{
                //    Response.Redirect("../Standard/FailedSession.aspx");
                //}

                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();
                FillDGRCommission();
                FillDGRChannel();
                LoadFirstChannel();
                FillDGRLoadingPeriodic();
            }
        }

        protected void FillDGRCommission()
        {
            conn.QueryString = "select POLICY_ID from POLICY_OTHER_SETUP where CODE = 'POL11' and VAL = '1' and POLICY_ID =  " + LB_ID.Text;
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                TBL_AGENT.Visible = false;
                TD_CHANNELS.Visible = true;
                return;
            }
            else
            {
                TBL_AGENT.Visible = true;
                TD_CHANNELS.Visible = false;
            }

            conn.QueryString = "exec SP_POLICY_AGENT '" + LB_ID.Text + "'";
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

            conn.QueryString = "select PRODUCT_GROUP from POLICY where ID = " + LB_ID.Text;
            conn.ExecuteQuery();
            switch (conn.GetFieldValue("PRODUCT_GROUP").ToString())
            {
                case "GTLR": DGR_AGENT.Columns[DGR_AGENT.Columns.Count - 1].Visible = false; break;
                case "SP": DGR_AGENT.Columns[DGR_AGENT.Columns.Count - 1].Visible = false; break;
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
                BT_SAVE_PERIODIC.Visible = false;
            }

            conn.QueryString = "select CODE, DESCR from V_LINK_UB_PR_TENOR_TYPE where CODE = 'Y' order by CODE desc";
            conn.ExecuteQuery();
            DDL_PERIODIC.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIODIC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select PRODUCT_GROUP from POLICY where ID = " + LB_ID.Text;
            conn.ExecuteQuery();
            switch (conn.GetFieldValue("PRODUCT_GROUP").ToString())
            {
                case "SP": conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PR_ENDORSEMENT_TYPE where CODE in ('SV_CLUN','SV_CREG','SV_TIR','SV_TOP') order by 2"; ; break;
                default: conn.QueryString = "select CODE, DESCR from UWBOX.dbo.PR_ENDORSEMENT_TYPE where CODE in ('GTL_REG') order by 2"; break;
            }
            conn.ExecuteQuery();
            DDL_TRANS_TYPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANS_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGRChannel()
        {
            conn.QueryString = "exec SP_POLICY_LOADING " + LB_ID.Text + ",null";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CHANNEL.DataSource = dt;
            DGR_CHANNEL.DataBind();

            for (int i = 0; i < DGR_CHANNEL.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_CHANNEL.Items[i].FindControl("LBT_CODE");
                lbt.Text = DGR_CHANNEL.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_CHANNEL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadChannel(e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }
        }

        protected void LoadChannel(string code, string descr)
        {
            int commission_readonly = 0;
            conn.QueryString = "select POLICY_ID from POLICY_OTHER_SETUP where CODE = 'POL11' and VAL = '1' and POLICY_ID =  " + LB_ID.Text;
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
            {
                commission_readonly = 1;
            }

            LB_CHANNELCODE.Text = code;
            LB_CHANNEL.Text = descr;

            conn.QueryString = "exec SP_POLICY_LOADING_DETAIL " +
                                LB_ID.Text + "," +
                                "'" + LB_CHANNELCODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LOADING.DataSource = dt;
            DGR_LOADING.DataBind();

            for (int i = 0; i < DGR_LOADING.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_VAL");
                Label lb = (Label)DGR_LOADING.Items[i].FindControl("LB_VAL");
                CheckBox cb = (CheckBox)DGR_LOADING.Items[i].FindControl("CB");

                txt.Text = DGR_LOADING.Items[i].Cells[1].Text;

                if (DGR_LOADING.Items[i].Cells[0].Text == "COINS" || DGR_LOADING.Items[i].Cells[0].Text == "REINS" || DGR_LOADING.Items[i].Cells[0].Text == "TOT")
                {
                    txt.Visible = false;
                    lb.Text = DGR_LOADING.Items[i].Cells[1].Text;
                    if (DGR_LOADING.Items[i].Cells[0].Text == "TOT")
                    {
                        DGR_LOADING.Items[i].BackColor = System.Drawing.Color.Yellow;
                        DGR_LOADING.Items[i].Font.Bold = true;
                        //DGR_LOADING.Items[i].Font.Size = FontUnit.Small;
                    }
                }
                else
                {
                    cb.Visible = true;
                    if (DGR_LOADING.Items[i].Cells[2].Text == "1")
                        cb.Checked = true;
                }

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txt.Enabled = false;
                    lb.Enabled = false;
                    cb.Enabled = false;
                }

                if (commission_readonly == 1 && DGR_LOADING.Items[i].Cells[3].Text == "1")
                {
                    txt.Enabled = false;
                    DGR_LOADING.Items[i].Cells[4].Font.Bold = true;
                    DGR_LOADING.Items[i].Cells[4].ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void LoadFirstChannel()
        {
            if (TD_CHANNELS.Visible)
            {
                if (DGR_CHANNEL.Items.Count > 0)
                {
                    try
                    {
                        LoadChannel(DGR_CHANNEL.Items[0].Cells[0].Text, DGR_CHANNEL.Items[0].Cells[1].Text);
                    }
                    catch { }
                }
            }
            else
            {
                conn.QueryString = "select " +
                                    "b.SUB_CODE, " +
                                    "b.DESCR " +
                                    "from		POLICY_AGENT a " +
                                    "inner join	MARKETING.dbo.PARAM_SUB_CHANNEL_DISTRIBUTION b on a.AGENT_LEVEL = b.SUB_CODE collate database_default " +
                                    "where " +
                                    "a.POLICY_ID = " + LB_ID.Text + " " +
                                    "and a.COMM_TYPE = 'COMM'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    LoadChannel(conn.GetFieldValue("SUB_CODE").ToString(), conn.GetFieldValue("DESCR").ToString());
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (DGR_LOADING.Items.Count > 0)
            {
                conn.QueryString = "delete from POLICY_LOADING where POLICY_ID=" + LB_ID.Text + " and SUB_CHANNEL='" + LB_CHANNELCODE.Text + "'";
                conn.ExecuteNonQuery();

                for (int i = 0; i < DGR_LOADING.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_LOADING.Items[i].FindControl("TXT_VAL");
                    CheckBox cb = (CheckBox)DGR_LOADING.Items[i].FindControl("CB");

                    if (!txt.Visible)
                        continue;

                    string nett = "0";
                    if (cb.Checked)
                        nett = "1";

                    conn.QueryString = "insert into POLICY_LOADING select " +
                                        LB_ID.Text + "," +
                                        "'" + DGR_LOADING.Items[i].Cells[0].Text + "'," +
                                        "'" + LB_CHANNELCODE.Text + "'," +
                                        txt.Text.Trim().Replace(",", "") + "," +
                                        nett + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                    conn.ExecuteNonQuery();
                }

                FillDGRChannel();
                LoadChannel(LB_CHANNELCODE.Text, LB_CHANNEL.Text);
            }
        }

        protected void FillDGRLoadingPeriodic()
        {
            conn.QueryString = "select PRODUCT_GROUP from POLICY where ID = " + LB_ID.Text + " and PRODUCT_GROUP in ('SP','GTLR')";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
            {
                TBL_PERIODIC.Visible = false;
                return;
            }

            TBL_LOADING.Visible = false;

            conn.QueryString = "exec SP_POLICY_LOADING_PERIODIC " +
                                LB_ID.Text + "," +
                                "'" + DDL_TRANS_TYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LOADING_PERIODIC.DataSource = dt;
            DGR_LOADING_PERIODIC.DataBind();

            for (int i = 0; i < DGR_LOADING_PERIODIC.Items.Count; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    string TXT_ID = j.ToString();
                    if (TXT_ID.Length < 2)
                        TXT_ID = "0" + TXT_ID;
                    TXT_ID = "TXT_VAL" + TXT_ID;
                    TextBox lo = (TextBox)DGR_LOADING_PERIODIC.Items[i].FindControl(TXT_ID);

                    lo.Text = DGR_LOADING_PERIODIC.Items[i].Cells[j].Text;
                    if (lo.Text != "0" && lo.Text.Length != 0)
                        lo.BackColor = System.Drawing.Color.Yellow;

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        lo.Enabled = false;
                    }
                }
            }
        }

        protected void BT_SAVE_PERIODIC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_LOADING_PERIODIC.Items.Count; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    string seq = "0" + j.ToString();
                    if (seq.Length > 2)
                        seq = j.ToString();

                    string val = ((TextBox)DGR_LOADING_PERIODIC.Items[i].FindControl("TXT_VAL" + seq)).Text.Trim().Replace(",", "");

                    try
                    {
                        conn.QueryString = "exec SP_POLICY_LOADING_PERIODIC_UPSERT " +
                                            LB_ID.Text + "," +
                                            "'" + DGR_LOADING_PERIODIC.Items[i].Cells[0].Text + "'," +
                                            val + "," +
                                            j.ToString() + "," +
                                            "'" + DDL_TRANS_TYPE.SelectedValue + "'," +
                                            "'" + DDL_PERIODIC.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRLoadingPeriodic();
        }

        protected void DGR_LOADING_PERIODIC_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_POLICY_LOADING_PERIODIC_TOTAL " +
                                    LB_ID.Text + "," +
                                    "'" + DDL_TRANS_TYPE.SelectedValue + "'";
                conn.ExecuteQuery();

                e.Item.Cells[11].Text = "TOTAL";
                e.Item.Cells[12].Text = conn.GetFieldValue("VAL01").ToString();
                e.Item.Cells[13].Text = conn.GetFieldValue("VAL02").ToString();
                e.Item.Cells[14].Text = conn.GetFieldValue("VAL03").ToString();
                e.Item.Cells[15].Text = conn.GetFieldValue("VAL04").ToString();
                e.Item.Cells[16].Text = conn.GetFieldValue("VAL05").ToString();
                e.Item.Cells[17].Text = conn.GetFieldValue("VAL06").ToString();
                e.Item.Cells[18].Text = conn.GetFieldValue("VAL07").ToString();
                e.Item.Cells[19].Text = conn.GetFieldValue("VAL08").ToString();
                e.Item.Cells[20].Text = conn.GetFieldValue("VAL09").ToString();
                e.Item.Cells[21].Text = conn.GetFieldValue("VAL10").ToString();
            }
        }

        protected void DDL_TRANS_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRLoadingPeriodic();
        }

        protected void BT_SAVE_AGENT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                TextBox txtAGENTCODE = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_AGENTCODE");
                TextBox txtVAL = (TextBox)DGR_AGENT.Items[i].FindControl("TXT_COMM");

                string val = txtVAL.Text.Trim().Replace(",", "");
                if (val == "")
                    val = "0";

                try
                {
                    conn.QueryString = "exec SP_POLICY_AGENT_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DGR_AGENT.Items[i].Cells[0].Text + "'," +
                                        "'" + txtAGENTCODE.Text.Trim() + "'," +
                                        val + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillDGRCommission();
            FillDGRChannel();
            LoadFirstChannel();
            FillDGRLoadingPeriodic();
        }
    }
}