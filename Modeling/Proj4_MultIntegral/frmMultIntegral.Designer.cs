namespace Proj4_MultIntegral
{
    partial class frmMultIntegral
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
            this.udN = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tbTauStart = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbEps1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbEps = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbSeq = new System.Windows.Forms.RadioButton();
            this.rbCells = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udNGauss = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.udYCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.udXCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTauNu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTauFin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.zgc = new ZedGraph.ZedGraphControl();
            ((System.ComponentModel.ISupportInitialize)(this.udN)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNGauss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udYCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udXCount)).BeginInit();
            this.SuspendLayout();
            // 
            // udN
            // 
            this.udN.Location = new System.Drawing.Point(77, 206);
            this.udN.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udN.Name = "udN";
            this.udN.Size = new System.Drawing.Size(116, 20);
            this.udN.TabIndex = 59;
            this.udN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udN.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 208);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "N";
            // 
            // tbTauStart
            // 
            this.tbTauStart.Location = new System.Drawing.Point(77, 29);
            this.tbTauStart.Name = "tbTauStart";
            this.tbTauStart.Size = new System.Drawing.Size(116, 20);
            this.tbTauStart.TabIndex = 45;
            this.tbTauStart.Text = "0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbEps1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.tbEps);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(799, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 659);
            this.panel1.TabIndex = 27;
            // 
            // tbEps1
            // 
            this.tbEps1.Location = new System.Drawing.Point(66, 438);
            this.tbEps1.Name = "tbEps1";
            this.tbEps1.Size = new System.Drawing.Size(121, 20);
            this.tbEps1.TabIndex = 71;
            this.tbEps1.Text = "1E-9";
            this.tbEps1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 438);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 70;
            this.label10.Text = "Eps1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 415);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 69;
            this.label12.Text = "Eps";
            // 
            // tbEps
            // 
            this.tbEps.Location = new System.Drawing.Point(66, 412);
            this.tbEps.Name = "tbEps";
            this.tbEps.Size = new System.Drawing.Size(121, 20);
            this.tbEps.TabIndex = 68;
            this.tbEps.Text = "1E-9";
            this.tbEps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 493);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "=";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 493);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 66;
            this.label7.Text = "τ =";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(66, 464);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 65;
            this.textBox1.Text = "0,9";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 467);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "ε";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 33);
            this.button1.TabIndex = 25;
            this.button1.Text = "Draw && Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSeq);
            this.groupBox2.Controls.Add(this.rbCells);
            this.groupBox2.Location = new System.Drawing.Point(3, 268);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 85);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solve Method";
            // 
            // rbSeq
            // 
            this.rbSeq.AutoSize = true;
            this.rbSeq.Location = new System.Drawing.Point(22, 50);
            this.rbSeq.Name = "rbSeq";
            this.rbSeq.Size = new System.Drawing.Size(81, 17);
            this.rbSeq.TabIndex = 1;
            this.rbSeq.Text = "Seq integral";
            this.rbSeq.UseVisualStyleBackColor = true;
            // 
            // rbCells
            // 
            this.rbCells.AutoSize = true;
            this.rbCells.Checked = true;
            this.rbCells.Location = new System.Drawing.Point(22, 27);
            this.rbCells.Name = "rbCells";
            this.rbCells.Size = new System.Drawing.Size(47, 17);
            this.rbCells.TabIndex = 0;
            this.rbCells.TabStop = true;
            this.rbCells.Text = "Cells";
            this.rbCells.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udNGauss);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.udYCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.udXCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbTauNu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbTauFin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.udN);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbTauStart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 259);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solve Method";
            // 
            // udNGauss
            // 
            this.udNGauss.Location = new System.Drawing.Point(77, 161);
            this.udNGauss.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udNGauss.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udNGauss.Name = "udNGauss";
            this.udNGauss.Size = new System.Drawing.Size(116, 20);
            this.udNGauss.TabIndex = 69;
            this.udNGauss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udNGauss.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 68;
            this.label9.Text = "N Gauss";
            // 
            // udYCount
            // 
            this.udYCount.Location = new System.Drawing.Point(77, 133);
            this.udYCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udYCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udYCount.Name = "udYCount";
            this.udYCount.Size = new System.Drawing.Size(116, 20);
            this.udYCount.TabIndex = 67;
            this.udYCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udYCount.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Y Count";
            // 
            // udXCount
            // 
            this.udXCount.Location = new System.Drawing.Point(77, 107);
            this.udXCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.udXCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udXCount.Name = "udXCount";
            this.udXCount.Size = new System.Drawing.Size(116, 20);
            this.udXCount.TabIndex = 65;
            this.udXCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udXCount.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "X Count";
            // 
            // tbTauNu
            // 
            this.tbTauNu.Location = new System.Drawing.Point(77, 81);
            this.tbTauNu.Name = "tbTauNu";
            this.tbTauNu.Size = new System.Drawing.Size(116, 20);
            this.tbTauNu.TabIndex = 63;
            this.tbTauNu.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(36, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 62;
            this.label2.Text = "τν";
            // 
            // tbTauFin
            // 
            this.tbTauFin.Location = new System.Drawing.Point(77, 55);
            this.tbTauFin.Name = "tbTauFin";
            this.tbTauFin.Size = new System.Drawing.Size(116, 20);
            this.tbTauFin.TabIndex = 61;
            this.tbTauFin.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(36, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 16);
            this.label1.TabIndex = 60;
            this.label1.Text = "τ2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(36, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "τ1";
            // 
            // zgc
            // 
            this.zgc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgc.BackColor = System.Drawing.Color.PaleGreen;
            this.zgc.IsShowCursorValues = true;
            this.zgc.Location = new System.Drawing.Point(12, 12);
            this.zgc.Name = "zgc";
            this.zgc.ScrollGrace = 0;
            this.zgc.ScrollMaxX = 0;
            this.zgc.ScrollMaxY = 0;
            this.zgc.ScrollMaxY2 = 0;
            this.zgc.ScrollMinX = 0;
            this.zgc.ScrollMinY = 0;
            this.zgc.ScrollMinY2 = 0;
            this.zgc.Size = new System.Drawing.Size(781, 635);
            this.zgc.TabIndex = 26;
            // 
            // frmMultIntegral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 659);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.zgc);
            this.Name = "frmMultIntegral";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.udN)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udNGauss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udYCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udXCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown udN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbTauStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbSeq;
        private System.Windows.Forms.RadioButton rbCells;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private ZedGraph.ZedGraphControl zgc;
        private System.Windows.Forms.TextBox tbTauFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTauNu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udYCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udXCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown udNGauss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbEps1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbEps;

    }
}

