namespace SegmentCut
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbRYe = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRXe = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbRYb = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbRXb = new System.Windows.Forms.TextBox();
            this.pictBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbX2 = new System.Windows.Forms.TextBox();
            this.tbX1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gorupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnUpdate);
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Controls.Add(this.gorupBox3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Location = new System.Drawing.Point(616, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(243, 582);
            this.panel3.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbClipper);
            this.groupBox1.Controls.Add(this.rbSegment);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(16, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 71);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мышиный ввод";
            // 
            // rbClipper
            // 
            this.rbClipper.AutoSize = true;
            this.rbClipper.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbClipper.Location = new System.Drawing.Point(24, 45);
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
            this.rbSegment.Location = new System.Drawing.Point(24, 20);
            this.rbSegment.Name = "rbSegment";
            this.rbSegment.Size = new System.Drawing.Size(74, 19);
            this.rbSegment.TabIndex = 0;
            this.rbSegment.TabStop = true;
            this.rbSegment.Text = "Отрезок";
            this.rbSegment.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Wheat;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.btnUpdate.Location = new System.Drawing.Point(16, 514);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(211, 30);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Обновить";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlCuttingLine);
            this.groupBox3.Controls.Add(this.pnlCuttingRect);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.pnlBackgroud);
            this.groupBox3.Controls.Add(this.pnlLine);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(16, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(211, 161);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Метод и цвета";
            // 
            // pnlCuttingLine
            // 
            this.pnlCuttingLine.BackColor = System.Drawing.Color.LimeGreen;
            this.pnlCuttingLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCuttingLine.Location = new System.Drawing.Point(169, 123);
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
            this.pnlCuttingRect.Location = new System.Drawing.Point(169, 94);
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
            this.label15.Location = new System.Drawing.Point(23, 123);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 15);
            this.label15.TabIndex = 21;
            this.label15.Text = "Цвет отсеч. линии";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(23, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(108, 15);
            this.label14.TabIndex = 20;
            this.label14.Text = "Цвет отсекателя";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(23, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "Цвет фона";
            // 
            // pnlBackgroud
            // 
            this.pnlBackgroud.BackColor = System.Drawing.Color.Cornsilk;
            this.pnlBackgroud.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBackgroud.Location = new System.Drawing.Point(169, 61);
            this.pnlBackgroud.Name = "pnlBackgroud";
            this.pnlBackgroud.Size = new System.Drawing.Size(22, 22);
            this.pnlBackgroud.TabIndex = 2;
            this.toolTip1.SetToolTip(this.pnlBackgroud, "Цвет фона");
            this.pnlBackgroud.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCuttingLine_MouseUp);
            // 
            // pnlLine
            // 
            this.pnlLine.BackColor = System.Drawing.Color.Crimson;
            this.pnlLine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine.Location = new System.Drawing.Point(169, 32);
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
            this.label6.Location = new System.Drawing.Point(23, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Цвет линии";
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
            this.gorupBox3.Location = new System.Drawing.Point(16, 251);
            this.gorupBox3.Name = "gorupBox3";
            this.gorupBox3.Size = new System.Drawing.Size(211, 94);
            this.gorupBox3.TabIndex = 1;
            this.gorupBox3.TabStop = false;
            this.gorupBox3.Text = "Отрезок";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(104, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "Y2";
            // 
            // tbYe
            // 
            this.tbYe.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbYe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbYe.Location = new System.Drawing.Point(132, 56);
            this.tbYe.Name = "tbYe";
            this.tbYe.Size = new System.Drawing.Size(45, 21);
            this.tbYe.TabIndex = 12;
            this.tbYe.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(104, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "X2";
            // 
            // tbXe
            // 
            this.tbXe.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbXe.Location = new System.Drawing.Point(132, 26);
            this.tbXe.Name = "tbXe";
            this.tbXe.Size = new System.Drawing.Size(45, 21);
            this.tbXe.TabIndex = 11;
            this.tbXe.Text = "473";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(27, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y1";
            // 
            // tbYb
            // 
            this.tbYb.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbYb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbYb.Location = new System.Drawing.Point(53, 56);
            this.tbYb.Name = "tbYb";
            this.tbYb.Size = new System.Drawing.Size(45, 21);
            this.tbYb.TabIndex = 9;
            this.tbYb.Text = "441";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(27, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "X1";
            // 
            // tbXb
            // 
            this.tbXb.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbXb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbXb.Location = new System.Drawing.Point(53, 28);
            this.tbXb.Name = "tbXb";
            this.tbXb.Size = new System.Drawing.Size(45, 21);
            this.tbXb.TabIndex = 8;
            this.tbXb.Text = "130";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbRYe);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbRXe);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tbRYb);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tbRXb);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(16, 351);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 95);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Отсекатель";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(104, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Y2";
            // 
            // tbRYe
            // 
            this.tbRYe.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbRYe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRYe.Location = new System.Drawing.Point(132, 60);
            this.tbRYe.Name = "tbRYe";
            this.tbRYe.Size = new System.Drawing.Size(45, 21);
            this.tbRYe.TabIndex = 19;
            this.tbRYe.Text = "348";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(104, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "X2";
            // 
            // tbRXe
            // 
            this.tbRXe.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbRXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRXe.Location = new System.Drawing.Point(132, 30);
            this.tbRXe.Name = "tbRXe";
            this.tbRXe.Size = new System.Drawing.Size(45, 21);
            this.tbRXe.TabIndex = 18;
            this.tbRXe.Text = "428";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(27, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "Y1";
            // 
            // tbRYb
            // 
            this.tbRYb.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbRYb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRYb.Location = new System.Drawing.Point(53, 60);
            this.tbRYb.Name = "tbRYb";
            this.tbRYb.Size = new System.Drawing.Size(45, 21);
            this.tbRYb.TabIndex = 16;
            this.tbRYb.Text = "95";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(27, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 15);
            this.label12.TabIndex = 17;
            this.label12.Text = "X1";
            // 
            // tbRXb
            // 
            this.tbRXb.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tbRXb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbRXb.Location = new System.Drawing.Point(53, 32);
            this.tbRXb.Name = "tbRXb";
            this.tbRXb.Size = new System.Drawing.Size(45, 21);
            this.tbRXb.TabIndex = 15;
            this.tbRXb.Text = "150";
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
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Wheat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.button1.Location = new System.Drawing.Point(16, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 30);
            this.button1.TabIndex = 18;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(220)))), ((int)(((byte)(194)))));
            this.ClientSize = new System.Drawing.Size(871, 601);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pictBox);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Segmet cutting";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gorupBox3.ResumeLayout(false);
            this.gorupBox3.PerformLayout();
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
        private System.Windows.Forms.Panel pnlBackgroud;
        private System.Windows.Forms.Panel pnlLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gorupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbX2;
        private System.Windows.Forms.TextBox tbX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRYe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRXe;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbRYb;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbRXb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbClipper;
        private System.Windows.Forms.RadioButton rbSegment;
        private System.Windows.Forms.Button button1;
    }
}

