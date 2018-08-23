using Laserfiche.PdfImporter.Helpers;
using Laserfiche.PdfImporter.Models;
using Laserfiche.PdfImporter.Repository;
using PdfToImage;
using System.IO;

namespace Laserfiche.PdfImporter.Services
{
    public class PdfImportService : IPdfImporter
    {
        private readonly PDFConvert _converter;
        private readonly ILaserficheRepository _laserficheRepository;
        private readonly IAppSettings _appSettings;

        public PdfImportService(PDFConvert converter, 
            IAppSettings appSettings,
            ILaserficheRepository laserficheRepository)
        {
            _converter = converter;
            _appSettings = appSettings;
            _laserficheRepository = laserficheRepository;

            _converter.OutputFormat = "tiffg4";
        }

        public PdfImporterResponse Import(string pdfPath)
        {
            if (!Directory.Exists(pdfPath))
                throw new System.ArgumentException($"Path {pdfPath} not exists");

            var lfPath = _appSettings.GetString("lf.path");
            var lfVolume = _appSettings.GetString("lf.volume");
            var tifPath = Path.Combine(pdfPath, "tif");

            if (!Directory.Exists(tifPath))
                Directory.CreateDirectory(tifPath);

            foreach (var file in Directory.EnumerateFiles(pdfPath, "*.pdf"))
            {
                var fileInfo = new FileInfo(file);
                var tifFile = Path.Combine(tifPath, fileInfo.Name.Replace(fileInfo.Extension, ".tif"));
                if (!_converter.Convert(file, tifFile))
                {
                    continue;
                }
                _laserficheRepository.ImportDocument(
                    $@"{lfPath}\{Path.GetFileNameWithoutExtension(fileInfo.Name)}",
                    lfVolume, tifFile);
            }

            return new PdfImporterResponse();
        }
        
    }
}