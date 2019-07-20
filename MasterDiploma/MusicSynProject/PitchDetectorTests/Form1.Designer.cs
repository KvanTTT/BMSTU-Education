namespace PitchDetectorTests
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbAutocorrel = new System.Windows.Forms.TextBox();
			this.tbAutocorrelNew = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.nudFreq = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.cbParallel = new System.Windows.Forms.CheckBox();
			this.tbMaxLikehood = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.tbHPS = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tbZCR = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.btnPlot = new System.Windows.Forms.Button();
			this.nudMinFreq = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.nudMaxFreq = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.nudFreqStep = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.nudSampleCount = new System.Windows.Forms.NumericUpDown();
			this.nudSamplingRate = new System.Windows.Forms.NumericUpDown();
			this.tbZCRTime = new System.Windows.Forms.TextBox();
			this.tbHPSTime = new System.Windows.Forms.TextBox();
			this.tbMaxLikehoodTime = new System.Windows.Forms.TextBox();
			this.tbAutocorrelationNewTime = new System.Windows.Forms.TextBox();
			this.tbAutocorrelationTime = new System.Windows.Forms.TextBox();
			this.nudSamplesStep = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.nudMaxSamples = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.nudMinSamples = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.btnPlotTimes = new System.Windows.Forms.Button();
			this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
			((System.ComponentModel.ISupportInitialize)(this.nudFreq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinFreq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFreq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSampleCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSamplingRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSamplesStep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxSamples)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinSamples)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(31, 238);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Autocorrelation";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(31, 265);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Modifed Autocorrelation";
			// 
			// tbAutocorrel
			// 
			this.tbAutocorrel.Location = new System.Drawing.Point(154, 235);
			this.tbAutocorrel.Name = "tbAutocorrel";
			this.tbAutocorrel.ReadOnly = true;
			this.tbAutocorrel.Size = new System.Drawing.Size(69, 20);
			this.tbAutocorrel.TabIndex = 2;
			// 
			// tbAutocorrelNew
			// 
			this.tbAutocorrelNew.Location = new System.Drawing.Point(154, 265);
			this.tbAutocorrelNew.Name = "tbAutocorrelNew";
			this.tbAutocorrelNew.ReadOnly = true;
			this.tbAutocorrelNew.Size = new System.Drawing.Size(69, 20);
			this.tbAutocorrelNew.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(31, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Frequency";
			// 
			// nudFreq
			// 
			this.nudFreq.Location = new System.Drawing.Point(154, 68);
			this.nudFreq.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudFreq.Name = "nudFreq";
			this.nudFreq.Size = new System.Drawing.Size(149, 20);
			this.nudFreq.TabIndex = 5;
			this.nudFreq.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(31, 118);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Sampling Rate";
			// 
			// btnCalculate
			// 
			this.btnCalculate.Location = new System.Drawing.Point(34, 373);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(116, 22);
			this.btnCalculate.TabIndex = 8;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(33, 176);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Parallel";
			// 
			// cbParallel
			// 
			this.cbParallel.AutoSize = true;
			this.cbParallel.Checked = true;
			this.cbParallel.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbParallel.Location = new System.Drawing.Point(154, 176);
			this.cbParallel.Name = "cbParallel";
			this.cbParallel.Size = new System.Drawing.Size(60, 17);
			this.cbParallel.TabIndex = 10;
			this.cbParallel.Text = "Parallel";
			this.cbParallel.UseVisualStyleBackColor = true;
			// 
			// tbMaxLikehood
			// 
			this.tbMaxLikehood.Location = new System.Drawing.Point(154, 291);
			this.tbMaxLikehood.Name = "tbMaxLikehood";
			this.tbMaxLikehood.ReadOnly = true;
			this.tbMaxLikehood.Size = new System.Drawing.Size(69, 20);
			this.tbMaxLikehood.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(31, 291);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "HPS";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(154, 17);
			this.trackBar1.Maximum = 5000;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(149, 45);
			this.trackBar1.TabIndex = 13;
			this.trackBar1.Value = 400;
			// 
			// tbHPS
			// 
			this.tbHPS.Location = new System.Drawing.Point(154, 320);
			this.tbHPS.Name = "tbHPS";
			this.tbHPS.ReadOnly = true;
			this.tbHPS.Size = new System.Drawing.Size(69, 20);
			this.tbHPS.TabIndex = 16;
			this.tbHPS.Visible = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(31, 320);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(29, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "HPS";
			this.label7.Visible = false;
			// 
			// tbZCR
			// 
			this.tbZCR.Location = new System.Drawing.Point(154, 209);
			this.tbZCR.Name = "tbZCR";
			this.tbZCR.ReadOnly = true;
			this.tbZCR.Size = new System.Drawing.Size(69, 20);
			this.tbZCR.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(31, 209);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(29, 13);
			this.label8.TabIndex = 17;
			this.label8.Text = "ZCR";
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraphControl1.Location = new System.Drawing.Point(496, 12);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0D;
			this.zedGraphControl1.ScrollMaxX = 0D;
			this.zedGraphControl1.ScrollMaxY = 0D;
			this.zedGraphControl1.ScrollMaxY2 = 0D;
			this.zedGraphControl1.ScrollMinX = 0D;
			this.zedGraphControl1.ScrollMinY = 0D;
			this.zedGraphControl1.ScrollMinY2 = 0D;
			this.zedGraphControl1.Size = new System.Drawing.Size(458, 349);
			this.zedGraphControl1.TabIndex = 19;
			// 
			// btnPlot
			// 
			this.btnPlot.Location = new System.Drawing.Point(323, 377);
			this.btnPlot.Name = "btnPlot";
			this.btnPlot.Size = new System.Drawing.Size(140, 22);
			this.btnPlot.TabIndex = 20;
			this.btnPlot.Text = "Plot";
			this.btnPlot.UseVisualStyleBackColor = true;
			this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
			// 
			// nudMinFreq
			// 
			this.nudMinFreq.Location = new System.Drawing.Point(560, 380);
			this.nudMinFreq.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudMinFreq.Name = "nudMinFreq";
			this.nudMinFreq.Size = new System.Drawing.Size(77, 20);
			this.nudMinFreq.TabIndex = 22;
			this.nudMinFreq.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(506, 382);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(48, 13);
			this.label9.TabIndex = 21;
			this.label9.Text = "Min Freq";
			// 
			// nudMaxFreq
			// 
			this.nudMaxFreq.Location = new System.Drawing.Point(706, 380);
			this.nudMaxFreq.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudMaxFreq.Name = "nudMaxFreq";
			this.nudMaxFreq.Size = new System.Drawing.Size(77, 20);
			this.nudMaxFreq.TabIndex = 24;
			this.nudMaxFreq.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(652, 382);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(51, 13);
			this.label10.TabIndex = 23;
			this.label10.Text = "Max Freq";
			// 
			// nudFreqStep
			// 
			this.nudFreqStep.Location = new System.Drawing.Point(845, 380);
			this.nudFreqStep.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudFreqStep.Name = "nudFreqStep";
			this.nudFreqStep.Size = new System.Drawing.Size(77, 20);
			this.nudFreqStep.TabIndex = 26;
			this.nudFreqStep.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(810, 382);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(29, 13);
			this.label11.TabIndex = 25;
			this.label11.Text = "Step";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(33, 143);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(73, 13);
			this.label12.TabIndex = 27;
			this.label12.Text = "Sample Count";
			// 
			// nudSampleCount
			// 
			this.nudSampleCount.Location = new System.Drawing.Point(154, 143);
			this.nudSampleCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudSampleCount.Name = "nudSampleCount";
			this.nudSampleCount.Size = new System.Drawing.Size(149, 20);
			this.nudSampleCount.TabIndex = 28;
			this.nudSampleCount.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			// 
			// nudSamplingRate
			// 
			this.nudSamplingRate.Location = new System.Drawing.Point(154, 116);
			this.nudSamplingRate.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudSamplingRate.Name = "nudSamplingRate";
			this.nudSamplingRate.Size = new System.Drawing.Size(149, 20);
			this.nudSamplingRate.TabIndex = 29;
			this.nudSamplingRate.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			// 
			// tbZCRTime
			// 
			this.tbZCRTime.Location = new System.Drawing.Point(229, 209);
			this.tbZCRTime.Name = "tbZCRTime";
			this.tbZCRTime.ReadOnly = true;
			this.tbZCRTime.Size = new System.Drawing.Size(69, 20);
			this.tbZCRTime.TabIndex = 36;
			// 
			// tbHPSTime
			// 
			this.tbHPSTime.Location = new System.Drawing.Point(229, 320);
			this.tbHPSTime.Name = "tbHPSTime";
			this.tbHPSTime.ReadOnly = true;
			this.tbHPSTime.Size = new System.Drawing.Size(69, 20);
			this.tbHPSTime.TabIndex = 35;
			this.tbHPSTime.Visible = false;
			// 
			// tbMaxLikehoodTime
			// 
			this.tbMaxLikehoodTime.Location = new System.Drawing.Point(229, 291);
			this.tbMaxLikehoodTime.Name = "tbMaxLikehoodTime";
			this.tbMaxLikehoodTime.ReadOnly = true;
			this.tbMaxLikehoodTime.Size = new System.Drawing.Size(69, 20);
			this.tbMaxLikehoodTime.TabIndex = 34;
			// 
			// tbAutocorrelationNewTime
			// 
			this.tbAutocorrelationNewTime.Location = new System.Drawing.Point(229, 265);
			this.tbAutocorrelationNewTime.Name = "tbAutocorrelationNewTime";
			this.tbAutocorrelationNewTime.ReadOnly = true;
			this.tbAutocorrelationNewTime.Size = new System.Drawing.Size(69, 20);
			this.tbAutocorrelationNewTime.TabIndex = 33;
			// 
			// tbAutocorrelationTime
			// 
			this.tbAutocorrelationTime.Location = new System.Drawing.Point(229, 235);
			this.tbAutocorrelationTime.Name = "tbAutocorrelationTime";
			this.tbAutocorrelationTime.ReadOnly = true;
			this.tbAutocorrelationTime.Size = new System.Drawing.Size(69, 20);
			this.tbAutocorrelationTime.TabIndex = 32;
			// 
			// nudSamplesStep
			// 
			this.nudSamplesStep.Location = new System.Drawing.Point(856, 788);
			this.nudSamplesStep.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudSamplesStep.Name = "nudSamplesStep";
			this.nudSamplesStep.Size = new System.Drawing.Size(77, 20);
			this.nudSamplesStep.TabIndex = 44;
			this.nudSamplesStep.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(821, 790);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(29, 13);
			this.label13.TabIndex = 43;
			this.label13.Text = "Step";
			// 
			// nudMaxSamples
			// 
			this.nudMaxSamples.Location = new System.Drawing.Point(727, 788);
			this.nudMaxSamples.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudMaxSamples.Name = "nudMaxSamples";
			this.nudMaxSamples.Size = new System.Drawing.Size(77, 20);
			this.nudMaxSamples.TabIndex = 42;
			this.nudMaxSamples.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(652, 790);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(70, 13);
			this.label14.TabIndex = 41;
			this.label14.Text = "Max Samples";
			// 
			// nudMinSamples
			// 
			this.nudMinSamples.Location = new System.Drawing.Point(580, 788);
			this.nudMinSamples.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
			this.nudMinSamples.Name = "nudMinSamples";
			this.nudMinSamples.Size = new System.Drawing.Size(66, 20);
			this.nudMinSamples.TabIndex = 40;
			this.nudMinSamples.Value = new decimal(new int[] {
            9000,
            0,
            0,
            0});
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(506, 790);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(67, 13);
			this.label15.TabIndex = 39;
			this.label15.Text = "Min Samples";
			// 
			// btnPlotTimes
			// 
			this.btnPlotTimes.Location = new System.Drawing.Point(323, 785);
			this.btnPlotTimes.Name = "btnPlotTimes";
			this.btnPlotTimes.Size = new System.Drawing.Size(140, 22);
			this.btnPlotTimes.TabIndex = 38;
			this.btnPlotTimes.Text = "Plot";
			this.btnPlotTimes.UseVisualStyleBackColor = true;
			this.btnPlotTimes.Click += new System.EventHandler(this.btnPlotTimes_Click);
			// 
			// zedGraphControl2
			// 
			this.zedGraphControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraphControl2.Location = new System.Drawing.Point(496, 420);
			this.zedGraphControl2.Name = "zedGraphControl2";
			this.zedGraphControl2.ScrollGrace = 0D;
			this.zedGraphControl2.ScrollMaxX = 0D;
			this.zedGraphControl2.ScrollMaxY = 0D;
			this.zedGraphControl2.ScrollMaxY2 = 0D;
			this.zedGraphControl2.ScrollMinX = 0D;
			this.zedGraphControl2.ScrollMinY = 0D;
			this.zedGraphControl2.ScrollMinY2 = 0D;
			this.zedGraphControl2.Size = new System.Drawing.Size(458, 349);
			this.zedGraphControl2.TabIndex = 37;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(966, 822);
			this.Controls.Add(this.nudSamplesStep);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.nudMaxSamples);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.nudMinSamples);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.btnPlotTimes);
			this.Controls.Add(this.zedGraphControl2);
			this.Controls.Add(this.tbZCRTime);
			this.Controls.Add(this.tbHPSTime);
			this.Controls.Add(this.tbMaxLikehoodTime);
			this.Controls.Add(this.tbAutocorrelationNewTime);
			this.Controls.Add(this.tbAutocorrelationTime);
			this.Controls.Add(this.nudSamplingRate);
			this.Controls.Add(this.nudSampleCount);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.nudFreqStep);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.nudMaxFreq);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.nudMinFreq);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnPlot);
			this.Controls.Add(this.zedGraphControl1);
			this.Controls.Add(this.tbZCR);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.tbHPS);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.tbMaxLikehood);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cbParallel);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnCalculate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.nudFreq);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbAutocorrelNew);
			this.Controls.Add(this.tbAutocorrel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudFreq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinFreq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFreq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSampleCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSamplingRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSamplesStep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxSamples)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMinSamples)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbAutocorrel;
		private System.Windows.Forms.TextBox tbAutocorrelNew;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudFreq;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox cbParallel;
		private System.Windows.Forms.TextBox tbMaxLikehood;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.TextBox tbHPS;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbZCR;
		private System.Windows.Forms.Label label8;
		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Button btnPlot;
		private System.Windows.Forms.NumericUpDown nudMinFreq;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.NumericUpDown nudMaxFreq;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown nudFreqStep;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown nudSampleCount;
		private System.Windows.Forms.NumericUpDown nudSamplingRate;
		private System.Windows.Forms.TextBox tbZCRTime;
		private System.Windows.Forms.TextBox tbHPSTime;
		private System.Windows.Forms.TextBox tbMaxLikehoodTime;
		private System.Windows.Forms.TextBox tbAutocorrelationNewTime;
		private System.Windows.Forms.TextBox tbAutocorrelationTime;
		private System.Windows.Forms.NumericUpDown nudSamplesStep;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown nudMaxSamples;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown nudMinSamples;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btnPlotTimes;
		private ZedGraph.ZedGraphControl zedGraphControl2;
	}
}

