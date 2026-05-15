using System;
using System.IO;
using DMS.DBConnection;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Reporting.WebForms;

namespace AGR.Form_Agent
{
    public partial class AGENT_SCREENING : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "startLoading", "startDotLoading();", true);
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                conn.QueryString = "select CODE, DESCR from [SECURITY].[dbo].[PR_EXPORT_FORMAT] where CODE = 'XLS'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_FILETYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }
                
            }

        }

        protected void FillDGR()
        {
            if(ViewState["DataTable"] == null)
            {
                return;
            }

            LB_RECORD.Text = "";

            DataTable dta = (DataTable)ViewState["DataTable"];

            string columnName0 = dta.Columns[0].ColumnName;
            string columnName1 = dta.Columns[1].ColumnName;
            string filterExpression = "";


            if (!string.IsNullOrEmpty(TXT_NAME.Text) && !string.IsNullOrWhiteSpace(TXT_NAME.Text))
            {
                string name = TXT_NAME.Text.Trim();

                if (filterExpression != "")
                {
                    filterExpression += " AND ";
                    filterExpression += string.Format("[{0}] LIKE '%{1}%'", columnName0, name);
                    //filterExpression += $"[{columnName0}] LIKE '%{name}%'";
                }
                else
                {
                    filterExpression += string.Format("[{0}] LIKE '%{1}%'", columnName0, name);
                    //filterExpression = $"[{columnName0}] LIKE '%{name}%'";
                }
            }

            if (!string.IsNullOrEmpty(TXT_DATE_START.Text) && !string.IsNullOrWhiteSpace(TXT_DATE_START.Text))
            {
                DateTime resultUS;
                DateTime resultGB;

                bool isDateUS = DateTime.TryParse(TXT_DATE_START.Text.Trim(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                bool isDateGB = DateTime.TryParse(TXT_DATE_START.Text.Trim(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                if (isDateGB)
                {
                    if (filterExpression != "")
                    {
                        filterExpression += " AND ";
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultGB);
                        //filterExpression += $"[{columnName1}] >= '#{resultGB:yyyy-MM-dd}#'";
                    }
                    else
                    {
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultGB);
                        //filterExpression = $"[{columnName1}] >= '#{resultGB:yyyy-MM-dd}#'";
                    }
                }
                else if (isDateUS)
                {
                    if (filterExpression != "")
                    {
                        filterExpression += " AND ";
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultUS);
                        //filterExpression += $"[{columnName1}] >= '#{resultUS:yyyy-MM-dd}#'";
                    }
                    else
                    {
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultUS);
                        //filterExpression = $@"[{columnName1}] >= '#{resultUS:yyyy-MM-dd}#'";
                    }
                }
            }

            if (!string.IsNullOrEmpty(TXT_DATE_END.Text) && !string.IsNullOrWhiteSpace(TXT_DATE_END.Text))
            {
                DateTime resultUS;
                DateTime resultGB;

                bool isDateUS = DateTime.TryParse(TXT_DATE_END.Text.Trim(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                bool isDateGB = DateTime.TryParse(TXT_DATE_END.Text.Trim(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                if (isDateGB)
                {
                    if (filterExpression != "")
                    {
                        filterExpression += " AND ";
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultGB);
                        //filterExpression += $@"[{columnName1}] <= '#{resultGB:yyyy-MM-dd}#'";
                    }
                    else
                    {
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultGB);
                        //filterExpression = $@"[{columnName1}] <= '#{resultGB:yyyy-MM-dd}#'";
                    }
                }
                else if (isDateUS)
                {
                    if (filterExpression != "")
                    {
                        filterExpression += " AND ";
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultUS);
                        //filterExpression += $@"[{columnName1}] <= '#{resultUS:yyyy-MM-dd}#'";
                    }
                    else
                    {
                        filterExpression += string.Format("[{0}] >= '#{1:yyyy-MM-dd}#'", columnName1, resultUS);
                        //filterExpression = $@"[{columnName1}] <= '#{resultUS:yyyy-MM-dd}#'";
                    }
                }
            }

            if (!string.IsNullOrEmpty(DDL_FLAG.SelectedValue) && !string.IsNullOrWhiteSpace(DDL_FLAG.SelectedValue))
            {
                string flag = DDL_FLAG.SelectedValue.Trim();

                if (filterExpression != "")
                {
                    filterExpression += " AND ";
                    filterExpression += string.Format("[FLAG] = '%{0}%'", flag);
                    //filterExpression += $@"[FLAG] = '%{flag}%'";
                }
                else
                {
                    filterExpression += string.Format("[FLAG] = '%{0}%'", flag);
                    //filterExpression = $@"[FLAG] = '%{flag}%'";
                }
            }
            
            if (filterExpression != "")
            {
                DataRow[] result = dta.Select(filterExpression);

                if (result.Length > 0)
                {
                    DataTable filteredTable = result.CopyToDataTable();
                    DGR.DataSource = filteredTable;
                    DGR.DataBind();

                    LB_RECORD.Text = filteredTable.Rows.Count.ToString() + " records";
                }
                else
                {
                    DataTable emptyTable = dta.Clone();
                    DGR.DataSource = emptyTable;
                    DGR.DataBind();

                    LB_RECORD.Text = "0 records";
                }
            }
            else
            {
                DGR.DataSource = dta;
                DGR.DataBind();
                LB_RECORD.Text = dta.Rows.Count.ToString() + " records";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SHOW_Click(object sender, EventArgs e)
        {
            TXT_NAME.Text = "";
            TXT_DATE_START.Text = "";
            TXT_DATE_END.Text = "";
            DDL_FLAG.SelectedIndex = 0;
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            //DataTable dta = (DataTable)ViewState["DataTable"];


            //ReportViewer1.Visible = false;
            //ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("RPT_AGENTS.rdlc");

            //Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dta);
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.Refresh();

            //byte[] bytes = ReportViewer1.LocalReport.Render("PDF");

            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment; filename=report.pdf");
            //Response.BinaryWrite(bytes);
            //Response.End();

            if(DDL_FILETYPE.SelectedValue == "XLS")
            {
                DateTime dt = DateTime.UtcNow;
                DateTime utc = dt.ToUniversalTime();
                long unixTime = (long)(utc - new DateTime(1970, 1, 1)).TotalSeconds;
                //long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();
                string filename = "agent_screening_" + unixTime.ToString() + ".xlsx";

                byte[] bytes = ReportViewer1.LocalReport.Render("EXCELOPENXML");

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename="+filename);
                Response.BinaryWrite(bytes);
                Response.End();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //if (e.CommandName == "Select")
            //{
            //    Response.Redirect("BlackListDetail.aspx?ID=" + e.Item.Cells[0].Text);
            //}

            //if (e.CommandName == "Delete")
            //{
            //    try
            //    {
            //        conn.QueryString = "delete from [CLIENT_BASE].[dbo].[MEMBER_MASTER_BLACKLIST] where ID = '" + e.Item.Cells[0].Text + "'";
            //        conn.ExecuteNonQuery();
            //        FillDGR();
            //    }
            //    catch { }
            //}
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.SQLToFile("template_upload_screening_agent.xlsx",
                                    "select top 1 THEFILE from [ARCHIEVE].[dbo].[AGR_ARSIP] where NAMAFILE='template_upload_screening_agent.xlsx' and OWNER1 = 'SYSTEM'",
                                    Page);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            DDL_FILETYPE.Visible = false;
            BT_DOWNLOAD.Visible = false;
            LB_EXPORT.Visible = false;
            DGR.DataSource = null;
            DGR.DataBind();

            LB_ERR.Text = "";
            
            if (!FU.HasFile)
                return;

            conn.QueryString = "select VALUE from [SECURITY].[dbo].[SC_GENERAL_SET] where APP_CODE = '0' and PARAMETER = 'UPLOAD_FOLDER'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DateTime dt = DateTime.UtcNow;
            DateTime utc = dt.ToUniversalTime();
            long filename = (long)(utc - new DateTime(1970, 1, 1)).TotalSeconds;
            //long filename = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string extension = Path.GetExtension(FU.FileName);
            //string filename = GlobalUse.RemoveParentheses(Path.GetFileName(FU.FileName));
            //string fullpath = conn.GetFieldValue(0, 0).ToString() + "\\" + Session["s"] + filename;
            string fullpath = conn.GetFieldValue(0, 0).ToString() + "\\" + filename + extension;

            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            Process(fullpath);

            //FU.FileContent.Dispose();

            //FillDGR();

            
            ClientScript.RegisterStartupScript(this.GetType(), "hideLoading", "hideLoading();", true);
        }

        protected void Process(string FullPath)
        {
            //string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            string fileExtension = System.IO.Path.GetExtension(FullPath);

            string connstr = "";

            string folderPath = Path.GetDirectoryName(FullPath);
            string fileName = Path.GetFileName(FullPath);

            int records = 0;
            int minNumCol = 3;
            //int numColToBeScanned = 0;
            

            DataTable dta = new DataTable();
            dta.Columns.Add("AGENTNAME");
            dta.Columns.Add("DOB", typeof(DateTime));
            dta.Columns.Add("POB");
            dta.Columns.Add("GENDER");
            dta.Columns.Add("JOB");
            dta.Columns.Add("RELIGION");
            dta.Columns.Add("MARITALSTATUS");
            dta.Columns.Add("IDTYPE");
            dta.Columns.Add("IDNO");
            dta.Columns.Add("TAXNO");
            dta.Columns.Add("CITIZENSHIP");
            dta.Columns.Add("PHONE");
            dta.Columns.Add("EMAIL");
            dta.Columns.Add("ADDRESS");
            dta.Columns.Add("CITY");
            dta.Columns.Add("PROVINCE");
            dta.Columns.Add("COUNTRY");
            dta.Columns.Add("ZIPCODE");
            dta.Columns.Add("EDUCATION");
            dta.Columns.Add("FLAG");
            dta.Columns.Add("SOURCE");
            dta.Columns.Add("REMARK");

            if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                
                try
                {
                    using (StreamReader reader = new StreamReader(FullPath))
                    {
                        string line = reader.ReadLine();

                        while (!reader.EndOfStream)
                        {
                            
                            line = reader.ReadLine();

                            if (string.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            using (TextFieldParser parser = new TextFieldParser(new StringReader(line)))
                            {
                                parser.HasFieldsEnclosedInQuotes = true;
                                parser.SetDelimiters(",");

                                string[] fields = parser.ReadFields();
                                

                                if(fields.Length < minNumCol)
                                {
                                    parser.SetDelimiters(";");
                                    fields = parser.ReadFields();

                                    if (fields.Length < minNumCol)
                                    {
                                        continue;
                                    }
                                }


                                DataRow newRow = dta.NewRow();

                                string fullname = "", dob = "";

                                for (int i = 0; i < dta.Columns.Count - 3; i++)
                                {
                                    if (i == 0)
                                    {
                                        fullname = fields[i].ToString();
                                    }

                                    if (i == 1)
                                    {
                                        DateTime resultUS;
                                        DateTime resultGB;

                                        bool isDateUS = DateTime.TryParse(fields[i].ToString(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                                        bool isDateGB = DateTime.TryParse(fields[i].ToString(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                                        if (isDateGB)
                                        {
                                            dob = resultGB.ToString("yyyy-MM-dd");
                                        }
                                        else if (isDateUS)
                                        {
                                            dob = resultUS.ToString("yyyy-MM-dd");
                                        } 
                                    }

                                    if (i < fields.Length)
                                    {
                                        newRow[i] = fields[i];
                                    }
                                    else
                                    {
                                        if(i == 1)
                                        {
                                            newRow[i] = DBNull.Value;
                                        }
                                        else
                                        {
                                            newRow[i] = "";
                                        }
                                        
                                    }

                                }

                                conn.QueryString = "SELECT FLAG, SOURCE, REMARK " +
                                                        "FROM  [MARKETING].[dbo].[UFN_GET_AGENT_BLACKLIST_INFO] " +
                                                        "( '" + fullname + "', '" + dob + "')";

                                conn.ExecuteQuery();

                                newRow[dta.Columns.Count - 3] = conn.GetFieldValue(0, 0).ToString();
                                newRow[dta.Columns.Count - 2] = conn.GetFieldValue(0, 1).ToString();
                                newRow[dta.Columns.Count - 1] = conn.GetFieldValue(0, 2).ToString();

                                dta.Rows.Add(newRow);
                                records++;

                            }

                        }
                    }

                    LB_RECORD.Text = records + " records";

                    ViewState["DataTable"] = dta;
                    
                    ReportViewer1.Visible = false;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reports/RPT_AGENTS.rdlc");

                    Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dta);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.Refresh();

                    DGR.DataSource = dta;
                    DGR.DataBind();

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);

                    ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);


                    DDL_FILETYPE.Visible = true;
                    BT_DOWNLOAD.Visible = true;
                    LB_EXPORT.Visible = true;

                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);
                }
            }
            else
            {
                if (fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    connstr = @"Provider=Microsoft.ACE.OLEDB.12.0; 
                    Data Source=" + FullPath + @"; 
                    Extended Properties=""Excel 8.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text"";";
                }
                else if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;
                    Data Source=" + FullPath + @";
                    Extended Properties=""Excel 12.0 Xml;IMEX=1;HDR=Yes;"";";
                }
                else 
                {
                    LB_ERR.Text = "Ekstensi " + fileExtension + " tidak didukung";
                    return;
                }

                using (OleDbConnection con = new OleDbConnection(connstr))
                {
                    try
                    {
                        con.Open();

                        DataTable schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        if (schemaTable != null && schemaTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in schemaTable.Rows)
                            {
                                string sheetName = row["TABLE_NAME"].ToString();
                                // Remove single quotes if present
                                if (sheetName.StartsWith("'") && sheetName.EndsWith("'"))
                                {
                                    sheetName = sheetName.Substring(1, sheetName.Length - 2);
                                }

                                // Optional: Skip hidden/system sheets
                                if (!sheetName.EndsWith("$") && !sheetName.EndsWith("$'"))
                                    continue;

                                using (OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetName + "]", con))
                                {

                                    DataTable dt = new DataTable();
                                    da.Fill(dt);

                                    foreach (DataRow myRow in dt.Rows)
                                    {
                                        string fullname = "", dob = "";

                                        DataRow newRow = dta.NewRow();
                                        
                                        for (int i = 0; i < dta.Columns.Count - 3; i++)
                                        {
                                            if (i == 0)
                                            {
                                                fullname = myRow[i].ToString();
                                            }

                                            if (i == 1)
                                            {
                                                DateTime resultUS;
                                                DateTime resultGB;

                                                bool isDateUS = DateTime.TryParse(myRow[i].ToString(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                                                bool isDateGB = DateTime.TryParse(myRow[i].ToString(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                                                if (isDateUS)
                                                {
                                                    dob = resultUS.ToString("yyyy-MM-dd");
                                                    newRow[i] = resultUS.Date;
                                                }
                                                else if (isDateGB)
                                                {
                                                    dob = resultGB.ToString("yyyy-MM-dd");
                                                    newRow[i] = resultGB.Date;
                                                }
                                                else
                                                {
                                                    newRow[i] = myRow[i];
                                                }
                                            }
                                            else
                                            {
                                                newRow[i] = myRow[i];
                                            }

                                        }

                                        conn.QueryString = "SELECT FLAG, SOURCE, REMARK " +
                                                                "FROM  [MARKETING].[dbo].[UFN_GET_AGENT_BLACKLIST_INFO] " +
                                                                "( '" + fullname + "', '" + dob + "')";

                                        conn.ExecuteQuery();

                                        newRow[dta.Columns.Count - 3] = conn.GetFieldValue(0, 0).ToString();
                                        newRow[dta.Columns.Count - 2] = conn.GetFieldValue(0, 1).ToString();
                                        newRow[dta.Columns.Count - 1] = conn.GetFieldValue(0, 2).ToString();

                                        dta.Rows.Add(newRow);
                                        records++;

                                    }
                                }
                            }
                        }

                        con.Close();

                        LB_RECORD.Text = records + " records";

                        ReportViewer1.Visible = false;
                        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reports/RPT_AGENTS.rdlc");

                        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dta);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        ReportViewer1.LocalReport.Refresh();

                        DGR.DataSource = dta;
                        DGR.DataBind();

                        ViewState["DataTable"] = dta;

                        if (File.Exists(FullPath))
                            File.Delete(FullPath);

                        ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);

                        DDL_FILETYPE.Visible = true;
                        BT_DOWNLOAD.Visible = true;
                        LB_EXPORT.Visible = true;
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = ex.Message;
                        con.Close();
                        if (File.Exists(FullPath))
                            File.Delete(FullPath);
                    }
                }
                    
            }
            
        }
    }
}