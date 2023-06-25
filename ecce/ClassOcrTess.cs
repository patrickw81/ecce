using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ecce
{
    internal class ClassOcrTess
    {
        
        private  TesseractEngine? Ocr_eng { get; set; }
        private Tesseract.PageSegMode Pages_mod { get; set;}

        public ClassOcrTess(string pagemd = "Auto", string language = "deu", string engin = "Default")
        {
            try
            {
                Tesseract.EngineMode myenginMode = Tesseract.EngineMode.TesseractOnly;

                if (Enum.TryParse(engin, out Tesseract.EngineMode enginMode))
                {
                    myenginMode = enginMode;
                }
                Ocr_eng = new TesseractEngine(@"./tessdata", language, myenginMode);
                Pages_mod = Tesseract.PageSegMode.Auto;
               

                if (Enum.TryParse(pagemd, out Tesseract.PageSegMode pagMode))
                {
                    Pages_mod = pagMode;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        public string Readtxt_UTF8(Image<Gray, byte> img)
        {
            var bitmap = img.ToBitmap();
            Tesseract.Pix pix;
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = memoryStream.ToArray();
                pix = Tesseract.Pix.LoadFromMemory(bytes);

            }
            string text;
            using (var page = Ocr_eng!.Process(pix, Pages_mod))
            {
                text = page.GetText();
                text= text.Replace("\n", "\r\n");
            }
            return text;
        }
    }
}