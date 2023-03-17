namespace ecce
{
    partial class Param_v_h_destr
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
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Horizontal";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(100, 58);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBar1.RightToLeftLayout = true;
            this.trackBar1.Size = new System.Drawing.Size(717, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 200;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vertical";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(100, 109);
            this.trackBar2.Maximum = 200;
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBar2.RightToLeftLayout = true;
            this.trackBar2.Size = new System.Drawing.Size(717, 45);
            this.trackBar2.TabIndex = 4;
            this.trackBar2.Value = 200;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(682, 171);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(126, 35);
            this.close.TabIndex = 5;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(552, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Param_table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 250);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Name = "Param_table";
            this.Text = "Param Line Dedection";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TrackBar trackBar1;
        private Label label2;
        private TrackBar trackBar2;
        private Button close;
        private Button button1;
    }
}