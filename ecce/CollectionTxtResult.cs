using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ecce
{
    internal class CollectionTxtResult
    {
        public string Form_text { get; set; } = "";
        public string Text { get; set; } = "";
        public string Summary { get; set; } = "";
        public string Person { get; set; } = "";
        public string Place { get; set; } = "";
        public string Time { get; set; } = "";
        public string Title { get; set; } = "";
        public string Classifikation { get; set; } = "";
        public string StrSha256 { get; set; } = "";
        public string StrSha512 { get; set; } = "";
        public string ImgPath { get; set; } = "";
        public int ImgWidth { get; set; }
        public int ImgHeight { get; set; }
        public string FileName { get; set; } = "";
    }
}
