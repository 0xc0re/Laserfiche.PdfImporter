using Laserfiche.DocumentServices;
using Laserfiche.PdfImporter.Helpers;
using Laserfiche.RepositoryAccess;
using System;

namespace Laserfiche.PdfImporter.Repository
{

    public class LaserficheRepository : ILaserficheRepository, IDisposable
    {

        private readonly Session _session;

        public LaserficheRepository(IAppSettings appSetting)
        {
            var server = appSetting.GetString("lf.server");
            var repository = appSetting.GetString("lf.repository");
            var user = appSetting.GetString("lf.user");
            var password = appSetting.GetString("lf.password");
            var repositoryRegistration = new RepositoryRegistration(server, repository);

            _session = new Session();
            _session.LogIn(user, password, repositoryRegistration);
        }

        public void ImportDocument(string laserfichePath, string volume, string filePath)
        {
            using (DocumentInfo document = new DocumentInfo(_session))
            {
                document.Create(laserfichePath, volume, EntryNameOption.AutoRename);

                DocumentImporter importer = new DocumentImporter
                {
                    Document = document,
                    OcrImages = true
                };
                importer.ImportImages(filePath);
            }
        }

        public void Dispose()
        {
            _session?.Close();
            _session?.Discard();
        }

    }
}