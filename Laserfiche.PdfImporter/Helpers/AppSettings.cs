using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;

namespace Laserfiche.PdfImporter.Helpers
{
    public class AppSettings : IAppSettings
    {
        private readonly NameValueCollection _settings;

        public AppSettings()
        {
            _settings = ConfigurationManager.AppSettings;
        }


        public string GetString(string key)
        {
            return _settings[key];
        }

        public int GetInt32(string key)
        {
            return Convert.ToInt32(_settings[key]);
        }

        public long GetInt64(string key)
        {
            return Convert.ToInt64(_settings[key]);
        }
        public bool GetBoolean(string key)
        {
            return bool.Parse(_settings[key]);
        }

        public DateTime GetDateTime(string key, string format)
        {
            return DateTime.ParseExact(_settings[key], format,CultureInfo.InvariantCulture);
        }
    }
}