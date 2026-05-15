using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.IO;
using System.Data.OleDb;
using System.EnterpriseServices;
using OfficeOpenXml;
using HEALTH.Models;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace HEALTH.Form_Klaim
{
    public partial class ProviderTarif : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_TRF_TGLBERLAKU.Text = GlobalUse.GlobalDateFormat(DateTime.Now.ToString(), "d/M/yyyy");
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                FillDGR_Tarif();
                FillDGR_TarifDocument();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TARIF_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRF_KODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_PROVIDER_KELAS_KAMAR where CODE in ('033', '029', '019', '052', '057', '066', '056', '007','012') ORDER BY DESCR";
            conn.ExecuteQuery();
            DDL_TRF_KAMAR.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRF_KAMAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            FillDokumen();
            FillDDL_SubTarif();
        }

        protected void FillDokumen()
        {
            conn.QueryString = "SELECT TOP 1 PATH_DOKUMEN FROM PROVIDER_TARIF_DOKUMEN WHERE IS_DELETED = 0 AND KODE_PROVIDER = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            string path = conn.GetFieldValue("PATH_DOKUMEN");
            //LBL_TITLE_DOCUMENT.Text = path;

            //TXT_FILE_UPLOAD.Visible = string.IsNullOrEmpty(LBL_TITLE_DOCUMENT.Text);
            //BT_TRF_UPLOAD.Visible = string.IsNullOrEmpty(LBL_TITLE_DOCUMENT.Text);

            //BT_VIEW.Visible = !string.IsNullOrEmpty(LBL_TITLE_DOCUMENT.Text);
            //BT_DELETE.Visible = !string.IsNullOrEmpty(LBL_TITLE_DOCUMENT.Text);

            string _path = Request.PhysicalApplicationPath + path;

            //if (!string.IsNullOrEmpty(path) && File.Exists(_path))
            //{
            //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            //    string url = baseUrl + path;

            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak di temukan');", true);
            //}
        }

        protected void FillDDL_SubTarif()
        {
            conn.QueryString = "select KODE_SUB_TARIF,DESCR from PARAM_TBL_PROVIDER_SUB_TARIF where KODE_TARIF='" + DDL_TRF_KODE.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_TRF_SUB.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRF_SUB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_TRF_KODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_SubTarif();
            FillDGR_Tarif();
        }

        protected void FillDGR_Tarif()
        {
            /*
            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.KODE_TARIF, " +
                                "KODE_TARIF_DESCR = b.DESCR, " +
                                "TGL_BERLAKU = convert(varchar(20),a.TGL_BERLAKU,106), " +
                                "TGL_AKHIR_BERLAKU = convert(varchar(20),a.TGL_AKHIR_BERLAKU,106), " +
                                "a.KELAS_KAMAR, " +
                                "a.DESCR, " +
                                "TARIF = replace(convert(varchar(100),convert(money,a.TARIF),1),'.00',''), " +
                                "a.TARIF_P " +
                                "from PROVIDER_TARIF a " +
                                "inner join PR_TARIF_PROVIDER b on a.KODE_TARIF=b.CODE " +
                                "where " +
                                "KODE_PROVIDER='" +LB_CODE.Text+ "' " +
                                "order by  " +
                                "b.DESCR, " +
                                "a.TGL_BERLAKU desc";
            */
            conn.QueryString = "exec SP_CLM_PROVIDER_TARIF '" + LB_CODE.Text + "','" + DDL_TRF_KODE.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TARIF.DataSource = dt;
            DGR_TARIF.DataBind();


            for (int i = 0; i < DGR_TARIF.Items.Count; i++)
            {

                /*
                DropDownList ddlTGL = (DropDownList)DGR_TARIF.Items[i].FindControl("DDL_TARIF_TGL");            
                */
                TextBox txtKET = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_KET");
                TextBox txtTARIF = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_RP");
                TextBox txtTGL = (TextBox)DGR_TARIF.Items[i].FindControl("TXT_TRF_TGL");
                Button btDel = (Button)DGR_TARIF.Items[i].FindControl("BT_DELETE");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                txtKET.Text = DGR_TARIF.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                txtTARIF.Text = DGR_TARIF.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                txtTGL.Text = DGR_TARIF.Items[i].Cells[6].Text.Replace("&nbsp;", "");

                /*
                conn.QueryString = "select distinct " +
                                    "convert(varchar(20),TGL_BERLAKU,112), " +
                                    "convert(varchar(20),TGL_BERLAKU,106) " +
                                    "from PROVIDER_TARIF a " +
                                    "where " +
                                    "KODE_PROVIDER='" +LB_CODE.Text+ "' " +
                                    "and isnull(KODE_TARIF,'') = '" + DGR_TARIF.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "' " +
                                    "and isnull(KELAS_KAMAR,'') = '" + DGR_TARIF.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "' " +
                                    "and isnull(DESCR,'') = '" + DGR_TARIF.Items[i].Cells[4].Text.Replace("&nbsp;","") + "' " +
                                    "order by " +
                                    "1 desc";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTGL.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddlTGL.SelectedValue = DGR_TARIF.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
                catch { }

                txtTARIF.Text = DGR_TARIF.Items[i].Cells[6].Text;
                */
            }

        }

        protected void FillDGR_TarifDocument()
        {

            conn.QueryString = "SELECT ID, PATH_DOKUMEN AS [PATH] FROM PROVIDER_TARIF_DOKUMEN WHERE IS_DELETED = 0 AND KODE_PROVIDER = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TARIF_DOCUMENT.DataSource = dt;
            DGR_TARIF_DOCUMENT.DataBind();


            for (int i = 0; i < DGR_TARIF_DOCUMENT.Items.Count; i++)
            {
                Button btDel = (Button)DGR_TARIF_DOCUMENT.Items[i].FindControl("BT_DELETE");
                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE?')){return false;};");
            }
        }

        protected void BT_TEMPLATE_DOWNLOAD_Click(object sender, EventArgs e)
        {
            string path = "Templates/TemplateBukuTarif.xlsx";
            if (!string.IsNullOrEmpty(path) && File.Exists(Request.PhysicalApplicationPath + path))
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                string url = baseUrl + path;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak di temukan');", true);
            }

        }

        protected void BT_TRF_UPLOAD_TEMPLATE_Click(object sender, EventArgs e)
        {
            try
            {
                HttpPostedFile userPostedFile = Request.Files["TXT_FILE_UPLOAD_TEMPLATE"];

                if (userPostedFile.ContentLength == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen template tidak boleh kosong');", true);
                    return;
                }


                string path = UploadFile(userPostedFile);
                string fullPath = Request.PhysicalApplicationPath + path;

                ProcessFile(fullPath, Path.GetFileName(path));

                if (File.Exists(fullPath))
                    File.Delete(fullPath);


                //conn.QueryString = "INSERT INTO PROVIDER_TARIF_DOKUMEN " +
                //    "(ID, KODE_PROVIDER, PATH_DOKUMEN, IS_DELETED,CREATEBY,CREATEDATE,LASTCHANGEBY,LASTCHANGEDATE) " +
                //    "VALUES " +
                //    "(NEWID(), '" + LB_CODE.Text + "','" + path + "',0,'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE(),'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE());";
                //conn.ExecuteNonQuery();

                //FillDGR_TarifDocument();

                //ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen berhasil di simpan');", true);
            }
            catch (System.Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('" + ex.Message + "');", true);
                return;
            }
        }


        protected void DGR_TARIF_DOCUMENT_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            string id = e.Item.Cells[0].Text.Replace("&nbsp;", "");
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");


            //conn.QueryString = $"SELECT ID, PATH_DOKUMEN AS [PATH] FROM PROVIDER_TARIF_DOKUMEN WHERE IS_DELETED = 0 AND ID = '{id}'";
            conn.QueryString = string.Format("SELECT ID, PATH_DOKUMEN AS [PATH] FROM PROVIDER_TARIF_DOKUMEN WHERE IS_DELETED = 0 AND ID = '{0}'",id);

            conn.ExecuteQuery();
            string path = conn.GetFieldValue("PATH");

            if (e.CommandName == "View")
            {

                string _path = Request.PhysicalApplicationPath + path;

                if (!string.IsNullOrEmpty(path) && File.Exists(_path))
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    string url = baseUrl + path;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak di temukan');", true);
                }
            }

            if (e.CommandName == "Delete")
            {

                string _path = Request.PhysicalApplicationPath + path;

                if (!string.IsNullOrEmpty(path) && File.Exists(_path))
                {
                    File.Delete(_path);
                }


                //conn.QueryString = $"UPDATE PROVIDER_TARIF_DOKUMEN SET IS_DELETED = 1, LASTCHANGEDATE = GETDATE(), LASTCHANGEBY = '{user}' WHERE ID='{id}'";
                conn.QueryString = string.Format("UPDATE PROVIDER_TARIF_DOKUMEN SET IS_DELETED = 1, LASTCHANGEDATE = GETDATE(), LASTCHANGEBY = '{0}' WHERE ID='{1}'",user, id);

                conn.ExecuteNonQuery();
                FillDGR_TarifDocument();
            }

        }

        protected void BT_TRF_VIEW_Click(object sender, EventArgs e)
        {
            //string path = LBL_TITLE_DOCUMENT.Text;
            //string _path = Request.PhysicalApplicationPath + path;

            //if (!string.IsNullOrEmpty(path) && File.Exists(_path))
            //{
            //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            //    string url = baseUrl + path;

            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak di temukan');", true);
            //}
        }


        protected void BT_TRF_DELETE_Click(object sender, EventArgs e)
        {
            //string path = LBL_TITLE_DOCUMENT.Text;

            //string _path = Request.PhysicalApplicationPath + path;

            //if (!string.IsNullOrEmpty(path) && File.Exists(_path))
            //{
            //    File.Delete(_path);
            //}

            //conn.QueryString = "UPDATE a SET IS_DELETED = 1 FROM PROVIDER_TARIF_DOKUMEN as a WHERE IS_DELETED = 0 AND PATH_DOKUMEN = '" + LBL_TITLE_DOCUMENT.Text + "' AND KODE_PROVIDER = '" + LB_CODE.Text + "'";
            //conn.ExecuteQuery();

            //FillDokumen();

            //ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen berhasil di hapus');", true);
        }

        protected void BT_TRF_UPLOAD_Click(object sender, EventArgs e)
        {
            try
            {
                HttpPostedFile userPostedFile = Request.Files["TXT_FILE_UPLOAD_BUKU_TARIF"];

                if (userPostedFile.ContentLength == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen tidak boleh kosong');", true);
                    return;
                }


                string path = UploadFile(userPostedFile);

                conn.QueryString = "INSERT INTO PROVIDER_TARIF_DOKUMEN " +
                    "(ID, KODE_PROVIDER, PATH_DOKUMEN, IS_DELETED,CREATEBY,CREATEDATE,LASTCHANGEBY,LASTCHANGEDATE) " +
                    "VALUES " +
                    "(NEWID(), '" + LB_CODE.Text + "','" + path + "',0,'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE(),'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE());";
                conn.ExecuteNonQuery();

                FillDGR_TarifDocument();

                ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen berhasil di simpan');", true);
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('" + error + "');", true);
            }
        }

        private void ProcessFile(string fullpath, string filename)
        {
            FileInfo file = new FileInfo(fullpath);
            bool hasDuplicate = false;

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.First();
                var rowCount = ws.Dimension.End.Row;
                List<TemplateProviderTarif> dataTemplates = new List<TemplateProviderTarif>();

                DateTime now = DateTime.Now;

                var duplicateCheck = new HashSet<string>();
                 

                for (int i = 2; i <= rowCount; i++)
                {
                    TemplateProviderTarif data = new TemplateProviderTarif();

                    data.KODE_PROVIDER = LB_CODE.Text;
                    data.JENIS_TARIF = ws.Cells[i, 1].Value.ToString();
                    data.KELAS_KAMAR = ws.Cells[i, 2].Value.ToString();
                    data.KODE_SUB_TARIF = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString();
                    //    data.KODE_SUB_TARIF = ws.Cells[i, 3].Value == null ? string.Empty : ws.Cells[i, 3].Value.ToString();
                    data.KELAS_KAMAR= ws.Cells[i, 2].Value.ToString();

                    conn.QueryString = "select TOP 1  KODE_SUB_TARIF from PARAM_TBL_PROVIDER_SUB_TARIF   WHERE DESCR ='" + ws.Cells[i, 3].Value.ToString() + "'";
                    conn.ExecuteQuery();
                    
                    for (int a = 0; a < conn.GetRowCount(); a++)
                    {
                        data.KODE_SUB_TARIF =conn.GetFieldValue(a, 0).ToString();
                    }
                    data.KETERANGAN = ws.Cells[i, 4].Value == null ? string.Empty : ws.Cells[i, 4].Value.ToString();
                   // data.TARIF = Decimal.Parse(ws.Cells[i, 5].Value.ToString());      
                    data.TANGGAL_BERLAKU = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    /*string tarifRaw = ws.Cells[i, 5]?.Value?.ToString()?.Trim();
                    if (decimal.TryParse(tarifRaw, out decimal tarif))
                    {
                        data.TARIF = tarif;
                    }
                    else
                    {
                        data.TARIF = 0;
                    }*/
                    object cellValue = ws.Cells[i, 5] != null ? ws.Cells[i, 5].Value : null;
                    string tarifRaw = cellValue != null ? cellValue.ToString().Trim() : string.Empty;

                    decimal tarif;
                    if (decimal.TryParse(tarifRaw, out tarif))
                    {
                        data.TARIF = tarif;
                    }
                    else
                    {
                        data.TARIF = 0;
                    }


                    // Cek duplikat
                    //string key = $"{data.JENIS_TARIF}|{data.KELAS_KAMAR}|{data.KODE_SUB_TARIF}";
                    string key = string.Format("{0}|{1}|{2}", data.JENIS_TARIF, data.KELAS_KAMAR, data.KODE_SUB_TARIF);
                    if (!duplicateCheck.Add(key))
                    {
                        hasDuplicate = true;

                        //string alertScript = $"<script>alert('Duplikat ditemukan di baris {i}: {key}');</script>";
                        string alertScript = "<script>alert('Duplikat ditemukan di baris "+i+": "+key+"');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", alertScript);

                        return;
                    }



                    dataTemplates.Add(data);
                }

                string request = JsonConvert.SerializeObject(dataTemplates);

                conn.QueryString = "exec SP_PROVIDER_TARIF_BULK_UPSERT N'" + request + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteQuery();

            }
            if  ( hasDuplicate != true)
            {
                FillDGR_Tarif();
                ClientScript.RegisterStartupScript(this.GetType(), "alter", "alert('Dokumen berhasil di simpan');", true);
            }
        }


        protected void BT_TRF_SAVE_Click(object sender, EventArgs e)
        {
            string kelaskamar = "null";
            string subtarif = "null";
            if (DDL_TRF_KAMAR.SelectedValue != "")
                kelaskamar = "'" + DDL_TRF_KAMAR.SelectedValue + "'";
            if (DDL_TRF_SUB.SelectedValue != "")
                subtarif = "'" + DDL_TRF_SUB.SelectedValue + "'";

            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_TARIF_INSERT " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_TRF_KODE.SelectedValue + "'," +
                                    kelaskamar + "," +
                                    subtarif + "," +
                                    "'" + TXT_TRF_DESCR.Text.Trim().Replace("'", "") + "'," +
                                    "'" + TXT_TRF_AMOUNT.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TRF_TGLBERLAKU.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TRF_TGLBERLAKU.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' ";
                //"'" + path + "'";
                conn.ExecuteNonQuery();

                FillDGR_Tarif();
            }
            catch (System.Exception ex)
            {
                string error = ex.Message;
            }
        }

        private string UploadFile(HttpPostedFile userPostedFile)
        {
            string filename;
            string basePath = "Upload/Tarif";
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

        protected void DGR_TARIF_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtKET = (TextBox)e.Item.FindControl("TXT_TRF_KET");
            TextBox txtTRF = (TextBox)e.Item.FindControl("TXT_TRF_RP");
            TextBox txtTGL = (TextBox)e.Item.FindControl("TXT_TRF_TGL");

            if (e.CommandName == "Edit")
            {
                try
                {
                    conn.QueryString = "update PROVIDER_TARIF set " +
                                        "DESCR = '" + txtKET.Text.Trim() + "'," +
                                        "TARIF = '" + txtTRF.Text.Trim().Replace(",", "") + "'," +
                                        "TGL_BERLAKU = '" + GlobalUse.GlobalDateFormat(txtTGL.Text.Trim(), "d/M/yyyy") + "'," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "ID='" + e.Item.Cells[0].Text.Replace("&nbsp;", "") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR_Tarif();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PROVIDER_TARIF where " +
                                    "ID='" + e.Item.Cells[0].Text.Replace("&nbsp;", "") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGR_Tarif();
        }
    }
}