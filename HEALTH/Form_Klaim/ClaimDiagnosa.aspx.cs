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
    public partial class ClaimDiagnosa : System.Web.UI.Page
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
                FillDGRDiagnosa();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();

            BT_CARI_ICD.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode1.aspx?field1=CODE&field2=DESCR&tablename=PR_ICD&parent=0&target=TXT_ICD','ICD','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void FillDGRDiagnosa()
        {
            conn.QueryString = "select " +
                                "a.ICD_CODE, " +
                                "c.DESCR " +
                                "from CLAIM_ICD a " +
                                "inner join PR_ICD c on a.ICD_CODE=c.CODE " +
                                "where " +
                                "a.CLAIM_NO='" + LB_CLAIMNO.Text + "' " +
                                "order by a.USERDATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DIAG_AWAL.DataSource = dt;
            DGR_DIAG_AWAL.DataBind();

            if (LB_TRACK.Text == "3" || LB_TRACK.Text == "4")
            {
                TXT_ICD.Visible = false;
                BT_CARI_ICD.Visible = false;
                BT_TAMBAH_ICD.Visible = false;
                DGR_DIAG_AWAL.Columns[2].Visible = false;
            }
        }

        protected void BT_TAMBAH_ICD_Click(object sender, EventArgs e)
        {
            if (Request.Form[TXT_ICD.UniqueID] == "")
                return;
            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_ICD_SAVE " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + Request.Form[TXT_ICD.UniqueID].Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRDiagnosa();
                Request.Form[TXT_ICD.UniqueID] = "";
            }
            catch { }
        }

        protected void DGR_DIAG_AWAL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_ICD " +
                                        "where " +
                                        "ICD_CODE='" + e.Item.Cells[0].Text + "' " +
                                        "and CLAIM_NO ='" + LB_CLAIMNO.Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRDiagnosa();
                }
                catch { }
            }
        }
    }
}