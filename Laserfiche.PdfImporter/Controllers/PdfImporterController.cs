using Laserfiche.PdfImporter.Helpers;
using Laserfiche.PdfImporter.Services;
using System.Web.Http;

namespace Laserfiche.PdfImporter.Controllers
{
    public class PdfImporterController : ApiController
    {

        private readonly IPdfImporter _pdfImporter;
        private readonly string _pdfPath;

        public PdfImporterController(IPdfImporter pdfImporter, IAppSettings appSettings)
        {
            _pdfImporter = pdfImporter;
            _pdfPath = appSettings.GetString("app.pdfpath");
        }

        public IHttpActionResult Post()
        {

            _pdfImporter.Import(_pdfPath);
                        
            return Ok();
        }

        
    }
}