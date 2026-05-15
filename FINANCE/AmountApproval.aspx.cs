using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Parameter
{
    public partial class AmountApproval : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGRRange();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from        PARAM_TIPE_SETTLEMENT a " +
                                "inner join  SECURITY.dbo.M_APPS b on a.APP_ID = b.CODE collate database_default " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLProcess();
        }

        protected void DDL_PROCESS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRRange();
        }

        protected void FillDGRRange()
        {
            TBL_DETAIL.Visible = false;
            conn.QueryString = "select " +
                                "b.CODE, " +
                                "DESCR          = UPPER(b.DESCR), " +
                                "START_AMOUNT   = replace(convert(varchar(100), convert(money,a.MIN_AMOUNT),1), '.00',''), " +
                                "END_AMOUNT     = replace(convert(varchar(100), convert(money,a.MAX_AMOUNT),1), '.00','') " +
                                "from           PARAM_SETTLEMENT_APPROVAL_LEVEL a " +
                                "inner join     PARAM_TIPE_SETTLEMENT b on a.APP_ID = b.APP_ID and a.CODE = b.CODE " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                "and a.CODE = '" + DDL_PROCESS.SelectedValue + "' " +
                                "order by " +
                                "a.MIN_AMOUNT";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRQUERY.DataSource = dt;
            DGRQUERY.DataBind();

            for (int i = 0; i < DGRQUERY.Items.Count; i++)
            {
                Button btDEL = (Button)DGRQUERY.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL_INSERT " +
                                    "'" + DDL_APP.SelectedValue + "'," +
                                    "'" + DDL_PROCESS.SelectedValue + "'," +
                                    TXT_BOTTOM.Text.Trim().Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            FillDGRRange();
        }

        protected void DGRQUERY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_SETTLEMENT_APPROVAL_LEVEL where " +
                                        "APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                        "and CODE   = '" + e.Item.Cells[0].Text + "' " +
                                        "and START_AMOUNT = " + e.Item.Cells[2].Text.Replace(",", "");
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRRange();
            }

            if (e.CommandName == "Detail")
            {
                LB_PROCESS.Text = e.Item.Cells[0].Text;
                LB_DESCR.Text = e.Item.Cells[1].Text;
                LB_BOTTOM.Text = e.Item.Cells[2].Text;
                LB_UPPER.Text = e.Item.Cells[3].Text;

                TBL_DETAIL.Visible = true;
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "select " +
                                "CODE		= a.CODE, " +
                                "FULLNAME   = LEFT(UPPER(LTRIM(isnull(a.FRONT_NAME, '')) + RTRIM(' ' + isnull(a.MID_NAME, '')) + RTRIM(' ' + isnull(a.LAST_NAME, ''))), 50), " +
                                "ROLE_DESCR = UPPER(b.DESCR) " +
                                "from       SECURITY.dbo.M_USERS a " +
                                "inner join SECURITY.dbo.M_ROLES b on a.ROLE_CODE = b.CODE " +
                                "where " +
                                "a.CODE collate database_default not in (select USERID from PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL where APP_ID = '" + DDL_APP.SelectedValue + "' and CODE='" + LB_PROCESS.Text + "' and MIN_AMOUNT=" + LB_BOTTOM.Text.Replace(",", "") + ") " +
                                "and a.ROLE_CODE not in ('99','100') " +
                                "order by 2";
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
            conn.QueryString = "select " +
                                "CODE		= a.CODE, " +
                                "FULLNAME   = LEFT(UPPER(LTRIM(isnull(a.FRONT_NAME, '')) + RTRIM(' ' + isnull(a.MID_NAME, '')) + RTRIM(' ' + isnull(a.LAST_NAME, ''))), 50), " +
                                "ROLE_DESCR = UPPER(b.DESCR) " +
                                "from       SECURITY.dbo.M_USERS a " +
                                "inner join SECURITY.dbo.M_ROLES b on a.ROLE_CODE = b.CODE " +
                                "where " +
                                "a.CODE collate database_default in (select USERID from PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL where APP_ID = '" + DDL_APP.SelectedValue + "' and CODE='" + LB_PROCESS.Text + "' and MIN_AMOUNT=" + LB_BOTTOM.Text.Replace(",", "") + ") " +
                                "and a.ROLE_CODE not in ('99','100') " +
                                "order by 2";
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

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    conn.QueryString = "insert into PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL select " +
                                        "'" + DDL_APP.SelectedValue + "'," +
                                        "'" + DDL_PROCESS.SelectedValue + "'," +
                                        LB_BOTTOM.Text.Replace(",", "") + "," +
                                        "'" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete          PARAM_SETTLEMENT_APPROVAL_LEVEL_DETAIL where " +
                                        "APP_ID         = '" + DDL_APP.SelectedValue + "' " +
                                        "and CODE       = '" + LB_PROCESS.Text + "' " +
                                        "and MIN_AMOUNT = " + LB_BOTTOM.Text.Replace(",", "") + " " +
                                        "and USERID     = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDDLProcess()
        {
            DDL_PROCESS.Items.Clear();
            conn.QueryString = "select CODE,DESCR = UPPER(DESCR) from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by DESCR";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROCESS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLProcess();
            FillDGRRange();
        }
    }
}