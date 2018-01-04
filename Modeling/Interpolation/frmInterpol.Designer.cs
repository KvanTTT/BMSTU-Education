namespace Interpolation
{
    partial class frmInterpol
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUpperBound = new System.Windows.Forms.NumericUpDown();
            this.tbBottomBound = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPointsTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tbRoot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbPoints = new System.Windows.Forms.RadioButton();
            this.rbFunc = new System.Windows.Forms.RadioButton();
            this.tbError = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbFuncResult = new System.Windows.Forms.TextBox();
            this.lbPower = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPower = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPolynomResult = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbX = new System.Windows.Forms.TextBox();
            this.gbMethod = new System.Windows.Forms.GroupBox();
            this.tbSquares = new System.Windows.Forms.Label();
            this.rbSpline = new System.Windows.Forms.RadioButton();
            this.rbNewton = new System.Windows.Forms.RadioButton();
            this.btnCalcul = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpperBound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBottomBound)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPointsTable)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPower)).BeginInit();
            this.gbMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.cmbMethod);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.tbRoot);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.tbError);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbFuncResult);
            this.panel1.Controls.Add(this.lbPower);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbPower);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbPolynomResult);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbX);
            this.panel1.Controls.Add(this.gbMethod);
            this.panel1.Location = new System.Drawing.Point(763, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 663);
            this.panel1.TabIndex = 28;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.tbUpperBound);
            this.groupBox5.Controls.Add(this.tbBottomBound);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(12, 338);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(235, 122);
            this.groupBox5.TabIndex = 72;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Графика";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(217, 25);
            this.button1.TabIndex = 72;
            this.button1.Text = "Настройка графиков";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "X2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "X1";
            // 
            // tbUpperBound
            // 
            this.tbUpperBound.Location = new System.Drawing.Point(151, 44);
            this.tbUpperBound.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.tbUpperBound.Name = "tbUpperBound";
            this.tbUpperBound.Size = new System.Drawing.Size(71, 20);
            this.tbUpperBound.TabIndex = 74;
            this.tbUpperBound.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbUpperBound.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.tbUpperBound.ValueChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tbBottomBound
            // 
            this.tbBottomBound.Location = new System.Drawing.Point(35, 43);
            this.tbBottomBound.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.tbBottomBound.Name = "tbBottomBound";
            this.tbBottomBound.Size = new System.Drawing.Size(71, 20);
            this.tbBottomBound.TabIndex = 73;
            this.tbBottomBound.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbBottomBound.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
            this.tbBottomBound.ValueChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "Границы";
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Интерполяция",
            "Аппроксимация"});
            this.cmbMethod.Location = new System.Drawing.Point(12, 11);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(242, 21);
            this.cmbMethod.TabIndex = 65;
            this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.cmbMethod_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPointsTable);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(12, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 191);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // dgvPointsTable
            // 
            this.dgvPointsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPointsTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4});
            this.dgvPointsTable.Location = new System.Drawing.Point(11, 48);
            this.dgvPointsTable.Name = "dgvPointsTable";
            this.dgvPointsTable.Size = new System.Drawing.Size(220, 126);
            this.dgvPointsTable.TabIndex = 68;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "X";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Y";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "ρ";
            this.Column4.Name = "Column4";
            this.Column4.Visible = false;
            this.Column4.Width = 40;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(220, 23);
            this.button2.TabIndex = 76;
            this.button2.Text = "Генерировать таблицу";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.OrangeRed;
            this.label12.Location = new System.Drawing.Point(78, 642);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 16);
            this.label12.TabIndex = 34;
            this.label12.Text = "Экстраполяция!";
            // 
            // tbRoot
            // 
            this.tbRoot.Location = new System.Drawing.Point(142, 613);
            this.tbRoot.Name = "tbRoot";
            this.tbRoot.ReadOnly = true;
            this.tbRoot.Size = new System.Drawing.Size(90, 20);
            this.tbRoot.TabIndex = 61;
            this.tbRoot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 616);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 60;
            this.label11.Text = "Корень уравнения";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbPoints);
            this.groupBox3.Controls.Add(this.rbFunc);
            this.groupBox3.Location = new System.Drawing.Point(12, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(242, 46);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Метод построения";
            // 
            // rbPoints
            // 
            this.rbPoints.AutoSize = true;
            this.rbPoints.Enabled = false;
            this.rbPoints.Location = new System.Drawing.Point(143, 19);
            this.rbPoints.Name = "rbPoints";
            this.rbPoints.Size = new System.Drawing.Size(55, 17);
            this.rbPoints.TabIndex = 1;
            this.rbPoints.Text = "Точки";
            this.rbPoints.UseVisualStyleBackColor = true;
            // 
            // rbFunc
            // 
            this.rbFunc.AutoSize = true;
            this.rbFunc.Checked = true;
            this.rbFunc.Location = new System.Drawing.Point(18, 19);
            this.rbFunc.Name = "rbFunc";
            this.rbFunc.Size = new System.Drawing.Size(71, 17);
            this.rbFunc.TabIndex = 0;
            this.rbFunc.TabStop = true;
            this.rbFunc.Text = "Функция";
            this.rbFunc.UseVisualStyleBackColor = true;
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(142, 587);
            this.tbError.Name = "tbError";
            this.tbError.ReadOnly = true;
            this.tbError.Size = new System.Drawing.Size(90, 20);
            this.tbError.TabIndex = 45;
            this.tbError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 590);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Погрешность";
            // 
            // tbFuncResult
            // 
            this.tbFuncResult.Location = new System.Drawing.Point(142, 535);
            this.tbFuncResult.Name = "tbFuncResult";
            this.tbFuncResult.ReadOnly = true;
            this.tbFuncResult.Size = new System.Drawing.Size(90, 20);
            this.tbFuncResult.TabIndex = 43;
            this.tbFuncResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbPower
            // 
            this.lbPower.Location = new System.Drawing.Point(21, 481);
            this.lbPower.Name = "lbPower";
            this.lbPower.Size = new System.Drawing.Size(104, 14);
            this.lbPower.TabIndex = 64;
            this.lbPower.Text = "Степень полинома";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 538);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Значение функции";
            // 
            // tbPower
            // 
            this.tbPower.Location = new System.Drawing.Point(142, 477);
            this.tbPower.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.tbPower.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tbPower.Name = "tbPower";
            this.tbPower.Size = new System.Drawing.Size(88, 20);
            this.tbPower.TabIndex = 63;
            this.tbPower.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.tbPower.ValueChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(20, 561);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 40;
            this.label5.Text = "Результат";
            // 
            // tbPolynomResult
            // 
            this.tbPolynomResult.Location = new System.Drawing.Point(142, 561);
            this.tbPolynomResult.Name = "tbPolynomResult";
            this.tbPolynomResult.ReadOnly = true;
            this.tbPolynomResult.Size = new System.Drawing.Size(90, 20);
            this.tbPolynomResult.TabIndex = 39;
            this.tbPolynomResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 513);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "X искомый";
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(142, 509);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(90, 20);
            this.tbX.TabIndex = 37;
            this.tbX.Text = "1,5";
            this.tbX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbX.TextChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gbMethod
            // 
            this.gbMethod.Controls.Add(this.tbSquares);
            this.gbMethod.Controls.Add(this.rbSpline);
            this.gbMethod.Controls.Add(this.rbNewton);
            this.gbMethod.Location = new System.Drawing.Point(12, 38);
            this.gbMethod.Name = "gbMethod";
            this.gbMethod.Size = new System.Drawing.Size(242, 45);
            this.gbMethod.TabIndex = 28;
            this.gbMethod.TabStop = false;
            this.gbMethod.Text = "Метод интерполяции";
            // 
            // tbSquares
            // 
            this.tbSquares.Location = new System.Drawing.Point(20, 19);
            this.tbSquares.Name = "tbSquares";
            this.tbSquares.Size = new System.Drawing.Size(194, 16);
            this.tbSquares.TabIndex = 66;
            this.tbSquares.Text = "Наименьших квадратов";
            // 
            // rbSpline
            // 
            this.rbSpline.AutoSize = true;
            this.rbSpline.Location = new System.Drawing.Point(144, 19);
            this.rbSpline.Name = "rbSpline";
            this.rbSpline.Size = new System.Drawing.Size(70, 17);
            this.rbSpline.TabIndex = 1;
            this.rbSpline.Text = "Сплайны";
            this.rbSpline.UseVisualStyleBackColor = true;
            this.rbSpline.CheckedChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // rbNewton
            // 
            this.rbNewton.AutoSize = true;
            this.rbNewton.Checked = true;
            this.rbNewton.Location = new System.Drawing.Point(18, 19);
            this.rbNewton.Name = "rbNewton";
            this.rbNewton.Size = new System.Drawing.Size(119, 17);
            this.rbNewton.TabIndex = 0;
            this.rbNewton.TabStop = true;
            this.rbNewton.Text = "Полином Ньютона";
            this.rbNewton.UseVisualStyleBackColor = true;
            this.rbNewton.CheckedChanged += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCalcul
            // 
            this.btnCalcul.Location = new System.Drawing.Point(671, 634);
            this.btnCalcul.Name = "btnCalcul";
            this.btnCalcul.Size = new System.Drawing.Size(86, 26);
            this.btnCalcul.TabIndex = 33;
            this.btnCalcul.Text = "Найти";
            this.btnCalcul.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(745, 663);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(558, 602);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(170, 43);
            this.button4.TabIndex = 36;
            this.button4.Text = "Обновить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmInterpol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 687);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCalcul);
            this.Name = "frmInterpol";
            this.Text = "Интерполяция";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmInterpol_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbUpperBound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBottomBound)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPointsTable)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbPower)).EndInit();
            this.gbMethod.ResumeLayout(false);
            this.gbMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbFuncResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPolynomResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbX;
        private System.Windows.Forms.Button btnCalcul;
        private System.Windows.Forms.GroupBox gbMethod;
        private System.Windows.Forms.RadioButton rbSpline;
        private System.Windows.Forms.RadioButton rbNewton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbPoints;
        private System.Windows.Forms.RadioButton rbFunc;
        private System.Windows.Forms.TextBox tbRoot;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgvPointsTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbPower;
        private System.Windows.Forms.NumericUpDown tbPower;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown tbUpperBound;
        private System.Windows.Forms.NumericUpDown tbBottomBound;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label tbSquares;
    }
}

