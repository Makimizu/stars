using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.IO;
using System.Web.Providers.Entities;
using System.Reflection;
using System.Data.SqlClient;
using System.Web.Services;
using OfficeOpenXml;
using HEALTH.Models;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Security.Claims;
using System.Security.Policy;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace HEALTH.Form_Klaim
{
    public partial class ProviderClinicalPathway : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LB_MODE.Text = "CREATE";
                LB_CODE.Text = Request.QueryString["CODE"].ToString();  //FULLNAME
 
                //Setup();
                Fill_DGR_CLINICAL_PATHWAY();              
                Fill_DGR_CLINICAL_PATHWAY_LOG();
            }
        }
         
        [WebMethod]
        public static List<string> SearchDiagnosa(string prefixText, int count)
        {
            List<string> results = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select TOP 10 CODE+'-'+DESCR_1 as DESCR_1 from PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL AND CODE+'-'+DESCR_1 LIKE '%'+@SearchText+'%' ORDER BY CODE+'-'+DESCR_1";
                    //"select TOP 10 CODE+' '+DESCR_1 as DESCR_1 from PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL AND CODE+' '+DESCR_1 LIKE '%'+@SearchText+'%' ORDER BY DESCR_1";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            results.Add(sdr["DESCR_1"].ToString());
                        }
                    }
                    conn.Close();

                    return results;
                }
            }
        }

        

        protected void BT_TEMPLATE_DOWNLOAD_PROV_Click(object sender, EventArgs e)
        {
 

            conn.QueryString = "SELECT [KODE_PROVIDER]      ,[NAMA_PROVIDER]      ,[KODE_ICD]    KODE_DIAGNOSA  ,[DIAGNOSA]  ,[LOS]    ,[Kelas 3]      ,[Kelas 2]      ,[Kelas 1]    ,[VIP UTAMA] as [KELAS UTAMA]   ,[VIP]      ,[VVIP]  ,[Super VIP] " +
                " FROM  [V_PROVIDER_TARIF_ICD_REPORT]    where kode_provider =  '" + LB_CODE.Text + "'";

            conn.ExecuteQuery();

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                int row = 1;
                int col = 1;

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Upload");


                worksheet.Cells[row, col++].RichText.Add("KODE_PROVIDER").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("NAMA_PROVIDER").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("KODE_DIAGNOSA").Bold = true;                
                worksheet.Cells[row, col++].RichText.Add("DIAGNOSA").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("LOS").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("Kelas 3").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("Kelas 2").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("Kelas 1").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("Kelas Utama").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("VIP").Bold = true;
                worksheet.Cells[row, col++].RichText.Add("VVIP").Bold = true;   	 
                worksheet.Cells[row, col++].RichText.Add("Super VIP").Bold = true;
                  


                worksheet.Cells[row +1, 1].Value = "" + LB_CODE.Text + "";
                 
                 

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#5DE2E7");
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    col = 1;
                    row++;
      
                    worksheet.Cells[row, col++].Value = "" + LB_CODE.Text + "";
                    worksheet.Cells[row, col++].Value = conn.GetFieldValue(i, 1).ToString();
                    worksheet.Cells[row, col++].Value = conn.GetFieldValue(i, 2).ToString();
                    worksheet.Cells[row, col++].Value = conn.GetFieldValue(i, 3).ToString();
                    worksheet.Cells[row, col++].Value = Convert.ToDouble(conn.GetFieldValue(i, 4).ToString());                    
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 5).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 6).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 7).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 8).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 9).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 10).ToString().Replace(".", "");
                    worksheet.Cells[row, col++].Value =conn.GetFieldValue(i, 11).ToString().Replace(".", "");




                    var colTotalCP = col++; 

                }

                conn.QueryString = "select TOP 1 NAMA from PROVIDER_MASTER  WHERE KODE_PROVIDER ='" + LB_CODE.Text + "'";
                conn.ExecuteQuery();


                //if ((worksheet.Cells[2, 2].Value?.ToString() ?? "") == "")
                if ((worksheet.Cells[2, 2].Value == null ? "" : worksheet.Cells[2, 2].Value.ToString()) == "")
                {
                    worksheet.Cells[2, 2].Value = conn.GetFieldValue(0, 0).ToString();
                }

                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("Content-Disposition", "attachment; filename=TemplateClinicalPathway" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.End();
            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (DataGridItem item in DGR_CLINICAL_PATHWAY.Items)
            {
                Button btDel = (Button)item.FindControl("BT_DELETE");
                if (btDel != null)
                {
                    ClientScript.RegisterForEventValidation(btDel.UniqueID, "Delete");
                }
            }
            base.Render(writer);
        }

        protected void Fill_DGR_CLINICAL_PATHWAY(string filter = null)
        { 
            conn.QueryString = "select id, [KODE_PROVIDER]      ,[NAMA_PROVIDER]      ,[KODE_ICD]  as KODE_DIAGNOSA    ,[DIAGNOSA]      ,[LOS]      ,[Kelas 3]      ,[Kelas 2]      ,[Kelas 1]   ,[VIP Utama] as [Kelas Utama] ,[VIP]      ,[VVIP]           ,[Super VIP]      ,[CREATEDATE]      ,[TGL_AKHIR_BERLAKU]  " +
              "  FROM  V_PROVIDER_TARIF_ICD_REPORT " +               
              "where   KODE_PROVIDER = '" + LB_CODE.Text + "'  AND  TGL_AKHIR_BERLAKU IS NULL ";


            if (filter != null)
            {
                conn.QueryString += filter;
            }   

            conn.QueryString += " ORDER BY  KODE_ICD ASC ";

            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CLINICAL_PATHWAY.DataSource = dt;
            DGR_CLINICAL_PATHWAY.DataBind();

            for (int i = 0; i < DGR_CLINICAL_PATHWAY.Items.Count; i++)
            {
                //Button btDel = (Button)DGR_CLINICAL_PATHWAY.Items[i].FindControl("BT_DELETE");
                //btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE?')){return false;};");


                Button btDel = (Button)DGR_CLINICAL_PATHWAY.Items[i].FindControl("BT_DELETE");

                // 🔹 Generate postback reference valid (aman dari event validation error)
                string postbackRef = ClientScript.GetPostBackEventReference(btDel, "Delete");

                // 🔹 Tambahkan SweetAlert2 confirm
                btDel.Attributes.Add("onclick", @"
                event.preventDefault();
                Swal.fire({
                title: 'Yakin ingin hapus?',
                text: 'Data yang dihapus tidak bisa dikembalikan!',
                icon: 'warning',
                position: 'center',
                showCancelButton: true,
                confirmButtonText: 'Ya, hapus!',
                cancelButtonText: 'Batal'
                }).then((result) => {
                if (result.isConfirmed) {
                " + postbackRef + @";
                }
                });
                ");


                //  var path = conn.GetFieldValue(i, 1).ToString();


                if (!string.IsNullOrEmpty(conn.GetFieldValue(i, 1).ToString()) == false)
                {
                    conn.QueryString = "delete from [PROVIDER_CLINICAL_PATHWAY]  " +
                    "where  KODE_PROVIDER= '" + LB_CODE.Text + "'  and KODE_DIAGNOSA ='" + conn.GetFieldValue(i, 1).ToString() + "'";

                    conn.ExecuteQuery();
                }

            }
        }

        protected void Fill_DGR_CLINICAL_PATHWAY_LOG(string filter = null)
        {
 
            conn.QueryString = "select id, DESCRIPTION,CREATE_DATE,KODE_PROVIDER from PROVIDER_TARIF_ICD_LOG  " +
                "where  KODE_PROVIDER= '" + LB_CODE.Text + "'";

            if (filter != null)
            {
                conn.QueryString += filter;
            }
 
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CLINICAL_PATHWAY_LOG.DataSource = dt;
            DGR_CLINICAL_PATHWAY_LOG.DataBind();            
        }

 

        protected void DGR_CLINICAL_PATHWAY_ItemCommand(object source, DataGridCommandEventArgs e)
        {             

            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
 

            if (e.CommandName == "Delete")
            {
           string icdx = e.Item.Cells[1].Text.Replace("&nbsp;", "").Trim();

                conn.QueryString = "update PROVIDER_CLINICAL_PATHWAY  SET TGL_AKHIR_BERLAKU= getdate()   " +
                   "WHERE  KODE_PROVIDER= '" + LB_CODE.Text + "'  and KODE_DIAGNOSA = '" + icdx + "'";
           
                conn.ExecuteNonQuery();
            }


            Fill_DGR_CLINICAL_PATHWAY();
        }


         protected void DGR_CLINICAL_PATHWAY_LOG_ItemCommand(object source, DataGridCommandEventArgs e)
        {
           
            int id = int.Parse(e.Item.Cells[0].Text.Replace("&nbsp;", ""));
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

            if (e.CommandName == "View")
            {                
                conn.QueryString = string.Format("select cp.ID, cp.UPLOAD_PATH from PROVIDER_CLINICAL_PATHWAY as cp where cp.ID = {0}", id);
                conn.ExecuteQuery();
                string path = conn.GetFieldValue("UPLOAD_PATH");

                string _path = Request.PhysicalApplicationPath + path;

                if (!string.IsNullOrEmpty(path) && File.Exists(_path))
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    string url = baseUrl + path;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak di temukan');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "swal","Swal.fire('Error', 'Dokumen tidak ditemukan', 'error');", true);


                }

            }

            

            Fill_DGR_CLINICAL_PATHWAY();
        }

        protected void DGR_CLINICAL_PATHWAY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSwal", "showLoading();", true);
            DGR_CLINICAL_PATHWAY.CurrentPageIndex = e.NewPageIndex;

            Fill_DGR_CLINICAL_PATHWAY();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSwal", "hideLoading();", true);
        }

        private string UploadFile(HttpPostedFile userPostedFile)
        {
            string filename;
            string basePath = "Upload/ClinicalPathway";
            string _path = Request.PhysicalApplicationPath + basePath;

            System.IO.Directory.CreateDirectory(_path);

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                string _fullpath = Request.PhysicalApplicationPath + basePath + "/" + filename;

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);

                userPostedFile.SaveAs(_fullpath);

                return basePath + "/" + filename;
            }
            return string.Empty;
        }

         
        private string UploadFileProv(HttpPostedFile userPostedFile)
        {
            string filename;
            string basePath = "Upload/ClinicalPathwayProvider";
            string _path = Request.PhysicalApplicationPath + basePath;

            System.IO.Directory.CreateDirectory(_path);

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                string _fullpath = Request.PhysicalApplicationPath + basePath + "/" + filename;

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);

                userPostedFile.SaveAs(_fullpath);

                return basePath + "/" + filename;
            }
            return string.Empty;
        }
      
        private void ProcessFileExcelProv(string fullpath, string filename)
        {
            FileInfo file = new FileInfo(fullpath);
            bool hasDuplicate = false;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.FirstOrDefault();                
                if (ws == null || ws.Dimension == null) return;

                int rowCount = ws.Dimension.End.Row;
                List<TemplateProviderClinicalPathwayProv> dataTemplates = new List<TemplateProviderClinicalPathwayProv>();
                HashSet<string> duplicateCheck = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                string kodeProvider = LB_CODE.Text.Replace("'", "''");
      
                conn.QueryString = string.Format("DELETE FROM PROVIDER_TARIF_ICD_LOG WHERE KODE_PROVIDER = '{0}'", kodeProvider);

                conn.ExecuteQuery();

                for (int i = 2; i <= rowCount; i++)
                {
                    var cells = ws.Cells;
                    var row = cells[i, 1, i, 12];
        
                    string kodeDiagnosa = cells[i, 3].Text == null ? "" : cells[i, 3].Text.Trim();
                    if (string.IsNullOrEmpty(kodeDiagnosa)) continue;
 
                    var data = new TemplateProviderClinicalPathwayProv
                    {
                      
                        KODE_PROVIDER = cells[i, 1].Text == null ? string.Empty : cells[i, 1].Text.Trim(), 
                        NAMA_PROVIDER = cells[i, 2].Text == null ? string.Empty : cells[i, 2].Text.Trim(),                        
                        KODE_DIAGNOSA = kodeDiagnosa,                    
                        DIAGNOSA = cells[i, 4].Text == null ? string.Empty : cells[i, 4].Text.Trim(),
                        KODE_TARIF = "KMR",
                        LOS = TryParseIntWithLogging(cells[i, 5].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        KELAS_3 = TryParseIntWithLogging(cells[i, 6].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        KELAS_2 = TryParseIntWithLogging(cells[i, 7].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        KELAS_1 = TryParseIntWithLogging(cells[i, 8].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        KELAS_UTAMA = TryParseIntWithLogging(cells[i, 9].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        VIP = TryParseIntWithLogging(cells[i, 10].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        VVIP = TryParseIntWithLogging(cells[i, 11].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                        SUPER_VIP = TryParseIntWithLogging(cells[i, 12].Text, i, kodeDiagnosa, kodeProvider, conn, ref hasDuplicate),
                    };

                    dataTemplates.Add(data);
                }

                if (!hasDuplicate && dataTemplates.Count > 0)
                {
                    BulkInsertClinicalPathwayProv(dataTemplates);
                }
            }
        }

       

        private int TryParseIntWithLogging(string value, int rowIndex, string kodeDiagnosa, string kodeProvider, Connection conn, ref bool hasDuplicate)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }

            hasDuplicate = LogDuplicateAndContinue(rowIndex, kodeDiagnosa, kodeProvider, conn);
            return 0;
        }

        private bool LogDuplicateAndContinue(int rowIndex, string kodeDiagnosa, string kodeProvider, Connection conn)
        {
            string message = string.Format("Row {rowIndex}: {kodeDiagnosa} duplicate or not found or any error!");
            string sanitizedProvider = kodeProvider.Replace("'", "''");

            conn.QueryString = string.Format("INSERT INTO PROVIDER_TARIF_ICD_LOG (KODE_PROVIDER, DESCRIPTION) VALUES ('{sanitizedProvider}', '{message}')");
            conn.ExecuteQuery(15000);

            return true; // Menandakan bahwa duplikat ditemukan
        }
         
        private bool BulkInsertClinicalPathway(List<TemplateProviderClinicalPathway> models)
        {

            string request = JsonConvert.SerializeObject(models);
            conn.QueryString = "exec SP_PROVIDER_CLINICALPATHWAY_BULK_UPSERT N'" + request + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(150000);

            return true;
        }

        private bool BulkInsertClinicalPathwayProv(List<TemplateProviderClinicalPathwayProv> models)
        {

            string request = JsonConvert.SerializeObject(models);
            conn.QueryString = "exec SP_PROVIDER_CLINICALPATHWAY_ICD_BULK_UPSERT N'" + request + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery(150000);

            return true;
        }

       

        protected void BT_GENERETE_PROV_Click(object sender, EventArgs e)
        {
            HttpPostedFile fileExcel = Request.Files["TXT_FILE_UPLOAD_BULK_PROV"];

            if (fileExcel.ContentLength == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen template tidak boleh kosong');", true);

                ClientScript.RegisterStartupScript(this.GetType(), "swal", "Swal.fire('Error', 'Dokumen template tidak boleh kosong', 'error');", true);

                return;
            }
 
            string pathExcel = UploadFile(fileExcel);

            string fullPath = Request.PhysicalApplicationPath + pathExcel;
            ProcessFileExcelProv(fullPath, Path.GetFileName(pathExcel));
            Fill_DGR_CLINICAL_PATHWAY();
            Fill_DGR_CLINICAL_PATHWAY_LOG();
        }

       
    }
}