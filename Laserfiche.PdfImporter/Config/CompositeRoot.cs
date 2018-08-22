using Laserfiche.PdfImporter.Helpers;
using Laserfiche.PdfImporter.Repository;
using Laserfiche.PdfImporter.Services;
using PdfToImage;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace Laserfiche.PdfImporter.Config
{
    public class CompositeRoot
    {

        private readonly Container _container;

        public CompositeRoot()
        {
            _container = new Container();
        }

        public void Init()
        {
            _container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

            _container.RegisterSingleton<IAppSettings, AppSettings>();
            _container.Register<PDFConvert>(Lifestyle.Scoped);
            _container.Register<ILaserficheRepository, LaserficheRepository>(Lifestyle.Scoped);
            _container.Register<IPdfImporter, PdfImportService>(Lifestyle.Scoped);

            _container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            _container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);
        }
    }
}