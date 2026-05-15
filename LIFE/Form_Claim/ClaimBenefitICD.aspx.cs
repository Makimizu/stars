using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Claim
{
    public partial class ClaimBenefitICD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                bDone = TrackDone();
                LoadDGRUnselected();
                LoadDGRSelected();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select LAST_TRACK from V_APPLICATION_CLAIM_MASTER where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and LAST_TRACK in (4,5)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                result = false;

            return result;
        }

        protected void LoadDGRUnselected()
        {
            if (bDone || Request.QueryString["readonly"] == "1")
            {
                TR_UNSELECTED.Visible = false;
                return;
            }

            conn.QueryString = "select top 100 CODE,DESCR from V_LINK_UW_PR_ICD " +
                                "where " +
                                "(replace(replace(CODE,'.',''),' ','') like '%" + TXT_ICDSEARCH.Text.Trim().Replace(".", "") + "%' " +
                                "or replace(replace(DESCR,'.',''),' ','') like '%" + TXT_ICDSEARCH.Text.Trim().Replace(".", "") + "%') " +
                                "and CODE not in (select ICD_CODE from APPLICATION_CLAIM_ICD where REGNO='" + LB_REGNO.Text + "' and SEQ=" + LB_SEQ.Text + ") " +
                                "order by 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ICD_UNSELECTED.DataSource = dt;
            DGR_ICD_UNSELECTED.DataBind();

            for (int i = 0; i < DGR_ICD_UNSELECTED.Items.Count; i++)
            {
                LinkButton lbDESCR = (LinkButton)DGR_ICD_UNSELECTED.Items[i].FindControl("LBT_SELECTED");
                lbDESCR.Text = DGR_ICD_UNSELECTED.Items[i].Cells[1].Text;
            }
        }

        protected void LoadDGRSelected()
        {
            TR_SELECTED.Visible = false;
            conn.QueryString = "exec SP_APPLICATION_CLAIM_ICD " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            TR_SELECTED.Visible = true;
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ICD_SELECTED.DataSource = dt;
            DGR_ICD_SELECTED.DataBind();

            if (bDone || Request.QueryString["readonly"] == "1")
            {
                DGR_ICD_SELECTED.Columns[2].Visible = false;
            }
        }

        protected void TXT_ICDSEARCH_TextChanged(object sender, EventArgs e)
        {
            LoadDGRUnselected();
        }

        protected void DGR_ICD_UNSELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    conn.QueryString = "insert into APPLICATION_CLAIM_ICD select " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                    conn.ExecuteNonQuery();
                }
                catch { }

                LoadDGRUnselected();
                LoadDGRSelected();
            }
        }

        protected void DGR_ICD_SELECTED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APPLICATION_CLAIM_ICD where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and SEQ = '" + LB_SEQ.Text + "' " +
                                        "and ICD_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                LoadDGRUnselected();
                LoadDGRSelected();
            }
        }
    }
}