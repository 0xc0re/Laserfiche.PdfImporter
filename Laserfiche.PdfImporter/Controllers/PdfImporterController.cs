using Laserfiche.PdfImporter.Helpers;
using Laserfiche.PdfImporter.Services;
using System.Web.Http;

namespace Laserfiche.PdfImporter.Controllers
{
    /// <summary>
    /// Pdf importer
    /// Convert and import to laserfiche repository
    /// </summary>
    public class PdfImporterController : ApiController
    {

        private readonly IPdfImporter _pdfImporter;
        private readonly string _pdfPath;

        public PdfImporterController(IPdfImporter pdfImporter, IAppSettings appSettings)
        {
            _pdfImporter = pdfImporter;
            _pdfPath = appSettings.GetString("app.pdfpath");
        }

        /// <summary>
        /// Convert pdf to tiff image and import them to laserfiche repository 
        /// </summary>
        /// <returns>The statistics of the files imported and no imported</returns>
        public IHttpActionResult Post()
        {
            var response = _pdfImporter.Import(_pdfPath);
            return Ok(response);
        }

        
    }
}