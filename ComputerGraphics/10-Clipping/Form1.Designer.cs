namespace Clipping
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.lblSelfintersect = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblConvex = new System.Windows.Forms.Label();
			this.lblBypass = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.udY = new System.Windows.Forms.NumericUpDown();
			this.udX = new System.Windows.Forms.NumericUpDown();
			this.button5 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.pnlCuttingLine = new System.Windows.Forms.Panel();
			this.pnlCuttingRect = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pnlBackground = new System.Windows.Forms.Panel();
			this.pnlLine = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbClipper = new System.Windows.Forms.RadioButton();
			this.rbIntercept = new System.Windows.Forms.RadioButton();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udX)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.DarkCyan;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(758, 660);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.MediumAquamarine;
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.groupBox5);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.groupBox1.Location = new System.Drawing.Point(764, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 660);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.LightGreen;
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button4.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button4.Location = new System.Drawing.Point(11, 494);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(247, 32);
			this.button4.TabIndex = 40;
			this.button4.Text = "Решить";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.LightGreen;
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button3.Location = new System.Drawing.Point(11, 532);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(247, 32);
			this.button3.TabIndex = 39;
			this.button3.Text = "Решить пошагово";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.LightGreen;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button2.Location = new System.Drawing.Point(10, 570);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(247, 32);
			this.button2.TabIndex = 38;
			this.button2.Text = "Удалить решение";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightGreen;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button1.Location = new System.Drawing.Point(10, 608);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(247, 32);
			this.button1.TabIndex = 37;
			this.button1.Text = "Удалить все";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.BackColor = System.Drawing.Color.DarkCyan;
			this.groupBox5.Controls.Add(this.lblSelfintersect);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Controls.Add(this.lblConvex);
			this.groupBox5.Controls.Add(this.lblBypass);
			this.groupBox5.Controls.Add(this.label11);
			this.groupBox5.Controls.Add(this.label12);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox5.ForeColor = System.Drawing.Color.LemonChiffon;
			this.groupBox5.Location = new System.Drawing.Point(10, 231);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(247, 81);
			this.groupBox5.TabIndex = 36;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Статистика";
			// 
			// lblSelfintersect
			// 
			this.lblSelfintersect.AutoSize = true;
			this.lblSelfintersect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblSelfintersect.Location = new System.Drawing.Point(140, 53);
			this.lblSelfintersect.Name = "lblSelfintersect";
			this.lblSelfintersect.Size = new System.Drawing.Size(19, 15);
			this.lblSelfintersect.TabIndex = 7;
			this.lblSelfintersect.Text = "---";
			this.lblSelfintersect.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(18, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 15);
			this.label2.TabIndex = 6;
			this.label2.Text = "Самопересечение:";
			this.label2.Visible = false;
			// 
			// lblConvex
			// 
			this.lblConvex.AutoSize = true;
			this.lblConvex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblConvex.Location = new System.Drawing.Point(98, 37);
			this.lblConvex.Name = "lblConvex";
			this.lblConvex.Size = new System.Drawing.Size(19, 15);
			this.lblConvex.TabIndex = 5;
			this.lblConvex.Text = "---";
			// 
			// lblBypass
			// 
			this.lblBypass.AutoSize = true;
			this.lblBypass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblBypass.Location = new System.Drawing.Point(98, 21);
			this.lblBypass.Name = "lblBypass";
			this.lblBypass.Size = new System.Drawing.Size(19, 15);
			this.lblBypass.TabIndex = 4;
			this.lblBypass.Text = "---";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label11.Location = new System.Drawing.Point(103, 37);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(0, 15);
			this.label11.TabIndex = 3;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label12.Location = new System.Drawing.Point(103, 21);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(0, 15);
			this.label12.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(18, 37);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(79, 15);
			this.label9.TabIndex = 1;
			this.label9.Text = "Выпуклость:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(18, 21);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "Обход:";
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.DarkCyan;
			this.groupBox3.Controls.Add(this.udY);
			this.groupBox3.Controls.Add(this.udX);
			this.groupBox3.Controls.Add(this.button5);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.Color.LemonChiffon;
			this.groupBox3.Location = new System.Drawing.Point(11, 231);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(247, 92);
			this.groupBox3.TabIndex = 35;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Конструирование";
			this.groupBox3.Visible = false;
			// 
			// udY
			// 
			this.udY.Location = new System.Drawing.Point(126, 25);
			this.udY.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.udY.Name = "udY";
			this.udY.Size = new System.Drawing.Size(51, 22);
			this.udY.TabIndex = 33;
			// 
			// udX
			// 
			this.udX.Location = new System.Drawing.Point(26, 25);
			this.udX.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.udX.Name = "udX";
			this.udX.Size = new System.Drawing.Size(51, 22);
			this.udX.TabIndex = 32;
			// 
			// button5
			// 
			this.button5.BackColor = System.Drawing.Color.LightGreen;
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button5.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button5.Location = new System.Drawing.Point(11, 53);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(168, 28);
			this.button5.TabIndex = 27;
			this.button5.Text = "Добавить";
			this.button5.UseVisualStyleBackColor = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(108, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 16);
			this.label5.TabIndex = 3;
			this.label5.Text = "Y";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(8, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "X";
			// 
			// groupBox4
			// 
			this.groupBox4.BackColor = System.Drawing.Color.DarkCyan;
			this.groupBox4.Controls.Add(this.pnlCuttingLine);
			this.groupBox4.Controls.Add(this.pnlCuttingRect);
			this.groupBox4.Controls.Add(this.label15);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.pnlBackground);
			this.groupBox4.Controls.Add(this.pnlLine);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox4.ForeColor = System.Drawing.Color.LemonChiffon;
			this.groupBox4.Location = new System.Drawing.Point(11, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(247, 117);
			this.groupBox4.TabIndex = 27;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Цвета";
			// 
			// pnlCuttingLine
			// 
			this.pnlCuttingLine.BackColor = System.Drawing.Color.OrangeRed;
			this.pnlCuttingLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlCuttingLine.Location = new System.Drawing.Point(111, 82);
			this.pnlCuttingLine.Name = "pnlCuttingLine";
			this.pnlCuttingLine.Size = new System.Drawing.Size(22, 22);
			this.pnlCuttingLine.TabIndex = 23;
			// 
			// pnlCuttingRect
			// 
			this.pnlCuttingRect.BackColor = System.Drawing.Color.SkyBlue;
			this.pnlCuttingRect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlCuttingRect.Location = new System.Drawing.Point(111, 54);
			this.pnlCuttingRect.Name = "pnlCuttingRect";
			this.pnlCuttingRect.Size = new System.Drawing.Size(22, 22);
			this.pnlCuttingRect.TabIndex = 22;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.ForeColor = System.Drawing.Color.LemonChiffon;
			this.label15.Location = new System.Drawing.Point(14, 82);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(79, 15);
			this.label15.TabIndex = 21;
			this.label15.Text = "Отсеченный";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.ForeColor = System.Drawing.Color.LemonChiffon;
			this.label14.Location = new System.Drawing.Point(14, 54);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(77, 15);
			this.label14.TabIndex = 20;
			this.label14.Text = "Отсекатель";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.Color.LemonChiffon;
			this.label1.Location = new System.Drawing.Point(147, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 15);
			this.label1.TabIndex = 19;
			this.label1.Text = "Фон";
			// 
			// pnlBackground
			// 
			this.pnlBackground.BackColor = System.Drawing.Color.DarkCyan;
			this.pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlBackground.Location = new System.Drawing.Point(193, 54);
			this.pnlBackground.Name = "pnlBackground";
			this.pnlBackground.Size = new System.Drawing.Size(22, 22);
			this.pnlBackground.TabIndex = 2;
			// 
			// pnlLine
			// 
			this.pnlLine.BackColor = System.Drawing.Color.Moccasin;
			this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlLine.Location = new System.Drawing.Point(111, 26);
			this.pnlLine.Name = "pnlLine";
			this.pnlLine.Size = new System.Drawing.Size(22, 22);
			this.pnlLine.TabIndex = 1;
			this.pnlLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlLine_MouseUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.Color.LemonChiffon;
			this.label3.Location = new System.Drawing.Point(14, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(81, 15);
			this.label3.TabIndex = 18;
			this.label3.Text = "Отсекаемый";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.DarkCyan;
			this.groupBox2.Controls.Add(this.rbClipper);
			this.groupBox2.Controls.Add(this.rbIntercept);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.Color.LemonChiffon;
			this.groupBox2.Location = new System.Drawing.Point(11, 135);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(247, 90);
			this.groupBox2.TabIndex = 26;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Мышиный ввод";
			// 
			// rbClipper
			// 
			this.rbClipper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbClipper.Location = new System.Drawing.Point(142, 31);
			this.rbClipper.Name = "rbClipper";
			this.rbClipper.Size = new System.Drawing.Size(95, 42);
			this.rbClipper.TabIndex = 1;
			this.rbClipper.Text = "Отсекатель (выпуклый)";
			this.rbClipper.UseVisualStyleBackColor = true;
			// 
			// rbIntercept
			// 
			this.rbIntercept.Checked = true;
			this.rbIntercept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbIntercept.Location = new System.Drawing.Point(18, 16);
			this.rbIntercept.Name = "rbIntercept";
			this.rbIntercept.Size = new System.Drawing.Size(118, 65);
			this.rbIntercept.TabIndex = 0;
			this.rbIntercept.TabStop = true;
			this.rbIntercept.Text = "Отсекаемый многоугольник";
			this.rbIntercept.UseVisualStyleBackColor = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1028, 660);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.udY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udX)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbClipper;
		private System.Windows.Forms.RadioButton rbIntercept;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.NumericUpDown udY;
		private System.Windows.Forms.NumericUpDown udX;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Panel pnlCuttingLine;
		private System.Windows.Forms.Panel pnlCuttingRect;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pnlBackground;
		private System.Windows.Forms.Panel pnlLine;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label lblSelfintersect;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblConvex;
		private System.Windows.Forms.Label lblBypass;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button button4;
	}
}

