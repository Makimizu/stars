using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Drawing;
using System.IO;
using System.Text;

public static class WordGenerator
{
    public static byte[] GenerateWordFromHtml(string html)
    {
        Document document = new Document();

        // Convert HTML string ke MemoryStream
        using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
        {
            document.LoadFromStream(htmlStream, FileFormat.Html, XHTMLValidationType.None);
        }

        // Simpan ke MemoryStream format DOCX
        using (MemoryStream ms = new MemoryStream())
        {
            document.SaveToStream(ms, FileFormat.Docx);
            return ms.ToArray();
        }
    }



    public static byte[] GenerateWordWithBackground(string html, string backgroundImagePath)
    {
        // Bersihkan tag HTML luar
        html = html.Replace("<!DOCTYPE html>", "")
                   .Replace("<html>", "")
                   .Replace("</html>", "")
                   .Replace("<body>", "")
                   .Replace("</body>", "");

        // CSS reset + margin kiri-kanan untuk teks HTML
        html = "<style>body, p, table { margin:0; padding:0; font-size:10pt; }" +
               "body { margin-left: 40px; margin-right: 40px; margin-top: 100px;}" +
               "</style>" + html;

        Document doc = new Document();
        Section section = doc.AddSection();
        section.PageSetup.Margins.Top = 100f; // dalam point (1 inch = 72 point)
        // Ukuran halaman default (misalnya A4)
        float pageWidth = section.PageSetup.PageSize.Width;
        float pageHeight = section.PageSetup.PageSize.Height;

        // Scaling background (0.9 = 90% dari lebar halaman)
        float scalePercent = 1.0f;
        float imgWidth = pageWidth * scalePercent;
        float imgHeight = pageHeight * scalePercent;

        // Offset background (ubah sesuai kebutuhan)
        float offsetX = -55; // negatif = geser kiri
        float offsetY = -40; // negatif = geser atas

        // Hitung posisi background dengan offset
        float posX = (pageWidth - imgWidth) / 2 + offsetX;
        float posY = (pageHeight - imgHeight) / 2 + offsetY;

        // Masukkan background di header
        HeaderFooter header = section.HeadersFooters.Header;
        Paragraph headerPara = header.AddParagraph();

        using (Image bgImg = Image.FromFile(backgroundImagePath))
        {
            DocPicture pic = headerPara.AppendPicture(bgImg);
            pic.Width = imgWidth;
            pic.Height = imgHeight;
            pic.TextWrappingStyle = TextWrappingStyle.Behind;
            pic.HorizontalPosition = posX;
            pic.VerticalPosition = posY;
        }

        // Tambahkan isi HTML di body
        Paragraph htmlPara = section.AddParagraph();
        htmlPara.AppendHTML(html);

        using (MemoryStream ms = new MemoryStream())
        {
            doc.SaveToStream(ms, FileFormat.Docx);
            return ms.ToArray();
        }
    }

    public static byte[] GenerateWordWithBackgroundV2(string html, string backgroundImagePath)
    {
        // Bersihkan tag HTML luar (biar lebih rapi)
        html = html.Replace("<!DOCTYPE html>", "")
                   .Replace("<html>", "")
                   .Replace("</html>", "")
                   .Replace("<body>", "")
                   .Replace("</body>", "");

        // CSS default
        html = "<style>body, p, table { margin:0; padding:0; font-size:10pt; }" +
               "body { margin-left: 40px; margin-right: 40px; margin-top: 100px;}" +
               "</style>" + html;

        // --- Tambahkan gambar base64 (TTD & Logo) ---
        string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD.jpg");
        //string logoPath = "";//Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

        string tandaTanganBase64 = ConvertImageToBase64(tandaTanganPath);
        //string logoBase64 = ConvertImageToBase64(logoPath);

        html = html.Replace("{{TTD}}", tandaTanganBase64);
        //.Replace("{{LOGO}}", logoBase64);

        // --- Buat dokumen Word ---
        Document doc = new Document();
        Section section = doc.AddSection();
        section.PageSetup.Margins.Top = 60f;
        section.PageSetup.Margins.Bottom = 50f;

        float pageWidth = section.PageSetup.PageSize.Width;
        float pageHeight = section.PageSetup.PageSize.Height;

        // Background image (auto scale + offset)
        float scalePercent = 1.0f;   // ubah ke 0.9 kalau mau lebih kecil
        float offsetX = -50f;        // geser kiri-kanan
        float offsetY = -40f;        // geser atas-bawah

        float imgWidth = pageWidth * scalePercent;
        float imgHeight = pageHeight * scalePercent;
        float posX = (pageWidth - imgWidth) / 2 + offsetX;
        float posY = (pageHeight - imgHeight) / 2 + offsetY;

        HeaderFooter header = section.HeadersFooters.Header;
        Paragraph headerPara = header.AddParagraph();

        using (Image bgImg = Image.FromFile(backgroundImagePath))
        {
            DocPicture pic = headerPara.AppendPicture(bgImg);
            pic.Width = imgWidth;
            pic.Height = imgHeight;
            pic.TextWrappingStyle = TextWrappingStyle.Behind;
            pic.HorizontalPosition = posX;
            pic.VerticalPosition = posY;
        }

        // Tambahkan isi HTML
        Paragraph htmlPara = section.AddParagraph();
        htmlPara.AppendHTML(html);

        // Simpan ke memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            doc.SaveToStream(ms, FileFormat.Docx);
            return ms.ToArray();
        }
    }

    public static byte[] GenerateWordWithBackgroundV3(string html, string backgroundImagePath)
    {
        // Bersihkan tag HTML luar (biar lebih rapi)
        html = html.Replace("<!DOCTYPE html>", "")
                   .Replace("<html>", "")
                   .Replace("</html>", "")
                   .Replace("<body>", "")
                   .Replace("</body>", "");

        // CSS default
        html = "<style>body, p, table { margin:0; padding:0; font-size:10pt; }" +
               "body { margin-left: 40px; margin-right: 40px; margin-top: 100px;}" +
               "</style>" + html;

        // --- Tambahkan gambar base64 (TTD & Logo) ---
        string tandaTanganPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TTD2.jpg");
        //string logoPath = "";//Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "STEMPEL.jpg");

        string tandaTanganBase64 = ConvertImageToBase64(tandaTanganPath);
        //string logoBase64 = ConvertImageToBase64(logoPath);

        html = html.Replace("{{TTD}}", tandaTanganBase64);
        //.Replace("{{LOGO}}", logoBase64);

        // --- Buat dokumen Word ---
        Document doc = new Document();
        Section section = doc.AddSection();
        section.PageSetup.Margins.Top = 60f;
        section.PageSetup.Margins.Bottom = 50f;

        float pageWidth = section.PageSetup.PageSize.Width;
        float pageHeight = section.PageSetup.PageSize.Height;

        // Background image (auto scale + offset)
        float scalePercent = 1.0f;   // ubah ke 0.9 kalau mau lebih kecil
        float offsetX = -50f;        // geser kiri-kanan
        float offsetY = -40f;        // geser atas-bawah

        float imgWidth = pageWidth * scalePercent;
        float imgHeight = pageHeight * scalePercent;
        float posX = (pageWidth - imgWidth) / 2 + offsetX;
        float posY = (pageHeight - imgHeight) / 2 + offsetY;

        HeaderFooter header = section.HeadersFooters.Header;
        Paragraph headerPara = header.AddParagraph();

        using (Image bgImg = Image.FromFile(backgroundImagePath))
        {
            DocPicture pic = headerPara.AppendPicture(bgImg);
            pic.Width = imgWidth;
            pic.Height = imgHeight;
            pic.TextWrappingStyle = TextWrappingStyle.Behind;
            pic.HorizontalPosition = posX;
            pic.VerticalPosition = posY;
        }

        // Tambahkan isi HTML
        Paragraph htmlPara = section.AddParagraph();
        htmlPara.AppendHTML(html);

        // Simpan ke memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            doc.SaveToStream(ms, FileFormat.Docx);
            return ms.ToArray();
        }
    }

    public static byte[] GenerateWordWithImageCard(string html)
    {
        // Bersihkan tag HTML luar (biar lebih rapi)
        html = html.Replace("<!DOCTYPE html>", "")
                   .Replace("<html>", "")
                   .Replace("</html>", "")
                   .Replace("<body>", "")
                   .Replace("</body>", "");

        // CSS default
        html = "<style>body, p, table { margin:0; padding:0; font-size:10pt; }" +
               "body { margin-left: 40px; margin-right: 40px; margin-top: 100px;}" +
               "</style>" + html;

        // --- Tambahkan gambar base64 (Kartu) ---
        string kartu_1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TAKAFUL_FULLERTON.jpg");
        string kartu_2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TAKAFUL_ADMEDIKA.jpg");
        string kartu_3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TAKAFUL_VIP CUSTOMER.jpg");
        string kartu_4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TAKAFUL_HALODOC.jpg");

        string kartu_1_Base64 = ConvertImageToBase64(kartu_1);
        string kartu_2_Base64 = ConvertImageToBase64(kartu_2);
        string kartu_3_Base64 = ConvertImageToBase64(kartu_3);
        string kartu_4_Base64 = ConvertImageToBase64(kartu_4);

        html = html.Replace("{{KARTU1}}", kartu_1_Base64)
                    .Replace("{{KARTU2}}", kartu_2_Base64)
                    .Replace("{{KARTU3}}", kartu_3_Base64)
                    .Replace("{{KARTU4}}", kartu_4_Base64);
        //.Replace("{{LOGO}}", logoBase64);

        // --- Buat dokumen Word ---
        Document doc = new Document();
        Section section = doc.AddSection();
        section.PageSetup.Margins.Top = 60f;
        section.PageSetup.Margins.Bottom = 50f;

        float pageWidth = section.PageSetup.PageSize.Width;
        float pageHeight = section.PageSetup.PageSize.Height;

        // Background image (auto scale + offset)
        float scalePercent = 1.0f;   // ubah ke 0.9 kalau mau lebih kecil
        float offsetX = -50f;        // geser kiri-kanan
        float offsetY = -40f;        // geser atas-bawah

        float imgWidth = pageWidth * scalePercent;
        float imgHeight = pageHeight * scalePercent;
        float posX = (pageWidth - imgWidth) / 2 + offsetX;
        float posY = (pageHeight - imgHeight) / 2 + offsetY;

        HeaderFooter header = section.HeadersFooters.Header;
        Paragraph headerPara = header.AddParagraph();

        // Tambahkan isi HTML
        Paragraph htmlPara = section.AddParagraph();
        htmlPara.AppendHTML(html);

        // Simpan ke memory stream
        using (MemoryStream ms = new MemoryStream())
        {
            doc.SaveToStream(ms, FileFormat.Docx);
            return ms.ToArray();
        }
    }

    private static string ConvertImageToBase64(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        string base64 = Convert.ToBase64String(bytes);
        string ext = Path.GetExtension(path).ToLower().Replace(".", "");
        return "data:image/" + ext + ";base64," + base64;
    }
}




