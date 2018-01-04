namespace Proj3_Integral
{
    partial class frmIntegral
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
            this.cmbIntMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbT0 = new System.Windows.Forms.TextBox();
            this.tbTw = new System.Windows.Forms.TextBox();
            this.tbN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbT_0 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.lblAnswer2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.UpDownNTrapez = new System.Windows.Forms.NumericUpDown();
            this.tbEps1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbEps = new System.Windows.Forms.TextBox();
            this.UpDownNSimpson = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.UpDownNGauss = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAnswer3 = new System.Windows.Forms.Label();
            this.tbq = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbh = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNTrapez)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNSimpson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNGauss)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(358, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Метод интегрирования";
            this.label1.Visible = false;
            // 
            // cmbIntMethod
            // 
            this.cmbIntMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIntMethod.FormattingEnabled = true;
            this.cmbIntMethod.Items.AddRange(new object[] {
            "Трапеции",
            "Гаусс"});
            this.cmbIntMethod.Location = new System.Drawing.Point(361, 191);
            this.cmbIntMethod.Name = "cmbIntMethod";
            this.cmbIntMethod.Size = new System.Drawing.Size(121, 21);
            this.cmbIntMethod.TabIndex = 1;
            this.cmbIntMethod.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "T0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tw";
            // 
            // tbT0
            // 
            this.tbT0.Location = new System.Drawing.Point(102, 79);
            this.tbT0.Name = "tbT0";
            this.tbT0.Size = new System.Drawing.Size(121, 20);
            this.tbT0.TabIndex = 4;
            this.tbT0.Text = "8000";
            // 
            // tbTw
            // 
            this.tbTw.Location = new System.Drawing.Point(102, 105);
            this.tbTw.Name = "tbTw";
            this.tbTw.Size = new System.Drawing.Size(121, 20);
            this.tbTw.TabIndex = 5;
            this.tbTw.Text = "2000";
            // 
            // tbN
            // 
            this.tbN.Location = new System.Drawing.Point(102, 131);
            this.tbN.Name = "tbN";
            this.tbN.Size = new System.Drawing.Size(121, 20);
            this.tbN.TabIndex = 7;
            this.tbN.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "n";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "Рассчитать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbP
            // 
            this.tbP.Location = new System.Drawing.Point(102, 27);
            this.tbP.Name = "tbP";
            this.tbP.Size = new System.Drawing.Size(121, 20);
            this.tbP.TabIndex = 9;
            this.tbP.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "p^0";
            // 
            // tbT_0
            // 
            this.tbT_0.Location = new System.Drawing.Point(102, 53);
            this.tbT_0.Name = "tbT_0";
            this.tbT_0.Size = new System.Drawing.Size(121, 20);
            this.tbT_0.TabIndex = 12;
            this.tbT_0.Text = "6000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "T^0";
            // 
            // lblAnswer
            // 
            this.lblAnswer.AutoSize = true;
            this.lblAnswer.Location = new System.Drawing.Point(143, 178);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(80, 13);
            this.lblAnswer.TabIndex = 13;
            this.lblAnswer.Text = "Trapezium: p = ";
            // 
            // lblAnswer2
            // 
            this.lblAnswer2.AutoSize = true;
            this.lblAnswer2.Location = new System.Drawing.Point(162, 225);
            this.lblAnswer2.Name = "lblAnswer2";
            this.lblAnswer2.Size = new System.Drawing.Size(61, 13);
            this.lblAnswer2.TabIndex = 14;
            this.lblAnswer2.Text = "Gauss: p = ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "N Trapez";
            // 
            // UpDownNTrapez
            // 
            this.UpDownNTrapez.Location = new System.Drawing.Point(344, 23);
            this.UpDownNTrapez.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.UpDownNTrapez.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownNTrapez.Name = "UpDownNTrapez";
            this.UpDownNTrapez.Size = new System.Drawing.Size(120, 20);
            this.UpDownNTrapez.TabIndex = 16;
            this.UpDownNTrapez.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpDownNTrapez.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // tbEps1
            // 
            this.tbEps1.Location = new System.Drawing.Point(343, 131);
            this.tbEps1.Name = "tbEps1";
            this.tbEps1.Size = new System.Drawing.Size(121, 20);
            this.tbEps1.TabIndex = 20;
            this.tbEps1.Text = "1E-9";
            this.tbEps1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(295, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Eps1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(295, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Eps";
            // 
            // tbEps
            // 
            this.tbEps.Location = new System.Drawing.Point(343, 105);
            this.tbEps.Name = "tbEps";
            this.tbEps.Size = new System.Drawing.Size(121, 20);
            this.tbEps.TabIndex = 17;
            this.tbEps.Text = "1E-9";
            this.tbEps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UpDownNSimpson
            // 
            this.UpDownNSimpson.Location = new System.Drawing.Point(344, 49);
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
            this.UpDownNSimpson.Size = new System.Drawing.Size(120, 20);
            this.UpDownNSimpson.TabIndex = 22;
            this.UpDownNSimpson.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpDownNSimpson.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "N Simpson";
            // 
            // UpDownNGauss
            // 
            this.UpDownNGauss.Location = new System.Drawing.Point(343, 75);
            this.UpDownNGauss.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDownNGauss.Name = "UpDownNGauss";
            this.UpDownNGauss.Size = new System.Drawing.Size(120, 20);
            this.UpDownNGauss.TabIndex = 24;
            this.UpDownNGauss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpDownNGauss.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(275, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "N Gauss";
            // 
            // lblAnswer3
            // 
            this.lblAnswer3.AutoSize = true;
            this.lblAnswer3.Location = new System.Drawing.Point(152, 202);
            this.lblAnswer3.Name = "lblAnswer3";
            this.lblAnswer3.Size = new System.Drawing.Size(71, 13);
            this.lblAnswer3.TabIndex = 25;
            this.lblAnswer3.Text = "Simpson: p = ";
            // 
            // tbq
            // 
            this.tbq.Location = new System.Drawing.Point(57, 301);
            this.tbq.Name = "tbq";
            this.tbq.Size = new System.Drawing.Size(121, 20);
            this.tbq.TabIndex = 29;
            this.tbq.Text = "0,1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 301);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "q";
            // 
            // tbh
            // 
            this.tbh.Location = new System.Drawing.Point(57, 270);
            this.tbh.Name = "tbh";
            this.tbh.Size = new System.Drawing.Size(121, 20);
            this.tbh.TabIndex = 27;
            this.tbh.Text = "0,05";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 273);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "h";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Eitken";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(202, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 33);
            this.button2.TabIndex = 31;
            this.button2.Text = "Рассчитать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(340, 277);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 32;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(340, 308);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(0, 13);
            this.label16.TabIndex = 33;
            // 
            // frmIntegral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 348);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbq);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbh);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblAnswer3);
            this.Controls.Add(this.UpDownNGauss);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.UpDownNSimpson);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbEps1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbEps);
            this.Controls.Add(this.UpDownNTrapez);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblAnswer2);
            this.Controls.Add(this.lblAnswer);
            this.Controls.Add(this.tbT_0);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbTw);
            this.Controls.Add(this.tbT0);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbIntMethod);
            this.Controls.Add(this.label1);
            this.Name = "frmIntegral";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNTrapez)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNSimpson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownNGauss)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIntMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbT0;
        private System.Windows.Forms.TextBox tbTw;
        private System.Windows.Forms.TextBox tbN;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbT_0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.Label lblAnswer2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown UpDownNTrapez;
        private System.Windows.Forms.TextBox tbEps1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbEps;
        private System.Windows.Forms.NumericUpDown UpDownNSimpson;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown UpDownNGauss;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAnswer3;
        private System.Windows.Forms.TextBox tbq;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbh;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}

