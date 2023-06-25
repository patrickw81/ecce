namespace ecce
{
    partial class FormSetSize
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
            this.numericHeight = new System.Windows.Forms.NumericUpDown();
            this.numericUpWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // numericHeight
            // 
            this.numericHeight.Location = new System.Drawing.Point(101, 54);
            this.numericHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.Name = "numericHeight";
            this.numericHeight.Size = new System.Drawing.Size(131, 23);
            this.numericHeight.TabIndex = 0;
            this.numericHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericHeight.Leave += new System.EventHandler(this.NumericHeight_Leave);
            this.numericHeight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.numericHeight_MouseUp);
            // 
            // numericUpWidth
            // 
            this.numericUpWidth.Location = new System.Drawing.Point(101, 108);
            this.numericUpWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpWidth.Name = "numericUpWidth";
            this.numericUpWidth.Size = new System.Drawing.Size(131, 23);
            this.numericUpWidth.TabIndex = 1;
            this.numericUpWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpWidth.Leave += new System.EventHandler(this.NumericUpWidth_Leave);
            this.numericUpWidth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NumericUpWidth_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Height:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width:";
            // 
            // btn_apply
            // 
            this.btn_apply.Location = new System.Drawing.Point(51, 193);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(100, 28);
            this.btn_apply.TabIndex = 4;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.BtnApllyClick);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(197, 193);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(100, 28);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // FormSetSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 281);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpWidth);
            this.Controls.Add(this.numericHeight);
            this.Name = "FormSetSize";
            this.Text = "Parameter Größe";
            ((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown numericHeight;
        private NumericUpDown numericUpWidth;
        private Label label1;
        private Label label2;
        private Button btn_apply;
        private Button btn_close;
    }
}