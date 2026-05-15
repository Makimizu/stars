using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class AGENT_TERMINATE_REACTIVATE : System.Web.UI.Page
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
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
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

            switch (LB_MODE.Text)
            {
                case "1": where = where + "a.CODE not in (select CODE from M_AGENT_TERMINATE_REACTIVATE where APPROVEBY is null) and a.ACTIVE = 0 "; break;
                case "2": where = where + "a.CODE not in (select CODE from M_AGENT_TERMINATE_REACTIVATE where APPROVEBY is null) and a.ACTIVE = 1 "; break;
                case "4": where = where + "a.CODE not in (select CODE from M_AGENT_TRANSFER_CASE where APPROVEBY is null) "; break;
            }

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




            conn.QueryString = "select " +
                                "a.CODE, " +
                                "FULLNAME = '<table style=\"border-spacing:0px;width:100%; font-size: xx-small;text-wrap:normal;\">" +
                                "           <tr><td style=\"width:80px;\">FULLNAME</td><td>' + isnull(a.FULLNAME, '') + '</td></tr>" +
                                "           <tr><td>DOB</td><td>' + convert(varchar(20),a.DOB,106) + '</td></tr>" +
                                "           <tr><td>LEVEL</td><td>' + isnull(a.SUBCD_DESCR, '') + '</td></tr>" +
                                "           <tr><td>UPLINER</td><td>' + isnull(a.UPLINER_NAME, '') + '</td></tr>" +
                                "           <tr><td>BRANCH</td><td>' + isnull(a.BRANCH_DESCR, '') + '</td></tr>" +
                                "           </table>', " +
                                "PERFORMANCE	= '<table style=\"border-spacing:0px;width:100%; font-size: xx-small;text-wrap:normal;\"> " +
                                "                   <tr><td style=\"width:50px;\">YEAR</td><td>' + convert(varchar(10), YEAR(dateadd(month, -1, GETDATE()))) + '</td></tr> " +
                                "                   <tr><td>CASES</td><td>' + convert(varchar(10), isnull(b.CASES, 0)) + '</td></tr> " +
                                "                   <tr><td>ANP</td><td>' + replace(convert(varchar(100), convert(money, isnull(b.ANP_BASIC, 0) + isnull(b.ANP_DL1, 0) + isnull(b.ANP_DL2, 0) + isnull(b.ANP_DL3, 0)), 1), '.00', '') + '</td></tr> " +
                                "                   </table>' " +
                                "from V_M_AGENTS a " +
                                "left join	M_AGENTS_ANP b on a.CODE = b.CODE and b.YEAR = YEAR(dateadd(month, -1, GETDATE())) " +
                                "where " + where +
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
                System.Web.UI.HtmlControls.HtmlTableRow row = (System.Web.UI.HtmlControls.HtmlTableRow)DGR.Items[i].FindControl("TR_TRNS");
                lb.Text = DGR.Items[i].Cells[1].Text;
                if (LB_MODE.Text == "1")
                    row.Visible = false;
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
            if (e.CommandName == "Transfer")
            {
                LB_TRNS_AGENTCODE.Text = e.Item.Cells[1].Text;
                LB_TRNS_AGENT.Text = e.Item.Cells[2].Text;

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
                    Label lbTRANSFERAGENTCODE = (Label)DGR.Items[i].FindControl("LB_AGENTCODE");

                    bool bOK = true;
                    if ((LB_MODE.Text == "2" || LB_MODE.Text == "4") && lbTRANSFERAGENTCODE.Text == "")
                        bOK = false;

                    if (LB_MODE.Text == "1" && cb.Checked && txtREASON.Text.Trim().Replace("'", "") != "" && bOK)
                    {
                        conn.QueryString = "exec SP_M_AGENT_TERMINATE_REACTIVATE " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            LB_MODE.Text + "," +
                                            "'" + txtREASON.Text.Trim().Replace("'", "") + "'," +
                                            "'" + lbTRANSFERAGENTCODE.Text.Trim().Replace("'", "") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }

                    if (LB_MODE.Text == "2" && cb.Checked && txtREASON.Text.Trim().Replace("'", "") != "" && bOK)
                    {
                        conn.QueryString = "exec SP_M_AGENT_TERMINATE_REACTIVATE " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            LB_MODE.Text + "," +
                                            "'" + txtREASON.Text.Trim().Replace("'", "") + "'," +
                                            "'" + lbTRANSFERAGENTCODE.Text.Trim().Replace("'", "") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }

                    if (LB_MODE.Text == "4" && cb.Checked && txtREASON.Text.Trim().Replace("'", "") != "" && bOK)
                    {
                        conn.QueryString = "exec SP_M_AGENT_TRANSFER_CASE " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'" + txtREASON.Text.Trim().Replace("'", "") + "'," +
                                            "'" + lbTRANSFERAGENTCODE.Text.Trim().Replace("'", "") + "'," +
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
                    case "1": bt.Text = "RE-ACTIVATE"; bt.BackColor = System.Drawing.Color.Blue; break;
                    case "2": bt.Text = "TERMINATE"; bt.BackColor = System.Drawing.Color.Red; break;
                    case "4": bt.Text = "TRANSFER CASE"; bt.BackColor = System.Drawing.Color.Green; break;
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