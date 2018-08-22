namespace Laserfiche.PdfImporter.Services
{
    public interface IPdfImporter
    {
        bool Import(string pdfPath);
    }
}