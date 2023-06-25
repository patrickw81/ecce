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
using Emgu.CV.ML;
using Tesseract;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.IO.Pipes;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.Arm;

//using System.Windows.Forms.VisualStyles;

namespace ecce
{
    internal class ClassImage
    {
        public string ImagePath { get; set; }
        public int OriginalHeight { get; set; }
        public int OriginalWidth { get; set; }
        public int ParamBinarized { get; set; } = 0;
        public int RoundResizedFactor { get; set; }
        public int ActualHeight { get; set; }
        public int ActualWidth { get; set; }
        public int ParamBlackDotsDestruction { get; set; } = 1;
        public int ParamHorizontalDestruction { get; set; } = 1;
        public int ParamVerticalDestruction { get; set; } = 1;
        public bool CheckBinarized { get; set; } = false;
        public string StrSHA512 { get; set; } = "";
        public string StrSHA256 { get; set; } = "";
        public int[]? ColumnHorizontalLine {get; set;}
        public VectorOfVectorOfPoint Contours { get; set; } = new ();
        List<CollectionSegAndContour> LstBoxandCountours { get; set; } = new ();
        public List<CollectionSegmentBox> ListSegmentationBox { get; set; } = new ();
        public ClassSelectedAreas SelecteAreas { get; set;}= new ();
        public Image<Bgr, Byte> ImgProcess { get; set; }
       // public Image<Bgr, Byte>? ImgBeforeBin { get; set; }
        public Image<Gray, Byte> ImgBinarized { get; set; }

        public ClassImage(string path)
        {
            ImagePath = path;
            GetChecksum512();
            GetChecksum256();
            ImgProcess = new Image<Bgr, byte>(path);
            OriginalHeight = ImgProcess.Height;
            OriginalWidth = ImgProcess.Width;
            ActualHeight = OriginalHeight;
            ActualWidth = OriginalWidth;

            //Vorab Binarize um Fehlermeldung bei Segmentierung zu umgehen
            ImgBinarized = new Image<Gray, byte>(ImgProcess.Width, ImgProcess.Height, new Gray(0));
            CvInvoke.Threshold(ImgProcess.Convert<Gray, Byte>(), ImgBinarized, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
        }

        private void GetChecksum512()
        {
            using SHA512 sha512 = SHA512.Create();
            using FileStream fileStream = File.OpenRead(ImagePath);
            byte[] hashValue = sha512.ComputeHash(fileStream);
            StrSHA512 = BitConverter.ToString(hashValue).Replace("-", String.Empty);       
        }
        private void GetChecksum256()
        {
            using SHA256 sha256 = SHA256.Create();
            using FileStream fileStream = File.OpenRead(ImagePath);
            byte[] hashValue = sha256.ComputeHash(fileStream);
            StrSHA256 = BitConverter.ToString(hashValue).Replace("-", String.Empty);
        }


        // Resize Image
        public Image<Bgr, Byte> GetNewSize(int width, int height)
        {
            RoundResizedFactor = (int)Math.Round((double)width/ OriginalWidth * 100);
            ImgProcess = ImgProcess.Resize(width, height, Inter.Linear);
            ActualHeight = height;
            ActualWidth= width;
            return ImgProcess;
        }

        // Noise Reduction
        public Image<Bgr, Byte> GetNoiseReduction()
        {
            //Mat img = imgProcess.Mat;
            Image<Bgr, Byte> outimg = new (ImgProcess.Width, ImgProcess.Height);
            int searchWindowSize = 10;
            int templateWindowSize = 10;
            float h = 5;
            CvInvoke.FastNlMeansDenoising(ImgProcess, outimg, h, searchWindowSize, templateWindowSize);
            ImgProcess = outimg.Copy();
            return ImgProcess;
        }

        // Sharp and GaussianBlur  Image
        public Image<Bgr, Byte> GetSharp()
        {
            CvInvoke.GaussianBlur(ImgProcess, ImgProcess, new Size(5, 5), 0, 0);
            float[,] kernel = new float[,] {
                 { -1, -1,-1 },
                 { -1, 9, -1 },
                 { -1, -1, -1 }
            };
            ConvolutionKernelF my_kernel = new (kernel);
            CvInvoke.Filter2D(ImgProcess, ImgProcess, my_kernel, new Point(-1, -1));
            return ImgProcess;
        }
        /*
        public void CheckStatusBinarize()
        {
           
            if (CheckBinarized)
            {
                ImgProcess = ImgBeforeBin!.Copy();
            }
            else
            {
                ImgBeforeBin = ImgProcess.Copy();
            }
        }
        */
        // Binarize Otsu
        public Image<Gray, Byte> GetOtsuBinarize()
        {
            //CheckStatusBinarize();
            ImgBinarized = new Image<Gray, byte>(ImgProcess.Width, ImgProcess.Height, new Gray(0));
            CvInvoke.Threshold(ImgProcess.Convert<Gray, Byte>(), ImgBinarized, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            //ImgProcess = ImgBinarized.Convert<Bgr, Byte>().Copy();
            CheckBinarized = true;
            return ImgBinarized;
        }

        //Binarize-Adopted
        public Image<Gray, Byte> GetAdoptedBinarize()
        {
            //CheckStatusBinarize();
            ImgBinarized = new Image<Gray, byte>(ImgProcess.Width, ImgProcess.Height, new Gray(0));
            CvInvoke.AdaptiveThreshold(ImgProcess.Convert<Gray, Byte>(), ImgBinarized, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 5, 10.0);
            //ImgProcess = ImgBinarized.Convert<Bgr, Byte>().Copy();
            CheckBinarized = true;
            return ImgBinarized;
        }

        //Binarize Param
        public void GetBinarizePerParam(int min)
        {
            //CheckStatusBinarize();
            //ImgBinarized = ImgBeforeBin!.Convert<Gray, byte>().ThresholdBinary(new Gray(min), new Gray(255));
            ImgBinarized = ImgProcess!.Convert<Gray, byte>().ThresholdBinary(new Gray(min), new Gray(255));
            CheckBinarized = true;
        }

        // Remove Line
        public Image<Gray, Byte> RemoveLine(int _hlength, int _vlength)
        {
            ParamHorizontalDestruction = _hlength;
            ParamVerticalDestruction = _vlength;
            Mat vProfile = new ();
            Mat hProfile = new ();
            var kernelV = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, ParamVerticalDestruction), new Point(-1, -1));
            var kernelH = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(ParamHorizontalDestruction, 1), new Point(-1, -1));
            CvInvoke.Dilate(ImgBinarized, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(hProfile, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Dilate(ImgBinarized, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(vProfile, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));

            var megedImage = vProfile.ToImage<Gray, byte>().And(hProfile.ToImage<Gray, byte>());

            Image<Gray, Byte> imgmanipulate = ImgBinarized.Or(megedImage.ThresholdBinaryInv(new Gray(245), new Gray(255)).Dilate(1));
            return imgmanipulate;
        }

        //Remove Black Frame
        public Image<Gray, Byte> RemoveBlackFrame()
        {
            VectorOfVectorOfPoint contours = new ();
            var h = new Mat();
            CvInvoke.FindContours(ImgBinarized, contours, h, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            Image<Gray, Byte> whiteImage = new(ImgBinarized.Width, ImgBinarized.Height);
            whiteImage.SetValue(new Gray(255).MCvScalar);
            List<Rectangle> rects = new ();
            Image<Gray, Byte> mask = new (ImgBinarized.Width, ImgBinarized.Height);
            
            List<VectorOfPoint> contourList = new();
            for (int i = 0; i < contours.Size; i++)
            {
                var bbox = CvInvoke.BoundingRectangle(contours[i]);
               
            }
           
            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                double area = CvInvoke.ContourArea(contour);
                if (area > 0)
                {
                    contourList.Add(contour);
                }
            }
            contourList = contourList.OrderByDescending(c => CvInvoke.ContourArea(c)).ToList();
            VectorOfVectorOfPoint contours2 = new ();
            contours2.Push(contourList[0]);

            Image<Gray, Byte> test = new (ImgBinarized.Width, ImgBinarized.Height);
            test = ImgBinarized.Copy();

            mask = new Image<Gray, Byte>(ImgBinarized.Width, ImgBinarized.Height);
            CvInvoke.DrawContours(mask, contours2, 0, new MCvScalar(255), -1);
            int dilationSize = 5;
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(dilationSize, dilationSize), new Point(-1, -1));
            CvInvoke.Erode(mask, mask, element, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

            CvInvoke.BitwiseAnd(test, mask, test);
            CvInvoke.BitwiseAnd(test, whiteImage, whiteImage, mask);
            ImgBinarized = whiteImage;
            return ImgBinarized;
        }
        //Skew /Skip
        public Image<Bgr, byte> SetDeskew(bool frame)
        {

            Image<Gray, Byte> gray;

            if (frame)
            {
                gray = ImgProcess.Convert<Gray, Byte>().ThresholdBinary(new Gray(200), new Gray(255)).Dilate(9);
            }
            else
            {
                gray = ImgProcess.Convert<Gray, Byte>().ThresholdBinaryInv(new Gray(200), new Gray(255)).Dilate(9);
            }

            List<RotatedRect> myrotadetrect = new ();
            VectorOfVectorOfPoint contours = new ();

            Mat hierarchy = new ();
            CvInvoke.FindContours(gray, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < contours.Size; i++)
            {
                var bbox = CvInvoke.BoundingRectangle(contours[i]);
                myrotadetrect.Add(CvInvoke.MinAreaRect(contours[i]));
            }

            myrotadetrect = myrotadetrect.OrderByDescending(x => (x.Size.Width * x.Size.Height)).ToList();

            var minarea = myrotadetrect[0];
            var rotationMatrix = new Mat(new Size(2, 3), Emgu.CV.CvEnum.DepthType.Cv32F, 1);
           
            RotatedRect myangle = new ();
            if (minarea.Angle >= 45)
            {
                myangle.Angle = minarea.Angle - 90;
            }
            else
            {
                myangle.Angle = minarea.Angle;
            }


            CvInvoke.GetRotationMatrix2D(minarea.Center, myangle.Angle, 1.0, rotationMatrix);
            CvInvoke.WarpAffine(ImgProcess, ImgProcess, rotationMatrix, ImgProcess.Size, Emgu.CV.CvEnum.Inter.Cubic, borderMode: Emgu.CV.CvEnum.BorderType.Replicate);

            if (CheckBinarized == false)
            {
                CvInvoke.Threshold(ImgProcess.Convert<Gray, Byte>(), ImgBinarized, 100, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
                return ImgProcess;
            }
            else
            {
                CvInvoke.WarpAffine(ImgBinarized, ImgBinarized, rotationMatrix, ImgBinarized.Size, Emgu.CV.CvEnum.Inter.Cubic, borderMode: Emgu.CV.CvEnum.BorderType.Replicate);
                return ImgBinarized.Convert<Bgr,Byte>();
            }
         }

        //Remove Dots
        public Image<Gray, byte> RemoveBlackDots(Image<Gray, Byte> imginput,int destroy_rec)
        {
            try
            {
                Image<Gray, Byte> img = imginput;
                Mat kernel = Mat.Ones(1, 1, DepthType.Cv8U, 1);
                var binary = img.MorphologyEx(MorphOp.Erode, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
                CvInvoke.BitwiseNot(binary, binary);
                var h = new Mat();
                CvInvoke.FindContours(binary, Contours, h, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                for (int i = 0; i < Contours.Size; i++)
                {
                    var bbox = CvInvoke.BoundingRectangle(Contours[i]);
                    int area = bbox.Width * bbox.Height;

                    if (area < destroy_rec)
                    {
                        CvInvoke.DrawContours(img, Contours, i, new MCvScalar(255, 255, 255), -1);
                    }
                }
                //CvInvoke.BitwiseNot(binary, binary);
                return img;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // StrongLines
        public Image<Gray, Byte> SetLineStrength()
        {
            Mat structuringElement = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(2, 2), new Point(-1, -1));

            CvInvoke.Erode(ImgBinarized, ImgBinarized, structuringElement, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0));
            CvInvoke.Dilate(ImgBinarized, ImgBinarized, structuringElement, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0));
            return ImgBinarized;
        }
        // WeakLines
        public Image<Gray, Byte> SetLineWeakness()
        {
            Mat structuringElement = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(2, 2), new Point(-1, -1));
            CvInvoke.Erode(ImgBinarized, ImgBinarized, structuringElement, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0));
            CvInvoke.Dilate(ImgBinarized, ImgBinarized, structuringElement, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0));
            return ImgBinarized;
        }

        //Image Segmentation
        //
        //
        public Image<Bgr, byte> SetSegmentation(int width=62, int height=52, int destroy_rec=1000)
        {
            try
            {
                LstBoxandCountours.Clear();
                ListSegmentationBox.Clear();
                Contours.Clear();
               
                Mat kernel = Mat.Ones(height, width, DepthType.Cv8U, 1);

                var binary = ImgBinarized.MorphologyEx(MorphOp.Erode, kernel, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 0, 0));
                CvInvoke.BitwiseNot(binary, binary);
                var h = new Mat();
                CvInvoke.FindContours(binary, Contours, h, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                for (int i = 0; i < Contours.Size; i++)
                {
                    var bbox = CvInvoke.BoundingRectangle(Contours[i]);
                    int area = bbox.Width * bbox.Height;

                    if (area > destroy_rec)
                    {
                        CollectionSegAndContour TempClass = new() { 
                            Rect = bbox,
                            IdxContour = i
                        };
                        LstBoxandCountours.Add(TempClass);
                    }
                }
                if (LstBoxandCountours.Count > 0)
                {
                    LstBoxandCountours = LstBoxandCountours.OrderBy(x => x.Rect.Y).ThenBy(x => x.Rect.X).ToList();
                    SortBoxexTableLeftRight();
                    return DrawSegmentationBoxes();
                }
                else
                {
                    return ImgBinarized.Convert<Bgr, byte>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);               
            }
        }
        public void SortBoxexTableLeftRight()
        {
            List<CollectionSegAndContour>[] TempListBoxandCountur = new List<CollectionSegAndContour>[LstBoxandCountours.Count];
            TempListBoxandCountur[0] = new ();
            TempListBoxandCountur[0].Add(LstBoxandCountours[0]);
            int chckbottom = TempListBoxandCountur[0][0].Rect.Bottom;

            int x = 0;
            for (int i = 1; i < LstBoxandCountours.Count; i++)
            {
                if (chckbottom > (LstBoxandCountours[i].Rect.Top + 5))
                {
                    TempListBoxandCountur[x].Add(LstBoxandCountours[i]);
                    if (LstBoxandCountours[i].Rect.Bottom > chckbottom) { chckbottom = LstBoxandCountours[i].Rect.Bottom; }
                }
                else
                {
                    x++;
                    TempListBoxandCountur[x] = new ();
                    TempListBoxandCountur[x].Add(LstBoxandCountours[i]);
                    chckbottom = LstBoxandCountours[i].Rect.Bottom;
                }
            }

            ColumnHorizontalLine = new int[(x + 1)];
            int _box_column = 0;
            int _box_row = x + 1;
            List<CollectionSegAndContour>[] Temlist2 = new List<CollectionSegAndContour>[TempListBoxandCountur.Length];
            ListSegmentationBox = new List<CollectionSegmentBox>();
            for (int i = 0; i <= x; i++)
            {
                Temlist2[i] = TempListBoxandCountur[i].OrderByDescending(x => x.Rect.Bottom).ToList();
                ColumnHorizontalLine[i] = Temlist2[i][0].Rect.Bottom;
                TempListBoxandCountur[i] = TempListBoxandCountur[i].OrderBy(x => x.Rect.X).ToList();
                for (int y = 0; y < TempListBoxandCountur[i].Count; y++)
                {
                    ListSegmentationBox.Add(new CollectionSegmentBox() { Rect = TempListBoxandCountur[i][y].Rect, Column = i, RowOfBox = y, IdxContour = TempListBoxandCountur[i][y].IdxContour });
                    if (y > _box_column)
                    {

                        _box_column = y;
                        //_index_row = i;
                    }
                }
            }
            _box_column++;
        }
        public Image<Bgr, byte> DrawSegmentationBoxes()
        {
            Image<Bgr, Byte> showimg = ImgBinarized.Convert<Bgr, byte>().Copy();
            if (ListSegmentationBox.Count > 0)
            {
                for (int i = 0; i < LstBoxandCountours.Count; i++)
                {
                    CvInvoke.Rectangle(showimg, ListSegmentationBox[i].Rect, new MCvScalar(200, 205, 0), 2);
                    CvInvoke.DrawContours(showimg, Contours, ListSegmentationBox[i].IdxContour, new MCvScalar(50, 50, 200), 1);
                    CvInvoke.PutText(showimg, i.ToString(), new Point((ListSegmentationBox[i].Rect.X + ListSegmentationBox[i].Rect.Width / 2), ListSegmentationBox[i].Rect.Y), FontFace.HersheyPlain, 2, new MCvScalar(20, 50, 255));
                }
            }
            return showimg;
        }
        public void DeleteSegmentation() {
            LstBoxandCountours.Clear();
            ListSegmentationBox.Clear();
            Contours.Clear();
            SelecteAreas.LstAreas.Clear();
            SelecteAreas.LstTagNames.Clear();
        }

        public Image<Bgr, byte> GetSegmentationTess(Tesseract.PageIteratorLevel pagelevl = Tesseract.PageIteratorLevel.Para, string language = "deu", string engin = "Default")
        {
            LstBoxandCountours.Clear();
            ListSegmentationBox.Clear();
            Contours.Clear();

            try
            {
                Tesseract.EngineMode myenginMode = Tesseract.EngineMode.Default;

                if (Enum.TryParse(engin, out Tesseract.EngineMode enginMode))
                {
                    myenginMode = enginMode;
                }
            }
            catch { }


            TesseractEngine ocrseg = new (@"./tessdata", language, Tesseract.EngineMode.LstmOnly);
            Tesseract.PageSegMode mymode = Tesseract.PageSegMode.Auto;

            var bitmap = ImgBinarized.ToBitmap();
            Tesseract.Pix pix;
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = memoryStream.ToArray();
                pix = Tesseract.Pix.LoadFromMemory(bytes);

            }
            
            Image<Bgr, Byte> showimg = ImgBinarized.Copy().Convert<Bgr, byte>();
            var page = ocrseg.Process(pix, mymode);
            Tesseract.PageIterator box = page.AnalyseLayout();

            box.Begin();
            int i = 0;
            do
            {
                if (box.TryGetBoundingBox(pagelevl, out Tesseract.Rect b))
                {
                    Rectangle rect = new ()
                    {
                        X = b.X1,
                        Y = b.Y1,
                        Width = b.Width,
                        Height = b.Height
                    };

                    //Point left = new (rect.X);
                    VectorOfPoint n = new ();
                    n.Push(new Point[] { new Point(rect.Left, rect.Top) });
                    n.Push(new Point[] { new Point(rect.Right, rect.Top) });
                    n.Push(new Point[] { new Point(rect.Right, rect.Bottom) });
                    n.Push(new Point[] { new Point(rect.Left, rect.Bottom) });

                    Contours.Push(n);
                    CvInvoke.Rectangle(showimg, rect, new MCvScalar(200, 205, 0), 1);
                    CvInvoke.PutText(showimg, i.ToString(), new Point((rect.X + rect.Width / 2), rect.Y), FontFace.HersheyPlain, 2, new MCvScalar(20, 50, 255));
                    ListSegmentationBox.Add(new CollectionSegmentBox() { Rect = rect, Column = i, RowOfBox = 0, IdxContour = i });
                    i++;
                }
            } while (box.Next(pagelevl));
            return showimg;
        }
        public Image<Bgr, Byte> ShowColumn()
        {
            if (ColumnHorizontalLine == null) return ImgBinarized.Convert<Bgr, byte>();
            
            Image<Bgr, Byte> showimg = DrawSegmentationBoxes();
            for (int i = 0; i < ColumnHorizontalLine.Length - 1; i++)
            {
                CvInvoke.Line(showimg, new Point(1, ColumnHorizontalLine[i]), new Point(showimg.Width, ColumnHorizontalLine[i]), new MCvScalar(20, 50, 255), 1, LineType.AntiAlias);
                
            }
            return showimg;
         }
        public void SortXtoY()
        {
            ListSegmentationBox = ListSegmentationBox.OrderBy(x => x.Rect.X).ThenBy(y => y.Rect.Y).ToList();
        }
        public void SortYtoX()
        {
            ListSegmentationBox = ListSegmentationBox.OrderBy(x => x.Rect.Y).ToList();
        }
        public Image<Gray, Byte> GetSegmentImageWithMask(int idx)
        {
            SelecteAreas.LstAreas = SelecteAreas.LstAreas.OrderBy(x => x.Rect.Y).ThenBy(y => y.Rect.X).ToList();
            Image<Gray, Byte> imgroi = new (ImgBinarized.Width, ImgBinarized.Height);
            imgroi = ImgBinarized.Copy();
            imgroi.ROI = ListSegmentationBox[idx].Rect;
            CvInvoke.BitwiseNot(imgroi, imgroi);
            Image<Gray, Byte> mask = new (ImgBinarized.Width, ImgBinarized.Height);
            CvInvoke.DrawContours(mask, Contours, ListSegmentationBox[idx].IdxContour, new MCvScalar(255), -1);
            mask.ROI = ListSegmentationBox[idx].Rect;
            CvInvoke.BitwiseAnd(imgroi, mask, imgroi);
            CvInvoke.BitwiseNot(imgroi, imgroi);
            return imgroi;
        }
        public Image<Gray, Byte> GetSegmentationWithoutMask(int idx)
        {
            Image<Gray, Byte> imgroi = ImgBinarized.Copy();
            imgroi.ROI = ListSegmentationBox[idx].Rect;
            //CvInvoke.BitwiseNot(imgroi, imgroi);
            return imgroi;
        }
        public Image<Gray, Byte> GetAreaWithoutMask(int idx)
        {
            Image<Gray, Byte> imgroi = ImgBinarized.Copy();
            imgroi.ROI = SelecteAreas.LstAreas[idx].Rect;
           
            //CvInvoke.BitwiseNot(imgroi, imgroi);
            return imgroi;
        }
    }
}