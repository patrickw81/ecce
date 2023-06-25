namespace ecce
{
    partial class FormSetSegmentation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar_width = new System.Windows.Forms.TrackBar();
            this.trackBar_height = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.numericUpDownHght = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWdth = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHght)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWdth)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDown1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.trackBar_width);
            this.panel1.Controls.Add(this.trackBar_height);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Location = new System.Drawing.Point(20, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 248);
            this.panel1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(224, 166);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            12000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDownChange);
            this.numericUpDown1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NumericUpDownKeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Destroy small boxes by Size lower";
            // 
            // trackBar_width
            // 
            this.trackBar_width.Location = new System.Drawing.Point(92, 102);
            this.trackBar_width.Maximum = 100;
            this.trackBar_width.Name = "trackBar_width";
            this.trackBar_width.Size = new System.Drawing.Size(579, 45);
            this.trackBar_width.TabIndex = 3;
            this.trackBar_width.Value = 30;
            this.trackBar_width.Scroll += new System.EventHandler(this.TrackbarWidthScroll);
            // 
            // trackBar_height
            // 
            this.trackBar_height.Location = new System.Drawing.Point(92, 38);
            this.trackBar_height.Maximum = 100;
            this.trackBar_height.Name = "trackBar_height";
            this.trackBar_height.Size = new System.Drawing.Size(579, 45);
            this.trackBar_height.TabIndex = 2;
            this.trackBar_height.Value = 40;
            this.trackBar_height.Scroll += new System.EventHandler(this.TrackbarHeightScroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(25, 46);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(43, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Heigth";
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(598, 320);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(114, 30);
            this.close.TabIndex = 1;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // numericUpDownHght
            // 
            this.numericUpDownHght.Location = new System.Drawing.Point(723, 76);
            this.numericUpDownHght.Name = "numericUpDownHght";
            this.numericUpDownHght.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownHght.TabIndex = 2;
            this.numericUpDownHght.ValueChanged += new System.EventHandler(this.HghtChange);
            this.numericUpDownHght.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HghtKeyUp);
            // 
            // numericUpDownWdth
            // 
            this.numericUpDownWdth.Location = new System.Drawing.Point(723, 138);
            this.numericUpDownWdth.Name = "numericUpDownWdth";
            this.numericUpDownWdth.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownWdth.TabIndex = 3;
            this.numericUpDownWdth.ValueChanged += new System.EventHandler(this.WdhtChange);
            this.numericUpDownWdth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WdhtKeyUp);
            // 
            // FormSetSegmentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 385);
            this.Controls.Add(this.numericUpDownWdth);
            this.Controls.Add(this.numericUpDownHght);
            this.Controls.Add(this.close);
            this.Controls.Add(this.panel1);
            this.Name = "FormSetSegmentation";
            this.Text = "param_block";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHght)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWdth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private TrackBar trackBar_height;
        private Label label2;
        private Label Label1;
        private TrackBar trackBar_width;
        private Button close;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private NumericUpDown numericUpDownHght;
        private NumericUpDown numericUpDownWdth;
    }
}