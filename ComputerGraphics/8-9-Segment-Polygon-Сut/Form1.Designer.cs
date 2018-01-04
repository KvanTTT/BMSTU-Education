namespace SegmentPolygonCut
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbConvex = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.edtEdgesCount = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.edtSegmentCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblSelfintersect = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConvex = new System.Windows.Forms.Label();
            this.lblBypass = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbClipper = new System.Windows.Forms.RadioButton();
            this.rbSegment = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlCuttingLine = new System.Windows.Forms.Panel();
            this.pnlCuttingRect = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlBackgroud = new System.Windows.Forms.Panel();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.gorupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbYe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbXe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbXb = new System.Windows.Forms.TextBox();
            this.pictBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbConvexDivide = new System.Windows.Forms.RadioButton();
            this.rbInterlace = new System.Windows.Forms.RadioButton();
            this.cbTriag = new System.Windows.Forms.CheckBox();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEdgesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSegmentCount)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gorupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Location = new System.Drawing.Point(616, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(259, 582);
            this.panel3.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.edtSegmentCount);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.ForeColor = System.Drawing.Color.Indigo;
            this.groupBox4.Location = new System.Drawing.Point(16, 162);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 79);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Конструирование";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // cbConvex
            // 
            this.cbConvex.AutoSize = true;
            this.cbConvex.Checked = true;
            this.cbConvex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbConvex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbConvex.Location = new System.Drawing.Point(934, 271);
            this.cbConvex.Name = "cbConvex";
            this.cbConvex.Size = new System.Drawing.Size(84, 19);
            this.cbConvex.TabIndex = 38;
            this.cbConvex.Text = "Выпуклый";
            this.cbConvex.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Aquamarine;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(924, 296);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(207, 23);
            this.button4.TabIndex = 37;
            this.button4.Text = "Генерировать многоугольник";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // edtEdgesCount
            // 
            this.edtEdgesCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtEdgesCount.Location = new System.Drawing.Point(1082, 251);
            this.edtEdgesCount.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.edtEdgesCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.edtEdgesCount.Name = "edtEdgesCount";
            this.edtEdgesCount.Size = new System.Drawing.Size(46, 21);
            this.edtEdgesCount.TabIndex = 36;
            this.edtEdgesCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.edtEdgesCount.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(930, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 15);
            this.label10.TabIndex = 35;
            this.label10.Text = "Количество ребер";
            // 
            // edtSegmentCount
            // 
            this.edtSegmentCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edtSegmentCount.Location = new System.Drawing.Point(173, 20);
            this.edtSegmentCount.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.edtSegmentCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtSegmentCount.Name = "edtSegmentCount";
            this.edtSegmentCount.Size = new System.Drawing.Size(46, 21);
            this.edtSegmentCount.TabIndex = 34;
            this.edtSegmentCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(21, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Количество отрезков";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Aquamarine;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(12, 47);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(207, 24);
            this.button3.TabIndex = 32;
            this.button3.Text = "Генерировать отрезки";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(29, 450);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(193, 17);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "Показыть отброшенные отрезки";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(38, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(163, 19);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Показывать разбиение";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Aquamarine;
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.button2.Location = new System.Drawing.Point(16, 473);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(228, 30);
            this.button2.TabIndex = 23;
            this.button2.Text = "Удалить все линии";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aquamarine;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.button1.Location = new System.Drawing.Point(16, 509);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 30);
            this.button1.TabIndex = 22;
            this.button1.Text = "Удалить отсекатель";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblSelfintersect);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.lblConvex);
            this.groupBox5.Controls.Add(this.lblBypass);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.ForeColor = System.Drawing.Color.Indigo;
            this.groupBox5.Location = new System.Drawing.Point(16, 246);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(228, 81);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Статистика";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(18, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Самопересечение:";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbClipper);
            this.groupBox1.Controls.Add(this.rbSegment);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.ForeColor = System.Drawing.Color.Indigo;
            this.groupBox1.Location = new System.Drawing.Point(16, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 47);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мышиный ввод";
            // 
            // rbClipper
            // 
            this.rbClipper.AutoSize = true;
            this.rbClipper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbClipper.Location = new System.Drawing.Point(111, 20);
            this.rbClipper.Name = "rbClipper";
            this.rbClipper.Size = new System.Drawing.Size(95, 19);
            this.rbClipper.TabIndex = 1;
            this.rbClipper.Text = "Отсекатель";
            this.rbClipper.UseVisualStyleBackColor = true;
            // 
            // rbSegment
            // 
            this.rbSegment.AutoSize = true;
            this.rbSegment.Checked = true;
            this.rbSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbSegment.Location = new System.Drawing.Point(21, 20);
            this.rbSegment.Name = "rbSegment";
            this.rbSegment.Size = new System.Drawing.Size(74, 19);
            this.rbSegment.TabIndex = 0;
            this.rbSegment.TabStop = true;
            this.rbSegment.Text = "Отрезок";
            this.rbSegment.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Aquamarine;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUpdate.Location = new System.Drawing.Point(16, 545);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(228, 30);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(252)))), ((int)(((byte)(188)))));
            this.groupBox3.Controls.Add(this.pnlCuttingLine);
            this.groupBox3.Controls.Add(this.pnlCuttingRect);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.pnlBackgroud);
            this.groupBox3.Controls.Add(this.pnlLine);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.ForeColor = System.Drawing.Color.Indigo;
            this.groupBox3.Location = new System.Drawing.Point(16, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 96);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Цвета";
            // 
            // pnlCuttingLine
            // 
            this.pnlCuttingLine.BackColor = System.Drawing.Color.SandyBrown;
            this.pnlCuttingLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCuttingLine.Location = new System.Drawing.Point(197, 58);
            this.pnlCuttingLine.Name = "pnlCuttingLine";
            this.pnlCuttingLine.Size = new System.Drawing.Size(22, 22);
            this.pnlCuttingLine.TabIndex = 23;
            this.toolTip1.SetToolTip(this.pnlCuttingLine, "Цвет фона");
            this.pnlCuttingLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCuttingLine_MouseUp);
            // 
            // pnlCuttingRect
            // 
            this.pnlCuttingRect.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlCuttingRect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCuttingRect.Location = new System.Drawing.Point(197, 30);
            this.pnlCuttingRect.Name = "pnlCuttingRect";
            this.pnlCuttingRect.Size = new System.Drawing.Size(22, 22);
            this.pnlCuttingRect.TabIndex = 22;
            this.toolTip1.SetToolTip(this.pnlCuttingRect, "Цвет линий");
            this.pnlCuttingRect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCuttingLine_MouseUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(103, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 15);
            this.label15.TabIndex = 21;
            this.label15.Text = "Отсеч. линия";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(103, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 15);
            this.label14.TabIndex = 20;
            this.label14.Text = "Отсекатель";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(14, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "Фон";
            // 
            // pnlBackgroud
            // 
            this.pnlBackgroud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(253)))), ((int)(((byte)(210)))));
            this.pnlBackgroud.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBackgroud.Location = new System.Drawing.Point(73, 58);
            this.pnlBackgroud.Name = "pnlBackgroud";
            this.pnlBackgroud.Size = new System.Drawing.Size(22, 22);
            this.pnlBackgroud.TabIndex = 2;
            this.toolTip1.SetToolTip(this.pnlBackgroud, "Цвет фона");
            this.pnlBackgroud.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCuttingLine_MouseUp);
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.OrangeRed;
            this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine.Location = new System.Drawing.Point(73, 30);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(22, 22);
            this.pnlLine.TabIndex = 1;
            this.toolTip1.SetToolTip(this.pnlLine, "Цвет линий");
            this.pnlLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCuttingLine_MouseUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(14, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Линия";
            // 
            // gorupBox3
            // 
            this.gorupBox3.Controls.Add(this.label4);
            this.gorupBox3.Controls.Add(this.tbYe);
            this.gorupBox3.Controls.Add(this.label5);
            this.gorupBox3.Controls.Add(this.tbXe);
            this.gorupBox3.Controls.Add(this.label3);
            this.gorupBox3.Controls.Add(this.tbYb);
            this.gorupBox3.Controls.Add(this.label2);
            this.gorupBox3.Controls.Add(this.tbXb);
            this.gorupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gorupBox3.ForeColor = System.Drawing.Color.Indigo;
            this.gorupBox3.Location = new System.Drawing.Point(922, 155);
            this.gorupBox3.Name = "gorupBox3";
            this.gorupBox3.Size = new System.Drawing.Size(228, 79);
            this.gorupBox3.TabIndex = 1;
            this.gorupBox3.TabStop = false;
            this.gorupBox3.Text = "Отрезок";
            this.gorupBox3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(101, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Y2";
            // 
            // tbYe
            // 
            this.tbYe.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tbYe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbYe.Location = new System.Drawing.Point(129, 48);
            this.tbYe.Name = "tbYe";
            this.tbYe.Size = new System.Drawing.Size(45, 21);
            this.tbYe.TabIndex = 12;
            this.tbYe.Text = "100";
            this.tbYe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(101, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "X2";
            // 
            // tbXe
            // 
            this.tbXe.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tbXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbXe.Location = new System.Drawing.Point(129, 18);
            this.tbXe.Name = "tbXe";
            this.tbXe.Size = new System.Drawing.Size(45, 21);
            this.tbXe.TabIndex = 11;
            this.tbXe.Text = "473";
            this.tbXe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(24, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y1";
            // 
            // tbYb
            // 
            this.tbYb.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tbYb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbYb.Location = new System.Drawing.Point(50, 48);
            this.tbYb.Name = "tbYb";
            this.tbYb.Size = new System.Drawing.Size(45, 21);
            this.tbYb.TabIndex = 9;
            this.tbYb.Text = "441";
            this.tbYb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(24, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "X1";
            // 
            // tbXb
            // 
            this.tbXb.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tbXb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbXb.Location = new System.Drawing.Point(50, 20);
            this.tbXb.Name = "tbXb";
            this.tbXb.Size = new System.Drawing.Size(45, 21);
            this.tbXb.TabIndex = 8;
            this.tbXb.Text = "130";
            this.tbXb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYb_KeyPress_1);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTriag);
            this.groupBox2.Controls.Add(this.rbInterlace);
            this.groupBox2.Controls.Add(this.rbConvexDivide);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.ForeColor = System.Drawing.Color.Indigo;
            this.groupBox2.Location = new System.Drawing.Point(16, 333);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 105);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Метод";
            // 
            // rbConvexDivide
            // 
            this.rbConvexDivide.AutoSize = true;
            this.rbConvexDivide.Checked = true;
            this.rbConvexDivide.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbConvexDivide.Location = new System.Drawing.Point(17, 20);
            this.rbConvexDivide.Name = "rbConvexDivide";
            this.rbConvexDivide.Size = new System.Drawing.Size(165, 19);
            this.rbConvexDivide.TabIndex = 0;
            this.rbConvexDivide.TabStop = true;
            this.rbConvexDivide.Text = "Разбиение на выпуклые";
            this.rbConvexDivide.UseVisualStyleBackColor = true;
            this.rbConvexDivide.CheckedChanged += new System.EventHandler(this.rbInterlace_CheckedChanged);
            // 
            // rbInterlace
            // 
            this.rbInterlace.AutoSize = true;
            this.rbInterlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbInterlace.Location = new System.Drawing.Point(17, 81);
            this.rbInterlace.Name = "rbInterlace";
            this.rbInterlace.Size = new System.Drawing.Size(103, 19);
            this.rbInterlace.TabIndex = 1;
            this.rbInterlace.Text = "Чередование";
            this.rbInterlace.UseVisualStyleBackColor = true;
            this.rbInterlace.CheckedChanged += new System.EventHandler(this.rbInterlace_CheckedChanged);
            // 
            // cbTriag
            // 
            this.cbTriag.AutoSize = true;
            this.cbTriag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbTriag.Location = new System.Drawing.Point(38, 43);
            this.cbTriag.Name = "cbTriag";
            this.cbTriag.Size = new System.Drawing.Size(105, 19);
            this.cbTriag.TabIndex = 2;
            this.cbTriag.Text = "Треугольники";
            this.cbTriag.UseVisualStyleBackColor = true;
            this.cbTriag.CheckedChanged += new System.EventHandler(this.cbTriag_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(252)))), ((int)(((byte)(188)))));
            this.ClientSize = new System.Drawing.Size(891, 601);
            this.Controls.Add(this.cbConvex);
            this.Controls.Add(this.pictBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.edtEdgesCount);
            this.Controls.Add(this.gorupBox3);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Segmet cutting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtEdgesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtSegmentCount)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gorupBox3.ResumeLayout(false);
            this.gorupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlBackgroud;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gorupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlCuttingLine;
        private System.Windows.Forms.Panel pnlCuttingRect;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbYe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbXe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbXb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbClipper;
        private System.Windows.Forms.RadioButton rbSegment;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblConvex;
        private System.Windows.Forms.Label lblBypass;
        private System.Windows.Forms.Label lblSelfintersect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown edtEdgesCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown edtSegmentCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox cbConvex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbTriag;
        private System.Windows.Forms.RadioButton rbInterlace;
        private System.Windows.Forms.RadioButton rbConvexDivide;
    }
}

