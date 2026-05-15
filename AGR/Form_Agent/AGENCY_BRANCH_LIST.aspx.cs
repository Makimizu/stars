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
    public partial class AGENCY_BRANCH_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_BRANCH.Text.Trim() != "")
                where = where + " and a.NAMA_CABANG like '%" + TXT_BRANCH.Text.Trim() + "%' ";

            if (TXT_AGENT.Text.Trim() != "")
                where = where + " and a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "BRANCH_CODE    = BRANCH_CODE, " +
                                "COMPANY_NAME	= '<a href=\"AGENCY_CORPORATE_FRAME.aspx?ID=' + a.COMPANY_CODE + '\">' + COMPANY_NAME + '</a>', " +
                                "BRANCH			= NAMA_CABANG, " +
                                "BRANCH_AGENT	= (case when a.AGENT_CODE is null then ''  " +
                                "                        else '<a href=\"AGENT_FRAME.aspx?AGENTCODE=' + AGENT_CODE + '\">' +AGENT_CODE + ' - ' + AGENT_NAME + '</a>' " +
                                "                        end), " +
                                "CHANNEL			= CHANNEL, " +
                                "LEVEL			= LEVEL, " +
                                "AGENTS			= isnull(a.AGENTS, 0) " +
                                "from		V_AGENCY_RELATION_OFFICER a " +
                                "where " +
                                "1=1 " + where +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();
            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DV_MAIN.Visible = false;
                DV_AGENTS.Visible = true;

                LB_BRANCHCODE.Text = e.Item.Cells[0].Text;
                LB_COMPANY.Text = e.Item.Cells[1].Text;
                LB_BRANCH.Text = e.Item.Cells[2].Text;


                conn.QueryString = "select distinct " +
                                    "CODE	= b.SUBCD, " +
                                    "DESCR	= b.CD_DESCR + ' - ' + b.SUBCD_DESCR " +
                                    "from		M_AGENT_AGENCY a " +
                                    "inner join	V_M_AGENTS b on a.CODE = b.CODE " +
                                    //"where " +
                                    //"a.BRANCH_CODE = '" + LB_BRANCHCODE.Text + "' " +
                                    "order by 2";
                conn.ExecuteQuery();
                DDL_LEADER_LEVEL.Items.Clear();
                DDL_SEARCH_AGENT.Items.Clear();
                DDL_SEARCH_AGENT.Items.Add(new ListItem("", ""));
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_LEADER_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                    DDL_SEARCH_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                FillDDLAgents();
                FillDGRLeader();
                DGR_AGENTS.CurrentPageIndex = 0;
                FillDGRAgents();
            }
        }

        protected void FillDDLAgents()
        {
            DDL_LEADER_AGENT.Items.Clear();

            conn.QueryString = "select " +
                                "a.CODE, " +
                                "b.CODE+ ' - ' + b.FULLNAME " +
                                "from		M_AGENT_AGENCY a  " +
                                "inner join	V_M_AGENTS b on a.CODE = b.CODE and b.SUBCD = '" + DDL_LEADER_LEVEL.SelectedValue + "' " +
                                //"where " +
                                //"a.BRANCH_CODE = '" + LB_BRANCHCODE.Text + "' " +
                                "order by b.FULLNAME";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LEADER_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGRLeader()
        {
            conn.QueryString = "exec SP_AGENCY_RELATION_OFFICER '" + LB_BRANCHCODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEADER.DataSource = dt;
            DGR_LEADER.DataBind();

            for (int i = 0; i < DGR_LEADER.Items.Count; i++)
            {
                Button btDEL = (Button)DGR_LEADER.Items[i].FindControl("BT_X");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                if (DGR_LEADER.Items[i].Cells[1].Text == "1")
                    DGR_LEADER.Items[i].BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void FillDGRAgents()
        {
            conn.QueryString = "exec SP_AGENCY_AGENTS " +
                                "'" + LB_BRANCHCODE.Text + "'," +
                                "'" + TXT_SEARCH_AGENT.Text.Trim() + "'," +
                                "'" + DDL_SEARCH_AGENT.SelectedValue + "'";
            conn.ExecuteQuery();

            LB_AGENT_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENTS.DataSource = dt;
            DGR_AGENTS.DataBind();
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            DV_MAIN.Visible = true;
            DV_AGENTS.Visible = false;
        }

        protected void DGR_LEADER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_AGENCY_RELATION_OFFICER_INSDEL " +
                                "'" + LB_BRANCHCODE.Text + "'," +
                                "'" + e.Item.Cells[2].Text + "'," +
                                "'" + e.Item.Cells[0].Text + "'," +
                                "0," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRLeader();
            }
        }

        protected void DGR_AGENTS_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_AGENTS.CurrentPageIndex = e.NewPageIndex;
            FillDGRAgents();
        }

        protected void BT_LEADER_ADD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_AGENCY_RELATION_OFFICER_INSDEL " +
                                "'" + LB_BRANCHCODE.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + DDL_LEADER_AGENT.SelectedValue + "'," +
                                "1," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGRLeader();
        }

        protected void DDL_SEARCH_AGENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR_AGENTS.CurrentPageIndex = 0;
            FillDGRAgents();
        }

        protected void TXT_SEARCH_AGENT_TextChanged(object sender, EventArgs e)
        {
            DGR_AGENTS.CurrentPageIndex = 0;
            FillDGRAgents();
        }

        protected void DDL_LEADER_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLAgents();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string where = "";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_BRANCH.Text.Trim() != "")
                where = where + " and a.NAMA_CABANG like '%" + TXT_BRANCH.Text.Trim() + "%' ";

            if (TXT_AGENT.Text.Trim() != "")
                where = where + " and a.AGENT_NAME like '%" + TXT_AGENT.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                //"BRANCH_CODE    = BRANCH_CODE, " +
                                //"COMPANY_NAME	= '<a href=\"AGENCY_CORPORATE_FRAME.aspx?ID=' + a.COMPANY_CODE + '\">' + COMPANY_NAME + '</a>', " +
                                //"BRANCH			= NAMA_CABANG, " +
                                //"BRANCH_AGENT	= (case when a.AGENT_CODE is null then ''  " +
                                //"                        else '<a href=\"AGENT_FRAME.aspx?AGENTCODE=' + AGENT_CODE + '\">' +AGENT_CODE + ' - ' + AGENT_NAME + '</a>' " +
                                //"                        end), " +
                                //"CHANNEL			= CHANNEL, " +
                                //"LEVEL			= LEVEL, " +
                                //"AGENTS			= isnull(a.AGENTS, 0) " +
                                //"from		V_AGENCY_RELATION_OFFICER a " +
                                "	b.BRANCH_CODE, " +
	                            "    a.NAMA_CABANG, " +
	                            "    BRANCH_ADDRESS = b.BRANCH_ADDRESS1, " +
	                            "    b.KOTAMADYA, " +
	                            "    c.PROVINCE_NAME, " +
	                            "    b.BRANCH_ZIP, " +
	                            "    b.BRANCH_PHONE, " +
	                            "    b.BRANCH_FAX, " +
	                            "    b.BRANCH_EMAIL, " +
	                            "    BRANCH_ACCNO = d.ACCNO, " +
	                            "    BRANCH_ACCNAME = d.ACCNAME, " +
	                            "    BRANCH_BANK_NAME = d.BANK_NAME, " +
	                            "    b.CREATEBY,b.CREATEDATE,b.LASTCHANGEBY,b.LASTCHANGEDATE " +
                                "from V_LINK_CB_BRANCH b " +
	                            "    left join V_AGENCY_RELATION_OFFICER a on a.BRANCH_CODE = b.BRANCH_CODE " +
	                            "    left join (select distinct(PROVINCE_CODE),PROVINCE_NAME from V_LINK_CB_PARAM_CITY) c on b.PROPINSI = c.PROVINCE_CODE " +
                                "    left join (select a.*,b.DESCR as BANK_NAME from V_LINK_CB_BRANCH_BANK_ACCOUNT a left join V_LINK_CB_PR_BANK b on a.ACCBANK = b.CODE) d on b.BRANCH_CODE = d.BRANCH_CODE and d.TIPE = 3 and d.STAT is not null " +
                                "where " +
                                "1=1 " + where +
                                "order by a.COMPANY_NAME";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "AGENCY_BRANCH_LIST", true);
        }
    }
}