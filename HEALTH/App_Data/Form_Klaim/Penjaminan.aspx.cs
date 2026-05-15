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
    public partial class Penjaminan : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_NOSURAT.Text = Request.QueryString["NOSURAT"].ToString();
                Setup();

                if (LB_NOSURAT.Text.Trim() != "")
                {
                    LoadRecord(LB_NOSURAT.Text.Trim());
                }

                CheckMode();
            }

        }

        protected void CheckMode()
        {
            if (LB_NOSURAT.Text == "")
            {
                TR_SMS.Visible = false;
                TR_APPROVAL.Visible = false;
                TR_BUTTONS.Visible = false;
                BT_CANCEL.Visible = false;
            }
            else
            {
                TR_SMS.Visible = false;
                TR_APPROVAL.Visible = false;
                TR_BUTTONS.Visible = false;
                BT_CANCEL.Visible = false;

                conn.QueryString = "select TRACK=MAX(SEQ) from TRACK_DATA where TIPE_CODE='CLMPROVSJ' and OWNER='" + LB_NOSURAT.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                    return;

                if (conn.GetFieldValue("TRACK").ToString() == "1")
                {
                    //TR_APPROVAL.Visible = true;
                    TR_BUTTONS.Visible = true;
                    TD_MONLIST.Visible = false;
                    TD_KLAIM.Visible = false;
                    TD_AKHIR.Visible = false;
                }
                else
                {
                    TR_SMS.Visible = true;
                    TR_BUTTONS.Visible = true;

                    if (conn.GetFieldValue("TRACK").ToString() == "2")
                    {
                        BT_CANCEL.Visible = true;
                        CheckCLAIMTRX();
                    }
                }
            }
        }

        protected void CheckCLAIMTRX()
        {
            conn.QueryString = "select CLAIM_NO from CLAIM_SURAT_JAMINAN_MASTER where NOMOR_SURAT_JAMINAN='" + LB_NOSURAT.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("CLAIM_NO").ToString() == "")
            {
                BT_CLAIM.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk masukan transaksi ?')){return false;};");
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_PENJAMINAN_SMS";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_SMS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            conn.QueryString = "select CODE,DESCR from PR_GELAR_DOKTER_SPESIALIS";
            conn.ExecuteQuery();
            DDL_GELAR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GELAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TIPE_SURAT_JAMINAN";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_CLAIM_ALASAN_NAIK_KELAS";
            conn.ExecuteQuery();
            DDL_ALASANNAIKKELAS.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ALASANNAIKKELAS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_KELAS_RS order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_KELAS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


        }


        protected void LoadRecord(string nomor)
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.NOMOR_SURAT_JAMINAN, " +
                                "a.REGNO, " +
                                "c.COMPANY_CODE, " +
                                "a.KODE_PROVIDER, " +
                                "TGL_MASUK = CONVERT(varchar(20),a.TGL_MASUK,103), " +
                                "a.ESTIMASI_INAP, " +
                                "TGL_ESTIMASI = CONVERT(varchar(20),a.TGL_MASUK+isnull(a.ESTIMASI_INAP,0),106), " +
                                "a.KELAS_KAMAR, " +
                                "a.NAMA_KAMAR, " +
                                "HARGA_KAMAR = replace(convert(varchar(100),CONVERT(money,a.HARGA_KAMAR),1),'.00',''), " +
                                "a.ALASAN_NAIK_KELAS_CODE, " +
                                "a.NOREK_MEDIS, " +
                                "a.DOKTER_RAWAT, " +
                                "a.KONTAK, " +
                                "PLAFON = replace(convert(varchar(100),CONVERT(money,a.PLAFON),1),'.00',''), " +
                                "a.TIPE_SURAT_JAMINAN, " +
                                "a.GELAR_DOKTER, " +
                                "a.PIC_PASIEN, " +
                                "a.PIC_PASIEN_PHONE " +
                                "from CLAIM_SURAT_JAMINAN_MASTER a " +
                                "inner join PESERTA_MASTER b on a.REGNO=b.REGNO " +
                                "inner join BRANCH c on b.BRANCH_CODE=c.BRANCH_CODE " +
                                "where NOMOR_SURAT_JAMINAN='" + nomor + "'";
            conn.ExecuteQuery();

            LB_ID.Text = conn.GetFieldValue("ID").ToString();
            LB_NOSURAT.Text = conn.GetFieldValue("NOMOR_SURAT_JAMINAN").ToString();


            TXT_TGLMASUK.Text = conn.GetFieldValue("TGL_MASUK").ToString();
            TXT_INAP.Text = conn.GetFieldValue("ESTIMASI_INAP").ToString();
            LB_TGL_AKHIR_SEMENTARA.Text = conn.GetFieldValue("TGL_ESTIMASI").ToString();
            TXT_NAMAKAMAR.Text = conn.GetFieldValue("NAMA_KAMAR").ToString();
            TXT_HARGAKAMAR.Text = conn.GetFieldValue("HARGA_KAMAR").ToString();
            TXT_DOKTER.Text = conn.GetFieldValue("DOKTER_RAWAT").ToString();
            TXT_KONTAK.Text = conn.GetFieldValue("KONTAK").ToString();
            TXT_PLAFON.Text = conn.GetFieldValue("PLAFON").ToString();
            TXT_PIC_PASIEN.Text = conn.GetFieldValue("PIC_PASIEN").ToString();
            TXT_PIC_PASIEN_PHONE.Text = conn.GetFieldValue("PIC_PASIEN_PHONE").ToString();
            TXT_NOREK_MEDIS.Text = conn.GetFieldValue("NOREK_MEDIS").ToString();

            try
            {
                DDL_TIPE.SelectedValue = conn.GetFieldValue("TIPE_SURAT_JAMINAN").ToString();
            }
            catch { }

            try
            {
                DDL_GELAR.SelectedValue = conn.GetFieldValue("GELAR_DOKTER").ToString();
            }
            catch { }

            try
            {
                DDL_KELAS.SelectedValue = conn.GetFieldValue("KELAS_KAMAR").ToString();
            }
            catch { }



            try
            {
                DDL_ALASANNAIKKELAS.SelectedValue = conn.GetFieldValue("ALASAN_NAIK_KELAS_CODE").ToString();
            }
            catch { }

            string regno = conn.GetFieldValue("REGNO").ToString();
            string kode_provider = conn.GetFieldValue("KODE_PROVIDER").ToString();

            SetREGNO(regno);
            SetPROVIDER(kode_provider);
        }

        protected void SetREGNO(string regno)
        {
            LB_REGNO.Text = regno;
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_INFO_PESERTA '" + regno + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO_PESERTA.DataSource = dt;
            DGR_INFO_PESERTA.DataBind();

        }

        protected void SetPROVIDER(string code)
        {
            LB_KODE_PROVIDER.Text = code;
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_INFO_PROVIDER '" + code + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INFO_PROVIDER.DataSource = dt;
            DGR_INFO_PROVIDER.DataBind();
        }

        protected void FillDGRCariPeserta()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.NAMA, " +
                                "c.COMPANY_NAME " +
                                "from PESERTA_MASTER a " +
                                "inner join BRANCH b on a.BRANCH_CODE=b.BRANCH_CODE " +
                                "inner join COMPANY c on b.COMPANY_CODE=c.COMPANY_CODE " +
                                "where " +
                                "a.STAT=a.STAT " +
                                "and a.REGNO like '%" + TXT_CARI_REGNO.Text.Trim() + "%' " +
                                "and a.NAMA like '%" + TXT_CARI_PESERTA.Text.Trim() + "%' " +
                                "and c.COMPANY_NAME like '%" + TXT_CARI_COMPANY.Text.Trim() + "%' " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CARI_PESERTA.DataSource = dt;
            DGR_CARI_PESERTA.DataBind();

            DGR_CARI_PESERTA.Visible = true;
        }

        protected void FillDGRCariProvider()
        {
            conn.QueryString = "select " +
                                "KODE_PROVIDER, " +
                                "NAMA = UPPER(NAMA) " +
                                "from PROVIDER_MASTER " +
                                "where " +
                                "STAT='" + DDL_STAT_PROVIDER.SelectedValue + "' " +
                                "and NAMA like '%" + TXT_NAMA_PROVIDER.Text.Trim() + "%' " +
                                "and JENIS_PROVIDER='PR' " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CARI_PROVIDER.DataSource = dt;
            DGR_CARI_PROVIDER.DataBind();

            DGR_CARI_PROVIDER.Visible = true;
        }

        protected void BT_CARI_REGNO_Click(object sender, EventArgs e)
        {
            /*
            if (TXT_CARI_PESERTA.Text.Trim() == "")
            {
                BT_CARI2.Visible = true;
                TBL_CARI_PESERTA.Visible = false;
                return;
            }
            */

            DGR_CARI_PESERTA.CurrentPageIndex = 0;
            FillDGRCariPeserta();
        }

        protected void DGR_CARI_PESERTA_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PESERTA.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariPeserta();
        }


        protected void DGR_CARI_PESERTA_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_REGNO.Text = e.Item.Cells[1].Text;
                SetREGNO(e.Item.Cells[1].Text);

                BT_CARI2.Visible = true;
                TBL_CARI_PESERTA.Visible = false;
            }
        }

        protected void BT_CARI2_Click(object sender, EventArgs e)
        {
            BT_CARI2.Visible = false;
            TBL_CARI_PESERTA.Visible = true;

            TXT_CARI_PESERTA.Text = "";
            TXT_CARI_COMPANY.Text = "";
            DGR_CARI_PESERTA.Visible = false;
        }

        protected void BT_CARI_PROVIDER_Click(object sender, EventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = 0;
            FillDGRCariProvider();
        }

        protected void DGR_CARI_PROVIDER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_CARI_PROVIDER.CurrentPageIndex = e.NewPageIndex;
            FillDGRCariProvider();
        }


        protected void DGR_CARI_PROVIDER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_KODE_PROVIDER.Text = e.Item.Cells[1].Text;
                SetPROVIDER(e.Item.Cells[1].Text);

                BT_CARI3.Visible = true;
                TBL_PROVIDER.Visible = false;
            }
        }


        protected void BT_CARI3_Click(object sender, EventArgs e)
        {
            BT_CARI3.Visible = false;
            TBL_PROVIDER.Visible = true;

            TXT_NAMA_PROVIDER.Text = "";
            DGR_CARI_PROVIDER.Visible = false;
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_APPROVAL " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    "2," +
                                    "'" + TXT_REJECT.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            Response.Redirect("../Form_Tools/InquiryScreen.aspx?CODE=035b");

        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            if (TXT_REJECT.Text.Trim() == "")
            {
                LB_ERROR.Text = "Alasan REJECT/CANCEL tidak boleh kosong !";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_APPROVAL " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    "3," +
                                    "'" + TXT_REJECT.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            Response.Redirect("../Form_Tools/InquiryScreen.aspx?CODE=035b");
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            if (TXT_REJECT.Text.Trim() == "")
            {
                LB_ERROR.Text = "Alasan REJECT/CANCEL tidak boleh kosong !";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_APPROVAL " +
                                    "'" + LB_NOSURAT.Text + "'," +
                                    "5," +
                                    "'" + TXT_REJECT.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            Response.Redirect("Penjaminan.aspx?NOSURAT=" + LB_NOSURAT.Text);
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            SaveMaster();
        }

        protected void SaveMaster()
        {
            LB_ERROR.Text = "";
            if (LB_REGNO.Text == "")
                return;

            string ID = "null";
            string NOSURAT = "null";
            string alasannaikkelas = "null";
            string tipejaminan = "null";
            string gelar = "null";

            try
            {
                if (LB_ID.Text != "")
                    ID = "'" + LB_ID.Text + "'";

                if (LB_NOSURAT.Text != "")
                    NOSURAT = "'" + LB_NOSURAT.Text + "'";

                if (DDL_ALASANNAIKKELAS.SelectedValue != "")
                    alasannaikkelas = "'" + DDL_ALASANNAIKKELAS.SelectedValue + "'";

                if (DDL_TIPE.SelectedValue != "")
                    tipejaminan = "'" + DDL_TIPE.SelectedValue + "'";

                if (DDL_GELAR.SelectedValue != "")
                    gelar = "'" + DDL_GELAR.SelectedValue + "'";


                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_UPSERT_VALIDATION " +
                                    ID + "," +
                                    NOSURAT + "," +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_KODE_PROVIDER.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLMASUK.Text, "d/M/yyyy") + "'," +
                                    "'" + TXT_INAP.Text.Trim().Replace(",", "") + "'," +
                                    "'" + DDL_KELAS.SelectedValue + "'," +
                                    "'" + TXT_NAMAKAMAR.Text.Trim() + "'," +
                                    "'" + TXT_HARGAKAMAR.Text.Trim().Replace(",", "") + "'," +
                                    alasannaikkelas + "," +
                                    "'" + TXT_NOREK_MEDIS.Text.Trim() + "'," +
                                    "'" + TXT_DOKTER.Text.Trim() + "'," +
                                    "'" + TXT_KONTAK.Text.Trim() + "'," +
                                    "'" + TXT_PLAFON.Text.Trim().Replace(",", "") + "'," +
                                    tipejaminan + "," +
                                    gelar + "," +
                                    "'" + TXT_PIC_PASIEN.Text.Trim() + "'," +
                                    "'" + TXT_PIC_PASIEN_PHONE.Text.Trim() + "'";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("RESULT").ToString().Trim() != "")
                {
                    LB_ERROR.Text = conn.GetFieldValue("RESULT").ToString().Trim();
                    return;
                }

                conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_UPSERT " +
                                    ID + "," +
                                    NOSURAT + "," +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_KODE_PROVIDER.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLMASUK.Text, "d/M/yyyy") + "'," +
                                    "'" + TXT_INAP.Text.Trim().Replace(",", "") + "'," +
                                    "'" + DDL_KELAS.SelectedValue + "'," +
                                    "'" + TXT_NAMAKAMAR.Text.Trim() + "'," +
                                    "'" + TXT_HARGAKAMAR.Text.Trim().Replace(",", "") + "'," +
                                    alasannaikkelas + "," +
                                    "'" + TXT_NOREK_MEDIS.Text.Trim() + "'," +
                                    "'" + TXT_DOKTER.Text.Trim() + "'," +
                                    "'" + TXT_KONTAK.Text.Trim() + "'," +
                                    "'" + TXT_PLAFON.Text.Trim().Replace(",", "") + "'," +
                                    tipejaminan + "," +
                                    gelar + "," +
                                    "'" + TXT_PIC_PASIEN.Text.Trim() + "'," +
                                    "'" + TXT_PIC_PASIEN_PHONE.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                LB_NOSURAT.Text = conn.GetFieldValue("NOMOR_SURAT_JAMINAN").ToString();
                CheckMode();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            Response.Redirect("Penjaminan.aspx?NOSURAT=" + LB_NOSURAT.Text);
            /*
            if (ID == "null")
            {
                Response.Redirect("Penjaminan.aspx?NOSURAT=");
            }
            else
            {
                Response.Redirect("Penjaminan.aspx?NOSURAT=" + LB_NOSURAT.Text);
            }
            */
        }

        protected void BT_SMS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&NOMOR_SURAT_JAMINAN=' + '" + LB_NOSURAT.Text + "&MODE=' + '" + DDL_SMS.SelectedValue + "&rs:Format=EXCEL'  from V_LINK_SC_REPORT_LIST where CODE='214'";
            conn.ExecuteQuery();
            string URL = conn.GetFieldValue("URL").ToString();
            Response.Redirect(URL);
        }

        protected void BT_INFO_AWAL_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "../Form_Tools/AdditionalInfo.aspx?tipe=JAMAWAL&owner=" + LB_NOSURAT.Text);
        }

        protected void BT_DIAG_AWAL_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "PenjaminanDiagnosa.aspx?NOSURAT=" + LB_NOSURAT.Text);
        }

        protected void BT_MONLIST_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "PenjaminanMonitoring.aspx?NOSURAT=" + LB_NOSURAT.Text);
        }

        protected void BT_ARSIP_Click(object sender, EventArgs e)
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_SJ", LB_NOSURAT.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ShowPopUp(((Button)sender).Text, URL);
        }

        protected void BT_TRACK_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "../Form_Tools/Track.aspx?tipe=CLMPROVSJ&owner=" + LB_NOSURAT.Text);
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "../Form_Tools/Remark.aspx?tipe=CLMSURJAM&owner=" + LB_NOSURAT.Text);
        }

        protected void BT_CLAIM_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select CLAIM_NO from CLAIM_SURAT_JAMINAN_MASTER where NOMOR_SURAT_JAMINAN='" + LB_NOSURAT.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("CLAIM_NO").ToString() != "")
            {
                string CLAIMNO = conn.GetFieldValue("CLAIM_NO").ToString();
                ShowPopUp(((Button)sender).Text, "ClaimBenTrx.aspx?CLAIM_NO=" + CLAIMNO);
            }
            else
            {
                CreateCLMTRX();
            }
        }

        protected void CreateCLMTRX()
        {
            conn.QueryString = "select TRACK=MAX(SEQ) from TRACK_DATA where TIPE_CODE='CLMPROVSJ' and OWNER='" + LB_NOSURAT.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                if (conn.GetFieldValue("TRACK").ToString() != "1")
                {
                    conn.QueryString = "select a.ICD_CODE	from CLAIM_SURAT_JAMINAN_ICD a " +
                                        "inner join CLAIM_SURAT_JAMINAN_MASTER b on a.SURAT_JAMINAN_ID=b.ID " +
                                        "where b.NOMOR_SURAT_JAMINAN = '" + LB_NOSURAT.Text + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() == 0)
                    {
                        GlobalTools.popMessage(this, "DIAGNOSA AWAL MASIH KOSONG");
                        return;
                    }


                    try
                    {
                        conn.QueryString = "exec SP_CLM_CLAIM_MASTER_UPSERT_SURJAM '" + LB_NOSURAT.Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery();
                        string CLAIMNO = conn.GetFieldValue("CLAIM_NO").ToString();
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.clmprovsjbody.location.href = 'ClaimBenTrx.aspx?CLAIM_NO=" + CLAIMNO + "';</script>");
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }

                }
            }
        }

        protected void BT_SURAT_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "PenjaminanSurat.aspx?NOSURAT=" + LB_NOSURAT.Text);
        }

        protected void BT_AKHIR_Click(object sender, EventArgs e)
        {
            ShowPopUp(((Button)sender).Text, "PenjaminanAkhir.aspx?NOSURAT=" + LB_NOSURAT.Text);
        }

        protected void ShowPopUp(string title, string url)
        {
            LB_TITLE.Text = title;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            ifClaim.Attributes.Add("src", url);
        }
    }
}