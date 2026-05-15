using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;

public class PdfGenerator
{
    public static byte[] GeneratePdfWithBackground(string html, string backgroundImagePath)
    {
        using (var ms = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);

            pdfDoc.AddEventHandler(iText.Kernel.Events.PdfDocumentEvent.START_PAGE,
                new PdfBackgroundHandler(backgroundImagePath));

            using (var htmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
            {
                HtmlConverter.ConvertToPdf(htmlStream, pdfDoc);
            }

            return ms.ToArray();
        }
    }

    public static byte[] GeneratePdfWithNoBackground(string html)
    {
        using (var ms = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(writer);

            using (var htmlStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(html)))
            {
                HtmlConverter.ConvertToPdf(htmlStream, pdfDoc);
            }

            pdfDoc.Close();
            return ms.ToArray();
        }
    }

}


//using System;
//using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.tool.xml;

//public class PdfGenerator
//{
//    public static byte[] GeneratePdfWithBackground(string html, string backgroundImagePath)
//    {
//        using (var ms = new MemoryStream())
//        {
//            using (var doc = new Document(PageSize.A4, 50, 50, 60, 60))
//            {
//                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

//                // Tambahkan event handler background
//                writer.PageEvent = new PdfBackgroundHelper(backgroundImagePath);

//                doc.Open();

//                using (var sr = new StringReader(html))
//                {
//                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
//                }

//                doc.Close();
//            }

//            return ms.ToArray();
//        }
//    }

//    public static byte[] GeneratePdfWithNoBackground(string html)
//    {
//        using (var ms = new MemoryStream())
//        {
//            using (var doc = new Document(PageSize.A4, 50, 50, 60, 60))
//            {
//                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

//                // Tambahkan event handler background
//                //writer.PageEvent = new PdfBackgroundHelper(backgroundImagePath);

//                doc.Open();

//                using (var sr = new StringReader(html))
//                {
//                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
//                }

//                doc.Close();
//            }

//            return ms.ToArray();
//        }
//    }

//}
