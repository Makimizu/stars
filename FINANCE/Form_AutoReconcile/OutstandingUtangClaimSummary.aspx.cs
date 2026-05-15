using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using System.Data.SqlClient;
using System.Globalization;

namespace FINANCE.Form_AutoReconcile
{
    public partial class OutstandingUtangClaimSummary : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        //protected Connection connAutoReportRecon = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appidarr"]));
        protected Connection connAutoReportRecon = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string spName;


        bool isSearch = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            DateTime currentDate = DateTime.Now;
            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            

            if (!IsPostBack)
            {
                TXT_STARTDATE.Text = startDate.ToString("dd/MM/yyyy");
                TXT_ENDDATE.Text = currentDate.ToString("dd/MM/yyyy");
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                try
                {
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                conn.QueryString = "select [KEY],[VALUE] from [AutoReportReconcile].[dbo].[mConfiguration] WHERE APP_ID = 'FN' AND CODE = '0001'";
                conn.ExecuteQuery();
                DDL_CHANNEL.Items.Add("");
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }

                DGR.CurrentPageIndex = 0;
                FillDGR();

            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;

            isSearch = true;
            FillDGR();
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            string startDate = TXT_STARTDATE.Text;
            string endDate = TXT_ENDDATE.Text;

          
            DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            startDate = startDateTxt.ToString("MM/dd/yyyy");

            DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            endDate = endDateTxt.ToString("MM/dd/yyyy");


            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
                    return;
            }
            else if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                return;
            else if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                return;
            else if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                return;


            DataTable dt = new DataTable();
            dt = GetListReconKomisiHealth(startDate, endDate);

            string filename = "Report_AutoRecon_ReportSummary_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("REPORT");
                ws.Cells["A1"].Value = "Report";
                ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": SUMMARY UTANG KLAIM ";
                ws.Cells["B2"].Value = ": " + startDate + " sd " + endDate;
                ws.Cells["A4"].LoadFromDataTable(dt, true);
                ws.Cells.AutoFitColumns();

                var ms = new System.IO.MemoryStream();
                pck.SaveAs(ms);
                ms.WriteTo(Response.OutputStream);
            }
        }

        public DataTable GetListReconKomisiHealth(string startDate, string endDate)
        {
            DataTable dt = new DataTable();
            string sqlDataSource = connAutoReportRecon.conn.ConnectionString;

            try
            {
                SqlDataReader reader;
                using (SqlConnection conn = new SqlConnection(sqlDataSource))
                {
                    conn.Open();

                    if (DDL_CHANNEL.SelectedValue != null)
                    {
                        if (DDL_CHANNEL.SelectedValue == "HEALTH")
                        {
                            spName = "[AutoReportReconcile].[dbo].[SP_OutstandingUtangSummary_HEALTH]";
                        }
                        else if (DDL_CHANNEL.SelectedValue == "GLIFE")
                        {
                            spName = "[AutoReportReconcile].[dbo].[SP_OutstandingUtangSummary_GLIFE]";
                        }
                        else if (DDL_CHANNEL.SelectedValue == "RISK")
                        {
                            spName = "[AutoReportReconcile].[dbo].[SP_OutstandingUtangSummary_RISK]";
                        }
                        else if (DDL_CHANNEL.SelectedValue == "LINK")
                        {
                            spName = "[AutoReportReconcile].[dbo].[SP_OutstandingUtangSummary_LINK]";
                        }
                        else if (DDL_CHANNEL.SelectedValue == "NON LINK")
                        {
                            spName = "[AutoReportReconcile].[dbo].[SP_OutstandingUtangSummary_NONLINK]";
                        }
                    }
                    if (spName != "")
                    {
                        using (SqlCommand cmd = new SqlCommand(spName, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@START_DATE", SqlDbType.Date);
                            cmd.Parameters["@START_DATE"].Value = startDate;

                            cmd.Parameters.Add("@END_DATE", SqlDbType.Date);
                            cmd.Parameters["@END_DATE"].Value = endDate;

                            cmd.Parameters.Add("@CHANNEL", SqlDbType.NVarChar);
                            cmd.Parameters["@CHANNEL"].Value = DDL_CHANNEL.SelectedValue;

                            cmd.Parameters.Add("@DOCNO", SqlDbType.NVarChar);
                            cmd.Parameters["@DOCNO"].Value = TXT_DOCNO.Text;

                            cmd.Parameters.Add("@POLICYNO", SqlDbType.NVarChar);
                            cmd.Parameters["@POLICYNO"].Value = TXT_POLICYNO.Text;

                            cmd.Parameters.Add("@STLDOCNO", SqlDbType.NVarChar);
                            cmd.Parameters["@STLDOCNO"].Value = TXT_STL_DOCNO.Text;

                            cmd.Parameters.Add("@NAMALEMBAGA", SqlDbType.NVarChar);
                            cmd.Parameters["@NAMALEMBAGA"].Value = TXT_NAMALEMBAGA.Text;

                            cmd.Parameters.Add("@NAMAPESERTA", SqlDbType.NVarChar);
                            cmd.Parameters["@NAMAPESERTA"].Value = TXT_NAMAPESERTA.Text;

                            reader = cmd.ExecuteReader();
                            dt.Load(reader);

                            reader.Close();
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            

            return dt;
        }


        protected void FillDGR()
        {

            try
            {
                LB_RESULT.Text = "";

                string startDate = TXT_STARTDATE.Text;
                string endDate = TXT_ENDDATE.Text;


                if (!isSearch)
                {

                    TXT_STARTDATE.Text = "01" + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                    TXT_ENDDATE.Text = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

                    startDate = DateTime.Now.ToString("MM") + "/" + "01" + "/" + DateTime.Now.ToString("yyyy");
                    endDate = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");

                    //startDate = "01" + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                    //endDate = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

                }
                else
                {
                    DateTime startDateTxt = DateTime.ParseExact(TXT_STARTDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime endDateTxt = DateTime.ParseExact(TXT_ENDDATE.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    startDate = startDateTxt.ToString("MM/dd/yyyy");
                    endDate = endDateTxt.ToString("MM/dd/yyyy");

                    //startDate = startDateTxt.ToString("yyyy-MM-dd");
                    //endDate = endDateTxt.ToString("yyyy-MM-dd");
                }

                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    if (Convert.ToDateTime(startDate) > Convert.ToDateTime(endDate))
                        return;
                }
                else if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                    return;
                else if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    return;
                else if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                    return;

               

                DataTable dt = new DataTable();

                dt = GetListReconKomisiHealth(startDate, endDate);


                DGR.DataSource = dt;
                DGR.DataBind();

                Session["dtGrid"] = dt;
                Session["startDate"] = startDate;
                Session["endDate"] = endDate;


                LB_RESULT.Text = "Records : " + dt.Rows.Count;

                GetDetailDGR(DGR, startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            

        }


        public void GetDetailDGR(DataGrid dgr, string startDate, string endDate)
        {

            DataTable dtDetail = new DataTable();


            conn.QueryString = "select URLAPP from FINANCE.dbo.V_LINK_SC_REPORT_LIST b where b.APP_ID = 'FN' and b.CODE = 404";
            conn.ExecuteQuery();
            dtDetail = conn.GetDataTable().Copy();

            string urlDetail = dtDetail.Rows[0]["URLAPP"].ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                string id = DGR.Items[i].Cells[3].Text;

                LinkButton lbDetail = (LinkButton)DGR.Items[i].FindControl("LB_DETAIL");


                string paramDetail = urlDetail + "&DOCNO=" + id + 
                                    "&START_DATE=" + startDate + "&END_DATE=" + endDate;

                lbDetail.Attributes.Add("onclick", "window.open('" + paramDetail + "','UTANGKLAIMGLIFE','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");


            }

        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;

            DGR.DataSource = Session["dtGrid"];
            DGR.DataBind();

            string startDate = Session["startDate"].ToString();
            string endDate = Session["endDate"].ToString();

            GetDetailDGR(DGR, startDate, endDate);

            //DGR.CurrentPageIndex = e.NewPageIndex;
            //DGR.DataBind();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}