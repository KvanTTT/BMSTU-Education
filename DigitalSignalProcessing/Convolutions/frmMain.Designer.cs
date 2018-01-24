namespace Convolutions
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
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.rbRectRect = new System.Windows.Forms.RadioButton();
			this.rbRectGauss = new System.Windows.Forms.RadioButton();
			this.rbGaussGauss = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.udT = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.udMagnitude = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.udMu = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.udSamplingRate = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.udSigma = new System.Windows.Forms.NumericUpDown();
			this.brnCalc = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.udT)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).BeginInit();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Location = new System.Drawing.Point(12, 12);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0D;
			this.zedGraphControl1.ScrollMaxX = 0D;
			this.zedGraphControl1.ScrollMaxY = 0D;
			this.zedGraphControl1.ScrollMaxY2 = 0D;
			this.zedGraphControl1.ScrollMinX = 0D;
			this.zedGraphControl1.ScrollMinY = 0D;
			this.zedGraphControl1.ScrollMinY2 = 0D;
			this.zedGraphControl1.Size = new System.Drawing.Size(624, 387);
			this.zedGraphControl1.TabIndex = 0;
			// 
			// rbRectRect
			// 
			this.rbRectRect.AutoSize = true;
			this.rbRectRect.Checked = true;
			this.rbRectRect.Location = new System.Drawing.Point(498, 431);
			this.rbRectRect.Name = "rbRectRect";
			this.rbRectRect.Size = new System.Drawing.Size(74, 17);
			this.rbRectRect.TabIndex = 1;
			this.rbRectRect.TabStop = true;
			this.rbRectRect.Text = "Rect Rect";
			this.rbRectRect.UseVisualStyleBackColor = true;
			this.rbRectRect.CheckedChanged += new System.EventHandler(this.brnCalc_Click);
			// 
			// rbRectGauss
			// 
			this.rbRectGauss.AutoSize = true;
			this.rbRectGauss.Location = new System.Drawing.Point(498, 454);
			this.rbRectGauss.Name = "rbRectGauss";
			this.rbRectGauss.Size = new System.Drawing.Size(81, 17);
			this.rbRectGauss.TabIndex = 2;
			this.rbRectGauss.Text = "Rect Gauss";
			this.rbRectGauss.UseVisualStyleBackColor = true;
			this.rbRectGauss.CheckedChanged += new System.EventHandler(this.brnCalc_Click);
			// 
			// rbGaussGauss
			// 
			this.rbGaussGauss.AutoSize = true;
			this.rbGaussGauss.Location = new System.Drawing.Point(498, 477);
			this.rbGaussGauss.Name = "rbGaussGauss";
			this.rbGaussGauss.Size = new System.Drawing.Size(88, 17);
			this.rbGaussGauss.TabIndex = 4;
			this.rbGaussGauss.Text = "Gauss Gauss";
			this.rbGaussGauss.UseVisualStyleBackColor = true;
			this.rbGaussGauss.CheckedChanged += new System.EventHandler(this.brnCalc_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(54, 432);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 20);
			this.label5.TabIndex = 25;
			this.label5.Text = "T";
			// 
			// udT
			// 
			this.udT.Location = new System.Drawing.Point(79, 431);
			this.udT.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udT.Name = "udT";
			this.udT.Size = new System.Drawing.Size(71, 20);
			this.udT.TabIndex = 24;
			this.udT.Value = new decimal(new int[] {
            341,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(301, 465);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 16);
			this.label4.TabIndex = 23;
			this.label4.Text = "Magnitude";
			// 
			// udMagnitude
			// 
			this.udMagnitude.Location = new System.Drawing.Point(378, 462);
			this.udMagnitude.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMagnitude.Name = "udMagnitude";
			this.udMagnitude.Size = new System.Drawing.Size(71, 20);
			this.udMagnitude.TabIndex = 22;
			this.udMagnitude.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(57, 463);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18, 20);
			this.label3.TabIndex = 21;
			this.label3.Text = "μ";
			// 
			// udMu
			// 
			this.udMu.Location = new System.Drawing.Point(79, 465);
			this.udMu.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udMu.Name = "udMu";
			this.udMu.Size = new System.Drawing.Size(71, 20);
			this.udMu.TabIndex = 20;
			this.udMu.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(275, 433);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 16);
			this.label2.TabIndex = 19;
			this.label2.Text = "Sampling Rate";
			// 
			// udSamplingRate
			// 
			this.udSamplingRate.Location = new System.Drawing.Point(378, 431);
			this.udSamplingRate.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSamplingRate.Name = "udSamplingRate";
			this.udSamplingRate.Size = new System.Drawing.Size(71, 20);
			this.udSamplingRate.TabIndex = 18;
			this.udSamplingRate.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(162, 462);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 20);
			this.label1.TabIndex = 17;
			this.label1.Text = "σ";
			// 
			// udSigma
			// 
			this.udSigma.Location = new System.Drawing.Point(184, 464);
			this.udSigma.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
			this.udSigma.Name = "udSigma";
			this.udSigma.Size = new System.Drawing.Size(71, 20);
			this.udSigma.TabIndex = 16;
			this.udSigma.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
			// 
			// brnCalc
			// 
			this.brnCalc.Location = new System.Drawing.Point(503, 506);
			this.brnCalc.Name = "brnCalc";
			this.brnCalc.Size = new System.Drawing.Size(75, 23);
			this.brnCalc.TabIndex = 26;
			this.brnCalc.Text = "Recalculate";
			this.brnCalc.UseVisualStyleBackColor = true;
			this.brnCalc.Click += new System.EventHandler(this.brnCalc_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(656, 544);
			this.Controls.Add(this.brnCalc);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.udT);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.udMagnitude);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.udMu);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.udSamplingRate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.udSigma);
			this.Controls.Add(this.rbGaussGauss);
			this.Controls.Add(this.rbRectGauss);
			this.Controls.Add(this.rbRectRect);
			this.Controls.Add(this.zedGraphControl1);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.udT)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMagnitude)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udMu)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSamplingRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udSigma)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.RadioButton rbRectRect;
		private System.Windows.Forms.RadioButton rbRectGauss;
		private System.Windows.Forms.RadioButton rbGaussGauss;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown udT;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown udMagnitude;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udMu;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown udSamplingRate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown udSigma;
		private System.Windows.Forms.Button brnCalc;
	}
}

