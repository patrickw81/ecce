using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace ecce
{
    internal class ClassResultSegment
    {
        private ClassOcrTess OcrTesseract { get; set; }
        public ClassImage? MyImage { get; set; }
        public List<CollectionAreaResult> ListResultSegmentedAreas { get; set; } = new();
        public CollectionAreaResult? Myresults { get; set; }     

        public ClassResultSegment(ClassOcrTess ocr)
        {
            OcrTesseract = ocr;
        }

        public async Task<string> RunOcr(ClassImage img)
        {
            MyImage = img;           
            SetDicResult();
            string result = "";
           
            int idx = ListResultSegmentedAreas.Count - 1;

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < MyImage.ListSegmentationBox.Count; i++)
                    {
                        string text = "";
                        text += OcrTesseract.Readtxt_UTF8(MyImage.GetSegmentationWithoutMask(i));
                        //MyImage.GetSegmentImageWithMask(i).Save(@"D:\c#\media\tif\"+i.ToString()+".jpg");
                        text = text.Replace("\r\n", " ");
                        
                        ListResultSegmentedAreas[idx].DictTxtperArea.Add(i.ToString(), text.Trim());
                        ListResultSegmentedAreas[idx].Rectangles.Add(MyImage.ListSegmentationBox[i].Rect);

                    }
                    result = "success";
                });
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        public void SetDicResult()
        {
            Myresults = new CollectionAreaResult(MyImage!.StrSHA256, MyImage.StrSHA512, MyImage.ImagePath, MyImage.OriginalWidth, MyImage.OriginalHeight);
            ListResultSegmentedAreas.Add(Myresults);
        }
        public (TextBox[], Label[]) GetResultPage()
        {
            TextBox[] mytxtbox = new TextBox[Myresults!.DictTxtperArea.Count];
            Label[] mylabel = new Label[Myresults.DictTxtperArea.Count];
            int y = 50;
            for (int i = 0; i < Myresults.DictTxtperArea.Count; i++)
            {
                var item = Myresults.DictTxtperArea.ElementAt(i);
                mylabel[i] = new()
                {
                    Location = new System.Drawing.Point(5, y),
                    Size = new System.Drawing.Size(120, 20),
                    Text = item.Key,
                };
                mytxtbox[i] = new()
                {
                    Location = new System.Drawing.Point(160, y),
                    Size = new System.Drawing.Size(600, 20),
                    Text = item.Value
                };
                y += 30;
            }
            return (mytxtbox, mylabel);
        }

        public void SetFileDialog(string filter, int flag)
        {
            string txt;
            switch (flag)
            {
                case 0: txt = GetString(); break;
                case 1: txt = GetXml(); break;
                case 2: txt = GetCsv(); break;
                default: return;
            }
            try
            {
                SaveFileDialog saveDialog = new()
                {
                    Filter = filter,
                    Title = "Save File",
                    RestoreDirectory = true,
                    InitialDirectory = Properties.ProjPath.Default.LastPath
                };

                if (saveDialog.ShowDialog() == DialogResult.OK) // display the dialog and check if the user clicked OK
                {
                    string fileName = saveDialog.FileName;
                    System.IO.File.WriteAllText(fileName, txt);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }

        public string GetXml()
        {
            XmlDocument xmldoc = new();
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            XmlAttribute Type = xmldoc.CreateAttribute("Type");
            Type.Value = "Segmented";
            root.Attributes!.SetNamedItem(Type);

            for (int i = 0; i < ListResultSegmentedAreas.Count; i++)
            {
                XmlNode Object = xmldoc.CreateElement("Object");
                root.AppendChild(Object);
                XmlNode ImgData = xmldoc.CreateElement("ImgData");
                XmlNode Path = xmldoc.CreateElement("Path");
                XmlNode Filename = xmldoc.CreateElement("Filename");
                XmlNode SHA256 = xmldoc.CreateElement("SHA256");
                XmlNode SHA512 = xmldoc.CreateElement("SHA512");
                XmlNode Width = xmldoc.CreateElement("Width");
                XmlNode Height = xmldoc.CreateElement("Height");
                Path.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].ImgPath));
                Filename.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].FileName));
                Width.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].ImgWidth.ToString()));
                Height.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].ImgHeight.ToString()));
                SHA256.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].StrSha256));
                SHA512.AppendChild(xmldoc.CreateTextNode(ListResultSegmentedAreas[i].StrSha512));
                ImgData.AppendChild(Path);
                ImgData.AppendChild(Filename);
                ImgData.AppendChild(Width);
                ImgData.AppendChild(Height);
                ImgData.AppendChild(SHA256);
                ImgData.AppendChild(SHA512);
                Object.AppendChild(ImgData);
                XmlNode TxtSegments = xmldoc.CreateElement("TxtSegments");
                for (int y = 0; y < ListResultSegmentedAreas[i].DictTxtperArea.Count; y++)
                {
                    var item = ListResultSegmentedAreas[i].DictTxtperArea.ElementAt(y);
                    XmlNode Child = xmldoc.CreateElement("Segment");
                    XmlAttribute ID = xmldoc.CreateAttribute("ID");
                    ID.Value = y.ToString();
                    Child.Attributes!.SetNamedItem(ID);                   
                    XmlAttribute HPOS = xmldoc.CreateAttribute("HPOS");
                    HPOS.Value = ListResultSegmentedAreas[i].Rectangles[y].X.ToString();
                    Child.Attributes!.SetNamedItem(HPOS);
                    XmlAttribute VPOS = xmldoc.CreateAttribute("VPOS");
                    VPOS.Value = ListResultSegmentedAreas[i].Rectangles[y].Y.ToString();
                    Child.Attributes!.SetNamedItem(VPOS);
                    XmlAttribute WIDTH = xmldoc.CreateAttribute("WIDTH");
                    WIDTH.Value = ListResultSegmentedAreas[i].Rectangles[y].Width.ToString();
                    Child.Attributes!.SetNamedItem(WIDTH);
                    XmlAttribute HEIGHT = xmldoc.CreateAttribute("HEIGHT");
                    HEIGHT.Value = ListResultSegmentedAreas[i].Rectangles[y].Height.ToString();
                    Child.Attributes!.SetNamedItem(HEIGHT);
                    Child.AppendChild(xmldoc.CreateTextNode(item.Value));
                    TxtSegments.AppendChild(Child);
                }
                Object.AppendChild(TxtSegments);
            }
            return xmldoc.OuterXml;
        }
        public string GetCsv()
        {
            string str = "Path q!Z SHA256 q!Z FileName q!Z Tagname = Value";
            for (int i = 0; i < ListResultSegmentedAreas.Count; i++)
            {
                str = str + "\r\n" + ListResultSegmentedAreas[i].ImgPath + " q!Z " + ListResultSegmentedAreas[i].StrSha256 + " q!Z " + ListResultSegmentedAreas[i].FileName;
                for (int y = 0; y < ListResultSegmentedAreas[i].DictTxtperArea.Count; y++)
                {
                    var item = ListResultSegmentedAreas[i].DictTxtperArea.ElementAt(y);
                    str = str + " q!Z " + item.Key + " = " + item.Value;
                }
            }
            return str;
        }

        private string GetString()
        {
            string str = "";
            for (int i = 0; i < ListResultSegmentedAreas.Count; i++)
            {
                str = str + "*******************  " + ListResultSegmentedAreas[i].ImgPath + " ******** " + ListResultSegmentedAreas[i].FileName + "*********************\r\n";
                for (int y = 0; y < ListResultSegmentedAreas[i].DictTxtperArea.Count; y++)
                {
                    var item = ListResultSegmentedAreas[i].DictTxtperArea.ElementAt(y);
                    str = str + "Segment " + item.Key + " = " + item.Value + "\r\n";
                }
            }
            return str;
        }
        public (Button, Button, Button) GetButtons()
        {
            Button button_save_txt = new()
            {
                Location = new Point(20, 98),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as Text",
                Name = "txt",
                UseVisualStyleBackColor = true
            };
            button_save_txt.Click += new System.EventHandler(this.BtnClickTxt!);

            Button button_save_xml = new()
            {
                Location = new Point(140, 98),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as XML",
                Name = "xml",
                UseVisualStyleBackColor = true
            };
            button_save_xml.Click += new System.EventHandler(this.BtnClickXml!);

            Button button_save_csv = new()
            {
                Location = new System.Drawing.Point(240, 98),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as CSV",
                Name = "csv",
                UseVisualStyleBackColor = true,
                Visible = false
            };
            button_save_csv.Click += new System.EventHandler(this.BtnClickCsV!);
            return (button_save_txt, button_save_xml, button_save_csv);
        }
        public void BtnClickTxt(object sender, EventArgs e)
        {
            SetFileDialog("txt Files (*.txt)|*.txt", 0);
        }
        public void BtnClickXml(object sender, EventArgs e)
        {
            SetFileDialog("XML Files (*.xml)|*.xml", 1);
        }
        public void BtnClickCsV(object sender, EventArgs e)
        {
            SetFileDialog("CSV Files (*.csv)|*.csv", 2);
        }

    }
}
