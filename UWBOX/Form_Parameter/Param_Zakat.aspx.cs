using ClosedXML.Excel;
using DMS.DBConnection;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWBOX.Class;

namespace UWBOX.Form_Parameter
{
    public partial class Param_Zakat : System.Web.UI.Page
    {
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

        public DataTable SaveZakatParamSecure(int tahun, decimal? nishob, double? tarif, bool kabisat, double? biayaPersen, decimal? biayaNominal, string user, Connection conn)
        {
            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("SP_PARAM_ZAKAT", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300; // ⏰ timeout 5 menit

                // Tambahkan semua parameter dengan aman
                cmd.Parameters.AddWithValue("@TAHUN_ZAKAT", tahun);
                cmd.Parameters.AddWithValue("@NISHOB_ZAKAT", nishob);
                cmd.Parameters.AddWithValue("@TARIF_ZAKAT", tarif);
                cmd.Parameters.AddWithValue("@KABISAT", kabisat);
                cmd.Parameters.AddWithValue("@BIAYA_PERSEN", biayaPersen);
                cmd.Parameters.AddWithValue("@BIAYA_NOMINAL", biayaNominal);
                cmd.Parameters.AddWithValue("@STATUS", "DRAFT");
                cmd.Parameters.AddWithValue("@CREATEBY", user);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["download"] == "1")
            //{
            //    DownloadExcel(Request.QueryString["tahun"]);
            //    return;
            //}

            if (!IsPostBack)
            {
                LoadTahunZakat();
                BindGrid();
            }
        }

        public static class FormatHelper
        {
            public static decimal? ParseDecimalFromRupiah(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return null;

                text = text.Trim();

                text = Regex.Replace(text, "(?i)rp", "");
                text = text.Replace(" ", "")
                           .Replace(".", "")
                           .Replace(",", ".");

                decimal result;
                if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                    return result;

                return null;
            }
            
            public static string FormatToRupiah(decimal? value)
            {
                if (value == null)
                    return string.Empty;

                // Gunakan format Indonesia (ID)
                CultureInfo culture = new CultureInfo("id-ID");
                return string.Format(culture, "Rp {0:N2}", value);
            }

            public static double? ParseDoubleFromIndo(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return null;

                text = text.Trim()
                           .Replace(".", "")   // hapus ribuan
                           .Replace(",", "."); // ubah desimal ke format internasional

                if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    return result;

                return null;
            }

        }

        private void LoadTahunZakat()
        {
            ddlTahunZakat.Items.Clear();
            int tahunSekarang = DateTime.Now.Year;
            for (int i = tahunSekarang - 5; i <= tahunSekarang + 5; i++)
            {
                ddlTahunZakat.Items.Add(i.ToString());
            }
            ddlTahunZakat.SelectedValue = tahunSekarang.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int tahun = int.Parse(ddlTahunZakat.SelectedValue);

                decimal? nishob = FormatHelper.ParseDecimalFromRupiah(txtNishobZakat.Text);

                double? tarif = FormatHelper.ParseDoubleFromIndo(txtTarifZakat.Text);

                bool kabisat = chkKabisat.Checked;

                double? biayaPersen = FormatHelper.ParseDoubleFromIndo(txtBiayaZakatPersen.Text);

                decimal? biayaNominal = FormatHelper.ParseDecimalFromRupiah(txtBiayaZakatNominal.Text);

                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") ?? "SYSTEM";

                DataTable dt = SaveZakatParamSecure(tahun, nishob, tarif, kabisat, biayaPersen, biayaNominal, user, conn: conn);

                if (dt.Rows.Count > 0)
                {
                    string action = dt.Rows[0]["ACTION"].ToString();
                    if (action == "LOCKED")
                    {
                        ShowMessage("Data sudah dikirim (SENT) dan tidak bisa diubah lagi!", true);
                        return;
                    }
                    else if (action == "UPDATED")
                        ShowMessage("Data berhasil diperbarui ke database.");
                    else
                        ShowMessage("Data berhasil disimpan ke database.");
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error saat simpan: " + ex.Message, true);
            }
        }


        public DataTable SendZakatParam(string tahun, string user, Connection conn)
        {
            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("SP_SEND_ZAKAT_INVESTMENT", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;

                cmd.Parameters.AddWithValue("@USERBY", user);
                cmd.Parameters.AddWithValue("@YEAR", tahun);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                int tahun = int.Parse(ddlTahunZakat.SelectedValue);
                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") ?? "SYSTEM";
                DataTable dt = SendZakatParam(tahun.ToString(), user, conn: conn);

                if (dt.Rows.Count > 0)
                {
                    string action = dt.Rows[0]["ACTION"].ToString();
                    if (action == "LOCKED")
                    {
                        ShowMessage("Data sudah dikirim (SENT) dan tidak bisa diubah lagi!", true);
                        return;
                    }
                }
                ShowMessage("Data berhasil dikirim (status = SENT).");
                BindGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("Error saat kirim: " + ex.Message, true);
            }
        }

        private void BindGrid()
        {
            conn.QueryString = @"
            SELECT 
                TAHUN_ZAKAT, 
                NISHOB_ZAKAT, 
                TARIF_ZAKAT, 
                KABISAT, 
                BIAYA_PERSEN, 
                BIAYA_NOMINAL, 
                STATUS,
                CREATED_AT,
                CREATEBY,
                LASTCHANGEBY,
                LASTCHANGEDATE
            FROM PARAM_ZAKAT_NEW 
            ORDER BY TAHUN_ZAKAT DESC";

            conn.ExecuteQuery();
            GridViewZakat.DataSource = conn.GetDataTable();
            GridViewZakat.DataBind();
        }

        //private void ShowMessage(string message, bool isError = false)
        //{
        //    lblMessage.Text = message;
        //    lblMessage.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        //    lblMessage.Style["display"] = "block";

        //    string script = @"
        //        setTimeout(function() {
        //            var lbl = document.getElementById('" + lblMessage.ClientID + @"');
        //            if(lbl) lbl.style.display = 'none';
        //        }, 3000);";

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLabel", script, true);
        //}

        private void ShowMessage(string message, bool isError = false)
        {
            string icon = isError ? "error" : "success";

            string script = @"
                                Swal.fire({{
                                    title: '{" + isError + @" ? ""Error"" : ""Berhasil"")}',
                                    text: '{" + message.Replace("'", "\\'") + @"}',
                                    icon: '" + icon + @"',
                                    confirmButtonText: 'OK'
                                }});
                            ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "SweetAlertMessage", script, true);
        }

        protected void GridViewZakat_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewZakat.SelectedRow;
            if (row != null)
            {
                ddlTahunZakat.SelectedValue = row.Cells[1].Text.Trim();

                var txtNishobZakats = row.Cells[2].Text;
                var txtNishobValue = FormatHelper.ParseDecimalFromRupiah(txtNishobZakats);
                txtNishobZakat.Text = row.Cells[2].Text; //FormatHelper.FormatToRupiah(txtNishobValue ?? 0);

                txtTarifZakat.Text = row.Cells[3].Text;
                chkKabisat.Checked = row.Cells[4].Text == "1" || row.Cells[4].Text.ToLower() == "true";
                txtBiayaZakatPersen.Text = Server.HtmlDecode(row.Cells[5].Text);

                var rawText = row.Cells[6].Text;
                var nominalValue = FormatHelper.ParseDecimalFromRupiah(rawText);
                txtBiayaZakatNominal.Text = row.Cells[6].Text;//FormatHelper.FormatToRupiah(nominalValue ?? 0);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string tahun = ddlTahunZakat.SelectedValue;

            //string reportUrl = "../../ReportViewer/Viewer.aspx" +
            //                   "?REPORT=LIFE_Report/RPT_APPLICATION_ZAKAT_NISHOB" +
            //                   "&YEAR=" + tahun;
            //string reportUrl = "../../ReportViewer/Viewer.aspx?APPID=LF&CODE=106"; //&YEAR=" + tahun + 
            //"&PAYMENT_STATUS=ALL&DELIVERY_STATUS=ALL&DELIVERY_MEDIA=ALL";

            //ClientScript.RegisterStartupScript(
            //    GetType(),
            //    "",
            //    "<script language='javascript'>" +
            //        "parent.content.location.href = '" + reportUrl + "';" +
            //    "</script>"
            //);
            //ScriptManager.RegisterStartupScript(
            //    this,
            //    GetType(),
            //    "OpenReport",
            //    "window.open('" + reportUrl + "', '_blank');",
            //    true
            //);
            //LoadReport(tahun);
            //frameReport.Attributes["src"] = reportUrl;
            //pnlReport.Visible = true;
            BindGridNishobZakat();
            btnDownload.Visible = true;
        }

        private void BindGridNishobZakat()
        {
            //conn.QueryString = "EXEC LIFE.dbo.RPT_APPLICATION_ZAKAT_NISHOB " + ddlTahunZakat.SelectedValue + ",'ALL','ALL','ALL';";
            //conn.ExecuteQuery();
            //gvZakatNishob.DataSource = conn.GetDataTable();
            //gvZakatNishob.DataBind();

            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("LIFE.dbo.RPT_APPLICATION_ZAKAT_NISHOB", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 420; // ⏰ timeout 7 menit

                // Tambahkan semua parameter dengan aman
                cmd.Parameters.AddWithValue("@YEAR", ddlTahunZakat.SelectedValue);
                cmd.Parameters.AddWithValue("@PAYMENT_STATUS", "ALL");
                cmd.Parameters.AddWithValue("@DELIVERY_STATUS", "ALL");
                cmd.Parameters.AddWithValue("@DELIVERY_MEDIA", "ALL");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);
                    gvZakatNishob.DataSource = dt;
                    gvZakatNishob.DataBind();
                }
            }
        }

        //private void LoadReport(string tahun)
        //{
        //    string appid = "LF"; // sesuaikan
        //    string code = "106";    // sesuaikan

        //    conn.QueryString = "select " +
        //                        "URL = replace(b.URL, '/Pages/ReportViewer.aspx?%2f' + b.FOLDER + '%2f' + b.REPORT_NAME + '&rs:Command=Render',''),  " +
        //                        "PATH = '/' + b.FOLDER + '/' + b.REPORT_NAME, " +
        //                        "ParameterName = a.ParameterName  " +
        //                        "from SECURITY.dbo.REPORT_LIST b " +
        //                        "left join SECURITY.dbo.V_REPORT_PARAMETER a on a.APP_ID = b.APP_ID and a.CODE = b.CODE " +
        //                        "where " +
        //                        "b.APP_ID = '" + appid + "' " +
        //                        "and b.CODE = " + code;

        //    conn.ExecuteQuery();

        //    string URL = conn.GetFieldValue("URL").ToString();
        //    string PATH = conn.GetFieldValue("PATH").ToString();

        //    RV.ProcessingMode = ProcessingMode.Remote;
        //    RV.ServerReport.ReportServerUrl = new Uri(URL);
        //    RV.ServerReport.ReportPath = PATH;

        //    RV.ServerReport.ReportServerCredentials = new ReportServerCredentials();

        //    ReportParameter[] parameters = new ReportParameter[]
        //    {
        //        new ReportParameter("YEAR", tahun)
        //    };

        //    RV.ServerReport.SetParameters(parameters);
        //    RV.ServerReport.Refresh();

        //    pnlReport.Visible = true;
        //}

        protected void GridViewZakat_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = "";
                if (DataBinder.Eval(e.Row.DataItem, "STATUS") != null)
                    status = DataBinder.Eval(e.Row.DataItem, "STATUS").ToString();
                if (status == "SENT")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

        protected void gvZakatNishob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvZakatNishob.PageIndex = e.NewPageIndex;
            BindGridNishobZakat();
        }

        //private void DownloadExcel(string tahun)
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("LIFE.dbo.RPT_APPLICATION_ZAKAT_NISHOB", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 420; // ⏰ timeout 7 menit

                // Tambahkan semua parameter dengan aman
                cmd.Parameters.AddWithValue("@YEAR", ddlTahunZakat.SelectedValue);
                //cmd.Parameters.AddWithValue("@YEAR", tahun);
                cmd.Parameters.AddWithValue("@PAYMENT_STATUS", "ALL");
                cmd.Parameters.AddWithValue("@DELIVERY_STATUS", "ALL");
                cmd.Parameters.AddWithValue("@DELIVERY_MEDIA", "ALL");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);

                    var workbook = new XLWorkbook();
                    var ws = workbook.Worksheets.Add("RPT_APPLICATION_ZAKAT_NISHOB");
                    var headers = new[]
                    {
                        "NO", "REGNO", "MEMBER_ID", "FULL NAME", "POLICY NUMBER", "CODE", "FUND CODE", "STATUS", "FUND NAME", "WEALTH PROPORTION", "ZAKAT PROPORTION",
                        "CHARGE", "TOTAL ZAKAT", "NISHAB", "AMOUNT", "UNIT", "NAV DATE", "NAV VALUE", "STATUS", "TREASURY MEMO NUMBER", "PAYMENT DATE",
                        "PAYMENT STATUS", "GENDER", "MOBILE NUMBER", "DELIVERY DATE", "EMAIL", "DELIVERY STATUS", "DELIVERY MEDIA"
                    };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        ws.Cell(1, i + 1).Value = headers[i];
                    }

                    // ===============================
                    // 🔹 Header Kolom 
                    // ===============================
                    var headerRange = ws.Range(1, 1, 1, headers.Length);
                    headerRange.Style.Font.Bold = true;
                    //headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    // ===============================
                    // 🔹 Isi Data
                    // ===============================
                    int row = 2;
                    foreach (DataRow item in dt.Rows)
                    {
                        ws.Cell(row, 1).Value = item["NO"] == DBNull.Value ? "" : item["NO"].ToString();
                        ws.Cell(row, 2).Value = item["REGNO"] == DBNull.Value ? "" : item["REGNO"].ToString();
                        ws.Cell(row, 3).Value = item["MEMBER_ID"] == DBNull.Value ? "" : item["MEMBER_ID"].ToString();
                        ws.Cell(row, 4).Value = item["FULL_NAME"] == DBNull.Value ? "" : item["FULL_NAME"].ToString();
                        var policyNumber = item["POLICY_NUMBER"] == DBNull.Value ? "" : item["POLICY_NUMBER"].ToString();
                        ws.Cell(row, 5).Value = "'" + policyNumber;
                        ws.Cell(row, 6).Value = item["CODE"] == DBNull.Value ? "" : "'" + item["CODE"].ToString();
                        ws.Cell(row, 7).Value = item["FUND_CODE"] == DBNull.Value ? "" : "'" + item["FUND_CODE"].ToString();
                        ws.Cell(row, 8).Value = item["POLICY_STATUS"] == DBNull.Value ? "" : item["POLICY_STATUS"].ToString();
                        ws.Cell(row, 9).Value = item["FUND_NAME"] == DBNull.Value ? "" : item["FUND_NAME"].ToString();

                        ws.Cell(row, 10).Value = item["WEALTH_PROPORTION"] == DBNull.Value ? 0 : Convert.ToDecimal(item["WEALTH_PROPORTION"]);
                        ws.Cell(row, 10).Style.NumberFormat.Format = "#,##0.00";

                        ws.Cell(row, 11).Value = item["ZAKAT_PROPORTION"] == DBNull.Value ? 0 : Convert.ToDecimal(item["ZAKAT_PROPORTION"]);
                        ws.Cell(row, 11).Style.NumberFormat.Format = "#,##0.00";

                        ws.Cell(row, 12).Value = item["CHARGE"] == DBNull.Value ? 0 : Convert.ToDecimal(item["CHARGE"]);
                        ws.Cell(row, 12).Style.NumberFormat.Format = "#,##0.00";

                        ws.Cell(row, 13).Value = item["TOTAL_ZAKAT"] == DBNull.Value ? 0 : Convert.ToDecimal(item["TOTAL_ZAKAT"]);
                        ws.Cell(row, 13).Style.NumberFormat.Format = "#,##0.00";

                        ws.Cell(row, 14).Value = item["NISHAB"] == DBNull.Value ? "" : item["NISHAB"].ToString();
                        ws.Cell(row, 15).Value = item["AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(item["AMOUNT"]);
                        ws.Cell(row, 15).Style.NumberFormat.Format = "#,##0.00";

                        ws.Cell(row, 16).Value = item["UNIT"] == DBNull.Value ? "" : item["UNIT"].ToString();
                        ws.Cell(row, 17).Value = item["NAV_DATE"] == DBNull.Value ? "" : item["NAV_DATE"].ToString();
                        ws.Cell(row, 18).Value = item["NAV_VALUE"] == DBNull.Value ? "" : item["NAV_VALUE"].ToString();
                        ws.Cell(row, 19).Value = item["STATUS"] == DBNull.Value ? "" : item["STATUS"].ToString();
                        ws.Cell(row, 20).Value = item["TREASURY_MEMO_NUMBER"] == DBNull.Value ? "" : item["TREASURY_MEMO_NUMBER"].ToString();
                        ws.Cell(row, 21).Value = item["PAYMENT_DATE"] == DBNull.Value ? "" : item["PAYMENT_DATE"].ToString();
                        ws.Cell(row, 21).Value = item["PAYMENT_STATUS"] == DBNull.Value ? "" : item["PAYMENT_STATUS"].ToString();
                        ws.Cell(row, 23).Value = item["GENDER"] == DBNull.Value ? "" : item["GENDER"].ToString();
                        //ws.Cell(row, 23).Value = item["MOBILE_NUMBER"] == DBNull.Value ? "" : item["MOBILE_NUMBER"].ToString();
                        var mobile = item["MOBILE_NUMBER"] == DBNull.Value ? "" : item["MOBILE_NUMBER"].ToString();
                        ws.Cell(row, 24).Value = "'" + mobile;
                        ws.Cell(row, 25).Value = item["DELIVERY_DATE"] == DBNull.Value ? "" : item["DELIVERY_DATE"].ToString();
                        ws.Cell(row, 26).Value = item["EMAIL"] == DBNull.Value ? "" : item["EMAIL"].ToString();
                        ws.Cell(row, 27).Value = item["DELIVERY_STATUS"] == DBNull.Value ? "" : item["DELIVERY_STATUS"].ToString();
                        ws.Cell(row, 28).Value = item["DELIVERY_MEDIA"] == DBNull.Value ? "" : item["DELIVERY_MEDIA"].ToString();

                        row++;
                    }
                    int totalRows = row - 1;
                    int totalCols = headers.Length;

                    // ===============================
                    // 🔹 Border untuk seluruh area data
                    // ===============================
                    var dataRange = ws.Range(4, 1, totalRows, totalCols);
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    dataRange.Style.Border.OutsideBorderColor = XLColor.Black;
                    dataRange.Style.Border.InsideBorderColor = XLColor.Black;

                    // ===============================
                    // 🔹 Autofit & Freeze
                    // ===============================
                    ws.Columns().AdjustToContents();
                    ws.SheetView.FreezeRows(1);

                    // ===============================
                    // 🔹 Output ke File
                    // ===============================
                    var stream = new MemoryStream(); // ❌ jangan pakai using
                    workbook.SaveAs(stream);
                    //workbook.Dispose();
                    var fileName = "RPT_APPLICATION_ZAKAT_NISHOB_" + ddlTahunZakat.SelectedValue + ".xlsx";
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Cookies.Add(new HttpCookie("fileDownload", "true")
                    {
                        Path = "/"
                    });
                    Response.BinaryWrite(stream.ToArray());
                    Response.Flush();
                    Response.End();
                    //HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
        }

        
    }
}