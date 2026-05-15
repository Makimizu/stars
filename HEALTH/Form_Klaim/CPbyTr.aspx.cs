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
//using System.Windows.Controls;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace HEALTH.Form_Klaim
{
    public partial class CPbyTr: System.Web.UI.Page
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
               
                FillDGRReport(TXT_CODE.Text.Trim(), TXT_NAME.Text.Trim());
                
            }
        }

        private DataTable GetReportData(string searchQuery,string searchQuery1)
        {
            string sortExpression = SortExpression;
            string sortDirection = SortDirection;
         
            //    if ( sortExpression == ViewState["SortExpression"]
            //    && sortDirection == ViewState["SortDirection"]
            //    && ViewState["LastSearchQuery"] as string == searchQuery 
            //    && ViewState["ReportData"] is DataTable cachedData)
            //{
            //    return cachedData;
            //}

            string query = @"
            SELECT    [KODE_PROVIDER]
          ,[NAMA_PROVIDER]
          ,[ALAMAT]
          ,[KOTA]
          ,[JENIS_TARIF]
          ,[SUB_TARIF]
          ,[Kelas_3]
          ,[Kelas_2]
          ,[Kelas_1]
          ,[Utama]
          ,[VIP]
          ,[Super_VIP]
          ,[VVIP]
          ,[ICU]
          ,[ISOLASI]
          ,[Mulai_Berlaku]   
      FROM  [V_PROVIDER_TARIF_REPORT]  WHERE 1=1";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                //query += $" AND KODE_PROVIDER LIKE '%{searchQuery}%'";
                query += string.Format(" AND KODE_PROVIDER LIKE '%{0}%'", searchQuery);
            }

            if (!string.IsNullOrEmpty(searchQuery1))
            {

                //query += $" AND  NAMA_PROVIDER LIKE '%{searchQuery1}%'";
                query += string.Format(" AND NAMA_PROVIDER LIKE '%{0}%'", searchQuery1);
            }


            conn.QueryString = query;
            conn.ExecuteQuery(120);
            DataTable dt = conn.GetDataTable().Copy();

            // Store data in ViewState to reuse later
            ViewState["ReportData"] = dt;
            ViewState["LastSearchQuery"] = searchQuery;
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            return dt;
        }

        protected void FillDGRReport(string searchQuery = "", string searchQuery1 = "")
        {
            DataTable dt = GetReportData(searchQuery, searchQuery1);

            DGR_REPORT1.DataSource = dt;
            DGR_REPORT1.DataBind();

            if (dt.Rows.Count == 0)
            {
                DataRow noDataRow = dt.NewRow();
                noDataRow["PROVIDER"] = "No Data Available";
                dt.Rows.Add(noDataRow);

                DGR_REPORT1.DataSource = dt;
                DGR_REPORT1.DataBind();

                DGR_REPORT1.AllowSorting = false;
                DGR_REPORT1.AllowPaging = false;
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

                string filePath = Path.Combine(folderPath, "Report_Tarif_by_Provider_Report.xlsx");
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
            string searchCode = TXT_CODE.Text.Trim();
            string searchName = TXT_NAME.Text.Trim();
            FillDGRReport(searchCode, searchName);

            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DGR_REPORT1_SortCommand(object source, DataGridSortCommandEventArgs e)
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
            FillDGRReport(TXT_CODE.Text.Trim(), TXT_NAME.Text.Trim());
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DGR_REPORT1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSwal", "showLoading();", true);
            DGR_REPORT1.CurrentPageIndex = e.NewPageIndex;
            FillDGRReport(TXT_CODE.Text.Trim(), TXT_NAME.Text.Trim());
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        protected void DDL_KIRIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_REPORT1.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_REPORT1.Items[i].FindControl("DDL_KIRIM");
                TextBox txtemailfax = (TextBox)DGR_REPORT1.Items[i].FindControl("TXT_EMAILFAX");

                if (ddl == (DropDownList)sender)
                {
                    if (ddl.SelectedValue == "0")
                        txtemailfax.Text = DGR_REPORT1.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (ddl.SelectedValue == "1")
                        txtemailfax.Text = DGR_REPORT1.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
            }
        }
    }
}