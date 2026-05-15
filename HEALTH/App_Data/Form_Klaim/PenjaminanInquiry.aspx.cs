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
    public partial class PenjaminanInquiry : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["MODE"].ToString();
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TIPE_SURAT_JAMINAN";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            string where = "";
            LB_RESULT.Text = "";

            if (DDL_MON.SelectedValue != "")
            {
                if (DDL_MON.SelectedValue != "1")
                {
                    where = where + " and a.NOMOR_SURAT_JAMINAN in (select OWNER FROM TRACK_DATA where TIPE_CODE='CLMPROVSJ' and SEQ in ('3','5','7')) ";
                }
                else
                {
                    where = where + " and a.NOMOR_SURAT_JAMINAN not in (select OWNER FROM TRACK_DATA where TIPE_CODE='CLMPROVSJ' and SEQ in ('3','5','7')) ";
                }
            }

            if (TXT_NO_SURJAM.Text.Trim() != "")
            {
                where = where + " and NOMOR_SURAT_JAMINAN='" + TXT_NO_SURJAM.Text.Trim() + "' ";
            }

            if (TXT_COMPANY.Text.Trim() != "")
            {
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            }

            if (TXT_NAMA.Text.Trim() != "")
            {
                where = where + " and NAMA_PESERTA like '%" + TXT_NAMA.Text.Trim() + "%' ";
            }

            if (TXT_NOPOL.Text.Trim() != "")
            {
                where = where + " and POLICY_NO like '%" + TXT_NOPOL.Text.Trim() + "%' ";
            }

            if (TXT_PROVIDER.Text.Trim() != "")
            {
                where = where + " and NAMA_PROVIDER like '%" + TXT_PROVIDER.Text.Trim() + "%' ";
            }

            if (DDL_VIP.SelectedValue != "")
            {
                where = where + " and a.VIP = '" + DDL_VIP.SelectedValue + "' ";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and a.TIPE_JAMINAN = '" + DDL_TIPE.SelectedValue + "' ";
            }

            if (TXT_MASUK1.Text.Trim() != "" || TXT_MASUK2.Text.Trim() != "")
            {
                string date1 = "'1 jan 1980'";
                string date2 = "GETDATE()";

                if (TXT_MASUK1.Text.Trim() != "")
                    date1 = "'" + GlobalUse.GlobalDateFormat(TXT_MASUK1.Text.Trim(), "d/M/yyyy") + "'";
                if (TXT_MASUK2.Text.Trim() != "")
                    date2 = "'" + GlobalUse.GlobalDateFormat(TXT_MASUK2.Text.Trim(), "d/M/yyyy") + "'";

                where = where + " and (convert(date,a.TGL_MASUK) between " + date1 + " and " + date2 + ") ";
            }

            if (TXT_PULANG1.Text.Trim() != "" || TXT_PULANG2.Text.Trim() != "")
            {
                string date1 = "'1 jan 1980'";
                string date2 = "GETDATE()";

                if (TXT_PULANG1.Text.Trim() != "")
                    date1 = "'" + GlobalUse.GlobalDateFormat(TXT_PULANG1.Text.Trim(), "d/M/yyyy") + "'";
                if (TXT_PULANG2.Text.Trim() != "")
                    date2 = "'" + GlobalUse.GlobalDateFormat(TXT_PULANG2.Text.Trim(), "d/M/yyyy") + "'";

                where = where + " and (convert(date,a.TGL_PULANG) between " + date1 + " and " + date2 + ") ";
            }

            if (TXT_BIAYA1.Text.Trim() != "" || TXT_BIAYA2.Text.Trim() != "")
            {
                string biaya1 = "0";
                string biaya2 = "999999999999";

                if (TXT_BIAYA1.Text.Trim() != "")
                    biaya1 = TXT_BIAYA1.Text.Trim().Replace(",", "");
                if (TXT_BIAYA2.Text.Trim() != "")
                    biaya2 = TXT_BIAYA2.Text.Trim().Replace(",", "");

                where = where + " and (a.BIAYA between " + biaya1 + " and " + biaya2 + ") ";
            }

            conn.QueryString = "SELECT  " +
                                "[NO SURAT JAMINAN] = NOMOR_SURAT_JAMINAN ," +
                                "[PESERTA] = NAMA_PESERTA ," +
                                "[VIP] = ( CASE WHEN VIP = 1 THEN 'YES' ELSE 'NO' END ) ," +
                                "[TIPE SURAT JAMINAN] =  TIPE_JAMINAN_DESCR," +
                                "[NO POLIS] = POLICY_NO ," +
                                "[PERUSAHAAN] = COMPANY_NAME ," +
                                "[PROVIDER] = NAMA_PROVIDER ," +
                                "[TGL MASUK] = CONVERT(VARCHAR(20), TGL_MASUK, 106) ," +
                                "[TGL AKHIR] = CONVERT(VARCHAR(20), TGL_AKHIR, 106) ," +
                                "[TGL PULANG] = CONVERT(VARCHAR(20), TGL_PULANG, 106) , " +
                                "[BIAYA AKHIR] = REPLACE(CONVERT(VARCHAR(100), CONVERT(MONEY, BIAYA), 1),'.00', '') ," +
                                "[LAST MNT] = CONVERT(VARCHAR(20), LASMON, 106) ," +
                                "[CNT MNT] = MON ," +
                                "[LOS] = LOS ," +
                                "[TRACK] = LAST_TRACK_DESCR ," +
                                "[DIAGNOSA] = ICD_DESCR " +
                                "FROM  V_CLM_SURAT_JAMINAN a " +
                                "WHERE   NOMOR_SURAT_JAMINAN IS NOT NULL " +
                                "AND ISNULL(NOMOR_SURAT_JAMINAN, '') IN (SELECT DISTINCT OWNER FROM TRACK_DATA WHERE TIPE_CODE = 'CLMPROVSJ' AND SEQ <> '1' ) " +
                                where + " ORDER BY TGL_MASUK desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                switch (LB_MODE.Text)
                {
                    case "0": Response.Redirect("Penjaminan.aspx?NOSURAT=" + e.Item.Cells[1].Text);
                        break;
                    case "1": Response.Redirect("HospitalMonitoring.aspx?NOSURAT=" + e.Item.Cells[1].Text);
                        break;
                }
            }
        }
    }
}