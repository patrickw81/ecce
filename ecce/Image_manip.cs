using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace ecce
{
    public class Image_manip
    {
        string imagePath;

        int org_height;
        int org_width;
        public int act_height;
        public int act_width;
        public int horizontal_dest_param=0;
        public int vertical_dest_param=0;
        public bool isbinarised = false;


        Image<Bgr, Byte> imgInput;
        public Image<Bgr, Byte> imgProcess{ get; set; }
        public Image<Bgr, Byte> before_bin { get; set; }


        public Image<Gray, Byte> imgBinarized { get; set; }

        public Image<Gray, Byte> imgGraywork { get; set; }


        public Image_manip(string path)
        {
            imagePath = path;
            imgInput = new Image<Bgr, byte>(path);
            org_height=imgInput.Height;
            org_width=imgInput.Width;
            act_height = org_height;
            act_width = org_width;
            //Vorab Binarize um Fehlermeldung bei Segmentierung zu umgehen
            imgBinarized = new Image<Gray, byte>(imgInput.Width, imgInput.Height, new Gray(0));
            CvInvoke.Threshold(imgInput.Convert<Gray, Byte>(), imgBinarized, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);

            image_handling(imgInput);
        }

        

       

        public void image_handling(Image<Bgr, Byte> img)
        {
            imgProcess = img;
            //imgGray = imgProcess.Convert<Gray, Byte>();
        }

        // Rotate not active!!
        public void rotate_img()
        {
            
        }

        // Resize Image
        public void resize_img(int width, int height)
        {
            imgProcess = imgProcess.Resize(width, height, Inter.Linear);
        }

        // Noise Reduction
        public void moise_reduction()
        {
            if (imgInput == null) return;
            Mat img = imgProcess.Mat;

            int searchWindowSize = 10;
            int templateWindowSize = 10;
            float h = 5;
            CvInvoke.FastNlMeansDenoising(imgProcess, img, h, searchWindowSize, templateWindowSize);
            image_handling(img.ToImage<Bgr, byte>());

        }

        // Sharp and GaussianBlur  Image
        public void sharpen()
        {
            if (imgInput == null) return;
            var img = new Image<Bgr, byte>(imgProcess.Width, imgProcess.Height);

            CvInvoke.GaussianBlur(imgProcess, img, new Size(5, 5), 0, 0);          

            float[,] kernel = new float[,] {
                 { -1, -1,-1 },
                 { -1, 9, -1 },
                 { -1, -1, -1 }
            };
            ConvolutionKernelF my_kernel = new ConvolutionKernelF(kernel);

            var outputImg = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.Filter2D(img, outputImg, my_kernel, new Point(-1, -1));
            image_handling(outputImg);
        }

        public void checkbinarized()
        {
            if (isbinarised)
            {
                imgProcess = before_bin.Copy();
            }
            else
            {
                before_bin = imgProcess.Copy();
            }
        }

        // Binarize Otsu
        public void otsubinarize()
        {
            checkbinarized();
            imgBinarized = new Image<Gray, byte>(imgProcess.Width, imgProcess.Height, new Gray(0));
            CvInvoke.Threshold(imgProcess.Convert<Gray, Byte>(), imgBinarized, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            image_handling(imgBinarized.Convert<Bgr, Byte>());
            isbinarised = true;
        }

        //Binarize-Adopted
        public void adoptedbinarize()
        {
            checkbinarized();
            imgBinarized = new Image<Gray, byte>(imgProcess.Width, imgProcess.Height, new Gray(0));
            CvInvoke.AdaptiveThreshold(imgProcess.Convert<Gray, Byte>(), imgBinarized, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 5, 10.0);
            image_handling(imgBinarized.Convert<Bgr, Byte>());
            isbinarised = true;
        }

        //Binarize Param
        public void binarizeConver(int min)
        {
            checkbinarized();
            imgBinarized = before_bin.Convert<Gray, byte>().ThresholdBinary(new Gray(min), new Gray(255));
            //image_handling(imgBinarized.Convert<Bgr, Byte>());
        }

        public void tablededection(int _hlength, int _vlength)
        {
            
            horizontal_dest_param = _hlength;
            vertical_dest_param = _vlength;
            //int length =(int)(img.Width*MorphTrehold/100);

            Mat vProfile = new Mat();
            Mat hProfile = new Mat();
            var kernelV = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, vertical_dest_param), new Point(-1, -1));
            var kernelH = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(horizontal_dest_param, 1), new Point(-1, -1));
            CvInvoke.Dilate(imgBinarized, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(hProfile, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));

            CvInvoke.Dilate(imgBinarized, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(vProfile, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));

            var megedImage = vProfile.ToImage<Gray, byte>().And(hProfile.ToImage<Gray, byte>());
            imgGraywork = imgBinarized.Or(megedImage.ThresholdBinaryInv(new Gray(245), new Gray(255)).Dilate(1));

            //imgGraywork = megedImage2;
            image_handling(imgGraywork.Convert<Bgr, Byte>());
        }
    }
}
