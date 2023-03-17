using Emgu.CV.Structure;
using Emgu.CV;
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

namespace ecce
{
    public partial class Form1 : Form
    {
        Files_List myfiles;
        Image_manip myimage;

        string imagePath;
       
        ChatGpt myKi;

        //rect creation
        bool IsMousedown = false;
        Point startlocat;
        Point endlocat;
        Rectangle rect_picturebox;

        Page_Segmentation mypage_seg = new Page_Segmentation();
        
        Archive_page myarchive_page;   
        MyAreas Defined_Areas= new MyAreas();     
        Result_text_only article;
        

        public Form1()
        {

            
            InitializeComponent();

            string[] item = tessmode();

            for (int i = 0; i < item.Length; i++)
            {
                string[] x = Path.GetFileName(item[i]).Split(".");

                this.tesss_mdl.Items.Add(x[0]);
            }
            this.tesss_mdl.Text = "deu";
            
        }

        public string[]  tessmode()
        {
            
            
            string[] fileList = Directory.GetFiles(@"./tessdata/", "*.traineddata");
            foreach (string file in fileList)
            {
                string fileName = Path.GetFileName(file);
                Debug.WriteLine(fileName);
            }

            return fileList;
        }

        //File *********************************************************************************************************************************************************

        //Open File
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "Image Files|*.tif;*.tiff;*.jpg;*.jpeg; *.png";

                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    imagePath = dialog.FileName;
                    //openimage(imagePath);
                    myimage = new Image_manip(imagePath);
                    refreshpicturebox();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Open Directory
        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();


            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string directoryPath = folderBrowserDialog.SelectedPath;

                IEnumerable<string> files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                               .Where(file => file.EndsWith(".jpg") || file.EndsWith(".tif") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".tiff"));
                Debug.WriteLine(files.Count());

                if (files.Any())
                {
                    myfiles = new Files_List(files.ToList());
                    imagePath = myfiles.switchemelemnt(0);
                    myimage = new Image_manip(imagePath);
                    refreshpicturebox();
                }
            }
        }

        //Close All
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Back
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (myfiles != null)
            {
                int intervall = int.Parse(interval_box.Text);
                imagePath = myfiles.switchemelemnt(-(intervall));
                myimage = new Image_manip(imagePath);
                pictureBox1.Image = myimage.imgProcess.ToBitmap();
            }
        }

        //Forward
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (myfiles != null)
            {
                int intervall = int.Parse(interval_box.Text);
                imagePath = myfiles.switchemelemnt(intervall);
                //openimage(imagePath);
                myimage = new Image_manip(imagePath);
                refreshpicturebox();
            }
        }

       
        //Reset
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myimage = new Image_manip(imagePath);
            refreshpicturebox();
        }


        private (double, double) imagescale()
        {
            Type pboxType = pictureBox1.GetType();
            PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
            Rectangle rectangle = (Rectangle)irProperty.GetValue(pictureBox1, null);
            Debug.WriteLine(rectangle.Width);
            Debug.WriteLine(rectangle.Height);
            return ((double)myimage.imgProcess.Width/rectangle.Width, (double)myimage.imgProcess.Height / rectangle.Height);
        }
       


        //Picture *********************************************************************************************************************************************************
        

        //Show picture
        public void refreshpicturebox(Image<Bgr, Byte> img = null)
        {
            
            if (img == null)
            {

                pictureBox1.Image = myimage.imgProcess.ToBitmap();
            }
            else
            {
                pictureBox1.Image = img.ToBitmap();
            }
            txt_picHeight.Text=myimage.act_height.ToString();
            txt_picWidth.Text=myimage.act_width.ToString();
            txt_vert_des.Text=myimage.vertical_dest_param.ToString();
            txt_horiz_dest.Text=myimage.horizontal_dest_param.ToString();
           
        }

        
        //rotate comming ?

       
        //Picture-Resize : Type Linear

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;

            Param_size my_param_blocksize = new Param_size(this,myimage, myimage.imgProcess.Width, myimage.imgProcess.Height) ;
            my_param_blocksize.Show();          
        }

        //noise Reduction
    
        private void noiseReductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            myimage.moise_reduction();
            refreshpicturebox();
        }

        //Shapren Blur
        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            myimage.sharpen();
            refreshpicturebox();
        }

        //Binarize-Otsu

        private void otsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            myimage.otsubinarize();
            refreshpicturebox();
           
        }
    
        //Binarize Adopted
        private void adoptedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            myimage.adoptedbinarize();
            refreshpicturebox();
        }

        //Binarie-Param

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            Param_Binar my_param_blocksize = new Param_Binar(this, myimage);
            my_param_blocksize.Show();
        }


        //Remove Elements *********************************************************************************************************************************************************

        //Remove-Line Vertical & Horizontal
        private void autoAssignToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            myimage.tablededection(20, 20);
            refreshpicturebox();
            
           
        }

        private void withParametersToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            Param_v_h_destr my_param_block = new Param_v_h_destr(this, myimage);
            my_param_block.Show();
        }


        //Segmentation-Block *********************************************************************************************************************************************************

        private void autoBlock_Click(object sender, EventArgs e)
        {
            getblock();
        }

        // same as line
        private void withParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Param_block my_param_block = new Param_block(this, mypage_seg.param_heigth, mypage_seg.param_width, mypage_seg.param_des);
            my_param_block.Show();
        }

        public void getblock(int heigt = 52, int width = 62, int destroy_rec = 1000)
        {
            if (myimage == null) return;
            
            
            pictureBox1.Image = mypage_seg.xblockSegmentation(myimage.imgBinarized, width, heigt, destroy_rec).ToBitmap();
            txt_segheigth.Text = mypage_seg.param_heigth.ToString();
            txt_segwidth.Text = mypage_seg.param_width.ToString();
            txt_dstrct_size.Text=mypage_seg.param_des.ToString();
            
        }

        //Segmentation-Block: Function Block 
        private void autoline_Click(object sender, EventArgs e)
        {
           
            getblock(3, 30);
        }

        //Show Cluster
   
        private void addShowClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mypage_seg.lst_segboxes.Count > 0)
            {
                for (int i = 0; i < mypage_seg.bottom_line.Length - 1; i++)
                {
                    CvInvoke.Line(mypage_seg.showimg, new Point(1, mypage_seg.bottom_line[i]), new Point(mypage_seg.showimg.Width, mypage_seg.bottom_line[i]), new MCvScalar(20, 50, 255), 1, LineType.AntiAlias);
                    pictureBox1.Image = mypage_seg.showimg.ToBitmap();
                }
            }
        }

        // Add Areas *********************************************************************************************************************************************************
        private void lineTxtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Type pboxType = pictureBox1.GetType();
            PropertyInfo irProperty = pboxType.GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
            rect_picturebox = (Rectangle)irProperty.GetValue(pictureBox1, null);
            IsMousedown = true;
            
            startlocat = e.Location;
        }

        private void showAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myimage == null) return;
            if (mypage_seg.showimg == null)
            {
                pictureBox1.Image = Defined_Areas.drawareas(myimage.imgProcess).ToBitmap();
            }
            else
            {
                Defined_Areas.drawareas(mypage_seg.showimg).ToBitmap();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMousedown)
            {
                endlocat = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private Rectangle getrect()
        {
            Rectangle _rect = new Rectangle();
            _rect.X = Math.Min(startlocat.X, endlocat.X);
            _rect.Y = Math.Min(startlocat.Y, endlocat.Y);
            _rect.Width = Math.Abs(startlocat.X - endlocat.X);
            _rect.Height = Math.Abs(startlocat.Y - endlocat.Y);
            return _rect;
        }

        public void tag_area(string text)
        {
            Defined_Areas.lst_myarea_loc[Defined_Areas.lst_myarea_loc.Count-1].tag = text;
            Defined_Areas.lst_tagnames.Add(text);
            if (mypage_seg.showimg == null)
            {
                pictureBox1.Image = Defined_Areas.drawareas(myimage.imgProcess).ToBitmap();
            }
            else
            {
                Defined_Areas.drawareas(mypage_seg.showimg).ToBitmap();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (IsMousedown)
            {
                e.Graphics.DrawRectangle(Pens.Red, getrect());
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMousedown)
            {
                Myarea_locate myarea = new Myarea_locate();
                Defined_Areas.lst_myarea_loc.Add(myarea);

                endlocat = e.Location;
                (double imgscale_w, double imgscale_h) = imagescale();
                endlocat = new Point((int)Math.Round((endlocat.X - rect_picturebox.X) * imgscale_w), (int)Math.Round((endlocat.Y - rect_picturebox.Y) * imgscale_h));
                Debug.WriteLine(imgscale_w + "  hh " + imgscale_h);
                startlocat = new Point((int)Math.Round((startlocat.X - rect_picturebox.X) * imgscale_w), (int)Math.Round((startlocat.Y - rect_picturebox.Y) * imgscale_h));
                
                if (startlocat.X < 0) { startlocat.X = 0; }
                if (startlocat.Y < 0) { startlocat.Y = 0; }
                if (startlocat.X > myimage.imgProcess.Width) { startlocat.X = myimage.imgProcess.Width - 1; }
                if (startlocat.Y > myimage.imgProcess.Height) { startlocat.Y = myimage.imgProcess.Height - 1; }

                if (endlocat.X < 0) { endlocat.X = 0; }
                if (endlocat.Y < 0) { endlocat.Y = 0; }
                if (endlocat.X > myimage.imgProcess.Width) { endlocat.X = myimage.imgProcess.Width - 1; }
                if (endlocat.Y > myimage.imgProcess.Height) { endlocat.Y = myimage.imgProcess.Height - 1; }

                Defined_Areas.lst_myarea_loc[Defined_Areas.lst_myarea_loc.Count - 1].rect = getrect();
                
                IsMousedown = false;
                pictureBox1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);

                Creat_Tag mytag = new Creat_Tag(this, Defined_Areas.lst_tagnames);
                mytag.Show();
                //draw_myareas();
            }
        }

        private void secPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Defined_Areas.lst_myarea_loc.Clear();
            Defined_Areas.lst_tagnames.Clear();
            refreshpicturebox();
        }
        private void saveAreasListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Defined_Areas.lst_myarea_loc.Count < 1) return;
            Defined_Areas.save_area();
        }

        private void loadAereaListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyAreas Defined_Areas = new MyAreas();
            Defined_Areas.load_xml();
        }


        //Teseract *********************************************************************************************************************************************************

        //private void runOCR_without_seg(bool analyze for ChatGPT) 
        private void runOCROnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            runOCR_without_seg(false);
        }

       
        private async Task runOCR_without_seg(bool analyze)
        {
            article = new Result_text_only();
            Ocr_Dedect myocr = new Ocr_Dedect(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text);
            //Ocr_Dedect myocr = new Ocr_Dedect();

            article.form_text = article.form_text + myocr.readtxt_UTF8(myimage.imgBinarized);
            article.combine_txt();
           
            if (analyze) 
            {
                
                //myKi = new ChatGpt("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", "text -davinci-003");
                string a = "Chatpgt deutsche Metadaten(Titel, Ort, Zeitraum, Personen, Schlagworte, Zusammenfassung) \n";
                string b = " <|endoftext|>";
                //Debug.WriteLine(myKi.SendChatGPTRequest(a + " " + article.form_text));
                try
                {
                    //var myresponse = await myKi.SendChatGPTRequest(a + " " + article.form_text+ b );

                    //article.summary = article.summary + myresponse.ToString();
                    article.summary = "Hier wäre deine Antwort - Sorry ist nicht geöffnet für dich";
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
               
            }
            splitContainer1.Panel2.Controls.Add(article.creat_table());
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myKi = new ChatGpt("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", "text -davinci-003");
            string a = "was Geschah am 11 September 2009";
            Debug.WriteLine(myKi.SendChatGPTRequest(a));
        }


        private void runOCRChatgptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            runOCR_without_seg(true);
        }


        //Archiv Katalog

        private void templateAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myarchive_page = new Archive_page();
            if (mypage_seg.lst_segboxes.Count() == 0)
            {
                return;
            }
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
           
            archiv_catalogue();            
        }

        private void archiv_catalogue()
        {
            int start = mypage_seg.starting_point();
            if ((mypage_seg.lst_segboxes.Count>0))
            {
                Archive_Column myColumn = new Archive_Column();
                Ocr_Dedect myocr = new Ocr_Dedect(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text);
                //Ocr_Dedect myocr = new Ocr_Dedect(tesss_mdl.Text);

                int x = 0;
                int myline = 0;
                string lasttitle = "";

                myarchive_page.lst_archiv_colmn.Add(myColumn);
                do
                {
                   if (myline != mypage_seg.lst_segboxes[x].column)
                   {
                       if ((myColumn.dsgl == false)&&(myColumn.title!="") )
                       {
                           lasttitle = myColumn.title;
                       }
                       myColumn = new Archive_Column();
                       myarchive_page.lst_archiv_colmn.Add(myColumn);
                       myline++;
                   }

                   string text = myocr.readtxt_UTF8(img_caption(mypage_seg.lst_segboxes[x].rect, mypage_seg.lst_segboxes[x].index_contour));
                   string my_txt = text.Replace("-\r\n", "");
                   myColumn.analysetxt(start, mypage_seg.lst_segboxes[x].rect.X, my_txt, lasttitle);
                    x++;

               } while (x < mypage_seg.lst_segboxes.Count);
                (TableLayoutPanel mytable, TextBox txt_csv, TextBox txt_xml) = myarchive_page.gen_archivpage(splitContainer1.Panel2.Width, splitContainer1.Panel2.Height);
                splitContainer1.Panel2.Controls.Add(mytable);
                splitContainer1.Panel2.Controls.Add(txt_csv);
                splitContainer1.Panel2.Controls.Add(txt_xml);

                mytable.Show();
                txt_csv.Show();
                txt_xml.Show();
            }
        }
        private Image<Gray, Byte> img_caption(Rectangle myrect, int index_contour)
        {
           
            Image<Gray, Byte> imgroi = new Image<Gray, Byte>(myimage.imgBinarized.Width, myimage.imgBinarized.Height);
            imgroi = myimage.imgBinarized.Copy();
            imgroi.ROI = myrect;
            CvInvoke.BitwiseNot(imgroi, imgroi);
            Image<Gray, Byte> mask = new Image<Gray, Byte>(myimage.imgBinarized.Width, myimage.imgBinarized.Height);
            CvInvoke.DrawContours(mask, mypage_seg.contours, index_contour, new MCvScalar(255), -1);
            mask.ROI = myrect;
            CvInvoke.BitwiseAnd(imgroi, mask, imgroi);
            CvInvoke.BitwiseNot(imgroi, imgroi);
            return imgroi;
        }


        private Image<Gray, Byte> img_caption_hardcut(Rectangle myrect)
        {

            Image<Gray, Byte> imgroi = new Image<Gray, Byte>(myimage.imgBinarized.Width, myimage.imgBinarized.Height);
            imgroi = myimage.imgBinarized.Copy();
            imgroi.ROI = myrect;
            CvInvoke.BitwiseNot(imgroi, imgroi);
            
            return imgroi;
        }

       




        private void softCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            if (mypage_seg.lst_segboxes.Count < 1) { MessageBox.Show("Create Segmentation"); return; }
            if (Defined_Areas.lst_myarea_loc.Count < 1) { MessageBox.Show("Create Areas"); return; }
            Defined_Areas.lst_myarea_loc = Defined_Areas.lst_myarea_loc.OrderBy(x => x.rect.Y).ThenBy(y => y.rect.X).ToList();
            Ocr_Dedect myocr = new Ocr_Dedect(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text);
            //Ocr_Dedect myocr = new Ocr_Dedect(tesss_mdl.Text);

            Result_area myresult = new Result_area();

            for(int i = 0; i < Defined_Areas.lst_myarea_loc.Count; i++)
            {
                string text = "";
                for (int u = 0; u < mypage_seg.lst_segboxes.Count; u++)
                {
                    Point point = new Point(mypage_seg.lst_segboxes[u].rect.X, mypage_seg.lst_segboxes[u].rect.Y);

                    if ((point.X >= Defined_Areas.lst_myarea_loc[i].rect.X && point.X <= Defined_Areas.lst_myarea_loc[i].rect.X + Defined_Areas.lst_myarea_loc[i].rect.Width) && (point.Y >= Defined_Areas.lst_myarea_loc[i].rect.Y && point.Y <= Defined_Areas.lst_myarea_loc[i].rect.Y + Defined_Areas.lst_myarea_loc[i].rect.Height))
                    {
                        text =text + myocr.readtxt_UTF8(img_caption(mypage_seg.lst_segboxes[u].rect, mypage_seg.lst_segboxes[u].index_contour)) + "\r\n";
                    }

                   
                }
                myresult.dict_myresult.Add(Defined_Areas.lst_myarea_loc[i].tag, text.Trim());
               
            }
           
            if (myresult.dict_myresult.Count < 1)
            {
                myresult.dict_myresult.Add("Result", "NO Text dedected");
            }
            (TextBox[] mytxt, Label[] mylalbl) = myresult.generate_resultpage();
            for(int i=0; i<mytxt.Length;i++)
            {
                splitContainer1.Panel2.Controls.Add(mylalbl[i]);
                splitContainer1.Panel2.Controls.Add(mytxt[i]);
            }
        }

        private void hardCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2.HasChildren == true)
            {
                splitContainer1.Panel2.Controls.Clear();
            }
            if (Defined_Areas.lst_myarea_loc.Count < 1) { MessageBox.Show("Create Areas"); return; }
            Defined_Areas.lst_myarea_loc = Defined_Areas.lst_myarea_loc.OrderBy(x => x.rect.Y).ThenBy(y => y.rect.X).ToList();
            Ocr_Dedect myocr = new Ocr_Dedect(pagmode.Text, tesss_mdl.Text, combo_enginemod.Text);
            //Ocr_Dedect myocr = new Ocr_Dedect(tesss_mdl.Text);
            Result_area myresult = new Result_area();

            for (int i = 0; i < Defined_Areas.lst_myarea_loc.Count; i++)
            {
                string text = myocr.readtxt_UTF8(img_caption_hardcut(Defined_Areas.lst_myarea_loc[i].rect));
                myresult.dict_myresult.Add(Defined_Areas.lst_myarea_loc[i].tag, text);
            }
            (TextBox[] mytxt, Label[] mylalbl) = myresult.generate_resultpage();
            if (myresult.dict_myresult.Count < 1)
            {
                myresult.dict_myresult.Add("Result", "NO Text dedected");
            }
            for (int i = 0; i < mytxt.Length; i++)
            {
                splitContainer1.Panel2.Controls.Add(mylalbl[i]);
                splitContainer1.Panel2.Controls.Add(mytxt[i]);
            }
        }




        //Test  *********************************************************************************************************************************************************

        private void test_durchge(int _hlength, int _vlength)
        {
           
            var img = myimage.imgBinarized;
            int hlength = _hlength;
            int vlength = _vlength;
            //int length =(int)(img.Width*MorphTrehold/100);

            Mat vProfile = new Mat();
            Mat hProfile = new Mat();
            Mat vduchprof = new Mat();
            Mat hdurchprof=new Mat();

            var kernelV = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, _vlength), new Point(-1, -1));
            var kernelH = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(_hlength, 1), new Point(-1, -1));

            var kernelHdurch = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(500,1), new Point(-1, -1));
            var kernelVdurch = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 5), new Point(-1, -1));

            // Durchgestrichen

            //horizontal durchgestrich
            CvInvoke.Dilate(img, hdurchprof, kernelHdurch, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(hdurchprof, hdurchprof, kernelHdurch, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));


            //Mat xProfile = hProfile.Clone();

            CvInvoke.Dilate(img, vduchprof, kernelVdurch, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0));
            CvInvoke.Erode(vduchprof, vduchprof, kernelVdurch, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));

            CvInvoke.BitwiseNot(hdurchprof, hdurchprof);
            CvInvoke.BitwiseAnd(hdurchprof, vduchprof, hdurchprof);
            CvInvoke.BitwiseNot(hdurchprof, hdurchprof);
            var x = hdurchprof.ToImage<Gray, byte>();
            var hope = myimage.imgBinarized.Or(x.ThresholdBinaryInv(new Gray(245), new Gray(255)).Dilate(1));


            //horziontal
            CvInvoke.Dilate(hope, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(hProfile, hProfile, kernelH, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));

            //vertical
            CvInvoke.Dilate(hope, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            CvInvoke.Erode(vProfile, vProfile, kernelV, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(255));
            var z = vProfile.ToImage<Gray, byte>().And(hProfile.ToImage<Gray, byte>());


            //var megedImage = vProfile.ToImage<Gray, byte>().And(hProfile.ToImage<Gray, byte>());
            //var megedImage2 = imgBinarized.Or(megedImage.ThresholdBinaryInv(new Gray(245), new Gray(255)).Dilate(1));

            //var imgas2 = hProfile.ToImage<Gray, byte>().And(duch.ToImage<Gray, byte>());

            var f = hope.Or(z.ThresholdBinaryInv(new Gray(245), new Gray(255)).Dilate(1));

            pictureBox1.Image = f.ToBitmap();
           
        }

       







        /*

        private void test_durchge2(int _hlength, int _vlength)
        {

            Mat img = imgGray.Mat;
            Mat test = imgGray.Mat;

            Mat res = new Mat(test.Size, test.Depth, test.NumberOfChannels);
            CvInvoke.BitwiseNot(img, img);

            Mat horizontal_inv = new Mat(img.Size, img.Depth, img.NumberOfChannels);
            CvInvoke.BitwiseNot(img, horizontal_inv);

            // Mask the inverted image with itself
            Mat masked_img = new Mat(img.Size, img.Depth, img.NumberOfChannels);
            CvInvoke.BitwiseAnd(img, img, masked_img, horizontal_inv);

            // Invert the masked image
            Mat masked_img_inv = new Mat(img.Size, img.Depth, img.NumberOfChannels);
            CvInvoke.BitwiseNot(masked_img, masked_img_inv);

            Mat kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(5, 1), new Point(-1, -1));
            Mat kernel2 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(1, 2), new Point(-1, -1));

            // Apply dilation to the inverted masked image
            Mat dilation = new Mat(img.Size, img.Depth, img.NumberOfChannels);
            Mat dilation2 = new Mat(img.Size, img.Depth, img.NumberOfChannels);
            CvInvoke.Dilate(masked_img_inv, dilation, kernel, new Point(-1, -1), 5, BorderType.Constant, new MCvScalar(0));
            CvInvoke.Dilate(masked_img_inv, dilation2, kernel2, new Point(-1, -1), 4, BorderType.Constant, new MCvScalar(0));
            var imgas2 = new Image<Gray, byte>(imgInput.Width, imgInput.Height);

            imgas2 = dilation2.ToImage<Gray, byte>().ThresholdBinary(new Gray(195), new Gray(255)).Sub(dilation.ToImage<Gray, byte>().ThresholdBinary(new Gray(205), new Gray(255)).Erode(1));

            var test2 = new Image<Gray, byte>(imgInput.Width, imgInput.Height);
            CvInvoke.BitwiseNot(imgas2, imgas2);
            imgas2 = imgas2.ThresholdBinary(new Gray(200), new Gray(255));
            imgBinarized = imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(185), new Gray(255));
            CvInvoke.BitwiseNot(imgBinarized, imgBinarized);
            test2 = test2.ThresholdBinary(new Gray(200), new Gray(255));
            test2 = test2.Add(imgBinarized, mask: imgas2);


            //CvInvoke.BitwiseNot(dilation, dilation);
            imgGraywork = dilation.ToImage<Gray, Byte>().ThresholdBinary(new Gray(200), new Gray(255));

            var imgas = new Image<Bgr, byte>(imgInput.Width, imgInput.Height);

            imgas = imgas.Add(imgInput, mask: imgGraywork);

            pictureBox1.Image = imgas.ToBitmap();

        }
        */




    }
}