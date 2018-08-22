using System;

namespace Laserfiche.PdfImporter.Helpers
{
    public interface IAppSettings
    {
        bool GetBoolean(string key);
        DateTime GetDateTime(string key, string format);
        int GetInt32(string key);
        long GetInt64(string key);
        string GetString(string key);
    }
}