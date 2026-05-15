using DMS.DBConnection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace REAS.Form_App
{
    public partial class UploadLanjutanFinSettlementReas : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string uploadDate;
        #endregion

        string query = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "Upload Lanjutan Fin Settlement Health Reas";

            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");

            uploadDate = currentDate.ToString("yyyy-MM-dd");
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type settlement sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }


            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    string filePath = Path.Combine(Server.MapPath("~/Upload/Settlement Lanjutan"), TXT_FILE_UPLOAD.FileName);
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
                            if (DDL_TYPE.SelectedValue == "contribution_reas")
                            {
                                if (string.IsNullOrEmpty(worksheet.Cells[row, 1].Text))
                                {
                                    string errorMessage = "Terjadi kesalahan saat membaca file: ID tidak boleh kosong.";
                                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                                    LB_ERR.Text = errorMessage + Environment.NewLine;
                                    return;
                                }
                               
                                string ID = worksheet.Cells[row, 1].Text; // ID

                                string NoSuratSettlement1 = string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) ? "" : worksheet.Cells[row, 2].Text;
                                decimal AmountSettlement1 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 3].Text) ? "0" : worksheet.Cells[row, 3].Text.Replace(",", "."));
                                string UpdateDateSettlemt1 = string.IsNullOrEmpty(worksheet.Cells[row, 4].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 4].Text;
                                string UploadDateSettlement1 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement2 = string.IsNullOrEmpty(worksheet.Cells[row, 5].Text) ? "" : worksheet.Cells[row, 5].Text;
                                decimal AmountSettlement2 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 6].Text) ? "0" : worksheet.Cells[row, 6].Text.Replace(",", "."));
                                string UpdateDateSettlemt2 = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 7].Text;
                                string UploadDateSettlement2 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement3 = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? "" : worksheet.Cells[row, 8].Text;
                                decimal AmountSettlement3 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "0" : worksheet.Cells[row, 9].Text.Replace(",", "."));
                                string UpdateDateSettlemt3 = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 10].Text;
                                string UploadDateSettlement3 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement4 = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;
                                decimal AmountSettlement4 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text.Replace(",", "."));
                                string UpdateDateSettlemt4 = string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 13].Text;
                                string UploadDateSettlement4 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement5 = string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) ? "" : worksheet.Cells[row, 14].Text;
                                decimal AmountSettlement5 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) ? "0" : worksheet.Cells[row, 15].Text.Replace(",", "."));
                                string UpdateDateSettlemt5 = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 16].Text;
                                string UploadDateSettlement5 = TXT_UPDATEDATE.Text;


                                string GenerateType = DDL_TYPE.SelectedValue;
                                string Note = TXT_DESCRIPTION.Text;

                                // Simpan ke database

                                query = "INSERT INTO [AutoReportReconcile].[dbo].[RptUploadSettlementLanjutan](GenerateDate, GenerateType, Id, NoSuratFinSettle1, AmountFinSettlement1, UpdateDateSettlement1, UploadDateSettlement1, NoSuratFinSettle2, AmountFinSettlement2, UpdateDateSettlement2, UploadDateSettlement2,NoSuratFinSettle3, AmountFinSettlement3, UpdateDateSettlement3, UploadDateSettlement3,NoSuratFinSettle4, AmountFinSettlement4, UpdateDateSettlement4, UploadDateSettlement4,NoSuratFinSettle5, AmountFinSettlement5, UpdateDateSettlement5, UploadDateSettlement5, Note)" +
                                    " VALUES (GETDATE(), '" +
                                    GenerateType + "', '" +
                                    ID + "', '" +

                                    NoSuratSettlement1 + "', " +
                                    AmountSettlement1 + ", CONVERT(DATE, '" +
                                    UpdateDateSettlemt1 + "', 103), CONVERT(DATE,'" +
                                    UploadDateSettlement1 + "', 103), '" +

                                    NoSuratSettlement2 + "', " +
                                    AmountSettlement2 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt2 + "', 103), CONVERT(DATE, '" +
                                    UploadDateSettlement2 + "', 103), '" +

                                    NoSuratSettlement3 + "', " +
                                    AmountSettlement3 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt3 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement3 + "', 103), '" +

                                    NoSuratSettlement4 + "', " +
                                    AmountSettlement4 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt4 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement4 + "', 103), '" +

                                    NoSuratSettlement5 + "', " +
                                    AmountSettlement5 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt5 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement5 + "', 103), '" +

                                    Note + "')";

                                conn.QueryString = query;
                                conn.ExecuteQuery();

                                query = "UPDATE [AutoReportReconcile].[dbo].[RptContributionAskes] SET Status = 'Settlement'  WHERE ID = '" + ID + "'";

                                conn.QueryString = query;
                                conn.ExecuteQuery();

                                //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                                LB_ERR.ForeColor = System.Drawing.Color.Green;
                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil disimpan." + Environment.NewLine;
                                //return;

                            }
                            else if (DDL_TYPE.SelectedValue == "claim_reas")
                            {
                                string ID = worksheet.Cells[row, 1].Text; // ID

                                string NoSuratSettlement1 = string.IsNullOrEmpty(worksheet.Cells[row, 2].Text) ? "" : worksheet.Cells[row, 2].Text;
                                decimal AmountSettlement1 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 3].Text) ? "0" : worksheet.Cells[row, 3].Text.Replace(",", "."));
                                string UpdateDateSettlemt1 = string.IsNullOrEmpty(worksheet.Cells[row, 4].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 4].Text;
                                string UploadDateSettlement1 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement2 = string.IsNullOrEmpty(worksheet.Cells[row, 5].Text) ? "" : worksheet.Cells[row, 5].Text;
                                decimal AmountSettlement2 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 6].Text) ? "0" : worksheet.Cells[row, 6].Text.Replace(",", "."));
                                string UpdateDateSettlemt2 = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 7].Text;
                                string UploadDateSettlement2 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement3 = string.IsNullOrEmpty(worksheet.Cells[row, 8].Text) ? "" : worksheet.Cells[row, 8].Text;
                                decimal AmountSettlement3 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 9].Text) ? "0" : worksheet.Cells[row, 9].Text.Replace(",", "."));
                                string UpdateDateSettlemt3 = string.IsNullOrEmpty(worksheet.Cells[row, 10].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 10].Text;
                                string UploadDateSettlement3 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement4 = string.IsNullOrEmpty(worksheet.Cells[row, 11].Text) ? "" : worksheet.Cells[row, 11].Text;
                                decimal AmountSettlement4 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 12].Text) ? "0" : worksheet.Cells[row, 12].Text.Replace(",", "."));
                                string UpdateDateSettlemt4 = string.IsNullOrEmpty(worksheet.Cells[row, 13].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 13].Text;
                                string UploadDateSettlement4 = TXT_UPDATEDATE.Text;

                                string NoSuratSettlement5 = string.IsNullOrEmpty(worksheet.Cells[row, 14].Text) ? "" : worksheet.Cells[row, 14].Text;
                                decimal AmountSettlement5 = Convert.ToDecimal(string.IsNullOrEmpty(worksheet.Cells[row, 15].Text) ? "0" : worksheet.Cells[row, 15].Text.Replace(",", "."));
                                string UpdateDateSettlemt5 = string.IsNullOrEmpty(worksheet.Cells[row, 16].Text) ? TXT_UPDATEDATE.Text : worksheet.Cells[row, 16].Text;
                                string UploadDateSettlement5 = TXT_UPDATEDATE.Text;


                                string GenerateType = DDL_TYPE.SelectedValue;
                                string Note = TXT_DESCRIPTION.Text;

                                // Simpan ke database

                                query = "INSERT INTO [AutoReportReconcile].[dbo].[RptUploadSettlementLanjutan](GenerateDate, GenerateType, Id, NoSuratFinSettle1, AmountFinSettlement1, UpdateDateSettlement1, UploadDateSettlement1, NoSuratFinSettle2, AmountFinSettlement2, UpdateDateSettlement2, UploadDateSettlement2,NoSuratFinSettle3, AmountFinSettlement3, UpdateDateSettlement3, UploadDateSettlement3,NoSuratFinSettle4, AmountFinSettlement4, UpdateDateSettlement4, UploadDateSettlement4,NoSuratFinSettle5, AmountFinSettlement5, UpdateDateSettlement5, UploadDateSettlement5, Note)" +
                                    " VALUES (GETDATE(), '" +
                                    GenerateType + "', '" +
                                    ID + "', '" +

                                    NoSuratSettlement1 + "', " +
                                    AmountSettlement1 + ", CONVERT(DATE, '" +
                                    UpdateDateSettlemt1 + "', 103), CONVERT(DATE,'" +
                                    UploadDateSettlement1 + "', 103), '" +

                                    NoSuratSettlement2 + "', " +
                                    AmountSettlement2 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt2 + "', 103), CONVERT(DATE, '" +
                                    UploadDateSettlement2 + "', 103), '" +

                                    NoSuratSettlement3 + "', " +
                                    AmountSettlement3 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt3 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement3 + "', 103), '" +

                                    NoSuratSettlement4 + "', " +
                                    AmountSettlement4 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt4 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement4 + "', 103), '" +

                                    NoSuratSettlement5 + "', " +
                                    AmountSettlement5 + ",CONVERT(DATE, '" +
                                    UpdateDateSettlemt5 + "', 103),CONVERT(DATE, '" +
                                    UploadDateSettlement5 + "', 103), '" +

                                    Note + "')";


                                LB_ERR.Text = query;

                                query = "UPDATE [AutoReportReconcile].[dbo].[RptClaimAskes] SET Status = 'Payment'  WHERE ID = '" + ID + "'";

                                conn.QueryString = query;
                                conn.ExecuteQuery();

                                //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                                LB_ERR.ForeColor = System.Drawing.Color.Green;
                                LB_ERR.Text += "Data dengan ID : " + ID + " berhasil disimpan." + Environment.NewLine;
                                //return;
                            }


                        }
                        Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
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
                //Response.Write("<script>alert('Silakan pilih file untuk diunggah.');window.location.href = window.location.href;</script>");

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
                    fileName = "Upload Lanjutan Settlement Reas Contribution.xlsx";  // Nama file yang akan diunduh
                }
                else if (DDL_TYPE.SelectedValue == "claim_reas")
                {
                    fileName = "Upload Lanjutan Settlement Reas Claim.xlsx";  // Nama file yang akan diunduh
                }
            }

            // Tentukan path folder tempat file disimpan
            string folderPath = Server.MapPath("~/Content/Template/");
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

        }
    }
}