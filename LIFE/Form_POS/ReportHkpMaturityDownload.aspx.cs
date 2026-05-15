using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace LIFE.Form_POS
{
    public partial class ReportHkpMaturityDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void ExportExcelFromSP()
        {
            //Update ke Office 365
            string connString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            DataTable dt = new DataTable();

            // Deklarasi variabel
            DateTime startDate, endDate;

            // Parsing tanggal
            DateTime.TryParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
            DateTime.TryParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            // === Load data dari Stored Procedure ===
            using (SqlConnection sqlConn = new SqlConnection(connString))
            {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand("RPT_HKP_CLM_all_Master", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1800;

                    cmd.Parameters.Add("@Tgl_Akhir_KontrakStart", SqlDbType.Date).Value = startDate;
                    cmd.Parameters.Add("@Tgl_Akhir_KontrakEnd", SqlDbType.Date).Value = endDate;
                    cmd.Parameters.Add("@Unitize_NonUnitize", SqlDbType.VarChar, 100).Value = ddlUnitize.SelectedValue;
                    cmd.Parameters.Add("@Currency", SqlDbType.VarChar, 100).Value = ddlCurrency.SelectedValue;
                    cmd.Parameters.Add("@Status_Pencairan", SqlDbType.VarChar, 100).Value = ddlStatusPencairan.SelectedValue;
                    cmd.Parameters.Add("@Status_Rekening", SqlDbType.VarChar, 100).Value = ddlStatusRekening.SelectedValue;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // === EPPlus setup ===
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Report");

                // Mapping kolom dan urutan (gunakan yang sudah kamu buat)
                var columnMap = new Dictionary<string, string>
            {
                { "No_Aplikasi", "No Aplikasi" },
                { "No_Polis", "No Polis" },
                { "Nama_Pemegang_Polis", "Nama Pemegang Polis" },
                { "Produk_Name", "Produk Name" },
                { "Tgl_Awal_Kontrak", "Tgl Awal Kontrak" },
                { "Tgl_Akhir_Kontrak", "Tgl Akhir Kontrak" },
                { "Unitize_NonUnitize", "Unitize/NonUnitize" },
                { "Currency", "Currency" },
                { "Fund_Amanah", "Fund Amanah" },
                { "Tgl_NAV_AMANAH", "Tgl NAV" },
                { "Nilai_NAV_AMANAH", "Nilai NAV" },
                { "Amount_AMANAH", "Amount" },
                { "Fund_Optima", "Fund Optima" },
                { "Tgl_NAV_OPTIMA", "Tgl NAV" },
                { "Nilai_NAV_OPTIMA", "Nilai NAV" },
                { "Amount_OPTIMA", "Amount" },
                { "Fund_Ekuita", "Fund Ekuita" },
                { "Tgl_NAV_EKUITA", "Tgl NAV" },
                { "Nilai_NAV_EKUITA", "Nilai NAV" },
                { "Amount_EKUITA", "Amounts" },
                { "Jumlah_Maturity", "Jumlah Maturity" },
                { "Kurs_$", "Kurs" },
                { "Jumlah_Maturity_RP", "Jumlah Maturity RP" },
                { "Status_Rekening", "Status Rekening" },
                { "Adjusment_Ujroh", "Adjusment Ujroh" },
                { "Adjusment_Tabarru", "Adjusment Tabarru" },
                { "Adjusment_Biaya_Bulanan", "Adjusment Biaya Bulanan" },
                { "Adjusment_Bagihasil", "Adjusment Bagi hasil" },
                { "Saldo_Maturity", "Saldo Maturity" },
                { "StatusHKP", "Status HKP" },
                { "AuthorDate", "Author Date" },
                { "Status_Pencairan", "Status Pencairan" },
                { "Tgl_PAID", "Tgl PAID" },
                { "Nominal_Bayar_ke_Rekening", "Nominal Bayar ke Rekening" },
                { "No_Memo", "No Memo" },
                { "Alamat_korespondensi", "Alamat Korespondensi" },
                { "Email", "Email" },
                { "Phone_1", "Phone 1" },
                { "Phone_2", "Phone 2" },
                { "No_Agen", "No Agen" },
                { "Tgl_Kirim_Informasi_HKP", "Tgl Kirim Informasi HKP" },
                { "Media_Kirim", "Media Kirim" },
                { "Status_Kirim", "Status Kirim" }
            };

                var columnOrder = columnMap.Keys.ToList();

                // === Tulis header ===
                for (int i = 0; i < columnOrder.Count; i++)
                {
                    var col = columnOrder[i];
                    ws.Cells[1, i + 1].Value = columnMap[col];
                    ws.Cells[1, i + 1].Style.Font.Bold = true;
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.CadetBlue);
                }

                // === Isi data ===
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < columnOrder.Count; c++)
                    {
                        var colName = columnOrder[c];
                        var cell = ws.Cells[r + 2, c + 1];
                        var value = dt.Rows[r][colName];

                        if (value == DBNull.Value)
                            cell.Value = null;
                        else if (dt.Columns[colName].DataType == typeof(DateTime))
                        {
                            cell.Value = Convert.ToDateTime(value);
                            cell.Style.Numberformat.Format = "dd/MM/yyyy";
                        }
                        else if (value is string s)
                            cell.Value = s.Replace("\u0000", ""); // hapus karakter null tersembunyi
                        else
                            cell.Value = value;
                    }
                }

                if (ws.Dimension != null)
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // === Siapkan response ===
                byte[] fileBytes = pck.GetAsByteArray();
                //string fileName = $"Report_HKP_Maturity_{DateTime.Now:yyyyMMdd}.xlsx";
                string fileName = string.Format("Report_HKP_Maturity_{0:yyyyMMdd}.xlsx", DateTime.Now);

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));


                var cookie = new HttpCookie("downloadFinished", "true")
                {
                    Path = "/",
                    Expires = DateTime.Now.AddMinutes(5)
                };
                Response.Cookies.Add(cookie);

                Response.BinaryWrite(fileBytes);
                Response.End(); 
            }
        }


        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            ExportExcelFromSP();          
        }
    }
}
