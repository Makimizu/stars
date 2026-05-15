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

namespace HEALTH.Form_Klaim
{
    public partial class CPbyNoSurat : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        private string sortExpression = "KODE_PROVIDER"; // Default sort column
        private string sortDirection = "desc"; // Default sort direction
        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? "ICD"; } // Replace with default sorting column
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
                string noSurat = Request.QueryString["SURAT"] ?? "";
                FillDGRReport(noSurat);

                LB_ID.Text = "Clinical Pathway on progress";
            }
        }

        protected void FillDGRReport(string noSurat = "")
        {
            string connectionString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            string queryString = "exec SP_GET_CP_BY_NO_SURAT @Nomor";

            using (SqlDataAdapter adapter = new SqlDataAdapter(queryString, connectionString))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@Nomor", noSurat);

                DataSet test = new DataSet();
                adapter.Fill(test, "CPData");

                DataTable dt = test.Tables["CPData"];

                // If no data is found, display "No CP Found"
                if (dt.Rows.Count == 0)
                {
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["PROVIDER"] = "No Data Available";
                    noDataRow["DIAGNOSA"] = "-";
                    noDataRow["DIAGNOSE"] = "-";
                    noDataRow["KELAS_KAMAR"] = "-";
                    noDataRow["AKTUAL_INAP"] = "0";
                    noDataRow["LAMA_RAWAT"] = "0";
                    noDataRow["BIAYA"] = "0.00";
                    noDataRow["TOTAL_BIAYA_CP"] = "0.00";
                    dt.Rows.Add(noDataRow);

                    DGR_REPORT.DataSource = dt;
                    DGR_REPORT.DataBind();

                    // Disable pagination and sorting for "No Data" row
                    DGR_REPORT.AllowSorting = false;
                    DGR_REPORT.AllowPaging = false;
                }
                else
                {
                    
                    DataView dv = new DataView(dt);

                    
                    //string sortExpression = $"{SortExpression} {SortDirection}";
                    //bugfix interpolate string
                    string sortExpression = string.Format("{0} {1}", SortExpression, SortDirection);
                    dv.Sort = sortExpression;

                    DGR_REPORT.DataSource = dv;
                    DGR_REPORT.DataBind();

                    for (int i = 0; i < DGR_REPORT.Items.Count; i++)
                    {
                        Button bt1 = (Button)DGR_REPORT.Items[i].FindControl("BT_PDF1");
                        string filePath = DGR_REPORT.Items[i].Cells[1].Text.Replace("&nbsp;", ""); 

                        if (!string.IsNullOrEmpty(filePath))
                        {
                            string fullUrl = ResolveUrl("~/" + filePath);
                            //bt1.Attributes.Add("onclick", $"window.open('{fullUrl}', '_blank'); return false;");
                            bt1.Attributes.Add("onclick", "window.open('" + fullUrl + "', '_blank'); return false;");

                        }
                        else
                        {
                            bt1.Enabled = false;
                        }
                    }
                }
            }
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
                SortDirection = "ASC"; // Default to ASC when switching columns
            }
            string searchQuery = Request.QueryString["SURAT"] ?? "";

            FillDGRReport(searchQuery); // Reload data with sorting
        }

        protected void DGR_REPORT_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_REPORT.CurrentPageIndex = e.NewPageIndex;
            FillDGRReport();
        }


        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            string searchQuery = Request.QueryString["SURAT"] ?? "";
            FillDGRReport(searchQuery);
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