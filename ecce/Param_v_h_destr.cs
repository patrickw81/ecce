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
    public partial class Param_v_h_destr : Form
    {
        Form1 form;
        Image_manip myimgcls;

        public Param_v_h_destr(Form1 form, Image_manip img_cls)
        {
            InitializeComponent();
            myimgcls=img_cls;
            this.form = form;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (form != null)
            {
                myimgcls.tablededection(trackBar1.Value, trackBar2.Value);
                form.refreshpicturebox();
                //form.dummyLineRmove(trackBar1.Value,trackBar2.Value);
         

            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            myimgcls.image_handling(myimgcls.imgBinarized.Convert<Bgr, Byte>());
            form.refreshpicturebox();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myimgcls.imgBinarized = myimgcls.imgGraywork;
            this.Close();
        }
    }
}
