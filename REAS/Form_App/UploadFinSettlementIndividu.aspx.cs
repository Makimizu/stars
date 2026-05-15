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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace REAS.Form_App
{
    public partial class UploadFinSettlementIndividu : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string uploadDate;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "UPLOAD FIN SETTLEMENT INDIVIDUAL - REAS";

            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");

            uploadDate = currentDate.ToString("yyyy-MM-dd");

        }

        string query = "";
        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type technical sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string filePath = Path.Combine(Server.MapPath("~/Upload/Settlement"), TXT_FILE_UPLOAD.FileName);
                    TXT_FILE_UPLOAD.SaveAs(filePath);

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
                            if (DDL_TYPE.SelectedValue == "contribution_individual")
                            {
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }

                                if (string.IsNullOrEmpty(worksheet.Cells[row, 18].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }

                                string ID = worksheet.Cells[row, 1].Text; // ID
                                string StatusFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) ? "" : worksheet.Cells[row, 15].Text;
                                string PerihalSuratFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? "" : worksheet.Cells[row, 16].Text;
                                string NoSuratFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 17].Text) ? "" : worksheet.Cells[row, 17].Text;
                                decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 18].Text) ? "0" : worksheet.Cells[row, 18].Text);
                                string updateDateTxt = worksheet.Cells[row, 19].Text;

                                DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                                string GenerateType = DDL_TYPE.SelectedValue;
                                string Note = TXT_DESCRIPTION.Text;

                                // Simpan ke database
                                conn.QueryString = "EXEC [AutoReportReconcile].[dbo].[SP_UploadSettlement_Individual] " +
                                     "'" + GenerateType.Trim() + "'," +
                                     "'" + ID.Trim() + "'," +
                                     "'" + StatusFinSettle.Trim() + "'," +
                                     "'" + NoSuratFinSettle.Trim() + "'," +
                                     "'" + PerihalSuratFinSettle.Trim() + "'," +
                                     "'" + AmountSettlement + "'," +
                                     "'" + StartDate + "'," +
                                     "'" + Note + "'";
                                conn.ExecuteQuery();

                                //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                                LB_ERR.ForeColor = System.Drawing.Color.Green;
                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil disimpan." + Environment.NewLine;
                                //return;

                            }
                            else if (DDL_TYPE.SelectedValue == "claim_individual")
                            {
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }

                                if (string.IsNullOrEmpty(worksheet.Cells[row, 11].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: Amount tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }

                                string ID = worksheet.Cells[row, 1].Text; // ID
                                string StatusFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) ? "" : worksheet.Cells[row, 15].Text;
                                string PerihalSuratFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? "" : worksheet.Cells[row, 16].Text;
                                string NoSuratFinSettle = string.IsNullOrEmpty(worksheet.Cells[row, 17].Text) ? "" : worksheet.Cells[row, 17].Text;
                                decimal AmountSettlement = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 18].Text) ? "0" : worksheet.Cells[row, 18].Text);
                                string updateDateTxt = worksheet.Cells[row, 19].Text;

                                DateTime startDateTxt = DateTime.ParseExact(TXT_UPDATEDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                string StartDate = startDateTxt.ToString("yyyy-MM-dd");

                                string GenerateType = DDL_TYPE.SelectedValue;
                                string Note = TXT_DESCRIPTION.Text;

                                // Simpan ke database
                                conn.QueryString = "EXEC [AutoReportReconcile].[dbo].[SP_UploadSettlement_Individual] " +
                                     "'" + GenerateType.Trim() + "'," +
                                     "'" + ID.Trim() + "'," +
                                     "'" + StatusFinSettle.Trim() + "'," +
                                     "'" + NoSuratFinSettle.Trim() + "'," +
                                     "'" + PerihalSuratFinSettle.Trim() + "'," +
                                     "'" + AmountSettlement + "'," +
                                     "'" + StartDate + "'," +
                                     "'" + Note + "'";
                                conn.ExecuteQuery();

                                //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                                LB_ERR.ForeColor = System.Drawing.Color.Green;
                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil disimpan." + Environment.NewLine;
                                //return;
                            }


                            Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                        }
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
                if (DDL_TYPE.SelectedValue == "contribution_individual")
                {
                    fileName = "Upload Settlement Individual Contribution.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "claim_individual")
                {
                    fileName = "Upload Settlement Individual Claim.xlsx";  // Nama file yang akan diunduh
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
    }
}