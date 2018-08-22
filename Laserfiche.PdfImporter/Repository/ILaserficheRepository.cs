namespace Laserfiche.PdfImporter.Repository
{
    public interface ILaserficheRepository
    {
        void ImportDocument(string laserfichePath, string volume, string filePath);
    }
}