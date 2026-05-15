using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class PenjaminanAkhir : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                LB_ID.Text = Request.QueryString["NOSURAT"].ToString();
                BT_AKHIR_SAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES AKHIR ?')){return false;};");
                ShowAKHIR();
            }
        }

        protected void BT_AKHIR_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_AKHIR_VALIDASI " +
                                "'" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("RESULT").ToString() != "")
            {
                LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString();
                return;
            }


            if (TXT_TGLAKHIR.Text.Trim() == "" || TXT_TGLPULANG.Text.Trim() == "")
                return;

            string tglbill, tglakhir, tglpulang;
            tglbill = "null";
            tglakhir = "null";
            tglpulang = "null";

            if (TXT_TGLBILL.Text.Trim() != "")
            {
                tglbill = "'" + GlobalUse.GlobalDateFormat(TXT_TGLBILL.Text.Trim(), "d/M/yyyy") + " " + DDL_TGLBILL_HH.SelectedValue + ":" + DDL_TGLBILL_MM.SelectedValue + "'";
            }

            if (TXT_TGLAKHIR.Text.Trim() != "")
            {
                tglakhir = "'" + GlobalUse.GlobalDateFormat(TXT_TGLAKHIR.Text.Trim(), "d/M/yyyy") + " " + DDL_TGLAKHIR_HH.SelectedValue + ":" + DDL_TGLAKHIR_MM.SelectedValue + "'";
            }

            if (TXT_TGLPULANG.Text.Trim() != "")
            {
                tglpulang = "'" + GlobalUse.GlobalDateFormat(TXT_TGLPULANG.Text.Trim(), "d/M/yyyy") + " " + DDL_TGLPULANG_HH.SelectedValue + ":" + DDL_TGLPULANG_MM.SelectedValue + "'";
            }

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_AKHIR " +
                                    "'" + LB_ID.Text + "'," +
                                    tglbill + "," +
                                    tglakhir + "," +
                                    tglpulang + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                Response.Redirect("Penjaminan.aspx?NOSURAT=" + LB_ID.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message;
            }
        }

        protected void ShowAKHIR()
        {
            conn.QueryString = "select TRACK = SEQ from TRACK_DATA where TIPE_CODE='CLMPROVSJ' and OWNER='" +LB_ID.Text+ "' and SEQ in (3,5,7)";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                BT_AKHIR_SAVE.Visible = false;
            }


            conn.QueryString = "select " +
                                "TGL_TERIMA_BILL = convert(varchar(20),TGL_TERIMA_BILL,103), " +
                                "TGL_TERIMA_BILL_HH = right('00' + convert(varchar(2),datepart(hour,TGL_TERIMA_BILL)),2), " +
                                "TGL_TERIMA_BILL_MM = right('00' + convert(varchar(2),datepart(minute,TGL_TERIMA_BILL)),2), " +
                                "TGL_AKHIR = convert(varchar(20),ISNULL(TGL_AKHIR, (select MAX(TGL_MONITORING) from CLAIM_SURAT_JAMINAN_MONITORING where SURAT_JAMINAN_ID=a.ID)),103),  " +
                                "TGL_AKHIR_HH = right('00' + convert(varchar(2),datepart(hour,TGL_AKHIR)),2), " +
                                "TGL_AKHIR_MM = right('00' + convert(varchar(2),datepart(minute,TGL_AKHIR)),2), " +
                                "TGL_PULANG = convert(varchar(20),TGL_PULANG,103), " +
                                "TGL_PULANG_HH = right('00' + convert(varchar(2),datepart(hour,TGL_PULANG)),2), " +
                                "TGL_PULANG_MM = right('00' + convert(varchar(2),datepart(minute,TGL_PULANG)),2) " +
                                "from CLAIM_SURAT_JAMINAN_MASTER a " +
                                "where " +
                                "NOMOR_SURAT_JAMINAN = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            TXT_TGLBILL.Text = conn.GetFieldValue("TGL_TERIMA_BILL").ToString();
            TXT_TGLAKHIR.Text = conn.GetFieldValue("TGL_AKHIR").ToString();
            TXT_TGLPULANG.Text = conn.GetFieldValue("TGL_PULANG").ToString();

            try
            {
                DDL_TGLBILL_HH.SelectedValue = conn.GetFieldValue("TGL_TERIMA_BILL_HH").ToString();
                DDL_TGLBILL_MM.SelectedValue = conn.GetFieldValue("TGL_TERIMA_BILL_MM").ToString();
            }
            catch { }

            try
            {
                DDL_TGLAKHIR_HH.SelectedValue = conn.GetFieldValue("TGL_AKHIR_HH").ToString();
                DDL_TGLAKHIR_MM.SelectedValue = conn.GetFieldValue("TGL_AKHIR_MM").ToString();
            }
            catch { }

            try
            {
                DDL_TGLPULANG_HH.SelectedValue = conn.GetFieldValue("TGL_PULANG_HH").ToString();
                DDL_TGLPULANG_MM.SelectedValue = conn.GetFieldValue("TGL_PULANG_MM").ToString();
            }
            catch { }
        }
    }
}