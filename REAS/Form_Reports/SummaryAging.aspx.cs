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

namespace REAS.Form_Reports
{
    public partial class SummaryAging : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string SelectedStatus;
        protected string ReasName;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            DGR.AllowPaging = true;
            if (!IsPostBack)
            {
                DateTime currentDate = DateTime.Now;
                DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);
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

        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";

            string query = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_FINANCIAL_SETTLEMENT] WHERE 1=1";

            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                query += " AND CONVERT(VARCHAR(10), CAST(AR_DATE AS DATETIME), 120)  BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
            }

            if (!string.IsNullOrEmpty(SelectedStatus))
            {
                query += " AND STATUS = '" + SelectedStatus.ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(ReasName))
            {
                query += " AND REAS_NAME LIKE '%" + ReasName.Replace("'", "''") + "%'";
            }


            query += " ORDER BY ID ASC";

            conn.QueryString = query;
            conn.ExecuteQuery();

            // Display the result count
            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            // Bind the data to the grid
            DataTable dt = new DataTable();
            //dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
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
            SelectedStatus = DDL_STATUS.SelectedValue;


            FillDGR();
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            LB_DOWNLOAD.Text = "Please wait, Downloading File......";
            if (!string.IsNullOrEmpty(TXT_STARTDATE.Text) && !string.IsNullOrEmpty(TXT_ENDDATE.Text))
            {
                //DateTime startDateTxt = DateTime.Parse(TXT_STARTDATE.Text);
                DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                StartDate = startDateTxt.ToString("yyyy-MM-dd");
                //DateTime endDateTxt = DateTime.Parse(TXT_ENDDATE.Text);

                DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                EndDate = endDateTxt.ToString("yyyy-MM-dd");
                SelectedStatus = DDL_STATUS.SelectedValue;
            }

            ReasName = DDL_REAS.SelectedValue;
            SelectedStatus = DDL_STATUS.SelectedValue;

            // Set up the response
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ClaimReasHealth_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            // Set the EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Retrieve data
            DataTable dt = GetDataDownload(StartDate, EndDate, ReasName, SelectedStatus);

            // Create Excel package
            using (ExcelPackage pck = new ExcelPackage())
            {
                // Add a new worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List Claim Reas Health");

                // Add headers and information
                ws.Cells["A1"].Value = "Report";
                //ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": Claim Reas Health";
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

        private DataTable GetDataDownload(string startDate, string endDate, string reasName, string selectedStatus)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM [AutoReportReconcile].[dbo].[V_RPT_FINANCIAL_SETTLEMENT] WHERE 1=1";

            if (!string.IsNullOrEmpty(StartDate) && !string.IsNullOrEmpty(EndDate))
            {
                query += " AND CONVERT(DATE, AR_DATE) BETWEEN '" + StartDate + "' AND '" + EndDate + "'";
            }


            if (!string.IsNullOrEmpty(SelectedStatus))
            {
                query += " AND Status = '" + SelectedStatus + "'";
            }

            if (!string.IsNullOrEmpty(ReasName))
            {
                query += " AND REAS_NAME LIKE '%" + ReasName.Replace("'", "''") + "%'";
            }

            query += " ORDER BY TGL_KLAIM ASC, ID ASC";


            conn.QueryString = query;
            conn.ExecuteQuery();

            // Bind the data to the grid
            dt = conn.GetDataTable().Copy();

            return dt;
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            DGR.DataSource = Session["dtGrid"];
            FillDGR();
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
    }
}