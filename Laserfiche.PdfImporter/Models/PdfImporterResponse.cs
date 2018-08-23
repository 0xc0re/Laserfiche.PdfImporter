using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laserfiche.PdfImporter.Models
{
    public class PdfImporterResponse
    {
        public string PdfPath { get; set; }
        public string LaserfichePath { get; set; }
        public int TotalFilesNoImported { get; set; }
        public int TotalFilesImported { get; set; }
        public List<string> FilesImported { get; set; }
        public List<string> FilesNoImported { get; set; }
    }
}