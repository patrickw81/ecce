using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ecce
{
    internal partial class FormSetSegmentation : Form
    {
        private Form1 ClassForm { get; set; }
        private ClassImage MyImage { get; set; }
        public FormSetSegmentation(Form1 form, ClassImage img_cls, int height, int width, int destruct=1000)
        {
            InitializeComponent();
            trackBar_height.Value = height;
            trackBar_width.Value = width;
            numericUpDown1.Value = destruct;
            ClassForm = form;
            MyImage = img_cls;
        }
     
        private void TrackbarHeightScroll(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                numericUpDownHght.Value = trackBar_height.Value;
                RunSeg();
            }
        }

        private void TrackbarWidthScroll(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                numericUpDownWdth.Value = trackBar_width.Value;
                RunSeg();
            }
        }

        private void NumericUpDownChange(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                RunSeg();
            }
        }

        private void NumericUpDownKeyUp(object sender, KeyEventArgs e)
        {
            if (ClassForm != null)
            {
                RunSeg();
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HghtChange(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                trackBar_height.Value= (int)numericUpDownHght.Value ;
                RunSeg();
            }
        }

        private void WdhtChange(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                trackBar_width.Value=(int) numericUpDownWdth.Value;
                RunSeg();
            }
        }
        private void RunSeg()
        {
            ClassForm.RefreshPicture(MyImage.SetSegmentation(trackBar_width.Value, trackBar_height.Value, (int)numericUpDown1.Value));
            ClassForm.RefreshSegmenationInfoBox((int)trackBar_height.Value, (int)trackBar_width.Value, (int)numericUpDown1.Value);
        }

        private void HghtKeyUp(object sender, KeyEventArgs e)
        {
            trackBar_height.Value = (int)numericUpDownHght.Value;
            RunSeg();
        }

        private void WdhtKeyUp(object sender, KeyEventArgs e)
        {
            trackBar_width.Value = (int)numericUpDownWdth.Value;
            RunSeg();
        }
    }
}
