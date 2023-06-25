using Emgu.CV.Aruco;
using Emgu.CV.Flann;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class CollectionAreaResult
    {
        public Dictionary<string, string> DictTxtperArea;
        public string StrSha512 { get; set; } = "";
        public string StrSha256 { get; set; } = "";
        public string ImgPath { get; set; } = "";
        public int ImgWidth { get; set; }
        public int ImgHeight { get; set; }
        public string FileName { get; set; } = "";
        public List<Rectangle> Rectangles { get; set; } = new();
        public CollectionAreaResult(string strsha256, string strsha512, string path, int width, int height)
        {
            DictTxtperArea = new Dictionary<string, string>();
            StrSha512 = strsha512;
            StrSha256 = strsha256;
            ImgPath = path;
            ImgWidth = width;
            ImgHeight = height;
            GetFilename();
        }

        private void GetFilename()
        {
            string[] str = ImgPath.Split("\\");
            FileName = str[^1];
        }
    }
}
