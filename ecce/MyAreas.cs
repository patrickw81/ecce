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

namespace ecce
{
    internal class MyAreas
    {
        public List<Myarea_locate> lst_myarea_loc = new List<Myarea_locate>();
        public List<string>lst_tagnames= new List<string>();
        Image<Bgr, Byte> imgShowbox;

        public Image<Bgr, byte> drawareas(Image<Bgr, Byte> img)
        {
            if (lst_myarea_loc.Count > 0)
            {
                Debug.WriteLine(lst_myarea_loc.Count());
                for (int i = 0; i < lst_myarea_loc.Count(); i++)
                {
                    Debug.WriteLine("dsa ist mein iiiii" + i);
                    CvInvoke.Rectangle(img, lst_myarea_loc[i].rect, new MCvScalar(200, 205, 0), 1);
                    CvInvoke.PutText(img, lst_myarea_loc[i].tag, new Point(lst_myarea_loc[i].rect.X, lst_myarea_loc[i].rect.Y), FontFace.HersheyPlain, 2, new MCvScalar(20, 50, 255));

                }
            }
            return img;
        }

        public void save_area()
        {
            try
            {
                string xmlData = write_xml().OuterXml;
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "XML Files (*.xml)|*.xml"; // set the file filter
                saveDialog.Title = "Save File"; // set the dialog title
                saveDialog.RestoreDirectory = true;
                
                
                saveDialog.InitialDirectory = Application.StartupPath+ "config_area"; 
                //saveDialog.InitialDirectory = @"\config_area"; // set the initial directory relative to the current directory

                if (saveDialog.ShowDialog() == DialogResult.OK) // display the dialog and check if the user clicked OK
                {
                    string fileName = saveDialog.FileName;
                    System.IO.File.WriteAllText(fileName, xmlData);
                }
            }catch(Exception ex) { };
        //D:\c#\emgu_tess\ecce\ecce\config_area\

        }
        private XmlDocument write_xml()
        {
            XmlDocument xmldoc = new XmlDocument();
            var encWithoutBom = new UTF8Encoding(false);
            XmlNode docNode = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmldoc.AppendChild(docNode);
            XmlNode root = xmldoc.CreateElement("root");
            xmldoc.AppendChild(root);
            for (int i =0; i< lst_myarea_loc.Count; i++)
            {
                XmlNode Object = xmldoc.CreateElement("Object");
                XmlNode rectang = xmldoc.CreateElement("rect");
                XmlNode tagname = xmldoc.CreateElement("tag");
                root.AppendChild(Object);
                rectang.AppendChild(xmldoc.CreateTextNode(lst_myarea_loc[i].rect.ToString()));
                tagname.AppendChild(xmldoc.CreateTextNode(lst_myarea_loc[i].tag));
                Object.AppendChild(rectang);
                Object.AppendChild(tagname);
             }
            Debug.WriteLine(xmldoc.OuterXml);
            return xmldoc;
        }


        public void load_xml()
        {
            try
            {   
                OpenFileDialog openFileDialog = new OpenFileDialog();
                // set the file filter for the dialog
                openFileDialog.Filter = "XML File (*.xml)|*.xml";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.InitialDirectory = Application.StartupPath + "config_area";

                // display the dialog and check if the user clicked OK
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the selected file name and path
                    string fileName = openFileDialog.FileName;
                    string fileContents = File.ReadAllText(fileName);
                    
                    
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(fileContents);

                    read_xml(doc);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        private void read_xml(XmlDocument doc)
        {
                                
            XmlNodeList itemList = doc.SelectNodes("//Object");

            
            for (int i =0; i<itemList.Count;i++)
            {
                
                
                // get the "name" element of the "item" node
                
                string rect_str = itemList[i].SelectSingleNode("rect").InnerText;
                string tagname = itemList[i].SelectSingleNode("tag").InnerText;

                string[] parts = rect_str.Split(new char[] { ',', '{', '}', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                int width = int.Parse(parts[3]);
                int height = int.Parse(parts[4]);
                Rectangle myrect = new Rectangle(x, y, width, height);
                lst_myarea_loc[i].rect=myrect;
                lst_myarea_loc[i].tag = tagname;
            }
        }
    }


   
}
