using DMS.DBConnection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace REAS.Form_App
{
    public partial class UploadFinSettlementGLIFE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string uploadDate;
        List<UploadedDataList> uploadedDataList = new List<UploadedDataList>();
        string filePath;
        #endregion

        public class UploadedDataList
        {
            public string JOURNAL_NO { get; set; }
            public string ID { get; set; }
            public string POLICY_NO { get; set; }
            public string LAST_TECHNICAL_DATE { get; set; }
            public string NOTE { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "UPLOAD FINANCIAL SETTLEMENT GLIFE - REAS";

            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");

            uploadDate = currentDate.ToString("yyyy-MM-dd");
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type Settlement sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string filePath = Path.Combine(Server.MapPath("~/Upload/Settlement"), TXT_FILE_UPLOAD.FileName);
                    TXT_FILE_UPLOAD.SaveAs(filePath);

                    if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                    {
                        CheckContributionGTLRProcess(filePath);
                    }
                    else if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                    {
                        CheckContributionNonGTLRProcess(filePath);
                    }
                    else if (DDL_TYPE.SelectedValue == "contribution_renewal")
                    {
                        CheckContributionRenewalProcess(filePath);
                    }
                    else if (DDL_TYPE.SelectedValue == "refund")
                    {
                        CheckRefundProcess(filePath);
                    }
                    else if (DDL_TYPE.SelectedValue == "claim_glife")
                    {
                        CheckClaimProcess(filePath);
                    }


                }
                catch (Exception ex)
                {
                    string errorMessage = "Terjadi kesalahan saat membaca file: " + ex.Message;
                    //Response.Write("<script>alert('" + errorMessage + "');window.location.href = window.location.href;</script>");

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

        private void CheckClaimProcess(string filePath)
        {
            // Membaca data dari file Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (workbook == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                    return;
                }

                var worksheet = workbook.Worksheets["UPLOAD"];
                if (worksheet == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                    return;
                }

                // Contoh membaca data dari cell
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (DDL_TYPE.SelectedValue == "claim_glife")
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 10].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        string ID = worksheet.Cells[row, 1].Text; // ID
                        string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                        // Simpan ke database
                        conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_GLIFE_CLAIM_JOURNAL_TECHNICAL_MEMO] " +
                             "'" + ID.Trim() + "'," +
                             "'" + POLICY_NO + "'";
                        conn.ExecuteQuery();

                        // Menyimpan data ke dalam database
                        if (conn.GetFieldValue("NOTE").ToString() == "ID belum di Technical."
                            || conn.GetFieldValue("NOTE").ToString() == "ID sudah pernah di buat untuk Memo dan Memo masih On Process.")
                        {
                            // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                            uploadedDataList.Add(new UploadedDataList
                            {
                                ID = ID,
                                POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                NOTE = conn.GetFieldValue("NOTE").ToString()
                            });

                        }

                    }
                }

                LBL_PATH_FILE.Text = filePath.ToString();

                int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID belum di Technical.") || x.NOTE.Contains("ID sudah pernah di buat untuk Memo dan Memo masih On Process.")).Count();
                if (errorUpload > 0)
                {
                    btnProcess.Visible = false;
                    btnDownload.Visible = true;
                    LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Technical, <br /> Upload Settlement tidak dapat di proses sampai data Technical sudah di Journal terlebih dahulu atau Memo yang masih berjalan.";
                }
                else
                {
                    btnProcess.Visible = true;
                    btnDownload.Visible = false;
                    LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Settlement ?";
                }
                // Isi data ke GridView
                GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                return;
            }
        }

        private void CheckRefundProcess(string filePath)
        {
            // Membaca data dari file Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (workbook == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                    return;
                }

                var worksheet = workbook.Worksheets["UPLOAD"];
                if (worksheet == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                    return;
                }

                // Contoh membaca data dari cell
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (DDL_TYPE.SelectedValue == "refund")
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 10].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        string ID = worksheet.Cells[row, 1].Text; // ID
                        string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                        // Simpan ke database
                        conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_GLIFE_PRODUKSI_REFUND_JOURNAL_TECHNICAL_MEMO] " +
                             "'" + ID.Trim() + "'," +
                             "'" + POLICY_NO + "'";
                        conn.ExecuteQuery();

                        // Menyimpan data ke dalam database
                        if (conn.GetFieldValue("NOTE").ToString() == "ID belum di Technical."
                            || conn.GetFieldValue("NOTE").ToString() == "ID sudah pernah di buat untuk Memo dan Memo masih On Process.")
                        {
                            // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                            uploadedDataList.Add(new UploadedDataList
                            {
                                ID = ID,
                                POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                NOTE = conn.GetFieldValue("NOTE").ToString()
                            });

                        }

                    }
                }

                LBL_PATH_FILE.Text = filePath.ToString();

                int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID belum di Technical.") || x.NOTE.Contains("ID sudah pernah di buat untuk Memo dan Memo masih On Process.")).Count();
                if (errorUpload > 0)
                {
                    btnProcess.Visible = false;
                    btnDownload.Visible = true;
                    LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Technical, <br /> Upload Settlement tidak dapat di proses sampai data Technical sudah di Journal terlebih dahulu atau Memo yang masih berjalan.";
                }
                else
                {
                    btnProcess.Visible = true;
                    btnDownload.Visible = false;
                    LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Settlement ?";
                }
                // Isi data ke GridView
                GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                return;
            }
        }

        private void CheckContributionRenewalProcess(string filePath)
        {
            // Membaca data dari file Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (workbook == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                    return;
                }

                var worksheet = workbook.Worksheets["UPLOAD"];
                if (worksheet == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                    return;
                }

                // Contoh membaca data dari cell
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (DDL_TYPE.SelectedValue == "contribution_renewal")
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 15].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        string ID = worksheet.Cells[row, 1].Text; // ID
                        string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                        // Simpan ke database
                        conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_GLIFE_PRODUKSI_RENEWAL_JOURNAL_TECHNICAL_MEMO] " +
                             "'" + ID.Trim() + "'," +
                             "'" + POLICY_NO + "'";
                        conn.ExecuteQuery();

                        // Menyimpan data ke dalam database
                        if (conn.GetFieldValue("NOTE").ToString() == "ID belum di Technical."
                            || conn.GetFieldValue("NOTE").ToString() == "ID sudah pernah di buat untuk Memo dan Memo masih On Process.")
                        {
                            // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                            uploadedDataList.Add(new UploadedDataList
                            {
                                ID = ID,
                                POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                NOTE = conn.GetFieldValue("NOTE").ToString()
                            });

                        }

                    }
                }

                LBL_PATH_FILE.Text = filePath.ToString();

                int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID belum di Technical.") || x.NOTE.Contains("ID sudah pernah di buat untuk Memo dan Memo masih On Process.")).Count();
                if (errorUpload > 0)
                {
                    btnProcess.Visible = false;
                    btnDownload.Visible = true;
                    LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Technical, <br /> Upload Settlement tidak dapat di proses sampai data Technical sudah di Journal terlebih dahulu atau Memo yang masih berjalan.";
                }
                else
                {
                    btnProcess.Visible = true;
                    btnDownload.Visible = false;
                    LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Settlement ?";
                }
                // Isi data ke GridView
                GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                return;
            }
        }

        private void CheckContributionNonGTLRProcess(string filePath)
        {
            // Membaca data dari file Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (workbook == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                    return;
                }

                var worksheet = workbook.Worksheets["UPLOAD"];
                if (worksheet == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                    return;
                }

                // Contoh membaca data dari cell
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 15].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        string ID = worksheet.Cells[row, 1].Text; // ID
                        string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                        // Simpan ke database
                        conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_GLIFE_PRODUKSI_GTLR_NONGTLR_JOURNAL_TECHNICAL_MEMO] " +
                             "'" + ID.Trim() + "'," +
                             "'" + POLICY_NO + "'";
                        conn.ExecuteQuery();

                        // Menyimpan data ke dalam database
                        if (conn.GetFieldValue("NOTE").ToString() == "ID belum di Technical."
                            || conn.GetFieldValue("NOTE").ToString() == "ID sudah pernah di buat untuk Memo dan Memo masih On Process.")
                        {
                            // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                            uploadedDataList.Add(new UploadedDataList
                            {
                                ID = ID,
                                POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                NOTE = conn.GetFieldValue("NOTE").ToString()
                            });

                        }

                    }
                }

                LBL_PATH_FILE.Text = filePath.ToString();

                int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID belum di Technical.") || x.NOTE.Contains("ID sudah pernah di buat untuk Memo dan Memo masih On Process.")).Count();
                if (errorUpload > 0)
                {
                    btnProcess.Visible = false;
                    btnDownload.Visible = true;
                    LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Technical, <br /> Upload Settlement tidak dapat di proses sampai data Technical sudah di Journal terlebih dahulu atau Memo yang masih berjalan.";
                }
                else
                {
                    btnProcess.Visible = true;
                    btnDownload.Visible = false;
                    LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Settlement ?";
                }
                // Isi data ke GridView
                GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                return;
            }
        }

        private void CheckContributionGTLRProcess(string filePath)
        {
            // Membaca data dari file Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (workbook == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                    return;
                }

                var worksheet = workbook.Worksheets["UPLOAD"];
                if (worksheet == null)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                    return;
                }

                // Contoh membaca data dari cell
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                    {
                        if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        if (string.IsNullOrEmpty(worksheet.Cells[row, 15].Text))
                        {
                            string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = errorMessage + Environment.NewLine;
                            return;
                        }

                        string ID = worksheet.Cells[row, 1].Text; // ID
                        string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                        // Simpan ke database
                        conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_GLIFE_PRODUKSI_GTLR_NONGTLR_JOURNAL_TECHNICAL_MEMO] " +
                             "'" + ID.Trim() + "'," +
                             "'" + POLICY_NO + "'";
                        conn.ExecuteQuery();

                        // Menyimpan data ke dalam database
                        if (conn.GetFieldValue("NOTE").ToString() == "ID belum di Technical."
                            || conn.GetFieldValue("NOTE").ToString() == "ID sudah pernah di buat untuk Memo dan Memo masih On Process.")
                        {
                            // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                            uploadedDataList.Add(new UploadedDataList
                            {
                                ID = ID,
                                POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                NOTE = conn.GetFieldValue("NOTE").ToString()
                            });

                        }

                    }
                }

                LBL_PATH_FILE.Text = filePath.ToString();

                int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID belum di Technical.") || x.NOTE.Contains("ID sudah pernah di buat untuk Memo dan Memo masih On Process.")).Count();
                if (errorUpload > 0)
                {
                    btnProcess.Visible = false;
                    btnDownload.Visible = true;
                    LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Technical, <br /> Upload Settlement tidak dapat di proses sampai data Technical sudah di Journal terlebih dahulu atau Memo yang masih berjalan.";
                }
                else
                {
                    btnProcess.Visible = true;
                    btnDownload.Visible = false;
                    LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Settlement ?";
                }
                // Isi data ke GridView
                GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                return;
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type settlement sebelum download dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            string fileName = "";
            if (DDL_TYPE.SelectedValue != "")
            {
                if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                {
                    fileName = "Template Upload Financial Glife - Contribution GTLR.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                {
                    fileName = "Template Upload Financial Glife - Contribution NON GTLR.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "contribution_renewal")
                {
                    fileName = "Template Upload Financial Glife - Contribution Renewal.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "refund")
                {
                    fileName = "Template Upload Financial Glife - Refund.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "claim_glife")
                {
                    fileName = "Template Upload Financial Glife - Claim.xlsx";  // Nama file yang akan diunduh
                }
            }

            // Tentukan path folder tempat file disimpan
            string folderPath = Server.MapPath("~/Content/Template/Upload Settlement GLIFE/");
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

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
        }


        protected void btnDownloadList_Click(object sender, EventArgs e)
        {

            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "DataReasExistsMemo" + DateTime.Now.ToString("yyyyMMdd") + "_" + DDL_TYPE.SelectedValue.ToString() + ".xlsx";
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
                ws.Cells["B1"].Value = ": MEMO_" + DDL_TYPE.SelectedValue.ToString().ToUpper();
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

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            filePath = LBL_PATH_FILE.Text;

            try
            {

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Membaca data dari file Excel
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workbook = package.Workbook;
                    if (workbook == null)
                    {
                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                        LB_ERR.Text = "Upload File Tidak ditemukkan." + Environment.NewLine;
                        return;
                    }

                    var worksheet = workbook.Worksheets["UPLOAD"];
                    if (worksheet == null)
                    {
                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                        LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                        return;
                    }

                    // Contoh membaca data dari cell
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (DDL_TYPE.SelectedValue == "contribution_gtlr")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 12].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string StatusSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            string PerihalSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "" : worksheet.Cells[row, 10].Text;
                            string NoSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;

                            decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text);
                            decimal AmountTabbaru = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) ? "0" : worksheet.Cells[row, 13].Text);
                            decimal AmountUjroh = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) ? "0" : worksheet.Cells[row, 14].Text);

                            string updateDateTxt = worksheet.Cells[row, 15].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_SETTLEMENT_PRODUKSI_GTLR_GLIFE] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + StatusSettlement.Trim() + "'," +
                                 "'" + NoSuratSettlement.Trim() + "'," +
                                 "'" + PerihalSuratSettlement.Trim() + "'," +
                                 "" + AmountSettlement.ToString() + "," +
                                 "" + AmountTabbaru.ToString() + "," +
                                 "" + AmountUjroh.ToString() + "," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Settlement Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat Settlement. terdapat di memo yang aktif. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat Settlement. <BR />";
                            }
                        }

                        else if (DDL_TYPE.SelectedValue == "contribution_non_gtlr")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 12].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string StatusSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            string PerihalSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "" : worksheet.Cells[row, 10].Text;
                            string NoSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;

                            decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text);
                            decimal AmountTabbaru = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) ? "0" : worksheet.Cells[row, 13].Text);
                            decimal AmountUjroh = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) ? "0" : worksheet.Cells[row, 14].Text);

                            string updateDateTxt = worksheet.Cells[row, 15].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_SETTLEMENT_PRODUKSI_NONGTLR_GLIFE] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + StatusSettlement.Trim() + "'," +
                                 "'" + NoSuratSettlement.Trim() + "'," +
                                 "'" + PerihalSuratSettlement.Trim() + "'," +
                                 "" + AmountSettlement.ToString() + "," +
                                 "" + AmountTabbaru.ToString() + "," +
                                 "" + AmountUjroh.ToString() + "," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Settlement Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat Settlement. terdapat di memo yang aktif. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat Settlement. <BR />";
                            }
                        }

                        else if (DDL_TYPE.SelectedValue == "contribution_renewal")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 12].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string StatusSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            string PerihalSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "" : worksheet.Cells[row, 10].Text;
                            string NoSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;

                            decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text);
                            decimal AmountTabbaru = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) ? "0" : worksheet.Cells[row, 13].Text);
                            decimal AmountUjroh = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) ? "0" : worksheet.Cells[row, 14].Text);

                            string updateDateTxt = worksheet.Cells[row, 15].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_SETTLEMENT_PRODUKSI_RENEWAL_GLIFE] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + StatusSettlement.Trim() + "'," +
                                 "'" + NoSuratSettlement.Trim() + "'," +
                                 "'" + PerihalSuratSettlement.Trim() + "'," +
                                 "" + AmountSettlement.ToString() + "," +
                                 "" + AmountTabbaru.ToString() + "," +
                                 "" + AmountUjroh.ToString() + "," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Settlement Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat Settlement. terdapat di memo yang aktif. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat Settlement. <BR />";
                            }
                        }

                        else if (DDL_TYPE.SelectedValue == "refund")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 10].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string StatusSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? "" : worksheet.Cells[row, 7].Text;
                            string PerihalSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? "" : worksheet.Cells[row, 8].Text;
                            string NoSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "0" : worksheet.Cells[row, 10].Text);
                            string updateDateTxt = worksheet.Cells[row, 11].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_SETTLEMENT_PRODUKSI_REFUND_GLIFE] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + StatusSettlement.Trim() + "'," +
                                 "'" + NoSuratSettlement.Trim() + "'," +
                                 "'" + PerihalSuratSettlement.Trim() + "'," +
                                 "" + AmountSettlement.ToString() + "," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Settlement Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat Settlement. terdapat di memo yang aktif. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat Settlement. <BR />";
                            }
                        }

                        else if (DDL_TYPE.SelectedValue == "claim_glife")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 3].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Nomor Polis tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 10].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string StatusSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? "" : worksheet.Cells[row, 7].Text;
                            string PerihalSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? "" : worksheet.Cells[row, 8].Text;
                            string NoSuratSettlement = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "0" : worksheet.Cells[row, 10].Text);
                            string updateDateTxt = worksheet.Cells[row, 11].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_SETTLEMENT_CLAIM_GLIFE] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + StatusSettlement.Trim() + "'," +
                                 "'" + NoSuratSettlement.Trim() + "'," +
                                 "'" + PerihalSuratSettlement.Trim() + "'," +
                                 "" + AmountSettlement.ToString() + "," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Settlement Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat Settlement. terdapat di memo yang aktif. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat Settlement. <BR />";
                            }
                        }
                    }
                    return;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Logika untuk membatalkan proses
            // Misalnya, reset semua form atau menutup modal
            //closeModal();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalScript", "closeModal();", true);
        }
    }
}