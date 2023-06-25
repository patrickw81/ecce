using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ecce
{
    internal partial class FormSetBinarize : Form
    {
        private Form1 ClassForm { get; set; }
        private ClassImage MyImage { get; set; }

        public FormSetBinarize(Form1 form, ClassImage img_cls)
        {
            InitializeComponent();
            MyImage = img_cls;
            ClassForm = form;
        }
        private void TrackbarScroll(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                numericUpDown1.Value = trackBar1.Value;
                RunBin();
            }
        }
        private void BtnApplyClick(object sender, EventArgs e)
        {
            MyImage.CheckBinarized = true;
            MyImage.ParamBinarized = trackBar1.Value;
            //MyImage.ImgProcess = MyImage.ImgBinarized.Convert<Bgr, Byte>().Clone();
            this.Close();
        }
        private void BtnCloseClick(object sender, EventArgs e)
        {
            MyImage.CheckBinarized = false;
            //MyImage.CheckStatusBinarize();
            ClassForm.RefreshPicture(MyImage.ImgProcess);
            this.Close();
        }

        private void NumericValueChange(object sender, EventArgs e)
        {
            
            trackBar1.Value= (int)numericUpDown1.Value;
            RunBin();
        }

        private void NumericValueKeyUp(object sender, KeyEventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            RunBin();
        }

        private void RunBin()
        {
            MyImage.GetBinarizePerParam((int)numericUpDown1.Value);
            ClassForm.toolStripParamBin.Text = trackBar1.Value.ToString();
            ClassForm.RefreshPicture(MyImage.ImgBinarized.Convert<Bgr, Byte>());
        }
    }
}
