using Laserfiche.PdfImporter.Models;

namespace Laserfiche.PdfImporter.Services
{
    public interface IPdfImporter
    {
        PdfImporterResponse Import(string pdfPath);
    }
}