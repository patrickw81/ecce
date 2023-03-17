using Emgu.CV.LineDescriptor;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ecce
{
    internal class Archive_page
    {
        public List<Archive_Column> lst_archiv_colmn;

        public Archive_page()
        {
            lst_archiv_colmn = new List<Archive_Column>();
        }

        public (TableLayoutPanel, System.Windows.Forms.TextBox, System.Windows.Forms.TextBox) gen_archivpage(int width, int heigth)
        {
            System.Windows.Forms.TextBox mytxtbox= new System.Windows.Forms.TextBox();
            mytxtbox.Multiline = true;
            mytxtbox.Location = new System.Drawing.Point(1, (lst_archiv_colmn.Count + 1) * 60 + 50);
            mytxtbox.ScrollBars = ScrollBars.Vertical;
            mytxtbox.Size = new System.Drawing.Size(1200, 300);
            mytxtbox.Text = csv_file();
            System.Windows.Forms.TextBox myxml = new System.Windows.Forms.TextBox();
            myxml.Multiline = true;
            myxml.ScrollBars = ScrollBars.Vertical;
            myxml.Location = new System.Drawing.Point(1, (lst_archiv_colmn.Count + 1) * 60 + 380);
            myxml.Size = new System.Drawing.Size(1200, 300);
            string xml_form = xml_file().OuterXml;
            xml_form = xml_form.Replace("<", "\r\n<");
            xml_form = xml_form.Replace("\r\n</", "</");
            myxml.Text = xml_form.Trim();


            return (creat_table(width, heigth), mytxtbox, myxml);
        }

        public TableLayoutPanel creat_table(int width, int heigth)
        {
            TableLayoutPanel mytable = new TableLayoutPanel();
            mytable.Size = new System.Drawing.Size(width, heigth);
            mytable.Height = (lst_archiv_colmn.Count + 1) * 60;
            //mytable.Dock = System.Windows.Forms.DockStyle.Fill;
            mytable.Name = "Table_Archive_Catalogue";
            mytable.TabIndex = 1;
            mytable.Location = new System.Drawing.Point(1, 50);
            genrate_header(ref mytable);
            generate_tabl_body(ref mytable);
            string mycsv = csv_file();

            return mytable;
        }

        private void genrate_header(ref TableLayoutPanel mytable)
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
                my_txt[i] = new System.Windows.Forms.TextBox();
                my_txt[i].Dock = DockStyle.Fill;
                my_txt[i].ReadOnly = true;
                my_txt[i].Text = header[i];
                mytable.Controls.Add(my_txt[i], i, 0);
            }
        }

        private void generate_tabl_body(ref TableLayoutPanel mytable)
        {
            for (int i = 0; i < lst_archiv_colmn.Count; i++)
            {
                mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60));
                System.Windows.Forms.TextBox[] my_txt = new System.Windows.Forms.TextBox[9];
                for (int j = 0; j < 9; j++)
                {
                    my_txt[j] = new System.Windows.Forms.TextBox();
                    my_txt[j].Dock = DockStyle.Fill;
                    my_txt[j].Multiline = true;
                    mytable.Controls.Add(my_txt[j], j, i + 1);
                }
                my_txt[0].Text = lst_archiv_colmn[i].id.Trim();
                my_txt[1].Text = lst_archiv_colmn[i].title.Trim();
                my_txt[2].Text = lst_archiv_colmn[i].contains.Trim();
                my_txt[3].Text = lst_archiv_colmn[i].inserted.Trim();
                my_txt[4].Text = lst_archiv_colmn[i].lifespan.Trim();
                my_txt[5].Text = lst_archiv_colmn[i].start.Trim();
                my_txt[6].Text = lst_archiv_colmn[i].end.Trim();
                my_txt[7].Text = lst_archiv_colmn[i].remarks.Trim();
                my_txt[8].Text = lst_archiv_colmn[i].old_id.Trim();

            }
        }

        public XmlDocument xml_file()
        {
            XmlDocument xmldoc = new XmlDocument();
            var encWithoutBom = new UTF8Encoding(false);
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            for (int i = 0; i < lst_archiv_colmn.Count; i++)
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

                Id.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].id));
                Title.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].title));
                Contains.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].contains));
                Inserted.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].inserted));
                Lifespan.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].lifespan));
                Start.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].start));
                End.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].end));
                Remarks.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].remarks));
                Old_id.AppendChild(xmldoc.CreateTextNode(lst_archiv_colmn[i].old_id));
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
            //Debug.WriteLine(xmldoc.OuterXml);
            return xmldoc;
        }

        public string csv_file()
        {
            string my_csv = ""; ;
            for (int i = 0; i < lst_archiv_colmn.Count; i++)
            {
                my_csv += lst_archiv_colmn[i].id.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].title.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].contains.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].inserted.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].lifespan.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].start.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].end.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].remarks.Trim() + " @! ";
                my_csv += lst_archiv_colmn[i].old_id.Trim() + "\r\n";
                

            }
            return my_csv;
        }
    }
}

