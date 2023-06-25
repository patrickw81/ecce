using System;
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
    internal partial class FormSetSize : Form
    {
        private ClassImage ImageClass { get; set; }
        private decimal WidthValue { get; set; }
        private decimal HeightValue { get; set; }
        private Form1 BasicForm{get;set;}

        public FormSetSize(Form1 myForm, ClassImage image_clas)
        {
            InitializeComponent();
            WidthValue = image_clas.ActualWidth;
            HeightValue = image_clas.ActualHeight;
            numericUpWidth.Value = WidthValue;
            numericHeight.Value = HeightValue;
            BasicForm = myForm;
            this.ImageClass = image_clas;
        }

        private void SetInfoWidth()
        {
            decimal x = numericUpWidth.Value / WidthValue * numericHeight.Value;
            if (x < 10000)
            {
                numericHeight.Value = x;
            }
            else
            {
                MessageBox.Show("Zu groß");
                numericUpWidth.Value = WidthValue;
                numericHeight.Value = HeightValue;
            }
            if (numericHeight.Value < 1) { numericHeight.Value = 1; }
            WidthValue = numericUpWidth.Value;
            HeightValue = numericHeight.Value;
        }
        private void SetInfoHeight()
        {
            decimal x = numericUpWidth.Value / HeightValue * numericHeight.Value;
            if (x < 10000)
            {
                numericUpWidth.Value = x;
            }
            else
            {
                MessageBox.Show("Zu groß");
                numericUpWidth.Value = WidthValue;
                numericHeight.Value = HeightValue;
            }
            if (numericHeight.Value < 1) { numericHeight.Value = 1; }
            WidthValue = numericUpWidth.Value;
            HeightValue = numericHeight.Value;
        }

        private void NumericHeight_Leave(object sender, EventArgs e)
        {
            SetInfoHeight();
        }

    
        private void NumericUpWidth_Leave(object sender, EventArgs e)
        {
            SetInfoWidth();
        }

        private void NumericUpWidth_MouseUp(object sender, MouseEventArgs e)
        {
            SetInfoWidth();
        }

        private void BtnApllyClick(object sender, EventArgs e)
        {
            BasicForm.RefreshPicture(ImageClass.GetNewSize((int)numericUpWidth.Value, (int)numericHeight.Value));
            BasicForm.SetTxtBoxImgSize();
            this.Close();
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericHeight_MouseUp(object sender, MouseEventArgs e)
        {
            SetInfoHeight();
        }
    }
}
