using DMS.DBConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace REAS.Form_App
{
    public partial class Memo : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string StartDateUpdate;
        protected string EndDateUpdate;
        protected string SelectedStatus;
        protected string ReasName;
        protected string joinedValues;
        protected string selectedReasName;

        private int pageSize = 200;
        private int totalRows = 0;
        private decimal totalContribution = 0;
        private decimal totalTabarru = 0;
        private decimal totalUjroh = 0;
        private decimal totalClaimRefund = 0;
        private decimal totalNetOff = 0;

        int lastNumber;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ViewState.Clear();
                divPanel.Style["display"] = "none";
                LBL_TITLE.Text = "SETTLEMENT DETAIL DATA";

                DateTime currentDate = DateTime.Now;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
                TXT_STARTDATE.Text = startDate.ToString("dd/MM/yyyy");
                TXT_ENDDATE.Text = currentDate.ToString("dd/MM/yyyy");

                StartDate = startDate.ToString("yyyy-MM-dd");
                EndDate = currentDate.ToString("yyyy-MM-dd");



                BindReasDropdown();

                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                DGR.CurrentPageIndex = 0;

                
            }
        }

        private void FillDGR(int pageNumbers)
        {
            LB_RESULT.Text = "";
            SelectedStatus = joinedValues;

            string[] valuesType = joinedValues.Split(',');
            // Cek apakah ada "all" di array
            bool hasAll = valuesType.Any(v => v.Trim().Equals("all", StringComparison.OrdinalIgnoreCase));

            string listForSQL;

            if (hasAll)
            {
                // Jika ada "all", listForSQL hanya berisi 'all'
                listForSQL = "'all'";
            }
            else
            {
                // Jika tidak ada "all", masukkan semua value
                List<string> valuesForSQL = valuesType.Select(v => v.Trim()).ToList();
                listForSQL = joinedValues;
            }

            string connectString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_GET_SETTLEMENT_DETAIL]", connect))
            {
                if (ReasName == null)
                {
                    ReasName = "";
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@START_DATE", StartDate);
                cmd.Parameters.AddWithValue("@END_DATE", EndDate);
                cmd.Parameters.AddWithValue("@REAS_NAME", selectedReasName);
                cmd.Parameters.AddWithValue("@TYPE", SelectedStatus);

                connect.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(ds);

                    dt = ds.Tables[0];

                    totalRows = Convert.ToInt32(dt.Rows.Count);

                    DGR.DataSource = dt;
                    DGR.DataBind();


                    ViewState["PageNumber"] = pageNumbers;
                    ViewState["TotalRows"] = totalRows;

                    LB_RESULT.Text = "Total Data : " + totalRows.ToString() + " Records";

                    totalContribution = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMOUNT_SETTLEMENT_CONTRIBUTION"));
                    totalClaimRefund = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMOUNT_SETTLEMENT_CLAIM_REFUND"));
                    totalNetOff = dt.AsEnumerable().Sum(row => row.Field<decimal>("TOTAL_NETOFF"));

                    totalTabarru = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMOUNT_TABBARU"));
                    totalUjroh = dt.AsEnumerable().Sum(row => row.Field<decimal>("AMOUNT_UJROH"));

                    ViewState["TotalAmountContribution"] = totalContribution;
                    ViewState["TotalAmountClaimRefund"] = totalClaimRefund;
                    ViewState["TotalAmountNetOff"] = totalNetOff;
                    ViewState["TotalAmountTabarru"] = totalTabarru;
                    ViewState["TotalAmountUjroh"] = totalUjroh;


                    LBL_STR_TOTAL_CTRB.Text = "Rp. " + totalContribution.ToString("N2");
                    LBL_STR_TOTAL_CLMRF.Text = "Rp. " + totalClaimRefund.ToString("N2");
                    LBL_STR_TOTAL_NETOFF.Text = "Rp. " + totalNetOff.ToString("N2");
                    LBL_STR_TOTAL_TBR.Text = "Rp. " + totalTabarru.ToString("N2");
                    LBL_STR_TOTAL_UJR.Text = "Rp. " + totalUjroh.ToString("N2");

                    int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    BindPaging(totalPages);
                }
                catch (Exception ex)
                {
                    throw;
                }

                connect.Close();
            }
        }

        private void BindPaging(int totalPages)
        {
            int currentPage = Convert.ToInt32(ViewState["PageNumber"]);

            DataTable dtPaging = new DataTable();
            dtPaging.Columns.Add("PageNumber", typeof(int));

            for (int i = 1; i <= totalPages; i++)
                dtPaging.Rows.Add(i);

            rptPaging.DataSource = dtPaging;
            rptPaging.DataBind();

            // Highlight halaman aktif
            foreach (RepeaterItem item in rptPaging.Items)
            {
                LinkButton lnk = (LinkButton)item.FindControl("lnkPage");
                if (lnk.Text == currentPage.ToString())
                {
                    lnk.Enabled = false;
                    lnk.ForeColor = System.Drawing.Color.White;
                    lnk.BackColor = System.Drawing.Color.DarkBlue;
                }
            }

            lnkPrev.Enabled = currentPage > 1;
            lnkNext.Enabled = currentPage < totalPages; ;
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int page = Convert.ToInt32(e.CommandArgument);
                StartDate = ViewState["StartDate"] as string;
                EndDate = ViewState["EndDate"] as string;
                ReasName = ViewState["ReasName"] as string;
                SelectedStatus = ViewState["Status"] as string;
                FillDGR(page);
            }
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(ViewState["PageNumber"]);
            if (currentPage > 1)
                FillDGR(currentPage - 1);
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(ViewState["PageNumber"]);
            int totalRows = Convert.ToInt32(ViewState["TotalRows"]);
            int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);

            if (currentPage < totalPages)
                FillDGR(currentPage + 1);
        }

        private void BindReasDropdown()
        {
            DDL_REAS.Items.Clear();
            conn.QueryString = "SELECT COMPANY_NAME, UPPER(COMPANY_NAME) COMPANY_NAME FROM REINSURANCE.dbo.V_COMPANY";
            conn.ExecuteQuery();

            // (Opsional) item ALL
            DDL_REAS.Items.Add(new ListItem("ALL REAS", "all"));

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_REAS.Items.Add(
                    new ListItem(
                        conn.GetFieldValue(i, 1).ToString(), // TEXT (UPPER)
                        conn.GetFieldValue(i, 0).ToString()  // VALUE (ORIGINAL)
                    )
                );
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) || !string.IsNullOrEmpty(TXT_ENDDATE.Text))
            {
                if (string.IsNullOrEmpty(TXT_STARTDATE.Text) || string.IsNullOrEmpty(TXT_ENDDATE.Text))
                {
                    Response.Write("<script>alert('Pastikan start date dan end date terisi.');window.location.href = window.location.href;</script>");
                    return;
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
                        return;

                    }
                }
            }
            if (string.IsNullOrEmpty(DDL_TYPE.SelectedValue) || DDL_TYPE.SelectedValue == "")
            {
                Response.Write("<script>alert('Pastikan type reas terisi.');window.location.href = window.location.href;</script>");
                return;

            }

            List<string> selectedValues = DDL_TYPE.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value)
                                          .ToList();
            joinedValues = string.Join(",", selectedValues);

            List<string> listReasName = DDL_REAS.Items
                                          .Cast<ListItem>()
                                          .Where(i => i.Selected)
                                          .Select(i => i.Value)
                                          .ToList();
            selectedReasName = string.Join(",", listReasName);



            ReasName = DDL_REAS.SelectedValue;
            SelectedStatus = joinedValues;

            ViewState["StartDate"] = StartDate;
            ViewState["EndDate"] = EndDate;
            ViewState["ReasName"] = selectedReasName;
            ViewState["Type"] = SelectedStatus;

            FillDGR(1);

            tblStringResult.Visible = true;

        }

        protected void BT_CREATE_Click(object sender, EventArgs e)
        {
            divPanel.Style["display"] = "block";

            DateTime currentDate = DateTime.Now;
            TXT_UPDATEDATE.Text = currentDate.ToString("dd/MM/yyyy");
            GenerateHijriyahDate(currentDate);

            TXT_AMOUNT.Text = Convert.ToDecimal(ViewState["TotalAmountNetOff"]).ToString("N2");
            TXT_ACTUAL_AMOUNT.Text = Convert.ToDecimal(ViewState["TotalAmountNetOff"]).ToString("N2");


            if (TXT_ACTUAL_AMOUNT.Text.Contains("-"))
            {
                DDL_MEMO.SelectedValue = "EM";
            }
            else
            {
                DDL_MEMO.SelectedValue = "IM";

            }

        }

        protected void DDL_REAS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BT_CANCEL_MEMO_Click(object sender, EventArgs e)
        {
            divPanel.Style["display"] = "none";
        }

        protected void BT_CREATE_MEMO_Click(object sender, EventArgs e)
        {
//            ScriptManager.RegisterStartupScript(
//    ToolkitScriptManager1,
//    ToolkitScriptManager1.GetType(),
//    "showModalStatus",
//    $"openModalStatus('Pembuatan Memo Berhasil!');",
//    true
//);


            if (!TXT_FILE_UPLOAD.HasFile)
            {
                string script = "alert('Document tidak boleh kosong.');";
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", script, true);
                return;
            }
            else if (DDL_MEMO.SelectedValue == "")
            {
                string script = "alert('Memo Type tidak boleh kosong.');";
                ClientScript.RegisterStartupScript(this.GetType(), "Alert", script, true);
                return;
            }
            else
            {
                try
                {
                    var cell1 = TXT_AMOUNT.Text;
                    if (cell1 == "")
                    {
                        cell1 = "0";
                    }
                    else
                    {
                        cell1 = TXT_AMOUNT.Text;
                    }

                    var cell2 = TXT_ACTUAL_AMOUNT.Text;
                    if (cell2 == "")
                    {
                        cell2 = "0";
                    }
                    else
                    {
                        cell2 = TXT_ACTUAL_AMOUNT.Text;
                    }

                    decimal amountValue = decimal.Parse(cell1, CultureInfo.InvariantCulture);
                    decimal actualAmountValue = decimal.Parse(cell2, CultureInfo.InvariantCulture);

                    conn.QueryString = @"
                    IF EXISTS (SELECT 1 FROM [dbo].[RTF_GENERATE_DATA_LOG] WHERE [TYPE] = 'CreateMemo') 
                    BEGIN
                        SELECT MAX([LASTNUMBER]) + 1 AS LastNumber FROM [dbo].[RTF_GENERATE_DATA_LOG] WHERE [TYPE] = 'CreateMemo';
                    END
                    ELSE
                    BEGIN
                        SELECT 0 + 1 AS LastNumber;
                    END";

                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        lastNumber = Convert.ToInt32(conn.GetFieldValue(0, "LastNumber"));
                    }

                    DateTime now = DateTime.Now;
                    string formattedMemoNo = DDL_MEMO.SelectedValue + "-ATK-RTK-" + string.Format("{0:D5}/{1:MM}/{1:yyyy}", lastNumber, now);

                    string connString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);

                    using (SqlConnection connect = new SqlConnection(connString))
                    using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_CREATE_MEMO]", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MEMOID", formattedMemoNo);
                        cmd.Parameters.AddWithValue("@HIJRIYAHDATE", TXT_HIJRIYAH.Text ?? "");
                        cmd.Parameters.AddWithValue("@PERIHAL", TXT_PERIHAL.Text ?? "");
                        cmd.Parameters.AddWithValue("@REMARKS", TXT_REMARKS.Text ?? "");
                        cmd.Parameters.AddWithValue("@REAS_NAME", DDL_REAS.SelectedValue ?? "");
                        cmd.Parameters.AddWithValue("@MEMO_TYPE", DDL_MEMO.SelectedValue ?? "");
                        cmd.Parameters.AddWithValue("@UPLOAD_FILE_PATH", "");  // kosong seperti query awal
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amountValue;
                        cmd.Parameters.Add("@ACTUAL_AMOUNT", SqlDbType.Decimal).Value = actualAmountValue;
                        cmd.Parameters.AddWithValue("@AMOUNT_SPELL", TXT_AMOUNT_SPELL.Text ?? "");
                        cmd.Parameters.AddWithValue("@REFRENCE", TXT_REFERENCE.Text ?? "");
                        cmd.Parameters.AddWithValue("@MEMOCREATEDATE", DateTime.Now);
                        cmd.Parameters.AddWithValue("@MEMODATE", TXT_UPDATEDATE.Text);

                        connect.Open();
                        cmd.ExecuteNonQuery();
                    }

                    if (DGR.Items.Count > 0)
                    {
                        foreach (DataGridItem item in DGR.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                // Access specific cells within the row
                                string Type = item.Cells[1].Text;
                                string ID = item.Cells[2].Text;
                                string NoPolis = item.Cells[6].Text;

                                using (SqlConnection connect = new SqlConnection(connString))
                                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_CREATE_MEMO_DETAIL]", connect))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@MEMOID", formattedMemoNo);
                                    cmd.Parameters.AddWithValue("@ID", ID ?? "");
                                    cmd.Parameters.AddWithValue("@TYPE", Type ?? "");
                                    cmd.Parameters.AddWithValue("@POLICY_NO", NoPolis ?? "");

                                    connect.Open();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    if (Request.Files.Count > 0)
                    {
                        int i = 0;
                        foreach (string fileKey in Request.Files)
                        {

                            HttpPostedFile postedFile = Request.Files[i];
                            if (postedFile != null && postedFile.ContentLength > 0)
                            {
                                string nomemoTxt = formattedMemoNo.Replace("/", "-");
                                string fileName = Path.GetFileName(postedFile.FileName);

                                string fileExtension = Path.GetExtension(fileName).ToLower();


                                string uploadPath = @"~/Upload/Memo Dokumen Tambahan/" + nomemoTxt;

                                string filePath = Path.Combine(Server.MapPath(uploadPath), fileName);
                                string directoryPath = Path.GetDirectoryName(filePath);
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                // Save the file to the server
                                postedFile.SaveAs(filePath);

                                using (SqlConnection connect = new SqlConnection(connString))
                                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_CREATE_MEMO_DETAIL_DOC]", connect))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@MEMOID", formattedMemoNo);
                                    cmd.Parameters.AddWithValue("@FILE_PATH", filePath.ToString() ?? "");

                                    connect.Open();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            i++;

                        }
                    }

                    conn.QueryString = @"
                    IF EXISTS (SELECT 1 FROM [dbo].[RTF_GENERATE_DATA_LOG] WHERE [TYPE] = 'CreateMemo' AND [Year] = " + now.Year + " AND [Month] = " + now.Month + ") " +
                        "BEGIN " +
                            "UPDATE [dbo].[RTF_GENERATE_DATA_LOG] " +
                            "SET [LASTNUMBER] = " + lastNumber +
                            "WHERE [Type] = 'CreateMemo' AND [Year] = " + now.Year + " AND [Month] = " + now.Month + "; " +
                        " END ELSE " +
                        "BEGIN " +
                            "INSERT INTO [dbo].[RTF_GENERATE_DATA_LOG] ([TYPE], [YEAR], [MONTH], [LASTNUMBER], [MODIFIED_DATE]) " +
                            "VALUES ('CreateMemo', " + now.Year + ", " + now.Month + ", " + lastNumber + ", GETDATE()); " +
                        "END";


                    conn.ExecuteQuery();


                    Response.Write("<script>alert('Data berhasil di simpan.');window.location.href = window.location.href;</script>");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalStatus", "openModalStatus('Pembuatan Memo Berhasil!');", true);
                    return;



                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void TXT_UPDATEDATE_TextChanged(object sender, EventArgs e)
        {
            DateTime memoDate;

            if (DateTime.TryParseExact(
                TXT_UPDATEDATE.Text,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out memoDate))
            {
                GenerateHijriyahDate(memoDate);
            }
            else
            {
                TXT_HIJRIYAH.Text = string.Empty;
            }
        }

        private void GenerateHijriyahDate(DateTime memoDate)
        {
            DateTime normalizedDate = memoDate.Date;

            string tanggal = normalizedDate.ToString("yyyy-MM-dd");

            conn.QueryString = @"SELECT dbo.UFN_TanggalHijriahIndonesia('" + tanggal + "') AS TanggalHijriah";

            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                TXT_HIJRIYAH.Text = conn.GetFieldValue("TanggalHijriah");
            }
        }

        protected void TXT_ACTUAL_AMOUNT_TextChanged(object sender, EventArgs e)
        {
            decimal amount;

            if (decimal.TryParse(TXT_ACTUAL_AMOUNT.Text.Replace(",", "."), out amount))
            {
                TXT_AMOUNT_SPELL.Text = TerbilangRupiah(amount);
            }
            else
            {
                TXT_AMOUNT_SPELL.Text = string.Empty;
            }
        }

        private string TerbilangRupiah(decimal angka)
        {
            long rupiah = (long)Math.Floor(angka);
            int sen = (int)Math.Round((angka - rupiah) * 100);

            string hasil = Terbilang(rupiah) + " RUPIAH";

            if (sen > 0)
                hasil += " " + Terbilang(sen) + " SEN";

            return hasil.Trim();
        }

        private string Terbilang(long angka)
        {
            string[] satuan = {
                "", "SATU", "DUA", "TIGA", "EMPAT", "LIMA",
                "ENAM", "TUJUH", "DELAPAN", "SEMBILAN",
                "SEPULUH", "SEBELAS"
            };

            if (angka < 12)
                return satuan[angka];

            if (angka < 20)
                return (Terbilang(angka - 10) + " BELAS").Trim();

            if (angka < 100)
                return (Terbilang(angka / 10) + " PULUH " + Terbilang(angka % 10)).Trim();

            if (angka < 200)
                return ("SERATUS " + Terbilang(angka - 100)).Trim();

            if (angka < 1000)
                return (Terbilang(angka / 100) + " RATUS " + Terbilang(angka % 100)).Trim();

            if (angka < 2000)
                return ("SERIBU " + Terbilang(angka - 1000)).Trim();

            if (angka < 1000000)
                return (Terbilang(angka / 1000) + " RIBU " + Terbilang(angka % 1000)).Trim();

            if (angka < 1000000000)
                return (Terbilang(angka / 1000000) + " JUTA " + Terbilang(angka % 1000000)).Trim();

            if (angka < 1000000000000)
                return (Terbilang(angka / 1000000000) + " MILIAR " + Terbilang(angka % 1000000000)).Trim();

            return (Terbilang(angka / 1000000000000) + " TRILIUN " + Terbilang(angka % 1000000000000)).Trim();
        }
    }

}