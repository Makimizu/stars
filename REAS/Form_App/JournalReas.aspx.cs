using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using System.Web.Services;
using OfficeOpenXml;
using System.Windows.Controls;


namespace REAS.Form_App
{
    public partial class JournalReas : System.Web.UI.Page
    {

        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string NoSurat;
        protected string JournalID;
        protected string ReasName;
        protected string query;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LBL_TITLE.Text = "JOURNAL REAS";
            BindReasDropdown();
            if (!IsPostBack)
            {
                DateTime currentDate = DateTime.Now;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                TXT_STARTDATE.Text = startDate.ToString("dd/MM/yyyy");
                TXT_ENDDATE.Text = currentDate.ToString("dd/MM/yyyy");
                StartDate = startDate.ToString("yyyy-MM-dd");
                EndDate = currentDate.ToString("yyyy-MM-dd");

                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }


                FillDGR();
            }


        }

        private void BindReasDropdown()
        {
            conn.QueryString = "SELECT COMPANY_NAME, UPPER(COMPANY_NAME) COMPANY_NAME FROM REINSURANCE.dbo.V_COMPANY";
            conn.ExecuteQuery();
            DDL_REAS.Items.Insert(0, new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REAS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_REAS_CODE_DETAIL]";
            conn.ExecuteQuery();
            DDL_REAS_SUB.Items.Insert(0, new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REAS_SUB.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 1).ToString()));
            }
        }

        private void FillDGR()
        {
            string query = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_JOURNAL] WHERE 1=1";

            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                query += " AND CONVERT(VARCHAR(10), CAST(JOURNAL_DATE AS DATETIME), 120) BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
            }

            if (!string.IsNullOrEmpty(JournalID))
            {
                query += " AND [JOURNAL_ID] LIKE '%" + JournalID.Replace("'", "''") + "%'";
            }


            query += " ORDER BY CONVERT(VARCHAR, JOURNAL_DATE, 23) DESC";

            conn.QueryString = query;
            conn.ExecuteQuery();

            // Display the result count
            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
            {
                DGR.AllowPaging = false;
            }

            // Bind the data to the grid
            DataTable dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            if (DGR.Items.Count <= 0)
            {
                //BT_JOURNAL.Enabled = false;
                BT_DOWNLOAD.Enabled = false;
            }
            else
            {
                //BT_JOURNAL.Enabled = true;
                BT_DOWNLOAD.Enabled = true;
            }
        }

        protected void BT_VIEW_Click(object sender, EventArgs e)
        {

        }

        protected void DDL_REAS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) && !string.IsNullOrEmpty(TXT_ENDDATE.Text))
            {
                DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                StartDate = startDateTxt.ToString("yyyy-MM-dd");

                DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                EndDate = endDateTxt.ToString("yyyy-MM-dd");
            }

            ReasName = DDL_REAS.SelectedValue;
            JournalID = TXT_NO_SURAT.Text;

            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "JournalReas_" + DateTime.Now.ToString("yyyyMMdd") + "_" + JournalID + ".xlsx";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            // Set the EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Retrieve data
            DataTable dt = GetDataDownload(StartDate, EndDate, ReasName, JournalID);

            // Create Excel package
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Add a new worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List Journal Reas");

                // Add headers and information
                ws.Cells["A1"].Value = "Report";
                //ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": Journal Reas";
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

        private DataTable GetDataDownload(string startDate, string endDate, string reasName, string journalID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_CONTRIBUTION_ASKES] WHERE 1=1";

            if (!string.IsNullOrEmpty(journalID))
            {
                
                if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
                {
                    query += " AND CONVERT(VARCHAR(10), CAST(AR_DATE AS DATETIME), 120) BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
                }
            }

            if (!string.IsNullOrEmpty(journalID))
            {
                query += " AND JOURNAL_ID LIKE '%" + journalID.Replace("'", "''") + "%'";
            }

            if (!string.IsNullOrEmpty(ReasName))
            {
                query += " AND REAS_NAME LIKE '%" + ReasName.Replace("'", "''") + "%'";
            }


            query += " ORDER BY ID ASC";


            conn.QueryString = query;
            conn.ExecuteQuery();

            // Bind the data to the grid
            dt = conn.GetDataTable().Copy();

            return dt;
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) || !string.IsNullOrEmpty(TXT_ENDDATE.Text))
            {
                if (string.IsNullOrEmpty(TXT_STARTDATE.Text) || string.IsNullOrEmpty(TXT_ENDDATE.Text))
                {
                    Response.Write("<script>alert('Pastikan start date dan end date terisi.');window.location.href = window.location.href;</script>");
                }
                else if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) && !string.IsNullOrEmpty(TXT_ENDDATE.Text))
                {
                    DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StartDate = startDateTxt.ToString("yyyy-MM-dd");

                    DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    EndDate = endDateTxt.ToString("yyyy-MM-dd");

                    if (startDateTxt > endDateTxt)
                    {
                        // Tampilkan pesan kesalahan
                        Response.Write("<script>alert('Pastikan start date tidak boleh melebih tanggal end date.');window.location.href = window.location.href;</script>");
                    }
                }
            }

            ReasName = DDL_REAS.SelectedValue;
            JournalID = TXT_NO_SURAT.Text;

            FillDGR();

        }

        protected void BT_JOURNAL_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);
        }

        protected void btnView_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string id = e.CommandArgument.ToString();
                // Retrieve detail data based on the ID
                DataTable detailTable = GetDetailData(id); // Replace with your method to get details
                //hiddenNoMemoView.Value = id; // Set nilai hidden field
                //txtNoMemoView.Text = id; // Set TextBox
                string gridContent = "<table class='table table-bordered'><thead><tr>" +
                    "<th>JOURNAL ID</th>" +
                    "<th>ID</th>" +
                    "<th>NO POLIS</th>" +
                    "<th>REAS TYPE</th>" +
                    "<th>REMARKS</th>" +
                    "</tr></thead><tbody>";

                if (detailTable != null && detailTable.Rows.Count > 0)
                {
                    foreach (DataRow item in detailTable.Rows)
                    {
                        gridContent += "<tr>" +
                            "<td>" + item["JOURNAL_ID"] + "</td>" +
                            "<td>" + item["ID"] + "</td>" +
                            "<td>" + item["POLICY_NO"] + "</td>" +
                            "<td>" + item["REAS_TYPE"] + "</td>" +
                            "<td>" + item["REMARK_DETIAL"] + "</td>" +
                            "</tr>";
                    }
                    gridContent += "</tbody></table>";

                    // Mengubah DataTable menjadi JSON dan kirim ke klien
                    string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(detailTable);

                    // Menjalankan script untuk menampilkan modal di sisi klien
                    string script = "openModalViewDetailMemo({jsonData});";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalViewDetail", script, true);

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalViewDetail", "openModalViewDetailMemo('" + script + "');window.location.reload(true);", true);
                }
                else
                {
                    // Jika DataTable kosong, menampilkan pesan atau tindakan lain
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalError", "alert('No data available.');", true);
                }
            }
        }

        private DataTable GetDetailData(string id)
        {
            string query = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_JOURNAL_DETAIL] WHERE JOURNAL_ID = '" + id + "'";
            conn.QueryString = query;
            conn.ExecuteQuery();

            return conn.GetDataTable().Copy();
        }

        int lastNumber;
        string filePath;
        protected void btnUploadDraft_Click(object sender, EventArgs e)
        {
            //  INSERT JOURNAL NYA DULU 
            //[dbo].[SP_UploadDraft_Journal]
            try
            {
                conn.QueryString = @"
                    IF EXISTS (SELECT 1 FROM [AutoReportReconcile].[dbo].[TampGenerateDataLog] WHERE [Type] = 'DraftJournal') 
                    BEGIN
                        SELECT MAX(LastNumber) + 1 AS LastNumber FROM [AutoReportReconcile].[dbo].[TampGenerateDataLog] WHERE [Type] = 'DraftJournal';
                    END
                    ELSE
                    BEGIN
                        SELECT 0 + 1 AS LastNumber;
                    END";

                conn.ExecuteQuery();

                // Check if the query returned any rows
                if (conn.GetRowCount() > 0)
                {
                    lastNumber = Convert.ToInt32(conn.GetFieldValue(0, "LastNumber"));
                }

                DateTime now = DateTime.Now;
                string JOURNAL_ID = "DRFT" + DDL_REAS_SUB.SelectedValue.ToString() + DDL_TYPE.SelectedValue.ToString() + string.Format("{1:yyyy}{1:MM}{0:D5}", lastNumber, now);
                string REMARKS = TXT_REMARKS.Text; // POLICY_NO

                // Simpan ke database
                conn.QueryString = "EXEC [AutoReportReconcile].[dbo].[SP_UploadDraft_Journal] " +
                     "'" + DDL_REAS_SUB.SelectedValue + "'," +
                     "'" + DDL_TYPE.SelectedValue + "'," +
                     "'" + JOURNAL_ID + "'," +
                     "'" + REMARKS + "'";
                conn.ExecuteQuery();

                if (TXT_FILE_UPLOAD.HasFile)
                {
                    try
                    {

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                        filePath = Path.Combine(Server.MapPath("~/Upload/DraftJournal"), TXT_FILE_UPLOAD.FileName);
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
                                string ID = worksheet.Cells[row, 1].Text; // ID
                                string POLICY_NO = worksheet.Cells[row, 2].Text; // POLICY_NO
                                string REMARK = worksheet.Cells[row, 3].Text; // POLICY_NO

                                // Simpan ke database
                                conn.QueryString = "EXEC [AutoReportReconcile].[dbo].[SP_UploadDraft_JournalDetail] " +
                                    "'" + JOURNAL_ID + "'," +
                                    "'" + ID.Trim() + "'," +
                                    "'" + POLICY_NO + "'," +
                                    "'" + REMARK + "'";
                                conn.ExecuteQuery();

                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Upload Technical Berhasil!');", true);

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

                conn.QueryString = @"
                IF EXISTS (SELECT 1 FROM [AutoReportReconcile].[dbo].[TampGenerateDataLog] 
                            WHERE [Type] = 'DraftJournal' AND [Year] = " + now.Year + " AND [Month] = " + now.Month + ") " +
                    "BEGIN " +
                        "UPDATE [AutoReportReconcile].[dbo].[TampGenerateDataLog] " +
                        "SET LastNumber = " + lastNumber +
                        "WHERE [Type] = 'DraftJournal' AND [Year] = " + now.Year + " AND [Month] = " + now.Month + "; " +
                    " END ELSE " +
                    "BEGIN " +
                        "INSERT INTO [AutoReportReconcile].[dbo].[TampGenerateDataLog] ([Type], [Year], [Month], LastNumber, ModifiedDate) " +
                        "VALUES ('DraftJournal', " + now.Year + ", " + now.Month + ", " + lastNumber + ", GETDATE()); " +
                    "END";

                conn.ExecuteQuery();



                //Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Pembuatan Memo Berhasil!');", true);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            string fileName = "Upload Draft Journal.xlsx";
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

        protected void btnReject_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Reject")
            {
                string id = e.CommandArgument.ToString();

                // Update MEMO
                query = "UPDATE [AutoReportReconcile].[dbo].[RptJournalReas]" +
                        " SET IS_APPROVE = 2, JOURNAL_STATUS = 'REJECTED'" +
                        " WHERE JOURNAL_ID = '" + id + "';";

                conn.QueryString = query;
                conn.ExecuteQuery();
              

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('JOURNAL : " + id + ", telah di batalkan.');window.location.reload(true);", true);
                //Response.Redirect(Request.RawUrl);
                return;
                /*Response.Write("<script>alert('Memo dengan no : " + id + ", telah di Reject.');window.location.href = window.location.href;</script>");*/
            }
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                string id = e.CommandArgument.ToString();

                // Update MEMO
                // Update MEMO
                query = "UPDATE [AutoReportReconcile].[dbo].[RptJournalReas]" +
                        " SET IS_APPROVE = 1, JOURNAL_STATUS = 'APPROVED'" +
                        " WHERE JOURNAL_ID = '" + id + "';";


                conn.QueryString = query;
                conn.ExecuteQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('JOURNAL : " + id + ", telah di setujui.');window.location.reload(true);", true);
                return;
            }
        }

        protected void btnDownloadDetail_Click(object sender, EventArgs e)
        {

        }

        protected void btnCloseView_Click(object sender, EventArgs e)
        {

        }

        protected void DDL_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_REAS_SUB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}