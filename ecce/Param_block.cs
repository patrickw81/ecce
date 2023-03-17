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
    public partial class Param_block : Form
    {
        Form1 form;
        public Param_block(Form1 form, int height, int width, int destruct=1000)
        {
            InitializeComponent();
            trackBar_height.Value = height;
            trackBar_width.Value = width;
            numericUpDown1.Value = destruct;
            this.form = form;
        }

        private void setParamLength(object sender, EventArgs e)
        {
            if (form != null)
            {
                form.getblock(trackBar_height.Value, trackBar_width.Value,(int)numericUpDown1.Value);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setParamLength_2(object sender, KeyEventArgs e)
        {
            if (form != null)
            {
                form.getblock(trackBar_height.Value, trackBar_width.Value, (int)numericUpDown1.Value);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (form != null)
            {
                form.getblock(trackBar_height.Value, trackBar_width.Value, (int)numericUpDown1.Value);
            }
        }
    }
}
