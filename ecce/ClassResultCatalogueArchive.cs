using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ecce
{
    internal class ClassResultCatalogueArchive
    {
        public List<ClassCatalogueArchiveCol> LstArchiveColumn { get; set; } = new();
        public ClassCatalogueArchiveCol ArchiveColumn { get; set; } = new();
        private ClassOcrTess OcrTesseract { get; set; }
        public ClassResultCatalogueArchive(ClassOcrTess paramocr)
        {
            OcrTesseract = paramocr;
        }
        public async Task<string> RunOcr(ClassImage img)
        {
            ClassImage myimage = img;
            int x = 0;
            int myline = 0;
            int leftcolumnline = GetColumnLeft(myimage.ListSegmentationBox);
            string lasttitle = "";
            ArchiveColumn = new ClassCatalogueArchiveCol();
            do
            {
                if (myline != myimage.ListSegmentationBox[x].Column)
                {

                    if ((ArchiveColumn.Ditto == false) && (ArchiveColumn.TitleFile != ""))
                    {
                        lasttitle = ArchiveColumn.TitleFile;
                    }
                    LstArchiveColumn!.Add(ArchiveColumn);
                    ArchiveColumn = new ClassCatalogueArchiveCol();
                    myline++;
                }
                await Task.Run(() =>
                {
                    string text = OcrTesseract!.Readtxt_UTF8(myimage.GetSegmentImageWithMask(x));
                    text = text.Replace("-\r\n", "");
                    int rectX = myimage.ListSegmentationBox[x].Rect.X;
                    ArchiveColumn.Analysetxt(leftcolumnline, rectX, text, lasttitle);
                });
                x++;
            } while (x < myimage.ListSegmentationBox.Count);
            LstArchiveColumn!.Add(ArchiveColumn);
            return "sucess";
        }

        public static int GetColumnLeft(List<CollectionSegmentBox> lstmysegbox)
        {
            int startingpoint = 5000;
            for (int i = 0; i < lstmysegbox.Count; i++)
            {        
                if (startingpoint > lstmysegbox[i].Rect.X)
                {
                    startingpoint = lstmysegbox[i].Rect.X;
                }
            }
            return startingpoint;
        }

        public (TableLayoutPanel, TextBox, TextBox) GetResultasTable(int width, int heigth)
        {
            TextBox mytxtbox = new () {
                Multiline = true,
                Location = new Point(1, (LstArchiveColumn!.Count + 1) * 60 + 220),
                ScrollBars = ScrollBars.Vertical,
                Size = new Size(1200, 300),
                Text = GetCsv(),
            };
            TextBox myxml = new () {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new System.Drawing.Point(1, (LstArchiveColumn.Count + 1) * 60 + 580),
                Size = new System.Drawing.Size(1200, 300)
            };
            string xml_form = GetXml();
            xml_form = xml_form.Replace("<", "\r\n<");
            xml_form = xml_form.Replace("\r\n</", "</");
            myxml.Text = xml_form.Trim();
            return (GetTable(width, heigth), mytxtbox, myxml);
        }

        public TableLayoutPanel GetTable(int width, int heigth)
        {
            TableLayoutPanel mytable = new ()
            {
                Size = new System.Drawing.Size(width, heigth),
                Height = (LstArchiveColumn!.Count + 1) * 60,
                Name = "Table_Archive_Catalogue",
                TabIndex = 1,
                Location = new System.Drawing.Point(1, 200)
             };
            GetTableHEader(ref mytable);
            GetTableBody(ref mytable);
            return mytable;
        }

        private static void GetTableHEader(ref TableLayoutPanel mytable)
        {
            System.Windows.Forms.TextBox[] my_txt = new System.Windows.Forms.TextBox[9];
            string[] header = new string[] { "Id", "Title", "Contains", "Inserted", "lifespan", "Start", "End", "Remarks", "Old Id" };
            mytable.ColumnCount = 9;
            mytable.RowCount = 1;
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100));
            for (int i = 0; i < 1 + 1; i++)
            {
                mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60));
            }
            for (int i = 0; i < 9; i++)
            {
                my_txt[i] = new System.Windows.Forms.TextBox() {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Text = header[i],
                 };
                mytable.Controls.Add(my_txt[i], i, 0);
            }
        }

        private void GetTableBody(ref TableLayoutPanel mytable)
        {
            for (int i = 0; i < LstArchiveColumn!.Count; i++)
            {
                mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60));
                System.Windows.Forms.TextBox[] my_txt = new System.Windows.Forms.TextBox[9];
                for (int j = 0; j < 9; j++)
                {
                    my_txt[j] = new System.Windows.Forms.TextBox() {
                        Dock = DockStyle.Fill,
                        Multiline = true
                    };
                mytable.Controls.Add(my_txt[j], j, i + 1);
                }
                my_txt[0].Text = LstArchiveColumn[i].IdFile.Trim();
                my_txt[1].Text = LstArchiveColumn[i].TitleFile.Trim();
                my_txt[2].Text = LstArchiveColumn[i].Contains.Trim();
                my_txt[3].Text = LstArchiveColumn[i].Inserted.Trim();
                my_txt[4].Text = LstArchiveColumn[i].Lifespan.Trim();
                my_txt[5].Text = LstArchiveColumn[i].StartYear.Trim();
                my_txt[6].Text = LstArchiveColumn[i].EndYear.Trim();
                my_txt[7].Text = LstArchiveColumn[i].Remarks.Trim();
                my_txt[8].Text = LstArchiveColumn[i].FormerId.Trim();
            }
        }
        public string GetXml()
        {
            XmlDocument xmldoc = new ();
            //var encWithoutBom = new UTF8Encoding(false);
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            XmlAttribute Type = xmldoc.CreateAttribute("Type");
            Type.Value = "ArchiveCatalogue";
            root.Attributes!.SetNamedItem(Type);

            for (int i = 0; i < LstArchiveColumn!.Count; i++)
            {
                XmlNode Object = xmldoc.CreateElement("Object");
                XmlNode Id = xmldoc.CreateElement("Id");
                XmlNode Title = xmldoc.CreateElement("Title");
                XmlNode Contains = xmldoc.CreateElement("Contains");
                XmlNode Inserted = xmldoc.CreateElement("Inserted");
                XmlNode Lifespan = xmldoc.CreateElement("Lifespan");
                XmlNode Start = xmldoc.CreateElement("Start");
                XmlNode End = xmldoc.CreateElement("End");
                XmlNode Remarks = xmldoc.CreateElement("Remarks");
                XmlNode Old_id = xmldoc.CreateElement("Old_id");
                root.AppendChild(Object);
                Id.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].IdFile));
                Title.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].TitleFile));
                Contains.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].Contains));
                Inserted.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].Inserted));
                Lifespan.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].Lifespan));
                Start.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].StartYear));
                End.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].EndYear));
                Remarks.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].Remarks));
                Old_id.AppendChild(xmldoc.CreateTextNode(LstArchiveColumn[i].FormerId));
                Object.AppendChild(Id);
                Object.AppendChild(Title);
                Object.AppendChild(Contains);
                Object.AppendChild(Inserted);
                Object.AppendChild(Lifespan);
                Object.AppendChild(Start);
                Object.AppendChild(End);
                Object.AppendChild(Remarks);
                Object.AppendChild(Old_id);
            }
            return xmldoc.OuterXml;
        }

        public string GetCsv()
        {
            string my_csv = ""; ;
            for (int i = 0; i < LstArchiveColumn!.Count; i++)
            {
                my_csv += LstArchiveColumn[i].IdFile.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].TitleFile.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].Contains.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].Inserted.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].Lifespan.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].StartYear.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].EndYear.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].Remarks.Trim() + " @! ";
                my_csv += LstArchiveColumn[i].FormerId.Trim() + "\r\n";
            }
            return my_csv;
        }

        public (Button, Button, Button) GetButtons()
        {
            Button button_save_txt = new()
            {
                Location = new System.Drawing.Point(20, 50),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as Text",
                Name = "txt",
                UseVisualStyleBackColor = true,
                Visible = false
            };
            button_save_txt.Click += new System.EventHandler(this.BtnClickTxt!);

            Button button_save_xml = new()
            {
                Location = new System.Drawing.Point(140, 50),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as XML",
                Name = "xml",
                UseVisualStyleBackColor = true
            };
            button_save_xml.Click += new System.EventHandler(this.BtnClickXml!);

            Button button_save_csv = new()
            {
                Location = new System.Drawing.Point(240, 50),
                Size = new System.Drawing.Size(91, 30),
                Text = "Export as CSV",
                Name = "csv",
                UseVisualStyleBackColor = true
            };
            button_save_csv.Click += new System.EventHandler(this.BtnClickCsV!);
            return (button_save_txt, button_save_xml, button_save_csv);
        }

        public void SetFileDialog(string filter, int flag)
        {
            string txt;
            switch (flag)
            {
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

        public void BtnClickTxt(object sender, EventArgs e)
        {
            return;
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
