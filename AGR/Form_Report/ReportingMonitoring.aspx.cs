
using System; 
using System.Web.UI;
using System.Web.UI.WebControls; 
using DMS.DBConnection;
using System.Data; 
using System.IO; 
namespace AGR.Form_Report
{
    public partial class ReportingMonitoring : System.Web.UI.Page
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


            if (txtStartDate.Text == "" || txtEndDate.Text == "")
            {
                lblError.Text = "START or END date cannot be blank!";
            }
            else
            {
                startDate = txtStartDate.Text;
                endDate = txtEndDate.Text;

                if (reportType == "0")
                {
                    FillGrid(startDate, endDate, "polis individu");
                    Session["paramRPT"] = "0";
                }
                else if (reportType == "1")
                {
                    FillGrid(startDate, endDate, "polis corporate");
                    Session["paramRPT"] = "1";
                }
                else 
                {
                    FillGrid(startDate, endDate, "polis health");
                    Session["paramRPT"] = "2";
                } 
            }
        }


        protected void FillGrid(string startDate, string endDate, string paramRPT)
        {
            DataTable dt = new DataTable();
            dt = GetData(startDate, endDate, paramRPT);

            if (paramRPT == "polis individu")
            {
                DGR_LIST.DataSource = dt;
                DGR_LIST.DataBind();
                DGR_LIST.Visible = true;
                DGR_LIST2.Visible = false;
                DGR_LIST3.Visible = false;
            }
            else if  (paramRPT == "polis corporate")
            {
                DGR_LIST2.DataSource = dt;
                DGR_LIST2.DataBind();
                DGR_LIST.Visible = false;
                DGR_LIST2.Visible = true;
                DGR_LIST3.Visible = false;
            }
            else
            {
                DGR_LIST3.DataSource = dt;
                DGR_LIST3.DataBind();
                DGR_LIST.Visible = false;
                DGR_LIST2.Visible = false;
                DGR_LIST3.Visible = true;
            }

                btnExportExcel.Visible = true;
        }

        protected DataTable GetData(string startDate, string endDate, string paramRpt)
        {
            DataTable dt = new DataTable();
            string query = string.Empty;

            if (paramRpt == "polis individu")
            {
                query = "EXEC RPT_POLIS_INDIVIDU '" + startDate + "','" + endDate + "'";
            }
            else if (paramRpt == "polis corporate")
            {
                query = "EXEC RPT_POLIS_CORPORATE '" + startDate + "','" + endDate + "'";
            }
            else
            {
                query = "EXEC RPT_POLIS_HEALTH '" + startDate + "','" + endDate + "'";
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

        protected void ExportToExcel(DataTable dt)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            string paramRPT = Session["paramRPT"].ToString();
            var date = string.Format("{0:yyyyMMdd}.txt", DateTime.Now);
            var filenameindividu = "REPORT_POLIS_INDIVIDU_" + date;
            var filenamecorporate = "REPORT_POLIS_CORPORATE_" + date;
            var filenamehealth = "REPORT_POLIS_HEALTH_" + date;


            Response.Clear();
            Response.Buffer = true;

            if (paramRPT == "0")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filenameindividu + "");
            }
            else if (paramRPT == "1")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filenamecorporate + "");
            }
            else if (paramRPT == "2")
            {
                Response.AddHeader("content-disposition", "attachment;filename=" + filenamehealth + "");
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

            string reportType = ddreportType.SelectedValue.ToString();
            string date = string.Format("{0:yyyyMMdd}", DateTime.Now);
            string filnameDetail = "";

            if (reportType == "0")
            {
                filnameDetail = "REPORT_POLIS_INDIVIDU_" + date;
            }
            else if (reportType == "1")
            {
                filnameDetail = "REPORT_POLIS_CORPORATE_" + date;
            }
            else if (reportType == "2")
            {
                filnameDetail = "REPORT_POLIS_HEALTH_" + date;
            }

            GlobalUse.ExportDataSetToExcel(dt, this, filnameDetail, true);  
        }
    }
}