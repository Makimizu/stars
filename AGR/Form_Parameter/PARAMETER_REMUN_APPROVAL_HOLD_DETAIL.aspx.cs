using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_REMUN_APPROVAL_HOLD_DETAIL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                FillDGRLevel();
            }
        }

        #region GRID BIND
        protected void FillDGRLevel()
        {
            //conn.QueryString = $@"EXEC SP_PARAM_REMUN_APPROVAL_HOLD '{1}'";
            //fixing interpolated string
            conn.QueryString = string.Format(@"EXEC SP_PARAM_REMUN_APPROVAL_HOLD '{0}'", 1);
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LEVEL.DataSource = dt;
            DGR_LEVEL.DataBind();

            for (int i = 0; i < DGR_LEVEL.Items.Count; i++)
            {
                LinkButton lbtLevel = (LinkButton)DGR_LEVEL.Items[i].FindControl("Link_Level");
                lbtLevel.Text = DGR_LEVEL.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_LEVEL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                TR_TITLE.Visible = true;
                TR_CONTENT.Visible = true;
                LB_LEVEL_ID.Text = e.Item.Cells[0].Text;

                LB_UNSELECTED.Text = "UNSELECTED " + e.Item.Cells[1].Text;
                LB_SELECTED.Text = "SELECTED " + e.Item.Cells[1].Text;

                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_APPROVAL_HOLD_USER " +
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

        protected void DGR_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                /*string strQry = $@"INSERT INTO PARAM_REMUN_APPROVAL_HOLD_DETAIL 
                                SELECT SysCode = '{LB_LEVEL_ID.Text.Trim()}',
                                UserId = '{e.Item.Cells[0].Text}',
                                CreatedAt = GETDATE(),
                                CreatedBy = '{GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")}'";*/
                //fixing interpolate string in visual studio 2012
                string strQry = string.Format(@"
                                            INSERT INTO PARAM_REMUN_APPROVAL_HOLD_DETAIL 
                                            SELECT 
                                                SysCode = '{0}',
                                                UserId = '{1}',
                                                CreatedAt = GETDATE(),
                                                CreatedBy = '{2}'",
                                            LB_LEVEL_ID.Text.Trim(),
                                            e.Item.Cells[0].Text,
                                            GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")
                                        );

                conn.QueryString = strQry;
                conn.ExecuteQuery();

                FillDGRUnselected();
                FillDGRSelected();
                FillDGRLevel();
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_APPROVAL_HOLD_USER " +
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

        protected void DGR_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                /*string stQuery = $@"DELETE FROM PARAM_REMUN_APPROVAL_HOLD_DETAIL 
                                    WHERE 
                                    SysCode = '{LB_LEVEL_ID.Text.Trim()}' 
                                    and UserId = '{e.Item.Cells[0].Text}'";*/
                //fixing interpolated string in visual studio 2012
                string stQuery = string.Format(@"
                                            DELETE FROM PARAM_REMUN_APPROVAL_HOLD_DETAIL 
                                            WHERE 
                                                SysCode = '{0}' 
                                                AND UserId = '{1}'",
                                            LB_LEVEL_ID.Text.Trim(),
                                            e.Item.Cells[0].Text
                                        );

                conn.QueryString = stQuery;

                conn.ExecuteQuery();

                FillDGRUnselected();
                FillDGRSelected();
                FillDGRLevel();
            }
        }
        #endregion GRID BIND

        #region Action Button
        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGRUnselected();
        }
        #endregion Action Button
    }
}