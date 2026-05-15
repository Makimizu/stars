using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_EVALUATION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            //conn.QueryString = "select CODE,DESCR from PR_CHANNEL_DISTRIBUTION order by 2";
            conn.QueryString = "select CODE,DESCR from PR_MARKET_SEGMENT order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLLevel();
        }

        protected void FillDDLLevel()
        {
            DDL_LEVELFROM.Items.Clear();
            DDL_LEVELTO.Items.Clear();

            //conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' and seq > 0";
            conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT  = '" + DDL_CHANNEL.SelectedValue + "' and seq > 0";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVELFROM.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_LEVELTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            if (DDL_MODE.SelectedValue == "0")
            {
                bool bFound = false;
                for (int i = 0; i < DDL_LEVELTO.Items.Count; i++)
                {
                    if (DDL_LEVELTO.Items[i].Value == "")
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    DDL_LEVELTO.Items.Add(new ListItem("-- TERMINATE --", ""));
            }
            else
            {
                for (int i = 0; i < DDL_LEVELTO.Items.Count; i++)
                {
                    if (DDL_LEVELTO.Items[i].Value == "")
                    {
                        DDL_LEVELTO.Items.RemoveAt(i);
                        break;
                    }
                }

            }

            //conn.QueryString = "select SUB_CODE,DESCR,TEAM_MEMBER='0' from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc";
            conn.QueryString = "select SUB_CODE,DESCR,TEAM_MEMBER='0' from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEVELMEMBER.DataSource = dt;
            DGR_LEVELMEMBER.DataBind();
            for (int i = 0; i < DGR_LEVELMEMBER.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LEVELMEMBER.Items[i].FindControl("TXT_TEAM_MEMBER");
                txt.Text = DGR_LEVELMEMBER.Items[i].Cells[2].Text;
            }

        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
        }

        protected void FillDGR()
        {
            LB_INFO.Text = "";
            LB_CURRENT_LEVEL.Text = "";
            LB_NEXT_LEVEL.Text = "";
            TBL_PERIOD.Visible = false;
            TBL_TRAINING.Visible = false;
            TBL_LVLMEMBER.Visible = false;

            conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION " + DDL_MODE.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btTRAINING = (Button)DGR.Items[i].FindControl("BT_TRAINING");
                if (DDL_MODE.SelectedValue == "0")
                {
                    btTRAINING.Visible = false;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                //FillDDLLevel();

                conn.QueryString = "select " +
                                   "     a.CURRENT_AGENT_LEVEL," +
                                   "     a.NEXT_AGENT_LEVEL," +
                                   "     CHANNEL		= c.DESCR," +
                                   "     MINIMUM_ANP	= replace(convert(varchar(100), convert(money, isnull(a.MINIMUM_ANP,0)), 1), '.00', '')," +
                                   "     a.MINIMUM_TEAM_MEMBER," +
                                   "     a.MINIMUM_PERSISTENCE," +
                                   "     a.MINIMUM_MONTH_MEMBERSHIP" +
                                   " from		PARAM_AGENT_EVALUATION a" +
                                   " inner join	PARAM_SUB_CHANNEL_DISTRIBUTION b on a.CURRENT_AGENT_LEVEL = b.SUB_CODE" +
                                   " inner join	PR_MARKET_SEGMENT c on b.MARKET_SEGMENT = c.CODE" +
                                   " where MODE = " + DDL_MODE.SelectedValue + " and " +
                                   "         a.CURRENT_AGENT_LEVEL = '" + e.Item.Cells[1].Text + "' and " +
                                   "         a.NEXT_AGENT_LEVEL = '" + e.Item.Cells[2].Text + "' ";
                                   //" inner join	PR_CHANNEL_DISTRIBUTION c on b.CD_CODE = c.CODE" +
                conn.ExecuteQuery();
                //TXT_ACTIVEPERIOD.Text = conn.QueryString.ToString();
                DataTable dt1;
                dt1 = new DataTable();
                dt1 = conn.GetDataTable().Copy();
                foreach (DataRow row in dt1.Rows)
                {
                    DDL_CHANNEL.SelectedItem.Value = row["CHANNEL"].ToString();
                    DDL_LEVELFROM.SelectedValue = row["CURRENT_AGENT_LEVEL"].ToString();
                    DDL_LEVELTO.SelectedValue = row["NEXT_AGENT_LEVEL"].ToString();
                    TXT_ANP.Text = row["MINIMUM_ANP"].ToString();
                    TXT_MEMBER.Text = row["MINIMUM_TEAM_MEMBER"].ToString();
                    TXT_PERS.Text = row["MINIMUM_PERSISTENCE"].ToString();
                    TXT_ACTIVEPERIOD.Text = row["MINIMUM_MONTH_MEMBERSHIP"].ToString();
                }

                conn.QueryString = "select a.SUB_CODE,a.DESCR,isnull(b.TEAM_MEMBER,0)TEAM_MEMBER from PARAM_SUB_CHANNEL_DISTRIBUTION a" +
                                   "     left join PARAM_AGENT_EVALUATION_LEVEL_MEMBER b on a.SUB_CODE = b.SUB_CODE " +
                                   "     left join PR_MARKET_SEGMENT c on a.MARKET_SEGMENT = c.CODE " +
                                   " where c.DESCR = '" + DDL_CHANNEL.SelectedItem.Text + "' and " +
                                   "     seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION " +
                                   "             where SUB_CODE = '" + e.Item.Cells[1].Text + "') and seq > 0 and" +
                                   "     b.CURRENT_AGENT_LEVEL = '" + e.Item.Cells[1].Text + "' and" +
                                   "     b.NEXT_AGENT_LEVEL = '" + e.Item.Cells[2].Text + "' and" +
                                   "     b.MODE = " + DDL_MODE.SelectedValue + " " +
                                   "     order by SEQ desc";
                                   //"     left join PR_CHANNEL_DISTRIBUTION c on a.CD_CODE = c.CODE " +
                conn.ExecuteQuery();
                //TXT_ACTIVEPERIOD.Text = conn.QueryString.ToString();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_LEVELMEMBER.DataSource = dt;
                DGR_LEVELMEMBER.DataBind();
                for (int i = 0; i < DGR_LEVELMEMBER.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR_LEVELMEMBER.Items[i].FindControl("TXT_TEAM_MEMBER");
                    txt.Text = DGR_LEVELMEMBER.Items[i].Cells[2].Text;
                }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_AGENT_EVALUATION_LEVEL_MEMBER where CURRENT_AGENT_LEVEL = '" + e.Item.Cells[1].Text + "' and NEXT_AGENT_LEVEL = '" + e.Item.Cells[2].Text + "' and MODE = " + DDL_MODE.SelectedValue + " ";
                conn.ExecuteNonQuery();
                conn.QueryString = "delete from PARAM_AGENT_EVALUATION where CURRENT_AGENT_LEVEL = '" + e.Item.Cells[1].Text + "' and NEXT_AGENT_LEVEL = '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Period")
            {
                LB_INFO.Text = DDL_MODE.SelectedItem.Text + " PERIOD";
                LB_CURRENT_LEVEL.Text = e.Item.Cells[1].Text;
                LB_NEXT_LEVEL.Text = e.Item.Cells[2].Text;
                LB_MODE_PERIOD.Text = DDL_MODE.SelectedItem.Text;
                LB_CURRENT_LEVEL_PERIOD.Text = e.Item.Cells[4].Text;
                LB_NEXT_LEVEL_PERIOD.Text = e.Item.Cells[5].Text;

                TBL_PERIOD.Visible = true;
                TBL_TRAINING.Visible = false;
                TBL_LVLMEMBER.Visible = false;

                FillDGRPeriod();
            }

            if (e.CommandName == "Training")
            {
                LB_INFO.Text = "REQUIRED TRAINING";
                LB_CURRENT_LEVEL.Text = e.Item.Cells[1].Text;
                LB_NEXT_LEVEL.Text = e.Item.Cells[2].Text;
                LB_MODE_TRAINING.Text = DDL_MODE.SelectedItem.Text;
                LB_CURRENT_LEVEL_TRAINING.Text = e.Item.Cells[4].Text;
                LB_NEXT_LEVEL_TRAINING.Text = e.Item.Cells[5].Text;

                TBL_PERIOD.Visible = false;
                TBL_TRAINING.Visible = true;
                TBL_LVLMEMBER.Visible = false;

                FillDGRTraining();
            }

            if (e.CommandName == "LvlMember")
            {
                LB_INFO.Text = "SALES FORCE";
                LB_CURRENT_LEVEL.Text = e.Item.Cells[1].Text;
                LB_NEXT_LEVEL.Text = e.Item.Cells[2].Text;
                LB_MODE_LVLMEMBER.Text = DDL_MODE.SelectedItem.Text;
                LB_CURRENT_LEVEL_LVLMEMBER.Text = e.Item.Cells[4].Text;
                LB_NEXT_LEVEL_LVLMEMBER.Text = e.Item.Cells[5].Text;

                TBL_PERIOD.Visible = false;
                TBL_TRAINING.Visible = false;
                TBL_LVLMEMBER.Visible = true;

                FillDGRlvlMember();
            }
        }

        protected void FillDGRPeriod()
        {
            conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_PERIOD " +
                                    "'" + LB_CURRENT_LEVEL.Text.Replace("&nbsp;", "") + "'," +
                                    "'" + LB_NEXT_LEVEL.Text.Replace("&nbsp;", "") + "'," +
                                    DDL_MODE.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERIOD.DataSource = dt;
            DGR_PERIOD.DataBind();
        }

        protected void FillDGRTraining()
        {
            conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_REQ_TRAINING " +
                                    "'" + LB_CURRENT_LEVEL.Text + "'," +
                                    "'" + LB_NEXT_LEVEL.Text + "'," +
                                    DDL_MODE.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRAINING.DataSource = dt;
            DGR_TRAINING.DataBind();
            for (int i = 0; i < DGR_TRAINING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TRAINING.Items[i].FindControl("CB");
                if (DGR_TRAINING.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void FillDGRlvlMember()
        {
            conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_LEVEL_MEMBER " +
                                    "'" + LB_CURRENT_LEVEL.Text + "'," +
                                    "'" + LB_NEXT_LEVEL.Text + "'," +
                                    DDL_MODE.SelectedValue;
            conn.ExecuteQuery();
            //TXT_ACTIVEPERIOD.Text = conn.QueryString.ToString();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LVLMEMBER.DataSource = dt;
            DGR_LVLMEMBER.DataBind();
        }

        protected void TXT_SUBMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_UPSERT " +
                                "'" + DDL_LEVELFROM.SelectedValue + "'," +
                                "'" + DDL_LEVELTO.SelectedValue + "'," +
                                DDL_MODE.SelectedValue + "," +
                                TXT_ANP.Text.Trim().Replace(",", "") + "," +
                                TXT_MEMBER.Text.Trim().Replace(",", "") + "," +
                                TXT_PERS.Text.Trim().Replace(",", "") + "," +
                                TXT_ACTIVEPERIOD.Text.Trim().Replace(",", "") + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_LEVELMEMBER.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LEVELMEMBER.Items[i].FindControl("TXT_TEAM_MEMBER");

                conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_LEVEL_MEMBER_UPSERT " +
                                "'" + DDL_LEVELFROM.SelectedValue + "'," +
                                "'" + DDL_LEVELTO.SelectedValue + "'," +
                                DDL_MODE.SelectedValue + "," +
                                "'" + DGR_LEVELMEMBER.Items[i].Cells[0].Text + "'," +
                                txt.Text.Trim() + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            ClientScript.RegisterStartupScript(this.GetType(), "myalert1", "alert('Your Data has been Saved Successfully.');", true);
            
            FillDGR();
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
            FillDGR();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_TRAINING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TRAINING.Items[i].FindControl("CB");
                if ((CheckBox)sender == cb)
                {
                    string stat = "1";
                    if (!cb.Checked)
                        stat = "0";

                    conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_REQ_TRAINING_SET " +
                                        "'" + LB_CURRENT_LEVEL.Text + "'," +
                                        "'" + LB_NEXT_LEVEL.Text + "'," +
                                        DDL_MODE.SelectedValue + "," +
                                        "'" + DGR_TRAINING.Items[i].Cells[0].Text + "'," +
                                        stat + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    return;
                }
            }

            FillDGRTraining();
        }

        protected void DGR_PERIOD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_AGENT_EVALUATION_PERIOD where " +
                                    "CURRENT_AGENT_LEVEL = '" + LB_CURRENT_LEVEL.Text + "' " +
                                    "and NEXT_AGENT_LEVEL = '" + LB_NEXT_LEVEL.Text + "' " +
                                    "and MODE = " + DDL_MODE.SelectedValue + " " +
                                    "and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRPeriod();
            }
        }

        protected void BT_PERIOD_ADD_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_PARAM_AGENT_EVALUATION_PERIOD_INSERT " +
                                        "'" + LB_CURRENT_LEVEL.Text.Replace("&nbsp;", "") + "'," +
                                        "'" + LB_NEXT_LEVEL.Text.Replace("&nbsp;", "") + "'," +
                                        DDL_MODE.SelectedValue + "," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PERIOD_START.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(TXT_PERIOD_END.Text.Trim(), "d/M/yyyy") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRPeriod();
            }
            catch { }

            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Your Data has been Saved Successfully.');", true);
        }

        protected void DDL_LEVELFROM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_MODE.SelectedValue == "0")
            {
                //conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc";
                conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc";
                conn.ExecuteQuery();
                DDL_LEVELTO.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_LEVELTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_LEVELTO.Items.Add(new ListItem("-- TERMINATE --", ""));
            }
            else 
            {
                //conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' and seq > (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ ";
                conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' and seq > (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ ";
                conn.ExecuteQuery();
                DDL_LEVELTO.Items.Clear();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_LEVELTO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            //conn.QueryString = "select SUB_CODE,DESCR,TEAM_MEMBER='0' from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc ";
            conn.QueryString = "select SUB_CODE,DESCR,TEAM_MEMBER='0' from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' and seq <= (select seq from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + DDL_LEVELFROM.SelectedValue + "') and seq > 0 order by SEQ desc ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEVELMEMBER.DataSource = dt;
            DGR_LEVELMEMBER.DataBind();
            for (int i = 0; i < DGR_LEVELMEMBER.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LEVELMEMBER.Items[i].FindControl("TXT_TEAM_MEMBER");
                txt.Text = DGR_LEVELMEMBER.Items[i].Cells[2].Text;
            }

        }

        protected void TXT_TEAM_MEMBER_TextChanged(object sender, EventArgs e)
        {
            int memberCount = 0;
            for (int i = 0; i < DGR_LEVELMEMBER.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LEVELMEMBER.Items[i].FindControl("TXT_TEAM_MEMBER");
                int parsedValue;
                if (!int.TryParse(txt.Text, out parsedValue))
                {
                    txt.Text = "0";
                }
                memberCount = memberCount + int.Parse(txt.Text);
            }

            TXT_MEMBER.Text = memberCount.ToString();
        }
    }
}