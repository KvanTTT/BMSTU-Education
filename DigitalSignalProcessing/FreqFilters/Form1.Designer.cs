namespace FreqFilters
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbGaussInterference = new System.Windows.Forms.RadioButton();
			this.rbImpulse = new System.Windows.Forms.RadioButton();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.rbFreq = new System.Windows.Forms.RadioButton();
			this.rbTime = new System.Windows.Forms.RadioButton();
			this.btnRecalc = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmbFilterType = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
			this.zedGraphControl1.Size = new System.Drawing.Size(526, 404);
			this.zedGraphControl1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.rbGaussInterference);
			this.groupBox1.Controls.Add(this.rbImpulse);
			this.groupBox1.Location = new System.Drawing.Point(561, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(176, 54);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Interference";
			// 
			// rbGaussInterference
			// 
			this.rbGaussInterference.AutoSize = true;
			this.rbGaussInterference.Location = new System.Drawing.Point(95, 26);
			this.rbGaussInterference.Name = "rbGaussInterference";
			this.rbGaussInterference.Size = new System.Drawing.Size(55, 17);
			this.rbGaussInterference.TabIndex = 8;
			this.rbGaussInterference.Text = "Gauss";
			this.rbGaussInterference.UseVisualStyleBackColor = true;
			this.rbGaussInterference.CheckedChanged += new System.EventHandler(this.rbImpulse_CheckedChanged);
			// 
			// rbImpulse
			// 
			this.rbImpulse.AutoSize = true;
			this.rbImpulse.Checked = true;
			this.rbImpulse.Location = new System.Drawing.Point(12, 26);
			this.rbImpulse.Name = "rbImpulse";
			this.rbImpulse.Size = new System.Drawing.Size(61, 17);
			this.rbImpulse.TabIndex = 7;
			this.rbImpulse.TabStop = true;
			this.rbImpulse.Text = "Impulse";
			this.rbImpulse.UseVisualStyleBackColor = true;
			this.rbImpulse.CheckedChanged += new System.EventHandler(this.rbImpulse_CheckedChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.rbFreq);
			this.groupBox5.Controls.Add(this.rbTime);
			this.groupBox5.Location = new System.Drawing.Point(561, 125);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(176, 43);
			this.groupBox5.TabIndex = 19;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Display Domain";
			// 
			// rbFreq
			// 
			this.rbFreq.AutoSize = true;
			this.rbFreq.Location = new System.Drawing.Point(95, 19);
			this.rbFreq.Name = "rbFreq";
			this.rbFreq.Size = new System.Drawing.Size(46, 17);
			this.rbFreq.TabIndex = 8;
			this.rbFreq.Text = "Freq";
			this.rbFreq.UseVisualStyleBackColor = true;
			this.rbFreq.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
			// 
			// rbTime
			// 
			this.rbTime.AutoSize = true;
			this.rbTime.Checked = true;
			this.rbTime.Location = new System.Drawing.Point(12, 19);
			this.rbTime.Name = "rbTime";
			this.rbTime.Size = new System.Drawing.Size(48, 17);
			this.rbTime.TabIndex = 7;
			this.rbTime.TabStop = true;
			this.rbTime.Text = "Time";
			this.rbTime.UseVisualStyleBackColor = true;
			this.rbTime.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
			// 
			// btnRecalc
			// 
			this.btnRecalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRecalc.Location = new System.Drawing.Point(561, 383);
			this.btnRecalc.Name = "btnRecalc";
			this.btnRecalc.Size = new System.Drawing.Size(176, 33);
			this.btnRecalc.TabIndex = 20;
			this.btnRecalc.Text = "Redraw";
			this.btnRecalc.UseVisualStyleBackColor = true;
			this.btnRecalc.Click += new System.EventHandler(this.btnRecalc_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.cmbFilterType);
			this.groupBox2.Location = new System.Drawing.Point(561, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(176, 47);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter Type";
			// 
			// cmbFilterType
			// 
			this.cmbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFilterType.FormattingEnabled = true;
			this.cmbFilterType.Items.AddRange(new object[] {
            "Low IIR Butterwort",
            "Low FIR Butterwort",
            "Low FIR Gauss",
            "High IIR Butterwort",
            "High FIR Butterwort",
            "High FIR Gauss",
            "Winer"});
			this.cmbFilterType.Location = new System.Drawing.Point(12, 19);
			this.cmbFilterType.Name = "cmbFilterType";
			this.cmbFilterType.Size = new System.Drawing.Size(149, 21);
			this.cmbFilterType.TabIndex = 22;
			this.cmbFilterType.SelectedIndexChanged += new System.EventHandler(this.cmbFilterType_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(749, 437);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btnRecalc);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.zedGraphControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbGaussInterference;
		private System.Windows.Forms.RadioButton rbImpulse;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rbFreq;
		private System.Windows.Forms.RadioButton rbTime;
		private System.Windows.Forms.Button btnRecalc;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cmbFilterType;
	}
}

