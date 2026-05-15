using System;
using System.IO;
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
    public partial class PenjaminanDiagnosa : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["NOSURAT"].ToString();
                FillDGRDiagnosa();
                BT_CARI_ICD.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode1.aspx?field1=CODE&field2=DESCR&tablename=PR_ICD&parent=0&target=TXT_ICD','ICD','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void FillDGRDiagnosa()
        {
            conn.QueryString = "select " +
                                "a.ICD_CODE, " +
                                "c.DESCR " +
                                "from CLAIM_SURAT_JAMINAN_ICD a " +
                                "inner join CLAIM_SURAT_JAMINAN_MASTER b on a.SURAT_JAMINAN_ID=b.ID " +
                                "inner join PR_ICD c on a.ICD_CODE=c.CODE " +
                                "where " +
                                "b.NOMOR_SURAT_JAMINAN='" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DIAG_AWAL.DataSource = dt;
            DGR_DIAG_AWAL.DataBind();
        }

        protected void BT_TAMBAH_ICD_Click(object sender, EventArgs e)
        {
            if (TXT_ICD.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_ICD_SAVE " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + TXT_ICD.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRDiagnosa();
                TXT_ICD.Text = "";
            }
            catch { }
        }

        protected void DGR_DIAG_AWAL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_SURAT_JAMINAN_ICD " +
                                        "where " +
                                        "ICD_CODE='" + e.Item.Cells[0].Text + "' " +
                                        "and SURAT_JAMINAN_ID in " +
                                        "(select ID from CLAIM_SURAT_JAMINAN_MASTER where NOMOR_SURAT_JAMINAN='" + LB_ID.Text + "')";
                    conn.ExecuteNonQuery();
                    FillDGRDiagnosa();
                }
                catch { }
            }
        }
    }
}