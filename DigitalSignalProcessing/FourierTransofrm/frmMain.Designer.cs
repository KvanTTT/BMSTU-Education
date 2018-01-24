namespace FourierTransofrm
{
	partial class frmMain
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
			this.rbDelta = new System.Windows.Forms.RadioButton();
			this.rbRectangle = new System.Windows.Forms.RadioButton();
			this.rbGaussian = new System.Windows.Forms.RadioButton();
			this.udSigma = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.udSamplingRate = new System.Windows.Forms.NumericUpDown();
			this.lblt0 = new System.Windows.Forms.Label();
			this.udt0 = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.udMu = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.udMagnitude = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.udT = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
			this.cbDFT = new System.Windows.Forms.CheckBox();
			this.cbFFT = new System.Windows.Forms.CheckBox();
			this.lblDftTime = new System.Windows.Forms.Label();
			this.lblFftTime = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udt0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// rbDelta
			// 
			this.rbDelta.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.rbDelta.AutoSize = true;
			this.rbDelta.Checked = true;
			this.rbDelta.Location = new System.Drawing.Point(118, 453);
			this.rbDelta.Name = "rbDelta";
			this.rbDelta.Size = new System.Drawing.Size(74, 17);
			this.rbDelta.TabIndex = 1;
			this.rbDelta.TabStop = true;
			this.rbDelta.Text = "Delta func";
			this.rbDelta.UseVisualStyleBackColor = true;
			this.rbDelta.CheckedChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// rbRectangle
			// 
			this.rbRectangle.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.rbRectangle.AutoSize = true;
			this.rbRectangle.Location = new System.Drawing.Point(118, 487);
			this.rbRectangle.Name = "rbRectangle";
			this.rbRectangle.Size = new System.Drawing.Size(104, 17);
			this.rbRectangle.TabIndex = 2;
			this.rbRectangle.Text = "Rectangle signal";
			this.rbRectangle.UseVisualStyleBackColor = true;
			this.rbRectangle.CheckedChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// rbGaussian
			// 
			this.rbGaussian.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.rbGaussian.AutoSize = true;
			this.rbGaussian.Location = new System.Drawing.Point(118, 520);
			this.rbGaussian.Name = "rbGaussian";
			this.rbGaussian.Size = new System.Drawing.Size(93, 17);
			this.rbGaussian.TabIndex = 3;
			this.rbGaussian.Text = "Gaussian func";
			this.rbGaussian.UseVisualStyleBackColor = true;
			this.rbGaussian.CheckedChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// udSigma
			// 
			this.udSigma.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udSigma.Location = new System.Drawing.Point(474, 520);
			this.udSigma.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSigma.Name = "udSigma";
			this.udSigma.Size = new System.Drawing.Size(71, 20);
			this.udSigma.TabIndex = 4;
			this.udSigma.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			this.udSigma.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(452, 518);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 20);
			this.label1.TabIndex = 5;
			this.label1.Text = "σ";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(598, 469);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Sampling Rate";
			// 
			// udSamplingRate
			// 
			this.udSamplingRate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udSamplingRate.Location = new System.Drawing.Point(701, 467);
			this.udSamplingRate.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSamplingRate.Name = "udSamplingRate";
			this.udSamplingRate.Size = new System.Drawing.Size(71, 20);
			this.udSamplingRate.TabIndex = 6;
			this.udSamplingRate.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.udSamplingRate.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// lblt0
			// 
			this.lblt0.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.lblt0.AutoSize = true;
			this.lblt0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblt0.Location = new System.Drawing.Point(344, 454);
			this.lblt0.Name = "lblt0";
			this.lblt0.Size = new System.Drawing.Size(23, 20);
			this.lblt0.TabIndex = 9;
			this.lblt0.Text = "t0";
			// 
			// udt0
			// 
			this.udt0.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udt0.Location = new System.Drawing.Point(369, 453);
			this.udt0.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udt0.Name = "udt0";
			this.udt0.Size = new System.Drawing.Size(71, 20);
			this.udt0.TabIndex = 8;
			this.udt0.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.udt0.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(347, 519);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18, 20);
			this.label3.TabIndex = 11;
			this.label3.Text = "μ";
			// 
			// udMu
			// 
			this.udMu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udMu.Location = new System.Drawing.Point(369, 521);
			this.udMu.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMu.Name = "udMu";
			this.udMu.Size = new System.Drawing.Size(71, 20);
			this.udMu.TabIndex = 10;
			this.udMu.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.udMu.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(624, 501);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 16);
			this.label4.TabIndex = 13;
			this.label4.Text = "Magnitude";
			// 
			// udMagnitude
			// 
			this.udMagnitude.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udMagnitude.Location = new System.Drawing.Point(701, 498);
			this.udMagnitude.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMagnitude.Name = "udMagnitude";
			this.udMagnitude.Size = new System.Drawing.Size(71, 20);
			this.udMagnitude.TabIndex = 12;
			this.udMagnitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.udMagnitude.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(344, 488);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 20);
			this.label5.TabIndex = 15;
			this.label5.Text = "T";
			// 
			// udT
			// 
			this.udT.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.udT.Location = new System.Drawing.Point(369, 487);
			this.udT.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udT.Name = "udT";
			this.udT.Size = new System.Drawing.Size(71, 20);
			this.udT.TabIndex = 14;
			this.udT.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			this.udT.ValueChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.button1.Location = new System.Drawing.Point(819, 459);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(138, 28);
			this.button1.TabIndex = 16;
			this.button1.Text = "Recalculate";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.zedGraphControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.zedGraphControl2);
			this.splitContainer1.Size = new System.Drawing.Size(1030, 420);
			this.splitContainer1.SplitterDistance = 502;
			this.splitContainer1.TabIndex = 18;
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zedGraphControl1.Location = new System.Drawing.Point(0, 0);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0D;
			this.zedGraphControl1.ScrollMaxX = 0D;
			this.zedGraphControl1.ScrollMaxY = 0D;
			this.zedGraphControl1.ScrollMaxY2 = 0D;
			this.zedGraphControl1.ScrollMinX = 0D;
			this.zedGraphControl1.ScrollMinY = 0D;
			this.zedGraphControl1.ScrollMinY2 = 0D;
			this.zedGraphControl1.Size = new System.Drawing.Size(502, 420);
			this.zedGraphControl1.TabIndex = 1;
			// 
			// zedGraphControl2
			// 
			this.zedGraphControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.zedGraphControl2.Location = new System.Drawing.Point(0, 0);
			this.zedGraphControl2.Name = "zedGraphControl2";
			this.zedGraphControl2.ScrollGrace = 0D;
			this.zedGraphControl2.ScrollMaxX = 0D;
			this.zedGraphControl2.ScrollMaxY = 0D;
			this.zedGraphControl2.ScrollMaxY2 = 0D;
			this.zedGraphControl2.ScrollMinX = 0D;
			this.zedGraphControl2.ScrollMinY = 0D;
			this.zedGraphControl2.ScrollMinY2 = 0D;
			this.zedGraphControl2.Size = new System.Drawing.Size(524, 420);
			this.zedGraphControl2.TabIndex = 18;
			// 
			// cbDFT
			// 
			this.cbDFT.AutoSize = true;
			this.cbDFT.Checked = true;
			this.cbDFT.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDFT.Location = new System.Drawing.Point(819, 500);
			this.cbDFT.Name = "cbDFT";
			this.cbDFT.Size = new System.Drawing.Size(47, 17);
			this.cbDFT.TabIndex = 19;
			this.cbDFT.Text = "DFT";
			this.cbDFT.UseVisualStyleBackColor = true;
			this.cbDFT.CheckedChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// cbFFT
			// 
			this.cbFFT.AutoSize = true;
			this.cbFFT.Checked = true;
			this.cbFFT.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbFFT.Location = new System.Drawing.Point(819, 523);
			this.cbFFT.Name = "cbFFT";
			this.cbFFT.Size = new System.Drawing.Size(45, 17);
			this.cbFFT.TabIndex = 20;
			this.cbFFT.Text = "FFT";
			this.cbFFT.UseVisualStyleBackColor = true;
			this.cbFFT.CheckedChanged += new System.EventHandler(this.rbDelta_CheckedChanged);
			// 
			// lblDftTime
			// 
			this.lblDftTime.AutoSize = true;
			this.lblDftTime.Location = new System.Drawing.Point(902, 500);
			this.lblDftTime.Name = "lblDftTime";
			this.lblDftTime.Size = new System.Drawing.Size(54, 13);
			this.lblDftTime.TabIndex = 21;
			this.lblDftTime.Text = "lblDftTime";
			// 
			// lblFftTime
			// 
			this.lblFftTime.AutoSize = true;
			this.lblFftTime.Location = new System.Drawing.Point(902, 524);
			this.lblFftTime.Name = "lblFftTime";
			this.lblFftTime.Size = new System.Drawing.Size(52, 13);
			this.lblFftTime.TabIndex = 22;
			this.lblFftTime.Text = "lblFftTime";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1063, 560);
			this.Controls.Add(this.lblFftTime);
			this.Controls.Add(this.lblDftTime);
			this.Controls.Add(this.cbFFT);
			this.Controls.Add(this.cbDFT);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.udT);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.udMagnitude);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.udMu);
			this.Controls.Add(this.lblt0);
			this.Controls.Add(this.udt0);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.udSamplingRate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.udSigma);
			this.Controls.Add(this.rbGaussian);
			this.Controls.Add(this.rbRectangle);
			this.Controls.Add(this.rbDelta);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udt0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udT)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbDelta;
		private System.Windows.Forms.RadioButton rbRectangle;
		private System.Windows.Forms.RadioButton rbGaussian;
		private System.Windows.Forms.NumericUpDown udSigma;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown udSamplingRate;
		private System.Windows.Forms.Label lblt0;
		private System.Windows.Forms.NumericUpDown udt0;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udMu;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown udMagnitude;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown udT;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ZedGraph.ZedGraphControl zedGraphControl1;
		private ZedGraph.ZedGraphControl zedGraphControl2;
		private System.Windows.Forms.CheckBox cbDFT;
		private System.Windows.Forms.CheckBox cbFFT;
		private System.Windows.Forms.Label lblDftTime;
		private System.Windows.Forms.Label lblFftTime;
	}
}

