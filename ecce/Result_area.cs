using Emgu.CV.Aruco;
using Emgu.CV.Flann;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class Result_area
    {
        public Dictionary<string, string> dict_myresult;
       

        public Result_area() {

            dict_myresult = new Dictionary<string, string>();
        }

        public string print_dict()
        {
            string[] myitem = new string[dict_myresult.Count];
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> pair in dict_myresult)
            {
                list.Add(pair.Key + "=" + pair.Value);
            }
            string result = string.Join(",", list);
            return result;

        }

        public (System.Windows.Forms.TextBox[], System.Windows.Forms.Label[]) generate_resultpage()
        {
            System.Windows.Forms.TextBox[] mytxtbox = new System.Windows.Forms.TextBox[dict_myresult.Count];
            System.Windows.Forms.Label[] mylabel = new Label[dict_myresult.Count];
       
            
            int y = 50;
            
            for(int i =0; i < dict_myresult.Count;i++)
            {
                var item = dict_myresult.ElementAt(i);
                mylabel[i] = new System.Windows.Forms.Label();
                mylabel[i].Location= new System.Drawing.Point(5, y);
                mylabel[i].Size = new System.Drawing.Size(120, 20);
                mylabel[i].Text = item.Key;


                mytxtbox[i]= new System.Windows.Forms.TextBox();
                mytxtbox[i].Location = new System.Drawing.Point(160,y );
                mytxtbox[i].Size = new System.Drawing.Size(600, 20);
                mytxtbox[i].Text = item.Value;

                y = y + 30;
            }

                return (mytxtbox, mylabel);
        }

    }
}
