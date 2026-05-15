using System;
using System.Web;
using System.Web.UI;

namespace HEALTH.Form_Klaim
{
    public partial class DownloadWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GeneratedWord"] != null)
            {
                byte[] wordBytes = Session["GeneratedWord"] as byte[];

                if (wordBytes != null && wordBytes.Length > 0)
                {
                    string namaFile = Request.QueryString["namaFile"];
                    if (string.IsNullOrWhiteSpace(namaFile))
                    {
                        namaFile = "Dokumen";
                    }

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.AddHeader("Content-Disposition", $"attachment;filename=\"{namaFile}.docx\"");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(wordBytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Response.Write("File Word kosong atau rusak.");
                }

                Session.Remove("GeneratedWord");
            }
            else
            {
                Response.Write("Tidak ada file Word untuk diunduh.");
            }
        }
    }
}
