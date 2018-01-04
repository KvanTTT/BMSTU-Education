namespace Segment
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
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.panel3 = new System.Windows.Forms.Panel();
			this.button6 = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbMethod = new System.Windows.Forms.ComboBox();
			this.gorupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbYe = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbXe = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbYb = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbXb = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.tbSegLength = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.tbSegNumber = new System.Windows.Forms.TextBox();
			this.pictBox = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbX2 = new System.Windows.Forms.TextBox();
			this.tbX1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.panel3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.gorupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.button6);
			this.panel3.Controls.Add(this.groupBox5);
			this.panel3.Controls.Add(this.groupBox3);
			this.panel3.Controls.Add(this.gorupBox3);
			this.panel3.Controls.Add(this.groupBox2);
			this.panel3.Location = new System.Drawing.Point(616, 5);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(243, 582);
			this.panel3.TabIndex = 8;
			// 
			// button6
			// 
			this.button6.BackColor = System.Drawing.Color.PowderBlue;
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button6.ForeColor = System.Drawing.SystemColors.Highlight;
			this.button6.Location = new System.Drawing.Point(16, 502);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(211, 42);
			this.button6.TabIndex = 4;
			this.button6.Text = "Очистить";
			this.button6.UseVisualStyleBackColor = false;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.button5);
			this.groupBox5.Controls.Add(this.button2);
			this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox5.Location = new System.Drawing.Point(16, 367);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(211, 120);
			this.groupBox5.TabIndex = 3;
			this.groupBox5.TabStop = false;
			// 
			// button5
			// 
			this.button5.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button5.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.button5.Location = new System.Drawing.Point(9, 20);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(190, 40);
			this.button5.TabIndex = 3;
			this.button5.Text = "Строить график ступенчатости";
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.button2.Location = new System.Drawing.Point(9, 71);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(190, 40);
			this.button2.TabIndex = 4;
			this.button2.Text = "Строить диаграму быстродействия";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.panel2);
			this.groupBox3.Controls.Add(this.panel1);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.cmbMethod);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.Location = new System.Drawing.Point(16, 7);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(211, 85);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Метод и цвета";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Location = new System.Drawing.Point(148, 49);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(24, 24);
			this.panel2.TabIndex = 2;
			this.toolTip1.SetToolTip(this.panel2, "Цвет фона");
			this.panel2.Click += new System.EventHandler(this.panel2_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Black;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Location = new System.Drawing.Point(114, 49);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(24, 24);
			this.panel1.TabIndex = 1;
			this.toolTip1.SetToolTip(this.panel1, "Цвет линий");
			this.panel1.Click += new System.EventHandler(this.panel1_Click);
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(40, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 15);
			this.label6.TabIndex = 18;
			this.label6.Text = "Цвета";
			// 
			// cmbMethod
			// 
			this.cmbMethod.BackColor = System.Drawing.Color.AntiqueWhite;
			this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cmbMethod.FormattingEnabled = true;
			this.cmbMethod.Items.AddRange(new object[] {
            "ЦДА",
            "Брезенхем (действительный)",
            "Брезенхем (целочисленный)",
            "Брезенхем с устранением ступенек",
            "Стандартный алгоритм"});
			this.cmbMethod.Location = new System.Drawing.Point(13, 20);
			this.cmbMethod.Name = "cmbMethod";
			this.cmbMethod.Size = new System.Drawing.Size(178, 23);
			this.cmbMethod.TabIndex = 0;
			this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.cmbMethod_SelectedIndexChanged_1);
			this.cmbMethod.SelectionChangeCommitted += new System.EventHandler(this.cmbMethod_SelectionChangeCommitted);
			// 
			// gorupBox3
			// 
			this.gorupBox3.Controls.Add(this.groupBox1);
			this.gorupBox3.Controls.Add(this.button1);
			this.gorupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gorupBox3.Location = new System.Drawing.Point(16, 98);
			this.gorupBox3.Name = "gorupBox3";
			this.gorupBox3.Size = new System.Drawing.Size(211, 137);
			this.gorupBox3.TabIndex = 1;
			this.gorupBox3.TabStop = false;
			this.gorupBox3.Text = "Отрезок";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.tbYe);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tbXe);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbYb);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tbXb);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox1.Location = new System.Drawing.Point(13, 19);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(178, 81);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Координаты";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(91, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(21, 15);
			this.label4.TabIndex = 7;
			this.label4.Text = "Y2";
			// 
			// tbYe
			// 
			this.tbYe.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbYe.Location = new System.Drawing.Point(119, 47);
			this.tbYe.Name = "tbYe";
			this.tbYe.Size = new System.Drawing.Size(45, 21);
			this.tbYe.TabIndex = 3;
			this.tbYe.Text = "100";
			this.tbYe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(91, 23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(22, 15);
			this.label5.TabIndex = 5;
			this.label5.Text = "X2";
			// 
			// tbXe
			// 
			this.tbXe.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbXe.Location = new System.Drawing.Point(119, 17);
			this.tbXe.Name = "tbXe";
			this.tbXe.Size = new System.Drawing.Size(45, 21);
			this.tbXe.TabIndex = 2;
			this.tbXe.Text = "450";
			this.tbXe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(21, 15);
			this.label3.TabIndex = 3;
			this.label3.Text = "Y1";
			// 
			// tbYb
			// 
			this.tbYb.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbYb.Location = new System.Drawing.Point(40, 47);
			this.tbYb.Name = "tbYb";
			this.tbYb.Size = new System.Drawing.Size(45, 21);
			this.tbYb.TabIndex = 1;
			this.tbYb.Text = "256";
			this.tbYb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(22, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "X1";
			// 
			// tbXb
			// 
			this.tbXb.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbXb.Location = new System.Drawing.Point(40, 19);
			this.tbXb.Name = "tbXb";
			this.tbXb.Size = new System.Drawing.Size(45, 21);
			this.tbXb.TabIndex = 0;
			this.tbXb.Text = "301";
			this.tbXb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.button1.Location = new System.Drawing.Point(9, 106);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(185, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Строить отрезок";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.tbSegLength);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.tbSegNumber);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.Location = new System.Drawing.Point(16, 241);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(211, 120);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Пучек отрезков";
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.button3.Location = new System.Drawing.Point(9, 85);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(185, 25);
			this.button3.TabIndex = 2;
			this.button3.Text = "Строить пучек отрезков";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// tbSegLength
			// 
			this.tbSegLength.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbSegLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbSegLength.Location = new System.Drawing.Point(144, 54);
			this.tbSegLength.Name = "tbSegLength";
			this.tbSegLength.Size = new System.Drawing.Size(49, 21);
			this.tbSegLength.TabIndex = 1;
			this.tbSegLength.Text = "150";
			this.tbSegLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(6, 54);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(94, 15);
			this.label9.TabIndex = 24;
			this.label9.Text = "Длина отрезка";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(5, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(133, 15);
			this.label8.TabIndex = 23;
			this.label8.Text = "Количество отрезков";
			// 
			// tbSegNumber
			// 
			this.tbSegNumber.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbSegNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbSegNumber.Location = new System.Drawing.Point(144, 19);
			this.tbSegNumber.Name = "tbSegNumber";
			this.tbSegNumber.Size = new System.Drawing.Size(49, 21);
			this.tbSegNumber.TabIndex = 0;
			this.tbSegNumber.Text = "10";
			this.tbSegNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress);
			// 
			// pictBox
			// 
			this.pictBox.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pictBox.Location = new System.Drawing.Point(8, 5);
			this.pictBox.Name = "pictBox";
			this.pictBox.Size = new System.Drawing.Size(602, 582);
			this.pictBox.TabIndex = 9;
			this.pictBox.TabStop = false;
			this.pictBox.Click += new System.EventHandler(this.pictBox_Click);
			this.pictBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictBox_MouseUp_1);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.tbX2);
			this.groupBox4.Controls.Add(this.tbX1);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.button4);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox4.Location = new System.Drawing.Point(907, 252);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(211, 114);
			this.groupBox4.TabIndex = 22;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "График";
			this.groupBox4.Visible = false;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(111, 53);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(33, 16);
			this.label10.TabIndex = 32;
			this.label10.Text = "X2 =";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(10, 53);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(33, 16);
			this.label7.TabIndex = 31;
			this.label7.Text = "X1 =";
			// 
			// tbX2
			// 
			this.tbX2.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbX2.Location = new System.Drawing.Point(148, 50);
			this.tbX2.Name = "tbX2";
			this.tbX2.Size = new System.Drawing.Size(49, 21);
			this.tbX2.TabIndex = 30;
			this.tbX2.Text = "10";
			// 
			// tbX1
			// 
			this.tbX1.BackColor = System.Drawing.Color.AntiqueWhite;
			this.tbX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbX1.Location = new System.Drawing.Point(49, 50);
			this.tbX1.Name = "tbX1";
			this.tbX1.Size = new System.Drawing.Size(49, 21);
			this.tbX1.TabIndex = 0;
			this.tbX1.Text = "-10";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(10, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 16);
			this.label1.TabIndex = 28;
			this.label1.Text = "Y = X*sin(X)";
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button4.ForeColor = System.Drawing.Color.DarkSlateBlue;
			this.button4.Location = new System.Drawing.Point(13, 80);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(185, 25);
			this.button4.TabIndex = 27;
			this.button4.Text = "Строить график";
			this.button4.UseVisualStyleBackColor = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(185)))), ((int)(((byte)(196)))));
			this.ClientSize = new System.Drawing.Size(867, 599);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.pictBox);
			this.Controls.Add(this.panel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Form1";
			this.Text = "Построение отрезков";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel3.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.gorupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.GroupBox gorupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbYe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbXe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbXb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbSegLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSegNumber;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbX2;
        private System.Windows.Forms.TextBox tbX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
    }
}

