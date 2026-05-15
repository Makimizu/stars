using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GLIFE.Form_Parameter
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
            conn.QueryString = "select CODE,DESCR from PR_PROCESS_TYPE order by DESCR";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROCESS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
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
                                "b.DESCR, " +
                                "START_AMOUNT = replace(convert(varchar(100), convert(money,a.START_AMOUNT),1), '.00',''), " +
                                "END_AMOUNT = replace(convert(varchar(100), convert(money,a.END_AMOUNT),1), '.00','') " +
                                "from PARAM_APPROVAL_RANGE a " +
                                "inner join PR_PROCESS_TYPE b on a.CODE = b.CODE " +
                                "where " +
                                "a.CODE = '" + DDL_PROCESS.SelectedValue + "' " +
                                "order by " +
                                "a.START_AMOUNT";
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
                conn.QueryString = "exec SP_PARAM_APPROVAL_RANGE_INSERT " +
                                    "'" + DDL_PROCESS.SelectedValue + "'," +
                                    TXT_BOTTOM.Text.Trim().Replace(",", "") + "," +
                                    TXT_UPPER.Text.Trim().Replace(",", "") + "," +
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
                    conn.QueryString = "delete from PARAM_APPROVAL_RANGE where " +
                                        "CODE = '" + e.Item.Cells[0].Text + "' " +
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
                                "a.CODE, " +
                                "FULLNAME = LEFT(UPPER(LTRIM(isnull(a.FRONT_NAME,'')) + RTRIM(' ' + isnull(a.MID_NAME,'')) + RTRIM(' ' + isnull(a.LAST_NAME,''))), 50), " +
                                "ROLE_DESCR = UPPER(a.ROLE_DESCR) " +
                                "from V_LINK_SC_M_USERS a " +
                                "where " +
                                "a.CODE not in (select USERID from PARAM_APPROVAL_DETAIL where CODE='" + LB_PROCESS.Text + "' and START_AMOUNT=" + LB_BOTTOM.Text.Replace(",", "") + ") " +
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
                                "a.CODE, " +
                                "FULLNAME = LEFT(UPPER(LTRIM(isnull(a.FRONT_NAME,'')) + RTRIM(' ' + isnull(a.MID_NAME,'')) + RTRIM(' ' + isnull(a.LAST_NAME,''))), 50), " +
                                "ROLE_DESCR = UPPER(a.ROLE_DESCR) " +
                                "from V_LINK_SC_M_USERS a " +
                                "where " +
                                "a.CODE in (select USERID from PARAM_APPROVAL_DETAIL where CODE='" + LB_PROCESS.Text + "' and START_AMOUNT=" + LB_BOTTOM.Text.Replace(",", "") + ") " +
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
                    conn.QueryString = "insert into PARAM_APPROVAL_DETAIL select " +
                                        "'" + LB_PROCESS.Text + "'," +
                                        LB_BOTTOM.Text.Replace(",","") + "," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
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
                    conn.QueryString = "delete PARAM_APPROVAL_DETAIL where " +
                                        "CODE = '" + LB_PROCESS.Text + "' " +
                                        "and START_AMOUNT = " + LB_BOTTOM.Text.Replace(",", "") + " " +
                                        "and USERID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRUnselected();
                FillDGRSelected();
            }
        }
    }
}