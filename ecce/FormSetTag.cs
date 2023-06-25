using Emgu.CV;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ecce
{
    public partial class FormSetTag : Form
    {
        
        private Form1 TempForm { get; set; }
        private List<string> LstTagNames { get; set; } = new List<string>();


    public FormSetTag(Form1 form, List<string>_tagnames)
        {
            InitializeComponent();
            TempForm = form;
            LstTagNames = _tagnames;
        }
       
        private void BtnApplyClick(object sender, EventArgs e)
        {
            char c= textBox1.Text.ToString()[0];
            if (textBox1.Text.ToString() == "")
            {
                MessageBox.Show("Tagname must contain letters");
            }
            if (LstTagNames.Contains(textBox1.Text.ToString())){
                MessageBox.Show("Tagname already exist");
            }
            else if (char.IsDigit(c)) {
                MessageBox.Show("First postision must be char");
            }
            else {
                TempForm.SetTagName(textBox1.Text);
                this.Close();
            }
        }
    }
}
