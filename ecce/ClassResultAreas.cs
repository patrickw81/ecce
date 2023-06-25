using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace ecce
{
    internal class ClassResultAreas
    {
        public List<CollectionAreaResult> ListResultSegmentedAreas { get; set; }= new ();
        private ClassOcrTess MyOcr { get; set; }
        public CollectionAreaResult? Myresults { get; set; }
        public ClassImage? MyImage { get; set; }

        public ClassResultAreas(ClassOcrTess ocr)
        {
            MyOcr = ocr;
        }

        public async Task<string> RunOcr(ClassImage img,int flag)
        {
            MyImage = img;
            SetDicResult();
            string result = "";
            int idx = ListResultSegmentedAreas.Count - 1;
           
            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < MyImage.SelecteAreas.LstAreas.Count; i++)
                    {
                        string text = "";
                        if (flag == 0)
                        {
                            for (int u = 0; u < MyImage.ListSegmentationBox.Count; u++)
                            {
                                Point point = new (MyImage.ListSegmentationBox[u].Rect.X, MyImage.ListSegmentationBox[u].Rect.Y);

                                if ((point.X >= MyImage.SelecteAreas.LstAreas[i].Rect.X && point.X <= MyImage.SelecteAreas.LstAreas[i].Rect.X + MyImage.SelecteAreas.LstAreas[i].Rect.Width) && (point.Y >= MyImage.SelecteAreas.LstAreas[i].Rect.Y && point.Y <= MyImage.SelecteAreas.LstAreas[i].Rect.Y + MyImage.SelecteAreas.LstAreas[i].Rect.Height))
                                {
                                    text += MyOcr.Readtxt_UTF8(MyImage.GetSegmentImageWithMask(u));
                                }
                            }
                        }
                        else
                        {
                            text += MyOcr.Readtxt_UTF8(MyImage.GetAreaWithoutMask(i));
                            MyImage.GetAreaWithoutMask(i).Save(@"D:\c#\media\tif\" + i.ToString() + ".jpg");
                        }
                        text = text.Replace("\r\n", " ");
                        ListResultSegmentedAreas[idx].DictTxtperArea.Add(MyImage.SelecteAreas.LstAreas[i].Tag, text.Trim());
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

        public (TextBox[],Label[]) GetResultPage()
        {
            TextBox[] mytxtbox = new TextBox[Myresults!.DictTxtperArea.Count];
            Label[] mylabel = new Label[Myresults.DictTxtperArea.Count];
            int y = 50;
            for (int i = 0; i < Myresults.DictTxtperArea.Count; i++)
            {
                var item = Myresults.DictTxtperArea.ElementAt(i);
                mylabel[i] = new() {
                    Location = new System.Drawing.Point(5, y),
                    Size = new System.Drawing.Size(120, 20),
                    Text = item.Key,
                };
                mytxtbox[i] = new() {
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
            XmlDocument xmldoc = new ();
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            for (int i = 0; i < ListResultSegmentedAreas.Count; i++)
            {
                XmlNode Object = xmldoc.CreateElement("Object");
                root.AppendChild(Object);
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
                Object.AppendChild(Path);
                Object.AppendChild(Filename);
                Object.AppendChild(Width);
                Object.AppendChild(Height);
                Object.AppendChild(SHA256);
                Object.AppendChild(SHA512);
                for (int y =0; y< ListResultSegmentedAreas[i].DictTxtperArea.Count; y++)
                {
                    var item = ListResultSegmentedAreas[i].DictTxtperArea.ElementAt(y);
                    XmlNode Child = xmldoc.CreateElement(item.Key);
                    Child.AppendChild(xmldoc.CreateTextNode(item.Value));
                    Object.AppendChild(Child);
                }
            }
            return xmldoc.OuterXml;
        }
        public string GetCsv()
        {
            string str = "Path q!Z SHA512 q!Z FileName q!Z Tagname = Value";
            for (int i = 0; i < ListResultSegmentedAreas.Count; i++)
            {
                str = str + "\r\n" + ListResultSegmentedAreas[i].ImgPath + " q!Z " + ListResultSegmentedAreas[i].StrSha512 + " q!Z " + ListResultSegmentedAreas[i].FileName;
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
                    str = str +   item.Key + " = " + item.Value + "\r\n";
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

            Button button_save_csv = new() {
                Location = new System.Drawing.Point(240, 98),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as CSV",
                Name = "csv",
                UseVisualStyleBackColor = true
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

