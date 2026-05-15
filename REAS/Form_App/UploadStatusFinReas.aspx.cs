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
    public partial class UploadStatusFinReas : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string uploadDate;
        List<UploadedDataList> uploadedDataList = new List<UploadedDataList>();
        string filePath;
        #endregion

        public class UploadedDataList
        {
            public string MEMOID { get; set; }
            public string ID { get; set; }
            public string POLICY_NO { get; set; }
            public string NOTE { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "UPDATE STATUS FIN REAS";
            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");

            uploadDate = currentDate.ToString("yyyy-MM-dd");
            LBL_AMOUNT_REAS.Visible = false;
            LBL_ACTUAL_AMOUNT_REAS.Visible = false;
            TXT_AMOUNT_REAS.Visible = false;
            TXT_ACTUAL_AMOUNT_REAS.Visible = false;

            //TXT_AMOUNT_REAS.Visible = false;
            //TXT_ACTUAL_AMOUNT_REAS.Visible = false;

        }

        private string _path, _fullpath;

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (DDL_TYPE_MEMO.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type sebelum upload dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            LB_ERR.Text = "";
            if (TXT_FILE_UPLOAD.HasFile)
            {
                try
                {

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    filePath = Path.Combine(Server.MapPath("~/Upload/Treasury/Status Reas"), TXT_FILE_UPLOAD.FileName);
                    TXT_FILE_UPLOAD.SaveAs(filePath);

                    Console.WriteLine(new FileInfo(filePath));

                    // Simpan ke database
                    conn.QueryString = "SELECT COUNT(1) AS TOTAL FROM [AutoReportReconcile].[dbo].[RptMemo] WHERE MEMOID = " +
                         "'" + TXT_DESCRIPTION.Text.ToString().Trim() + "' AND MEMO_TYPE = '" + DDL_TYPE_MEMO.SelectedValue.ToString().Trim() + "' AND ISAPPROVE = 1";
                    conn.ExecuteQuery();

                    if (Convert.ToInt32(conn.GetFieldValue("TOTAL")) == 0)
                    {
                        btnDownload.Visible = false;
                        btnProcess.Visible = false;
                        LBL_PROCESS.Text = "Memo dengan no tersebut tidak di temukan, mohon di cek kembali Nomor Memo nya!";
                    }
                    else
                    {
                        LBL_PROCESS.Text = "Memo dengan nomor tersebut telah di temukan, apakah ingin melanjutakan proses Update Status Reas tersebut?";
                        btnProcess.Visible = true;
                        btnDownload.Visible = false;
                    }

                    // Tampilkan modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);

                    return;
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
            if (DDL_TYPE_MEMO.SelectedValue == "")
            {
                Response.Write("<script>alert('Silakan pilih terlebih dahulu type technical sebelum download dokumen.');window.location.href = window.location.href;</script>");
                return;
            }

            string fileName = "";
            //if (DDL_TYPE.SelectedValue != "")
            //{
            //    if (DDL_TYPE.SelectedValue == "contribution_reas")
            //    {
            //        fileName = "Upload Technical Reas Contribution.xlsx";  // Nama file yang akan diunduh
            //    }
            //    else if (DDL_TYPE.SelectedValue == "claim_reas")
            //    {
            //        fileName = "Upload Technical Reas Claim.xlsx";  // Nama file yang akan diunduh
            //    }
            //}

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
            string fileName = "DataReasExistsMemo" + DateTime.Now.ToString("yyyyMMdd") + "_" + DDL_TYPE_MEMO.SelectedValue.ToString() + ".xlsx";
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
                ws.Cells["B1"].Value = ": MEMO_" + DDL_TYPE_MEMO.SelectedValue.ToString().ToUpper();
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
                    // Simpan ke database
                    conn.QueryString = "UPDATE [AutoReportReconcile].[dbo].[RptMemo] SET ISTREASURYUPDATE = 1  WHERE MEMOID = " +
                         "'" + TXT_DESCRIPTION.Text.ToString().Trim() + "' AND MEMO_TYPE = '" + DDL_TYPE_MEMO.SelectedValue.ToString().Trim() + "' AND ISAPPROVE = 1";
                    conn.ExecuteQuery();

                    string statusDB = conn.GetFieldValue("STATUS").ToString();
                    // Jika proses upload dan simpan data berhasil
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Update Status Reas dengan Memo tersebut Berhasil!');", true);

                    return;
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

        protected void DDL_TYPE_MEMO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_TYPE_MEMO.SelectedValue.Contains("EM"))
            {
                LBL_AMOUNT_REAS.Visible = true;
                LBL_ACTUAL_AMOUNT_REAS.Visible = true;
                TXT_AMOUNT_REAS.Visible = true;
                TXT_ACTUAL_AMOUNT_REAS.Visible = true;
            }
            else
            {
                LBL_AMOUNT_REAS.Visible = false;
                LBL_ACTUAL_AMOUNT_REAS.Visible = false;
                TXT_AMOUNT_REAS.Visible = false;
                TXT_ACTUAL_AMOUNT_REAS.Visible = false;
            }
        }

        protected void TXT_DESCRIPTION_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_TYPE_MEMO_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void BTN_CHECKMEMO_Click(object sender, EventArgs e)
        {
            string MEMO_NO = TXT_DESCRIPTION.Text;

            conn.QueryString = "EXEC [AutoReportReconcile].[dbo].[SP_CheckMemoIsActive] " +
                         "'" + MEMO_NO.Trim() + "'";
            conn.ExecuteQuery();

            string Note = conn.GetFieldValue("REMARKS").ToString();
            if (Note.Contains("Memo ditemukan."))
            {
                TXT_AMOUNT_REAS.Text = conn.GetFieldValue("AMOUNT_REAS").ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Memo ditemukan.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Memo tidak di temukan.');", true);

            }
            // Jika proses upload dan simpan data berhasil


            return;
        }

        private void SaveDataToDatabase()
        {
            // Logika untuk menyimpan data dari GridView ke database
            // Ini bisa dilakukan dengan melakukan iterasi data di GridView dan menyimpannya ke database
        }
    }
}