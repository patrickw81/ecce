using Emgu.CV.Structure;
using Emgu.CV;
using System.Configuration;
using System.Diagnostics;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Windows.Forms;
using Emgu.CV.Ocl;
using Emgu.CV.Cuda;
using System.Threading.Tasks;
using Emgu.CV.OCR;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Web;
using Emgu.CV.LineDescriptor;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml.Serialization;
using System.Reflection;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.IO;
using static System.Net.WebRequestMethods;
using Microsoft.VisualBasic;
using System;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

namespace ecce
{
    public partial class Form1 : Form
    {
        private CollectionFileList? Myfiles { get; set; }
        private ClassImage? Image { get; set; }
        private int IdxImage { get; set; } = 0;
        bool Runbatch { get; set; } = true;
        bool IsMousedown = false;
        Point startlocat;
        Point endlocat;
        Rectangle rect_picturebox;

        ClassResultCatalogueArchive? ResultCatalogueArchive { get; set; }
        ClassResultTxtOnly? ResultTxtOnly { get; set; }
        ClassFormBatchConfig? BatchConfiguration { get; set; }
        ClassResultAreas? ResultAreas { get; set; }
        ClassResultSegment? ResultSegment { get; set; }

        public Form1()
        {
            InitializeComponent();
            string[] item = LoadTesseractTrainingsData();
            for (int i = 0; i < item.Length; i++)
            {
                string[] x = Path.GetFileName(item[i]).Split(".");
                this.tesss_mdl.Items.Add(x[0]);
            }
            this.tesss_mdl.Text = "deu";
        }

        public static string[] LoadTesseractTrainingsData()
        {
            string[] fileList = Directory.GetFiles(@"./tessdata/", "*.traineddata");
            /* Hier üprüfen
            foreach (string file in fileList)
            {
                string fileName = Path.GetFileName(file);
            }
            */
            return fileList;
        }

        //File *********************************************************************************************************************************************************
        //Open File
        private void MenuOpenFileClick(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog dialog = new()
                {
                    Filter = "Image Files|*.tif;*.tiff;*.jpg;*.jpeg; *.png",
                    RestoreDirectory = true,
                    InitialDirectory = Properties.ProjPath.Default.LastPath
            };
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    string imagePath = dialog.FileName;
                    Myfiles = new CollectionFileList(imagePath);
                    Image = new ClassImage(imagePath);
                    RefreshPicture(Image.ImgProcess);
                    Properties.ProjPath.Default.LastPath= Path.GetDirectoryName(imagePath);
                    Properties.ProjPath.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        //Open Directory
        private void MenuOpenDirectoryClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new()
            {
                
                InitialDirectory = Properties.ProjPath.Default.LastPath
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string directoryPath = folderBrowserDialog.SelectedPath;

                IEnumerable<string> files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                               .Where(file => file.EndsWith(".jpg") || file.EndsWith(".tif") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".tiff"));

                if (files.Any())
                {
                    Myfiles = new CollectionFileList(files.ToList());
                    string imagePath = Myfiles.SwitchPicture(0);
                    Image = new ClassImage(imagePath);
                    RefreshPicture(Image.ImgProcess);
                    Properties.ProjPath.Default.LastPath = Path.GetDirectoryName(imagePath);
                    Properties.ProjPath.Default.Save();
                }
            }
        }

        //Close All
        private void MenuCloseAppClick(object sender, EventArgs e)
        {
            this.Close();
        }

        //Back
        private void MenuImageListBackClick(object sender, EventArgs e)
        {
            if (Myfiles == null) return;
            int intervall = int.Parse(interval_box.Text);
            if (IdxImage - intervall < 0) { return; }
            IdxImage -= intervall;
            string imagePath = Myfiles.SwitchPicture(IdxImage);
            Image = new ClassImage(imagePath);
            pictureBox1.Image = Image.ImgProcess.ToBitmap();
        }

        //Forward
        private void MenuImageListForwardClick(object sender, EventArgs e)
        {
            if (Myfiles == null) return;
            int intervall = int.Parse(interval_box.Text);
            if (IdxImage + intervall > Myfiles.CountFiles - 1) { return; }
            IdxImage += intervall;
            string imagePath = Myfiles.SwitchPicture(IdxImage);
            Image = new ClassImage(imagePath);
            pictureBox1.Image = Image.ImgProcess.ToBitmap();
        }

        //Reset
        private void MenuResetClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            string imagePath = Image.ImagePath;
            Image = new ClassImage(imagePath);
            pictureBox1.Image = Image.ImgProcess.ToBitmap();
        }

        private (double, double) GetImageScale()
        {
            Type pboxType = pictureBox1.GetType();
            PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance)!;
            Rectangle rectangle = (Rectangle)irProperty.GetValue(pictureBox1, null)!;
            return ((double)Image!.ImgProcess.Width / rectangle.Width, (double)Image.ImgProcess.Height / rectangle.Height);
        }


        //Picture *********************************************************************************************************************************************************
        //Show picture

        public void RefreshPicture(Image<Bgr, byte> img)
        {
            pictureBox1.Image = img.ToBitmap();
            txt_picHeight.Text = Image!.ActualHeight.ToString();
            txt_picWidth.Text = Image.ActualWidth.ToString();
        }
        public void RefreshSegmenationInfoBox(int height, int width, int destruction)
        {
            InfoSegmentationHeight.Text = height.ToString();
            InfoSegmentationWidth.Text = width.ToString();
            InfoSegmentationDestruction.Text = destruction.ToString();
        }

        //Picture-Resize : Type Linear
        private void MenuResizeClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            FormSetSize my_param_blocksize = new(this, Image);
            my_param_blocksize.Show();
        }
        public void SetTxtBoxImgSize() {
            txt_picHeight.Text = Image!.ActualHeight.ToString();
            txt_picWidth.Text = Image!.ActualWidth.ToString();
        }
        public void SetTxtBoxLineElimination()
        {
            txt_vert_des.Text = Image!.ParamVerticalDestruction.ToString();
            txt_horiz_dest.Text = Image.ParamHorizontalDestruction.ToString();
        }
        public void SetTxtBoxBlackDots()
        {
            InfoBlackDots.Text = Image!.ParamBlackDotsDestruction.ToString();

        }

        //noise Reduction      
        private void MenuNoiseReductionClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetNoiseReduction().ToBitmap();
        }

        //Shapren Blur
        private void MenuSharpenClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetSharp().ToBitmap();
        }

        //Binarize-Otsu
        private void MenuOtsuBinarizeClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetOtsuBinarize().ToBitmap();
        }

        //Binarize Adopted
        private void MenuAdoptedBinarizeClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetAdoptedBinarize().ToBitmap();
        }

        //Binarie-Param
        private void MenuParamBinarizedClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            FormSetBinarize my_param_blocksize = new(this, Image);
            my_param_blocksize.Show();
        }

        //Lines ********************************************************************************************************************************************************
        //Skew
        private void MenuDeskewWithFrameClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.SetDeskew(true).ToBitmap();
        }
        private void MenuDeskewNoFrameClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.SetDeskew(false).ToBitmap();
        }
        // Remove Black Frame
        private void MenuRemoveBlackFrameClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.RemoveBlackFrame().ToBitmap();
        }
        //Remove Black Dots
        private void RemoveBlackDotsClick(object sender, EventArgs e)
        {
            if (Image == null) return;

            FormSetParamDots my_param_block = new(this, Image);
            my_param_block.Show();
        }

        //Remove-Line Vertical & Horizontal
        private void MenuLineEliminationAuto(object sender, EventArgs e)
        {
            if (Image == null) return;
            LineElimination();

        }
        private void MenuLineEliminationParamClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            FormSetLParamLineElimin my_param_block = new(this, Image);
            my_param_block.Show();
        }
        private void LineElimination()
        {
            Image<Gray, byte> cannyImage = Image!.ImgBinarized.Canny(100, 60);
            LineSegment2D[] lines = cannyImage.HoughLinesBinary(1, Math.PI / 180, 50, 20, 2)[0];
            lines = lines.OrderBy(x => x.Length).ToArray();
            var frequencies = lines.GroupBy(i => i.Length).Select(g => new { Value = g.Key, Count = g.Count() });
            int a = (int)(Math.Round(lines[(int)lines.Length / 2].Length));
            var frequencyArray = frequencies.ToArray();
            int b = (int)(Math.Round(frequencyArray[(int)(frequencyArray.Length * 0.125)].Value));
            int c = (int)(a + b) / 2;
            Debug.WriteLine(c);
            Image.ImgBinarized = Image.RemoveLine(c, c);
            pictureBox1.Image= Image.ImgBinarized.ToBitmap();
            Image.ImgProcess = Image.ImgBinarized.Convert<Bgr, Byte>().Copy();
        }


        // Strong Line
        private void MenuStrongLinesClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.SetLineStrength().ToBitmap();
        }
        //Weak Line
        private void MenuWeakLinesClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.SetLineWeakness().ToBitmap();
        }

        //Segmentation-Block *********************************************************************************************************************************************************

        //Segmentation-Block
        private void MenuSegmentationBlockClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            int w = (int)Image.ActualWidth/45;
            int h = (int)Image.ActualHeight/54;
            pictureBox1.Image = Image.SetSegmentation(w,h).ToBitmap();
            RefreshSegmenationInfoBox(w,h, 1000);
        }

        //Segmentation-Line
        private void MenuSegmentationLineAutoClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            int w = (int)Image.ActualWidth / 10;
            pictureBox1.Image = Image.SetSegmentation(w,3).ToBitmap();
            RefreshSegmenationInfoBox(3, w, 1000);
        }

        // Segmentation-Parameter
        private void MenuSegmentationParameterClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            int height, width, destr;
            try
            {
                height = Int32.Parse(InfoSegmentationHeight.Text);
                width = Int32.Parse(InfoSegmentationWidth.Text);
                destr = Int32.Parse(InfoSegmentationDestruction.Text);
            }
            catch
            {
                height = 1;
                width = 1;
                destr = 1;
            }
            FormSetSegmentation my_param_block = new(this, Image, height, width, destr);
            my_param_block.Show();
        }

        // Tesseract Segmentation
        private void MenuSegmentationTessWordClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Word, tesss_mdl.Text, combo_enginemod.Text).ToBitmap();
        }
        private void MenuSegmentationTessLineClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetSegmentationTess(Tesseract.PageIteratorLevel.TextLine, tesss_mdl.Text, combo_enginemod.Text).ToBitmap();
        }
        private void MenuSegmentationTessParagraphClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Para, tesss_mdl.Text, combo_enginemod.Text).ToBitmap();
        }
        private void MenuSegmentationTessBlockClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Block, tesss_mdl.Text, combo_enginemod.Text).ToBitmap();
        }
       
        //Show Cluster
        private void MenutShwoColumnLineClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.Image = Image.ShowColumn().ToBitmap();
        }
        // Sort
        private void MenuSortXthenYClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            Image.SortXtoY();
            pictureBox1.Image = Image.DrawSegmentationBoxes().ToBitmap();
        }
        private void MenuSortYClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            Image.SortYtoX();
            pictureBox1.Image = Image.DrawSegmentationBoxes().ToBitmap();
        }
        private void MenuSortLikeTableClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            Image.SortBoxexTableLeftRight();
            pictureBox1.Image = Image.DrawSegmentationBoxes().ToBitmap();
        }

        // Add Areas *********************************************************************************************************************************************************
        private void MenuSetAreasClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(PictureBoxMouseDown);
        }

        private void PictureBoxMouseDown(object? sender, MouseEventArgs e)
        {
            Type pboxType = pictureBox1.GetType();
            PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance)!;
            rect_picturebox = (Rectangle)irProperty.GetValue(pictureBox1, null)!;
            IsMousedown = true;
            startlocat = e.Location;
        }

        private void ShowAreasClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            GetDrawAreas();
        }

        private void GetDrawAreas()
        {
            pictureBox1.Image = Image!.SelecteAreas.DrawAreas(Image.SelecteAreas.DrawAreas(Image.ImgBinarized.Convert<Bgr,byte>())).ToBitmap();
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMousedown)
            {
                endlocat = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private Rectangle GetRect()
        {
            Rectangle _rect = new()
            {
                X = Math.Min(startlocat.X, endlocat.X),
                Y = Math.Min(startlocat.Y, endlocat.Y),
                Width = Math.Abs(startlocat.X - endlocat.X),
                Height = Math.Abs(startlocat.Y - endlocat.Y),
            };
            return _rect;
        }

        public void SetTagName(string text)
        {
            Image!.SelecteAreas.LstAreas[Image.SelecteAreas.LstAreas.Count - 1].Tag = text;
            Image.SelecteAreas.LstTagNames.Add(text);
            pictureBox1.Image = Image.SelecteAreas.DrawAreas(Image.ImgBinarized.Convert<Bgr,byte>()).ToBitmap();
        }

        private void PictureBoxPaintRec(object sender, PaintEventArgs e)
        {
            if (IsMousedown)
            {
                e.Graphics.DrawRectangle(Pens.Red, GetRect());
            }
        }

        private void PictureBoxMouseUp(object? sender, MouseEventArgs e)
        {
            if (IsMousedown)
            {
                CollectionArea myarea = new();
                Image!.SelecteAreas.LstAreas.Add(myarea);
                endlocat = e.Location;
                (double imgscale_w, double imgscale_h) = GetImageScale();
                endlocat = new Point((int)Math.Round((endlocat.X - rect_picturebox.X) * imgscale_w), (int)Math.Round((endlocat.Y - rect_picturebox.Y) * imgscale_h));
                startlocat = new Point((int)Math.Round((startlocat.X - rect_picturebox.X) * imgscale_w), (int)Math.Round((startlocat.Y - rect_picturebox.Y) * imgscale_h));

                if (startlocat.X < 0) { startlocat.X = 0; }
                if (startlocat.Y < 0) { startlocat.Y = 0; }
                if (startlocat.X > Image.ImgProcess.Width) { startlocat.X = Image.ImgProcess.Width - 1; }
                if (startlocat.Y > Image.ImgProcess.Height) { startlocat.Y = Image.ImgProcess.Height - 1; }

                if (endlocat.X < 0) { endlocat.X = 0; }
                if (endlocat.Y < 0) { endlocat.Y = 0; }
                if (endlocat.X > Image.ImgProcess.Width) { endlocat.X = Image.ImgProcess.Width - 1; }
                if (endlocat.Y > Image.ImgProcess.Height) { endlocat.Y = Image.ImgProcess.Height - 1; }

                Image.SelecteAreas.LstAreas[^1].Rect = GetRect();
                IsMousedown = false;
                pictureBox1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);

                FormSetTag mytag = new(this, Image.SelecteAreas.LstTagNames);
                mytag.Show();
            }
        }

        private void ClearAreasClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            Image.DeleteSegmentation();
            pictureBox1.Image=Image.ImgBinarized.ToBitmap();
        }

        private void SaveAreaXmlClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            if (Image.SelecteAreas.LstAreas.Count < 1)
            {
                MessageBox.Show("No Areas");
                return;
            }
            Image.SelecteAreas.ExportAreaAsXmL();
        }

        private void LoadAreaXmlClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            Image.SelecteAreas = new ClassSelectedAreas();
            Image.SelecteAreas.ImportAreaAsXml();
            GetDrawAreas();
        }

        //Teseract *********************************************************************************************************************************************************
        //private void runOCR_without_seg(bool analyze for ChatGPT) 

        private async void MenuRunOcrTxtOnlyClick(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            splitContainer1.Panel2.Controls.Clear();
            Task mytask = RunOcrTxtOnly(false);
            await mytask;
        }

        private async Task RunOcrTxtOnly(bool analyze)
        {
            if (Image == null) return;
            ResultTxtOnly = new ClassResultTxtOnly(new ClassOcrTess(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text));
            Task<string> myTask = ResultTxtOnly.RunOcr(Image.ImgBinarized, new CollectionTxtResult()
            {
                ImgPath = Image!.ImagePath,
                ImgWidth = Image.OriginalWidth,
                ImgHeight = Image.OriginalHeight,
                StrSha256 = Image.StrSHA256,
                StrSha512 = Image.StrSHA512
            });
            await myTask;

            if (analyze)
            {
                //myKi = new ChatGpt("", "text -davinci-003");
                //string a = "Chatpgt deutsche Metadaten(Titel, Ort, Zeitraum, Personen, Schlagworte, Zusammenfassung) \n";
                //string b = " <|endoftext|>";
                //Debug.WriteLine(myKi.SendChatGPTRequest(a + " " + article.form_text));
                try
                {
                    //var myresponse = await myKi.SendChatGPTRequest(a + " " + article.form_text+ b );

                    //article.summary = article.summary + myresponse.ToString();
                    //my_result_text.myList.summary = "Hier wäre deine Antwort - Sorry ist nicht geöffnet für dich";
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
           
            (Button button_save_txt, Button button_save_xml, Button button_save_csv) = ResultTxtOnly!.GetButtons();
            splitContainer1.Panel2.Controls.Add(button_save_txt);
            splitContainer1.Panel2.Controls.Add(button_save_xml);
            splitContainer1.Panel2.Controls.Add(button_save_csv);
            splitContainer1.Panel2.Controls.Add(ResultTxtOnly.GetResultasTable());
        }
        private async void MenuTessSegmentedClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            ResultSegment = new(new ClassOcrTess(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text));
            Task<string> myTaks = ResultSegment.RunOcr(Image!);
            await myTaks;
            Button button_save_txt = new();
            Button button_save_xml = new();
            Button button_save_csv = new();
            (button_save_txt, button_save_xml, button_save_csv) = ResultSegment!.GetButtons();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(button_save_txt);
            splitContainer1.Panel2.Controls.Add(button_save_xml);
            splitContainer1.Panel2.Controls.Add(button_save_csv);
        }


        private async void MenuRunOcrTxtOnlyChatGptClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            Task<string> myTask = ResultTxtOnly!.RunOcr(Image!.ImgBinarized, new CollectionTxtResult()
            {
                ImgPath = Image!.ImagePath,
                ImgWidth = Image.OriginalWidth,
                ImgHeight = Image.OriginalHeight,
                StrSha256 = Image.StrSHA256,
                StrSha512 = Image.StrSHA512
            });
            await myTask;
        }

        //Archiv Katalog
        private async void MenuCatalogueArchivTemplateA(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (Image!.ListSegmentationBox.Count == 0)
            {
                return;
            }
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            Task mytask = RunCatalogueTemplateA();
            await mytask;
        }
        private async Task RunCatalogueTemplateA()
        {
            if (Image == null) return;
            ResultCatalogueArchive = new ClassResultCatalogueArchive(new ClassOcrTess(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text));

            Task<string> myTaks = ResultCatalogueArchive.RunOcr(Image);
            await myTaks;
            (Button button_save_txt, Button button_save_xml, Button button_save_csv) = ResultCatalogueArchive!.GetButtons();
            (TableLayoutPanel mytable, TextBox txt_csv, TextBox txt_xml) = ResultCatalogueArchive.GetResultasTable(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
            splitContainer1.Panel2.Controls.Add(button_save_txt);
            splitContainer1.Panel2.Controls.Add(button_save_xml);
            splitContainer1.Panel2.Controls.Add(button_save_csv);
            splitContainer1.Panel2.Controls.Add(mytable);
            splitContainer1.Panel2.Controls.Add(txt_csv);
            splitContainer1.Panel2.Controls.Add(txt_xml);
            mytable.Show();
            txt_csv.Show();
            txt_xml.Show();
        }

        private async void MenuSoftCutClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            if (Image!.ListSegmentationBox.Count < 1) { MessageBox.Show("Create Segmentation"); return; }
            if (Image.SelecteAreas.LstAreas.Count < 1) { MessageBox.Show("Create Areas"); return; }

            ResultAreas = new ClassResultAreas(new ClassOcrTess(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text));
            Task<string> myTaks = ResultAreas.RunOcr(Image, 0);
            await myTaks;
            (Button button_save_txt, Button button_save_xml, Button button_save_csv) = ResultAreas!.GetButtons();
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(button_save_txt);
            splitContainer1.Panel2.Controls.Add(button_save_xml);
            splitContainer1.Panel2.Controls.Add(button_save_csv);
        }

        private async void MenuHardCutClick(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            if (Image!.SelecteAreas.LstAreas.Count < 1) { MessageBox.Show("Create Areas"); return; }
            ResultAreas = new ClassResultAreas(new ClassOcrTess(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text));
            Task<string> myTaks = ResultAreas!.RunOcr(Image, 1);
            await myTaks;

            (TextBox[] mytxt, Label[] mylalbl) = ResultAreas.GetResultPage();
            if (ResultAreas.ListResultSegmentedAreas[0].DictTxtperArea.Count < 1)
            {
                ResultAreas.ListResultSegmentedAreas[0].DictTxtperArea.Add("Result", "NO Text dedected");
            }
            for (int i = 0; i < mytxt.Length; i++)
            {
                splitContainer1.Panel2.Controls.Add(mylalbl[i]);
                splitContainer1.Panel2.Controls.Add(mytxt[i]);
            }
        }

        //Batch Prozess
        private void MenuLoadBatchProcessClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            int seg_prm_h = 1;
            int seg_prm_w = 1;
            int seg_des = 1;
            if (Image.ListSegmentationBox != null && InfoSegmentationHeight.Text != "")
            {
                seg_prm_h = Int32.Parse(InfoSegmentationHeight.Text);
                seg_prm_w = Int32.Parse(InfoSegmentationWidth.Text);
                seg_des = Int32.Parse(InfoSegmentationDestruction.Text);
            }
            BatchConfiguration = new ClassFormBatchConfig(Int32.Parse(interval_box.Text), Image.RoundResizedFactor, Image.ParamBinarized, Image.ParamVerticalDestruction, Image.ParamHorizontalDestruction, seg_prm_w, seg_prm_h, seg_des, Image.ParamBlackDotsDestruction, Image.ActualWidth,Image.ActualHeight);
            BatchConfiguration.Applystartbatch += this.StarBatch;
            splitContainer1.Panel2.Controls.Add(BatchConfiguration);
        }

        public void Stopbatch(){
            Runbatch = false;
        }

        public async void StarBatch()
        {
            
            splitContainer1.Panel2.Controls.Clear();
            Result_Form myresultform = new (Myfiles!.CountFiles);
            myresultform.Applystopbatch += this.Stopbatch;
            splitContainer1.Panel2.Controls.Add(myresultform);         
            var progress = new Progress<CollectionProgressData>(data =>
            {
                myresultform.status_update(data.Status, data.Value);
                pictureBox1.Image= Image!.ImgBinarized.ToBitmap();
            });

            switch (BatchConfiguration!.OcrMode)
            {
                case "OCR_only": ResultTxtOnly = new ClassResultTxtOnly(new ClassOcrTess(BatchConfiguration.OcrPageMode, BatchConfiguration.OcrLanguageMode, BatchConfiguration.OcrEnginMode)); break;
                case "Segmented": ResultSegment = new (new ClassOcrTess(BatchConfiguration.OcrPageMode, BatchConfiguration.OcrLanguageMode, BatchConfiguration.OcrEnginMode)); break;
                case "Archive_Cat_Temp_A": ResultCatalogueArchive = new ClassResultCatalogueArchive(new ClassOcrTess(BatchConfiguration.OcrPageMode, BatchConfiguration.OcrLanguageMode, BatchConfiguration.OcrEnginMode)); break;
                case "Areas_SoftCut": ResultAreas = new ClassResultAreas(new ClassOcrTess(BatchConfiguration.OcrPageMode, BatchConfiguration.OcrLanguageMode, BatchConfiguration.OcrEnginMode)); break;
                case "Areas_HardCut": ResultAreas = new ClassResultAreas(new ClassOcrTess(BatchConfiguration.OcrPageMode, BatchConfiguration.OcrLanguageMode, BatchConfiguration.OcrEnginMode)); break;
                default: break;
            }

            for (int i = BatchConfiguration.Start; i< Myfiles!.CountFiles; i += BatchConfiguration.Intervall)
            {
                if (!Runbatch) break;
                bool x = await RunBatchTask(progress, i);
            }

            Runbatch = true;
            Button button_save_txt =new ();
            Button button_save_xml = new ();
            Button button_save_csv = new ();
            switch (BatchConfiguration.OcrMode)
            {
                case "OCR_only":(button_save_txt,  button_save_xml,  button_save_csv) = ResultTxtOnly!.GetButtons();break;
                case "Segmented": (button_save_txt, button_save_xml, button_save_csv) = ResultSegment!.GetButtons(); break;
                case "Archive_Cat_Temp_A": (button_save_txt, button_save_xml, button_save_csv) = ResultCatalogueArchive!.GetButtons(); break;
                case "Areas_SoftCut": (button_save_txt, button_save_xml, button_save_csv) = ResultAreas!.GetButtons(); break;
                case "Areas_HardCut": (button_save_txt, button_save_xml, button_save_csv) = ResultAreas!.GetButtons(); break;
                default: break;
            }
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(button_save_txt);
            splitContainer1.Panel2.Controls.Add(button_save_xml);
            splitContainer1.Panel2.Controls.Add(button_save_csv);
        }       

        private async Task<bool> RunBatchTask(IProgress<CollectionProgressData> progress, int idx)
        {
            string status;
            try
            {
                Image = new ClassImage(Myfiles!.SwitchPicture(idx));
                if (BatchConfiguration!.SelectedAreas != "")
                {
                    Image.SelecteAreas = new ClassSelectedAreas();
                    Image.SelecteAreas.ReadXml(BatchConfiguration.SelectedAreas!);
                }
                if (BatchConfiguration!.BoolResize) { Image.GetNewSize((int)Math.Round(Image.ImgProcess.Width * BatchConfiguration.ResizeFactor / 100), (int)Math.Round(Image.ImgProcess.Height * BatchConfiguration.ResizeFactor / 100)); }
                if (BatchConfiguration.BoolNoiseReduction) { Image.GetNoiseReduction(); }
                if (BatchConfiguration.BoolSharpen) { Image.GetSharp(); }
                
                switch (BatchConfiguration.BinararizedMode)
                {
                    case "Otsu": Image.GetOtsuBinarize(); break;
                    case "Adopted": Image.GetAdoptedBinarize(); break;
                    case "With Parameters": Image.GetBinarizePerParam(BatchConfiguration.ParamBinaraize); break;
                    default: break;
                }
                switch (BatchConfiguration.DeskewMode)
                {
                    case "Black Frame": Image.SetDeskew(true); break;
                    case "No Frame": Image.SetDeskew(false); break;
                    default: break;
                }
                if (BatchConfiguration.BoolRemoveFrame) { Image.RemoveBlackFrame(); }
                if (BatchConfiguration.BoolRemoveBlackDots) { Image.ImgBinarized= Image.RemoveBlackDots(Image.ImgBinarized, BatchConfiguration.ParamRemoveBlackDots); }
                               
                switch (BatchConfiguration.LineEliminationMode)
                {
                    case "Auto": LineElimination(); break;
                    case "Parameters": Image.ImgBinarized = Image.RemoveLine(BatchConfiguration.LineElimminationHorizontal, BatchConfiguration.LineEliminationVertical); break;
                    default: break;
                }

                switch (BatchConfiguration.LineMode)
                {
                    case "Strong": Image.SetLineStrength(); break;
                    case "Weak": Image.SetLineWeakness(); break;
                    default: break;
                }

                switch (BatchConfiguration.SegmentationMode)
                {
                    case "Line Auto": Image.SetSegmentation(3, 30, 1000); break;
                    case "Block Auto": Image.SetSegmentation(52, 62, 1000); break;
                    case "Parameter": Image.SetSegmentation(BatchConfiguration.SegmentationParamWidth, BatchConfiguration.SegmentationParamHeight, BatchConfiguration.SegmentationParamDestroy); break;
                    case "Tessseract_Word": Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Word, tesss_mdl.Text, combo_enginemod.Text).ToBitmap(); break;
                    case "Tessseract_Line": Image.GetSegmentationTess(Tesseract.PageIteratorLevel.TextLine, tesss_mdl.Text, combo_enginemod.Text).ToBitmap(); break;
                    case "Tessseract_Para": Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Para, tesss_mdl.Text, combo_enginemod.Text).ToBitmap(); break;
                    case "Tessseract_Block": Image.GetSegmentationTess(Tesseract.PageIteratorLevel.Block, tesss_mdl.Text, combo_enginemod.Text).ToBitmap(); break;
                    default: break;
                }

                switch (BatchConfiguration.SortMode)
                {
                    case "X then Y": Image.SortXtoY(); ; break;
                    case "Y": Image.SortYtoX(); break;
                    case "Left_2_Right": Image.SortBoxexTableLeftRight(); break;
                    default: break;
                }
                string result="Neutral";
                Task<string> myTask;
                switch (BatchConfiguration.OcrMode)
                {
                    case "OCR_only":
                        myTask = ResultTxtOnly!.RunOcr(Image.ImgBinarized, new CollectionTxtResult()
                        {
                            ImgPath = Image!.ImagePath,
                            ImgWidth = Image.OriginalWidth,
                            ImgHeight = Image.OriginalHeight,
                            StrSha256 = Image.StrSHA256,
                            StrSha512 = Image.StrSHA512
                        });
                        result = await myTask; 
                        break;
                    case "Segmented":
                        myTask = ResultSegment!.RunOcr(Image!);
                        result = await myTask;
                        break;
                    case "Archive_Cat_Temp_A":
                        myTask = ResultCatalogueArchive!.RunOcr(Image);
                        result = await myTask;
                        break;
                    case "Areas_SoftCut":
                         myTask = ResultAreas!.RunOcr(Image,0);
                         result = await myTask; 
                         break;
                    case "Areas_HardCut":
                        myTask = ResultAreas!.RunOcr(Image,1);
                        result = await myTask;
                        break;
                    default: break;
                }
                status = result;
            }
            catch(Exception ex)
            {
                status= ex.ToString();
            }
            progress.Report(new CollectionProgressData { Status = status, Value = idx+1 });

            await Task.Delay(1000);
            return true;
        } 
    }
}