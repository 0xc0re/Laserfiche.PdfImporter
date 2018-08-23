using Laserfiche.DocumentServices;
using Laserfiche.PdfImporter.Helpers;
using Laserfiche.RepositoryAccess;
using Nuaguil.Security;
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
            //_session.LogIn(user, password, repositoryRegistration);
            var impersonate = new ImpersonationHelper("ssf", user, password);
            impersonate.Impersonate(() => _session.LogIn(repositoryRegistration));
        }

        public void ImportDocument(string laserfichePath, string volume, string filePath)
        {
            using (EntryInfo entry = Entry.TryGetEntryInfo(laserfichePath, _session) ?? new DocumentInfo(_session))
            {
                if (!(entry is DocumentInfo document))
                    throw new Exception($"Error creating laserfiche document {laserfichePath}");

                if(document.IsNew)
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