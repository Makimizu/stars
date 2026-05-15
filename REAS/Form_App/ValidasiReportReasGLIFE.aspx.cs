using DMS.DBConnection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace REAS.Form_App
{
    public partial class ValidasiReportReasGLIFE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string uploadDate = "";
        List<UploadedDataList> uploadedDataList = new List<UploadedDataList>();
        #endregion
        public class UploadedDataList 
        {
            public string ID { get; set; }
            public string REAS_NAME { get; set; }
            public string NOTE { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "VALIDASI REPORT REAS GLIFE";
            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");
            uploadDate = currentDate.ToString("yyyy-MM-dd");
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type report glife sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string filePath = Path.Combine(Server.MapPath("~/Upload/ValidasiGLIFE"), TXT_FILE_UPLOAD.FileName);
                    TXT_FILE_UPLOAD.SaveAs(filePath);

                    // Membaca data dari file Excel
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        #region Claim GLIFE
                        if (DDL_TYPE.SelectedValue == "claim")
                        {
                            var workbook = package.Workbook;
                            if (workbook == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                                return;
                            }

                            var worksheet = workbook.Worksheets["CLAIM"];
                            if (worksheet == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                                return;
                            }

                            // Contoh membaca data dari cell
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Cells[row, 1].Text != "")
                                {
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text) || string.IsNullOrEmpty(worksheet.Cells[row, 25].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: terdapat data yang tidak sesuai.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }

                                    string ID = worksheet.Cells[row, 1].Text; // ID
                                    string REAS_KOAS_NAME = worksheet.Cells[row, 2].Text;
                                    string NAMA_PESERTA = worksheet.Cells[row, 3].Text;
                                    string NO_POLIS = worksheet.Cells[row, 4].Text;
                                    string NAMA_LEMBAGA = worksheet.Cells[row, 5].Text;
                                    string AWAL_KONTRAK = worksheet.Cells[row, 6].Text;
                                    string AKHIR_KONTRAK = worksheet.Cells[row, 7].Text;
                                    string TGL_LAHIR = worksheet.Cells[row, 8].Text;
                                    string TGL_MENINGGAL = worksheet.Cells[row, 9].Text;
                                    string TGL_KLAIM = worksheet.Cells[row, 10].Text;
                                    string MANFAAT = worksheet.Cells[row, 11].Text;
                                    string BEBAN_REAS = worksheet.Cells[row, 12].Text;
                                    string JUMLAH_KLAIM = worksheet.Cells[row, 13].Text;
                                    string SHARE_REAS = worksheet.Cells[row, 14].Text;
                                    string KETERANGAN = worksheet.Cells[row, 15].Text;
                                    string NO_KLAIM = worksheet.Cells[row, 16].Text;
                                    string MONTH = worksheet.Cells[row, 17].Text;
                                    string YEAR = worksheet.Cells[row, 18].Text;
                                    string NP_TREATY = worksheet.Cells[row, 19].Text;
                                    string LEMBAGA = worksheet.Cells[row, 20].Text;
                                    string SHARE = worksheet.Cells[row, 21].Text;
                                    string PERIODE_LAPORAN = worksheet.Cells[row, 22].Text;
                                    string CHANNEL = worksheet.Cells[row, 23].Text;
                                    string STATUS_ATK = worksheet.Cells[row, 24].Text;
                                    string PERIODE_PELAPORAN = worksheet.Cells[row, 25].Text;
                                    string STATUS_DATA = "VERIFIED";
                                    string VERIFIED_DATE = DateTime.Now.ToShortDateString();

                                    // Simpan ke database
                                    conn.QueryString = "EXEC [dbo].[SP_RTF_VALIDASI_CLAIMGLIFE] " +
                                        "'" + ID + "'," +
                                        "'" + REAS_KOAS_NAME + "'," +
                                        "'" + NAMA_PESERTA + "'," +
                                        "'" + NO_POLIS + "'," +
                                        "'" + NAMA_LEMBAGA + "'," +
                                        "'" + AWAL_KONTRAK + "'," +
                                        "'" + AKHIR_KONTRAK + "'," +
                                        "'" + TGL_LAHIR + "'," +
                                        "'" + TGL_MENINGGAL + "'," +
                                        "'" + TGL_KLAIM + "'," +
                                        "" + MANFAAT + "," +
                                        "" + BEBAN_REAS + "," +
                                        "" + JUMLAH_KLAIM + "," +
                                        "" + SHARE_REAS + "," +
                                        "'" + KETERANGAN + "'," +
                                        "'" + NO_KLAIM + "'," +
                                        "" + MONTH + "," +
                                        "" + YEAR + "," +
                                        "'" + NP_TREATY + "'," +
                                        "'" + LEMBAGA + "'," +
                                        "" + SHARE + "," +
                                        "'" + PERIODE_LAPORAN + "'," +
                                        "'" + CHANNEL + "'," +
                                        "'" + STATUS_ATK + "'," +
                                        "'" + PERIODE_PELAPORAN + "'," +
                                        "'" + STATUS_DATA + "'," +
                                        "'" + VERIFIED_DATE + "'";
                                    conn.QueryString = conn.QueryString.Replace("'NULL'", "NULL");
                                    conn.QueryString = conn.QueryString.Replace("''", "NULL");
                                    conn.ExecuteQuery();


                                    if (conn.GetFieldValue("NOTE").ToString() == "Data tidak di temukkan dan gagal di ubah.")
                                    {
                                        // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                        uploadedDataList.Add(new UploadedDataList
                                        {
                                            ID = ID,
                                            REAS_NAME = conn.GetFieldValue("REAS_KOAS_NAME").ToString(),
                                            NOTE = conn.GetFieldValue("NOTE").ToString()
                                        });

                                    }
                                }
                            }

                            LBL_PATH_FILE.Text = filePath.ToString();

                            int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah.")).Count();
                            if (errorUpload > 0)
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b>, yang tidak di temukkan dan gagal dirubah.";
                            }
                            else
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Data berhasil di rubah.";
                            }

                            // Isi data ke GridView
                            GV_UploadedData.DataSource = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah."));
                            GV_UploadedData.DataBind();

                            // Tampilkan modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                            return;
                        }
                        else if (DDL_TYPE.SelectedValue == "refund" )
                        {
                            var workbook = package.Workbook;
                            if (workbook == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                                return;
                            }

                            var worksheet = workbook.Worksheets["REFUND"];
                            if (worksheet == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                                return;
                            }

                            // Contoh membaca data dari cell
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Cells[row, 1].Text != "")
                                {
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }
                                    //KOMEN 2 Maret 2026
                                    //if (string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) || string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                                    //{
                                    //    string errorMessage = "Terjadi kesalahan saat membaca file: terdapat data yang tidak sesuai.";
                                    //    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    //    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    //    return;
                                    //}

                                    string ID = worksheet.Cells[row, 1].Text;
                                    string REAS_KOAS_NAME = worksheet.Cells[row, 2].Text;
                                    string NAMA_PESERTA = worksheet.Cells[row, 3].Text;
                                    string NO_POLIS = worksheet.Cells[row, 4].Text;
                                    string NAMA_LEMBAGA = worksheet.Cells[row, 5].Text;
                                    string AWAL_KONTRAK = worksheet.Cells[row, 6].Text;
                                    string AKHIR_KONTRAK = worksheet.Cells[row, 7].Text;
                                    string TGL_LAHIR = worksheet.Cells[row, 8].Text;
                                    string TGL_KLAIM = worksheet.Cells[row, 9].Text;
                                    string TGL_PELUNASAN = worksheet.Cells[row, 10].Text;
                                    string MANFAAT = worksheet.Cells[row, 11].Text;
                                    string BEBAN_REAS = worksheet.Cells[row, 12].Text;
                                    string JUMLAH_KLAIM = worksheet.Cells[row, 13].Text;
                                    string SHARE_REAS = worksheet.Cells[row, 14].Text;
                                    string KETERANGAN = worksheet.Cells[row, 15].Text;
                                    string NO_KLAIM = worksheet.Cells[row, 16].Text;
                                    string MONTH = worksheet.Cells[row, 17].Text;
                                    string YEAR = worksheet.Cells[row, 18].Text;
                                    string NP_TREATY = worksheet.Cells[row, 19].Text;
                                    string NP_TREATY_DESCR = worksheet.Cells[row, 20].Text;
                                    string SHARE = worksheet.Cells[row, 21].Text;
                                    string PERIODE_LAPORAN = worksheet.Cells[row, 22].Text;
                                    string CHANNEL = worksheet.Cells[row, 23].Text;
                                    string STATUS_ATK = worksheet.Cells[row, 24].Text;
                                    string PERIODE_PELAPORAN_WEEKLY = worksheet.Cells[row, 25].Text;
                                    string STATUS_DATA = "VERIFIED";
                                    string VERIFIED_DATE = DateTime.Now.ToShortDateString();

                                    // Simpan ke database
                                    conn.QueryString = "EXEC [dbo].[SP_RTF_VALIDASI_PRODUKSI_REFUNDGLIFE] " +
                                          "'" + ID + "'," +
                                          "'" + REAS_KOAS_NAME + "'," +
                                          "'" + NAMA_PESERTA + "'," +
                                          "'" + NO_POLIS + "'," +
                                          "'" + NAMA_LEMBAGA + "'," +
                                          "'" + AWAL_KONTRAK + "'," +
                                          "'" + AKHIR_KONTRAK + "'," +
                                          "'" + TGL_LAHIR + "'," +
                                          "'" + TGL_KLAIM + "'," +
                                          "'" + TGL_PELUNASAN + "'," +
                                          "" + MANFAAT + "," +
                                          "" + BEBAN_REAS + "," +
                                          "" + JUMLAH_KLAIM + "," +
                                          "" + SHARE_REAS + "," +
                                          "'" + KETERANGAN + "'," +
                                          "'" + NO_KLAIM + "'," +
                                          "" + MONTH + "," +
                                          "" + YEAR + "," +
                                          "'" + NP_TREATY + "'," +
                                          "'" + NP_TREATY_DESCR + "'," +
                                          "" + SHARE + "," +
                                          "'" + PERIODE_LAPORAN + "'," +
                                          "'" + CHANNEL + "'," +
                                          "'" + STATUS_ATK + "'," +
                                          "'" + PERIODE_PELAPORAN_WEEKLY + "'," +
                                          "'" + STATUS_DATA + "'," +
                                          "'" + VERIFIED_DATE + "'";
                                    conn.QueryString = conn.QueryString.Replace("'NULL'", "NULL");
                                    conn.QueryString = conn.QueryString.Replace("''", "NULL");
                                    conn.ExecuteQuery();


                                    if (conn.GetFieldValue("NOTE").ToString() == "Data tidak di temukkan dan gagal di ubah.")
                                    {
                                        // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                        uploadedDataList.Add(new UploadedDataList
                                        {
                                            ID = ID,
                                            REAS_NAME = conn.GetFieldValue("REAS_KOAS_NAME").ToString(),
                                            NOTE = conn.GetFieldValue("NOTE").ToString()
                                        });

                                    }
                                }
                            }

                            LBL_PATH_FILE.Text = filePath.ToString();

                            int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah.")).Count();
                            if (errorUpload > 0)
                            {
                                LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b>, yang tidak di temukkan dan gagal dirubah.";
                            }
                            else
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Data berhasil di rubah.";
                            }

                            // Isi data ke GridView
                            GV_UploadedData.DataSource = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah."));
                            GV_UploadedData.DataBind();

                            // Tampilkan modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                            return;
                        }
                        else if (DDL_TYPE.SelectedValue == "contribution_renewal" )
                        {
                            var workbook = package.Workbook;
                            if (workbook == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                                return;
                            }

                            var worksheet = workbook.Worksheets["CONTRIBUTION RENEWAL"];
                            if (worksheet == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                                return;
                            }

                            // Contoh membaca data dari cell
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Cells[row, 1].Text != "")
                                {
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) || string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: terdapat data yang tidak sesuai.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }

                                    string ID = worksheet.Cells[row, 1].Text;
                                    string NOMOR_POLIS = worksheet.Cells[row, 2].Text;
                                    string NAMA_LEMBAGA = worksheet.Cells[row, 3].Text;
                                    string NOMOR_PESERTA = worksheet.Cells[row, 4].Text;
                                    string NAMA_PESERTA = worksheet.Cells[row, 5].Text;
                                    string KODE_PRODUK = worksheet.Cells[row, 6].Text;
                                    string NAMA_PRODUK = worksheet.Cells[row, 7].Text;
                                    string JENIS_PRODUK = worksheet.Cells[row, 8].Text;
                                    string KODE_VALUTA = worksheet.Cells[row, 9].Text;
                                    string AWAL_KONTRAK = worksheet.Cells[row, 10].Text;
                                    string AKHIR_KONTRAK = worksheet.Cells[row, 11].Text;
                                    string TANGGAL_PRODUKSI = worksheet.Cells[row, 12].Text;
                                    string CARA_BAYAR = worksheet.Cells[row, 13].Text;
                                    string RETAK_TYPE = worksheet.Cells[row, 14].Text;
                                    string USIA_PASANGAN = worksheet.Cells[row, 15].Text;
                                    string SUMINS = worksheet.Cells[row, 16].Text;
                                    string DOB = worksheet.Cells[row, 17].Text;
                                    string START_AGE = worksheet.Cells[row, 18].Text;
                                    string TENOR_BULAN = worksheet.Cells[row, 19].Text;
                                    string TENOR_TAHUN = worksheet.Cells[row, 20].Text;
                                    string STATUS_UW = worksheet.Cells[row, 21].Text;
                                    string RATE_EM = worksheet.Cells[row, 22].Text;
                                    string RATE_EP = worksheet.Cells[row, 23].Text;
                                    string COMPANY_NAME = worksheet.Cells[row, 24].Text;
                                    string REINS_TC_DESC = worksheet.Cells[row, 25].Text;
                                    string DOCNO = worksheet.Cells[row, 26].Text;
                                    string PREMIUM_RATE = worksheet.Cells[row, 27].Text;
                                    string REINS_RATE = worksheet.Cells[row, 28].Text;
                                    string TOTAL_PERSEN_REAS_SHARE = worksheet.Cells[row, 29].Text;
                                    string TOTAL_REAS_SHARE = worksheet.Cells[row, 30].Text;
                                    string LOADING = worksheet.Cells[row, 31].Text;
                                    string KONTRIBUSI_REAS_STANDAR = worksheet.Cells[row, 32].Text;
                                    string KONTRIBUSI_EM = worksheet.Cells[row, 33].Text;
                                    string KONTRIBUSI_EP = worksheet.Cells[row, 34].Text;
                                    string KONTRIBUSI_RE = worksheet.Cells[row, 35].Text;
                                    string TABARRU_RE = worksheet.Cells[row, 36].Text;
                                    string UJROH_RE = worksheet.Cells[row, 37].Text;
                                    string PENURUNAN_RESIKO = worksheet.Cells[row, 38].Text;
                                    string PENURUNAN_SISA_BULAN = worksheet.Cells[row, 39].Text;
                                    string RENEWAL_FLAG = worksheet.Cells[row, 40].Text;
                                    string MONTH_PROD = worksheet.Cells[row, 41].Text;
                                    string STATUS_DATA = "VERIFIED";
                                    string VERIFIED_DATE = DateTime.Now.ToShortDateString();

                                    // Simpan ke database
                                    conn.QueryString = "EXEC [dbo].[SP_RTF_VALIDASI_PRODUKSI_RENEWALGLIFE] " +
                                        "'" + ID + "'," +
                                        "'" + NOMOR_POLIS + "'," +
                                        "'" + NAMA_LEMBAGA + "'," +
                                        "'" + NOMOR_PESERTA + "'," +
                                        "'" + NAMA_PESERTA + "'," +
                                        "'" + KODE_PRODUK + "'," +
                                        "'" + NAMA_PRODUK + "'," +
                                        "'" + JENIS_PRODUK + "'," +
                                        "'" + KODE_VALUTA + "'," +
                                        "'" + AWAL_KONTRAK + "'," +
                                        "'" + AKHIR_KONTRAK + "'," +
                                        "'" + TANGGAL_PRODUKSI + "'," +
                                        "'" + CARA_BAYAR + "'," +
                                        "'" + RETAK_TYPE + "'," +
                                        "" + USIA_PASANGAN + "," +
                                        "" + SUMINS + "," +
                                        "'" + DOB + "'," +
                                        "" + START_AGE + "," +
                                        "" + TENOR_BULAN + "," +
                                        "" + TENOR_TAHUN + "," +
                                        "'" + STATUS_UW + "'," +
                                        "" + RATE_EM + "," +
                                        "" + RATE_EP + "," +
                                        "'" + COMPANY_NAME + "'," +
                                        "'" + REINS_TC_DESC + "'," +
                                        "'" + DOCNO + "'," +
                                        "'" + PREMIUM_RATE + "'," +
                                        "" + REINS_RATE + "," +
                                        "" + TOTAL_PERSEN_REAS_SHARE + "," +
                                        "" + TOTAL_REAS_SHARE + "," +
                                        "" + LOADING + "," +
                                        "" + KONTRIBUSI_REAS_STANDAR + "," +
                                        "" + KONTRIBUSI_EM + "," +
                                        "" + KONTRIBUSI_EP + "," +
                                        "" + KONTRIBUSI_RE + "," +
                                        "" + TABARRU_RE + "," +
                                        "" + UJROH_RE + "," +
                                        "" + PENURUNAN_RESIKO + "," +
                                        "" + PENURUNAN_SISA_BULAN + "," +
                                        "'" + RENEWAL_FLAG + "'," +
                                        "'" + MONTH_PROD + "'," +
                                        "'" + STATUS_DATA + "'," +
                                        "'" + VERIFIED_DATE + "'";
                                    conn.QueryString = conn.QueryString.Replace("'NULL'", "NULL");
                                    conn.QueryString = conn.QueryString.Replace("''", "NULL");
                                    conn.ExecuteQuery();


                                    if (conn.GetFieldValue("NOTE").ToString() == "Data tidak di temukkan dan gagal di ubah.")
                                    {
                                        // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                        uploadedDataList.Add(new UploadedDataList
                                        {
                                            ID = ID,
                                            REAS_NAME = conn.GetFieldValue("NOMOR_POLIS").ToString(),
                                            NOTE = conn.GetFieldValue("NOTE").ToString()
                                        });

                                    }
                                }
                            }

                            LBL_PATH_FILE.Text = filePath.ToString();

                            int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah.")).Count();
                            if (errorUpload > 0)
                            {
                                LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b>, yang tidak di temukkan dan gagal dirubah.";
                            }
                            else
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Data berhasil di rubah.";
                            }

                            // Isi data ke GridView
                            GV_UploadedData.DataSource = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah."));
                            GV_UploadedData.DataBind();

                            // Tampilkan modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                            return;
                        }
                        else if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                        {
                            var workbook = package.Workbook;
                            if (workbook == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                                return;
                            }

                            var worksheet = workbook.Worksheets["CONTRIBUTION GTLR"];
                            if (worksheet == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                                return;
                            }

                            // Contoh membaca data dari cell
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Cells[row, 1].Text != "")
                                {
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) || string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: terdapat data yang tidak sesuai.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }

                                    string ID = worksheet.Cells[row, 1].Text;
                                    string NOMOR_POLIS = worksheet.Cells[row, 2].Text;
                                    string NAMA_LEMBAGA = worksheet.Cells[row, 3].Text;
                                    string NOMOR_PESERTA = worksheet.Cells[row, 4].Text;
                                    string NAMA_PESERTA = worksheet.Cells[row, 5].Text;
                                    string KODE_PRODUK = worksheet.Cells[row, 6].Text;
                                    string NAMA_PRODUK = worksheet.Cells[row, 7].Text;
                                    string JENIS_PRODUK = worksheet.Cells[row, 8].Text;
                                    string KODE_VALUTA = worksheet.Cells[row, 9].Text;
                                    string AWAL_KONTRAK = worksheet.Cells[row, 10].Text;
                                    string AKHIR_KONTRAK = worksheet.Cells[row, 11].Text;
                                    string TANGGAL_PRODUKSI = worksheet.Cells[row, 12].Text;
                                    string KODE_CARA_BAYAR = worksheet.Cells[row, 13].Text;
                                    string CARA_BAYAR = worksheet.Cells[row, 14].Text;
                                    string RETAK_TYPE = worksheet.Cells[row, 15].Text;
                                    string NAMA_REAS_COMPANY = worksheet.Cells[row, 16].Text;
                                    string AGREEMENT_QS = worksheet.Cells[row, 17].Text;
                                    string AGREEMENT_OR = worksheet.Cells[row, 18].Text;
                                    string PERSEN_SHARE_REAS_PPQS = worksheet.Cells[row, 19].Text;
                                    string PERSEN_SHARE_REAS_PPS = worksheet.Cells[row, 20].Text;
                                    string PERSEN_SHARE_REAS_PPF = worksheet.Cells[row, 21].Text;
                                    string RATE_REAS = worksheet.Cells[row, 22].Text;
                                    string PREMI_RE = worksheet.Cells[row, 23].Text;
                                    string OWN_RETENTION = worksheet.Cells[row, 24].Text;
                                    string AMOUNT_FACULTATIVE = worksheet.Cells[row, 25].Text;
                                    string USIA_PASANGAN = worksheet.Cells[row, 26].Text;
                                    string SUMINS = worksheet.Cells[row, 27].Text;
                                    string DOB = worksheet.Cells[row, 28].Text;
                                    string START_AGE = worksheet.Cells[row, 29].Text;
                                    string TENOR_TAHUN = worksheet.Cells[row, 30].Text;
                                    string TENOR_BULAN = worksheet.Cells[row, 31].Text;
                                    string STATUS_UW = worksheet.Cells[row, 32].Text;
                                    string NAMA_PASANGAN = worksheet.Cells[row, 33].Text;
                                    string DOB_PASANGAN = worksheet.Cells[row, 34].Text;
                                    string START_AGE_SPOUSE = worksheet.Cells[row, 35].Text;
                                    string RATE_EM = worksheet.Cells[row, 36].Text;
                                    string RATE_EP = worksheet.Cells[row, 37].Text;
                                    string CHK_POL_TERM = worksheet.Cells[row, 38].Text;
                                    string LAST_POL_ANNIV = worksheet.Cells[row, 39].Text;
                                    string POL_TERM_WHOLE_YR = worksheet.Cells[row, 40].Text;
                                    string POL_TERM_WHOLE_MONTH = worksheet.Cells[row, 41].Text;
                                    string LAST_END_OF_WHOLE_MONTH = worksheet.Cells[row, 42].Text;
                                    string POL_TERM_WHOLE_DAY = worksheet.Cells[row, 43].Text;
                                    string POL_TERM_IN_MONTH = worksheet.Cells[row, 44].Text;

                                    string SUM_ASSURED = worksheet.Cells[row, 45].Text;
                                    string SETORAN_BULANAN = worksheet.Cells[row, 46].Text;
                                    string SALDO_TABUNGAN = worksheet.Cells[row, 47].Text;

                                    string COMPANY_NAME = worksheet.Cells[row, 48].Text;
                                    string DESCR = worksheet.Cells[row, 49].Text;
                                    string DOCNO = worksheet.Cells[row, 50].Text;
                                    string RATE_DESCR = worksheet.Cells[row, 51].Text;
                                    string TENOR_CODE = worksheet.Cells[row, 52].Text;
                                    string RATE_FINAL = worksheet.Cells[row, 53].Text;
                                    string TOTAL_PCT_REAS_SHARE = worksheet.Cells[row, 54].Text;
                                    string TOTAL_REAS_SHARE = worksheet.Cells[row, 55].Text;
                                    string LOADING = worksheet.Cells[row, 56].Text;
                                    string KONTRIBUSI_REAS_STANDAR = worksheet.Cells[row, 57].Text;
                                    string PCT_EM = worksheet.Cells[row, 58].Text;
                                    string KONTRIBUSI_EM = worksheet.Cells[row, 59].Text;
                                    string PCT_EP = worksheet.Cells[row, 60].Text;
                                    string KONTRIBUSI_EP = worksheet.Cells[row, 61].Text;
                                    string KONTRIBUSI_RE = worksheet.Cells[row, 62].Text;
                                    string TABARRU_RE = worksheet.Cells[row, 63].Text;
                                    string UJROH_RE = worksheet.Cells[row, 64].Text;
                                    string MONTH_PROD = worksheet.Cells[row, 65].Text;
                                    string PENURUNAN_RESIKO = worksheet.Cells[row, 66].Text;
                                    string PENURUNAN_SISA_BULAN = worksheet.Cells[row, 67].Text;
                                    string LINI_USAHA_OJK = worksheet.Cells[row, 68].Text;

                                    string STATUS_DATA = "VERIFIED";
                                    string VERIFIED_DATE = DateTime.Now.ToShortDateString();

                                    // Simpan ke database
                                    conn.QueryString = "EXEC [dbo].[SP_RTF_VALIDASI_PRODUKSI_GTLR_GLIFE] " +
                                        "'" + ID                        + "'," + 
                                        "'" + NOMOR_POLIS               + "'," +
                                        "'" + NAMA_LEMBAGA              + "'," +
                                        "'" + NOMOR_PESERTA             + "'," +
                                        "'" + NAMA_PESERTA              + "'," +
                                        "'" + KODE_PRODUK               + "'," +
                                        "'" + NAMA_PRODUK               + "'," +
                                        "'" + JENIS_PRODUK              + "'," +
                                        "'" + KODE_VALUTA               + "'," +
                                        "'" + AWAL_KONTRAK              + "'," +
                                        "'" + AKHIR_KONTRAK             + "'," +
                                        "'" + TANGGAL_PRODUKSI          + "'," +
                                        "'" + KODE_CARA_BAYAR           + "'," +
                                        "'" + CARA_BAYAR                + "'," +
                                        "'" + RETAK_TYPE                + "'," +
                                        "'" + NAMA_REAS_COMPANY         + "'," +
                                        "'" + AGREEMENT_QS              + "'," +
                                        "'" + AGREEMENT_OR              + "'," +
                                        "" + PERSEN_SHARE_REAS_PPQS    + "," +
                                        "" + PERSEN_SHARE_REAS_PPS     + "," +
                                        "" + PERSEN_SHARE_REAS_PPF     + "," +
                                        "" + RATE_REAS                 + "," +
                                        "" + PREMI_RE                  + "," +
                                        "" + OWN_RETENTION             + "," +
                                        "" + AMOUNT_FACULTATIVE        + "," +
                                        "'" + USIA_PASANGAN             + "'," +
                                        "" + SUMINS                    + "," +
                                        "'" + DOB                       + "'," +
                                        "" + START_AGE                 + "," +
                                        "" + TENOR_TAHUN               + "," +
                                        "" + TENOR_BULAN               + "," +
                                        "'" + STATUS_UW                 + "'," +
                                        "'" + NAMA_PASANGAN             + "'," +
                                        "'" + DOB_PASANGAN              + "'," +
                                        "" + START_AGE_SPOUSE          + "," +
                                        "" + RATE_EM                   + "," +
                                        "" + RATE_EP                   + "," +
                                        "'" + CHK_POL_TERM              + "'," +
                                        "'" + LAST_POL_ANNIV            + "'," +
                                        "" + POL_TERM_WHOLE_YR         + "," +
                                        "" + POL_TERM_WHOLE_MONTH      + "," +
                                        "'" + LAST_END_OF_WHOLE_MONTH   + "'," +
                                        "" + POL_TERM_WHOLE_DAY        + "," +
                                        "" + POL_TERM_IN_MONTH         + "," +

                                        "" + SUM_ASSURED + "," +
                                        "" + SETORAN_BULANAN + "," +
                                        "" + SALDO_TABUNGAN + "," +


                                        "'" + COMPANY_NAME              + "'," +
                                        "'" + DESCR                     + "'," +
                                        "'" + DOCNO                     + "'," +
                                        "'" + RATE_DESCR                + "'," +
                                        "'" + TENOR_CODE                + "'," +
                                        "" + RATE_FINAL                + "," +
                                        "" + TOTAL_PCT_REAS_SHARE      + "," +
                                        "" + TOTAL_REAS_SHARE          + "," +
                                        "" + LOADING                   + "," +
                                        "" + KONTRIBUSI_REAS_STANDAR   + "," +
                                        "" + PCT_EM                    + "," +
                                        "" + KONTRIBUSI_EM             + "," +
                                        "" + PCT_EP                    + "," +
                                        "" + KONTRIBUSI_EP             + "," +
                                        "" + KONTRIBUSI_RE             + "," +
                                        "" + TABARRU_RE                + "," +
                                        "" + UJROH_RE                  + "," +
                                        "'" + MONTH_PROD                + "'," +
                                        "" + PENURUNAN_RESIKO          + "," +
                                        "" + PENURUNAN_SISA_BULAN      + "," +
                                        "'" + LINI_USAHA_OJK            + "'," +
                                        "'" + STATUS_DATA + "'," +
                                        "'" + VERIFIED_DATE + "'";
                                    conn.QueryString = conn.QueryString.Replace("'NULL'", "NULL");
                                    conn.QueryString = conn.QueryString.Replace("''", "NULL");
                                    conn.ExecuteQuery();


                                    if (conn.GetFieldValue("NOTE").ToString() == "Data tidak di temukkan dan gagal di ubah.")
                                    {
                                        // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                        uploadedDataList.Add(new UploadedDataList
                                        {
                                            ID = ID,
                                            REAS_NAME = conn.GetFieldValue("REAS_KOAS_NAME").ToString(),
                                            NOTE = conn.GetFieldValue("NOTE").ToString()
                                        });

                                    }
                                }
                            }

                            LBL_PATH_FILE.Text = filePath.ToString();

                            int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah.")).Count();
                            if (errorUpload > 0)
                            {
                                LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b>, yang tidak di temukkan dan gagal dirubah.";
                            }
                            else
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Data berhasil di rubah.";
                            }

                            // Isi data ke GridView
                            GV_UploadedData.DataSource = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah."));
                            GV_UploadedData.DataBind();

                            // Tampilkan modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                            return;
                        }
                        else if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                        {
                            var workbook = package.Workbook;
                            if (workbook == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                                return;
                            }

                            var worksheet = workbook.Worksheets["CONTRIBUTION NON GTLR"];
                            if (worksheet == null)
                            {
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                                return;
                            }

                            // Contoh membaca data dari cell
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Cells[row, 1].Text != "")
                                {
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) || string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                                    {
                                        string errorMessage = "Terjadi kesalahan saat membaca file: terdapat data yang tidak sesuai.";
                                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                                        LB_ERR.Text = errorMessage + Environment.NewLine;
                                        return;
                                    }

                                    string ID = worksheet.Cells[row, 1].Text;
                                    string NOMOR_POLIS = worksheet.Cells[row, 2].Text;
                                    string NAMA_LEMBAGA = worksheet.Cells[row, 3].Text;
                                    string NOMOR_PESERTA = worksheet.Cells[row, 4].Text;
                                    string NAMA_PESERTA = worksheet.Cells[row, 5].Text;
                                    string KODE_PRODUK = worksheet.Cells[row, 6].Text;
                                    string NAMA_PRODUK = worksheet.Cells[row, 7].Text;
                                    string JENIS_PRODUK = worksheet.Cells[row, 8].Text;
                                    string KODE_VALUTA = worksheet.Cells[row, 9].Text;
                                    string AWAL_KONTRAK = worksheet.Cells[row, 10].Text;
                                    string AKHIR_KONTRAK = worksheet.Cells[row, 11].Text;
                                    string TANGGAL_PRODUKSI = worksheet.Cells[row, 12].Text;
                                    string KODE_CARA_BAYAR = worksheet.Cells[row, 13].Text;
                                    string CARA_BAYAR = worksheet.Cells[row, 14].Text;
                                    string RETAK_TYPE = worksheet.Cells[row, 15].Text;
                                    string NAMA_REAS_COMPANY = worksheet.Cells[row, 16].Text;
                                    string AGREEMENT_QS = worksheet.Cells[row, 17].Text;
                                    string AGREEMENT_OR = worksheet.Cells[row, 18].Text;
                                    string PERSEN_SHARE_REAS_PPQS = worksheet.Cells[row, 19].Text;
                                    string PERSEN_SHARE_REAS_PPS = worksheet.Cells[row, 20].Text;
                                    string PERSEN_SHARE_REAS_PPF = worksheet.Cells[row, 21].Text;
                                    string RATE_REAS = worksheet.Cells[row, 22].Text;
                                    string PREMI_RE = worksheet.Cells[row, 23].Text;
                                    string OWN_RETENTION = worksheet.Cells[row, 24].Text;
                                    string AMOUNT_FACULTATIVE = worksheet.Cells[row, 25].Text;
                                    string USIA_PASANGAN = worksheet.Cells[row, 26].Text;
                                    string SUMINS = worksheet.Cells[row, 27].Text;
                                    string DOB = worksheet.Cells[row, 28].Text;
                                    string START_AGE = worksheet.Cells[row, 29].Text;
                                    string TENOR_TAHUN = worksheet.Cells[row, 30].Text;
                                    string TENOR_BULAN = worksheet.Cells[row, 31].Text;
                                    string STATUS_UW = worksheet.Cells[row, 32].Text;
                                    string NAMA_PASANGAN = worksheet.Cells[row, 33].Text;
                                    string DOB_PASANGAN = worksheet.Cells[row, 34].Text;
                                    string START_AGE_SPOUSE = worksheet.Cells[row, 35].Text;
                                    string RATE_EM = worksheet.Cells[row, 36].Text;
                                    string RATE_EP = worksheet.Cells[row, 37].Text;
                                    string CHK_POL_TERM = worksheet.Cells[row, 38].Text;
                                    string LAST_POL_ANNIV = worksheet.Cells[row, 39].Text;
                                    string POL_TERM_WHOLE_YR = worksheet.Cells[row, 40].Text;
                                    string POL_TERM_WHOLE_MONTH = worksheet.Cells[row, 41].Text;
                                    string LAST_END_OF_WHOLE_MONTH = worksheet.Cells[row, 42].Text;
                                    string POL_TERM_WHOLE_DAY = worksheet.Cells[row, 43].Text;
                                    string POL_TERM_IN_MONTH = worksheet.Cells[row, 44].Text;
                                    string COMPANY_NAME = worksheet.Cells[row, 45].Text;
                                    string DESCR = worksheet.Cells[row, 46].Text;
                                    string DOCNO = worksheet.Cells[row, 47].Text;
                                    string RATE_DESCR = worksheet.Cells[row, 48].Text;
                                    string TENOR_CODE = worksheet.Cells[row, 49].Text;
                                    string RATE_FINAL = worksheet.Cells[row, 50].Text;
                                    string TOTAL_PCT_REAS_SHARE = worksheet.Cells[row, 51].Text;
                                    string TOTAL_REAS_SHARE = worksheet.Cells[row, 52].Text;
                                    string LOADING = worksheet.Cells[row, 53].Text;
                                    string KONTRIBUSI_REAS_STANDAR = worksheet.Cells[row, 54].Text;
                                    string PCT_EM = worksheet.Cells[row, 55].Text;
                                    string KONTRIBUSI_EM = worksheet.Cells[row, 56].Text;
                                    string PCT_EP = worksheet.Cells[row, 57].Text;
                                    string KONTRIBUSI_EP = worksheet.Cells[row, 58].Text;
                                    string KONTRIBUSI_RE = worksheet.Cells[row, 59].Text;
                                    string TABARRU_RE = worksheet.Cells[row, 60].Text;
                                    string UJROH_RE = worksheet.Cells[row, 61].Text;
                                    string MONTH_PROD = worksheet.Cells[row, 62].Text;
                                    string PENURUNAN_RESIKO = worksheet.Cells[row, 63].Text;
                                    string PENURUNAN_SISA_BULAN = worksheet.Cells[row, 64].Text;
                                    string LINI_USAHA_OJK = worksheet.Cells[row, 65].Text;

                                    string STATUS_DATA = "VERIFIED";
                                    string VERIFIED_DATE = DateTime.Now.ToShortDateString();

                                    // Simpan ke database
                                    conn.QueryString = "EXEC [dbo].[SP_RTF_VALIDASI_PRODUKSI_NONGTLR_GLIFE] " +
                                        "'" + ID + "'," +
                                        "'" + NOMOR_POLIS + "'," +
                                        "'" + NAMA_LEMBAGA + "'," +
                                        "'" + NOMOR_PESERTA + "'," +
                                        "'" + NAMA_PESERTA + "'," +
                                        "'" + KODE_PRODUK + "'," +
                                        "'" + NAMA_PRODUK + "'," +
                                        "'" + JENIS_PRODUK + "'," +
                                        "'" + KODE_VALUTA + "'," +
                                        "'" + AWAL_KONTRAK + "'," +
                                        "'" + AKHIR_KONTRAK + "'," +
                                        "'" + TANGGAL_PRODUKSI + "'," +
                                        "'" + KODE_CARA_BAYAR + "'," +
                                        "'" + CARA_BAYAR + "'," +
                                        "'" + RETAK_TYPE + "'," +
                                        "'" + NAMA_REAS_COMPANY + "'," +
                                        "'" + AGREEMENT_QS + "'," +
                                        "'" + AGREEMENT_OR + "'," +
                                        "" + PERSEN_SHARE_REAS_PPQS + "," +
                                        "" + PERSEN_SHARE_REAS_PPS + "," +
                                        "" + PERSEN_SHARE_REAS_PPF + "," +
                                        "" + RATE_REAS + "," +
                                        "" + PREMI_RE + "," +
                                        "" + OWN_RETENTION + "," +
                                        "" + AMOUNT_FACULTATIVE + "," +
                                        "'" + USIA_PASANGAN + "'," +
                                        "" + SUMINS + "," +
                                        "'" + DOB + "'," +
                                        "" + START_AGE + "," +
                                        "" + TENOR_TAHUN + "," +
                                        "" + TENOR_BULAN + "," +
                                        "'" + STATUS_UW + "'," +
                                        "'" + NAMA_PASANGAN + "'," +
                                        "'" + DOB_PASANGAN + "'," +
                                        "" + START_AGE_SPOUSE + "," +
                                        "" + RATE_EM + "," +
                                        "" + RATE_EP + "," +
                                        "'" + CHK_POL_TERM + "'," +
                                        "'" + LAST_POL_ANNIV + "'," +
                                        "" + POL_TERM_WHOLE_YR + "," +
                                        "" + POL_TERM_WHOLE_MONTH + "," +
                                        "'" + LAST_END_OF_WHOLE_MONTH + "'," +
                                        "" + POL_TERM_WHOLE_DAY + "," +
                                        "" + POL_TERM_IN_MONTH + "," +
                                        "'" + COMPANY_NAME + "'," +
                                        "'" + DESCR + "'," +
                                        "'" + DOCNO + "'," +
                                        "'" + RATE_DESCR + "'," +
                                        "'" + TENOR_CODE + "'," +
                                        "" + RATE_FINAL + "," +
                                        "" + TOTAL_PCT_REAS_SHARE + "," +
                                        "" + TOTAL_REAS_SHARE + "," +
                                        "" + LOADING + "," +
                                        "" + KONTRIBUSI_REAS_STANDAR + "," +
                                        "" + PCT_EM + "," +
                                        "" + KONTRIBUSI_EM + "," +
                                        "" + PCT_EP + "," +
                                        "" + KONTRIBUSI_EP + "," +
                                        "" + KONTRIBUSI_RE + "," +
                                        "" + TABARRU_RE + "," +
                                        "" + UJROH_RE + "," +
                                        "'" + MONTH_PROD + "'," +
                                        "" + PENURUNAN_RESIKO + "," +
                                        "" + PENURUNAN_SISA_BULAN + "," +
                                        "'" + LINI_USAHA_OJK + "'," +
                                        "'" + STATUS_DATA + "'," +
                                        "'" + VERIFIED_DATE + "'";
                                    conn.QueryString = conn.QueryString.Replace("'NULL'", "NULL");
                                    conn.QueryString = conn.QueryString.Replace("''", "NULL");
                                    conn.ExecuteQuery();


                                    if (conn.GetFieldValue("NOTE").ToString() == "Data tidak di temukkan dan gagal di ubah.")
                                    {
                                        // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                        uploadedDataList.Add(new UploadedDataList
                                        {
                                            ID = ID,
                                            REAS_NAME = conn.GetFieldValue("REAS_KOAS_NAME").ToString(),
                                            NOTE = conn.GetFieldValue("NOTE").ToString()
                                        });

                                    }
                                }
                            }

                            LBL_PATH_FILE.Text = filePath.ToString();

                            int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah.")).Count();
                            if (errorUpload > 0)
                            {
                                LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b>, yang tidak di temukkan dan gagal dirubah.";
                            }
                            else
                            {
                                btnDownload.Visible = false;
                                LBL_PROCESS.Text = "Data berhasil di rubah.";
                            }

                            // Isi data ke GridView
                            GV_UploadedData.DataSource = uploadedDataList.Where(x => x.NOTE.Contains("Data tidak di temukkan dan gagal di ubah."));
                            GV_UploadedData.DataBind();

                            // Tampilkan modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                            return;
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = "Terjadi kesalahan saat membaca file: " + ex.Message;

                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = errorMessage + Environment.NewLine;
                    return;
                }
            }
            else
            {
                // Pesan jika tidak ada file yang diunggah
                Response.Write("<script>alert('Silakan pilih file untuk diunggah.');window.location.href = window.location.href;</script>");

                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = "Silakan pilih file untuk diunggah." + Environment.NewLine;
                return;
            }
        }

        protected void LBT_TEMPLATE_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type report glife sebelum download dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            string fileName = "";
            if (DDL_TYPE.SelectedValue != "")
            {
                if (DDL_TYPE.SelectedValue == "refund")
                {
                    fileName = "UPLOAD VALIDASI DATA REAS GLIFE REFUND.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "claim")
                {
                    fileName = "UPLOAD VALIDASI DATA REAS GLIFE CLAIM.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                {
                    fileName = "UPLOAD VALIDASI DATA REAS GLIFE CONTRIBUTION NON GTLR.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                {
                    fileName = "UPLOAD VALIDASI DATA REAS GLIFE CONTRIBUTION GTLR.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "contribution_renewal")
                {
                    fileName = "UPLOAD VALIDASI DATA REAS GLIFE CONTRIBUTION RENEWAL.xlsx";  // Nama file yang akan diunduh
                }
            }

            // Tentukan path folder tempat file disimpan
            string folderPath = Server.MapPath("~/Content/Template/Validasi GLIFE/");
            string filePath = Path.Combine(folderPath, fileName);

            // Periksa apakah file ada
            if (File.Exists(filePath))
            {
                // Atur header response untuk mengunduh file
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

                // Tulis file ke output response stream
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {
                // Pesan jika file tidak ditemukan
                Response.Write("<script>alert('File tidak ditemukan.'); window.location.href = window.location.href;</script>");
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Data_" + DDL_TYPE.SelectedValue + "_Bermasalah_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            // Set the EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Retrieve data
            DataTable dt = GetDataFromGridView(GV_UploadedData);

            // Create Excel package
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Add a new worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List");

                // Add headers and information
                ws.Cells["A1"].Value = "Report";
                //ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": Data Bermasalah";
                //ws.Cells["B2"].Value = ": {StartDate} - {EndDate}";

                // Load data from DataTable
                if (dt != null && dt.Rows.Count > 0)
                {
                    ws.Cells["A4"].LoadFromDataTable(dt, true);
                    ws.Cells.AutoFitColumns(); // Autofit column widths
                }
                else
                {
                    ws.Cells["A4"].Value = "No data available for the selected period.";
                }

                // Save to memory stream
                using (var ms = new MemoryStream())
                {
                    pck.SaveAs(ms);
                    ms.Position = 0; // Reset stream position

                    // Write to response
                    ms.CopyTo(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush(); // Ensure all data is sent
                }
            }

            HttpContext.Current.Response.End(); // End the response
        }

        private DataTable GetDataFromGridView(System.Web.UI.WebControls.GridView gV_UploadedData)
        {
            DataTable dt = new DataTable();

            // Add columns to DataTable based on the GridView columns
            foreach (TableCell cell in gV_UploadedData.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }

            // Add rows from the GridView to the DataTable
            foreach (GridViewRow row in gV_UploadedData.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalScript", "closeModal();", true);
        }
        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}