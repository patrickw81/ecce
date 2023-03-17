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
    public partial class Creat_Tag : Form
    {
        
        Form1 form;
        List<string> tagnames = new List<string>();


        public Creat_Tag(Form1 form, List<string>_tagnames)
        {
            InitializeComponent();
            this.form = form;
            tagnames = _tagnames;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if(tagnames.Count > 0 ) { Debug.WriteLine(tagnames[0]); }
            

            if (tagnames.Contains(textBox1.Text.ToString())){
                MessageBox.Show("Tagname already exist");
            }
            else {
                form.tag_area(textBox1.Text);
                this.Close();
            }
        }
    }
}
