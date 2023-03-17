using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ecce
{
    internal class Ocr_Dedect
    {
        Tesseract myocr = new Tesseract();


        public Ocr_Dedect(string pagemd, string language = "deu",string engin = "Default")
        {
            try
            {
                OcrEngineMode myenginMode = OcrEngineMode.Default;

                if (Enum.TryParse(engin, out OcrEngineMode enginMode))
                {
                     myenginMode = enginMode;
                }
               

                myocr.Init(@"./tessdata/", language, myenginMode);

                if (Enum.TryParse(pagemd, out PageSegMode pageSegMode))
                {
                    myocr.PageSegMode = pageSegMode;
                }
                else
                {
                    myocr.PageSegMode = PageSegMode.Auto;
                }


            }
            catch(Exception ex) { }
           
        }

        public string readtxt_UTF8 (Image<Gray, byte> img)
        {
            Debug.WriteLine("aaaaaaaaaaaaaa");
            myocr.SetImage(img);
            string text = myocr.GetUTF8Text();
            return text;
        }
        public string readtxt_UTF82(Image<Bgr, byte> img)
        {
            myocr.SetImage(img);
            string text = myocr.GetUTF8Text();
            return text;
        }

       

    }
}
