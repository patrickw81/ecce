using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ecce
{
    internal class Archive_Column
    {
        public string id { get; set; } = "";
        public string title { get; set; } = "";
        public string contains { get; set; } = "";
        public string inserted { get; set; } = "";
        public string lifespan { get; set; } = "";
        public string start { get; set; } = "";
        public string end { get; set; } = "";
        public string remarks { get; set; } = "";
        public string old_id { get; set; } = "";
        public bool dsgl { get; set; } = false;

        public void analysetxt(int startpoint, int leftpoint, string text, string lasttitel)
        {
            Regex jahreszahl = new Regex(@"\d{4}");
            Regex contains = new Regex(@"^[a-z]{1,2}\)|^[a-z]{1,2}\.|Enthält");
            Regex desg = new Regex(@"Desgl|Desg1|Desgleichen|Desg1eichen");
            Regex inserted = new Regex(@"Darin");
            
            string[] line = text.Split('\n');
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = line[i].Replace("\r", "");
                line[i] = line[i].Replace("\n", "");
            }
            if (line[line.Length - 1] == "")
            {
                line = line.SkipLast(1).ToArray();
            }

            int flag = 0;
            int start = 0;
            string[] element;
            if (leftpoint - 35 < startpoint)
            {
                flag = 0;
                string pattern_sig = @"(\d+?\.)|(\d+?\))";
                element = Regex.Split(line[0], pattern_sig);
                //element = line[0].Split(new[] { '.' }, 2);
                if (element[0] == "")
                {
                    element = element.Skip(1).ToArray();
                }
               
                start = 1;

                if (element.Length > 1)
                {
                    try
                    {
                        
                        id += element[0];
                        if (desg.IsMatch(element[1]))
                        {
                            flag = 1;
                            dsgl = true;
                            checkdsgl(element[1], lasttitel);
                            
                        }
                        else
                        {
                            creat_title(element[1]);
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    start = 0;
                }
            }
            if (startpoint + 20 < leftpoint)
            {
                flag = 5;
            }
            if (startpoint + 500 < leftpoint)
            {
                flag = 6;
            }
            for (int i = start; i < line.Length; i++)
            {

                element = line[i].Split(new[] { ' ' }, 2);

                if (desg.IsMatch(element[0]))
                {
                    flag = 1;
                    dsgl = true;
                }
                else if (inserted.IsMatch(element[0]))
                {
                    flag = 2;
                }
                else if (jahreszahl.IsMatch(element[0]))
                {
                    flag = 3;


                }
                else if (contains.IsMatch(element[0]))
                {
                    flag = 4;
                }
                switch (flag)
                {
                    case 0:
                        creat_title(line[i]);
                        break;
                    case 1: //Desgl
                        checkdsgl(line[i], lasttitel);
                        flag = 0;
                        break;
                    case 2:
                        creat_inserted(line[i]);
                        break;
                    case 3:
                       
                        creat_lifespan(line[i]);
                        creat_years(line[i]);
                        break;
                    case 4:
                        creat_contains(line[i]);
                        break;
                    case 5:
                        creat_remarks(line[i]);
                        break;
                    case 6:
                        creat_oldid(line[i]);
                        break;
                }
            }
        }
         private void checkdsgl(string text, string lasttitel)
        {
           
            string pattern = "(Desgleichen)|(Desg1eichen)|(Desg1.)|(Desgl.)";
            
            string[] element = Regex.Split(text.Trim(), pattern);
            if (element[0] == "")
            {
                element = element.Skip(1).ToArray();
            }

            if (element.Length > 0)
            {
                title = title + lasttitel.Trim() + " " + element[element.Length - 1];
                return;
            }
            else
            {
                
                title = title + lasttitel.Trim();
            }
            return;
        }
        private void creat_inserted(string text)
        {

            inserted = inserted.Trim()+ " " + text.Trim();
        }
        private void creat_lifespan(string text)
        {
            lifespan += text;
        }
        private void creat_title(string text)
        {
            title = title.Trim() + " " + text.Trim();
        }
        private void creat_contains(string text)
        {
            contains = contains.Trim() + " " + text.Trim();
        }
        private void creat_remarks(string text)
        {
            remarks = remarks.Trim() + " " + text.Trim();
        }
        private void creat_oldid(string text)
        {
            old_id = old_id.Trim() + " " + text.Trim();
        }
        public void creat_years(string text)
        {
            string[] jahre = text.Split("-");

            if (jahre.Length > 1)
            {

                start = start + jahre[0].Trim();
                end = end + jahre[1].Trim();

            }
            else
            {
                start = start + text;
                end = end + text;
            }
        }
    }


}
