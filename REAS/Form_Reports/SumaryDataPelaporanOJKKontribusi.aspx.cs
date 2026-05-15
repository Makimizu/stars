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
using OfficeOpenXml;
using System.Data.SqlClient;

namespace REAS.Form_Reports
{
    public partial class SumaryDataPelaporanOJKKontribusi : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string StartDateUpdate;
        protected string EndDateUpdate;
        protected string SelectedStatus;
        protected string ReasName;

        private int pageSize = 200;
        private int totalRows = 0;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //DGR.AllowPaging = true;

            if (!IsPostBack)
            {

                DateTime currentDate = DateTime.Now;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
                TXT_STARTDATE.Text = startDate.ToString("dd/MM/yyyy");
                TXT_ENDDATE.Text = currentDate.ToString("dd/MM/yyyy");

                StartDate = startDate.ToString("yyyy-MM-dd");
                EndDate = currentDate.ToString("yyyy-MM-dd");
                StartDateUpdate = startDate.ToString("yyyy-MM-dd");
                EndDateUpdate = currentDate.ToString("yyyy-MM-dd");

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

                FillDGR(1);


            }
        }

        private void BindReasDropdown()
        {
            
        }

        protected void FillDGR(int pageNumbers)
        {
            LB_RESULT.Text = "";


            string connectString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_GET_CONTRIBUTION_PELAPORAN_OJK_DATA]", connect))
            {
                if (ReasName == null)
                {
                    ReasName = "";
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PAGE_NUMBER", pageNumbers);
                cmd.Parameters.AddWithValue("@PAGE_SIZE", pageSize);
                cmd.Parameters.AddWithValue("@START_DATE", StartDate);
                cmd.Parameters.AddWithValue("@END_DATE", EndDate);

                connect.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(ds);

                    totalRows = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    dt = ds.Tables[1];
                    DGR.DataSource = dt;
                    DGR.DataBind();


                    ViewState["PageNumber"] = pageNumbers;
                    ViewState["TotalRows"] = totalRows;

                    LB_RESULT.Text = "Total Data : " + totalRows.ToString() + " Records";

                    int totalPages = (int)Math.Ceiling((double)totalRows / pageSize);
                    BindPaging(totalPages);
                }
                catch (Exception ex)
                {
                    throw;
                }

                connect.Close();


                if (DGR.Items.Count <= 0)
                {
                    BT_DOWNLOAD.Enabled = false;
                }
                else
                {
                    BT_DOWNLOAD.Enabled = true;
                }
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
            lnkNext.Enabled = currentPage < totalPages;
        }


        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int page = Convert.ToInt32(e.CommandArgument);
                StartDate = ViewState["StartDate"] as string;
                EndDate = ViewState["EndDate"] as string;
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

            ViewState["StartDate"] = StartDate;
            ViewState["EndDate"] = EndDate;

            FillDGR(1);
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            LB_DOWNLOAD.Text = "Please wait, Downloading File......";

            if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) && !string.IsNullOrEmpty(TXT_ENDDATE.Text))
            {
                DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                StartDate = startDateTxt.ToString("yyyy-MM-dd");

                DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                EndDate = endDateTxt.ToString("yyyy-MM-dd");
            }

            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "SummaryDataPelaporanOJK_Contribution_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + SelectedStatus + ".xlsx";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            // Set the EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Retrieve data
            DataTable dt = GetDataDownload(StartDate, EndDate, ReasName, SelectedStatus);

            // Create Excel package
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Add a new worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("LIST");

                // Add headers and information
                ws.Cells["A1"].Value = "Report";
                ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": Summary Data Pelaporan OJK Contribution";
                ws.Cells["B2"].Value = ": " + StartDate.ToString() + " - " + EndDate.ToString();

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
            HttpContext.Current.ApplicationInstance.CompleteRequest();


        }

        private DataTable GetDataDownload(string startDate, string endDate, string reasName, string selectedStatus)
        {
            string connectString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_GET_CONTRIBUTION_PELAPORAN_OJK_DATA_ALL]", connect))
            {
                if (ReasName == null)
                {
                    ReasName = "";
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@START_DATE", StartDate);
                cmd.Parameters.AddWithValue("@END_DATE", EndDate);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dts = new DataTable();

                try
                {
                    connect.Open();
                    da.Fill(dts);
                }
                catch (Exception ex)
                {
                    throw;
                }

                connect.Close();
                return dts;

            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            int newPageNumber = e.NewPageIndex + 1;
            StartDate = ViewState["StartDate"] as string;
            EndDate = ViewState["EndDate"] as string;
            FillDGR(newPageNumber);
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_REAS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BT_JOURNAL_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenModalScript", "openModal();", true);
        }


    }
}