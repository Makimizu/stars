using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using OfficeOpenXml; 
using System.Globalization;

namespace LIFE.Form_POS
{
    public partial class Upload_Email : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            string script = "";
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") ?? "SYSTEM";
            if (!FileUploadExcel.HasFile)
            {
                script = "showError('Silakan pilih file Excel terlebih dahulu.');";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                return;
            }

            string originalFileName = Path.GetFileName(FileUploadExcel.FileName);
            string filePath = Path.Combine(Server.MapPath("~/App_Data/"), Guid.NewGuid() + Path.GetExtension(originalFileName));
            FileUploadExcel.SaveAs(filePath);

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                using (SqlConnection conn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
                {
                    conn.Open();
                    ExcelWorksheet ws = package.Workbook.Worksheets[0];
                    int rowCount = ws.Dimension.Rows;

                    // Hapus semua data sebelumnya dengan filename sama
                    using (SqlCommand delCmd = new SqlCommand("DELETE FROM UPLOAD_STATUS_KIRIM WHERE FILENAME = @FILENAME", conn))
                    {
                        delCmd.Parameters.AddWithValue("@FILENAME", originalFileName);
                        delCmd.ExecuteNonQuery();
                    }

                    for (int row = 2; row <= rowCount; row++) // skip header
                    {
                        string email = ws.Cells[row, 1].Text.Trim();
                        string noPolis = ws.Cells[row, 2].Text.Trim();
                        string namaPemegang = ws.Cells[row, 3].Text.Trim();
                        string dateStr = ws.Cells[row, 4].Text.Trim();
                        string media = ws.Cells[row, 5].Text.Trim();
                        string status = ws.Cells[row, 6].Text.Trim();

                        // Skip baris kosong
                        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(noPolis) && string.IsNullOrEmpty(namaPemegang))
                            continue;

                        DateTime parsedDate;
                        DateTime.TryParse(dateStr, out parsedDate);

                        using (SqlCommand cmd = new SqlCommand("SP_UPLOAD_STATUS_KIRIM", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FILENAME", originalFileName);
                            cmd.Parameters.AddWithValue("@EMAIL", (object)email ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@NO_POLIS", (object)noPolis ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@NAMA_PEMEGANG_POLIS", (object)namaPemegang ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@DATE", parsedDate == DateTime.MinValue ? (object)DBNull.Value : parsedDate);
                            cmd.Parameters.AddWithValue("@MEDIA", (object)media ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@STATUS", (object)status ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@UPLOAD_BY", (object)user ?? DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                script = "setTimeout(function() { Swal.close(); showSuccess('"+ originalFileName +"'); }, 300);";


            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.Replace("'", "").Replace("\r", "").Replace("\n", " ");
                script = string.Format("Swal.close(); showError('Terjadi kesalahan: {0}');",errMsg);

            }
            finally
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }

            ClientScript.RegisterStartupScript(this.GetType(), "uploadResult", script, true);
        }
    }
}