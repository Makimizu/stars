using System;

namespace HEALTH.Templates
{
    public partial class DownloadPDF : System.Web.UI.Page // ✅ INHERIT dari Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["GeneratedPDF"] != null)
            {
                byte[] pdfBytes = (byte[])Session["GeneratedPDF"];
                string namaFile = Request.QueryString["namaFile"];
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + namaFile + ".pdf");
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                Response.BinaryWrite(pdfBytes);
                Response.End();

                Session.Remove("GeneratedPDF");
            }
            else
            {
                Response.Write("Tidak ada file PDF untuk diunduh.");
            }
        }
    }
}
