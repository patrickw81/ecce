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
    public partial class Param_size : Form
    {
        Image_manip image_clas;
        decimal width;
        decimal height;
        Form1 myform;

        public Param_size(Form1 form, Image_manip image_clas, int _width, int _height)
        {
            InitializeComponent();
            Debug.WriteLine(_width +"  "+ _height);
            numericUpWidth.Value = _width;
            numericHeight.Value = _height;
            width= _width;
            height= _height;

            myform = form;

            this.image_clas = image_clas;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void chg_width()
        {
            decimal x = numericUpWidth.Value / width * numericHeight.Value;
            if (x < 10000)
            {
                numericHeight.Value = x;
            }
            else
            {
                MessageBox.Show("Zu groß");
                numericUpWidth.Value = width;
                numericHeight.Value = height;
            }

            if (numericHeight.Value < 1) { numericHeight.Value = 1; }
            width = numericUpWidth.Value;
            height = numericHeight.Value;
        }

        private void numericHeight_Leave(object sender, EventArgs e)
        {
            chg_height();
        }

        private void numericHeight_Leave(object sender, MouseEventArgs e)
        {
            chg_height();
        }

        private void chg_height()
        {
            decimal x = numericHeight.Value / height * numericUpWidth.Value;
            if (x < 10000)
            {
                this.numericUpWidth.Value = x;
            }
            else
            {
                MessageBox.Show("Zu groß");
                numericUpWidth.Value = width;
                numericHeight.Value = height;

            }
            if (numericUpWidth.Value < 1) { numericUpWidth.Value = 1; }
            width = numericUpWidth.Value;
            height = numericHeight.Value;
           
        }

        private void numericUpWidth_Leave(object sender, EventArgs e)
        {
            chg_width();
        }

        private void numericUpWidth_MouseUp(object sender, MouseEventArgs e)
        {
            chg_width();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            image_clas.resize_img((int)numericUpWidth.Value,(int)numericHeight.Value);
            myform.refreshpicturebox();
            this.Close();
        }
    }
}
