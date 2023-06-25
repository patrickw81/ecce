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
    internal class ClassCatalogueArchiveCol
    {
        public string IdFile { get; set; } = "";
        public string TitleFile { get; set; } = "";
        public string Contains { get; set; } = "";
        public string Inserted { get; set; } = "";
        public string Lifespan { get; set; } = "";
        public string StartYear { get; set; } = "";
        public string EndYear { get; set; } = "";
        public string Remarks { get; set; } = "";
        public string FormerId { get; set; } = "";
        public bool Ditto { get; set; } = false;
        
        public void Analysetxt(int startpoint, int leftpoint, string text, string lasttitel)
        {
            Regex yearregex = new (@"\d{4}");
            Regex containsregex = new (@"^[a-z]{1,2}\)|^[a-z]{1,2}\.|Enthält");
            Regex ditoregex = new (@"Desgl|Desg1|Desgleichen|Desg1eichen");
            Regex insertedregex = new (@"Darin");

            string[] line = text.Split('\n');
            for (int i = 0; i < line.Length; i++)
            {
                line[i] = line[i].Replace("\r", "");
                line[i] = line[i].Replace("\n", "");
            }
            if (line[^1] == "")
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

                        IdFile += element[0];
                        if (ditoregex.IsMatch(element[1]))
                        {
                            flag = 1;
                            Ditto = true;
                            CheckDitto(element[1], lasttitel);

                        }
                        else
                        {
                            SetTitle(element[1]);
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

                if (ditoregex.IsMatch(element[0]))
                {
                    flag = 1;
                    Ditto = true;
                }
                else if (insertedregex.IsMatch(element[0]))
                {
                    flag = 2;
                }
                else if (yearregex.IsMatch(element[0]))
                {
                    flag = 3;


                }
                else if (containsregex.IsMatch(element[0]))
                {
                    flag = 4;
                }
                switch (flag)
                {
                    case 0:
                        SetTitle(line[i]);
                        break;
                    case 1: //Desgl
                        CheckDitto(line[i], lasttitel);
                        flag = 0;
                        break;
                    case 2:
                        SetInserted(line[i]);
                        break;
                    case 3:

                        SetLifespan(line[i]);
                        SetYears(line[i]);
                        break;
                    case 4:
                        SetContains(line[i]);
                        break;
                    case 5:
                        SetRemarks(line[i]);
                        break;
                    case 6:
                        SetFormerId(line[i]);
                        break;
                }
            }
        }
        private void CheckDitto(string text, string lasttitel)
        {

            string pattern = "(Desgleichen)|(Desg1eichen)|(Desg1.)|(Desgl.)";

            string[] element = Regex.Split(text.Trim(), pattern);
            if (element[0] == "")
            {
                element = element.Skip(1).ToArray();
            }

            if (element.Length > 0)
            {
                TitleFile = TitleFile + lasttitel.Trim() + " " + element[^1];
                return;
            }
            else
            {

                TitleFile += lasttitel.Trim();
            }
            return;
        }
        private void SetInserted(string text)
        {

            Inserted = Inserted.Trim() + " " + text.Trim();
        }
        private void SetLifespan(string text)
        {
            Lifespan += text;
        }
        private void SetTitle(string text)
        {
            TitleFile = TitleFile.Trim() + " " + text.Trim();
        }
        private void SetContains(string text)
        {
            Contains = Contains.Trim() + " " + text.Trim();
        }
        private void SetRemarks(string text)
        {
            Remarks = Remarks.Trim() + " " + text.Trim();
        }
        private void SetFormerId(string text)
        {
            FormerId = FormerId.Trim() + " " + text.Trim();
        }
        public void SetYears(string text)
        {
            string[] jahre = text.Split("-");

            if (jahre.Length > 1)
            {

                StartYear += jahre[0].Trim();
                EndYear +=jahre[1].Trim();

            }
            else
            {
                StartYear += text;
                EndYear += text;
            }
        }
    }
}
