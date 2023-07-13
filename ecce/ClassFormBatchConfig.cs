using Emgu.CV.Dnn;
using Emgu.CV.OCR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ecce
{
    public partial class ClassFormBatchConfig : UserControl
    {
        public delegate void Degg_startbatch();
        public Degg_startbatch? Applystartbatch;

        public int Intervall { get; set;}
        public int Start { get; set; } = 0;
        public string? SelectedAreas { get; set; } = "";
        public bool BoolResize { get; set; } = false;
        public double ResizeFactor { get; set; }
        public bool BoolNoiseReduction { get; set; } = false;
        public bool BoolSharpen { get; set; } = false;
        public string BinararizedMode { get; set; } = "Otsu";
        public int ParamBinaraize { get; set; } = 1;
        public string? DeskewMode { get; set; }
        public bool BoolRemoveBlackDots { get; set; } = false;
        public int ParamRemoveBlackDots { get; set; } = 1;
        public bool BoolRemoveFrame { get; set; } = false;
        public string? LineEliminationMode { get; set; }
        public int LineElimminationHorizontal { get; set; } = 1;
        public int LineEliminationVertical { get; set; }=1;
        public string? LineMode { get; set; }
        public string? SegmentationMode { get; set; }
        public int SegmentationParamWidth { get; set; } = 1;
        public int SegmentationParamHeight { get; set; } = 1;
        public int SegmentationParamDestroy { get; set; } = 0;
        public string? SortMode { get; set; }
        public string? OcrMode { get; set; }
        public string OcrLanguageMode { get; set; } = "deu";
        public string OcrPageMode { get; set; } = "Auto";
        public string OcrEnginMode { get; set; } = "LstmOnly";
        public string[]? FielListAreas { get; set; }



        public ClassFormBatchConfig(int ivall, int resize_fac, int bin_prm, int line_eli_prm_w, int line_eli_prm_h, int seg_prm_w, int seg_prm_h, int seg_prm_des,int blackdotsparam, int imgwdht,int imghgt)
        {
            
            InitializeComponent();

            Num_param_seg_w.Maximum = (int)imgwdht/2;
            Num_param_seg_h.Maximum = (int)imghgt /2;


            FielListAreas = GetAreaList();
            Num_interva.Value= ivall;
            Num_resize_fac.Value= resize_fac;
            Num_paramBin.Value= bin_prm;
            Num_param_line_eli_w.Value= line_eli_prm_w;
            Num_param_line_eli_h.Value= line_eli_prm_h;
            Num_param_seg_w.Value= seg_prm_w;
            Num_param_seg_h.Value= seg_prm_h;
            Num_param_seg_destr.Value = seg_prm_des;
            LineElimminationHorizontal = line_eli_prm_h;
            LineEliminationVertical = line_eli_prm_w;
            SegmentationParamWidth = seg_prm_w;
            SegmentationParamHeight = seg_prm_h;
            SegmentationParamDestroy = seg_prm_des;
            ParamRemoveBlackDots = blackdotsparam;
            NumParamBlackDots.Value = blackdotsparam;
            ParamBinaraize = bin_prm;


            for (int i = 0; i < FielListAreas.Length; i++)
            {
                string[] x = Path.GetFileName(FielListAreas[i]).Split(".");

                this.Box_areas.Items.Add(x[0]);
            }
            string[] item = GetTessMode();
            for (int i = 0; i < item.Length; i++)
            {
                string[] x = Path.GetFileName(item[i]).Split(".");

                this.tesss_mdl_b.Items.Add(x[0]);
            }
            this.tesss_mdl_b.Text = "deu";
            this.pagmode_b.Text = "Auto";
            this.combo_enginemod_b.Text = "LstmOnly";
        }

        public static string[] GetTessMode()
        {
            string[] fileList = Directory.GetFiles(@"./tessdata/", "*.traineddata");
            return fileList;
        }

        public static string[] GetAreaList()
        {
            string[] fileList = Directory.GetFiles(@"./config_area/", "*.xml");
           
            return fileList;
        }

        private void GetComboBoxSelectAreas(object sender, EventArgs e)
        {
            if (FielListAreas == null) return;
            SelectedAreas = FielListAreas[Box_areas.SelectedIndex-1];
            
        }

        private void SetIntervall(object sender, EventArgs e)
        {
            Intervall = (int)Num_interva.Value;
        }

        private void SetResize(object sender, EventArgs e)
        {
            if (Resize_box.Checked)
            {
                BoolResize = true;
                Num_resize_fac.ReadOnly = false;
            }
            else { BoolResize = false; Num_resize_fac.ReadOnly = true; }
        }

        private void SetNoiseReduction(object sender, EventArgs e)
        {
            if (Noise_box.Checked)
            {
                BoolNoiseReduction = true;
            }
            else { BoolNoiseReduction = false; }
        }

        private void SetSharpen(object sender, EventArgs e)
        {
            if (Sharpen_box.Checked)
            {
                BoolSharpen = true;
            }
            else { BoolSharpen = false; }
        }   

        private void SetBinarizeMode(object sender, EventArgs e)
        {
            BinararizedMode = ComboBox_bina.Text.ToString();
            
            if (ComboBox_bina.SelectedIndex == 2)
            {
                Num_paramBin.ReadOnly= false;
            }
            else { Num_paramBin.ReadOnly = true; }
        }

        private void SetDeskew(object sender, EventArgs e)
        {
            DeskewMode =Box_skew.Text.ToString();
        }

        private void SetLineElimination(object sender, EventArgs e)
        {
            LineEliminationMode =Box_Lineelimi.Text.ToString();
            if (Box_Lineelimi.SelectedIndex == 2)
            {
                Num_param_line_eli_w.ReadOnly = false;
                Num_param_line_eli_h.ReadOnly = false;
            }
            else
            {
                Num_param_line_eli_w.ReadOnly = true;
                Num_param_line_eli_h.ReadOnly = true;
            }
        }

        private void SetLineMode(object sender, EventArgs e)
        {
            LineMode = Box_Linestrength.Text.ToString();
        }

        private void SetSegmentationMode(object sender, EventArgs e)
        {
            SegmentationMode = Box_Segment.Text.ToString();
            if(Box_Segment.SelectedIndex== 3) {
                Num_param_seg_w.ReadOnly = false;
                Num_param_seg_h.ReadOnly = false;
                Num_param_seg_destr.ReadOnly = false;
            }
            else{
                Num_param_seg_w.ReadOnly = true;
                Num_param_seg_h.ReadOnly = true;
                Num_param_seg_destr.ReadOnly = true;
            }
        }

        private void SetSortMode(object sender, EventArgs e)
        {
            SortMode = Box_Sorting.Text;
        }

        private void SetOcrMode(object sender, EventArgs e)
        {
            OcrMode = Box_Ocrmod.Text;
        }

        private void SetRemoveFrame(object sender, EventArgs e)
        {
            if (Box_Frame_rmv.Checked)
            {
                BoolRemoveFrame = true;
            }
            else { BoolRemoveFrame = false; }
        }

        private void Num_resize_fac_ValueChanged(object sender, EventArgs e)
        {
            ResizeFactor = (int)Num_resize_fac.Value;
        }

        private void SetOcrLanguageMode(object sender, EventArgs e)
        {
            OcrLanguageMode = this.tesss_mdl_b.Text;
        }

        private void SetOcRPageMode(object sender, EventArgs e)
        {
            OcrPageMode = this.pagmode_b.Text;
        }

        private void SetEnginMode(object sender, EventArgs e)
        {
            OcrEnginMode = this.combo_enginemod_b.Text;
        }

        private void SetStartPoint(object sender, EventArgs e)
        {
            Start = (int)Num_start.Value;
        }

        private void Num_param_line_eli_w_ValueChanged(object sender, EventArgs e)
        {
            
            LineEliminationVertical = (int)Num_param_line_eli_w.Value;
        }

        private void Num_param_line_eli_h_ValueChanged(object sender, EventArgs e)
        {
            LineElimminationHorizontal = (int)Num_param_line_eli_h.Value;
        }

        private void Num_param_seg_w_ValueChanged(object sender, EventArgs e)
        {
            SegmentationParamWidth = (int)Num_param_seg_w.Value;
        }

        private void Num_param_seg_h_ValueChanged(object sender, EventArgs e)
        {
            SegmentationParamHeight = (int)Num_param_seg_h.Value;
        }

        private void Num_param_seg_destr_ValueChanged(object sender, EventArgs e)
        {
            SegmentationParamDestroy = (int)Num_param_seg_destr.Value;
        }

        private void RemoveBlackDotsCheck(object sender, EventArgs e)
        {
            if (Box_Frame_rmv.Checked)
            {
                BoolRemoveBlackDots = true;
                NumParamBlackDots.ReadOnly= false;
            }
            else { 
                BoolRemoveBlackDots = false;
                NumParamBlackDots.ReadOnly = true;
            }
        }

        private void StartButtonClick(object sender, EventArgs e)
        {   
            /*
            List<string> funct = new();
            if (Noise_box.Checked)
            {
                funct.Add("noise_reduction");
            }
            if (Sharpen_box.Checked)
            {
                funct.Add("sharpen");
            }
            */
            Applystartbatch?.Invoke();
        }

        private void LoadSettingsClick(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new()
                {
                    Filter = "XML File (*.xml)|*.xml",
                    RestoreDirectory = true,
                    InitialDirectory = Application.StartupPath + "settings_batch"
                };
                // display the dialog and check if the user clicked OK
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the selected file name and path
                    string fileName = openFileDialog.FileName;
                    ReadXML(fileName);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  }
        }

        private void SaveSettingsClick(object sender, EventArgs e)
        {
            try
            {
                string directoryPath = Application.StartupPath + "settings_batch";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string xmlData = GetXml().OuterXml;
                SaveFileDialog saveDialog = new()
                {
                    Filter = "XML Files (*.xml)|*.xml",
                    Title = "Save File",
                    RestoreDirectory = true,
                    InitialDirectory = Application.StartupPath + "settings_batch"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK) // display the dialog and check if the user clicked OK
                {
                    string fileName = saveDialog.FileName;
                    System.IO.File.WriteAllText(fileName, xmlData);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }

        private XmlDocument GetXml()
        {
            XmlDocument xmldoc = new();
            //var encWithoutBom = new UTF8Encoding(false);
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            XmlAttribute prefix= xmldoc.CreateAttribute("prefix");
            prefix.Value = "batchsettings";
            root.Attributes!.SetNamedItem(prefix);
            xmldoc.AppendChild(root);
            XmlNode NStart = xmldoc.CreateElement("Start");
            NStart.AppendChild(xmldoc.CreateTextNode(Start.ToString()));
            XmlNode NIntervall = xmldoc.CreateElement("Intervall");
            NIntervall.AppendChild(xmldoc.CreateTextNode(Intervall.ToString()));
            XmlNode NSelectedAreas = xmldoc.CreateElement("SelectedAreas");
            NSelectedAreas.AppendChild(xmldoc.CreateTextNode(SelectedAreas));
            XmlNode NResize = xmldoc.CreateElement("Resize");
            NResize.AppendChild(xmldoc.CreateTextNode(BoolResize.ToString()));
            XmlNode NResizeFac = xmldoc.CreateElement("ResizeFactor");
            NResizeFac.AppendChild(xmldoc.CreateTextNode(ResizeFactor.ToString()));
            XmlNode NNoise = xmldoc.CreateElement("NoiseReduction");
            NNoise.AppendChild(xmldoc.CreateTextNode(BoolNoiseReduction.ToString()));
            XmlNode NSharpen = xmldoc.CreateElement("Sharpen");
            NSharpen.AppendChild(xmldoc.CreateTextNode(BoolSharpen.ToString()));
            XmlNode NBinarizeMode = xmldoc.CreateElement("BinarizeMode");
            NBinarizeMode.AppendChild(xmldoc.CreateTextNode(BinararizedMode.ToString()));
            XmlNode NBinarizeparam = xmldoc.CreateElement("BinarizeParam");
            NBinarizeparam.AppendChild(xmldoc.CreateTextNode(ParamBinaraize.ToString()));
            XmlNode NDeskew = xmldoc.CreateElement("DeskewMode");
           
            if (DeskewMode != null)
            {
               NDeskew.AppendChild(xmldoc.CreateTextNode(DeskewMode.ToString()));
            }

            XmlNode NRemoveBlackDots = xmldoc.CreateElement("RemoveBlackDots");
            NRemoveBlackDots.AppendChild(xmldoc.CreateTextNode(BoolRemoveBlackDots.ToString()));
            XmlNode NBlackDotsPram = xmldoc.CreateElement("ParamBlackDots");
            NBlackDotsPram.AppendChild(xmldoc.CreateTextNode(ParamRemoveBlackDots.ToString()));


            XmlNode NRemoveFrame = xmldoc.CreateElement("RemoveFrame");
            NRemoveFrame.AppendChild(xmldoc.CreateTextNode(BoolRemoveFrame.ToString()));
            XmlNode NLineEliminMode = xmldoc.CreateElement("LineEliminationMode");
            
            if (LineEliminationMode != null)
            {
                NLineEliminMode.AppendChild(xmldoc.CreateTextNode(LineEliminationMode.ToString()));
            }

            XmlNode NEliminationParamH = xmldoc.CreateElement("LineEliminParamWdt");
            NEliminationParamH.AppendChild(xmldoc.CreateTextNode(LineElimminationHorizontal.ToString()));

            XmlNode NEliminationParamV = xmldoc.CreateElement("LineEliminParamHgt");
            NEliminationParamV.AppendChild(xmldoc.CreateTextNode(LineEliminationVertical.ToString()));
            
            XmlNode NLineMode = xmldoc.CreateElement("LineMode");
            if (LineMode != null) { 
                NLineMode.AppendChild(xmldoc.CreateTextNode(LineMode.ToString()));
            }
            
            XmlNode NSegmentationMode = xmldoc.CreateElement("SegmentationMode");
            if (SegmentationMode != null)
            {
                NSegmentationMode.AppendChild(xmldoc.CreateTextNode(SegmentationMode.ToString()));
            }

            XmlNode NSegmentationParamWidth = xmldoc.CreateElement("SegmentationParamWidth");
            NSegmentationParamWidth.AppendChild(xmldoc.CreateTextNode(SegmentationParamWidth.ToString()));
            XmlNode NSegmentationParamHeight = xmldoc.CreateElement("SegmentationParamHeight");
            NSegmentationParamHeight.AppendChild(xmldoc.CreateTextNode(SegmentationParamHeight.ToString()));
            XmlNode NSegmentationParamDestroy = xmldoc.CreateElement("SegmentationParamDestroy");
            NSegmentationParamDestroy.AppendChild(xmldoc.CreateTextNode(SegmentationParamDestroy.ToString()));
            
            XmlNode NSortMode = xmldoc.CreateElement("SortMode");
            if (SortMode != null)
            {
                NSortMode.AppendChild(xmldoc.CreateTextNode(SortMode.ToString()));
            }

            XmlNode NOcrMode = xmldoc.CreateElement("OcrMode");
            if (OcrMode != null)
            {              
                NOcrMode.AppendChild(xmldoc.CreateTextNode(OcrMode.ToString()));
            }

            XmlNode NOcrLanguageMode = xmldoc.CreateElement("OcrLanguageMode");
            NOcrLanguageMode.AppendChild(xmldoc.CreateTextNode(OcrLanguageMode.ToString()));
            XmlNode NOcrPageMode = xmldoc.CreateElement("OcrPageMode");
            NOcrPageMode.AppendChild(xmldoc.CreateTextNode(OcrPageMode.ToString()));
            XmlNode NOcrEnginMode = xmldoc.CreateElement("OcrEnginMode");
            NOcrEnginMode.AppendChild(xmldoc.CreateTextNode(OcrEnginMode.ToString()));

            root.AppendChild(NStart);
            root.AppendChild(NIntervall);
            root.AppendChild(NSelectedAreas);
            root.AppendChild(NResize);
            root.AppendChild(NResizeFac);
            root.AppendChild(NNoise);
            root.AppendChild(NSharpen);
            root.AppendChild(NBinarizeMode);
            root.AppendChild(NBinarizeparam);
            root.AppendChild(NDeskew);
            root.AppendChild(NRemoveBlackDots);
            root.AppendChild(NBlackDotsPram);
            root.AppendChild(NRemoveFrame);
            root.AppendChild(NLineEliminMode);
            root.AppendChild(NEliminationParamH);
            root.AppendChild(NEliminationParamV);
            root.AppendChild(NLineMode);
            root.AppendChild(NSegmentationMode);
            root.AppendChild(NSegmentationParamWidth);
            root.AppendChild(NSegmentationParamHeight);
            root.AppendChild(NSegmentationParamDestroy);
            root.AppendChild(NSortMode);
            root.AppendChild(NOcrMode);
            root.AppendChild(NOcrLanguageMode);
            root.AppendChild(NOcrPageMode);
            root.AppendChild(NOcrEnginMode);

            return xmldoc;
        }

        public void ReadXML(string path)
        {
            string file = File.ReadAllText(path); 
            XmlDocument doc = new();
            doc.LoadXml(file);
            IntiSettings(doc);
        }
        private void IntiSettings(XmlDocument doc)
        {
            XmlNodeList itemList = doc.SelectNodes("root")!;
            if (itemList.Count == 0) return;
            try
            {
                if (itemList[0]!.Attributes!["prefix"] == null) return;
                if (itemList[0].Attributes["prefix"].Value != "batchsettings")
                {
                    MessageBox.Show("XML not Valid. Prefix != batchsettings");
                    return;
                }
                int  parsedvalueint = Int16.Parse(itemList[0].SelectSingleNode("Start")!.InnerText);
                Num_start.Value = parsedvalueint;
                Start = parsedvalueint;

                parsedvalueint = Int16.Parse(itemList[0].SelectSingleNode("Intervall")!.InnerText);
                Num_interva.Value = parsedvalueint;
                Intervall= parsedvalueint;              
                
                bool parsedvaluebool= bool.Parse(itemList[0].SelectSingleNode("Resize")!.InnerText);
                Resize_box.Checked = parsedvaluebool;
                BoolResize= parsedvaluebool;

                parsedvalueint=Int16.Parse(itemList[0].SelectSingleNode("ResizeFactor")!.InnerText);
                Num_resize_fac.Value= parsedvalueint;
                ResizeFactor =(double) parsedvalueint;

                parsedvaluebool = bool.Parse(itemList[0].SelectSingleNode("NoiseReduction")!.InnerText);
                Noise_box.Checked= parsedvaluebool;
                BoolNoiseReduction= parsedvaluebool;

                parsedvaluebool= bool.Parse(itemList[0].SelectSingleNode("Sharpen")!.InnerText);
                Sharpen_box.Checked= parsedvaluebool;
                BoolSharpen= parsedvaluebool;

                parsedvaluebool= bool.Parse(itemList[0].SelectSingleNode("RemoveBlackDots")!.InnerText);
                CkeckBoxBlackDots.Checked = parsedvaluebool;
                if (parsedvaluebool) { NumParamBlackDots.ReadOnly = false; }
                BoolRemoveBlackDots = parsedvaluebool;
                parsedvalueint= Int16.Parse(itemList[0].SelectSingleNode("ParamBlackDots")!.InnerText);
                NumParamBlackDots.Value = parsedvalueint;
                ParamRemoveBlackDots = parsedvalueint;

                string item = itemList[0].SelectSingleNode("BinarizeMode")!.InnerText;
                BinararizedMode = item;
                ComboBox_bina.SelectedItem = item;
                parsedvalueint= Int16.Parse(itemList[0].SelectSingleNode("BinarizeParam")!.InnerText);
                Num_paramBin.Value= parsedvalueint;

                item = itemList[0].SelectSingleNode("DeskewMode")!.InnerText;
                Box_skew.SelectedItem= itemList[0].SelectSingleNode("DeskewMode")!.InnerText;
                DeskewMode = item;

                parsedvaluebool = bool.Parse(itemList[0].SelectSingleNode("RemoveFrame")!.InnerText);
                Box_Frame_rmv.Checked= parsedvaluebool;
                BoolRemoveFrame= parsedvaluebool;

                item = itemList[0].SelectSingleNode("LineEliminationMode")!.InnerText;
                Box_Lineelimi.SelectedItem= item;
                LineEliminationMode = item;

                parsedvalueint= Int16.Parse(itemList[0].SelectSingleNode("LineEliminParamWdt")!.InnerText);
                Num_param_line_eli_w.Value = parsedvalueint;
                LineElimminationHorizontal = parsedvalueint;
                parsedvalueint =Int16.Parse(itemList[0].SelectSingleNode("LineEliminParamHgt")!.InnerText);
                Num_param_line_eli_h.Value = parsedvalueint;
                LineEliminationVertical= parsedvalueint;

                item = itemList[0].SelectSingleNode("LineMode")!.InnerText;
                Box_Linestrength.SelectedItem= item;
                LineMode= item;

                item = itemList[0].SelectSingleNode("SegmentationMode")!.InnerText;
                Box_Segment.SelectedItem= item;
                SegmentationMode = item;

                parsedvalueint = Int16.Parse(itemList[0].SelectSingleNode("SegmentationParamWidth")!.InnerText);
                Num_param_seg_w.Value= parsedvalueint;
                SegmentationParamWidth= parsedvalueint;

                parsedvalueint= Int16.Parse(itemList[0].SelectSingleNode("SegmentationParamHeight")!.InnerText);
                Num_param_seg_h.Value= parsedvalueint;
                SegmentationParamHeight= parsedvalueint;

                parsedvalueint = Int16.Parse(itemList[0].SelectSingleNode("SegmentationParamDestroy")!.InnerText);
                Num_param_seg_destr.Value= parsedvalueint;
                SegmentationParamDestroy= parsedvalueint;

                item = itemList[0].SelectSingleNode("SortMode")!.InnerText;
                Box_Sorting.SelectedItem = item;

                item= itemList[0].SelectSingleNode("OcrMode")!.InnerText;
                Box_Ocrmod.SelectedItem= item;
                OcrMode = item;

                item = itemList[0].SelectSingleNode("OcrPageMode")!.InnerText;
                pagmode_b.SelectedItem = item;
                OcrPageMode= item;

                item = itemList[0].SelectSingleNode("OcrEnginMode")!.InnerText;
                combo_enginemod_b.SelectedItem = item;
                OcrEnginMode = item;

                if (itemList[0].SelectSingleNode("SelectedAreas")!.InnerText != "")
                {
                    item = itemList[0].SelectSingleNode("SelectedAreas")!.InnerText;
                    string[] proof = item.Split("/");
                    string[] x = proof[^1].Split(".");

                    if (Box_areas.Items.Contains(x[0]))
                    {

                        Box_areas.SelectedItem = x[0];
                        Box_areas.Text = x[0];
                        SelectedAreas = FielListAreas[Box_areas.SelectedIndex - 1];
                    }
                }

                item = itemList[0].SelectSingleNode("OcrLanguageMode")!.InnerText;
                if (tesss_mdl_b.Items.Contains(item))
                {
                    tesss_mdl_b.SelectedItem = item;
                    OcrLanguageMode = item;
                 

                }

            } catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void Num_paramBinChange(object sender, EventArgs e)
        {
            ParamBinaraize =(int)Num_paramBin.Value;
        }

        private void SetParamBlackDots(object sender, EventArgs e)
        {
            ParamRemoveBlackDots=(int)NumParamBlackDots.Value;
        }
    }
}
