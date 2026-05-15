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
using System.Windows.Controls;


namespace REAS.Form_App
{
    public partial class UploadTechnicalReas : System.Web.UI.Page
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
            public string NOTE { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "UPLOAD TECHNICAL HEALTH - REAS";
            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");

            uploadDate = currentDate.ToString("yyyy-MM-dd");
        }

        private string _path, _fullpath;

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type technical sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            LB_ERR.Text = "";
            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    filePath = Path.Combine(Server.MapPath("~/Upload/Technical"), TXT_FILE_UPLOAD.FileName);
                    TXT_FILE_UPLOAD.SaveAs(filePath);

                    Console.WriteLine(new FileInfo(filePath));

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

                        var worksheet = workbook.Worksheets["Sheet1"];
                        if (worksheet == null)
                        {
                            LB_ERR.ForeColor = System.Drawing.Color.Red;
                            LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                            return;
                        }

                        // Contoh membaca data dari cell
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                        {
                            if (DDL_TYPE.SelectedValue == "contribution_reas")
                            {
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }

                                if (string.IsNullOrEmpty(worksheet.Cells[row, 19].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }


                                string ID = worksheet.Cells[row, 1].Text; // ID
                                string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                                // Simpan ke database
                                conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_HEALTH_CONTRIBUTION_JOURNAL] " +
                                     "'" + ID.Trim() + "'," +
                                     "'" + POLICY_NO + "'";
                                conn.ExecuteQuery();

                                // Menyimpan data ke dalam database
                                if (conn.GetFieldValue("NOTE").ToString() == "ID tersebut belum melakukan Journal.")
                                {
                                    // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                    uploadedDataList.Add(new UploadedDataList
                                    {
                                        JOURNAL_NO = conn.GetFieldValue("JOURNAL_NO").ToString(),
                                        ID = ID,
                                        POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                        NOTE = conn.GetFieldValue("NOTE").ToString()
                                    });

                                }

                                //return;
                            }
                            else if (DDL_TYPE.SelectedValue == "claim_reas")
                            {
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
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
                                string POLICY_NO = worksheet.Cells[row, 3].Text; // POLICY_NO

                                // Simpan ke database
                                conn.QueryString = "EXEC [dbo].[SP_RTF_CHECKDATA_HEALTH_CLAIM_JOURNAL] " +
                                     "'" + ID.Trim() + "'," +
                                     "'" + POLICY_NO + "'";
                                conn.ExecuteQuery();

                                // Menyimpan data ke dalam database
                                if (conn.GetFieldValue("NOTE").ToString() == "ID tersebut belum melakukan Journal.")
                                {
                                    // Simpan ke database atau buat data baru untuk ditampilkan di GridView
                                    uploadedDataList.Add(new UploadedDataList
                                    {
                                        JOURNAL_NO = conn.GetFieldValue("JOURNAL_NO").ToString(),
                                        ID = ID,
                                        POLICY_NO = conn.GetFieldValue("POLICY_NO").ToString(),
                                        NOTE = conn.GetFieldValue("NOTE").ToString()
                                    });

                                }
                            }
                        }
                        LBL_PATH_FILE.Text = filePath.ToString();

                        int errorUpload = uploadedDataList.Where(x => x.NOTE.Contains("ID tersebut belum melakukan Journal.")).Count();
                        if (errorUpload > 0)
                        {
                            btnProcess.Visible = false;
                            LBL_PROCESS.Text = "Terdapat <b>beberapa ID</b. yang belum di lakukan Journal, <br /> Upload Technical tidak dapat di proses sampai data sudah di Journal terlebih dahulu.";
                        }
                        else
                        {
                            btnDownload.Visible = false;
                            LBL_PROCESS.Text = "Apakah ingin melanjutkan process Upload Technical ?";
                        }
                        // Isi data ke GridView
                        GV_UploadedData.DataSource = uploadedDataList;
                        GV_UploadedData.DataBind();

                        // Tampilkan modal
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                        return;
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

        
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type technical sebelum download dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            string fileName = "";
            if (DDL_TYPE.SelectedValue != "")
            {
                if (DDL_TYPE.SelectedValue == "contribution_reas")
                {
                    fileName = "Upload Technical Health Contribution.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "claim_reas")
                {
                    fileName = "Upload Technical Reas Claim.xlsx";  // Nama file yang akan diunduh
                }
            }
            
            // Tentukan path folder tempat file disimpan
            string folderPath = Server.MapPath("~/Content/Template/Upload Technical - Settlement/");
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

                byte[] fileBytes = File.ReadAllBytes(filePath);
                Response.BinaryWrite(fileBytes);
                Response.Flush(); // Pastikan semua data dikirim
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                // Pesan jika file tidak ditemukan
                Response.Write("<script>alert('File tidak ditemukan.'); window.location.href = window.location.href;</script>");
            }
        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDownloadList_Click(object sender, EventArgs e)
        {
            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "JournalDataReas_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DDL_TYPE.SelectedValue.ToString() + ".xlsx";
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
                ws.Cells["B1"].Value = ": Data Belum di Journal_" + DDL_TYPE.SelectedValue.ToString().ToUpper();
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

                    var worksheet = workbook.Worksheets["Sheet1"];
                    if (worksheet == null)
                    {
                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                        LB_ERR.Text = "Data tidak dapat di proses." + Environment.NewLine;
                        return;
                    }

                    // Contoh membaca data dari cell
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        if (DDL_TYPE.SelectedValue == "contribution_reas")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }

                            if (string.IsNullOrEmpty(worksheet.Cells[row, 19].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                LB_ERR.ForeColor = System.Drawing.Color.Red;
                                LB_ERR.Text = errorMessage + Environment.NewLine;
                                return;
                            }


                            string ID = worksheet.Cells[row, 1].Text; // ID
                            string SuratPengajuan = string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) ? "" : worksheet.Cells[row, 15].Text;
                            string StatusTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? "" : worksheet.Cells[row, 16].Text;
                            string NoSuratTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 17].Text) ? "" : worksheet.Cells[row, 17].Text;
                            string PerihalSuratTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 18].Text) ? "" : worksheet.Cells[row, 18].Text;
                            
                            decimal AmountTabbaru = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 19].Text) ? "0" : worksheet.Cells[row, 19].Text);
                            decimal AmountUjroh = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 20].Text) ? "0" : worksheet.Cells[row, 20].Text);
                            decimal AmountContribution = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 21].Text) ? "0" : worksheet.Cells[row, 21].Text);
                            
                            string updateDateTxt = worksheet.Cells[row, 22].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;


                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_TECHNICAL_CONTRIBUTIONHEALTH] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + SuratPengajuan.Trim() + "'," +
                                 "'" + StatusTechnical.Trim() + "'," +
                                 "'" + NoSuratTechnical.Trim() + "'," +
                                 "'" + PerihalSuratTechnical.Trim() + "'," +
                                 "'" + AmountTabbaru.ToString() + "'," +
                                 "'" + AmountUjroh.ToString() + "'," +
                                 "'" + AmountContribution.ToString() + "'," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();

                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Technical Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat technical. Mohon di coba kembali.<BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat technical. <BR />";
                            }
                            //return;
                        }
                        else if (DDL_TYPE.SelectedValue == "claim_reas")
                        {
                            if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                            {
                                string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
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
                            string SuratPengajuan = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? "" : worksheet.Cells[row, 8].Text;
                            string StatusTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "" : worksheet.Cells[row, 9].Text;
                            string NoSuratTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? "" : worksheet.Cells[row, 10].Text;
                            string PerihalSuratTechnical = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;
                            decimal AmountTechnical = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text);
                            string updateDateTxt = worksheet.Cells[row, 13].Text;

                            DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                            string GenerateType = DDL_TYPE.SelectedValue;
                            string Note = TXT_DESCRIPTION.Text;

                            // Simpan ke database
                            conn.QueryString = "EXEC [dbo].[SP_RTF_UPLD_TECHNICAL_CLAIMHEALTH] " +
                                 "'" + GenerateType.Trim() + "'," +
                                 "'" + ID.Trim() + "'," +
                                 "'" + SuratPengajuan.Trim() + "'," +
                                 "'" + StatusTechnical.Trim() + "'," +
                                 "'" + NoSuratTechnical.Trim() + "'," +
                                 "'" + PerihalSuratTechnical.Trim() + "'," +
                                 "'" + AmountTechnical + "'," +
                                 "'" + StartDate + "'," +
                                 "'" + Note + "'";
                            conn.ExecuteQuery();


                            string statusDB = conn.GetFieldValue("STATUS").ToString();
                            // Jika proses upload dan simpan data berhasil
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Technical Berhasil!');", true);

                            if (statusDB == "FAILED")
                            {
                                LB_ERR.Text += "Data dengan ID : " + ID + ", gagal di proses membuat technical. Mohon di coba kembali. <BR />";
                            }
                            else
                            {

                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil membuat technical. <BR />";
                            }
                            //return;
                        }



                    }
                    //Response.Write("<script>alert('Data berhasil di simpan.');</script>");
                    //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                    return;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Logika untuk membatalkan proses
            // Misalnya, reset semua form atau menutup modal
            //closeModal();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseModalScript", "closeModal();", true);
        }

        private void ExportToExcel()
        {
            // Contoh: Ekspor data GridView ke file Excel
            // Anda bisa menggunakan berbagai library untuk ekspor ke Excel atau CSV
        }

        private void SaveDataToDatabase()
        {
            // Logika untuk menyimpan data dari GridView ke database
            // Ini bisa dilakukan dengan melakukan iterasi data di GridView dan menyimpannya ke database
        }
    }
}