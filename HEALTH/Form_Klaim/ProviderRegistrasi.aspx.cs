using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.IO;
//using iTextSharp.text.pdf;
using HEALTH.Form_Document;

namespace HEALTH.Form_Klaim
{
    public partial class ProviderRegistrasi : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();

                if (TXT_CODE.Text.Trim() != "")
                    LoadRecord(TXT_CODE.Text.Trim());

            }
        }

        protected void Setup()
        {
            if (TXT_CODE.Text == "")
            {
                TR_REJECT.Visible = false;
                BT_APPROVE.Visible = false;
                BT_REJECT.Visible = false;
            }
            else
            {
                BT_APPROVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk APPROVE ?')){return false;};");
                BT_REJECT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk REJECT ?')){return false;};");
            }

            conn.QueryString = "select CODE,DESCR from PR_PROPINSI";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROPINSI.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TITLE_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TITLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select KODE_PROVIDER,LTRIM(NAMA) from PROVIDER_MASTER where LTRIM(RTRIM(isnull(NAMA,'')))<>'' order by LTRIM(RTRIM(isnull(NAMA,'')))";
            conn.ExecuteQuery();
            DDL_GRUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GRUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_TIPE_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_JENIS_PROVIDER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_JENIS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,UPPER(DESCR) from PARAM_TBL_KOTA_PROVIDER order by 2";
            conn.ExecuteQuery();
            DDL_KOTA.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_KOTA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_KEWARGANEGARAAN";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_NEGARA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from PR_PROVIDER_KEPEMILIKAN";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_OWNER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select * from V_PROVIDER where KODE_PROVIDER='" + code + "'";
            conn.ExecuteQuery();

            try
            {
                DDL_TITLE.SelectedValue = conn.GetFieldValue("KODE_TITLE").ToString();
            }
            catch { }
            try
            {
                DDL_TIPE.SelectedValue = conn.GetFieldValue("TIPE_PROVIDER").ToString();
            }
            catch { }
            try
            {
                DDL_NEGARA.SelectedValue = conn.GetFieldValue("NEGARA").ToString();
            }
            catch { }
            try
            {
                DDL_KOTA.SelectedValue = conn.GetFieldValue("KOTA").ToString();
            }
            catch { }
            try
            {
                DDL_JENIS.SelectedValue = conn.GetFieldValue("JENIS_PROVIDER").ToString();
            }
            catch { }
            try
            {
                DDL_GRUP.SelectedValue = conn.GetFieldValue("KODE_PROVIDERGROUP").ToString();
            }
            catch { }
            try
            {
                DDL_OWNER.SelectedValue = conn.GetFieldValue("KEPEMILIKAN_PROVIDER").ToString();
            }
            catch { }

            TXT_ALAMAT1.Text = conn.GetFieldValue("ALAMAT").ToString();
            TXT_ALAMAT2.Text = conn.GetFieldValue("ALAMAT2").ToString();
            TXT_FAX1.Text = conn.GetFieldValue("FAX").ToString();
            TXT_FAX2.Text = conn.GetFieldValue("FAX2").ToString();
            TXT_KODEPOS.Text = conn.GetFieldValue("KODEPOS").ToString();
            TXT_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
            TXT_NPWPALAMAT.Text = conn.GetFieldValue("NPWP_ALAMAT").ToString();
            TXT_NPWPNAMA.Text = conn.GetFieldValue("NPWP_NAMA").ToString();
            TXT_NPWPNO.Text = conn.GetFieldValue("NPWP_NO").ToString();
            TXT_TELEPON1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_TELEPON2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_ADMEDIKA.Text = conn.GetFieldValue("KODE_ADMEDIKA").ToString();
            TXT_ALASAN.Text = conn.GetFieldValue("ALASAN_REGISTRASI").ToString();

            SetPROPINSI();
            //FillDGRSimilarity();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            LB_ERROR.Text = "";

            string kode = "null";
            string group = "null";
            if (TXT_CODE.Text != "")
                kode = "'" + TXT_CODE.Text + "'";
            if (DDL_GRUP.SelectedValue != "")
                group = "'" + DDL_GRUP.SelectedValue + "'";

            string fieldErrors = "";
            string fileError = "";

            //bool isValidFields = ValidateAllRequiredFields(out fieldErrors,
            //// TextBox
            //(TXT_NAMA, "Nama Provider wajib diisi"),
            //(TXT_TGL_REGISTRASI, "Tanggal Pengajuan wajib diisi"),
            //(TXT_NOSURAT, "No Surat wajib diisi"),
            //(TXT_NOPKSPROVIDER, "No PKS Provider wajib diisi"),
            //(TXT_ADMEDIKA, "Kode Admedika wajib diisi"),
            //(TXT_ALAMAT1, "Alamat 1 wajib diisi"),
            //(TXT_KODEPOS, "Kode Pos wajib diisi"),
            //(TXT_TELEPON1, "Telepon 1 wajib diisi"),
            //(TXT_FAX1, "Fax 1 wajib diisi"),
            //(TXT_NPWPNO, "No NPWP wajib diisi"),
            //(TXT_NPWPNAMA, "Nama NPWP wajib diisi"),
            //(TXT_NPWPALAMAT, "Alamat NPWP wajib diisi"),
            //(TXT_ALASAN, "Alasan Registrasi wajib diisi"),

            //// DropDownList
            //(DDL_TITLE, "Title wajib dipilih"),
            //(DDL_TUJUAN, "Tujuan wajib dipilih"),
            //(DDL_GRUP, "Grup wajib dipilih"),
            //(DDL_TIPE, "Tipe wajib dipilih"),
            //(DDL_JENIS, "Jenis wajib dipilih"),
            //(DDL_OWNER, "Owner wajib dipilih"),
            //(DDL_KOTA, "Kota wajib dipilih"),
            //(DDL_NEGARA, "Negara wajib dipilih")

            // );


            //bool isValidFile = ValidatePdfUpload(FileUploadPDF, out fileError);

            //if (!isValidFields || !isValidFile)
            //{
            //    LB_ERROR.ForeColor = System.Drawing.Color.Red;
            //    LB_ERROR.Text = fieldErrors + (fileError != "" ? "<br/>" + fileError : "");
            //    return;
            //}


            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_UPSERT " +
                                    kode + "," +
                                    group + "," +
                                    "'" + DDL_TIPE.SelectedValue + "'," +
                                    "'" + DDL_JENIS.SelectedValue + "'," +
                                    "'" + DDL_OWNER.SelectedValue + "'," +
                                    "'" + TXT_ADMEDIKA.Text.Trim() + "'," +
                                    "'" + DDL_TITLE.SelectedValue + "'," +
                                    "'" + TXT_NAMA.Text + "'," +
                                    "'" + TXT_ALAMAT1.Text.Trim() + "'," +
                                    "'" + TXT_ALAMAT2.Text.Trim() + "'," +
                                    "'" + TXT_KODEPOS.Text.Trim() + "'," +
                                    "'" + TXT_TELEPON1.Text.Trim() + "'," +
                                    "'" + TXT_TELEPON2.Text.Trim() + "'," +
                                    "'" + TXT_FAX1.Text.Trim() + "'," +
                                    "'" + TXT_FAX2.Text.Trim() + "'," +
                                    "'" + DDL_KOTA.SelectedValue + "'," +
                                    "'" + TXT_NPWPNO.Text.Trim() + "'," +
                                    "'" + TXT_NPWPNAMA.Text.Trim() + "'," +
                                    "'" + TXT_NPWPALAMAT.Text.Trim() + "'," +
                                    "'" + TXT_ALASAN.Text.Trim().Replace("'", "") + "'," +
                                    "'" + 1 + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //"'" + DDL_TUJUAN.SelectedItem + "'," +
                //"'" + TXT_TGL_REGISTRASI.Text.Trim().Replace("'", "") + "'," +
                //"'" + TXT_NOSURAT.Text.Trim().Replace("'", "") + "'," +
                //"'" + TXT_NOPKSPROVIDER.Text.Trim().Replace("'", "") + "'";

                //LB_ERROR.Text = "<BR>" + conn.QueryString;


                if (FileUploadPDF.HasFile || FileUploadPDF2.HasFile || FileUploadPDF3.HasFile)
                {
                    string fileName = Path.GetFileName(FileUploadPDF.FileName);
                    string fileExtension = Path.GetExtension(fileName).ToLower();

                    string relativePath = "~/Upload/Registration/" + fileName;
                    string fileUrl = ResolveUrl(relativePath);

                    string fileName2 = Path.GetFileName(FileUploadPDF2.FileName);
                    string fileExtension2 = Path.GetExtension(fileName2).ToLower();

                    string relativePath2 = "~/Upload/Registration/" + fileName2;
                    string fileUrl2 = ResolveUrl(relativePath2);

                    string fileName3 = Path.GetFileName(FileUploadPDF3.FileName);
                    string fileExtension3 = Path.GetExtension(fileName3).ToLower();

                    string relativePath3 = "~/Upload/Registration/" + fileName3;
                    string fileUrl3 = ResolveUrl(relativePath2);

                    //string[] allowedExtensions = { ".pdf", ".doc" };
                    //if (!allowedExtensions.Contains(fileExtension) || !allowedExtensions.Contains(fileExtension2))
                    //{
                    //    LB_ERROR.Text = "Hanya file PDF & DOC yang diperbolehkan.";
                    //    return;
                    //}

                    // Tentukan path penyimpanan di file server
                    string folderPath = Server.MapPath("~/Upload/Registration"); // ganti sesuai kebutuhan
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string _fullpath = Request.PhysicalApplicationPath + folderPath + "/" + fileExtension;

                    if (File.Exists(_fullpath))
                        File.Delete(_fullpath);

                    // Nama file unik (misalnya pakai timestamp)
                    string savedFileName = fileName;
                    string fullPath = Path.Combine(folderPath, savedFileName);

                    string savedFileName2 = fileName2;
                    string fullPath2 = Path.Combine(folderPath, savedFileName2);

                    string savedFileName3 = fileName3;
                    string fullPath3 = Path.Combine(folderPath, savedFileName3);

                    HttpUtility.UrlPathEncode(fileName);
                    HttpUtility.UrlPathEncode(fileName2);
                    HttpUtility.UrlPathEncode(fileName3);

                    try
                    {
                        if (FileUploadPDF.HasFile)
                        {
                            FileUploadPDF.SaveAs(fullPath);
                            LB_ERROR.ForeColor = System.Drawing.Color.Green;
                            //LB_ERROR.Text = "Upload berhasil: " + savedFileName;
                            string fileUrlnew = ResolveUrl("~/Upload/Registration/" + fileName);
                            LblUploadedFileName.Text = "Nama file: <a href='" + fileUrlnew + "' target='_blank'>" + fileName + "</a>";
                            // Jika mau simpan path ke database, Anda bisa pakai variable fullPath atau relative path
                        }

                        if (FileUploadPDF2.HasFile)
                        {
                            FileUploadPDF.SaveAs(fullPath2);
                            LB_ERROR.ForeColor = System.Drawing.Color.Green;
                            //LB_ERROR.Text = "Upload berhasil: " + savedFileName;
                            string fileUrlneww = ResolveUrl("~/Upload/Registration/" + fileName2);
                            LblUploadedFileName2.Text = "Nama file: <a href='" + fileUrlneww + "' target='_blank'>" + fileName2 + "</a>";
                        }
                        if (FileUploadPDF3.HasFile)
                        {
                            FileUploadPDF.SaveAs(fullPath3);
                            LB_ERROR.ForeColor = System.Drawing.Color.Green;
                            //LB_ERROR.Text = "Upload berhasil: " + savedFileName;
                            string fileUrlneu = ResolveUrl("~/Upload/Registration/" + fileName3);
                            LblUploadedFileName3.Text = "Nama file: <a href='" + fileUrlneu + "' target='_blank'>" + fileName3 + "</a>";
                        }

                    }
                    catch (Exception ex)
                    {
                        LB_ERROR.Text = "Upload gagal: " + ex.Message;
                    }

                    conn.ExecuteQuery();

                    TXT_CODE.Text = conn.GetFieldValue("KODE_PROVIDER").ToString();
                }
                else
                {
                    conn.ExecuteQuery();

                    TXT_CODE.Text = conn.GetFieldValue("KODE_PROVIDER").ToString();
                }

                //conn.ExecuteQuery();

                //TXT_CODE.Text = conn.GetFieldValue("KODE_PROVIDER").ToString();

                LB_ERROR.ForeColor = System.Drawing.Color.Blue;
            }
            catch (System.Exception ex)
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            //ClearForm();
        }

        private void ClearForm()
        {
            TXT_CODE.Text = "";
            DDL_GRUP.SelectedIndex = 0;
            DDL_TIPE.SelectedIndex = 0;
            DDL_JENIS.SelectedIndex = 0;
            DDL_OWNER.SelectedIndex = 0;
            TXT_ADMEDIKA.Text = "";
            DDL_TITLE.SelectedIndex = 0;
            TXT_NAMA.Text = "";
            TXT_ALAMAT1.Text = "";
            TXT_ALAMAT2.Text = "";
            TXT_KODEPOS.Text = "";
            TXT_TELEPON1.Text = "";
            TXT_TELEPON2.Text = "";
            TXT_FAX1.Text = "";
            TXT_FAX2.Text = "";
            DDL_KOTA.SelectedIndex = 0;
            TXT_NPWPNO.Text = "";
            TXT_NPWPNAMA.Text = "";
            TXT_NPWPALAMAT.Text = "";
            TXT_ALASAN.Text = "";
            LblUploadResult.Text = "";
            DDL_TUJUAN.SelectedValue = "1";
            TXT_TGL_REGISTRASI.Text = "";
            TXT_NOSURAT.Text = "";
            TXT_NOPKSPROVIDER.Text = "";
            //LB_ERROR.Text = "";
        }

        private bool ValidatePdfUpload(FileUpload fileUpload, out string message)
        {
            if (!fileUpload.HasFile)
            {
                message = "• File PDF belum dipilih.";
                return false;
            }

            string extension = Path.GetExtension(fileUpload.FileName).ToLower();
            if (extension != ".pdf")
            {
                message = "• Hanya file PDF yang diperbolehkan.";
                return false;
            }

            message = "";
            return true;
        }


        //private bool ValidateAllRequiredFields(out string message, params (Control control, string errorMessage)[] fields)
        //{
        //    var sb = new System.Text.StringBuilder();
        //    bool isValid = true;

        //    foreach (var (control, errorMessage) in fields)
        //    {
        //        if (control is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
        //        {
        //            sb.AppendLine("• " + errorMessage);
        //            isValid = false;
        //        }
        //        else if (control is DropDownList ddl && string.IsNullOrWhiteSpace(ddl.SelectedValue))
        //        {
        //            sb.AppendLine("• " + errorMessage);
        //            isValid = false;
        //        }
        //    }

        //    message = sb.ToString();
        //    return isValid;
        //}



        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            string jenisDokumen = DDL_TUJUAN.SelectedValue;
            string TGL_REGISTRASI = TXT_TGL_REGISTRASI.Text;
            string NOSURAT = TXT_NOSURAT.Text;
            string NAMA = TXT_NAMA.Text;
            string ALAMAT = TXT_ALAMAT1.Text;
            string NOPKS = TXT_NOPKSPROVIDER.Text;
            string KOTA = DDL_KOTA.SelectedItem.Text;
            string bgPath = Server.MapPath("~/Templates/Takaful_Template_page-0001.jpg");
            string bgPath2 = Server.MapPath("~/Templates/Takaful_Template_page-0002.jpg");

            DateTime date = DateTime.Now;
            string TGL_BUATSURAT = date.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("id-ID"));

            if (jenisDokumen == "PKS_Klinik")
            {
                if (TXT_NAMA.Text != "")
                {
                    string htmlTemplate = Document.Document_PKS_KLINIK(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA);
                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordWithImageCard(htmlTemplate);

                    string headerName = "PKS Klinik Provider";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                        setTimeout(function() {
                            document.getElementById('FileUploadPDF').value = '';
                        }, 200);
                    ", true);

                    string namaFile = "Draft PKS Klinik Provider" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);
                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }

            }
            else if (jenisDokumen == "PKS_RumahSakit")
            {
                if (TXT_NAMA.Text != "")
                {
                    string htmlTemplate = Document.Document_PKS_RumahSakit(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA);
                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordWithImageCard(htmlTemplate);

                    string headerName = "Draft PKS Rumah Sakit Provider";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                        setTimeout(function() {
                            document.getElementById('FileUploadPDF').value = '';
                        }, 200);
                    ", true);

                    string namaFile = "Draft PKS Rumah Sakit Provider" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);
                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }
            }
            else if (jenisDokumen == "Adendum")
            {
                if (TXT_NAMA.Text != "")
                {
                    string htmlTemplate = Document.Document_Adendum(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA);

                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordFromHtml(htmlTemplate);

                    string headerName = "Draft Adendum";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                        setTimeout(function() {
                            document.getElementById('FileUploadPDF').value = '';
                        }, 200);
                    ", true);

                    string namaFile = "Draft Addendum PKS Rumah Sakit" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);
                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }
            }
            else if (jenisDokumen == "Perpanjangan_PKS")
            {
                if (TXT_NAMA.Text != "" && TXT_ALAMAT1.Text != "" && TXT_NOSURAT.Text != "" && TXT_TGL_REGISTRASI.Text != "")
                {


                    string htmlTemplate = Document.Document_Kerjasama_Sementara(TGL_REGISTRASI, NOPKS, NAMA, ALAMAT, NAMA, TGL_BUATSURAT, KOTA);
                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordWithBackgroundV3(htmlTemplate, bgPath);

                    string headerName = "Surat Pengajuan Perpanjangan PKS";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                setTimeout(function() {
                    document.getElementById('FileUploadPDF').value = '';
                }, 200);
            ", true);

                    string namaFile = "Perjanjian Kerja Sama Sementara" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);
                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }
            }
            else if (jenisDokumen == "Kerjasama_Sementara")
            {
                if (TXT_NAMA.Text != "" && TXT_ALAMAT1.Text != "" && TXT_NOSURAT.Text != "" && TXT_TGL_REGISTRASI.Text != "")
                {

                    string htmlTemplate = Document.Document_Penawaran_PKS(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA, TGL_BUATSURAT, KOTA);
                    byte[] wordBytes = WordGenerator.GenerateWordWithBackgroundV2(htmlTemplate, bgPath);

                    string headerName = "Surat Kerja Sama Sementara";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                            setTimeout(function() {
                                document.getElementById('FileUploadPDF').value = '';
                            }, 200);
                        ", true);

                    string namaFile = "Surat penawaran kerjasama" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);
                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }
            }
            else if (jenisDokumen == "NDA")
            {
                if (TXT_NAMA.Text != "" && TXT_ALAMAT1.Text != "" && TXT_NOSURAT.Text != "" && TXT_TGL_REGISTRASI.Text != "")
                {
                    string htmlTemplate = Document.Document_NDA(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA);
                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordFromHtml(htmlTemplate);

                    string headerName = "NDA";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                        setTimeout(function() {
                            document.getElementById('FileUploadPDF').value = '';
                        }, 200);
                    ", true);

                    string namaFile = "NDA Takaful Keluarga" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);

                }
            }
            else if (jenisDokumen == "NDA_Nasional")
            {
                if (TXT_NAMA.Text != "" && TXT_ALAMAT1.Text != "" && TXT_NOSURAT.Text != "" && TXT_TGL_REGISTRASI.Text != "")
                {
                    string htmlTemplate = Document.DocumentNDANasional(TGL_REGISTRASI, NOSURAT, NAMA, ALAMAT, NAMA);
                    // Ganti generator PDF menjadi generator Word
                    byte[] wordBytes = WordGenerator.GenerateWordFromHtml(htmlTemplate);

                    string headerName = "NDA_Nasional";
                    Session["GeneratedWord"] = wordBytes;
                    Session["AddHeader"] = headerName;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ClearUpload", @"
                        setTimeout(function() {
                            document.getElementById('FileUploadPDF').value = '';
                        }, 200);
                    ", true);

                    string namaFile = "NDA Takaful Keluarga" + "_" + TXT_NAMA.Text;
                    Response.Redirect("DownloadWord.aspx?namaFile=" + namaFile);

                }
                else
                {
                    LB_DOWNLOAD.ForeColor = System.Drawing.Color.Red;
                    LB_DOWNLOAD.Text = "Field Tidak Boleh Kosong";
                    return;
                }
            }
            }
        
        protected void FillDGRSimilarity()
        {
            conn.QueryString = "select " +
                                "[NAMA] = UPPER(NAMA), " +
                                "ALAMAT, " +
                                "[STATUS] = b.DESCR, " +
                                "[% SIMILARITY] = convert(decimal(18,2),dbo.f_jaro_winkler('" + TXT_NAMA.Text.Trim() + "',NAMA)*100) " +
                                "from PROVIDER_MASTER a " +
                                "inner join PR_STATUS_PROVIDER b on a.STAT=b.CODE " +
                                "where " +
                                "dbo.f_jaro_winkler('" + TXT_NAMA.Text.Trim() + "',NAMA)>=0.8 " +
                                "and KODE_PROVIDER<>'" + TXT_CODE.Text + "' " +
                                "order by 4 desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SIMILARITY.DataSource = dt;
            DGR_SIMILARITY.DataBind();

            for (int i = 0; i < DGR_SIMILARITY.Items.Count; i++)
            {
                if (DGR_SIMILARITY.Items[i].Cells[3].Text == "NON AKTIF")
                {
                    DGR_SIMILARITY.Items[i].BackColor = System.Drawing.Color.Pink;
                    DGR_SIMILARITY.Items[i].Cells[3].ForeColor = System.Drawing.Color.Red;
                }

                if (DGR_SIMILARITY.Items[i].Cells[0].Text == "100,00")
                {
                    DGR_SIMILARITY.Items[i].Font.Bold = true;
                }
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_APPROVE '" + TXT_CODE.Text.Trim() + "',1,'" + TXT_REJECT.Text.Trim() + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("ProviderPending.aspx");
            }
            catch { }
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            if (TXT_REJECT.Text.Trim() == "")
            {
                GlobalTools.popMessage(this, "alasan REJECT tidak boleh kosong");
                return;
            }

            try
            {
                conn.QueryString = "exec SP_CLM_PROVIDER_APPROVE '" + TXT_CODE.Text.Trim() + "',1,'" + TXT_REJECT.Text.Trim() + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("ProviderPending.aspx");
            }
            catch { }
        }

        protected void DDL_KOTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPROPINSI();
        }

        protected void SetPROPINSI()
        {
            try
            {
                conn.QueryString = "select PROPINSI from PARAM_TBL_KOTA_PROVIDER where CODE='" + DDL_KOTA.SelectedValue + "'";
                conn.ExecuteQuery();
                DDL_PROPINSI.SelectedValue = conn.GetFieldValue("PROPINSI").ToString();
            }
            catch { }
        }

        //        private string GetSuratHtml3(
        //          string tanggalpengajuan,
        //          string nomorsurat,
        //          string namaPerusahaan,
        //          string alamatPerusahaan,
        //          string namaRS)
        //        {
        //            var html = @"
        //<!DOCTYPE html>
        //<html lang='id'>
        //<head>
        //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
        //    <title>Perjanjian Kerjasama Klinik Provider dengan Asuransi Takaful Keluarga 2024</title>
        //    <style>
        //        body {
        //            width: 21cm; height: 29.7cm; margin: 2cm;
        //            font-family: Arial, sans-serif; font-size: 11pt; line-height: 1.6;
        //        }
        //        .header {
        //            text-align: right; margin-bottom: 1em;
        //        }
        //        .subject p {
        //            margin: 0 0 0.5em;
        //        }
        //        p {
        //            margin: 0 0 0.8em; text-align: justify;
        //        }
        //        ol, ul {
        //            margin: 0.8em 0; padding-left: 1.5em;
        //        }
        //        .section-title {
        //            margin-top: 1.5em; font-weight: bold; text-align: left;
        //        }
        //        table {
        //            width: 100%; border-collapse: collapse; margin: 0.8em 0;
        //        }
        //        table, th, td {
        //            border: 1px solid #333;
        //        }
        //        th, td {
        //            padding: 0.5em; vertical-align: top;
        //        }
        //        th {
        //            background-color: #f0f0f0;
        //        }
        //        .signatures {
        //            display: flex; justify-content: space-between; margin-top: 3em;
        //        }
        //        .signatures div {
        //            width: 45%; text-align: center;
        //        }
        //    </style>
        //</head>
        //<body>
        //    <div class='header'>
        //        Jakarta, {{TanggalPengajuan}}
        //    </div>

        //    <div class='subject'>
        //        <p><strong>Nomor:</strong> {{NomorSurat}}</p>
        //        <p><strong>Perihal:</strong> Perjanjian Kerjasama Klinik Provider dengan Asuransi Takaful Keluarga 2024</p>
        //    </div>

        //    <p>Perjanjian Kerjasama Program Pelayanan Pengobatan Kesehatan Secara Berlangganan
        //       (selanjutnya disebut “Perjanjian”) ini dibuat pada tanggal {{TanggalPengajuan}}, oleh dan antara:</p>

        //    <p><strong>PIHAK PERTAMA:</strong> PT. Asuransi Takaful Keluarga, berkedudukan di Graha Takaful Indonesia,
        //       Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, diwakili oleh Penny Hikmahwati selaku Direktur Operasional,
        //       selanjutnya disebut “PIHAK PERTAMA”.</p>

        //    <p><strong>PIHAK KEDUA:</strong> {{NamaPerusahaan}}, beralamat di {{AlamatPerusahaan}},
        //       diwakili oleh {{NamaRS}} selaku Direktur, selanjutnya disebut “PIHAK KEDUA”.</p>

        //    <p>PIHAK PERTAMA dan PIHAK KEDUA (selanjutnya disebut “PARA PIHAK”) menerangkan terlebih dahulu sebagai berikut:</p>
        //    <ol>
        //        <li>PIHAK PERTAMA adalah perusahaan asuransi berdasarkan prinsip syariah yang menunjuk TPA untuk administrasi klaim kesehatan.</li>
        //        <li>PIHAK KEDUA adalah Klinik Kesehatan yang memberikan pelayanan kepada Peserta asuransi PIHAK PERTAMA.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 1 – DEFINISI</p>
        //    <ol>
        //        <li><strong>Asuransi Kesehatan</strong> adalah program asuransi yang diselenggarakan PIHAK PERTAMA bagi Peserta dan keluarganya.</li>
        //        <li><strong>Hari Kalender</strong> adalah setiap hari dalam kalender, termasuk Hari Libur Nasional.</li>
        //        <li><strong>Hari Kerja</strong> adalah hari Senin sampai Jumat, kecuali hari libur yang ditetapkan.</li>
        //        <li><strong>Kartu Peserta</strong> adalah kartu pengenal Peserta yang diterbitkan PIHAK PERTAMA.</li>
        //        <li><strong>Obat/Therapi</strong> adalah obat sesuai DOEN, ISO, atau MIMS untuk pengobatan medis.</li>
        //        <li><strong>Operasi</strong> adalah tindakan medis specialistis dengan sayatan, dengan atau tanpa anestesi.</li>
        //        <li><strong>Perjanjian</strong> adalah seluruh syarat dan kondisi yang disetujui PARA PIHAK, termasuk lampiran dan amandemen.</li>
        //        <li><strong>Peserta</strong> adalah karyawan atau individu terdaftar sebagai peserta Asuransi Kesehatan PIHAK PERTAMA.</li>
        //        <li><strong>Rawat Inap</strong> adalah perawatan minimal 12 jam di Klinik untuk penyakit atau cedera terjamin.</li>
        //        <li><strong>Rawat Jalan</strong> adalah perawatan satu hari tanpa inap di Klinik.</li>
        //        <li><strong>Surat Jaminan</strong> adalah surat bukti jaminan PIHAK PERTAMA untuk PIHAK KEDUA.</li>
        //        <li><strong>Tarif</strong> adalah harga layanan Klinik yang diberitahukan ke PIHAK PERTAMA setiap perubahan.</li>
        //        <li><strong>TPA</strong> adalah Third Party Administrator yang mengelola administrasi klaim kesehatan.</li>
        //        <li><strong>Clinical Pathway</strong> adalah tatalaksana multidisiplin untuk mutu perawatan konsisten.</li>
        //        <li><strong>Medical Efficacy</strong> adalah ukuran efektivitas intervensi medis.</li>
        //        <li><strong>Utilization Review</strong> adalah metode pengendalian biaya dan mutu layanan medis.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 2 – PENUNJUKAN DAN PENETAPAN</p>
        //    <p>PIHAK PERTAMA menunjuk PIHAK KEDUA sebagai Klinik penyelenggara pelayanan pengobatan kesehatan
        //       secara berlangganan, dan PIHAK KEDUA menerima penunjukan ini.</p>

        //    <p class='section-title'>PASAL 3 – RUANG LINGKUP PERJANJIAN</p>
        //    <ol>
        //        <li>PIHAK KEDUA wajib menyediakan pelayanan Rawat Jalan sesuai Kartu Peserta.</li>
        //        <li>Proses TPA adalah verifikasi TPA; selainnya disebut Proses Takaful.</li>
        //        <li>Kelas layanan dan fasilitas tercantum dalam Kartu Peserta.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 4 – PEMBEBASAN UANG PEMBAYARAN</p>
        //    <ol>
        //        <li>Peserta dibebaskan dari pembayaran dengan menunjukkan Kartu Peserta.</li>
        //        <li>Ketentuan pembebasan:
        //            <ol type='a'>
        //                <li>Proses TPA: biaya ditanggung sesuai limit setelah verifikasi.</li>
        //                <li>Proses Takaful: biaya Rawat Jalan ditanggung peserta, kecuali kartu VIP.</li>
        //            </ol>
        //        </li>
        //        <li>PIHAK KEDUA tidak memberikan layanan di luar ketentuan Perjanjian.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 5 – FASILITAS YANG DISEDIAKAN</p>
        //    <ol>
        //        <li>Pemeriksaan dan pengobatan dokter umum/spesialis</li>
        //        <li>Pemeriksaan laboratorium & diagnostik lain</li>
        //        <li>Obat sesuai DOEN, ISO, MIMS</li>
        //        <li>Operasi kecil dengan anestesi lokal</li>
        //        <li>Imunisasi dasar bayi (BCG, DPT, Polio, Campak)</li>
        //        <li>Pelayanan gigi: pencabutan, tambal, perawatan saraf, pembersihan karang</li>
        //    </ol>

        //    <p class='section-title'>PASAL 6 – BIAYA PELAYANAN</p>
        //    <ol>
        //        <li>Tarif sesuai yang berlaku di PIHAK KEDUA.</li>
        //        <li>Perubahan tarif diberitahukan tertulis 30 hari sebelumnya.</li>
        //        <li>Jika tidak diberitahukan, tarif lama tetap berlaku.</li>
        //        <li>Resiko akibat perubahan tanpa pemberitahuan ditanggung PIHAK KEDUA.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 7 – KETENTUAN PELAYANAN MEDIS</p>
        //    <ol>
        //        <li>Memperhatikan Daftar Pengecualian lampiran Perjanjian.</li>
        //        <li>Obat-obatan berpedoman pada DOEN, ISO, MIMS.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 8 – TATA CARA PELAYANAN MEDIS</p>
        //    <ol>
        //        <li>Validasi Kartu Peserta sebelum layanan.</li>
        //        <li>Proses Takaful melalui reimbursement kecuali kartu VIP.</li>
        //        <li>Jika TPA offline, keluarkan Surat Pengesahan & Tagihan.</li>
        //        <li>Kembalikan Kartu Peserta setelah layanan.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 9 – TATA CARA PENGAJUAN PENAGIHAN</p>
        //    <ol>
        //        <li>Kirim nota tagihan lengkap setelah pengobatan.</li>
        //        <li>Dokumen pendukung: kwitansi asli, resume medis, rincian biaya, surat jaminan, kartu peserta.</li>
        //        <li>Kirim ke alamat PIHAK PERTAMA di Graha Takaful Indonesia.</li>
        //        <li>Proses TPA boleh dikirim langsung ke TPA.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 10 – JANGKA WAKTU PENAGIHAN</p>
        //    <ol>
        //        <li>Penagihan selambat-lambatnya 30 Hari Kalender setelah layanan.</li>
        //        <li>Tagihan lewat batas tidak wajib dibayar.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 11 – PELAYANAN PESERTA</p>
        //    <p>Pelayanan pengobatan sesuai manfaat dan prosedur PIHAK PERTAMA.</p>

        //    <p class='section-title'>PASAL 12 – SISTEM PEMBAYARAN</p>
        //    <ol>
        //        <li>PIHAK PERTAMA membayar tagihan selambat-lambatnya 30 Hari Kalender setelah terima tagihan lengkap.</li>
        //        <li>Pembayaran melalui transfer ke rekening PIHAK KEDUA.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 13 – TUGAS DAN KEWAJIBAN</p>
        //    <ol>
        //        <li>Pelayanan sesuai standar diagnostik dan prosedur medis lazim.</li>
        //        <li>Cegah penyalahgunaan limit santunan.</li>
        //        <li>PIHAK KEDUA bertanggung jawab atas kebenaran keterangan medis.</li>
        //        <li>Tagih selisih biaya kepada Peserta sebelum meninggalkan Klinik.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 14 – JANGKA WAKTU PERJANJIAN</p>
        //    <ol>
        //        <li>Berlangsung 3 tahun sejak penandatanganan, otomatis diperpanjang jika tidak ada pembatalan.</li>
        //        <li>Evaluasi minimal sekali setahun.</li>
        //        <li>Pemberitahuan pembatalan/perubahan 60 hari sebelumnya.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 15 – PEMUTUSAN PERJANJIAN</p>
        //    <ol>
        //        <li>Pihak dapat membatalkan sepihak dengan pemberitahuan 30 hari jika lalai perbaikan.</li>
        //        <li>Peringatan tertulis 3 kali dalam sebulan tanpa perbaikan dapat memutuskan Perjanjian.</li>
        //        <li>Penyelesaian kewajiban dirundingkan bersama.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 16 – PERNYATAAN DAN JAMINAN</p>
        //    <ol>
        //        <li>PARA PIHAK wajib saling memberi informasi perubahan.</li>
        //        <li>PARA PIHAK menjamin memiliki wewenang dan izin pelaksanaan.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 17 – FORCE MAJEURE</p>
        //    <ol>
        //        <li>Kewajiban ditangguhkan jika terhalang force majeure.</li>
        //        <li>Pemberitahuan tertulis maksimal 14 Hari Kerja.</li>
        //        <li>Tanggung jawab keterlambatan pemberitahuan ditanggung pihak terdampak.</li>
        //        <li>Lanjutkan kegiatan setelah force majeure berakhir.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 18 – PEMBERITAHUAN</p>
        //    <div style='display:flex; justify-content:space-between;'>
        //        <div>
        //            <strong>PIHAK PERTAMA</strong><br/>
        //            PT. Asuransi Takaful Keluarga<br/>
        //            Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan<br/>
        //            Telp: 021-7991234, 021-79190005<br/>
        //            Email: provrelation-atk@takaful.com
        //        </div>
        //        <div>
        //            <strong>PIHAK KEDUA</strong><br/>
        //            {{NamaPerusahaan}}<br/>
        //            {{AlamatPerusahaan}}<br/>
        //            Telp: ………<br/>
        //            Email: ………
        //        </div>
        //    </div>

        //    <p class='section-title'>PASAL 19 – HUKUM YANG BERLAKU</p>
        //    <p>Perjanjian tunduk pada perundang-undangan Republik Indonesia.</p>

        //    <p class='section-title'>PASAL 20 – PENYELESAIAN PERSELISIHAN</p>
        //    <ol>
        //        <li>Diselesaikan musyawarah untuk mufakat.</li>
        //        <li>Jika tidak tercapai dalam 30 hari, diajukan ke Pengadilan Negeri Jakarta Selatan.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 21 – KERAHASIAAN</p>
        //    <ol>
        //        <li>Informasi Rahasia tidak diungkap kecuali diwajibkan hukum.</li>
        //        <li>Hanya pihak berwenang yang dapat mengakses dengan persetujuan tertulis.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 22 – KESELURUHAN PERJANJIAN</p>
        //    <ol>
        //        <li>Merupakan keseluruhan kesepakatan menggantikan perjanjian sebelumnya.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 23 – KETERPISAHAN</p>
        //    <ol>
        //        <li>Ketentuan batal tidak mempengaruhi ketentuan lain yang tetap berlaku.</li>
        //        <li>Ketentuan batal diganti dengan yang sah mencerminkan maksud semula.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 24 – PENGALIHAN HAK</p>
        //    <ol>
        //        <li>Tidak dapat dialihkan tanpa persetujuan tertulis pihak lain.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 25 – PERUBAHAN DAN TAMBAHAN</p>
        //    <ol>
        //        <li>Perubahan harus tertulis dan ditandatangani PARA PIHAK.</li>
        //        <li>Amandemen akibat peraturan baru dibahas bersama.</li>
        //    </ol>

        //    <p class='section-title'>PASAL 26 – PENUTUP</p>
        //    <p>Perjanjian dibuat rangkap 2 bermaterai cukup, masing-masing memiliki kekuatan hukum yang sama.</p>

        //    <div class='signatures'>
        //        <div>
        //            <p><strong>PIHAK PERTAMA</strong><br/>PT. Asuransi Takaful Keluarga</p>
        //            <p style='margin-top:3em;'>Penny Hikmahwati<br/>Direktur Operasional</p>
        //        </div>
        //        <div>
        //            <p><strong>PIHAK KEDUA</strong><br/>{{NamaPerusahaan}}</p>
        //            <p style='margin-top:3em;'>{{NamaRS}}<br/>Direktur</p>
        //        </div>
        //    </div>

        //    <p class='section-title'>LAMPIRAN I – DAFTAR PENGECUALIAN</p>
        //    <ol>
        //        <li>Perang, demonstrasi, huru-hara, pemberontakan, bencana alam, radiasi massal.</li>
        //        <li>Percobaan bunuh diri, pelanggaran hukum, alkohol, narkotika, psikotropika.</li>
        //        <li>Olahraga berisiko tinggi (panjat tebing, balap, diving, tinju, akrobatik).</li>
        //        <li>Penyakit menular seksual (HIV, AIDS, ARC) dan segala akibatnya.</li>
        //        <li>Tindakan eksperimen (ozon, stem cell, laser, spa, alternatif).</li>
        //        <li>Kondisi kongenital dan kelainan herediter.</li>
        //        <li>Gangguan tumbuh kembang (autisme, ADHD, retardasi mental).</li>
        //        <li>Pemeriksaan non-medis dan tindakan kosmetik.</li>
        //        <li>Tindakan medis oleh non-profesional.</li>
        //        <li>Pengobatan di luar manfaat Peserta.</li>
        //    </ol>

        //    <p class='section-title'>LAMPIRAN II – CONTOH KARTU PESERTA</p>
        //    <table>
        //        <thead>
        //            <tr><th>No</th><th>Kartu Peserta</th><th>Keterangan</th></tr>
        //        </thead>
        //        <tbody>
        //            <tr><td>1</td><td>…</td><td>…</td></tr>
        //            <tr><td>2</td><td>…</td><td>…</td></tr>
        //        </tbody>
        //    </table>
        //</body>
        //</html>
        //";

        //            return html
        //                .Replace("{{TanggalPengajuan}}", tanggalpengajuan)
        //                .Replace("{{NomorSurat}}", nomorsurat)
        //                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
        //                .Replace("{{AlamatPerusahaan}}", alamatPerusahaan)
        //                .Replace("{{NamaRS}}", namaRS);
        //        }
        //        private string GetSuratHtml4(
        //         string tanggalPengajuan,
        //         string nomorSurat,
        //         string namaPerusahaan,
        //         string alamatPerusahaan,
        //         string namaRS)
        //        {
        //            var html = @"
        //<!DOCTYPE html>
        //<html lang='id'>
        //<head>
        //    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
        //    <title>Perjanjian Kerahasiaan / Non-Disclosure Agreement</title>
        //    <style>
        //        body {
        //            width: 21cm;
        //            height: 29.7cm;
        //            margin: 2cm;
        //            font-family: Arial, sans-serif;
        //            font-size: 11pt;
        //            line-height: 1.5;
        //        }
        //        .header { margin-bottom: 1em; }
        //        .title {
        //            text-align: center;
        //            font-weight: bold;
        //            margin: 1em 0;
        //        }
        //        p {
        //            margin: 0 0 0.8em 0;
        //            text-align: justify;
        //        }
        //        ol {
        //            margin: 0.8em 0 0.8em 1.5em;
        //        }
        //        dl { margin: 0.8em 0; }
        //        .bilingual dt {
        //            font-weight: bold;
        //            margin-top: 1em;
        //        }
        //        .bilingual dd {
        //            margin-left: 1em;
        //            margin-bottom: 0.5em;
        //        }
        //        .section-title {
        //            font-weight: bold;
        //            margin-top: 1.5em;
        //            margin-bottom: 0.5em;
        //        }
        //        .signatures {
        //            display: flex;
        //            justify-content: space-between;
        //            margin-top: 3em;
        //        }
        //        .signatures div {
        //            width: 45%;
        //            text-align: center;
        //        }
        //    </style>
        //</head>
        //<body>

        //    <div class='header'>
        //        Jakarta, {{TanggalPengajuan}}
        //    </div>

        //    <div class='title'>
        //        PERJANJIAN KERAHASIAAN<br/>
        //        NON–DISCLOSURE AGREEMENT
        //    </div>

        //    <p><strong>NUMBER / NOMOR:</strong> {{NomorSurat}}</p>

        //    <p>This Non Disclosure Agreement is made and entered into on this day, ____, 2025 by and between:</p>
        //    <p>Perjanjian Kerahasiaan ini dibuat dan ditandatangani pada hari ____, 2025, oleh dan antara:</p>

        //    <dl class='bilingual'>
        //      <dt>FIRST PARTY<br/>PIHAK PERTAMA</dt>
        //      <dd>
        //        PT ASURANSI TAKAFUL KELUARGA, a company duly formed and incorporated under the laws of Indonesia with its principal place of business at Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, represented by Penny Hikmahwati as Operational Director ('FIRST PARTY');<br/>
        //        PT ASURANSI TAKAFUL KELUARGA, suatu perusahaan yang dibentuk dan didirikan di bawah Undang-undang Indonesia dengan kantor di Graha Takaful Indonesia, Jalan Mampang Prapatan Raya No. 100, Jakarta Selatan, diwakili oleh Penny Hikmahwati sebagai Direktur Operasional ('PIHAK PERTAMA').
        //      </dd>

        //      <dt>SECOND PARTY<br/>PIHAK KEDUA</dt>
        //      <dd>
        //        …………………………, a company duly formed and incorporated under the laws of Singapore with its principal place of business at …………………………, represented by ………………………… as …………………… ('SECOND PARTY');<br/>
        //        …………………………, sebuah perusahaan yang didirikan berdasarkan hukum Singapura dengan kantor di …………………………, diwakili oleh ………………………… sebagai ………………………… ('PIHAK KEDUA').
        //      </dd>
        //    </dl>

        //    <p>FIRST PARTY and SECOND PARTY hereinafter collectively referred to as “Parties” or singularly as “Party”.<br/>
        //       PIHAK PERTAMA dan PIHAK KEDUA selanjutnya disebut bersama sebagai “Para Pihak” atau masing-masing “Pihak”.</p>

        //    <p class='section-title'>WHEREAS / BAHWA</p>
        //    <ol>
        //      <li>the FIRST PARTY as a sharia insurance provider agrees to cooperate with the SECOND PARTY as described herein;<br/>
        //          PIHAK PERTAMA sebagai penyedia asuransi syariah sepakat bekerjasama dengan PIHAK KEDUA sebagaimana dijelaskan di Perjanjian ini;</li>
        //      <li>the SECOND PARTY is engaged in insurance technology in accordance with the objectives of this Agreement;<br/>
        //          PIHAK KEDUA bergerak dalam teknologi asuransi sesuai tujuan Perjanjian ini;</li>
        //      <li>the Parties plan to cooperate in utilization of insurance technology (“Objectives”);<br/>
        //          Para Pihak berencana bekerja sama memanfaatkan teknologi asuransi (“Tujuan”);</li>
        //      <li>in carrying out the Objectives, Parties may act as Receiving Party or Giving Party for Confidential Information;<br/>
        //          Dalam pelaksanaan Tujuan, Para Pihak dapat bertindak sebagai Penerima atau Pemberi Informasi Rahasia;</li>
        //      <li>the Parties agree to the following terms and conditions by entering into this Agreement.<br/>
        //          Para Pihak sepakat dengan syarat-syarat dan ketentuan berikut dengan menandatangani Perjanjian ini.</li>
        //    </ol>

        //    <p class='section-title'>ARTICLE 1 / PASAL 1: DEFINITION / DEFINISI</p>
        //    <ol>
        //      <li>“Confidential Information” means all non-public information disclosed by the Giving Party, including financial, business, customer data, plans, strategies, orally or in writing;<br/>
        //          “Informasi Rahasia” berarti semua informasi non-publik yang diungkapkan oleh Pihak Pemberi, meliputi data keuangan, usaha, pelanggan, rencana, strategi, lisan atau tertulis;</li>
        //      <li>includes third-party information disclosed for performing the Objectives;<br/>
        //          mencakup informasi pihak ketiga yang diungkap untuk pelaksanaan Tujuan;</li>
        //      <li>exclusions: publicly known, already lawfully in Receiving Party’s possession;<br/>
        //          pengecualian: sudah publik, telah secara sah dimiliki Pihak Penerima;</li>
        //      <li>exceptions: required by court order, law, or necessary to fulfill Giving Party’s obligations (with prior notice).<br/>
        //          pengecualian: diwajibkan oleh putusan pengadilan, hukum, atau untuk memenuhi kewajiban Pihak Pemberi (dengan pemberitahuan sebelumnya).</li>
        //    </ol>

        //    <p class='section-title'>ARTICLE 2 / PASAL 2: NON–DISCLOSURE AND SECRECY / LARANGAN PENGUNGKAPAN KERAHASIAAN</p>
        //    <ol>
        //      <li>Receiving Party shall use Confidential Information solely for the Objectives;<br/>
        //          Pihak Penerima hanya menggunakan Informasi Rahasia untuk Tujuan;</li>
        //      <li>shall not disclose to third parties unless similarly bound and remains liable for breach;<br/>
        //          tidak mengungkapkan ke pihak ketiga kecuali dengan ikatan yang sama dan tetap bertanggung jawab atas pelanggaran;</li>
        //      <li>shall implement safeguards and restrict access to employees with need-to-know;<br/>
        //          menerapkan langkah pengamanan dan membatasi akses karyawan yang perlu mengetahui;</li>
        //      <li>all Confidential Information and copies remain property of Giving Party and must be returned or destroyed upon termination.<br/>
        //          semua Informasi Rahasia dan salinannya tetap milik Pihak Pemberi dan harus dikembalikan atau dihancurkan setelah berakhirnya Perjanjian.</li>
        //    </ol>

        //    <p class='section-title'>ARTICLE 3 / PASAL 3: OWNERSHIP AND INFORMATION RETURN / KEPEMILIKAN DAN PENGEMBALIAN INFORMASI</p>
        //    <ol>
        //      <li>Upon request within 5 business days, Receiving Party shall destroy or return all Confidential Information, tools, documentation;<br/>
        //          Atas permintaan dalam 5 hari kerja, Pihak Penerima wajib menghancurkan atau mengembalikan semua Informasi Rahasia, alat, dokumentasi;</li>
        //      <li>and provide written statement of compliance.<br/>
        //          serta memberikan pernyataan tertulis bahwa kewajiban tersebut telah dilaksanakan.</li>
        //    </ol>

        //    <p class='section-title'>ARTICLE 4 / PASAL 4: TIME PERIOD / PERIODE WAKTU</p>
        //    <p>This Agreement effective from the date above; Confidential Information remains binding irrespective of termination of Cooperation Agreement.<br/>
        //       Perjanjian ini berlaku sejak tanggal di atas; Informasi Rahasia tetap mengikat tanpa memandang berakhirnya Perjanjian Kerjasama.</p>

        //    <p class='section-title'>ARTICLE 5 / PASAL 5: DISPLACEMENT / PENGALIHAN</p>
        //    <p>Neither Party may assign rights or obligations without prior written consent of the other Party.<br/>
        //       Para Pihak tidak dapat mengalihkan hak atau kewajiban tanpa persetujuan tertulis pihak lain.</p>

        //    <p class='section-title'>ARTICLE 6 / PASAL 6: FAIR COMPENSATION / PENGGANTIAN YANG ADIL</p>
        //    <p>Breaches may cause irreparable harm; Wrongdoing Party shall provide equitable relief or other remedies, including damages.<br/>
        //       Pelanggaran dapat menimbulkan kerugian yang tak terpulihkan; Pihak yang Merugikan wajib memberikan ganti rugi adil atau pemulihan lain, termasuk kompensasi.</p>

        //    <p class='section-title'>ARTICLE 7 / PASAL 7: GOVERNING LAW AND DISPUTE RESOLUTION / HUKUM YANG MENGATUR DAN PENYELESAIAN PERSELISIHAN</p>
        //    <ol>
        //      <li>Governing law: Republic of Singapore; Parties comply with its laws;<br/>
        //          Hukum yang berlaku: Republik Singapura; Para Pihak tunduk padanya;</li>
        //      <li>Disputes: first by amicable negotiation; if not resolved within 30 days, submitted to South Jakarta District Court Registrar.<br/>
        //          Perselisihan: pertama diselesaikan musyawarah; jika tidak dalam 30 hari, diajukan ke Panitera Pengadilan Negeri Jakarta Selatan.</li>
        //    </ol>

        //    <p class='section-title'>ARTICLE 8 / PASAL 8: OTHER PROVISIONS / KETENTUAN LAIN</p>
        //    <ol>
        //      <li>Parties execute this Agreement responsibly and in compliance with applicable laws;<br/>
        //          Para Pihak menandatangani Perjanjian ini dengan tanggung jawab dan sesuai hukum yang berlaku;</li>
        //      <li>Each Party has read, understood, and had opportunity to review;<br/>
        //          Masing-masing Pihak telah membaca, memahami, dan berkesempatan meninjau;</li>
        //      <li>Inconsistency: English version prevails.<br/>
        //          Ketidaksesuaian: versi Inggris yang berlaku.</li>
        //    </ol>

        //    <p>Thus this Agreement is made and signed by the Parties on sufficient stamp duty in duplicate, each holding one copy.<br/>
        //       Demikian Perjanjian ini dibuat dan ditandatangani oleh Para Pihak di atas meterai cukup dalam rangkap dua, masing-masing Pihak memegang satu.</p>

        //    <div class='signatures'>
        //      <div>
        //        <p><strong>For and on behalf of FIRST PARTY /</strong><br/>
        //           PT. Asuransi Takaful Keluarga</p>
        //        <p>Name/Nama: PENNY HIKMAHWATI<br/>
        //           Designation/Jabatan: Direktur Operasional<br/>
        //           Date/Tanggal: ________________</p>
        //      </div>
        //      <div>
        //        <p><strong>For and on behalf of SECOND PARTY /</strong><br/>
        //           …………………………</p>
        //        <p>Name/Nama: ________________<br/>
        //           Designation/Jabatan: ________________<br/>
        //           Date/Tanggal: ________________</p>
        //      </div>
        //    </div>

        //</body>
        //</html>
        //";

        //            return html
        //                .Replace("{{TanggalPengajuan}}", tanggalPengajuan)
        //                .Replace("{{NomorSurat}}", nomorSurat)
        //                .Replace("{{NamaPerusahaan}}", namaPerusahaan)
        //                .Replace("{{AlamatPerusahaan}}", alamatPerusahaan)
        //                .Replace("{{NamaRumahSakit}}", namaRS);
        //        }
    }
}