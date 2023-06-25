namespace ecce
{
    partial class FormSetLParamLineElimin
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
            this.trackBarH = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarV = new System.Windows.Forms.TrackBar();
            this.close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numericH = new System.Windows.Forms.NumericUpDown();
            this.numericV = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericV)).BeginInit();
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
            // trackBarH
            // 
            this.trackBarH.Location = new System.Drawing.Point(100, 58);
            this.trackBarH.Maximum = 200;
            this.trackBarH.Minimum = 1;
            this.trackBarH.Name = "trackBarH";
            this.trackBarH.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBarH.RightToLeftLayout = true;
            this.trackBarH.Size = new System.Drawing.Size(717, 45);
            this.trackBarH.TabIndex = 1;
            this.trackBarH.Value = 200;
            this.trackBarH.Scroll += new System.EventHandler(this.TrachbarHorizontalScroll);
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
            // trackBarV
            // 
            this.trackBarV.Location = new System.Drawing.Point(100, 109);
            this.trackBarV.Maximum = 200;
            this.trackBarV.Minimum = 1;
            this.trackBarV.Name = "trackBarV";
            this.trackBarV.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.trackBarV.RightToLeftLayout = true;
            this.trackBarV.Size = new System.Drawing.Size(717, 45);
            this.trackBarV.TabIndex = 4;
            this.trackBarV.Value = 200;
            this.trackBarV.Scroll += new System.EventHandler(this.TrackBarVerticalScroll);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(682, 171);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(126, 35);
            this.close.TabIndex = 5;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(552, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonApplyCLick);
            // 
            // numericH
            // 
            this.numericH.Location = new System.Drawing.Point(852, 60);
            this.numericH.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericH.Name = "numericH";
            this.numericH.Size = new System.Drawing.Size(120, 23);
            this.numericH.TabIndex = 7;
            this.numericH.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericH.ValueChanged += new System.EventHandler(this.numericHChange);
            this.numericH.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericHKeyUp);
            // 
            // numericV
            // 
            this.numericV.Location = new System.Drawing.Point(852, 120);
            this.numericV.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericV.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericV.Name = "numericV";
            this.numericV.Size = new System.Drawing.Size(120, 23);
            this.numericV.TabIndex = 8;
            this.numericV.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericV.ValueChanged += new System.EventHandler(this.numericVChange);
            this.numericV.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericVKeyUp);
            // 
            // FormSetLParamLineElimin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 260);
            this.Controls.Add(this.numericV);
            this.Controls.Add(this.numericH);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.trackBarV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarH);
            this.Controls.Add(this.label1);
            this.Name = "FormSetLParamLineElimin";
            this.Text = "Param Line Dedection";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TrackBar trackBarH;
        private Label label2;
        private TrackBar trackBarV;
        private Button close;
        private Button button1;
        private NumericUpDown numericH;
        private NumericUpDown numericV;
    }
}