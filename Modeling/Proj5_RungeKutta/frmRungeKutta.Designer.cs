namespace Proj5_RungeKutta
{
    partial class frmRungeKutta
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
            this.zgc = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbGir = new System.Windows.Forms.RadioButton();
            this.rbTrapezium = new System.Windows.Forms.RadioButton();
            this.rbRungeCutta4 = new System.Windows.Forms.RadioButton();
            this.rbRungeCutta2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UpDownNSimpson = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLe = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbTw = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbDt = new System.Windows.Forms.TextBox();
            this.tbEndT = new System.Windows.Forms.TextBox();
            this.tbStartT = new System.Windows.Forms.TextBox();
            this.tbAlpha = new System.Windows.Forms.TextBox();
            this.tbCk = new System.Windows.Forms.TextBox();
            this.tbLk = new System.Windows.Forms.TextBox();
            this.tbRk = new System.Windows.Forms.TextBox();
            this.tbIo = new System.Windows.Forms.TextBox();
            this.tbUo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbUt = new System.Windows.Forms.CheckBox();
            this.cbIt = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNSimpson)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.zgc.Size = new System.Drawing.Size(767, 593);
            this.zgc.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(785, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(229, 617);
            this.panel1.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 33);
            this.button1.TabIndex = 25;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbGir);
            this.groupBox2.Controls.Add(this.rbTrapezium);
            this.groupBox2.Controls.Add(this.rbRungeCutta4);
            this.groupBox2.Controls.Add(this.rbRungeCutta2);
            this.groupBox2.Location = new System.Drawing.Point(3, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 101);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solve Method";
            // 
            // rbGir
            // 
            this.rbGir.AutoSize = true;
            this.rbGir.Location = new System.Drawing.Point(26, 100);
            this.rbGir.Name = "rbGir";
            this.rbGir.Size = new System.Drawing.Size(38, 17);
            this.rbGir.TabIndex = 3;
            this.rbGir.Text = "Gir";
            this.rbGir.UseVisualStyleBackColor = true;
            this.rbGir.Visible = false;
            // 
            // rbTrapezium
            // 
            this.rbTrapezium.AutoSize = true;
            this.rbTrapezium.Location = new System.Drawing.Point(26, 77);
            this.rbTrapezium.Name = "rbTrapezium";
            this.rbTrapezium.Size = new System.Drawing.Size(74, 17);
            this.rbTrapezium.TabIndex = 2;
            this.rbTrapezium.Text = "Trapezium";
            this.rbTrapezium.UseVisualStyleBackColor = true;
            // 
            // rbRungeCutta4
            // 
            this.rbRungeCutta4.AutoSize = true;
            this.rbRungeCutta4.Location = new System.Drawing.Point(26, 54);
            this.rbRungeCutta4.Name = "rbRungeCutta4";
            this.rbRungeCutta4.Size = new System.Drawing.Size(94, 17);
            this.rbRungeCutta4.TabIndex = 1;
            this.rbRungeCutta4.Text = "Runge-Kutta 4";
            this.rbRungeCutta4.UseVisualStyleBackColor = true;
            // 
            // rbRungeCutta2
            // 
            this.rbRungeCutta2.AutoSize = true;
            this.rbRungeCutta2.Checked = true;
            this.rbRungeCutta2.Location = new System.Drawing.Point(26, 31);
            this.rbRungeCutta2.Name = "rbRungeCutta2";
            this.rbRungeCutta2.Size = new System.Drawing.Size(94, 17);
            this.rbRungeCutta2.TabIndex = 0;
            this.rbRungeCutta2.TabStop = true;
            this.rbRungeCutta2.Text = "Runge-Kutta 2";
            this.rbRungeCutta2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UpDownNSimpson);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbLe);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbTw);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbDt);
            this.groupBox1.Controls.Add(this.tbEndT);
            this.groupBox1.Controls.Add(this.tbStartT);
            this.groupBox1.Controls.Add(this.tbAlpha);
            this.groupBox1.Controls.Add(this.tbCk);
            this.groupBox1.Controls.Add(this.tbLk);
            this.groupBox1.Controls.Add(this.tbRk);
            this.groupBox1.Controls.Add(this.tbIo);
            this.groupBox1.Controls.Add(this.tbUo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 304);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solve Method";
            // 
            // UpDownNSimpson
            // 
            this.UpDownNSimpson.Location = new System.Drawing.Point(77, 271);
            this.UpDownNSimpson.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.UpDownNSimpson.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownNSimpson.Name = "UpDownNSimpson";
            this.UpDownNSimpson.Size = new System.Drawing.Size(116, 20);
            this.UpDownNSimpson.TabIndex = 59;
            this.UpDownNSimpson.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpDownNSimpson.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 273);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 58;
            this.label11.Text = "N Integr";
            // 
            // tbLe
            // 
            this.tbLe.Location = new System.Drawing.Point(77, 231);
            this.tbLe.Name = "tbLe";
            this.tbLe.Size = new System.Drawing.Size(116, 20);
            this.tbLe.TabIndex = 57;
            this.tbLe.Text = "10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Le";
            // 
            // tbTw
            // 
            this.tbTw.Location = new System.Drawing.Point(77, 205);
            this.tbTw.Name = "tbTw";
            this.tbTw.Size = new System.Drawing.Size(116, 20);
            this.tbTw.TabIndex = 55;
            this.tbTw.Text = "2000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Tw";
            // 
            // tbDt
            // 
            this.tbDt.Location = new System.Drawing.Point(77, 179);
            this.tbDt.Name = "tbDt";
            this.tbDt.Size = new System.Drawing.Size(116, 20);
            this.tbDt.TabIndex = 51;
            this.tbDt.Text = "1E-6";
            // 
            // tbEndT
            // 
            this.tbEndT.Location = new System.Drawing.Point(77, 153);
            this.tbEndT.Name = "tbEndT";
            this.tbEndT.Size = new System.Drawing.Size(116, 20);
            this.tbEndT.TabIndex = 50;
            this.tbEndT.Text = "3E-4";
            // 
            // tbStartT
            // 
            this.tbStartT.Location = new System.Drawing.Point(77, 153);
            this.tbStartT.Name = "tbStartT";
            this.tbStartT.Size = new System.Drawing.Size(58, 20);
            this.tbStartT.TabIndex = 49;
            this.tbStartT.Text = "0";
            this.tbStartT.Visible = false;
            // 
            // tbAlpha
            // 
            this.tbAlpha.Location = new System.Drawing.Point(77, 153);
            this.tbAlpha.Name = "tbAlpha";
            this.tbAlpha.Size = new System.Drawing.Size(116, 20);
            this.tbAlpha.TabIndex = 48;
            this.tbAlpha.Text = "1";
            this.tbAlpha.Visible = false;
            // 
            // tbCk
            // 
            this.tbCk.Location = new System.Drawing.Point(77, 71);
            this.tbCk.Name = "tbCk";
            this.tbCk.Size = new System.Drawing.Size(116, 20);
            this.tbCk.TabIndex = 47;
            this.tbCk.Text = "150E-6";
            // 
            // tbLk
            // 
            this.tbLk.Location = new System.Drawing.Point(77, 45);
            this.tbLk.Name = "tbLk";
            this.tbLk.Size = new System.Drawing.Size(116, 20);
            this.tbLk.TabIndex = 46;
            this.tbLk.Text = "60E-6";
            // 
            // tbRk
            // 
            this.tbRk.Location = new System.Drawing.Point(77, 19);
            this.tbRk.Name = "tbRk";
            this.tbRk.Size = new System.Drawing.Size(116, 20);
            this.tbRk.TabIndex = 45;
            this.tbRk.Text = "1";
            // 
            // tbIo
            // 
            this.tbIo.Location = new System.Drawing.Point(77, 123);
            this.tbIo.Name = "tbIo";
            this.tbIo.Size = new System.Drawing.Size(116, 20);
            this.tbIo.TabIndex = 44;
            this.tbIo.Text = "0,5";
            // 
            // tbUo
            // 
            this.tbUo.Location = new System.Drawing.Point(77, 97);
            this.tbUo.Name = "tbUo";
            this.tbUo.Size = new System.Drawing.Size(116, 20);
            this.tbUo.TabIndex = 43;
            this.tbUo.Text = "3000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "dt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Interval";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Alpha";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Ck";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Lk";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Rk";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Io";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Uo";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbIt);
            this.panel2.Controls.Add(this.cbUt);
            this.panel2.Location = new System.Drawing.Point(3, 420);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 48);
            this.panel2.TabIndex = 26;
            // 
            // cbUt
            // 
            this.cbUt.AutoSize = true;
            this.cbUt.Checked = true;
            this.cbUt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUt.Location = new System.Drawing.Point(26, 16);
            this.cbUt.Name = "cbUt";
            this.cbUt.Size = new System.Drawing.Size(43, 17);
            this.cbUt.TabIndex = 4;
            this.cbUt.Text = "U(t)";
            this.cbUt.UseVisualStyleBackColor = true;
            // 
            // cbIt
            // 
            this.cbIt.AutoSize = true;
            this.cbIt.Location = new System.Drawing.Point(115, 16);
            this.cbIt.Name = "cbIt";
            this.cbIt.Size = new System.Drawing.Size(38, 17);
            this.cbIt.TabIndex = 5;
            this.cbIt.Text = "I(t)";
            this.cbIt.UseVisualStyleBackColor = true;
            // 
            // frmRungeKutta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 617);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.zgc);
            this.Name = "frmRungeKutta";
            this.Text = "Runge-Kutta";
            this.Load += new System.EventHandler(this.frmRungeKutta_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNSimpson)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbGir;
        private System.Windows.Forms.RadioButton rbTrapezium;
        private System.Windows.Forms.RadioButton rbRungeCutta4;
        private System.Windows.Forms.RadioButton rbRungeCutta2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown UpDownNSimpson;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbTw;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbDt;
        private System.Windows.Forms.TextBox tbEndT;
        private System.Windows.Forms.TextBox tbStartT;
        private System.Windows.Forms.TextBox tbAlpha;
        private System.Windows.Forms.TextBox tbCk;
        private System.Windows.Forms.TextBox tbLk;
        private System.Windows.Forms.TextBox tbRk;
        private System.Windows.Forms.TextBox tbIo;
        private System.Windows.Forms.TextBox tbUo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbIt;
        private System.Windows.Forms.CheckBox cbUt;
    }
}

