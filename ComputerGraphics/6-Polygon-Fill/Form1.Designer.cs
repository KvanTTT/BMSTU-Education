namespace PolygonFill
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
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tbDelay = new System.Windows.Forms.TrackBar();
			this.cbContour = new System.Windows.Forms.CheckBox();
			this.cbDelay = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.udY = new System.Windows.Forms.NumericUpDown();
			this.udX = new System.Windows.Forms.NumericUpDown();
			this.udN = new System.Windows.Forms.NumericUpDown();
			this.button6 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.button5 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.gbMethod = new System.Windows.Forms.GroupBox();
			this.radioButton6 = new System.Windows.Forms.RadioButton();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pnlLine = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.pnlBackground = new System.Windows.Forms.Panel();
			this.pnlFill = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbDelay)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.udY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udN)).BeginInit();
			this.gbMethod.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.PowderBlue;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(737, 611);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Lavender;
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.gbMethod);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.groupBox1.Location = new System.Drawing.Point(743, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(287, 611);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.tbDelay);
			this.groupBox4.Controls.Add(this.cbContour);
			this.groupBox4.Controls.Add(this.cbDelay);
			this.groupBox4.Location = new System.Drawing.Point(10, 362);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(264, 79);
			this.groupBox4.TabIndex = 35;
			this.groupBox4.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(238, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(19, 13);
			this.label3.TabIndex = 37;
			this.label3.Text = "50";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(113, 45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 13);
			this.label1.TabIndex = 36;
			this.label1.Text = "1";
			// 
			// tbDelay
			// 
			this.tbDelay.AutoSize = false;
			this.tbDelay.Location = new System.Drawing.Point(106, 17);
			this.tbDelay.Maximum = 50;
			this.tbDelay.Minimum = 1;
			this.tbDelay.Name = "tbDelay";
			this.tbDelay.Size = new System.Drawing.Size(151, 20);
			this.tbDelay.TabIndex = 35;
			this.tbDelay.Value = 1;
			// 
			// cbContour
			// 
			this.cbContour.AutoSize = true;
			this.cbContour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbContour.Location = new System.Drawing.Point(8, 45);
			this.cbContour.Name = "cbContour";
			this.cbContour.Size = new System.Drawing.Size(74, 20);
			this.cbContour.TabIndex = 34;
			this.cbContour.Text = "Контур";
			this.cbContour.UseVisualStyleBackColor = true;
			// 
			// cbDelay
			// 
			this.cbDelay.AutoSize = true;
			this.cbDelay.Checked = true;
			this.cbDelay.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbDelay.Location = new System.Drawing.Point(8, 17);
			this.cbDelay.Name = "cbDelay";
			this.cbDelay.Size = new System.Drawing.Size(92, 20);
			this.cbDelay.TabIndex = 33;
			this.cbDelay.Text = "Задержка";
			this.cbDelay.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.udY);
			this.groupBox3.Controls.Add(this.udX);
			this.groupBox3.Controls.Add(this.udN);
			this.groupBox3.Controls.Add(this.button6);
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.button5);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.ForeColor = System.Drawing.Color.MidnightBlue;
			this.groupBox3.Location = new System.Drawing.Point(10, 264);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(264, 92);
			this.groupBox3.TabIndex = 34;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Конструирование";
			// 
			// udY
			// 
			this.udY.Location = new System.Drawing.Point(102, 25);
			this.udY.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.udY.Name = "udY";
			this.udY.Size = new System.Drawing.Size(51, 22);
			this.udY.TabIndex = 33;
			this.udY.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
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
			// udN
			// 
			this.udN.Location = new System.Drawing.Point(26, 56);
			this.udN.Maximum = new decimal(new int[] {
			10000,
			0,
			0,
			0});
			this.udN.Minimum = new decimal(new int[] {
			3,
			0,
			0,
			0});
			this.udN.Name = "udN";
			this.udN.Size = new System.Drawing.Size(70, 22);
			this.udN.TabIndex = 31;
			this.udN.Value = new decimal(new int[] {
			3,
			0,
			0,
			0});
			// 
			// button6
			// 
			this.button6.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button6.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button6.Location = new System.Drawing.Point(102, 53);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(156, 28);
			this.button6.TabIndex = 30;
			this.button6.Text = "Генерировать";
			this.button6.UseVisualStyleBackColor = false;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 58);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(18, 16);
			this.label7.TabIndex = 29;
			this.label7.Text = "N";
			// 
			// button5
			// 
			this.button5.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button5.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button5.Location = new System.Drawing.Point(168, 20);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(90, 28);
			this.button5.TabIndex = 27;
			this.button5.Text = "Добавить";
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(84, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 16);
			this.label5.TabIndex = 3;
			this.label5.Text = "Y";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(16, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "X";
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.SlateBlue;
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button4.ForeColor = System.Drawing.Color.AliceBlue;
			this.button4.Location = new System.Drawing.Point(10, 573);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(263, 32);
			this.button4.TabIndex = 33;
			this.button4.Text = "Диаграмма";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// gbMethod
			// 
			this.gbMethod.Controls.Add(this.radioButton6);
			this.gbMethod.Controls.Add(this.radioButton5);
			this.gbMethod.Controls.Add(this.radioButton4);
			this.gbMethod.Controls.Add(this.radioButton3);
			this.gbMethod.Controls.Add(this.radioButton2);
			this.gbMethod.Controls.Add(this.radioButton1);
			this.gbMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbMethod.ForeColor = System.Drawing.Color.MidnightBlue;
			this.gbMethod.Location = new System.Drawing.Point(10, 104);
			this.gbMethod.Name = "gbMethod";
			this.gbMethod.Size = new System.Drawing.Size(264, 154);
			this.gbMethod.TabIndex = 27;
			this.gbMethod.TabStop = false;
			this.gbMethod.Text = "Алгоритм заливки";
			// 
			// radioButton6
			// 
			this.radioButton6.AutoSize = true;
			this.radioButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton6.ForeColor = System.Drawing.Color.Navy;
			this.radioButton6.Location = new System.Drawing.Point(11, 124);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(152, 19);
			this.radioButton6.TabIndex = 6;
			this.radioButton6.Text = "Построчная затравка";
			this.radioButton6.UseVisualStyleBackColor = true;
			// 
			// radioButton5
			// 
			this.radioButton5.AutoSize = true;
			this.radioButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton5.ForeColor = System.Drawing.Color.Navy;
			this.radioButton5.Location = new System.Drawing.Point(11, 102);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(132, 19);
			this.radioButton5.TabIndex = 5;
			this.radioButton5.Text = "Простая затравка";
			this.radioButton5.UseVisualStyleBackColor = true;
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton4.ForeColor = System.Drawing.Color.Navy;
			this.radioButton4.Location = new System.Drawing.Point(11, 81);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(162, 19);
			this.radioButton4.TabIndex = 3;
			this.radioButton4.Text = "Список ребер с флагом";
			this.radioButton4.UseVisualStyleBackColor = true;
			// 
			// radioButton3
			// 
			this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton3.ForeColor = System.Drawing.Color.Navy;
			this.radioButton3.Location = new System.Drawing.Point(11, 58);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(191, 21);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.Text = "По ребрам с перегородкой";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton2.ForeColor = System.Drawing.Color.Navy;
			this.radioButton2.Location = new System.Drawing.Point(11, 35);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(162, 25);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "Заполнение по ребрам";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton1.ForeColor = System.Drawing.Color.Navy;
			this.radioButton1.Location = new System.Drawing.Point(11, 16);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(219, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "С упорядоченным списком ребер";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button3.Location = new System.Drawing.Point(10, 447);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(264, 32);
			this.button3.TabIndex = 26;
			this.button3.Text = "Залить";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.pnlLine);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.pnlBackground);
			this.groupBox2.Controls.Add(this.pnlFill);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox2.ForeColor = System.Drawing.Color.MidnightBlue;
			this.groupBox2.Location = new System.Drawing.Point(10, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(264, 86);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Цвета";
			// 
			// pnlLine
			// 
			this.pnlLine.BackColor = System.Drawing.Color.MidnightBlue;
			this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pnlLine.Location = new System.Drawing.Point(91, 25);
			this.pnlLine.Name = "pnlLine";
			this.pnlLine.Size = new System.Drawing.Size(22, 22);
			this.pnlLine.TabIndex = 28;
			this.pnlLine.Click += new System.EventHandler(this.pnlLine_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.Color.Navy;
			this.label2.Location = new System.Drawing.Point(15, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 16);
			this.label2.TabIndex = 29;
			this.label2.Text = "Граница";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.ForeColor = System.Drawing.Color.Navy;
			this.label13.Location = new System.Drawing.Point(127, 37);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(35, 16);
			this.label13.TabIndex = 27;
			this.label13.Text = "Фон";
			// 
			// pnlBackground
			// 
			this.pnlBackground.BackColor = System.Drawing.Color.PowderBlue;
			this.pnlBackground.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pnlBackground.Location = new System.Drawing.Point(168, 37);
			this.pnlBackground.Name = "pnlBackground";
			this.pnlBackground.Size = new System.Drawing.Size(22, 22);
			this.pnlBackground.TabIndex = 25;
			this.pnlBackground.Click += new System.EventHandler(this.pnlLine_Click);
			// 
			// pnlFill
			// 
			this.pnlFill.BackColor = System.Drawing.Color.MediumVioletRed;
			this.pnlFill.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlFill.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pnlFill.Location = new System.Drawing.Point(91, 54);
			this.pnlFill.Name = "pnlFill";
			this.pnlFill.Size = new System.Drawing.Size(22, 22);
			this.pnlFill.TabIndex = 24;
			this.pnlFill.Click += new System.EventHandler(this.pnlLine_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.Color.Navy;
			this.label6.Location = new System.Drawing.Point(15, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 16);
			this.label6.TabIndex = 26;
			this.label6.Text = "Заливка";
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button2.Location = new System.Drawing.Point(9, 485);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(264, 32);
			this.button2.TabIndex = 2;
			this.button2.Text = "Очистить заливку";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.ForeColor = System.Drawing.Color.MidnightBlue;
			this.button1.Location = new System.Drawing.Point(9, 523);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(264, 32);
			this.button1.TabIndex = 1;
			this.button1.Text = "Удалить все";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1030, 611);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Заливка областей";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbDelay)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.udY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udN)).EndInit();
			this.gbMethod.ResumeLayout(false);
			this.gbMethod.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel pnlLine;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel pnlBackground;
		private System.Windows.Forms.Panel pnlFill;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox gbMethod;
		private System.Windows.Forms.RadioButton radioButton6;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown udY;
		private System.Windows.Forms.NumericUpDown udX;
		private System.Windows.Forms.NumericUpDown udN;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar tbDelay;
		private System.Windows.Forms.CheckBox cbContour;
		private System.Windows.Forms.CheckBox cbDelay;
	}
}

