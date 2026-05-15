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
using Newtonsoft.Json;
using System.Web.Services;
using OfficeOpenXml;
using System.Windows;
using System.Data.SqlClient;

namespace REAS.Form_App
{
    public partial class MemoApproval : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected string StartDate;
        protected string EndDate;
        protected string StartDateUpdate;
        protected string EndDateUpdate;
        protected string SelectedStatus;
        protected string ReasName;
        protected string NoMemo;


        private int pageSize = 200;
        private int totalRows = 0;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //DGR.AllowPaging = true;

            if (!IsPostBack)
            {
                ViewState.Clear();

                DDL_STATUS.SelectedValue = "0";
                SelectedStatus = DDL_STATUS.SelectedValue;

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
                //BindDGDetailMemo();


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


        protected void FillDGR(int pageNumbers)
        {
            LB_RESULT.Text = "";


            string connectString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_GET_MEMO]", connect))
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
                cmd.Parameters.AddWithValue("@REAS_NAME", ReasName);
                cmd.Parameters.AddWithValue("@NOMEMO", NoMemo ?? "");
                cmd.Parameters.AddWithValue("@STATUS", SelectedStatus ?? "");


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

        

            }
        }

        private void BindDGDetailMemo(string MEMOID)
        {
            DataTable dt = GetMemoDetails(MEMOID); // Ambil data dari DB

            if (dt.Rows.Count == 0)
            {
                // Buat dummy row supaya header tetap tampil
                dt.Rows.Add(dt.NewRow());
            }

            DGDetailMemo.DataSource = dt;
            DGDetailMemo.DataBind();

            DGDetailMemo.Visible = true;
        }

        private DataTable GetMemoDetails(string mEMOID)
        {
            LB_RESULT.Text = "";


            string connectString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            using (SqlConnection connect = new SqlConnection(connectString))
            using (SqlCommand cmd = new SqlCommand("[dbo].[SP_RTF_GET_MEMODETAIL]", connect))
            {
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOMEMO", mEMOID);


                connect.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                    return dt;
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

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGDetailMemo.Visible = false;

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

            ViewState["StartDate"] = StartDate;
            ViewState["EndDate"] = EndDate;
            ViewState["ReasName"] = ReasName;
            ViewState["Status"] = SelectedStatus;

            FillDGR(1);
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            int newPageNumber = e.NewPageIndex + 1;
            StartDate = ViewState["StartDate"] as string;
            EndDate = ViewState["EndDate"] as string;
            ReasName = ViewState["ReasName"] as string;
            SelectedStatus = ViewState["Status"] as string;
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Approved")
            {
                string id = e.CommandArgument.ToString();

                // Update MEMO
                string query = "UPDATE [dbo].[RTF_MEMOREAS]" +
                        " SET ISAPPROVE = 1" +
                        " WHERE MEMOID = '" + id + "';";

                conn.QueryString = query;
                conn.ExecuteQuery();

                Response.Write("<script>alert('Data telah di Approve.');window.location.href = window.location.href;</script>");
            }
        }

        protected void btnReject_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Canceled")
            {
                string id = e.CommandArgument.ToString();

                // Update MEMO
                string query = "UPDATE [dbo].[RTF_MEMOREAS]" +
                        " SET ISAPPROVE = 2" +
                        " WHERE MEMOID = '" + id + "';";

                conn.QueryString = query;
                conn.ExecuteQuery();

                Response.Write("<script>alert('Data telah di batalkan.');window.location.href = window.location.href;</script>");
            }
        }
        private DataTable GetDetailData(string id)
        {
            string query = "SELECT DISTINCT TYPE AS MEMO_TYPE, NO_POLICY AS NO_POLIS, ID FROM RTF_MEMOREAS_DETAIL WHERE MEMOID = '" + id + "'";
            conn.QueryString = query;
            conn.ExecuteQuery();

            return conn.GetDataTable().Copy();
        }

        protected void btnView_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string id = e.CommandArgument.ToString();

                BindDGDetailMemo(id);

                // Retrieve detail data based on the ID
                DataTable detailTable = GetDetailData(id); // Replace with your method to get details
                //hiddenNoMemoView.Value = id; // Set nilai hidden field
                //txtNoMemoView.Text = id; // Set TextBox GV_UploadedData.DataSource = uploadedDataList;
                GV_UploadedData.DataSource = detailTable;
                GV_UploadedData.DataBind();

                // Tampilkan modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowModal", "openModal();", true);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalViewDetail", "openModalViewDetailMemo('" + script + "');window.location.reload(true);", true);
            }
                else
                {
                    // Jika DataTable kosong, menampilkan pesan atau tindakan lain
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalError", "alert('No data available.');", true);
                }
            
        }

        protected void btnDownload_Command(object sender, CommandEventArgs e)
        {
            string link = "";
            if (e.CommandName == "Download")
            {
                string id = e.CommandArgument.ToString();
                string ids = id.Replace("/", "-").ToString();

                string query = "SELECT REPLACE(a.MEMOID,'/','-') MEMOIDS, a.MEMOID, CASE WHEN a.MEMO_TYPE = 'IM' THEN 'INTERNAL MEMO' ELSE 'EXTERNAL MEMO' END MEMO_TYPE, a.REAS_NAME, a.ACTUAL_AMOUNT AS AMOUNT, b.NOTE, b.VALUE ISVALUE, c.FOLDER, c.[URL] AS URL_PATH FROM RTF_MEMOREAS a INNER JOIN [dbo].[RTF_PARAM_APPLICATION] b ON a.MEMO_TYPE = b.[KEY] INNER JOIN SECURITY.dbo.REPORT_LIST c ON b.VALUE  = c.CODE AND b.NOTE COLLATE SQL_Latin1_General_CP1_CI_AS = c.DESCR WHERE b.APP_ID = 'RE' AND b.CODE = '0006' AND REPLACE(a.MEMOID,'/','-') = '" + ids + "';";

                conn.QueryString = query;
                conn.ExecuteQuery();
                string MEMO_TYPE = conn.GetFieldValue("MEMO_TYPE");
                string REAS_NAME = conn.GetFieldValue("REAS_NAME");
                string URL_PATH = conn.GetFieldValue("URL_PATH");

                decimal AMOUNT = Convert.ToDecimal(conn.GetFieldValue("AMOUNT"));


                if (MEMO_TYPE == "INTERNAL MEMO")
                {
                    if (AMOUNT >= 50000001)
                    {
                        link = URL_PATH + "&NO_MEMO=" + ids + "&REAS_NAME=" + REAS_NAME;

                    }
                    else if (AMOUNT >= 1000000001)
                    {
                        link = URL_PATH + "&NO_MEMO=" + ids + "&REAS_NAME=" + REAS_NAME;

                    }
                    else if (AMOUNT >= 2000000001)
                    {
                        link = URL_PATH + "&NO_MEMO=" + ids + "&REAS_NAME=" + REAS_NAME;

                    }
                    else
                    {
                        link = URL_PATH + "&NO_MEMO=" + ids + "&REAS_NAME=" + REAS_NAME;
                    }
                }
                else
                {
                    link = URL_PATH + "&NO_MEMO=" + ids + "&COMPANY_NAME=" + REAS_NAME;

                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openReport", "window.open('" + link + "', '_blank');", true);
            }
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        //protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Page")
        //    {
        //        int page = Convert.ToInt32(e.CommandArgument);
        //        StartDate = ViewState["StartDate"] as string;
        //        EndDate = ViewState["EndDate"] as string;
        //        ReasName = ViewState["ReasName"] as string;
        //        SelectedStatus = ViewState["Status"] as string;
        //        FillDGR(page);
        //    }
        //}
    }
}