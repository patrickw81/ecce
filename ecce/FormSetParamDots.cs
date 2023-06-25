using Emgu.CV;
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
using System.Diagnostics;

namespace ecce
{
    internal partial class FormSetParamDots : Form
    {
        private Form1 ClassForm { get; set; }
        private ClassImage MyImage { get; set; }
        private Image<Gray, Byte> ImgGraywork { get; set; }

        public FormSetParamDots(Form1 form, ClassImage img_cls)
        {
            InitializeComponent();
            MyImage = img_cls;
            ClassForm = form;
            ImgGraywork=MyImage.ImgBinarized.Copy();
        }

        private void TrackScroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = (int)trackBar1.Value;
            RunRemoveBlackDots();
        }

        private void ApplyClick(object sender, EventArgs e)
        {
            if (ImgGraywork != null) {
                MyImage.ParamBlackDotsDestruction = (int)trackBar1.Value;
                ClassForm.SetTxtBoxBlackDots();
                MyImage.ImgBinarized = ImgGraywork.Copy();                
            }
            this.Close();
        }

        private void CloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetClick(object sender, EventArgs e)
        {
            ImgGraywork = MyImage.ImgBinarized.Copy();
            ClassForm.RefreshPicture(MyImage.ImgBinarized.Convert<Bgr, byte>());
            trackBar1.Value = 1;
            numericUpDown1.Value = 1;
        }

        private void NumericValueChange(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            RunRemoveBlackDots();
        }

        private void NumericKeyUp(object sender, KeyEventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
            RunRemoveBlackDots();
        }

        private void RunRemoveBlackDots()
        {
            ImgGraywork = MyImage.RemoveBlackDots(ImgGraywork, (int)trackBar1.Value);
            ClassForm.RefreshPicture(ImgGraywork.Convert<Bgr, byte>());
        }

    }
}
