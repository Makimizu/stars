using AjaxControlToolkit;
using DMS.DBConnection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BodyWord = DocumentFormat.OpenXml.Wordprocessing.Body;
using ControlUI = System.Web.UI.Control;
using PageSizePdfDocument = iTextSharp.text.PageSize;
using PdfDocument = iTextSharp.text.Document;
using W = DocumentFormat.OpenXml.Wordprocessing;
using WordDocument = DocumentFormat.OpenXml.Wordprocessing.Document;

namespace LIFE.Form_POS
{
    public partial class Maker_Zakat : System.Web.UI.Page
    {
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePeriodeDropdown();
                BindHeaderGrid();
                LoadPeriode();
                LoadSummaryData();
            }

            if (Request["__EVENTTARGET"] == "RefreshDetail")
            {
                string noIom = Request["__EVENTARGUMENT"];

                ViewState["ExpandAfterRefresh"] = noIom;

                BindHeaderGrid();

                ViewState["ExpandAfterRefresh"] = null;
            }
        }
        private void LoadPeriode()
        {
            ddlPeriode.Items.Clear();

            int currentYear = DateTime.Now.Year;

            ddlPeriode.Items.Add(currentYear.ToString());
            ddlPeriode.Items.Add((currentYear - 1).ToString());
            ddlPeriode.Items.Add((currentYear - 2).ToString());

            ddlPeriode.SelectedValue = currentYear.ToString();
        }
        
        private void LoadSummaryData()
        {
            int tahun = Convert.ToInt32(ddlPeriode.SelectedValue);

            DataRow dr = GetSummaryTotalZakat(tahun);

            if (dr != null)
            {
                txtTanggalNAV.Text = Convert.ToDateTime(dr["LastNAV"]).ToString("dd/MM/yyyy");
                txtJumlahPeserta.Text = dr["Jumlah_Peserta"].ToString();
                txtTotalBiaya.Text = Convert.ToDecimal(dr["Total_Biaya"]).ToString("N2", new CultureInfo("id-ID"));
                txtTotalZakat.Text = Convert.ToDecimal(dr["Total_Zakat"]).ToString("N2", new CultureInfo("id-ID"));
                txtTotalBayar.Text = Convert.ToDecimal(dr["Total_Dibayarkan"]).ToString("N2", new CultureInfo("id-ID"));
            }
            else
            {
                txtTanggalNAV.Text = "";
                txtJumlahPeserta.Text = "0";
                txtTotalBiaya.Text = "0";
                txtTotalZakat.Text = "0";
                txtTotalBayar.Text = "0";
            }
        }

        protected void ddlPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSummaryData();   // <-- refresh data sesuai tahun yang dipilih
        }
        
        private void LoadBanks(DropDownList ddl)
        {
            ddl.Items.Clear();
            //conn.QueryString = "SELECT BANK_CODE, BANK_NAME FROM REF_BANK ORDER BY BANK_NAME";
            conn.QueryString = "select KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by KODE";
            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable();
            ddl.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih Bank --", ""));

            //foreach (DataRow r in dt.Rows)
            //{
            //    ddl.Items.Add(new System.Web.UI.WebControls.ListItem(r["KODE"].ToString(), r["DESCR"].ToString()));
            //}
            for (int i = 0; i < conn.GetRowCount(); i++)
                ddl.Items.Add(new System.Web.UI.WebControls.ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }
        
        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlNewBank = (DropDownList)e.Row.FindControl("ddlNewBank");
                if (ddlNewBank != null)
                {
                    LoadBanks(ddlNewBank);
                }
            }
        }
        
        private void BindHeaderGrid()
        {
            conn.QueryString = 
                            "SELECT * FROM MAKER_ZAKAT_HDR " +
                            "WHERE ISNULL(STATUS, 'OPEN') = 'OPEN' " +
                            "ORDER BY CREATED_AT DESC";

            conn.ExecuteQuery();
            gvHeader.DataSource = conn.GetDataTable();
            gvHeader.DataBind();
        }
        
        protected void gvHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string noIOM = DataBinder.Eval(e.Row.DataItem, "NO_IOM").ToString();

                e.Row.Attributes["data-iom"] = noIOM;

                GridView gvDetail = (GridView)e.Row.FindControl("gvDetail");
                Panel detailPanel = (Panel)e.Row.FindControl("pnlDetail");
                Button btnExpand = (Button)e.Row.FindControl("btnExpand");


                conn.QueryString ="SELECT * FROM MAKER_ZAKAT_DTL WHERE NO_IOM = '" + noIOM.Replace("'", "''") + "' ORDER BY ID_DETAIL";

                conn.ExecuteQuery();

                gvDetail.DataSource = conn.GetDataTable().Copy();
                gvDetail.DataBind();

                string expandIom = ViewState["ExpandAfterRefresh"]?.ToString();
                if (!string.IsNullOrEmpty(expandIom) && noIOM == expandIom)
                {
                    if (detailPanel != null && gvDetail != null)
                    {
                        BindDetailGrid(noIOM, gvDetail);
                        detailPanel.Visible = true;

                        if (btnExpand != null)
                            btnExpand.Text = "-";
                    }
                }

            }
        }
        
        protected void gvHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string noIom = e.CommandArgument.ToString();
            if (e.CommandName == "Expand")
            {
                GridViewRow selectedRow = ((ControlUI)e.CommandSource).NamingContainer as GridViewRow;
                Panel detailPanel = (Panel)selectedRow.FindControl("pnlDetail");
                GridView gvDetail = (GridView)selectedRow.FindControl("gvDetail");
                Button btnExpand = (Button)selectedRow.FindControl("btnExpand");

                if (detailPanel != null && gvDetail != null)
                {
                    if (detailPanel.Visible) // collapse
                    {
                        detailPanel.Visible = false;
                        btnExpand.Text = "+";
                    }
                    else // expand
                    {
                        //tutup semua panel lain dulu
                        foreach (GridViewRow row in gvHeader.Rows)
                        {
                            Panel pnl = (Panel)row.FindControl("pnlDetail");
                            Button btn = (Button)row.FindControl("btnExpand");
                            if (pnl != null) pnl.Visible = false;
                            if (btn != null) btn.Text = "+";
                        }

                        // buka panel ini
                        BindDetailGrid(noIom, gvDetail);
                        detailPanel.Visible = true;
                        btnExpand.Text = "-";
                        ViewState["CurrentIOM"] = noIom;
                    }
                }
            }
            if (e.CommandName == "ExportPdf")
            {
                ExportPdfByNoIom(noIom, "MAKER");
            }
            else if (e.CommandName == "ExportExcel")
            {
                ExportExcelByNoIom(noIom);
            }
            else if (e.CommandName == "ExportWord")
            {
                ExportWordByNoIom(noIom);
            }
            else if (e.CommandName == "DeleteHeader")
            {
                conn.QueryString ="EXEC SP_DELETE_MAKER_ZAKAT_HDR '" + noIom.Replace("'", "''") + "'";

                conn.ExecuteNonQuery();

                BindHeaderGrid();
                ShowMessage("Data header berhasil dihapus.", false);
            }
            else if (e.CommandName == "Verification")
            {
                if (!HasDetail(noIom))
                {
                    ShowMessage("Detail belum ditambahkan. Tidak bisa melakukan verification.", true);
                    return;
                }

                UpdateZakatStatus(noIom, "ON VERIFICATION", "Data dikirim ke checker untuk verifikasi.");
            }
        }
        
        private string FormatLine(string label, string value)
        {
            return label.PadRight(35, ' ') + ": " + value;
        }

        private void ExportPdfByNoIom(string noIom, string rolePage)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(
                    GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_MAKER_ZAKAT", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NO_IOM", noIom);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count < 2)
                        {
                            ShowMessage("Tidak ada data untuk diexport.", true);
                            return;
                        }

                        DataTable hdr = ds.Tables[0];
                        DataTable dt = ds.Tables[1];

                        if (hdr.Rows.Count == 0)
                        {
                            ShowMessage("Header tidak ditemukan.", true);
                            return;
                        }

                        var r = hdr.Rows[0];

                        // =========================
                        // USER LOGIN
                        // =========================
                        DataRow user = GetUserLogin();
                        string fullName = user?["FULLNAME"]?.ToString() ?? "";

                        bool isMaker = rolePage?.ToUpper() == "MAKER";
                        bool isChecker = rolePage?.ToUpper() == "CHECKER";

                        string preparedByName = isMaker ? fullName : "";
                        string approvedByName = isChecker ? fullName : "";
                        string approvedByDisplay = string.IsNullOrWhiteSpace(approvedByName)
                                                    ? "________________"
                                                    : $"({approvedByName})";

                        // =========================
                        // LOAD HTML TEMPLATE
                        // =========================
                        string html = LoadHtmlTemplate("~/Templates/iom_zakat.html");

                        // =========================
                        // HEADER
                        // =========================
                        html = html.Replace("{{NO_IOM}}", r["NO_IOM"].ToString());
                        html = html.Replace("{{JUMLAH_PESERTA}}", r["JUMLAH_PESERTA"].ToString());
                        html = html.Replace("{{JENIS_TRANSAKSI}}", r["JENIS_TRANSAKSI"].ToString());
                        html = html.Replace("{{TANGGAL_NAV}}",
                            Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy"));
                        html = html.Replace("{{PERIODE_ZAKAT}}", r["PERIODE_ZAKAT"].ToString());

                        int rangeDari = 0;
                        int rangeSampai = 0;

                        if (dt.Rows.Count > 0)
                        {
                            rangeDari = dt.AsEnumerable().Min(x =>
                                x["RANGE_DARI"] != DBNull.Value ? Convert.ToInt32(x["RANGE_DARI"]) : 0);

                            rangeSampai = dt.AsEnumerable().Max(x =>
                                x["RANGE_SAMPAI"] != DBNull.Value ? Convert.ToInt32(x["RANGE_SAMPAI"]) : 0);
                        }

                        var summary = GetJumlahZakatByRange(noIom, rangeDari, rangeSampai);

                        html = html.Replace("{{TOTAL_ZAKAT}}", summary.totalZakat.ToString("N2", new CultureInfo("id-ID")));
                        html = html.Replace("{{TOTAL_BIAYA}}", summary.totalBiaya.ToString("N2", new CultureInfo("id-ID")));
                        html = html.Replace("{{TOTAL_BAYAR}}", summary.totalBayar.ToString("N2", new CultureInfo("id-ID")));

                        // =========================
                        // DATE
                        // =========================
                        string createDate = "";
                        if (r["CREATED_AT"] != DBNull.Value)
                        {
                            DateTime dtCreate;
                            if (DateTime.TryParse(r["CREATED_AT"].ToString(), out dtCreate))
                                createDate = dtCreate.ToString("dd/MM/yyyy");
                        }

                        string updateDate = "";
                        if (r["UPDATEDATE"] != DBNull.Value)
                        {
                            DateTime dtUpdate;
                            if (DateTime.TryParse(r["UPDATEDATE"].ToString(), out dtUpdate))
                                updateDate = dtUpdate.ToString("dd/MM/yyyy");
                        }

                        html = html.Replace("{{CREATED_AT}}", createDate);
                        html = html.Replace("{{UPDATEDATE}}", updateDate);
                        html = html.Replace("{{PREPARED_BY_NAME}}", preparedByName);
                        html = html.Replace("{{APPROVED_BY_NAME}}", approvedByDisplay);

                        // =========================
                        // DETAIL TABLE
                        // =========================
                        StringBuilder detailRows = new StringBuilder();

                        foreach (DataRow d in dt.Rows)
                        {
                            detailRows.Append($@"
                                                <tr>
                                                    <td>
                                                        Nomor Rekening: {d["NO_REKENING"]}<br/>
                                                        Nama Rekening: {d["NAMA_REKENING"]}<br/>
                                                        Bank: {d["BANK_NAME"]}
                                                    </td>
                                                    <td style='text-align:center;'>{d["RANGE_DARI"]} s/d {d["RANGE_SAMPAI"]}</td>
                                                    <td class='right'>{Convert.ToDecimal(d["JUMLAH"]).ToString("N2", new CultureInfo("id-ID"))}</td>
                                                </tr>");
                        }

                        html = html.Replace("{{DETAIL_ROWS}}", detailRows.ToString());

                        // =========================
                        // SIGNATURE (FIX UTAMA)
                        // =========================
                        string signPreparedHtml = "";
                        string signApprovedHtml = "";

                        if (user != null && user["SIGNATURE"] != DBNull.Value)
                        {
                            byte[] signBytes = (byte[])user["SIGNATURE"];

                            string tempFile = Path.Combine(Path.GetTempPath(),
                                Guid.NewGuid().ToString() + ".png");

                            File.WriteAllBytes(tempFile, signBytes);

                            string imgTag = $"<img src='file:///{tempFile.Replace("\\", "/")}' style='height:60px;' />";

                            if (isMaker)
                                signPreparedHtml = imgTag;

                            if (isChecker)
                                signApprovedHtml = imgTag;
                        }

                        html = html.Replace("{{SIGN_PREPARED}}", signPreparedHtml);
                        html = html.Replace("{{SIGN_APPROVED}}", signApprovedHtml);

                        // =========================
                        // HTML → PDF
                        // =========================
                        using (MemoryStream ms = new MemoryStream())
                        {
                            PdfDocument document = new PdfDocument(PageSizePdfDocument.A4, 36, 36, 36, 36);
                            PdfWriter writer = PdfWriter.GetInstance(document, ms);

                            document.Open();

                            using (StringReader sr = new StringReader(html))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                            }

                            document.Close();

                            byte[] finalBytes = ms.ToArray();

                            // =========================
                            // RESPONSE
                            // =========================
                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-disposition",
                                "attachment;filename=IOM_" + noIom.Replace("/", "_") + ".pdf");

                            Response.BinaryWrite(finalBytes);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error export PDF: " + ex.Message, true);
            }
        }

        private void ExportExcelByNoIom(string noIom)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
                using (SqlCommand cmd = new SqlCommand("SP_GET_MAKER_ZAKAT", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NO_IOM", noIom);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count < 2)
                    {
                        ShowMessage("Tidak ada data.", true);
                        return;
                    }

                    DataTable hdr = ds.Tables[0];
                    DataTable dt = ds.Tables[1];

                    var r = hdr.Rows[0];

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage package = new ExcelPackage())
                    {
                        var ws = package.Workbook.Worksheets.Add("IOM Zakat");

                        int row = 1;

                        // ================= TITLE =================
                        ws.Cells[row, 1].Value = "IOM PERSETUJUAN TRANSAKSI ZAKAT";
                        ws.Cells[row, 1, row, 4].Merge = true;
                        ws.Cells[row, 1].Style.Font.Bold = true;
                        ws.Cells[row, 1].Style.Font.Size = 14;
                        ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        row += 2;

                        // ================= DATA TRANSAKSI =================
                        AddSection(ws, ref row, "DATA TRANSAKSI");

                        AddKeyValue(ws, ref row, "No IOM", r["NO_IOM"].ToString());
                        AddKeyValue(ws, ref row, "Jumlah Peserta", r["JUMLAH_PESERTA"].ToString());
                        AddKeyValue(ws, ref row, "Jenis Transaksi", r["JENIS_TRANSAKSI"].ToString());
                        AddKeyValue(ws, ref row, "Tanggal NAV",
                            Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy"));
                        AddKeyValue(ws, ref row, "Periode Zakat", r["PERIODE_ZAKAT"].ToString());

                        row++;

                        // ================= PERHITUNGAN =================
                        AddSection(ws, ref row, "PERHITUNGAN DANA");

                        AddAmount(ws, ref row, "Jumlah Zakat", r["TOTAL_ZAKAT"]);
                        AddAmount(ws, ref row, "Jumlah Biaya", r["TOTAL_BIAYA"]);
                        AddAmount(ws, ref row, "Total", r["TOTAL_BAYAR"], true);

                        row++;

                        // ================= INSTRUKSI TRANSFER =================
                        AddSection(ws, ref row, "INSTRUKSI TRANSFER");

                        // Header Table
                        ws.Cells[row, 1].Value = "DETAIL REKENING";
                        ws.Cells[row, 2].Value = "RANGE";
                        ws.Cells[row, 3].Value = "JUMLAH";

                        using (var range = ws.Cells[row, 1, row, 3])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }

                        row++;

                        foreach (DataRow d in dt.Rows)
                        {
                            ws.Cells[row, 1].Value =
                                $"No Rek: {d["NO_REKENING"]}\nNama: {d["NAMA_REKENING"]}\nBank: {d["BANK_NAME"]}";

                            ws.Cells[row, 2].Value =
                                $"{d["RANGE_DARI"]} s/d {d["RANGE_SAMPAI"]}";

                            ws.Cells[row, 3].Value =
                                Convert.ToDecimal(d["JUMLAH"]);

                            ws.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                            ws.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                            using (var range = ws.Cells[row, 1, row, 3])
                            {
                                range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }

                            row++;
                        }

                        row += 2;

                        // ================= TANDA TANGAN =================
                        ws.Cells[row, 1].Value = "Dipersiapkan Oleh";
                        ws.Cells[row, 3].Value = "Disetujui Oleh";

                        row += 3;

                        ws.Cells[row, 1].Value = "(____________________)";
                        ws.Cells[row, 3].Value = "(____________________)";

                        row++;

                        ws.Cells[row, 1].Value = "Tanggal";
                        ws.Cells[row, 3].Value = "Tanggal";

                        // Auto width
                        ws.Cells.AutoFitColumns();

                        // Export
                        Response.Clear();
                        Response.ContentType =
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition",
                            "attachment;filename=IOM_" + noIom.Replace("/", "_") + ".xlsx");

                        Response.BinaryWrite(package.GetAsByteArray());
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error Excel: " + ex.Message, true);
            }
        }

        private void ExportWordByNoIom(string noIom)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
                using (SqlCommand cmd = new SqlCommand("SP_GET_MAKER_ZAKAT", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NO_IOM", noIom);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables.Count < 2)
                    {
                        ShowMessage("Tidak ada data.", true);
                        return;
                    }

                    DataTable hdr = ds.Tables[0];
                    DataTable dt = ds.Tables[1];
                    var r = hdr.Rows[0];

                    // 1. Load HTML Template
                    string html = LoadHtmlTemplate("~/Templates/iom_zakat.html");

                    // 2. Replace data (SAMA seperti PDF)
                    html = html.Replace("{{NO_IOM}}", r["NO_IOM"].ToString());
                    html = html.Replace("{{JUMLAH_PESERTA}}", r["JUMLAH_PESERTA"].ToString());
                    html = html.Replace("{{JENIS_TRANSAKSI}}", r["JENIS_TRANSAKSI"].ToString());
                    html = html.Replace("{{TANGGAL_NAV}}",
                        Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy"));
                    html = html.Replace("{{PERIODE_ZAKAT}}", r["PERIODE_ZAKAT"].ToString());

                    html = html.Replace("{{TOTAL_ZAKAT}}",
                        Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N2", new CultureInfo("id-ID")));
                    html = html.Replace("{{TOTAL_BIAYA}}",
                        Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N2", new CultureInfo("id-ID")));
                    html = html.Replace("{{TOTAL_BAYAR}}",
                        Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N2", new CultureInfo("id-ID")));
                    DateTime tempDate;
                    string updateDate = DateTime.TryParse(r["UPDATEDATE"]?.ToString(), out tempDate)
                        ? tempDate.ToString("dd/MM/yyyy")
                        : "dd/MM/yyyy";

                    html = html.Replace("{{UPDATEDATE}}", updateDate);

                    // detail rows
                    StringBuilder detailRows = new StringBuilder();
                    foreach (DataRow d in dt.Rows)
                    {
                        detailRows.Append($@"
                                            <tr>
                                                <td>
                                                    Nomor Rekening: {d["NO_REKENING"]}<br/>
                                                    Nama Rekening: {d["NAMA_REKENING"]}<br/>
                                                    Bank: {d["BANK_NAME"]}
                                                </td>
                                                <td>{d["RANGE_DARI"]} s/d {d["RANGE_SAMPAI"]}</td>
                                                <td>{Convert.ToDecimal(d["JUMLAH"]).ToString("N2", new CultureInfo("id-ID"))}</td>
                                            </tr>");
                    }

                    html = html.Replace("{{DETAIL_ROWS}}", detailRows.ToString());

                    // 3. Convert HTML → Word
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (WordprocessingDocument wordDoc =
                            WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
                        {
                            var mainPart = wordDoc.AddMainDocumentPart();
                            mainPart.Document = new WordDocument();

                            // inject HTML langsung
                            string altChunkId = "AltChunkId1";
                            var chunk = mainPart.AddAlternativeFormatImportPart(
                                AlternativeFormatImportPartType.Html, altChunkId);

                            using (var stream = chunk.GetStream())
                            using (var writer = new StreamWriter(stream, Encoding.UTF8))
                            {
                                writer.Write(html);
                            }

                            var altChunk = new AltChunk { Id = altChunkId };
                            mainPart.Document.Body = new BodyWord();
                            mainPart.Document.Body.Append(altChunk);

                            mainPart.Document.Save();
                        }

                        byte[] bytes = ms.ToArray();

                        Response.Clear();
                        Response.ContentType =
                            "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        Response.AddHeader("content-disposition",
                            "attachment;filename=IOM_" + noIom.Replace("/", "_") + ".docx");

                        Response.BinaryWrite(bytes);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error Word: " + ex.Message, true);
            }
        }
        
        private void BindDetailGrid(string noIOM, GridView gvDetail)
        {
            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GET_MAKER_ZAKAT_DETAIL", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NO_IOM", noIOM);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            dt.Rows.Add(dt.NewRow()); 
                            gvDetail.DataSource = dt;
                            gvDetail.DataBind();
                            gvDetail.Rows[0].Visible = false; 
                        }
                        else
                        {
                            gvDetail.DataSource = dt;
                            gvDetail.DataBind();
                        }
                    }
                }
            }

        }
        
        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridView gvDetail = (GridView)sender;
                GridViewRow detailFooter = gvDetail.FooterRow;
                GridViewRow headerRow = (GridViewRow)gvDetail.NamingContainer;

                TextBox txtNewTransferKe = (TextBox)detailFooter.FindControl("txtNewTransferKe");
                TextBox txtNewNoRek = (TextBox)detailFooter.FindControl("txtNewNoRek");
                TextBox txtNewNamaRek = (TextBox)detailFooter.FindControl("txtNewNamaRek");
                DropDownList ddlNewBank = (DropDownList)detailFooter.FindControl("ddlNewBank");
                TextBox txtNewRangeDari = (TextBox)detailFooter.FindControl("txtNewRangeDari");
                TextBox txtNewRangeSampai = (TextBox)detailFooter.FindControl("txtNewRangeSampai");
                TextBox txtNewJumlah = (TextBox)detailFooter.FindControl("txtNewJumlah");


                string transferKe = string.IsNullOrEmpty(txtNewTransferKe.Text) ? "" : txtNewTransferKe.Text.Trim();
                string noRek = string.IsNullOrEmpty(txtNewNoRek.Text) ? "" : txtNewNoRek.Text.Trim();
                string namaRek = string.IsNullOrEmpty(txtNewNamaRek.Text) ? "" : txtNewNamaRek.Text.Trim();
                string bank = string.IsNullOrEmpty(ddlNewBank.SelectedValue) ? "" : ddlNewBank.SelectedValue;
                
                string bankText = ddlNewBank.SelectedItem.Text; // "014 - BANK CENTRAL ASIA"
                string bankName = bankText.Contains("-")
                                    ? bankText.Split('-')[1].Trim()
                                    : bankText;

                int rangeDari = string.IsNullOrWhiteSpace(txtNewRangeDari.Text) ? 0 : int.Parse(txtNewRangeDari.Text);
                int rangeSampai = string.IsNullOrWhiteSpace(txtNewRangeSampai.Text) ? 0 : int.Parse(txtNewRangeSampai.Text);
                
                decimal jumlah;
                decimal.TryParse(
                    txtNewJumlah.Text,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out jumlah
                );

                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                string noIom = ViewState["CurrentIOM"].ToString();

                int tahunZakat = GetHeaderPeriodeZakat(noIom);
                int maxRange = GetMaxRangeZakat(tahunZakat);


                if (e.CommandName == "AddNew")
                {
                    if (detailFooter == null) return;
                    if (noIom == null || string.IsNullOrEmpty(noIom))
                    {
                        ShowMessage("Nomor IOM tidak ditemukan.", true);
                        return;
                    }

                    if (!ValidateRange(rangeDari, rangeSampai))
                        return;

                    if (rangeSampai > maxRange)
                    {
                        ShowMessage($"Range data hanya tersedia sampai {maxRange}.", true);
                        return;
                    }

                    if (rangeDari > maxRange)
                    {
                        ShowMessage($"Range dari tidak boleh lebih besar dari {maxRange}.", true);
                        return;
                    }

                    var result = GetJumlahZakatByRange(noIom, rangeDari, rangeSampai);
                    decimal jumlahAuto = result.totalZakat;

                    if (jumlahAuto <= 0)
                    {
                        ShowMessage("Data zakat tidak ditemukan pada range tersebut.", true);
                        return;
                    }

                    jumlah = jumlahAuto;

                    conn.QueryString =
                            "EXEC SP_SAVE_MAKER_ZAKAT_DTL " +
                            "@NO_IOM = '" + noIom + "', " +
                            "@TRANSFER_KE = '" + transferKe + "', " +
                            "@NO_REKENING = '" + noRek + "', " +
                            "@NAMA_REKENING = '" + namaRek.Replace("'", "''") + "', " +
                            "@BANK = '" + bank + "', " +
                            "@BANK_NAME = '" + bankName + "', " +
                            "@RANGE_DARI = " + rangeDari + ", " +
                            "@RANGE_SAMPAI = " + rangeSampai + ", " +
                            "@JUMLAH = " + jumlah.ToString(CultureInfo.InvariantCulture) + ", " +
                            "@CREATEBY = '" + user + "'";

                    conn.ExecuteQuery();

                    BindDetailGrid(noIom, gvDetail);
                    ShowMessage("Detail IOM berhasil ditambahkan.");
                }
                else if (e.CommandName == "Inquiry")
                {
                    //BindDetailGrid(noIom, gvDetail);

                    GridViewRow row = (GridViewRow)((ControlUI)e.CommandSource).NamingContainer;

                    TextBox txtJumlah = (TextBox)gvDetail.FooterRow.FindControl("txtNewJumlah");

                    if (rangeDari != 0 && rangeSampai != 0)
                    {

                        if (rangeSampai > maxRange)
                        {
                            ShowMessage($"Range data hanya tersedia sampai {maxRange}.", true);
                            return;
                        }

                        if (rangeDari > maxRange)
                        {
                            ShowMessage($"Range dari tidak boleh lebih besar dari {maxRange}.", true);
                            return;
                        }

                        var result = GetJumlahZakatByRange(noIom, rangeDari, rangeSampai);
                        decimal jumlahAuto = result.totalZakat;

                        if (jumlahAuto <= 0)
                        {
                            ShowMessage("Data zakat tidak ditemukan pada range tersebut.", true);
                            return;
                        }

                        // Auto fill ke textbox Jumlah
                        txtJumlah.Text = jumlahAuto.ToString("N2", new CultureInfo("id-ID"));
                    }

                    //string accountNo = ((Label)row.FindControl("lblNoRekening")).Text;
                    //string accountName = ((Label)row.FindControl("lblNamaRekening")).Text;

                    //DropDownList ddlBank = (DropDownList)row.FindControl("lblBank");
                    //string bankCode = ddlBank != null ? ddlBank.SelectedValue : "";

                    // ambil clearing code
                    conn.QueryString = @"SELECT CLEARING_CODE FROM FINANCE.dbo.PARAM_TBL_BANK WHERE KODE = '" + bank + "'";
                    conn.ExecuteQuery(3000);

                    string clearingCode = conn.GetFieldValue("CLEARING_CODE")
                                              .ToString()
                                              .Substring(0, 3)
                                              .Trim();

                    string accounBMITakaful = "3040031803";
                    string transferAmount = "0";
                    string transferDesc = "-";
                    string userBy = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                    conn.QueryString = @" EXEC FINANCE.dbo.SP_API_BMI_INQUIRY '" + clearingCode + "','" + accounBMITakaful + "','" + noRek + "','" + transferAmount + "','" + transferDesc + "','" + userBy + "'";
                    conn.ExecuteQuery(3000);

                    if (conn.GetRowCount() > 0)
                    {
                        string errorCode = conn.GetFieldValue("errorCode").ToString();

                        if (errorCode == "00")
                        {
                            string destName = conn.GetFieldValue("toAccName").ToString().Replace("'", " ");
                            string destAccNo = noRek.Replace("'", " ");
                            string destBank = ddlNewBank.SelectedItem.Text.Replace("'", " ");

                            string script =
                                "Swal.fire({" +
                                "title: 'Inquiry Success'," +
                                "icon: 'success'," +
                                "html: `" +
                                    "<table style='width:100%; text-align:left;'>" +
                                        "<tr>" +
                                            "<td><b>Destination Name</b></td><td>:</td><td>" + destName + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td><b>Destination Account No</b></td><td>:</td><td>" + destAccNo + "</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                            "<td><b>Destination Bank</b></td><td>:</td><td>" + destBank + "</td>" +
                                        "</tr>" +
                                    "</table>`," +
                                "confirmButtonText: 'OK'" +
                                "});";

                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "InquirySuccess",
                                script,
                                true
                            );
                        }
                        else
                        {
                            string errorDesc = conn.GetFieldValue("errorDesc").ToString().Replace("'", " ");

                            ScriptManager.RegisterStartupScript(
                                this,
                                this.GetType(),
                                "InquiryError",
                                "Swal.fire('Inquiry Failed', '" + errorDesc + "', 'error');",
                                true
                            );
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                int index = msg.IndexOf("Last Query");
                if (index >= 0)
                {
                    msg = msg.Substring(0, index).Trim();
                }

                ShowMessage("Error di detail grid: " + msg, true);
            }

        }
        
        private void PopulatePeriodeDropdown()
        {
            ddlPeriode.Items.Clear();
            int yearNow = DateTime.Now.Year;
            for (int y = yearNow - 5; y <= yearNow + 5; y++)
                ddlPeriode.Items.Add(new System.Web.UI.WebControls.ListItem(y.ToString(), y.ToString()));
            ddlPeriode.SelectedValue = yearNow.ToString();
        }
        
        private void ShowMessage(string message, bool isError = false)
        {
            string icon = isError ? "error" : "success";
            string title = isError ? "Error" : "Berhasil";
            string script =
                        "Swal.fire({" +
                        "title: '" + title.Replace("'", "\\'") + "', " +
                        "text: '" + message.Replace("'", "\\'") + "', " +
                        "icon: '" + icon + "', " +
                        "confirmButtonText: 'OK'" +
                        "});";


            ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", script, true);
        }
        
        protected void btnSaveHeader_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTanggalNAV.Text) ||
                    string.IsNullOrWhiteSpace(ddlPeriode.SelectedValue) ||
                    string.IsNullOrWhiteSpace(txtJumlahPeserta.Text) ||
                    string.IsNullOrWhiteSpace(txtTotalZakat.Text) ||
                    string.IsNullOrWhiteSpace(txtTotalBayar.Text) ||
                    string.IsNullOrWhiteSpace(txtTotalBiaya.Text))
                {
                    ShowMessage("Lengkapi semua field header terlebih dahulu.", true);
                    return;
                }

                DateTime tanggalNav;
                if (!DateTime.TryParseExact(txtTanggalNAV.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tanggalNav))
                {
                    ShowMessage("Format Tanggal NAV tidak valid. Gunakan dd/MM/yyyy.", true);
                    return;
                }

                int periode = int.Parse(ddlPeriode.SelectedValue);
                int jumlahPeserta = int.Parse(txtJumlahPeserta.Text.Trim());

                decimal? totalZakat = SafeParseDecimalNoDecimal(txtTotalZakat.Text);
                decimal totalBiaya = SafeParseDecimalNoDecimal(txtTotalBiaya.Text);
                decimal totalBayar = SafeParseDecimalNoDecimal(txtTotalBayar.Text);

                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                if (totalZakat == 0)
                {
                    DataRow dr = GetSummaryTotalZakat(periode);

                    if (dr != null)
                        totalZakat = Convert.ToDecimal(dr["Total_Zakat"]);
                }

                if (!totalZakat.HasValue || totalZakat < 0)
                {
                    ShowMessage("Total Zakat tidak valid.", true);
                    return;
                }

                conn.QueryString =
                     "DECLARE @OUT_NO_IOM NVARCHAR(30); " +
                     "EXEC SP_SAVE_MAKER_ZAKAT_HDR " +
                     "    @NO_IOM = NULL, " +
                     "    @JUMLAH_PESERTA = " + jumlahPeserta + ", " +
                     "    @JENIS_TRANSAKSI = 'ZAKAT', " +
                     "    @TANGGAL_NAV = '" + tanggalNav.ToString("yyyy-MM-dd") + "', " +
                     "    @PERIODE_ZAKAT = " + periode + ", " +
                     "    @TOTAL_ZAKAT = " + totalZakat?.ToString(CultureInfo.InvariantCulture) + ", " +
                     "    @TOTAL_BIAYA = " + totalBiaya.ToString(CultureInfo.InvariantCulture) + ", " +
                     "    @TOTAL_BAYAR = " + totalBayar.ToString(CultureInfo.InvariantCulture) + ", " +
                     "    @CREATEBY = '" + user.Replace("'", "''") + "', " +
                     "    @OUT_NO_IOM = @OUT_NO_IOM OUTPUT; " +
                     "SELECT @OUT_NO_IOM AS NO_IOM;";


                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    string noIom = dt.Rows[0]["NO_IOM"].ToString();
                    ShowMessage("Header tersimpan. NO_IOM: " + noIom);
                    BindHeaderGrid();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error saat simpan header: " + ex.Message, true);
            }
        }

        //private decimal SafeParseDecimalNoDecimal(string s)
        //{
        //    if (string.IsNullOrWhiteSpace(s)) return 0m;
        //    var cleaned = s.Replace(".", "").Replace(",", ".");
        //    decimal v;
        //    if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out v)) return v;
        //    return 0m;
        //}
        private decimal SafeParseDecimalNoDecimal(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0m;

            decimal result;
            if (decimal.TryParse(
                s,
                NumberStyles.Any,
                new CultureInfo("id-ID"),
                out result))
            {
                return result;
            }

            return 0m;
        }

        private int GetHeaderJumlahPeserta(string noIom)
        {
            conn.QueryString =
                       "SELECT JUMLAH_PESERTA " +
                       "FROM MAKER_ZAKAT_HDR " +
                       "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";
            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count > 0) return int.Parse(dt.Rows[0]["JUMLAH_PESERTA"].ToString());
            return 0;
        }

        private decimal GetHeaderTotalBayar(string noIom)
        {
            conn.QueryString =
                         "SELECT TOTAL_BAYAR " +
                         "FROM MAKER_ZAKAT_HDR " +
                         "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";
            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count > 0) return decimal.Parse(dt.Rows[0]["TOTAL_BAYAR"].ToString(), CultureInfo.InvariantCulture);
            return 0m;
        }

        private decimal GetTotalDetailSum(string noIom)
        {
            conn.QueryString =
                         "SELECT ISNULL(SUM(JUMLAH),0) AS TOTAL " +
                         "FROM MAKER_ZAKAT_DTL " +
                         "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";
            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count > 0) return decimal.Parse(dt.Rows[0]["TOTAL"].ToString(), CultureInfo.InvariantCulture);
            return 0m;
        }

        private bool IsRangeOverlap(string noIom, int? excludeIdDetail, int from, int to)
        {
            string sql =
                "SELECT RANGE_DARI, RANGE_SAMPAI " +
                "FROM MAKER_ZAKAT_DTL " +
                "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";

            if (excludeIdDetail.HasValue)
                sql += " AND ID_DETAIL <> " + excludeIdDetail.Value;

            conn.QueryString = sql;
            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable();

            foreach (DataRow r in dt.Rows)
            {
                int f = int.Parse(r["RANGE_DARI"].ToString());
                int t = int.Parse(r["RANGE_SAMPAI"].ToString());

                // overlap check
                if (!(to < f || from > t))
                    return true;
            }

            return false;
        }

        private decimal GetDetailJumlahById(int id)
        {
            conn.QueryString =
        "SELECT ISNULL(JUMLAH,0) AS JUMLAH " +
        "FROM MAKER_ZAKAT_DTL " +
        "WHERE ID_DETAIL = " + id;
            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count > 0) return decimal.Parse(dt.Rows[0]["JUMLAH"].ToString(), CultureInfo.InvariantCulture);
            return 0m;
        }
        
        [WebMethod]
        public static string DeleteDetail(string id)
        {
            try
            {
                // Create a new Connection instance for static method context
                Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
                conn.QueryString =
                                    "EXEC SP_DELETE_MAKER_ZAKAT_DTL @ID_DETAIL = '" + id.Replace("'", "''") + "'";
                conn.ExecuteNonQuery();

                return "OK";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        protected void btnHiddenDeleteHeader_Click(object sender, EventArgs e)
        {
            string noIom = Request["__EVENTARGUMENT"];
            if (!string.IsNullOrEmpty(noIom))
            {
                DeleteHeader(noIom);
            }
        }

        private void DeleteHeader(string noIom)
        {
            try
            {
                conn.QueryString = "EXEC SP_DELETE_MAKER_ZAKAT_HDR @NO_IOM = '" + noIom.Replace("'", "''") + "'";

                conn.ExecuteNonQuery();

                ShowMessage("Header berhasil dihapus beserta detailnya.");
                BindHeaderGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("Error hapus header: " + ex.Message, true);
            }
        }
        
        private void UpdateZakatStatus(string noIom, string status, string note)
        {
            try
            {
                string userId = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")
                    .Replace("'", "''");

                string safeNoIom = noIom.Replace("'", "''");
                string safeStatus = status.Replace("'", "''");
                string safeNote = note.Replace("'", "''");

                conn.QueryString =
                                "UPDATE LIFE.DBO.MAKER_ZAKAT_HDR " +
                                "SET STATUS = '" + safeStatus + "', " +
                                "    UPDATEBY = '" + userId + "', " +
                                "    UPDATEDATE = GETDATE(), " +
                                "    NOTE = '" + safeNote + "' " +
                                "WHERE NO_IOM = '" + safeNoIom + "'";

                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable();
                BindHeaderGrid();

                string script =
                            "Swal.fire({" +
                            "icon: 'success'," +
                            "title: 'Berhasil!'," +
                            "text: 'Data berhasil dikirim ke proses verification'," +
                            "confirmButtonText: 'OK'" +
                            "});";

                ScriptManager.RegisterStartupScript(this, GetType(), "VerificationSuccess", script, true);
            }
            catch (Exception ex)
            {
                string safeError = ex.Message.Replace("'", " ");

                ScriptManager.RegisterStartupScript(this, GetType(), "VerificationError",
                    "Swal.fire('Error', '" + safeError + "', 'error');", true);
            }
        }

        private int GetHeaderPeriodeZakat(string noIom)
        {
            conn.QueryString =
                "SELECT PERIODE_ZAKAT " +
                "FROM LIFE.DBO.MAKER_ZAKAT_HDR " +
                "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";

            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();

            if (dt.Rows.Count > 0)
                return int.Parse(dt.Rows[0]["PERIODE_ZAKAT"].ToString());

            return 0;
        }

        private string LoadHtmlTemplate(string path)
        {
            return File.ReadAllText(Server.MapPath(path));
        }

        private void AddSection(ExcelWorksheet ws, ref int row, string title)
        {
            ws.Cells[row, 1].Value = title;
            ws.Cells[row, 1, row, 4].Merge = true;
            ws.Cells[row, 1].Style.Font.Bold = true;
            ws.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            row++;
        }

        private void AddKeyValue(ExcelWorksheet ws, ref int row, string key, string value)
        {
            ws.Cells[row, 1].Value = key;
            ws.Cells[row, 2].Value = ":";
            ws.Cells[row, 3].Value = value;
            row++;
        }

        private void AddAmount(ExcelWorksheet ws, ref int row, string label, object value, bool bold = false)
        {
            ws.Cells[row, 1].Value = label;
            ws.Cells[row, 3].Value = Convert.ToDecimal(value);
            ws.Cells[row, 3].Style.Numberformat.Format = "#,##0";
            ws.Cells[row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            if (bold)
                ws.Cells[row, 1, row, 3].Style.Font.Bold = true;

            row++;
        }

        private (decimal totalBayar, decimal totalBiaya, decimal totalZakat) GetJumlahZakatByRange(string noIom, int rangeDari, int rangeSampai)
        {
            int tahunZakat = GetHeaderPeriodeZakat(noIom);

            decimal totalBayar = 0;
            decimal totalBiaya = 0;
            decimal totalZakat = 0;

            using (SqlConnection sqlConn = new SqlConnection(
                GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("SP_GET_TOTAL_ZAKAT_BY_RANGE", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TAHUN_ZAKAT", tahunZakat);
                cmd.Parameters.AddWithValue("@RANGE_DARI", rangeDari);
                cmd.Parameters.AddWithValue("@RANGE_SAMPAI", rangeSampai);

                sqlConn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        totalBayar = reader["TOTAL_BAYAR"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_BAYAR"]) : 0;
                        totalBiaya = reader["TOTAL_BIAYA"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_BIAYA"]) : 0;
                        totalZakat = reader["TOTAL_ZAKAT"] != DBNull.Value ? Convert.ToDecimal(reader["TOTAL_ZAKAT"]) : 0;
                    }
                }
            }

            return (totalBayar, totalBiaya, totalZakat);
        }

        private bool ValidateRange(int rangeDari, int rangeSampai)
        {
            if (rangeDari == 0 || rangeSampai == 0)
            {
                ShowMessage("Range harus diisi.", true);
                return false;
            }

            if (rangeSampai < rangeDari)
            {
                ShowMessage("Range Sampai tidak boleh lebih kecil dari Range Dari.", true);
                return false;
            }

            return true;
        }

        private int GetMaxRangeZakat(int tahunZakat)
        {
            using (SqlConnection sqlConn = new SqlConnection(
                GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand(@"
                                                    SELECT MAX([NO]) 
                                                    FROM dbo.T_RPT_APPLICATION_ZAKAT_NISHOB
                                                    WHERE TAHUN_ZAKAT = @TAHUN_ZAKAT", sqlConn))
            {
                cmd.Parameters.AddWithValue("@TAHUN_ZAKAT", tahunZakat);

                sqlConn.Open();
                object result = cmd.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private DataRow GetUserLogin()
        {
            string userCode = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

            using (SqlConnection conn = new SqlConnection(GlobalUse.GetConnString(
                System.Configuration.ConfigurationManager.AppSettings["appid"])))
            {
                using (SqlCommand cmd = new SqlCommand(@"
                                                        SELECT 
                                                            CODE,
                                                            FRONT_NAME + ' ' + ISNULL(MID_NAME,'') + ' ' + LAST_NAME AS FULLNAME,
                                                            SIGNATURE
                                                        FROM SECURITY.DBO.M_USERS
                                                        WHERE CODE = @CODE", conn))
                {
                    cmd.Parameters.AddWithValue("@CODE", userCode);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
        }

        private DataRow GetSummaryTotalZakat(int tahun)
        {
            using (SqlConnection sqlConn = new SqlConnection(
                GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("SP_REPORT_NISHOB_SUMMARY", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@YEAR", tahun);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
        }

        private bool HasDetail(string noIom)
        {
            conn.QueryString =
                "SELECT COUNT(1) CNT FROM MAKER_ZAKAT_DTL " +
                "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";

            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();

            if (dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0]["CNT"]);
                return count > 0;
            }

            return false;
        }
    }
}
