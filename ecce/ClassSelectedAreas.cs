using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.SqlTypes;
using System.Web;
using System.IO;

namespace ecce
{
    internal class ClassSelectedAreas
    {
        public List<CollectionArea> LstAreas { get; set; } = new ();
        public List<string>LstTagNames { get; set; } = new ();

        public Image<Bgr, byte> DrawAreas(Image<Bgr, Byte> img)
        {
            
            if (LstAreas.Count > 0)
            {
                for (int i = 0; i < LstAreas.Count; i++)
                {
                    CvInvoke.Rectangle(img, LstAreas[i].Rect, new MCvScalar(200, 205, 0), 1);
                    CvInvoke.PutText(img, LstAreas[i].Tag, new Point(LstAreas[i].Rect.X, LstAreas[i].Rect.Y), FontFace.HersheyPlain, 2, new MCvScalar(20, 50, 255));
                }
            }
            return img;
        }

        public void ExportAreaAsXmL()
        {
            try
            {
                string xmlData = GetXml().OuterXml;
                SaveFileDialog saveDialog = new()
                {
                    Filter = "XML Files (*.xml)|*.xml",
                    Title = "Save File",
                    RestoreDirectory = true,
                    InitialDirectory = Application.StartupPath + "config_area"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK) // display the dialog and check if the user clicked OK
                {
                    string fileName = saveDialog.FileName;
                    System.IO.File.WriteAllText(fileName, xmlData);
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); };
        }

        private XmlDocument GetXml()
        {
            XmlDocument xmldoc = new ();
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            XmlAttribute prefix = xmldoc.CreateAttribute("prefix");
            prefix.Value = "areas";
            root.Attributes!.SetNamedItem(prefix);
            xmldoc.AppendChild(root);
            for (int i =0; i< LstAreas.Count; i++)
            {
                XmlNode Object = xmldoc.CreateElement("Object");
                XmlNode rectang = xmldoc.CreateElement("rect");
                XmlNode tagname = xmldoc.CreateElement("tag");
                root.AppendChild(Object);
                rectang.AppendChild(xmldoc.CreateTextNode(LstAreas[i].Rect.ToString()));
                tagname.AppendChild(xmldoc.CreateTextNode(LstAreas[i].Tag));
                Object.AppendChild(rectang);
                Object.AppendChild(tagname);
             }
            return xmldoc;
        }

        public void ImportAreaAsXml()
        {
            try
            {
                OpenFileDialog openFileDialog = new()
                {
                    Filter = "XML File (*.xml)|*.xml",
                    RestoreDirectory = true,
                    InitialDirectory = Application.StartupPath + "config_area"
                };
                // display the dialog and check if the user clicked OK
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the selected file name and path
                    string fileName = openFileDialog.FileName;
                    ReadXml(fileName);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message ); }    
        }

        public void ReadXml(string path)
        {
            string file = File.ReadAllText(path);
            XmlDocument doc = new ();
            doc.LoadXml(file);
            InitAreas(doc);
        }

        private void InitAreas(XmlDocument doc)
        {

            XmlNodeList rootList = doc.SelectNodes("root")!;
            if (rootList[0].Attributes["prefix"].Value != "areas")
            {
                MessageBox.Show("XML not Valid. Prefix != areas");
                return;
            }

            XmlNodeList itemList = doc.SelectNodes("//Object")!;
            if (itemList.Count != 0)
            {
                
                for (int i = 0; i < itemList.Count; i++)
                {
                    CollectionArea mylist= new ();
                    LstAreas.Add(mylist);
                    
                    string tagname = itemList[i]!.SelectSingleNode("tag")!.InnerText;
                    string rect_str = itemList[i]!.SelectSingleNode("rect")!.InnerText;
                                        
                    string[] parts = rect_str.Split(new char[] { ',', '{', '}', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    int x = int.Parse(parts[0].Split('=')[1]);
                    int y = int.Parse(parts[1].Split('=')[1]);
                    int width = int.Parse(parts[2].Split('=')[1]);
                    int height = int.Parse(parts[3].Split('=')[1]);
                    Rectangle myrect = new (x, y, width, height);
                    LstAreas[i].Rect = myrect;
                    LstAreas[i].Tag= tagname;
                }
            } 
        }
    } 
}
