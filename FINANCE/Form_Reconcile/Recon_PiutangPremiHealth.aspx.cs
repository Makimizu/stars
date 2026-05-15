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

namespace FINANCE.Form_Reconcile
{
    public partial class Recon_PiutangPremiHealth : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        //protected Connection connAutoReportRecon = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appidarr"]));
        protected Connection connAutoReportRecon = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
     

        bool isSearch = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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


            string startDate = hdnStartDate.Value;
            string endDate = hdnEndDate.Value;


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
            dt = GetListReconPiutangPremiHealth(startDate, endDate);

            string filename = "List_Recon_PiutangPremiHealth_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));

            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("List_Recon_PiutangPremiHealth");
                ws.Cells["A1"].Value = "Report";
                ws.Cells["A2"].Value = "Period";
                ws.Cells["B1"].Value = ": SUMMARY PIUTANG PREMI HEALTH RECON ";
                ws.Cells["B2"].Value = ": " + startDate + " sd " + endDate; 
                ws.Cells["A4"].LoadFromDataTable(dt, true);
                ws.Cells.AutoFitColumns();

                var ms = new System.IO.MemoryStream();
                pck.SaveAs(ms);
                ms.WriteTo(Response.OutputStream);
            }


        }

        public DataTable GetListReconPiutangPremiHealth(string startDate, string endDate)
        {


            DataTable dt = new DataTable();
            string sqlDataSource = connAutoReportRecon.conn.ConnectionString;

            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();

                string spName = "AutoReportReconcile.dbo.SP_ReconPiutangPremiHealth";

                using (SqlCommand cmd = new SqlCommand(spName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@START_DATE", SqlDbType.Date);
                    cmd.Parameters["@START_DATE"].Value = startDate;

                    cmd.Parameters.Add("@END_DATE", SqlDbType.Date);
                    cmd.Parameters["@END_DATE"].Value = endDate;

                    reader = cmd.ExecuteReader();
                    dt.Load(reader); ;

                    reader.Close();
                    conn.Close();
                }
            }

            return dt;
        }

        protected void FillDGR()
        {
            

            LB_RESULT.Text = "";

            string startDate = hdnStartDate.Value;
            string endDate = hdnEndDate.Value;


            if (!isSearch)
            {
                TXT_STARTDATE.Text = "01" + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                TXT_ENDDATE.Text = DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");

                startDate = DateTime.Now.ToString("MM") + "/" + "01" + "/" + DateTime.Now.ToString("yyyy");
                endDate = DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyyy");
            }
            //else
            //{
            //    string[] dateStartParts = TXT_STARTDATE.Text.Split('/');

            //    if (dateStartParts.Length == 3)
            //    {
            //        string day = dateStartParts[0];   // 30
            //        string month = dateStartParts[1]; // 09
            //        string year = dateStartParts[2];  // 2024

            //        startDate = day.PadLeft(2, '0') + "/" + month.PadLeft(2, '0') + "/" + year;
            //    }

            //    string[] dateEndParts = TXT_ENDDATE.Text.Split('/');

            //    if (dateStartParts.Length == 3)
            //    {
            //        string day = dateEndParts[0];   // 30
            //        string month = dateEndParts[1]; // 09
            //        string year = dateEndParts[2];  // 2024

            //        endDate = day.PadLeft(2, '0') + "/" + month.PadLeft(2, '0') + "/" + year;
            //    }

            //}

            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                //DateTime startDate2 = Convert.ToDateTime(startDate);
                //DateTime endDate2 = Convert.ToDateTime(endDate);
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

            dt = GetListReconPiutangPremiHealth(startDate, endDate);

            DGR.DataSource = dt;
            DGR.DataBind();

            Session["dtGrid"] = dt;
            Session["startDate"] = startDate;
            Session["endDate"] = endDate;


            LB_RESULT.Text = "Records : " + dt.Rows.Count;

            GetDetailDGR(DGR, startDate, endDate);

        }

        public void GetDetailDGR(DataGrid dgr, string startDate, string endDate)
        {

            DataTable dtDetail = new DataTable();


            conn.QueryString = "select URLAPP from FINANCE.dbo.V_LINK_SC_REPORT_LIST b where b.APP_ID = 'FN' and b.CODE = 382";
            conn.ExecuteQuery();
            dtDetail = conn.GetDataTable().Copy();

            string urlDetail = dtDetail.Rows[0]["URLAPP"].ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {


                string docNo = DGR.Items[i].Cells[1].Text;

                string remark = DGR.Items[i].Cells[12].Text.Trim();
                string code = DGR.Items[i].Cells[11].Text.Trim() + "_" + remark;
            

                LinkButton lbDetail = (LinkButton)DGR.Items[i].FindControl("LB_DETAIL");


                string paramDetail = urlDetail + "&DOCNO=" + docNo + "&CODE_RPT=" + code +
                                    "&START_DATE=" + startDate + "&END_DATE=" + endDate;

                if (remark.ToUpper() == "MATCHING")
                    lbDetail.Visible = false;
                else
                    lbDetail.Attributes.Add("onclick", "window.open('" + paramDetail + "','PIUTANGPREMIDETAIL','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");


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

        }
 
    

  

    }
}