using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.NetworkInformation;
using OfficeOpenXml.FormulaParsing.Utilities;
using OfficeOpenXml;

namespace HEALTH.Form_Klaim
{
    public partial class CPbyRs : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        public DataTable dt = new DataTable();
        private string sortExpression = "KODE_PROVIDER"; // Default sort column
        private string sortDirection = "desc"; // Default sort direction
        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "PROVIDER"; } // Replace with default sorting column
            set { ViewState["SortExpression"] = value; }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "DESC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            if (!IsPostBack)
            {
                for (int year = DateTime.Now.Year; year >= DateTime.Now.Year - 10; year--)
                {
                    DDL_YEAR.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                string PROVIDER = Request.QueryString["PROVIDER"] ?? "";
                if (!string.IsNullOrEmpty(PROVIDER))
                {
                    FillDGRReport(PROVIDER);
                }

                LB_ID.Text = "Clinical Pathway on progress";
            }
        }

        private DataTable GetReportData(string searchQuery)
        {
            string sortExpression = SortExpression;
            string sortDirection = SortDirection;
            string monthFilter = DDL_MONTH.SelectedValue;
            string yearFilter = DDL_YEAR.SelectedValue;

            // Check if data is already stored and matches the current search query
            /*if (monthFilter == ViewState["MonthFilter"]
                && yearFilter == ViewState["YearFilter"]
                && sortExpression == ViewState["SortExpression"]
                && sortDirection == ViewState["SortDirection"]
                && ViewState["LastSearchQuery"] as string == searchQuery 
                && ViewState["ReportData"] is DataTable cachedData)
            {
                return cachedData;
            }*/
            DataTable cachedData = ViewState["ReportData"] as DataTable;
            if (monthFilter == ViewState["MonthFilter"]
                && yearFilter == ViewState["YearFilter"]
                && sortExpression == ViewState["SortExpression"]
                && sortDirection == ViewState["SortDirection"]
                && ViewState["LastSearchQuery"] as string == searchQuery
                && cachedData != null)
            {
                return cachedData;
            }



            string query = @"
                SELECT 
                    PROVIDER, 
                    MONTH(TGL_KLAIM) AS MONTH, 
                    DATENAME(MONTH, TGL_KLAIM) AS MONTH_NAME,
                    YEAR(TGL_KLAIM) AS YEAR, 
                    COUNT(STATUS_CP) AS TOTAL_CP,
                    SUM(CASE WHEN STATUS_CP = 1 THEN 1 ELSE 0 END) AS CP_COUNT, 
                    SUM(CASE WHEN STATUS_CP = 2 THEN 1 ELSE 0 END) AS CP_NOT_FITS_COUNT 
                FROM 
                    V_CP_BY_CLAIM 
                WHERE 1=1";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                //query += $" AND PROVIDER LIKE '%{searchQuery}%'";
                query += string.Format(" AND PROVIDER LIKE '%{0}%'", searchQuery);
            }

            if (!string.IsNullOrEmpty(DDL_MONTH.SelectedValue))
            {
                //query += $" AND MONTH(TGL_KLAIM) = {DDL_MONTH.SelectedValue}";
                query += string.Format(" AND MONTH(TGL_KLAIM) = {0}", DDL_MONTH.SelectedValue);
            }

            if (!string.IsNullOrEmpty(DDL_YEAR.SelectedValue))
            {
                //query += $" AND YEAR(TGL_KLAIM) = {DDL_YEAR.SelectedValue}";
                query += string.Format(" AND YEAR(TGL_KLAIM) = {0}", DDL_YEAR.SelectedValue);
            }

            query += @"
            and STATUS_CP != 0
           GROUP BY 
            PROVIDER, 
            YEAR(TGL_KLAIM), 
            MONTH(TGL_KLAIM), 
            DATENAME(MONTH, TGL_KLAIM)";

            if (!string.IsNullOrEmpty(SortExpression) && !string.IsNullOrEmpty(SortDirection))
            {
                //query += $" ORDER BY {SortExpression} {SortDirection}";
                query += string.Format(" ORDER BY {0} {1}", SortExpression, SortDirection);
            }

            conn.QueryString = query;
            conn.ExecuteQuery(120);
            DataTable dt = conn.GetDataTable().Copy();

            // Store data in ViewState to reuse later
            ViewState["ReportData"] = dt;
            ViewState["LastSearchQuery"] = searchQuery;
            ViewState["YearFilter"] = yearFilter;
            ViewState["MonthFilter"] = monthFilter;
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            return dt;
        }

        protected void FillDGRReport(string searchQuery = "")
        {
            DataTable dt = GetReportData(searchQuery);

            DGR_REPORT.DataSource = dt;
            DGR_REPORT.DataBind();

            if (dt.Rows.Count == 0)
            {
                DataRow noDataRow = dt.NewRow();
                noDataRow["PROVIDER"] = "No Data Available";
                dt.Rows.Add(noDataRow);

                DGR_REPORT.DataSource = dt;
                DGR_REPORT.DataBind();

                DGR_REPORT.AllowSorting = false;
                DGR_REPORT.AllowPaging = false;
            }
        }


        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            DataTable dt = ViewState["ReportData"] as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                // Handle empty data scenario
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write("No data available to download.");
                Response.End();
                return;
            }

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report");

                // Load DataTable directly into Worksheet
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Store the file on the server
                string folderPath = Server.MapPath("~/Downloads/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, "CP_CLAIM_BY_RS_Report.xlsx");
                File.WriteAllBytes(filePath, package.GetAsByteArray());

                // Provide download link
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //Response.AddHeader("content-disposition", $"attachment; filename={Path.GetFileName(filePath)}");
                Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(filePath));

                Response.TransmitFile(filePath);
                Response.End();
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void BT_FILTER_Click(object sender, EventArgs e)
        {
            string searchProvider = TXT_SEARCH.Text.Trim();
            FillDGRReport(searchProvider);

            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DGR_REPORT_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            if (SortExpression == e.SortExpression)
            {
                SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                SortExpression = e.SortExpression;
                SortDirection = "ASC";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showSwal", "showLoading();", true);
            FillDGRReport(TXT_SEARCH.Text.Trim());
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DGR_REPORT_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSwal", "showLoading();", true);
            DGR_REPORT.CurrentPageIndex = e.NewPageIndex;
            FillDGRReport(TXT_SEARCH.Text.Trim());
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DDL_KIRIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_REPORT.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_REPORT.Items[i].FindControl("DDL_KIRIM");
                TextBox txtemailfax = (TextBox)DGR_REPORT.Items[i].FindControl("TXT_EMAILFAX");

                if (ddl == (DropDownList)sender)
                {
                    if (ddl.SelectedValue == "0")
                        txtemailfax.Text = DGR_REPORT.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (ddl.SelectedValue == "1")
                        txtemailfax.Text = DGR_REPORT.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
            }
        }
    }
}