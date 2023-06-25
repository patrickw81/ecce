using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;

namespace ecce
{
    internal class ClassResultTxtOnly
    {
        public List<CollectionTxtResult> LstResultTxtOnly { get; set; }= new ();
        public CollectionTxtResult? ResultPage { get; set; }
        private ClassOcrTess OcrTesseract { get; set; }

        public ClassResultTxtOnly(ClassOcrTess ocr)
        {
            OcrTesseract = ocr;
        }

        public async Task<string> RunOcr(Image<Gray, Byte> imgBinarized, CollectionTxtResult page)
        {          
            ResultPage = page;
            ResultPage.FileName = SeparateFilename();
            string result = "";
            try
            {
                await Task.Run(() =>
                {
                    string txt = OcrTesseract.Readtxt_UTF8(imgBinarized);
                    ResultPage!.Form_text = txt;
                    LstResultTxtOnly.Add(ResultPage!);
                    CombineTxt();
                    result = "success";
                });
            }
            catch (Exception ex) { result = ex.ToString(); }
            return result;
        }

        private string SeparateFilename()
        {
            string[] str = ResultPage!.ImgPath.Split("\\");
            return str[^1];
        }

        private void CombineTxt()
        {
            ResultPage!.Text = ResultPage.Form_text.Replace("-\r\n", "");
            ResultPage.Text = ResultPage.Text.Replace("\r\n", " ");
        }

        public TableLayoutPanel GetResultasTable(int width = 1000, int heigth = 1000)
        {
            TableLayoutPanel mytable = new ()
            {
                ColumnCount = 2,
                RowCount = 8,
                Size = new System.Drawing.Size(width, heigth),
                Location = new System.Drawing.Point(1, 50)
            };
            mytable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 800));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mytable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            
            TextBox[] my_txt = new TextBox[16];

            for (int i = 0; i < 8; i++)
            {
                my_txt[i] = new() {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Multiline = true,
                };
                mytable.Controls.Add(my_txt[i], 0, i);
            }

            my_txt[0].Text = "Formatted Text";
            my_txt[1].Text = "Plain Text";
            my_txt[2].Text = "summary";
            my_txt[3].Text = "title";
            my_txt[4].Text = "Time";
            my_txt[5].Text = "Place";
            my_txt[6].Text = "Personen";
            my_txt[7].Text = "Classifikation";

            int x = 0;

            for (int i = 8; i < 16; i++)
            {
                my_txt[i] = new TextBox() { 
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Vertical
                };
                mytable.Controls.Add(my_txt[i], 1, x);
                x++;
            }

            my_txt[8].Text = ResultPage!.Form_text;
            my_txt[9].Text = ResultPage.Text;
            my_txt[10].Text = ResultPage.Summary;
            my_txt[11].Text = ResultPage.Title;
            my_txt[12].Text = ResultPage.Time;
            my_txt[13].Text = ResultPage.Place;
            my_txt[14].Text = ResultPage.Person;
            my_txt[15].Text = ResultPage.Classifikation;

            return mytable;
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

        public void SetFileDialog(string filter, int flag)
        {
            string txt;
            switch (flag) {
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

        private string GetString() {
            string str = "";
            for (int i = 0; i < LstResultTxtOnly.Count; i++)
            {
                str += LstResultTxtOnly[i].Form_text;
            }
            return str;
        }

        private string GetXml()
        {
            XmlDocument xmldoc = new ();
            //var encWithoutBom = new UTF8Encoding(false);
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            for (int i = 0; i < LstResultTxtOnly.Count; i++) {

                XmlNode Object = xmldoc.CreateElement("Object");
                XmlNode Path = xmldoc.CreateElement("Path");
                XmlNode Filename = xmldoc.CreateElement("Filename");
                XmlNode SHA256 = xmldoc.CreateElement("SHA256");
                XmlNode SHA512 = xmldoc.CreateElement("SHA512");
                XmlNode Width = xmldoc.CreateElement("Width");
                XmlNode Height = xmldoc.CreateElement("Height");
                XmlNode Text = xmldoc.CreateElement("Text");
                Path.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].ImgPath));
                Filename.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].FileName));
                Width.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].ImgWidth.ToString()));
                Height.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].ImgHeight.ToString()));
                SHA256.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].StrSha256));
                SHA512.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].StrSha512));
                Text.AppendChild(xmldoc.CreateTextNode(LstResultTxtOnly[i].Text));
                root.AppendChild(Object);
                Object.AppendChild(Path);
                Object.AppendChild(Filename);
                Object.AppendChild(Width);
                Object.AppendChild(Height);
                Object.AppendChild(SHA256);
                Object.AppendChild(SHA512);
                Object.AppendChild(Text);

                string[] block = LstResultTxtOnly[i].Form_text.Split("\r\n\r\n");

                for (int y = 0; y < block.Length; y++)
                {
                    XmlNode Block = xmldoc.CreateElement("Block");
                    Object.AppendChild(Block);
                    string[] lines = block[y].Split("\r\n");
                    for (int u = 0; u < lines.Length; u++)
                    {
                        XmlNode Line = xmldoc.CreateElement("Line");
                        Line.AppendChild(xmldoc.CreateTextNode(lines[u]));
                        Block.AppendChild(Line);
                    }

                }

            }
            return xmldoc.OuterXml;
        }
        private string GetCsv()
        {
            string str = "Path q!Z SHA256 q!Z FileName q!Z Text \r\n";
            for (int i = 0; i < LstResultTxtOnly.Count; i++)
            {
                str = str + LstResultTxtOnly[i].ImgPath + " q!Z " + LstResultTxtOnly[i].StrSha256 + " q!Z " + LstResultTxtOnly[i].FileName + " q!Z " + LstResultTxtOnly[i].Text + "\r\n";
            }
            return str;
        }

        public (Button, Button, Button) GetButtons()
        {
            Button button_save_txt = new() {
                Location = new System.Drawing.Point(20, 98),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as Text",
                Name = "txt",
                UseVisualStyleBackColor = true,
            };
            button_save_txt.Click += new System.EventHandler(this.BtnClickTxt!);

            Button button_save_xml = new()
            {
                Location = new System.Drawing.Point(140, 98),
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
            return (button_save_txt,button_save_xml,button_save_csv);
        }
    }
}
