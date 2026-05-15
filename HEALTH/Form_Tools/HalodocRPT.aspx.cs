using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Text;

namespace HEALTH.Form_Tools
{
    public partial class HalodocRPT : System.Web.UI.Page
    {

        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            string reportType = ddreportType.SelectedValue.ToString();
            string startDate = string.Empty;
            string endDate = string.Empty;
            string policyNo = string.Empty;

           
            if (txtStartDate.Text == "" || txtEndDate.Text == "")
            {
                lblError.Text = "START or END date cannot be blank!";
            }
            else 
            {
                startDate = txtStartDate.Text;
                endDate = txtEndDate.Text;
                policyNo = txtPolicyNo.Text;
                
                if (reportType == "0") 
                {
                     FillGrid(policyNo, startDate, endDate, "Halodoc Detail");
                     Session["paramRPT"] = "0";
                }
                else if (reportType == "1") 
                {
                    FillGrid(policyNo, startDate, endDate, "Halodoc Header");
                    Session["paramRPT"] = "1";
                }
                else if (reportType == "2") 
                {
                    FillGrid(policyNo, startDate, endDate, "Admedika EClaim");
                    Session["paramRPT"] = "2";
                }
            
            }
        }


        protected void FillGrid(string policyNo, string startDate, string endDate, string paramRPT) 
        {
            DataTable dt = new DataTable();
            dt = GetData(policyNo, startDate, endDate, paramRPT);

            if (paramRPT == "Halodoc Detail") 
            {
                DGR_LIST.DataSource = dt;
                DGR_LIST.DataBind();
                DGR_LIST.Visible = true;
                DGR_LIST2.Visible = false;
                DGR_LIST3.Visible = false;
            }
            else if (paramRPT == "Halodoc Header") 
            {
                DGR_LIST2.DataSource = dt;
                DGR_LIST2.DataBind();
                DGR_LIST2.Visible = true;
                DGR_LIST.Visible = false;
                DGR_LIST3.Visible = false;
            }
            else if (paramRPT == "Admedika EClaim") 
            {
                DGR_LIST3.DataSource = dt;
                DGR_LIST3.DataBind();
                DGR_LIST3.Visible = true;
                DGR_LIST.Visible = false;
                DGR_LIST2.Visible = false;
            }

            btnExportCSV.Visible = true;
            btnExportExcel.Visible = true;
            btnExportPDF.Visible = false;
        }
        
        protected DataTable GetData(string policyNo, string startDate, string endDate, string paramRpt) 
        {
            DataTable dt = new DataTable();
            string query = string.Empty;

            if (paramRpt == "Halodoc Detail")
            {
                query = "EXEC ATK_RPT_CLAIM_HISTORY_DETAIL_HALODOC '" + policyNo + "','" + startDate + "','" + endDate + "'";
            }
            else if (paramRpt == "Halodoc Header")
            {
                query = "EXEC ATK_RPT_CLAIM_HISTORY_HEADER_HALODOC '" + policyNo + "','" + startDate + "','" + endDate + "'";
            }
            else if (paramRpt == "Admedika EClaim") 
            {
                query = "EXEC ATK_RPT_ADMEDIKA_ECLAIM '" + policyNo + "','" + startDate + "','" + endDate + "'";
            }
            
            try
            {
                conn.QueryString = query;
                conn.ExecuteQuery(1200);
                dt = conn.GetDataTable().Copy();
                Session["datatable"] = dt;
                lblCount.Text = dt.Rows.Count.ToString() + " Records"; 
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }

            return dt;
        }

        protected void chkNull_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNull.Checked)
            {
                txtPolicyNo.Enabled = false;
            }
            else 
            {
                txtPolicyNo.Enabled = true;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["datatable"];
            ExportToCSV(dt);


        }

        protected void ExportToCSV(DataTable dt) 
        {
            string paramRPT = Session["paramRPT"].ToString();
            var date = string.Format("{0:yyyyMMdd}.txt", DateTime.Now);
            var filnameDetail = "CLAIM_HISTORY_DETAIL_" + date;
            var filenameHeader = "CLAIM_HISTORY_HEADER_" + date;
            var filenameEClaim = "ADMEDIKA_ECLAIM_" + date;
            
            try
            {
                Response.Clear();
                Response.Buffer = true;

                if (paramRPT == "0")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + filnameDetail + "");
                }
                else if (paramRPT == "1")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + filenameHeader + "");
                }
                else if (paramRPT == "2")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + filenameEClaim + "");
                }
            
                
                Response.Charset = "";
                Response.ContentType = "application/text";
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        sb.Append(@"""");
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";"));
                        sb.Append(@"""");
                        sb.Append(@",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("\r\n");
                }

                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }

        protected void ExportToExcel(DataTable dt) 
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            string paramRPT = Session["paramRPT"].ToString();
            var date = string.Format("{0:yyyyMMdd}.txt", DateTime.Now);
            var filnameDetail = "CLAIM_HISTORY_DETAIL_" + date;
            var filenameHeader = "CLAIM_HISTORY_HEADER_" + date;
            var filenameEClaim = "ADMEDIKA_ECLAIM_" + date;


            Response.Clear();
            Response.Buffer = true;

            if (paramRPT == "0")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filnameDetail + "");
            }
            else if (paramRPT == "1")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filenameHeader + "");
            }
            else if (paramRPT == "2")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filenameEClaim + "");
            }

            //Response.AddHeader("content-disposition", "attachment;filename=ATK_RPT_CLAIM_HISTORY_DETAIL_HALODOC.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }

            GridView1.RenderControl(hw);
            
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["datatable"];
            ExportToExcel(dt);
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {

        }

        protected void ExportToPDF(DataTable dt) 
        {
            //GridView GridView1 = new GridView();
            //GridView1.AllowPaging = false;
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            //GridView1.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            
        }
    }
}