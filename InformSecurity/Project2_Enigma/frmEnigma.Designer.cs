namespace Project2_Enigma
{
    partial class frmEnigma
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
            System.Windows.Forms.Button btnRandomDisks;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ofdMachine = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCrypt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDecodeFile = new System.Windows.Forms.TextBox();
            this.tbEncodeFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSorceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDecodeStr = new System.Windows.Forms.TextBox();
            this.tbEncodeStr = new System.Windows.Forms.TextBox();
            this.tbSrcStr = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbInverseDecoding = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRandSeed = new System.Windows.Forms.Button();
            this.gvDisks = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.udDiskCount = new System.Windows.Forms.NumericUpDown();
            this.ofdKeys = new System.Windows.Forms.OpenFileDialog();
            this.sfdKeys = new System.Windows.Forms.SaveFileDialog();
            this.sfdMachine = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            btnRandomDisks = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDisks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDiskCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRandomDisks
            // 
            btnRandomDisks.Location = new System.Drawing.Point(18, 300);
            btnRandomDisks.Name = "btnRandomDisks";
            btnRandomDisks.Size = new System.Drawing.Size(121, 31);
            btnRandomDisks.TabIndex = 29;
            btnRandomDisks.Text = "Перемешать диски";
            btnRandomDisks.UseVisualStyleBackColor = true;
            btnRandomDisks.Click += new System.EventHandler(this.btnRandomDisks_Click);
            // 
            // ofdMachine
            // 
            this.ofdMachine.FileName = "openFileDialog1";
            this.ofdMachine.Filter = "Enigma (*.enigma)|*.enigma|All files (*.*)|*.*";
            // 
            // btnCrypt
            // 
            this.btnCrypt.Location = new System.Drawing.Point(14, 110);
            this.btnCrypt.Name = "btnCrypt";
            this.btnCrypt.Size = new System.Drawing.Size(149, 29);
            this.btnCrypt.TabIndex = 10;
            this.btnCrypt.Text = "Зашифровать файл";
            this.btnCrypt.UseVisualStyleBackColor = true;
            this.btnCrypt.Click += new System.EventHandler(this.btnCrypt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.btnDecrypt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbDecodeFile);
            this.groupBox1.Controls.Add(this.tbEncodeFile);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbSorceFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCrypt);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 214);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Шифрование файла";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.PeachPuff;
            this.button9.ForeColor = System.Drawing.Color.BurlyWood;
            this.button9.Location = new System.Drawing.Point(167, 116);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(25, 18);
            this.button9.TabIndex = 21;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(365, 72);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(26, 23);
            this.button7.TabIndex = 14;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(14, 145);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 29);
            this.button4.TabIndex = 20;
            this.button4.Text = "Открыть папку с файлами";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(365, 46);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(26, 23);
            this.button6.TabIndex = 13;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(365, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 23);
            this.button5.TabIndex = 12;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(196, 110);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(149, 29);
            this.btnDecrypt.TabIndex = 11;
            this.btnDecrypt.Text = "Расшифровать файл";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Расшифрованный файл";
            // 
            // tbDecodeFile
            // 
            this.tbDecodeFile.Location = new System.Drawing.Point(158, 74);
            this.tbDecodeFile.Name = "tbDecodeFile";
            this.tbDecodeFile.Size = new System.Drawing.Size(207, 20);
            this.tbDecodeFile.TabIndex = 6;
            this.tbDecodeFile.Text = "../../Data/Карта метро (decode).png";
            // 
            // tbEncodeFile
            // 
            this.tbEncodeFile.Location = new System.Drawing.Point(158, 48);
            this.tbEncodeFile.Name = "tbEncodeFile";
            this.tbEncodeFile.Size = new System.Drawing.Size(207, 20);
            this.tbEncodeFile.TabIndex = 5;
            this.tbEncodeFile.Text = "../../Data/Карта метро.png.encode";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Зашифрованный файл";
            // 
            // tbSorceFile
            // 
            this.tbSorceFile.Location = new System.Drawing.Point(158, 22);
            this.tbSorceFile.Name = "tbSorceFile";
            this.tbSorceFile.Size = new System.Drawing.Size(207, 20);
            this.tbSorceFile.TabIndex = 3;
            this.tbSorceFile.Text = "../../Data/Карта метро.png";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Исходный файл";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbDecodeStr);
            this.groupBox2.Controls.Add(this.tbEncodeStr);
            this.groupBox2.Controls.Add(this.tbSrcStr);
            this.groupBox2.Location = new System.Drawing.Point(12, 232);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(397, 219);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Шифрование строки";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Исходная строка";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Расшифрованная строка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Шифрованная строка";
            // 
            // tbDecodeStr
            // 
            this.tbDecodeStr.Location = new System.Drawing.Point(21, 125);
            this.tbDecodeStr.Name = "tbDecodeStr";
            this.tbDecodeStr.ReadOnly = true;
            this.tbDecodeStr.Size = new System.Drawing.Size(344, 20);
            this.tbDecodeStr.TabIndex = 17;
            // 
            // tbEncodeStr
            // 
            this.tbEncodeStr.Location = new System.Drawing.Point(21, 86);
            this.tbEncodeStr.Name = "tbEncodeStr";
            this.tbEncodeStr.ReadOnly = true;
            this.tbEncodeStr.Size = new System.Drawing.Size(344, 20);
            this.tbEncodeStr.TabIndex = 16;
            // 
            // tbSrcStr
            // 
            this.tbSrcStr.Location = new System.Drawing.Point(21, 47);
            this.tbSrcStr.Name = "tbSrcStr";
            this.tbSrcStr.Size = new System.Drawing.Size(344, 20);
            this.tbSrcStr.TabIndex = 15;
            this.tbSrcStr.Text = "aaaaaaaaasdfghjk1234";
            this.tbSrcStr.TextChanged += new System.EventHandler(this.tbSrcStr_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbInverseDecoding);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button8);
            this.groupBox3.Controls.Add(btnRandomDisks);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnRandSeed);
            this.groupBox3.Controls.Add(this.gvDisks);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.udDiskCount);
            this.groupBox3.Location = new System.Drawing.Point(415, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 439);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Устройство машины";
            // 
            // cbInverseDecoding
            // 
            this.cbInverseDecoding.AutoSize = true;
            this.cbInverseDecoding.Location = new System.Drawing.Point(77, 409);
            this.cbInverseDecoding.Name = "cbInverseDecoding";
            this.cbInverseDecoding.Size = new System.Drawing.Size(144, 17);
            this.cbInverseDecoding.TabIndex = 32;
            this.cbInverseDecoding.Text = "Обратная декодировка";
            this.cbInverseDecoding.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 33);
            this.button2.TabIndex = 31;
            this.button2.Text = "Загрузить машину";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(18, 334);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(121, 33);
            this.button8.TabIndex = 30;
            this.button8.Text = "Сохранить машину";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(145, 370);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 33);
            this.button3.TabIndex = 28;
            this.button3.Text = "Загрузить ключ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 33);
            this.button1.TabIndex = 27;
            this.button1.Text = "Сохранить ключ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRandSeed
            // 
            this.btnRandSeed.Location = new System.Drawing.Point(145, 300);
            this.btnRandSeed.Name = "btnRandSeed";
            this.btnRandSeed.Size = new System.Drawing.Size(118, 31);
            this.btnRandSeed.TabIndex = 26;
            this.btnRandSeed.Text = "Перемешать ключ";
            this.btnRandSeed.UseVisualStyleBackColor = true;
            this.btnRandSeed.Click += new System.EventHandler(this.button3_Click);
            // 
            // gvDisks
            // 
            this.gvDisks.AllowUserToResizeColumns = false;
            this.gvDisks.AllowUserToResizeRows = false;
            this.gvDisks.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.gvDisks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvDisks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gvDisks.Location = new System.Drawing.Point(18, 60);
            this.gvDisks.Name = "gvDisks";
            this.gvDisks.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvDisks.Size = new System.Drawing.Size(244, 227);
            this.gvDisks.TabIndex = 23;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Номер";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            this.Column1.Width = 80;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.DividerWidth = 3;
            this.Column2.HeaderText = "Seed";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 85;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Dir";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column3.Width = 40;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Start Pos";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.Width = 75;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Количество дисков";
            // 
            // udDiskCount
            // 
            this.udDiskCount.Location = new System.Drawing.Point(154, 26);
            this.udDiskCount.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.udDiskCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udDiskCount.Name = "udDiskCount";
            this.udDiskCount.ReadOnly = true;
            this.udDiskCount.Size = new System.Drawing.Size(56, 20);
            this.udDiskCount.TabIndex = 21;
            this.udDiskCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.udDiskCount.ValueChanged += new System.EventHandler(this.udDiskCount_ValueChanged);
            // 
            // ofdKeys
            // 
            this.ofdKeys.FileName = "openFileDialog2";
            this.ofdKeys.Filter = "Enigma keys (*.key)|*.key|All files (*.*)|*.*";
            this.ofdKeys.RestoreDirectory = true;
            this.ofdKeys.Title = "Выберите файл ключа";
            // 
            // sfdKeys
            // 
            this.sfdKeys.Filter = "Enigma keys (*.key)|*.key|All files (*.*)|*.*";
            this.sfdKeys.Title = "Введите имя ключа";
            // 
            // sfdMachine
            // 
            this.sfdMachine.Filter = "Enigma (*.enigma)|*.enigma|All files (*.*)|*.*";
            this.sfdMachine.Title = "Введите имя ключа";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All files (*.*)|*.*";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Title = "Выберите файл ключа";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 463);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Энигма";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvDisks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDiskCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdMachine;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnCrypt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDecodeFile;
        private System.Windows.Forms.TextBox tbEncodeFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSorceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbDecodeStr;
        private System.Windows.Forms.TextBox tbEncodeStr;
        private System.Windows.Forms.TextBox tbSrcStr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRandSeed;
        private System.Windows.Forms.DataGridView gvDisks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown udDiskCount;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.OpenFileDialog ofdKeys;
        private System.Windows.Forms.SaveFileDialog sfdKeys;
        private System.Windows.Forms.SaveFileDialog sfdMachine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox cbInverseDecoding;
        private System.Windows.Forms.Button button9;
    }
}

