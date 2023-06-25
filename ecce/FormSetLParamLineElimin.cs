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

namespace ecce
{
    internal partial class FormSetLParamLineElimin : Form
    {
        private Form1 ClassForm { get; set; }
        private ClassImage MyImage { get; set; }
        private Image<Gray, Byte>? ImgGraywork { get; set; }
        public FormSetLParamLineElimin(Form1 form, ClassImage imgcls)
        {
            InitializeComponent();
            MyImage = imgcls;
            ClassForm = form;
        }

        private void TrachbarHorizontalScroll(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                numericH.Value=trackBarH.Value;
                RunElimination();
            }
        }

        private void TrackBarVerticalScroll(object sender, EventArgs e)
        {
            if (ClassForm != null)
            {
                numericV.Value=trackBarV.Value;
                RunElimination();
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            ClassForm.RefreshPicture(MyImage.ImgBinarized.Convert<Bgr, Byte>());
            this.Close();
        }

        private void ButtonApplyCLick(object sender, EventArgs e)
        {
            if (ImgGraywork != null)
            {
                MyImage.ImgBinarized = ImgGraywork;
                MyImage.ImgProcess = MyImage.ImgBinarized.Convert<Bgr, Byte>().Copy();
            }
            this.Close();
        }

        private void RunElimination()
        {
            ImgGraywork = MyImage.RemoveLine(trackBarH.Value, trackBarV.Value);
            ClassForm.RefreshPicture(ImgGraywork.Convert<Bgr, Byte>());
        }

        private void numericHChange(object sender, EventArgs e)
        {
            trackBarH.Value=(int)numericH.Value;
            RunElimination();
        }

        private void numericHKeyUp(object sender, KeyEventArgs e)
        {
            trackBarH.Value = (int)numericH.Value;
            RunElimination();
        }

        private void numericVChange(object sender, EventArgs e)
        {
            trackBarV.Value = (int)numericV.Value;
            RunElimination();
        }

        private void numericVKeyUp(object sender, KeyEventArgs e)
        {
            trackBarV.Value = (int)numericV.Value;
            RunElimination();
        }
    }
}
