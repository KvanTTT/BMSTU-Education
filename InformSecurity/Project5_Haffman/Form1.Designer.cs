namespace Project5_Haffman
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tbDecodeFile = new System.Windows.Forms.TextBox();
            this.tbEncodeFile = new System.Windows.Forms.TextBox();
            this.tbSorceFile = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCrypt = new System.Windows.Forms.Button();
            this.tbDecodeTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbEncodeTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRatio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbRatio);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbDecodeTime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbEncodeTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.tbDecodeFile);
            this.groupBox1.Controls.Add(this.tbEncodeFile);
            this.groupBox1.Controls.Add(this.tbSorceFile);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.btnDecrypt);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCrypt);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 258);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(360, 71);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(26, 23);
            this.button7.TabIndex = 27;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(360, 45);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(26, 23);
            this.button6.TabIndex = 26;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(360, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tbDecodeFile
            // 
            this.tbDecodeFile.Location = new System.Drawing.Point(153, 73);
            this.tbDecodeFile.Name = "tbDecodeFile";
            this.tbDecodeFile.Size = new System.Drawing.Size(207, 20);
            this.tbDecodeFile.TabIndex = 24;
            this.tbDecodeFile.Text = "../../../Data/asdf.txt.hcs.txt";
            // 
            // tbEncodeFile
            // 
            this.tbEncodeFile.Location = new System.Drawing.Point(153, 47);
            this.tbEncodeFile.Name = "tbEncodeFile";
            this.tbEncodeFile.Size = new System.Drawing.Size(207, 20);
            this.tbEncodeFile.TabIndex = 23;
            this.tbEncodeFile.Text = "../../../Data/asdf.txt.hcs";
            // 
            // tbSorceFile
            // 
            this.tbSorceFile.Location = new System.Drawing.Point(153, 21);
            this.tbSorceFile.Name = "tbSorceFile";
            this.tbSorceFile.Size = new System.Drawing.Size(207, 20);
            this.tbSorceFile.TabIndex = 22;
            this.tbSorceFile.Text = "../../../Data/asdf.txt";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.PeachPuff;
            this.button9.ForeColor = System.Drawing.Color.BurlyWood;
            this.button9.Location = new System.Drawing.Point(173, 115);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(25, 18);
            this.button9.TabIndex = 21;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(202, 109);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(149, 29);
            this.btnDecrypt.TabIndex = 11;
            this.btnDecrypt.Text = "Extract";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Decompress file";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Compress file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source file";
            // 
            // btnCrypt
            // 
            this.btnCrypt.Location = new System.Drawing.Point(20, 109);
            this.btnCrypt.Name = "btnCrypt";
            this.btnCrypt.Size = new System.Drawing.Size(149, 29);
            this.btnCrypt.TabIndex = 10;
            this.btnCrypt.Text = "Compress";
            this.btnCrypt.UseVisualStyleBackColor = true;
            this.btnCrypt.Click += new System.EventHandler(this.btnCrypt_Click);
            // 
            // tbDecodeTime
            // 
            this.tbDecodeTime.Location = new System.Drawing.Point(162, 186);
            this.tbDecodeTime.Name = "tbDecodeTime";
            this.tbDecodeTime.ReadOnly = true;
            this.tbDecodeTime.Size = new System.Drawing.Size(143, 20);
            this.tbDecodeTime.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Extrac time";
            // 
            // tbEncodeTime
            // 
            this.tbEncodeTime.Location = new System.Drawing.Point(162, 160);
            this.tbEncodeTime.Name = "tbEncodeTime";
            this.tbEncodeTime.ReadOnly = true;
            this.tbEncodeTime.Size = new System.Drawing.Size(143, 20);
            this.tbEncodeTime.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Compress time";
            // 
            // tbRatio
            // 
            this.tbRatio.Location = new System.Drawing.Point(162, 212);
            this.tbRatio.Name = "tbRatio";
            this.tbRatio.ReadOnly = true;
            this.tbRatio.Size = new System.Drawing.Size(143, 20);
            this.tbRatio.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Ratio";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 282);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Haffman compress";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox tbDecodeFile;
        private System.Windows.Forms.TextBox tbEncodeFile;
        private System.Windows.Forms.TextBox tbSorceFile;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCrypt;
        private System.Windows.Forms.TextBox tbDecodeTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbEncodeTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRatio;
        private System.Windows.Forms.Label label4;

    }
}

