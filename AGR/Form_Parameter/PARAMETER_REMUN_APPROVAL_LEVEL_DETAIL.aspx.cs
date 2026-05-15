using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_REMUN_APPROVAL_LEVEL_DETAIL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }

        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "CODE, " +
                                "DESCR " +
                                "from        PR_MARKET_SEGMENT a " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MARKET_SEGMENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLAmountRange();
            FillDGRLevel();
        }

        protected void FillDDLAmountRange()
        {
            conn.QueryString = "select " +
                                "CODE = a.MIN_AMOUNT, " +
                                "DESCR = REPLACE(convert(varchar(100), convert(money, a.MIN_AMOUNT), 1), '.00', '') + ' - ' + REPLACE(convert(varchar(100), convert(money, a.MAX_AMOUNT), 1), '.00', '') " +
                                "from PARAM_REMUN_APPROVAL_LEVEL a " +
                                "where " +
                                "a.MARKET_SEGMENT = '" + DDL_MARKET_SEGMENT.SelectedValue + "' " +
                                "order by " +
                                "a.MIN_AMOUNT ";
            conn.ExecuteQuery();
            DDL_AMOUNT_RANGE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AMOUNT_RANGE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDGRLevel();
        }

        protected void FillDGRLevel()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_APPROVAL_LEVEL " +
                "'" + DDL_MARKET_SEGMENT.SelectedValue + "'," +
                "'" + DDL_AMOUNT_RANGE.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEVEL.DataSource = dt;
            DGR_LEVEL.DataBind();

            for (int i = 0; i < DGR_LEVEL.Items.Count; i++)
            {
                LinkButton lbtLevel = (LinkButton)DGR_LEVEL.Items[i].FindControl("LBT_LEVEL");
                lbtLevel.Text = "APPROVAL " + DGR_LEVEL.Items[i].Cells[0].Text;
            }
        }


        protected void DDL_MARKET_SEGMENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            TR_TITLE.Visible = false;
            TR_CONTENT.Visible = false;
            FillDDLAmountRange();
            FillDGRLevel();
        }

        protected void DDL_AMOUNT_RANGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            TR_TITLE.Visible = false;
            TR_CONTENT.Visible = false;
            FillDGRLevel();
        }

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "insert into PARAM_REMUN_APPROVAL_LEVEL_DETAIL select " +
                                    "MARKET_SEGMENT = '" + DDL_MARKET_SEGMENT.SelectedValue + "'," +
                                    "MIN_AMOUNT = '" + DDL_AMOUNT_RANGE.SelectedValue + "'," +
                                    "SEQ = '" + LB_LEVEL_ID.Text.Trim() + "'," +
                                    "USERID = '" + e.Item.Cells[0].Text + "'," + 
                                    "USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(),"UserID") + "',"+
                                    "USERDATE = GETDATE()";
                conn.ExecuteQuery();

                FillDGRUnselected();
                FillDGRSelected();
                FillDGRLevel();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_REMUN_APPROVAL_LEVEL_DETAIL where " +
                                    "MARKET_SEGMENT = '" + DDL_MARKET_SEGMENT.SelectedValue + "' " +
                                    "and MIN_AMOUNT = '" + DDL_AMOUNT_RANGE.SelectedValue + "' " +
                                    "and SEQ = '" + LB_LEVEL_ID.Text.Trim() + "' " +
                                    "and USERID = '" + e.Item.Cells[0].Text + "'";
                    
                conn.ExecuteQuery();

                FillDGRUnselected();
                FillDGRSelected();
                FillDGRLevel();
            }
        }

        protected void DGR_LEVEL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                TR_TITLE.Visible = true;
                TR_CONTENT.Visible = true;
                LB_LEVEL_ID.Text = e.Item.Cells[0].Text;

                LB_UNSELECTED.Text = "UNSELECTED APPROVAL " + e.Item.Cells[0].Text;
                LB_SELECTED.Text = "SELECTED APPROVAL " + e.Item.Cells[0].Text;

                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_APPROVAL_LEVEL_USER " +
                                "'" + DDL_MARKET_SEGMENT.SelectedValue + "'," +
                                "'" + DDL_AMOUNT_RANGE.SelectedValue + "'," +
                                "'" + LB_LEVEL_ID.Text.Trim() + "'," +
                                "0," +
                                "'" + TXT_SEARCH.Text.Trim() + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNSELECTED.DataSource = dt;
            DGR_UNSELECTED.DataBind();
            for (int i = 0; i < DGR_UNSELECTED.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_UNSELECTED.Items[i].FindControl("LBT_CODE");
                lbt.Text = DGR_UNSELECTED.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_APPROVAL_LEVEL_USER " +
                                "'" + DDL_MARKET_SEGMENT.SelectedValue + "'," +
                                "'" + DDL_AMOUNT_RANGE.SelectedValue + "'," +
                                "'" + LB_LEVEL_ID.Text.Trim() + "'," +
                                "1," +
                                "''";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_SELECTED.Items[i].FindControl("LBT_CODE2");
                lbt.Text = DGR_SELECTED.Items[i].Cells[1].Text;
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGRUnselected();
        }
    }
}