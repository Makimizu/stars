using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class AGENT_PROMOTION_DEMOTION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";


            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_AGENCY.Text.Trim() != "")
                where = where + " and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.MARKET_SEGMENT = " + DDL_CHANNEL.SelectedValue + " ";


            string ScaleSign = ">", ShortLevel = "ASC";
            if (LB_MODE.Text == "6")
            {
                ScaleSign = "<";
                ShortLevel = "DESC";
            }

            conn.QueryString = "select " +
                                "a.CODE, " +
                                "FULLNAME = '<table style=\"border-spacing:0px;width:100%; font-size: xx-small;text-wrap:normal;\">" +
                                "           <tr><td style=\"width:80px;\">FULLNAME</td><td>' + isnull(a.FULLNAME, '') + '</td></tr>" +
                                "           <tr><td>DOB</td><td>' + convert(varchar(20),a.DOB,106) + '</td></tr>" +
                                "           <tr><td>UPLINER</td><td>' + isnull(a.UPLINER_NAME, '') + '</td></tr>" +
                                "           <tr><td>BRANCH</td><td>' + isnull(a.BRANCH_DESCR, '') + '</td></tr>" +
                                "           </table>', " +
                                "PERFORMANCE	= '<table style=\"border-spacing:0px;width:100%; font-size: xx-small;text-wrap:normal;\"> " +
                                "                   <tr><td style=\"width:50px;\">YEAR</td><td>' + convert(varchar(10), YEAR(dateadd(month, -1, GETDATE()))) + '</td></tr> " +
                                "                   <tr><td>CASES</td><td>' + convert(varchar(10), isnull(b.CASES, 0)) + '</td></tr> " +
                                "                   <tr><td>ANP</td><td>' + replace(convert(varchar(100), convert(money, isnull(b.ANP_BASIC, 0) + isnull(b.ANP_DL1, 0) + isnull(b.ANP_DL2, 0) + isnull(b.ANP_DL3, 0)), 1), '.00', '') + '</td></tr> " +
                                "                   </table>', " +
                                "LEVEL          = a.SUBCD, " +
                                "LEVEL_DESCR    = isnull(a.SUBCD_DESCR, ''), " +
                                "SQL_LEVEL	= 'select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = ''' + isnull(a.CD, '') + ''' and SUB_CODE <> ''' + isnull(a.SUBCD, '') + ''' and SEQ>0 and SEQ " + ScaleSign + " ' + convert(varchar(10), aa.SEQ) + ' order by SEQ " + ShortLevel + "' " +
                                "from       V_M_AGENTS a " +
                                "inner join PARAM_SUB_CHANNEL_DISTRIBUTION aa on a.SUBCD = aa.SUB_CODE and aa.SEQ > 0 " +
                                "left join	M_AGENTS_ANP b on a.CODE = b.CODE and b.YEAR = YEAR(dateadd(month, -1, GETDATE())) " +
                                "where " +
                                "a.CODE not in (select CODE from M_AGENT_PROMOTION_DEMOTION where APPROVEBY is null) and a.ACTIVE = 1 " + where +
                                "order by " +
                                "a.FULLNAME";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                Label lbLEVELFROM = (Label)DGR.Items[i].FindControl("LB_LEVELFROM");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_LEVEL");

                lb.Text = DGR.Items[i].Cells[1].Text;
                lbLEVELFROM.Text = DGR.Items[i].Cells[4].Text;

                conn.QueryString = DGR.Items[i].Cells[2].Text;
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Request")
            {
                LB_TRNS_AGENTCODE.Text = e.Item.Cells[1].Text;
                LB_TRNS_AGENT.Text = e.Item.Cells[5].Text;

                DV_MAIN.Visible = false;
                DV_TRANSFER.Visible = true;
            }

            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Approve")
            {
                //try
                //{
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    TextBox txtREASON = (TextBox)DGR.Items[i].FindControl("TXT_REASON");
                    Label lbAGENTCODE = (Label)DGR.Items[i].FindControl("LB_AGENTCODE");
                    DropDownList ddlLEVEL = (DropDownList)DGR.Items[i].FindControl("DDL_LEVEL");

                    bool bOK = true;
                    //if (lbAGENTCODE.Text == "")
                    //    bOK = false;

                    if (cb.Checked && txtREASON.Text.Trim().Replace("'", "") != "" && bOK)
                    {
                        conn.QueryString = "exec SP_M_AGENT_PROMOTION_DEMOTION " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'" + LB_MODE.Text + "'," +
                                            "'" + DGR.Items[i].Cells[3].Text + "'," +
                                            "'" + ddlLEVEL.SelectedValue + "'," +
                                            "'" + txtREASON.Text.Trim().Replace("'", "") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }

                }

                DGR.CurrentPageIndex = 0;
                FillDGR();
                //}
                //catch { }
            }

        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Button bt = (Button)e.Item.FindControl("BT_A");
                switch (LB_MODE.Text)
                {
                    case "6": bt.Text = "DEMOTE"; bt.BackColor = System.Drawing.Color.Red; break;
                    case "5": bt.Text = "PROMOTE"; bt.BackColor = System.Drawing.Color.Blue; break;
                }
            }
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            DV_MAIN.Visible = true;
            DV_TRANSFER.Visible = false;
        }

        protected void FillDGRTransferSearch()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.FULLNAME, " +
                                "a.SUBCD_DESCR, " +
                                "a.AGENCY_NAME " +
                                "from		V_M_AGENTS a " +
                                "inner join	V_M_AGENTS b on b.CODE = '" + LB_TRNS_AGENTCODE.Text + "' and a.CD = b.CD and a.CODE <> b.CODE and isnull(a.AGENCY_CODE, '') = isnull(b.AGENCY_CODE, '') " +
                                "where " +
                                "a.FULLNAME like '%" + TXT_TRNS_NAME.Text.Trim() + "%' " +
                                "and a.SUBCD_DESCR like '%" + TXT_TRNS_LEVEL.Text.Trim() + "%' " +
                                "order by " +
                                "2,3";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRNS.DataSource = dt;
            DGR_TRNS.DataBind();

            LB_TRNS_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_TRNS.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_TRNS.Items[i].FindControl("LB_TRNS_CODE");
                lb.Text = DGR_TRNS.Items[i].Cells[1].Text;
            }
        }

        protected void BT_UPLINER_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_TRNS.CurrentPageIndex = 0;
            FillDGRTransferSearch();
        }

        protected void DGR_UPLINER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_TRNS.CurrentPageIndex = e.NewPageIndex;
            FillDGRTransferSearch();
        }

        protected void DGR_UPLINER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    if (LB_TRNS_AGENTCODE.Text == DGR.Items[i].Cells[1].Text)
                    {
                        Label lbAGENTCODE = (Label)DGR.Items[i].FindControl("LB_AGENTCODE");
                        Label lbAGENTNAME = (Label)DGR.Items[i].FindControl("LB_AGENTNAME");

                        lbAGENTCODE.Text = e.Item.Cells[1].Text;
                        lbAGENTNAME.Text = " - " + e.Item.Cells[2].Text;
                        break;
                    }
                }

                DV_MAIN.Visible = true;
                DV_TRANSFER.Visible = false;
            }
        }
    }
}