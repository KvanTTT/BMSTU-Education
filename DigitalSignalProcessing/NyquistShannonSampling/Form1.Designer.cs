namespace NyquistShannonSampling
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
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.btnRecalc = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.udT = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.udMu = new System.Windows.Forms.NumericUpDown();
			this.lblt0 = new System.Windows.Forms.Label();
			this.udt0 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.udSigma = new System.Windows.Forms.NumericUpDown();
			this.rbGaussian = new System.Windows.Forms.RadioButton();
			this.rbRectangle = new System.Windows.Forms.RadioButton();
			this.rbDelta = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.udMagnitude = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.udSamplingRate = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.udDelta = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.udT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udt0)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).BeginInit();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0D;
			this.zedGraphControl1.ScrollMaxX = 0D;
			this.zedGraphControl1.ScrollMaxY = 0D;
			this.zedGraphControl1.ScrollMaxY2 = 0D;
			this.zedGraphControl1.ScrollMinX = 0D;
			this.zedGraphControl1.ScrollMinY = 0D;
			this.zedGraphControl1.ScrollMinY2 = 0D;
			this.zedGraphControl1.Size = new System.Drawing.Size(839, 338);
			this.zedGraphControl1.TabIndex = 0;
			// 
			// btnRecalc
			// 
			this.btnRecalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRecalc.Location = new System.Drawing.Point(706, 407);
			this.btnRecalc.Name = "btnRecalc";
			this.btnRecalc.Size = new System.Drawing.Size(131, 26);
			this.btnRecalc.TabIndex = 1;
			this.btnRecalc.Text = "Recalc";
			this.btnRecalc.UseVisualStyleBackColor = true;
			this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(251, 417);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 20);
			this.label5.TabIndex = 26;
			this.label5.Text = "T";
			// 
			// udT
			// 
			this.udT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udT.Location = new System.Drawing.Point(276, 416);
			this.udT.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udT.Name = "udT";
			this.udT.Size = new System.Drawing.Size(71, 20);
			this.udT.TabIndex = 25;
			this.udT.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(254, 448);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18, 20);
			this.label3.TabIndex = 24;
			this.label3.Text = "μ";
			// 
			// udMu
			// 
			this.udMu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udMu.Location = new System.Drawing.Point(276, 450);
			this.udMu.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMu.Name = "udMu";
			this.udMu.Size = new System.Drawing.Size(71, 20);
			this.udMu.TabIndex = 23;
			this.udMu.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			// 
			// lblt0
			// 
			this.lblt0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblt0.AutoSize = true;
			this.lblt0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblt0.Location = new System.Drawing.Point(251, 383);
			this.lblt0.Name = "lblt0";
			this.lblt0.Size = new System.Drawing.Size(23, 20);
			this.lblt0.TabIndex = 22;
			this.lblt0.Text = "t0";
			// 
			// udt0
			// 
			this.udt0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udt0.Location = new System.Drawing.Point(276, 382);
			this.udt0.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udt0.Name = "udt0";
			this.udt0.Size = new System.Drawing.Size(71, 20);
			this.udt0.TabIndex = 21;
			this.udt0.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(359, 447);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 20);
			this.label1.TabIndex = 20;
			this.label1.Text = "σ";
			// 
			// udSigma
			// 
			this.udSigma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udSigma.Location = new System.Drawing.Point(381, 449);
			this.udSigma.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSigma.Name = "udSigma";
			this.udSigma.Size = new System.Drawing.Size(71, 20);
			this.udSigma.TabIndex = 19;
			this.udSigma.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			// 
			// rbGaussian
			// 
			this.rbGaussian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbGaussian.AutoSize = true;
			this.rbGaussian.Location = new System.Drawing.Point(25, 449);
			this.rbGaussian.Name = "rbGaussian";
			this.rbGaussian.Size = new System.Drawing.Size(93, 17);
			this.rbGaussian.TabIndex = 18;
			this.rbGaussian.Text = "Gaussian func";
			this.rbGaussian.UseVisualStyleBackColor = true;
			this.rbGaussian.CheckedChanged += new System.EventHandler(this.btnRecalc_Click);
			// 
			// rbRectangle
			// 
			this.rbRectangle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbRectangle.AutoSize = true;
			this.rbRectangle.Location = new System.Drawing.Point(25, 416);
			this.rbRectangle.Name = "rbRectangle";
			this.rbRectangle.Size = new System.Drawing.Size(104, 17);
			this.rbRectangle.TabIndex = 17;
			this.rbRectangle.Text = "Rectangle signal";
			this.rbRectangle.UseVisualStyleBackColor = true;
			this.rbRectangle.CheckedChanged += new System.EventHandler(this.btnRecalc_Click);
			// 
			// rbDelta
			// 
			this.rbDelta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.rbDelta.AutoSize = true;
			this.rbDelta.Checked = true;
			this.rbDelta.Location = new System.Drawing.Point(25, 382);
			this.rbDelta.Name = "rbDelta";
			this.rbDelta.Size = new System.Drawing.Size(74, 17);
			this.rbDelta.TabIndex = 16;
			this.rbDelta.TabStop = true;
			this.rbDelta.Text = "Delta func";
			this.rbDelta.UseVisualStyleBackColor = true;
			this.rbDelta.CheckedChanged += new System.EventHandler(this.btnRecalc_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(515, 420);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 16);
			this.label4.TabIndex = 30;
			this.label4.Text = "Magnitude";
			// 
			// udMagnitude
			// 
			this.udMagnitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udMagnitude.Location = new System.Drawing.Point(592, 417);
			this.udMagnitude.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMagnitude.Name = "udMagnitude";
			this.udMagnitude.Size = new System.Drawing.Size(71, 20);
			this.udMagnitude.TabIndex = 29;
			this.udMagnitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(489, 385);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 16);
			this.label2.TabIndex = 28;
			this.label2.Text = "Sampling Rate";
			// 
			// udSamplingRate
			// 
			this.udSamplingRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.udSamplingRate.Location = new System.Drawing.Point(592, 383);
			this.udSamplingRate.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSamplingRate.Name = "udSamplingRate";
			this.udSamplingRate.Size = new System.Drawing.Size(71, 20);
			this.udSamplingRate.TabIndex = 27;
			this.udSamplingRate.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(546, 449);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 32;
			this.label6.Text = "Delta";
			// 
			// udDelta
			// 
			this.udDelta.Location = new System.Drawing.Point(592, 447);
			this.udDelta.Name = "udDelta";
			this.udDelta.Size = new System.Drawing.Size(71, 20);
			this.udDelta.TabIndex = 33;
			this.udDelta.Text = "0,1";
			this.udDelta.TextChanged += new System.EventHandler(this.btnRecalc_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(863, 492);
			this.Controls.Add(this.udDelta);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.udMagnitude);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.udSamplingRate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.udT);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.udMu);
			this.Controls.Add(this.lblt0);
			this.Controls.Add(this.udt0);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.udSigma);
			this.Controls.Add(this.rbGaussian);
			this.Controls.Add(this.rbRectangle);
			this.Controls.Add(this.rbDelta);
			this.Controls.Add(this.btnRecalc);
			this.Controls.Add(this.zedGraphControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.udT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udt0)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Button btnRecalc;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown udT;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udMu;
		private System.Windows.Forms.Label lblt0;
		private System.Windows.Forms.NumericUpDown udt0;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown udSigma;
		private System.Windows.Forms.RadioButton rbGaussian;
		private System.Windows.Forms.RadioButton rbRectangle;
		private System.Windows.Forms.RadioButton rbDelta;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown udMagnitude;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown udSamplingRate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox udDelta;
	}
}

