using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimRevision : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                Setup();
                ShowRevision();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();

            conn.QueryString = "SELECT A.CODE,B.DESCR+' - '+A.DESCR FROM PARAM_CLAIM_ADD_REASON A " +
                                    "INNER JOIN dbo.PR_CLAIM_CTGRY_ADD_REASON B ON B.CODE = A.CTGRY_ADD " +
                                    "WHERE A.TIPE_ADD ='R' " +
                                    "ORDER BY B.DESCR,A.DESCR";
            conn.ExecuteQuery();
            for (int revi = 0; revi < conn.GetRowCount(); revi++)
                DDL_REVISION_REMARK.Items.Add(new ListItem(conn.GetFieldValue(revi, 1).ToString(), conn.GetFieldValue(revi, 0).ToString()));
        }

        protected void ShowRevision()
        {
            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_REVISION_HISTORY '" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_REVISION.DataSource = dt;
                DGR_REVISION.DataBind();

                conn.QueryString = "SELECT REASON_CODE FROM dbo.CLAIM_REVISION WHERE CLAIM_NO='" + LB_CLAIMNO.Text + "'";
                conn.ExecuteQuery();
                DDL_REVISION_REMARK.SelectedValue = conn.GetFieldValue(0, 0);
            }
            catch
            {
                //DDL_REVISION_REMARK.Items.Clear();
            };
        }

        protected void DGR_REVISION_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_CLAIM_REVISION_HISTORY_TOTAL '" + LB_CLAIMNO.Text + "'";
                    conn.ExecuteQuery();

                    e.Item.Cells[2].Text = "TOTAL";
                    e.Item.Cells[3].Text = conn.GetFieldValue("AMOUNT_PENGAJUAN").ToString();
                    e.Item.Cells[4].Text = conn.GetFieldValue("AMOUNT_CASH").ToString();
                    e.Item.Cells[5].Text = conn.GetFieldValue("AMOUNT_BAYAR").ToString();
                    e.Item.Cells[6].Text = conn.GetFieldValue("EXCESS").ToString();
                    e.Item.Cells[7].Text = conn.GetFieldValue("REFUND").ToString();
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "UPDATE dbo.CLAIM_REVISION SET REASON_CODE='" + DDL_REVISION_REMARK.SelectedValue + "' WHERE CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteNonQuery();
            ShowRevision();
        }
    }
}