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
    public partial class Param_Binar : Form
    {
        Form1 form;
        Image_manip myimage;

        public Param_Binar(Form1 form, Image_manip img_cls)
        {
            InitializeComponent();
            myimage = img_cls;
            
            this.form = form;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (form != null)
            {

                myimage.binarizeConver(trackBar1.Value);
                form.refreshpicturebox(myimage.imgBinarized.Convert<Bgr, Byte>());

            }
        }

        private void close_Click(object sender, EventArgs e)
        {

            myimage.isbinarised = false;
            myimage.checkbinarized();
            form.refreshpicturebox();
            this.Close();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            myimage.isbinarised = true;
            this.Close();


        }
    }
}
