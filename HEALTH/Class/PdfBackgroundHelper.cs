using System.IO;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Geom;

public class PdfBackgroundHandler : IEventHandler
{
    private readonly ImageData backgroundImage;

    public PdfBackgroundHandler(string imagePath)
    {
        if (File.Exists(imagePath))
        {
            backgroundImage = ImageDataFactory.Create(imagePath);
        }
    }

    public void HandleEvent(Event @event)
    {
        if (backgroundImage == null) return;

        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
        PdfDocument pdfDoc = docEvent.GetDocument();
        PdfPage page = docEvent.GetPage();
        Rectangle pageSize = page.GetPageSize();

        var canvas = new iText.Kernel.Pdf.Canvas.PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

        // Tambahkan gambar background
        canvas.AddImage(backgroundImage, pageSize.GetWidth(), 0, // Lebar gambar
            0, pageSize.GetHeight(), // Tinggi gambar
            0, 0, // Posisi X,Y
            false);
    }
}
