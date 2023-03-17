using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ecce
{
    internal class Result_text_only
    {
        public string form_text { get; set; } = "";
        public string text { get; set; } = "";
        public string summary { get; set; } = "";
        public string person { get; set; } = "";
        public string place { get; set; } = "";
        public string time { get; set; } = "";
        public string title { get; set; } = "";
        public string classifikation { get; set; } = "";


        public void combine_txt()
        {

            text = form_text.Replace("-\r\n", "");
            text = text.Replace("\r\n", " ");
          
        }
        public TableLayoutPanel creat_table(int width=1000, int heigth = 1000) {
            TableLayoutPanel mytable = new TableLayoutPanel();
            mytable.ColumnCount = 2;
            mytable.RowCount = 8;
            mytable.Size = new System.Drawing.Size(width, heigth);
            mytable.Location = new System.Drawing.Point(1, 50);
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
            mytable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
            TextBox[] my_txt = new TextBox[16];
            for (int i = 0; i < 8; i++)
            {
                my_txt[i] = new TextBox();
                my_txt[i].Dock = DockStyle.Fill;
                my_txt[i].ReadOnly = true;
                my_txt[i].Multiline = true;
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
                my_txt[i] = new TextBox();
                my_txt[i].Multiline= true;
                my_txt[i].Dock = DockStyle.Fill;
                my_txt[i].ScrollBars= ScrollBars.Vertical;
                mytable.Controls.Add(my_txt[i], 1, x);
                x++;
            }
            my_txt[8].Text = form_text;
            my_txt[9].Text = text;
            my_txt[10].Text = summary;
            my_txt[11].Text = title;
            my_txt[12].Text = time;
            my_txt[13].Text = place;
            my_txt[14].Text = person;
            my_txt[15].Text = classifikation;

            return mytable;
        }




    }
}
