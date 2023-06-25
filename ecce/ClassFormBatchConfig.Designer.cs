namespace ecce
{
    partial class ClassFormBatchConfig
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Resize_box = new System.Windows.Forms.CheckBox();
            this.Noise_box = new System.Windows.Forms.CheckBox();
            this.Sharpen_box = new System.Windows.Forms.CheckBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.Num_interva = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboBox_bina = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Box_skew = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Box_Lineelimi = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Box_Linestrength = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Box_Segment = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Box_Sorting = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Box_areas = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Box_Ocrmod = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Num_paramBin = new System.Windows.Forms.NumericUpDown();
            this.Num_param_line_eli_w = new System.Windows.Forms.NumericUpDown();
            this.Num_param_line_eli_h = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.Num_param_seg_destr = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Num_param_seg_w = new System.Windows.Forms.NumericUpDown();
            this.Num_param_seg_h = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Num_resize_fac = new System.Windows.Forms.NumericUpDown();
            this.Box_Frame_rmv = new System.Windows.Forms.CheckBox();
            this.tesss_mdl_b = new System.Windows.Forms.ComboBox();
            this.pagmode_b = new System.Windows.Forms.ComboBox();
            this.combo_enginemod_b = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.Num_start = new System.Windows.Forms.NumericUpDown();
            this.CkeckBoxBlackDots = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.NumParamBlackDots = new System.Windows.Forms.NumericUpDown();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.LoadSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Num_interva)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_paramBin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_line_eli_w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_line_eli_h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_destr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_resize_fac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumParamBlackDots)).BeginInit();
            this.SuspendLayout();
            // 
            // Resize_box
            // 
            this.Resize_box.AutoSize = true;
            this.Resize_box.Location = new System.Drawing.Point(112, 174);
            this.Resize_box.Name = "Resize_box";
            this.Resize_box.Size = new System.Drawing.Size(100, 19);
            this.Resize_box.TabIndex = 1;
            this.Resize_box.Text = "Resize   Factor";
            this.Resize_box.UseVisualStyleBackColor = true;
            this.Resize_box.CheckedChanged += new System.EventHandler(this.SetResize);
            // 
            // Noise_box
            // 
            this.Noise_box.AutoSize = true;
            this.Noise_box.Location = new System.Drawing.Point(112, 204);
            this.Noise_box.Name = "Noise_box";
            this.Noise_box.Size = new System.Drawing.Size(98, 19);
            this.Noise_box.TabIndex = 2;
            this.Noise_box.Text = "Reduce Noise";
            this.Noise_box.UseVisualStyleBackColor = true;
            this.Noise_box.CheckedChanged += new System.EventHandler(this.SetNoiseReduction);
            // 
            // Sharpen_box
            // 
            this.Sharpen_box.AutoSize = true;
            this.Sharpen_box.Location = new System.Drawing.Point(112, 234);
            this.Sharpen_box.Name = "Sharpen_box";
            this.Sharpen_box.Size = new System.Drawing.Size(69, 19);
            this.Sharpen_box.TabIndex = 3;
            this.Sharpen_box.Text = "Sharpen";
            this.Sharpen_box.UseVisualStyleBackColor = true;
            this.Sharpen_box.CheckedChanged += new System.EventHandler(this.SetSharpen);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(109, 886);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // Num_interva
            // 
            this.Num_interva.Location = new System.Drawing.Point(301, 129);
            this.Num_interva.Name = "Num_interva";
            this.Num_interva.Size = new System.Drawing.Size(69, 23);
            this.Num_interva.TabIndex = 5;
            this.Num_interva.ValueChanged += new System.EventHandler(this.SetIntervall);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Intervall";
            // 
            // ComboBox_bina
            // 
            this.ComboBox_bina.FormattingEnabled = true;
            this.ComboBox_bina.Items.AddRange(new object[] {
            "Otsu",
            "Adopted",
            "With Parameters"});
            this.ComboBox_bina.Location = new System.Drawing.Point(214, 269);
            this.ComboBox_bina.Name = "ComboBox_bina";
            this.ComboBox_bina.Size = new System.Drawing.Size(121, 23);
            this.ComboBox_bina.TabIndex = 7;
            this.ComboBox_bina.Text = "Otsu";
            this.ComboBox_bina.SelectedIndexChanged += new System.EventHandler(this.SetBinarizeMode);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Binarize";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Skew";
            // 
            // Box_skew
            // 
            this.Box_skew.FormattingEnabled = true;
            this.Box_skew.Items.AddRange(new object[] {
            "",
            "Black Frame",
            "No Frame"});
            this.Box_skew.Location = new System.Drawing.Point(214, 314);
            this.Box_skew.Name = "Box_skew";
            this.Box_skew.Size = new System.Drawing.Size(121, 23);
            this.Box_skew.TabIndex = 10;
            this.Box_skew.SelectedIndexChanged += new System.EventHandler(this.SetDeskew);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 440);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Line Elimination";
            // 
            // Box_Lineelimi
            // 
            this.Box_Lineelimi.FormattingEnabled = true;
            this.Box_Lineelimi.Items.AddRange(new object[] {
            "",
            "Auto",
            "Parameters"});
            this.Box_Lineelimi.Location = new System.Drawing.Point(214, 439);
            this.Box_Lineelimi.Name = "Box_Lineelimi";
            this.Box_Lineelimi.Size = new System.Drawing.Size(121, 23);
            this.Box_Lineelimi.TabIndex = 12;
            this.Box_Lineelimi.SelectedIndexChanged += new System.EventHandler(this.SetLineElimination);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(109, 516);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Line Weight";
            // 
            // Box_Linestrength
            // 
            this.Box_Linestrength.FormattingEnabled = true;
            this.Box_Linestrength.Items.AddRange(new object[] {
            "",
            "Strong",
            "Weak"});
            this.Box_Linestrength.Location = new System.Drawing.Point(214, 511);
            this.Box_Linestrength.Name = "Box_Linestrength";
            this.Box_Linestrength.Size = new System.Drawing.Size(121, 23);
            this.Box_Linestrength.TabIndex = 14;
            this.Box_Linestrength.SelectedIndexChanged += new System.EventHandler(this.SetLineMode);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 556);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Segmentation";
            // 
            // Box_Segment
            // 
            this.Box_Segment.FormattingEnabled = true;
            this.Box_Segment.Items.AddRange(new object[] {
            "",
            "Line Auto",
            "Block Auto",
            "Parameter",
            "Tessseract_Word",
            "Tessseract_Line",
            "Tessseract_Para",
            "Tessseract_Block"});
            this.Box_Segment.Location = new System.Drawing.Point(214, 551);
            this.Box_Segment.Name = "Box_Segment";
            this.Box_Segment.Size = new System.Drawing.Size(121, 23);
            this.Box_Segment.TabIndex = 16;
            this.Box_Segment.SelectedIndexChanged += new System.EventHandler(this.SetSegmentationMode);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(109, 699);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "Sorting";
            // 
            // Box_Sorting
            // 
            this.Box_Sorting.FormattingEnabled = true;
            this.Box_Sorting.Items.AddRange(new object[] {
            "",
            "X then Y",
            "Y",
            "Left_2_Right"});
            this.Box_Sorting.Location = new System.Drawing.Point(214, 694);
            this.Box_Sorting.Name = "Box_Sorting";
            this.Box_Sorting.Size = new System.Drawing.Size(121, 23);
            this.Box_Sorting.TabIndex = 18;
            this.Box_Sorting.SelectedIndexChanged += new System.EventHandler(this.SetSortMode);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(109, 655);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Load Areas";
            // 
            // Box_areas
            // 
            this.Box_areas.FormattingEnabled = true;
            this.Box_areas.Items.AddRange(new object[] {
            ""});
            this.Box_areas.Location = new System.Drawing.Point(214, 651);
            this.Box_areas.Name = "Box_areas";
            this.Box_areas.Size = new System.Drawing.Size(121, 23);
            this.Box_areas.TabIndex = 20;
            this.Box_areas.SelectedIndexChanged += new System.EventHandler(this.GetComboBoxSelectAreas);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(109, 742);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "OCR Mod";
            // 
            // Box_Ocrmod
            // 
            this.Box_Ocrmod.FormattingEnabled = true;
            this.Box_Ocrmod.Items.AddRange(new object[] {
            "OCR_only",
            "Segmented",
            "Archive_Cat_Temp_A",
            "Areas_SoftCut",
            "Areas_HardCut"});
            this.Box_Ocrmod.Location = new System.Drawing.Point(214, 739);
            this.Box_Ocrmod.Name = "Box_Ocrmod";
            this.Box_Ocrmod.Size = new System.Drawing.Size(121, 23);
            this.Box_Ocrmod.TabIndex = 22;
            this.Box_Ocrmod.SelectedIndexChanged += new System.EventHandler(this.SetOcrMode);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(346, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Parameter";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(346, 440);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 15);
            this.label11.TabIndex = 24;
            this.label11.Text = "Parameter Width";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(346, 471);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 15);
            this.label12.TabIndex = 25;
            this.label12.Text = "Parameter Height";
            // 
            // Num_paramBin
            // 
            this.Num_paramBin.Location = new System.Drawing.Point(449, 270);
            this.Num_paramBin.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_paramBin.Name = "Num_paramBin";
            this.Num_paramBin.ReadOnly = true;
            this.Num_paramBin.Size = new System.Drawing.Size(120, 23);
            this.Num_paramBin.TabIndex = 26;
            this.Num_paramBin.ValueChanged += new System.EventHandler(this.Num_paramBinChange);
            // 
            // Num_param_line_eli_w
            // 
            this.Num_param_line_eli_w.Location = new System.Drawing.Point(449, 439);
            this.Num_param_line_eli_w.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Num_param_line_eli_w.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_line_eli_w.Name = "Num_param_line_eli_w";
            this.Num_param_line_eli_w.ReadOnly = true;
            this.Num_param_line_eli_w.Size = new System.Drawing.Size(120, 23);
            this.Num_param_line_eli_w.TabIndex = 27;
            this.Num_param_line_eli_w.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_line_eli_w.ValueChanged += new System.EventHandler(this.Num_param_line_eli_w_ValueChanged);
            // 
            // Num_param_line_eli_h
            // 
            this.Num_param_line_eli_h.Location = new System.Drawing.Point(449, 470);
            this.Num_param_line_eli_h.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.Num_param_line_eli_h.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_line_eli_h.Name = "Num_param_line_eli_h";
            this.Num_param_line_eli_h.ReadOnly = true;
            this.Num_param_line_eli_h.Size = new System.Drawing.Size(120, 23);
            this.Num_param_line_eli_h.TabIndex = 28;
            this.Num_param_line_eli_h.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_line_eli_h.ValueChanged += new System.EventHandler(this.Num_param_line_eli_h_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(346, 617);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 15);
            this.label13.TabIndex = 29;
            this.label13.Text = "Parameter Destr";
            // 
            // Num_param_seg_destr
            // 
            this.Num_param_seg_destr.Location = new System.Drawing.Point(449, 615);
            this.Num_param_seg_destr.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_param_seg_destr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_destr.Name = "Num_param_seg_destr";
            this.Num_param_seg_destr.ReadOnly = true;
            this.Num_param_seg_destr.Size = new System.Drawing.Size(120, 23);
            this.Num_param_seg_destr.TabIndex = 30;
            this.Num_param_seg_destr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_destr.ValueChanged += new System.EventHandler(this.Num_param_seg_destr_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(346, 586);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Parameter Height";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(346, 554);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 15);
            this.label15.TabIndex = 32;
            this.label15.Text = "Parameter Width";
            // 
            // Num_param_seg_w
            // 
            this.Num_param_seg_w.Location = new System.Drawing.Point(449, 548);
            this.Num_param_seg_w.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.Num_param_seg_w.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_w.Name = "Num_param_seg_w";
            this.Num_param_seg_w.ReadOnly = true;
            this.Num_param_seg_w.Size = new System.Drawing.Size(120, 23);
            this.Num_param_seg_w.TabIndex = 33;
            this.Num_param_seg_w.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_w.ValueChanged += new System.EventHandler(this.Num_param_seg_w_ValueChanged);
            // 
            // Num_param_seg_h
            // 
            this.Num_param_seg_h.Location = new System.Drawing.Point(449, 581);
            this.Num_param_seg_h.Maximum = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.Num_param_seg_h.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_h.Name = "Num_param_seg_h";
            this.Num_param_seg_h.ReadOnly = true;
            this.Num_param_seg_h.Size = new System.Drawing.Size(120, 23);
            this.Num_param_seg_h.TabIndex = 34;
            this.Num_param_seg_h.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_param_seg_h.ValueChanged += new System.EventHandler(this.Num_param_seg_h_ValueChanged);
            // 
            // Num_resize_fac
            // 
            this.Num_resize_fac.Location = new System.Drawing.Point(214, 172);
            this.Num_resize_fac.Name = "Num_resize_fac";
            this.Num_resize_fac.ReadOnly = true;
            this.Num_resize_fac.Size = new System.Drawing.Size(120, 23);
            this.Num_resize_fac.TabIndex = 35;
            this.Num_resize_fac.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_resize_fac.ValueChanged += new System.EventHandler(this.Num_resize_fac_ValueChanged);
            // 
            // Box_Frame_rmv
            // 
            this.Box_Frame_rmv.AutoSize = true;
            this.Box_Frame_rmv.Location = new System.Drawing.Point(112, 360);
            this.Box_Frame_rmv.Name = "Box_Frame_rmv";
            this.Box_Frame_rmv.Size = new System.Drawing.Size(136, 19);
            this.Box_Frame_rmv.TabIndex = 36;
            this.Box_Frame_rmv.Text = "Remove Black Frame";
            this.Box_Frame_rmv.UseVisualStyleBackColor = true;
            this.Box_Frame_rmv.CheckedChanged += new System.EventHandler(this.SetRemoveFrame);
            // 
            // tesss_mdl_b
            // 
            this.tesss_mdl_b.FormattingEnabled = true;
            this.tesss_mdl_b.Location = new System.Drawing.Point(449, 740);
            this.tesss_mdl_b.Name = "tesss_mdl_b";
            this.tesss_mdl_b.Size = new System.Drawing.Size(143, 23);
            this.tesss_mdl_b.TabIndex = 37;
            this.tesss_mdl_b.SelectedIndexChanged += new System.EventHandler(this.SetOcrLanguageMode);
            // 
            // pagmode_b
            // 
            this.pagmode_b.FormattingEnabled = true;
            this.pagmode_b.Items.AddRange(new object[] {
            "Auto",
            "RawLine",
            "SingleLine",
            "SingleWord",
            "AutoOsd",
            "CircleWord"});
            this.pagmode_b.Location = new System.Drawing.Point(449, 778);
            this.pagmode_b.Name = "pagmode_b";
            this.pagmode_b.Size = new System.Drawing.Size(143, 23);
            this.pagmode_b.TabIndex = 38;
            this.pagmode_b.SelectedIndexChanged += new System.EventHandler(this.SetOcRPageMode);
            // 
            // combo_enginemod_b
            // 
            this.combo_enginemod_b.FormattingEnabled = true;
            this.combo_enginemod_b.Items.AddRange(new object[] {
            "LstmOnly",
            "TesseractOnly",
            "Default",
            "TesseractLstmCombined"});
            this.combo_enginemod_b.Location = new System.Drawing.Point(449, 817);
            this.combo_enginemod_b.Name = "combo_enginemod_b";
            this.combo_enginemod_b.Size = new System.Drawing.Size(143, 23);
            this.combo_enginemod_b.TabIndex = 39;
            this.combo_enginemod_b.SelectedIndexChanged += new System.EventHandler(this.SetEnginMode);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(362, 745);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 15);
            this.label16.TabIndex = 40;
            this.label16.Text = "Model_Lang";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(362, 784);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 15);
            this.label17.TabIndex = 41;
            this.label17.Text = "Page_Mode";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(362, 820);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 15);
            this.label18.TabIndex = 42;
            this.label18.Text = "Engin_Mode";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(112, 131);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 15);
            this.label19.TabIndex = 43;
            this.label19.Text = "Start";
            // 
            // Num_start
            // 
            this.Num_start.Location = new System.Drawing.Point(156, 129);
            this.Num_start.Name = "Num_start";
            this.Num_start.Size = new System.Drawing.Size(65, 23);
            this.Num_start.TabIndex = 44;
            this.Num_start.ValueChanged += new System.EventHandler(this.SetStartPoint);
            // 
            // CkeckBoxBlackDots
            // 
            this.CkeckBoxBlackDots.AutoSize = true;
            this.CkeckBoxBlackDots.Location = new System.Drawing.Point(112, 397);
            this.CkeckBoxBlackDots.Name = "CkeckBoxBlackDots";
            this.CkeckBoxBlackDots.Size = new System.Drawing.Size(127, 19);
            this.CkeckBoxBlackDots.TabIndex = 45;
            this.CkeckBoxBlackDots.Text = "Remove Black Dots";
            this.CkeckBoxBlackDots.UseVisualStyleBackColor = true;
            this.CkeckBoxBlackDots.CheckedChanged += new System.EventHandler(this.RemoveBlackDotsCheck);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(323, 397);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(119, 15);
            this.label20.TabIndex = 46;
            this.label20.Text = "Parameter Black Dots";
            // 
            // NumParamBlackDots
            // 
            this.NumParamBlackDots.Location = new System.Drawing.Point(449, 393);
            this.NumParamBlackDots.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumParamBlackDots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumParamBlackDots.Name = "NumParamBlackDots";
            this.NumParamBlackDots.ReadOnly = true;
            this.NumParamBlackDots.Size = new System.Drawing.Size(120, 23);
            this.NumParamBlackDots.TabIndex = 47;
            this.NumParamBlackDots.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumParamBlackDots.ValueChanged += new System.EventHandler(this.SetParamBlackDots);
            // 
            // SaveSettings
            // 
            this.SaveSettings.Location = new System.Drawing.Point(119, 64);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(120, 23);
            this.SaveSettings.TabIndex = 48;
            this.SaveSettings.Text = "Save Batch Settings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettingsClick);
            // 
            // LoadSettings
            // 
            this.LoadSettings.Location = new System.Drawing.Point(277, 64);
            this.LoadSettings.Name = "LoadSettings";
            this.LoadSettings.Size = new System.Drawing.Size(130, 23);
            this.LoadSettings.TabIndex = 49;
            this.LoadSettings.Text = "Load Batch Settings";
            this.LoadSettings.UseVisualStyleBackColor = true;
            this.LoadSettings.Click += new System.EventHandler(this.LoadSettingsClick);
            // 
            // ClassFormBatchConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.LoadSettings);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.NumParamBlackDots);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.CkeckBoxBlackDots);
            this.Controls.Add(this.Num_start);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.combo_enginemod_b);
            this.Controls.Add(this.pagmode_b);
            this.Controls.Add(this.tesss_mdl_b);
            this.Controls.Add(this.Box_Frame_rmv);
            this.Controls.Add(this.Num_resize_fac);
            this.Controls.Add(this.Num_param_seg_h);
            this.Controls.Add(this.Num_param_seg_w);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Num_param_seg_destr);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Num_param_line_eli_h);
            this.Controls.Add(this.Num_param_line_eli_w);
            this.Controls.Add(this.Num_paramBin);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Box_Ocrmod);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Box_areas);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Box_Sorting);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Box_Segment);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Box_Linestrength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Box_Lineelimi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Box_skew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBox_bina);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Num_interva);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Sharpen_box);
            this.Controls.Add(this.Noise_box);
            this.Controls.Add(this.Resize_box);
            this.Name = "ClassFormBatchConfig";
            this.Size = new System.Drawing.Size(728, 958);
            ((System.ComponentModel.ISupportInitialize)(this.Num_interva)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_paramBin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_line_eli_w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_line_eli_h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_destr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_param_seg_h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_resize_fac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumParamBlackDots)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox Resize_box;
        private CheckBox Noise_box;
        private CheckBox Sharpen_box;
        private Button StartButton;
        private NumericUpDown Num_interva;
        private Label label1;
        private ComboBox ComboBox_bina;
        private Label label2;
        private Label label3;
        private ComboBox Box_skew;
        private Label label4;
        private ComboBox Box_Lineelimi;
        private Label label5;
        private ComboBox Box_Linestrength;
        private Label label6;
        private ComboBox Box_Segment;
        private Label label7;
        private ComboBox Box_Sorting;
        private Label label8;
        private ComboBox Box_areas;
        private Label label9;
        private ComboBox Box_Ocrmod;
        private Label label10;
        private Label label11;
        private Label label12;
        private NumericUpDown Num_paramBin;
        private NumericUpDown Num_param_line_eli_w;
        private NumericUpDown Num_param_line_eli_h;
        private Label label13;
        private NumericUpDown Num_param_seg_destr;
        private Label label14;
        private Label label15;
        private NumericUpDown Num_param_seg_w;
        private NumericUpDown Num_param_seg_h;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private NumericUpDown Num_resize_fac;
        private CheckBox Box_Frame_rmv;
        private ComboBox tesss_mdl_b;
        private ComboBox pagmode_b;
        private ComboBox combo_enginemod_b;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private NumericUpDown Num_start;
        private CheckBox CkeckBoxBlackDots;
        private Label label20;
        private NumericUpDown NumParamBlackDots;
        private Button SaveSettings;
        private Button LoadSettings;
    }
}
