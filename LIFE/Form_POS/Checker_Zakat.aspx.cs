using AjaxControlToolkit;
using DMS.DBConnection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PageSizePdfDocument = iTextSharp.text.PageSize;
using PdfDocument = iTextSharp.text.Document;
using W = DocumentFormat.OpenXml.Wordprocessing;

namespace LIFE.Form_POS
{
    public partial class Checker_Zakat : System.Web.UI.Page
    {
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        public string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePeriodeDropdown();
                BindHeaderGrid();
                user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                LoadPeriode();
                LoadSummaryData();
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

            using (SqlConnection sqlConn = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"])))
            using (SqlCommand cmd = new SqlCommand("SP_REPORT_NISHOB_SUMMARY", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@YEAR", tahun);

                sqlConn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    DateTime navDate = Convert.ToDateTime(dr["LastNAV"]);
                    txtTanggalNAV.Text = navDate.ToString("dd/MM/yyyy");
                    txtJumlahPeserta.Text = dr["Jumlah_Peserta"].ToString();
                    txtTotalBiaya.Text = Convert.ToDecimal(dr["Total_Biaya"]).ToString("N2");
                    txtTotalZakat.Text = Convert.ToDecimal(dr["Total_Zakat"]).ToString("N2");
                    txtTotalBayar.Text = Convert.ToDecimal(dr["Total_Dibayarkan"]).ToString("N2");
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
        }

        protected void ddlPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSummaryData();   // <-- refresh data sesuai tahun yang dipilih
        }

        private void PopulatePeriodeDropdown()
        {
            ddlPeriode.Items.Clear();
            int yearNow = DateTime.Now.Year;
            for (int y = yearNow - 5; y <= yearNow + 5; y++)
                ddlPeriode.Items.Add(new System.Web.UI.WebControls.ListItem(y.ToString(), y.ToString()));
            ddlPeriode.SelectedValue = yearNow.ToString();
        }

        private void BindHeaderGrid()
        {
            conn.QueryString =
                            "SELECT * FROM MAKER_ZAKAT_HDR " +
                            "WHERE ISNULL(STATUS, 'OPEN') <> 'OPEN' " +
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
                GridView gvDetail = (GridView)e.Row.FindControl("gvDetail");

                //conn.QueryString = $"SELECT * FROM MAKER_ZAKAT_DTL WHERE NO_IOM = '{noIOM}' ORDER BY ID_DETAIL";
                conn.QueryString = string.Format("SELECT * FROM MAKER_ZAKAT_DTL WHERE NO_IOM = '{0}' ORDER BY ID_DETAIL",noIOM);

                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable().Copy();
                if (gvDetail != null)
                {
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
        private void LoadHeaderToForm(string noIom)
        {
            //conn.QueryString = $"SELECT * FROM MAKER_ZAKAT_HDR WHERE NO_IOM = '{noIom}'";
            conn.QueryString = string.Format("SELECT * FROM MAKER_ZAKAT_HDR WHERE NO_IOM = '{0}'",noIom);

            conn.ExecuteQuery();

            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtTanggalNAV.Text = Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy");
                ddlPeriode.SelectedValue = r["PERIODE_ZAKAT"].ToString();
                txtJumlahPeserta.Text = r["JUMLAH_PESERTA"].ToString();
                txtTotalZakat.Text = Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N2");
                txtTotalBiaya.Text = Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N2");
                txtTotalBayar.Text = Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N2");

                ViewState["EditIOM"] = noIom;

                //ShowMessage($"Data header No IOM {noIom} siap diubah.");
            }
        }
        protected void gvHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string noIom = e.CommandArgument.ToString();

            if (e.CommandName == "Expand")
            {
                GridViewRow selectedRow = ((Control)e.CommandSource).NamingContainer as GridViewRow;
                Panel detailPanel = (Panel)selectedRow.FindControl("pnlDetail");
                GridView gvDetail = (GridView)selectedRow.FindControl("gvDetail");
                Button btnExpand = (Button)selectedRow.FindControl("btnExpand");

                if (detailPanel.Visible)
                {
                    detailPanel.Visible = false;
                    btnExpand.Text = "+";
                }
                else
                {
                    foreach (GridViewRow row in gvHeader.Rows)
                    {
                        Panel pnl = (Panel)row.FindControl("pnlDetail");
                        Button btn = (Button)row.FindControl("btnExpand");
                        if (pnl != null) pnl.Visible = false;
                        if (btn != null) btn.Text = "+";
                    }

                    BindDetailGrid(noIom, gvDetail);
                    detailPanel.Visible = true;
                    btnExpand.Text = "-";
                    ViewState["CurrentIOM"] = noIom;
                }
            }

            else if (e.CommandName == "EditHeader")
            {
                LoadHeaderToForm(noIom);
            }

            else if (e.CommandName == "ExportPdf")
            {
                ExportPdfByNoIom(noIom, "CHECKER");
            }
            else if (e.CommandName == "ExportExcel")
            {
                ExportExcelByNoIom(noIom);
            }
            else if (e.CommandName == "ExportWord")
            {
                ExportWordByNoIom(noIom);
            }
            else if (e.CommandName == "Approve")
            {
                ApproveZakat(noIom);
            }
            else if (e.CommandName == "Reject")
            {
                UpdateZakatStatus(noIom, "REJECTED", "Data ditolak oleh checker.");
            }
            else if (e.CommandName == "BackToMaker")
            {
                UpdateZakatStatus(noIom, "BACK_TO_MAKER", "Data dikembalikan ke maker untuk revisi.");
            }
        }
        private void LoadHeaderData(string noIom)
        {
            //conn.QueryString = $"SELECT * FROM MAKER_ZAKAT_HDR WHERE NO_IOM = '{noIom}'";
            conn.QueryString = string.Format("SELECT * FROM MAKER_ZAKAT_HDR WHERE NO_IOM = '{0}'",noIom);

            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();
            if (dt.Rows.Count == 0)
            {
                ShowMessage("Data tidak ditemukan.", true);
                return;
            }

            DataRow r = dt.Rows[0];
            ViewState["CurrentIOM"] = noIom;

            ddlPeriode.SelectedValue = r["PERIODE_ZAKAT"].ToString();
            txtTanggalNAV.Text = Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy");
            txtJumlahPeserta.Text = r["JUMLAH_PESERTA"].ToString();
            txtTotalBiaya.Text = Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N2");
            txtTotalZakat.Text = Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N2");
            txtTotalBayar.Text = Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N2");

            ShowMessage("Data header {noIom} berhasil dimuat.");
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
        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // footer: load banks into ddlNewBank
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlNewBank = (DropDownList)e.Row.FindControl("ddlNewBank");
                if (ddlNewBank != null) LoadBanks(ddlNewBank);
            }
        }
        private void LoadBanks(DropDownList ddl)
        {
            ddl.Items.Clear();
            //conn.QueryString = "SELECT BANK_CODE, BANK_NAME FROM REF_BANK ORDER BY BANK_NAME";
            conn.QueryString = "select KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by KODE";
            conn.ExecuteQuery();
            ddl.Items.Add(new System.Web.UI.WebControls.ListItem("-- Pilih Bank --", ""));
            //foreach (DataRow r in conn.GetDataTable().Rows)
            //{
            //    ddl.Items.Add(new System.Web.UI.WebControls.ListItem(r["BANK_NAME"].ToString(), r["BANK_CODE"].ToString()));
            //}
            for (int i = 0; i < conn.GetRowCount(); i++)
                ddl.Items.Add(new System.Web.UI.WebControls.ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }
        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // reuse Maker logic for AddNew
            try
            {
                GridView gvDetail = (GridView)sender;
                GridViewRow detailFooter = gvDetail.FooterRow;

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
                int rangeDari = string.IsNullOrWhiteSpace(txtNewRangeDari.Text) ? 0 : int.Parse(txtNewRangeDari.Text);
                int rangeSampai = string.IsNullOrWhiteSpace(txtNewRangeSampai.Text) ? 0 : int.Parse(txtNewRangeSampai.Text);
                decimal jumlah = string.IsNullOrWhiteSpace(txtNewJumlah.Text) ? 0 : decimal.Parse(txtNewJumlah.Text.Replace(".", "").Replace(",", ""));

                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                string noIom = ViewState["CurrentIOM"] == null ? "" : ViewState["CurrentIOM"].ToString();

                if (e.CommandName == "AddNew")
                {
                    
                    if (detailFooter == null) return;
                    if (string.IsNullOrEmpty(noIom))
                    {
                        ShowMessage("Nomor IOM tidak ditemukan.", true);
                        return;
                    }

                    //conn.QueryString = $"EXEC SP_SAVE_MAKER_ZAKAT_DTL " +
                    //    $"@NO_IOM = '{noIom}', " +
                    //    $"@TRANSFER_KE = '{transferKe}', " +
                    //    $"@NO_REKENING = '{noRek}', " +
                    //    $"@NAMA_REKENING = '{namaRek}', " +
                    //    $"@BANK = '{bank}', " +
                    //    $"@RANGE_DARI = {rangeDari}, " +
                    //    $"@RANGE_SAMPAI = {rangeSampai}, " +
                    //    $"@JUMLAH = {jumlah.ToString(CultureInfo.InvariantCulture)}, " +
                    //    $"@CREATEBY = '{user}'";
                    //conn.ExecuteQuery();
                    conn.QueryString =
                                "EXEC SP_SAVE_MAKER_ZAKAT_DTL " +
                                "@NO_IOM = '" + noIom.Replace("'", "''") + "', " +
                                "@TRANSFER_KE = '" + transferKe.Replace("'", "''") + "', " +
                                "@NO_REKENING = '" + noRek.Replace("'", "''") + "', " +
                                "@NAMA_REKENING = '" + namaRek.Replace("'", "''") + "', " +
                                "@BANK = '" + bank.Replace("'", "''") + "', " +
                                "@RANGE_DARI = " + rangeDari + ", " +
                                "@RANGE_SAMPAI = " + rangeSampai + ", " +
                                "@JUMLAH = " + jumlah.ToString(CultureInfo.InvariantCulture) + ", " +
                                "@CREATEBY = '" + user.Replace("'", "''") + "'";

                    conn.ExecuteQuery();


                    conn.ExecuteQuery();


                    BindDetailGrid(noIom, gvDetail);
                    ShowMessage("Detail IOM {noIom} berhasil ditambahkan.");
                }
                else if (e.CommandName == "Inquiry")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

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
                        if (conn.GetFieldValue("errorCode").ToString() != "00")
                        {
                            string destName = conn.GetFieldValue("toAccName").ToString();
                            string destAccNo = noRek;
                            string destBank = ddlNewBank.SelectedItem.Text;

                            string script = @"
                                                Swal.fire({{
                                                    title: 'Inquiry Success',
                                                    icon: 'success',
                                                    html: `
                                                        <table style='width:100%; text-align:left;'>
                                                            <tr>
                                                                <td><b>Destination Name</b></td>
                                                                <td>:</td>
                                                                <td>{destName}</td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Destination Account No</b></td>
                                                                <td>:</td>
                                                                <td>{destAccNo}</td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Destination Bank</b></td>
                                                                <td>:</td>
                                                                <td>{destBank}</td>
                                                            </tr>
                                                        </table>
                                                    `,
                                                    confirmButtonText: 'OK'
                                                }});
                                            ";

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
                                "Swal.fire('Error', '" + errorDesc + "', 'error');",
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
                        ShowMessage("Tidak ada data untuk diexport.", true);
                        return;
                    }

                    DataTable hdr = ds.Tables[0];
                    DataTable dt = ds.Tables[1];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        // create docx in memory
                        using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document, true))
                        {
                            var mainPart = wordDoc.AddMainDocumentPart();
                            mainPart.Document = new W.Document(new W.Body());
                            W.Body body = mainPart.Document.Body;

                            // Judul (center, bold, size)
                            var titlePara = new W.Paragraph(
                                new W.ParagraphProperties(
                                    new W.Justification() { Val = W.JustificationValues.Center },
                                    new W.SpacingBetweenLines() { After = "200" }
                                ),
                                new W.Run(
                                    new W.RunProperties(
                                        new W.Bold(),
                                        new W.FontSize() { Val = "28" } // 28 -> 14pt (OpenXML size is in half-points)
                                    ),
                                    new W.Text("IOM PERSETUJUAN TRANSAKSI ZAKAT")
                                )
                            );
                            body.Append(titlePara);

                            // spasi
                            body.Append(new W.Paragraph(new W.Run(new W.Text(" "))));

                            if (hdr.Rows.Count > 0)
                            {
                                var r = hdr.Rows[0];
                                //        string[] headerLines =
                                //        {
                                //    $"No IOM: {r["NO_IOM"]}",
                                //    $"Jumlah Peserta: {r["JUMLAH_PESERTA"]}",
                                //    $"Jenis Transaksi: {r["JENIS_TRANSAKSI"]}",
                                //    $"Tanggal NAV: {Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy")}",
                                //    $"Periode Zakat: {r["PERIODE_ZAKAT"]}",
                                //    "",
                                //    $"Jumlah Zakat: {Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N0")}",
                                //    $"Jumlah Biaya: {Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N0")}",
                                //    $"Total Bayar: {Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N0")}"
                                //};
                                string[] headerLines =
                                    {
                                        string.Format("No IOM: {0}", r["NO_IOM"]),
                                        string.Format("Jumlah Peserta: {0}", r["JUMLAH_PESERTA"]),
                                        string.Format("Jenis Transaksi: {0}", r["JENIS_TRANSAKSI"]),
                                        string.Format("Tanggal NAV: {0}",
                                            Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy")),
                                        string.Format("Periode Zakat: {0}", r["PERIODE_ZAKAT"]),
                                        "",
                                        string.Format("Jumlah Zakat: {0}",
                                            Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N2")),
                                        string.Format("Jumlah Biaya: {0}",
                                            Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N2")),
                                        string.Format("Total Bayar: {0}",
                                            Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N2"))
                                    };

                                foreach (var line in headerLines)
                                {
                                    var p = new W.Paragraph(new W.Run(new W.Text(line)));
                                    p.ParagraphProperties = new W.ParagraphProperties(new W.SpacingBetweenLines() { After = "100" });
                                    body.Append(p);
                                }
                            }

                            // pemisah
                            body.Append(new W.Paragraph(new W.Run(new W.Text(new string('-', 80)))));

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow d in dt.Rows)
                                {
                                    //        string[] detailLines =
                                    //        {
                                    //    $"TRANSFER KE: {d["TRANSFER_KE"]}",
                                    //    $"Nomor Rekening: {d["NO_REKENING"]}",
                                    //    $"Nama Rekening: {d["NAMA_REKENING"]}",
                                    //    $"Bank: {d["BANK"]}",
                                    //    $"Jumlah: {Convert.ToDecimal(d["JUMLAH"]).ToString("N0")}",
                                    //    $"Range: {d["RANGE_DARI"]} s/d {d["RANGE_SAMPAI"]}",
                                    //    ""
                                    //};
                                    string[] detailLines =
                                        {
                                        "TRANSFER KE: " + d["TRANSFER_KE"],
                                        "Nomor Rekening: " + d["NO_REKENING"],
                                        "Nama Rekening: " + d["NAMA_REKENING"],
                                        "Bank: " + d["BANK"],
                                        "Jumlah: " + Convert.ToDecimal(d["JUMLAH"]).ToString("N2"),
                                        "Range: " + d["RANGE_DARI"] + " s/d " + d["RANGE_SAMPAI"],
                                        ""
                                    };

                                    foreach (var line in detailLines)
                                    {
                                        var p = new W.Paragraph(new W.Run(new W.Text(line)));
                                        p.ParagraphProperties = new W.ParagraphProperties(new W.SpacingBetweenLines() { After = "80" });
                                        body.Append(p);
                                    }
                                }
                            }

                            mainPart.Document.Save();
                        }

                        byte[] bytes = ms.ToArray();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        //Response.AddHeader("content-disposition", "attachment;filename=" + $"IOM_{noIom.Replace("/", "_")}.docx");
                        Response.AddHeader("content-disposition","attachment;filename=IOM_" + noIom.Replace("/", "_") + ".docx");
                        Response.BinaryWrite(bytes);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error export Word: " + ex.Message, true);
            }
        }

        private string LoadHtmlTemplate(string path)
        {
            return File.ReadAllText(Server.MapPath(path));
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
                        // GET USER (MAKER & CHECKER)
                        // =========================
                        string makerCode = r["CREATEBY"]?.ToString();
                        string checkerCode = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

                        DataRow makerUser = GetUserLogin(makerCode);
                        DataRow checkerUser = GetUserLogin(checkerCode);

                        string makerName = makerUser?["FULLNAME"]?.ToString() ?? "";
                        string checkerName = checkerUser?["FULLNAME"]?.ToString() ?? "";

                        // =========================
                        // LOAD TEMPLATE
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

                        html = html.Replace("{{TOTAL_ZAKAT}}", summary.totalZakat.ToString("N2"));
                        html = html.Replace("{{TOTAL_BIAYA}}", summary.totalBiaya.ToString("N2"));
                        html = html.Replace("{{TOTAL_BAYAR}}", summary.totalBayar.ToString("N2"));

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
                        html = html.Replace("{{PREPARED_BY_NAME}}", makerName);
                        html = html.Replace("{{APPROVED_BY_NAME}}", checkerName);

                        // =========================
                        // DETAIL
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
                                                    <td class='right'>{Convert.ToDecimal(d["JUMLAH"]).ToString("N2")}</td>
                                                </tr>");
                        }

                        html = html.Replace("{{DETAIL_ROWS}}", detailRows.ToString());

                        // =========================
                        // SIGNATURE (DUA-DUANYA HTML)
                        // =========================
                        string signPreparedHtml = "";
                        string signApprovedHtml = "";

                        // === MAKER SIGN ===
                        if (makerUser != null && makerUser["SIGNATURE"] != DBNull.Value)
                        {
                            byte[] signBytes = (byte[])makerUser["SIGNATURE"];

                            string tempFile = Path.Combine(Path.GetTempPath(),
                                Guid.NewGuid().ToString() + ".png");

                            File.WriteAllBytes(tempFile, signBytes);

                            signPreparedHtml = $"<img src='file:///{tempFile.Replace("\\", "/")}' style='height:60px;' />";
                        }

                        // === CHECKER SIGN ===
                        if (checkerUser != null && checkerUser["SIGNATURE"] != DBNull.Value)
                        {
                            byte[] signBytes = (byte[])checkerUser["SIGNATURE"];

                            string tempFile = Path.Combine(Path.GetTempPath(),
                                Guid.NewGuid().ToString() + ".png");

                            File.WriteAllBytes(tempFile, signBytes);

                            signApprovedHtml = $"<img src='file:///{tempFile.Replace("\\", "/")}' style='height:60px;' />";
                        }

                        html = html.Replace("{{SIGN_PREPARED}}", signPreparedHtml);
                        html = html.Replace("{{SIGN_APPROVED}}", signApprovedHtml);

                        // =========================
                        // HTML → PDF
                        // =========================
                        using (MemoryStream ms = new MemoryStream())
                        {
                            Document document = new Document(PageSize.A4, 36, 36, 36, 36);
                            PdfWriter writer = PdfWriter.GetInstance(document, ms);

                            document.Open();

                            using (StringReader sr = new StringReader(html))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                            }

                            document.Close();

                            byte[] finalBytes = ms.ToArray();

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
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GET_MAKER_ZAKAT", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NO_IOM", noIom);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables.Count < 2) { ShowMessage("Tidak ada data untuk diexport.", true); return; }

                        DataTable hdr = ds.Tables[0];
                        DataTable dt = ds.Tables[1];
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            ExcelWorksheet ws = package.Workbook.Worksheets.Add("IOM Zakat");
                            int row = 1;
                            ws.Cells[row, 1].Value = "IOM PERSETUJUAN TRANSAKSI ZAKAT";
                            ws.Cells[row, 1, row, 4].Merge = true;
                            ws.Cells[row, 1].Style.Font.Bold = true;
                            ws.Cells[row, 1].Style.Font.Size = 14;
                            row += 2;

                            //if (hdr.Rows.Count > 0)
                            //{
                            //    var r = hdr.Rows[0];
                            //    ws.Cells[row++, 1].Value = $"No IOM\t\t\t\t: {r["NO_IOM"]}";
                            //    ws.Cells[row++, 1].Value = $"Jumlah Peserta\t\t: {r["JUMLAH_PESERTA"]}";
                            //    ws.Cells[row++, 1].Value = $"Jenis Transaksi\t\t: {r["JENIS_TRANSAKSI"]}";
                            //    ws.Cells[row++, 1].Value = $"Tanggal NAV\t\t\t: {Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy")}";
                            //    ws.Cells[row++, 1].Value = $"Periode Zakat\t\t: {r["PERIODE_ZAKAT"]}";
                            //    row++;
                            //    ws.Cells[row++, 1].Value = $"Jumlah Zakat\t\t\t\t\t: {Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N0")}";
                            //    ws.Cells[row++, 1].Value = $"Jumlah Biaya\t\t\t\t\t: {Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N0")}";
                            //    ws.Cells[row++, 1].Value = $"Total Yang Harus Dibayarkan\t: {Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N0")}";
                            //    row++;
                            //}

                            if (hdr.Rows.Count > 0)
                            {
                                var r = hdr.Rows[0];

                                ws.Cells[row++, 1].Value = "No IOM\t\t\t\t: " + r["NO_IOM"];
                                ws.Cells[row++, 1].Value = "Jumlah Peserta\t\t: " + r["JUMLAH_PESERTA"];
                                ws.Cells[row++, 1].Value = "Jenis Transaksi\t\t: " + r["JENIS_TRANSAKSI"];
                                ws.Cells[row++, 1].Value = "Tanggal NAV\t\t\t: "
                                    + Convert.ToDateTime(r["TANGGAL_NAV"]).ToString("dd/MM/yyyy");
                                ws.Cells[row++, 1].Value = "Periode Zakat\t\t: " + r["PERIODE_ZAKAT"];

                                row++;

                                ws.Cells[row++, 1].Value = "Jumlah Zakat\t\t\t\t\t: "
                                    + Convert.ToDecimal(r["TOTAL_ZAKAT"]).ToString("N2");
                                ws.Cells[row++, 1].Value = "Jumlah Biaya\t\t\t\t\t: "
                                    + Convert.ToDecimal(r["TOTAL_BIAYA"]).ToString("N2");
                                ws.Cells[row++, 1].Value = "Total Yang Harus Dibayarkan\t: "
                                    + Convert.ToDecimal(r["TOTAL_BAYAR"]).ToString("N2");

                                row++;
                            }

                            ws.Cells[row++, 1].Value = "----------------------------------------------------------------------------------------------";

                            //foreach (DataRow r in dt.Rows)
                            //{
                            //    ws.Cells[row++, 1].Value = $"TRANSFER KE {r["TRANSFER_KE"]}";
                            //    ws.Cells[row++, 1].Value = $"Nomor Rekening\t\t: {r["NO_REKENING"]}";
                            //    ws.Cells[row++, 1].Value = $"Nama Rekening\t\t: {r["NAMA_REKENING"]}";
                            //    ws.Cells[row++, 1].Value = $"Bank\t\t\t: {r["BANK"]}";
                            //    ws.Cells[row++, 1].Value = $"Jumlah\t\t\t: {Convert.ToDecimal(r["JUMLAH"]).ToString("N0")}";
                            //    ws.Cells[row++, 1].Value = $"Range\t\t\t: {r["RANGE_DARI"]} s/d {r["RANGE_SAMPAI"]}";
                            //    row++;
                            //}
                            foreach (DataRow r in dt.Rows)
                            {
                                ws.Cells[row++, 1].Value = "TRANSFER KE " + r["TRANSFER_KE"];
                                ws.Cells[row++, 1].Value = "Nomor Rekening\t\t: " + r["NO_REKENING"];
                                ws.Cells[row++, 1].Value = "Nama Rekening\t\t: " + r["NAMA_REKENING"];
                                ws.Cells[row++, 1].Value = "Bank\t\t\t: " + r["BANK"];
                                ws.Cells[row++, 1].Value = "Jumlah\t\t\t: "
                                    + Convert.ToDecimal(r["JUMLAH"]).ToString("N2");
                                ws.Cells[row++, 1].Value = "Range\t\t\t: "
                                    + r["RANGE_DARI"] + " s/d " + r["RANGE_SAMPAI"];

                                row++;
                            }

                            ws.Cells[1, 1, row, 4].AutoFitColumns();

                            Response.Clear();
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            //Response.AddHeader("content-disposition", "attachment;filename=" + $"IOM_{noIom.Replace("/", "_")}.xlsx");
                            Response.AddHeader("content-disposition","attachment;filename=IOM_" + noIom.Replace("/", "_") + ".xlsx");

                            Response.BinaryWrite(package.GetAsByteArray());
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error export Excel: " + ex.Message, true);
            }
        }
        
        private string FormatLine(string label, string value)
        {
            return label.PadRight(35, ' ') + ": " + value;
        }
        
        private void ShowMessage(string message, bool isError = false)
        {
            string icon = isError ? "error" : "success";
            string title = isError ? "Error" : "Berhasil";
            //string script = $@"Swal.fire({{title: '{title}', text: '{message.Replace("'", "\\'")}', icon: '{icon}', confirmButtonText: 'OK'}});";
            string script = string.Format("Swal.fire({{title: '{0}', text: '{1}', icon: '{2}', confirmButtonText: 'OK'}});",title,message.Replace("'", "\\'"),icon);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "swal", script, true);
        }
        
        private void SaveZakatHeader(
            string noIom,
            int peserta,
            DateTime tanggalNav,
            int periode,
            decimal totalZakat,
            decimal totalBiaya,
            decimal totalBayar,
            string status,
            string note,
            string user)
        {
            try
            {
                string sqlNoIom = string.IsNullOrEmpty(noIom)
                    ? "NULL"
                    : "'" + noIom.Replace("'", "''") + "'";

                                string sqlNote = string.IsNullOrEmpty(note)
                                    ? "NULL"
                                    : "N'" + note.Replace("'", "''") + "'";

                            conn.QueryString =
                                "DECLARE @OUT_NO_IOM NVARCHAR(30); " +
                                "EXEC SP_SAVE_MAKER_ZAKAT_HDR " +
                                "@NO_IOM = " + sqlNoIom + ", " +
                                "@JUMLAH_PESERTA = " + peserta + ", " +
                                "@JENIS_TRANSAKSI = 'ZAKAT', " +
                                "@TANGGAL_NAV = '" + tanggalNav.ToString("yyyy-MM-dd") + "', " +
                                "@PERIODE_ZAKAT = " + periode + ", " +
                                "@TOTAL_ZAKAT = " + totalZakat.ToString(CultureInfo.InvariantCulture) + ", " +
                                "@TOTAL_BIAYA = " + totalBiaya.ToString(CultureInfo.InvariantCulture) + ", " +
                                "@TOTAL_BAYAR = " + totalBayar.ToString(CultureInfo.InvariantCulture) + ", " +
                                "@STATUS = '" + status.Replace("'", "''") + "', " +
                                "@CREATEBY = '" + user.Replace("'", "''") + "', " +
                                "@NOTE = " + sqlNote + ", " +
                                "@OUT_NO_IOM = @OUT_NO_IOM OUTPUT; " +
                                "SELECT @OUT_NO_IOM AS NO_IOM;";


                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable();

                if (dt.Rows.Count > 0)
                {
                    string outIom = dt.Rows[0]["NO_IOM"].ToString();
                    string statusText = string.IsNullOrEmpty(noIom)
                                        ? "berhasil disimpan"
                                        : "berhasil diperbarui";
                    string script = string.Format(
                                            "Swal.fire({{ icon: 'success', title: 'Berhasil!', text: 'Data {0} dengan No. IOM: {1}', confirmButtonText: 'OK' }});",
                                            statusText,
                                            outIom
                                        );

                    ScriptManager.RegisterStartupScript(this, GetType(), "SaveSuccess", script, true);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.Replace("'", " ");

                string script = string.Format(
                    "Swal.fire({{ icon: 'error', title: 'Gagal!', text: 'Terjadi kesalahan: {0}', confirmButtonText: 'OK' }});",
                    errorMessage
                );

                ScriptManager.RegisterStartupScript(this, GetType(), "SaveError", script, true);
            }
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

                DateTime tanggalNav = DateTime.ParseExact(txtTanggalNAV.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int periode = int.Parse(ddlPeriode.SelectedValue);
                int peserta = int.Parse(txtJumlahPeserta.Text);
                decimal totalZakat = SafeParseDecimalNoDecimal(txtTotalZakat.Text);
                decimal totalBiaya = SafeParseDecimalNoDecimal(txtTotalBiaya.Text);
                decimal totalBayar = SafeParseDecimalNoDecimal(txtTotalBayar.Text);

                string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                string noIom = ViewState["EditIOM"] == null ? "" : ViewState["EditIOM"].ToString();

                if (!string.IsNullOrEmpty(noIom))
                {
                    SaveZakatHeader(noIom, peserta, tanggalNav, periode, totalZakat, totalBiaya, totalBayar, string.Empty, string.Empty, user);
                }
                else
                {
                    ShowMessage("Tidak ada header yang dipilih untuk diedit.", true);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error update header: " + ex.Message, true);
            }
        }
        
        private void UpdateZakatStatus(string noIom, string status, string note)
        {
            try
            {
                string userId = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")
                    .Replace("'", "''");

                string safeNote = note.Replace("'", "''");
                string safeNoIom = noIom.Replace("'", "''");
                string safeStatus = status.Replace("'", "''");

                if (status == "BACK_TO_MAKER")
                    safeStatus = "OPEN";

                conn.QueryString =
                            "UPDATE MAKER_ZAKAT_HDR " +
                            "SET STATUS = '" + safeStatus + "', " +
                            "    UPDATEBY = '" + userId + "', " +
                            "    UPDATEDATE = GETDATE(), " +
                            "    NOTE = '" + safeNote + "' " +
                            "WHERE NO_IOM = '" + safeNoIom + "'";

                conn.ExecuteQuery();
                DataTable dt = conn.GetDataTable();
                BindHeaderGrid();

                string msg = status == "APPROVED" ? "Data berhasil disetujui!"
                       : status == "REJECTED" ? "Data telah ditolak."
                       : status == "BACK_TO_MAKER" ? "Data dikembalikan ke maker."
                       : "Status diperbarui.";

                ScriptManager.RegisterStartupScript(this, GetType(), "StatusUpdated",
                    $"Swal.fire({{icon:'success',title:'Berhasil!',text:'{msg}'}});", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "StatusError",
                    $"Swal.fire({{icon:'error',title:'Gagal!',text:'{ex.Message}'}});", true);
                }
        }
        
        private decimal SafeParseDecimalNoDecimal(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0m;
            var cleaned = s.Replace(".", "").Replace(",", ".");
            decimal v;
            if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out v)) return v;
            return 0m;
        }

        private void ApproveZakat(string noIom)
        {
            try
            {
                string userId = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID").Replace("'", "''");
                string safeNoIom = noIom.Replace("'", "''");
                string appId = "LF";
                string settlementType = "CHR";
                string makerUser = GetMakerUser(noIom);

                if (string.Equals(userId, makerUser, StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "SoDValidation",
                        "Swal.fire({icon:'warning'," +
                        "title:'Warning'," +
                        "text:'Maker tidak boleh melakukan approve data sendiri.'});",
                        true);
                    return;
                }

                conn.QueryString =
                    "BEGIN TRY " +
                    "BEGIN TRANSACTION; " +

                    // 🔥 CALL SP FINANCE
                    "EXEC FINANCE.DBO.SP_INSERT_SETTLEMENT_FOR_CHECKER_ZAKAT " +
                    "@APP_ID = '" + appId + "', " +
                    "@TIPE_SETTLEMENT = '" + settlementType + "', " +
                    "@NO_IOM = '" + safeNoIom + "', " +
                    "@USERBY = '" + userId.Replace("'", "''") + "'; " +

                    // 🔥 UPDATE STATUS
                    "UPDATE MAKER_ZAKAT_HDR " +
                    "SET STATUS = 'APPROVED', " +
                    "    UPDATEBY = '" + userId.Replace("'", "''") + "', " +
                    "    UPDATEDATE = GETDATE(), " +
                    "    NOTE = 'Data disetujui oleh checker.' " +
                    "WHERE NO_IOM = '" + safeNoIom + "'; " +

                    "COMMIT TRANSACTION; " +
                    "END TRY " +
                    "BEGIN CATCH " +
                    "ROLLBACK TRANSACTION; " +
                    "THROW; " +
                    "END CATCH;";

                conn.ExecuteQuery();

                BindHeaderGrid();

                ScriptManager.RegisterStartupScript(this, GetType(), "ApproveSuccess",
                    "Swal.fire({icon:'success',title:'Berhasil!',text:'Data berhasil di-approve & dikirim ke finance.'});",
                    true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ApproveError",
                    $"Swal.fire({{icon:'error',title:'Gagal!',text:'{ex.Message}'}});",
                    true);
            }
        }

        private DataRow GetUserLogin(string userCode)
        {
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

        private string GetMakerUser(string noIom)
        {
            conn.QueryString =
                "SELECT CREATEBY FROM MAKER_ZAKAT_HDR " +
                "WHERE NO_IOM = '" + noIom.Replace("'", "''") + "'";

            conn.ExecuteQuery();
            DataTable dt = conn.GetDataTable();

            if (dt.Rows.Count > 0)
                return dt.Rows[0]["CREATEBY"].ToString();

            return "";
        }


    }
}
