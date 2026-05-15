using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR
{
    public partial class AGENT_MOVEMENT : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        int nAgentStatus;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["code"];
                Setup();
                LoadLevel();
                LoadStructure();
                LoadMovement();
                LoadUplinerList();
            }

        }

        protected void Setup()
        {
            nAgentStatus = 0;
            try
            {
                conn.QueryString = "select ACTIVE = isnull(ACTIVE, 0) from M_AGENTS where CODE = '" + LB_ID.Text + "'";
                conn.ExecuteQuery();
                nAgentStatus = int.Parse(conn.GetFieldValue("ACTIVE").ToString());
            }
            catch { }

            if (nAgentStatus != 0)
            {
                Response.Redirect("AGENT_MOVEMENT_ACTIVE.aspx?CODE=" + LB_ID.Text);
                //TR_INPUT.Visible = false;
            }
            else
            {
                conn.QueryString = "select DEFAULT_DATE = convert(varchar(20), isnull(JOINTDATE, CREATEDATE), 103) from M_AGENTS where CODE = '" + LB_ID.Text + "'";
                conn.ExecuteQuery();
                TXT_START_DATE_LEVEL.Text = conn.GetFieldValue("DEFAULT_DATE").ToString();
                TXT_START_DATE_MOVEMENT.Text = conn.GetFieldValue("DEFAULT_DATE").ToString();
                TXT_START_DATE_STRUCTURE.Text = conn.GetFieldValue("DEFAULT_DATE").ToString();
            }

            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            FillDDLLevel();
        }

        protected void FillDDLLevel()
        {
            DDL_LEVEL.Items.Clear();
            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadLevel()
        {
            conn.QueryString = "select " +
                                "START_DATE		= convert(varchar(20), a.START_DATE, 106), " +
                                "CHANNEL_DESCR	= a.CHANNEL_DESCR, " +
                                "LEVEL_DESCR		= a.LEVEL_DESCR " +
                                "from	V_M_AGENTS_LEVEL a " +
                                "where " +
                                "a.AGENT_CODE = '" + LB_ID.Text + "' " +
                                "order by " +
                                "a.START_DATE desc";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                BT_SAVE_STRUCTURE.Enabled = false;
                BT_SET_NOUPLINER.Enabled = false;
                BT_SEARCH_UPLINER.Enabled = false;
            }
            else
            {
                BT_SAVE_STRUCTURE.Enabled = true;
                BT_SET_NOUPLINER.Enabled = true;
                BT_SEARCH_UPLINER.Enabled = true;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEVEL.DataSource = dt;
            DGR_LEVEL.DataBind();

            if (nAgentStatus != 0)
                DGR_LEVEL.Columns[DGR_LEVEL.Columns.Count - 1].Visible = false;
        }

        protected void LoadStructure()
        {
            conn.QueryString = "select " +
                                "START_DATE		= convert(varchar(20), a.START_DATE, 106), " +
                                "UPLINER_NAME	= a.UPLINER_NAME, " +
                                "LEVEL          = a.LEVEL, " +
                                "STEP_LEVEL     = convert(varchar(10), a.STEP_LEVEL) + ' LEVEL ABOVE' " +
                                "from	V_M_AGENTS_MOVEMENT a " +
                                "where " +
                                "a.AGENT_CODE = '" + LB_ID.Text + "' " +
                                "order by " +
                                "a.START_DATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_STRUCTURE.DataSource = dt;
            DGR_STRUCTURE.DataBind();

            if (nAgentStatus != 0)
                DGR_STRUCTURE.Columns[DGR_STRUCTURE.Columns.Count - 1].Visible = false;
        }

        protected void LoadMovement()
        {
            conn.QueryString = "select " +
                               "START_DATE		= convert(varchar(20), a.START_DATE, 106), " +
                               "COMPANY_NAME	= a.COMPANY_NAME, " +
                               "BRANCH_CODE     = a.BRANCH_CODE, " +
                               "BRANCH_NAME     = a.BRANCH_NAME " +
                                "from	V_M_AGENTS_AGENCY a " +
                               "where " +
                               "a.AGENT_CODE = '" + LB_ID.Text + "' " +
                               "order by " +
                               "a.START_DATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MOVEMENT.DataSource = dt;
            DGR_MOVEMENT.DataBind();

            if (nAgentStatus != 0)
                DGR_MOVEMENT.Columns[DGR_MOVEMENT.Columns.Count - 1].Visible = false;
        }

        protected void LoadUplinerList()
        {
            conn.QueryString = "exec SP_M_AGENT_UPLINER '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UPLINER_LIST.DataSource = dt;
            DGR_UPLINER_LIST.DataBind();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
        }

        protected void DGR_LEVEL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_LEVEL where CODE = '" + LB_ID.Text + "' and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadLevel();
            }
        }

        protected void DGR_STRUCTURE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_MOVEMENT where CODE = '" + LB_ID.Text + "' and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadStructure();
            }
        }

        protected void DGR_MOVEMENT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_AGENCY where CODE = '" + LB_ID.Text + "' and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadMovement();
            }
        }

        protected void BT_SEARCH_UPLINER_Click(object sender, EventArgs e)
        {
            ShowUplinerSearch();
        }

        protected void FillDGRUpliner()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.FULLNAME, " +
                                "a.SUBCD_DESCR, " +
                                "a.AGENCY_NAME " +
                                "from		V_M_AGENTS a " +
                                "inner join	V_M_AGENTS b on b.CODE = '" + LB_ID.Text + "' and a.CD = b.CD and a.CODE <> b.CODE and isnull(a.AGENCY_CODE, '') = isnull(b.AGENCY_CODE, '') " +
                                "where " +
                                "a.FULLNAME like '%" + TXT_UPLINER.Text.Trim() + "%' " +
                                "and a.SUBCD_DESCR like '%" + TXT_LEVEL.Text.Trim() + "%' " +
                                "order by " +
                                "2,3";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UPLINER.DataSource = dt;
            DGR_UPLINER.DataBind();

            LB_UPLINER_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_UPLINER.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_UPLINER.Items[i].FindControl("LB_UPLINER_CODE");
                lb.Text = DGR_UPLINER.Items[i].Cells[1].Text;
            }
        }

        protected void BT_SEARCH_COMPANY_Click(object sender, EventArgs e)
        {
            ShowAgencySearch();
        }

        protected void FillDGRAgency()
        {
            conn.QueryString = "select " +
                                "BRANCH_CODE		= br.BRANCH_CODE, " +
                                "BRANCH_NAME		= br.NAMA_CABANG, " +
                                "COMPANY_NAME	    = b.COMPANY_NAME " +
                                "from		V_LINK_CB_BRANCH br  " +
                                "inner join	V_LINK_CB_COMPANY b on br.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "b.COMPANY_NAME like '%" + TXT_AGENCYNAME.Text.Trim() + "%' " +
                                "order by 3,2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY.DataSource = dt;
            DGR_AGENCY.DataBind();

            LB_AGENCY_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_AGENCY.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_AGENCY.Items[i].FindControl("LB_BRANCH_CODE");
                lb.Text = DGR_AGENCY.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_UPLINER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_UPLINER.CurrentPageIndex = e.NewPageIndex;
            FillDGRUpliner();
        }

        protected void DGR_UPLINER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_UPLINER.Text = e.Item.Cells[1].Text;
                LB_UPLINER_NAME.Text = e.Item.Cells[2].Text + "<BR>";
                BT_SAVE_STRUCTURE.Visible = true;
                TBL_UPLINER.Visible = false;
            }
        }

        protected void BT_UPLINER_SEARCH_Click(object sender, EventArgs e)
        {
            ShowUplinerSearch();
        }

        protected void DGR_AGENCY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_AGENCY.CurrentPageIndex = e.NewPageIndex;
            FillDGRAgency();
        }

        protected void DGR_AGENCY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_BRANCH_CODE.Text = e.Item.Cells[1].Text;
                LB_BRANCH_NAME.Text = e.Item.Cells[2].Text + "<BR>";
                BT_SAVE_MOVEMENT.Visible = true;
                TBL_AGENCY.Visible = false;
            }
        }

        protected void BT_AGENCY_SEARCH_Click(object sender, EventArgs e)
        {
            ShowAgencySearch();
        }


        protected void ShowUplinerSearch()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            TBL_AGENCY.Visible = false;
            TBL_UPLINER.Visible = true;
            DGR_UPLINER.CurrentPageIndex = 0;
            FillDGRUpliner();
        }

        protected void ShowAgencySearch()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            TBL_AGENCY.Visible = true;
            TBL_UPLINER.Visible = false;
            DGR_AGENCY.CurrentPageIndex = 0;
            FillDGRAgency();
        }

        protected void BT_SAVE_LEVEL_Click(object sender, EventArgs e)
        {
            if (TXT_START_DATE_LEVEL.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_M_AGENT_LEVEL_INSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE_LEVEL.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + DDL_LEVEL.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            LoadLevel();
        }

        protected void BT_SAVE_STRUCTURE_Click(object sender, EventArgs e)
        {
            if (TXT_START_DATE_STRUCTURE.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_M_AGENT_MOVEMENT_INSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE_STRUCTURE.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + LB_UPLINER.Text + "'," +
                                "'" + DDL_UPLINER_LAYER.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "1";
            conn.ExecuteNonQuery();
            LoadStructure();
        }

        protected void BT_SAVE_MOVEMENT_Click(object sender, EventArgs e)
        {
            if (TXT_START_DATE_MOVEMENT.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_M_AGENT_AGENCY_INSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE_MOVEMENT.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + LB_BRANCH_CODE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "1";
            conn.ExecuteNonQuery();
            LB_UPLINER.Text = "";
            LB_UPLINER_NAME.Text = "";
            BT_SAVE_STRUCTURE.Visible = false;
            //LoadStructure();
            Response.Redirect("AGENT_MOVEMENT.ASPX?code=" + LB_ID.Text);
        }

        protected void BT_SET_NOUPLINER_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_M_AGENT_MOVEMENT_INSERT " +
                                "'" + LB_ID.Text + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE_LEVEL.Text.Trim(), "d/M/yyyy") + "'," +
                                "''," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "1";
            conn.ExecuteNonQuery();
            LB_BRANCH_CODE.Text = "";
            LB_BRANCH_NAME.Text = "";
            BT_SAVE_MOVEMENT.Visible = false;
            //LoadMovement();
            Response.Redirect("AGENT_MOVEMENT.ASPX?code=" + LB_ID.Text);
        }
    }
}