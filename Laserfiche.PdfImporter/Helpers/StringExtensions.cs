using System.Text.RegularExpressions;

namespace Laserfiche.PdfImporter.Helpers
{
    public static class StringExtensions
    {

        public static string ReplaceLatinChars(this string value)
        {
            value = value?.ToLower();
            if (value == null) return null;
            return Regex.Replace(value, @"([áéíóúñ])", x =>
            {
                if (x.Value == "á")
                    return "a";
                if (x.Value == "é")
                    return "e";
                if (x.Value == "í")
                    return "i";
                if (x.Value == "ó")
                    return "o";
                if (x.Value == "ú")
                    return "u";
                if (x.Value == "ñ")
                    return "n";
                return x.Value;
            });
        }

    }
}