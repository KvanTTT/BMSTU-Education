namespace Project4_Queue
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRight = new System.Windows.Forms.TextBox();
            this.tbLeft = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblθParam = new System.Windows.Forms.Label();
            this.lblKParam = new System.Windows.Forms.Label();
            this.tbθParam = new System.Windows.Forms.TextBox();
            this.tbKParam = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAverageStayTime = new System.Windows.Forms.TextBox();
            this.tbMaxQueueLength = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTimeStep = new System.Windows.Forms.TextBox();
            this.udRequestCount = new System.Windows.Forms.NumericUpDown();
            this.rbCount = new System.Windows.Forms.RadioButton();
            this.rbTime = new System.Windows.Forms.RadioButton();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.cbInverseLink = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAverageQueueLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRequestCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbRight);
            this.groupBox1.Controls.Add(this.tbLeft);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 80);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Right";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Left";
            // 
            // tbRight
            // 
            this.tbRight.Location = new System.Drawing.Point(108, 45);
            this.tbRight.Name = "tbRight";
            this.tbRight.Size = new System.Drawing.Size(61, 20);
            this.tbRight.TabIndex = 15;
            this.tbRight.Text = "12";
            // 
            // tbLeft
            // 
            this.tbLeft.Location = new System.Drawing.Point(108, 19);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.Size = new System.Drawing.Size(61, 20);
            this.tbLeft.TabIndex = 14;
            this.tbLeft.Text = "8";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblθParam);
            this.groupBox2.Controls.Add(this.lblKParam);
            this.groupBox2.Controls.Add(this.tbθParam);
            this.groupBox2.Controls.Add(this.tbKParam);
            this.groupBox2.Location = new System.Drawing.Point(12, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 82);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service machine";
            // 
            // lblθParam
            // 
            this.lblθParam.AutoSize = true;
            this.lblθParam.Location = new System.Drawing.Point(12, 54);
            this.lblθParam.Name = "lblθParam";
            this.lblθParam.Size = new System.Drawing.Size(13, 13);
            this.lblθParam.TabIndex = 32;
            this.lblθParam.Text = "θ";
            // 
            // lblKParam
            // 
            this.lblKParam.AutoSize = true;
            this.lblKParam.Location = new System.Drawing.Point(12, 28);
            this.lblKParam.Name = "lblKParam";
            this.lblKParam.Size = new System.Drawing.Size(13, 13);
            this.lblKParam.TabIndex = 31;
            this.lblKParam.Text = "k";
            // 
            // tbθParam
            // 
            this.tbθParam.Location = new System.Drawing.Point(108, 51);
            this.tbθParam.Name = "tbθParam";
            this.tbθParam.Size = new System.Drawing.Size(61, 20);
            this.tbθParam.TabIndex = 30;
            this.tbθParam.Text = "1";
            // 
            // tbKParam
            // 
            this.tbKParam.Location = new System.Drawing.Point(108, 25);
            this.tbKParam.Name = "tbKParam";
            this.tbKParam.Size = new System.Drawing.Size(61, 20);
            this.tbKParam.TabIndex = 29;
            this.tbKParam.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Max queue length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Average stay time";
            // 
            // tbAverageStayTime
            // 
            this.tbAverageStayTime.Location = new System.Drawing.Point(140, 383);
            this.tbAverageStayTime.Name = "tbAverageStayTime";
            this.tbAverageStayTime.ReadOnly = true;
            this.tbAverageStayTime.Size = new System.Drawing.Size(82, 20);
            this.tbAverageStayTime.TabIndex = 34;
            // 
            // tbMaxQueueLength
            // 
            this.tbMaxQueueLength.Location = new System.Drawing.Point(140, 357);
            this.tbMaxQueueLength.Name = "tbMaxQueueLength";
            this.tbMaxQueueLength.ReadOnly = true;
            this.tbMaxQueueLength.Size = new System.Drawing.Size(82, 20);
            this.tbMaxQueueLength.TabIndex = 33;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 25);
            this.button1.TabIndex = 35;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbTimeStep);
            this.groupBox3.Controls.Add(this.udRequestCount);
            this.groupBox3.Controls.Add(this.rbCount);
            this.groupBox3.Controls.Add(this.rbTime);
            this.groupBox3.Controls.Add(this.tbTime);
            this.groupBox3.Location = new System.Drawing.Point(14, 187);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(208, 109);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dispatcher";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Time step";
            // 
            // tbTimeStep
            // 
            this.tbTimeStep.Location = new System.Drawing.Point(106, 78);
            this.tbTimeStep.Name = "tbTimeStep";
            this.tbTimeStep.Size = new System.Drawing.Size(61, 20);
            this.tbTimeStep.TabIndex = 37;
            this.tbTimeStep.Text = "0,001";
            // 
            // udRequestCount
            // 
            this.udRequestCount.Location = new System.Drawing.Point(106, 52);
            this.udRequestCount.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.udRequestCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udRequestCount.Name = "udRequestCount";
            this.udRequestCount.Size = new System.Drawing.Size(61, 20);
            this.udRequestCount.TabIndex = 30;
            this.udRequestCount.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // rbCount
            // 
            this.rbCount.AutoSize = true;
            this.rbCount.Location = new System.Drawing.Point(13, 52);
            this.rbCount.Name = "rbCount";
            this.rbCount.Size = new System.Drawing.Size(53, 17);
            this.rbCount.TabIndex = 36;
            this.rbCount.Text = "Count";
            this.rbCount.UseVisualStyleBackColor = true;
            // 
            // rbTime
            // 
            this.rbTime.AutoSize = true;
            this.rbTime.Checked = true;
            this.rbTime.Location = new System.Drawing.Point(13, 26);
            this.rbTime.Name = "rbTime";
            this.rbTime.Size = new System.Drawing.Size(48, 17);
            this.rbTime.TabIndex = 35;
            this.rbTime.TabStop = true;
            this.rbTime.Text = "Time";
            this.rbTime.UseVisualStyleBackColor = true;
            this.rbTime.CheckedChanged += new System.EventHandler(this.rbTime_CheckedChanged);
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(106, 26);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(61, 20);
            this.tbTime.TabIndex = 34;
            this.tbTime.Text = "300";
            // 
            // cbInverseLink
            // 
            this.cbInverseLink.AutoSize = true;
            this.cbInverseLink.Location = new System.Drawing.Point(14, 302);
            this.cbInverseLink.Name = "cbInverseLink";
            this.cbInverseLink.Size = new System.Drawing.Size(79, 17);
            this.cbInverseLink.TabIndex = 37;
            this.cbInverseLink.Text = "inverse link";
            this.cbInverseLink.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(140, 410);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(82, 20);
            this.textBox1.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Request count";
            // 
            // tbAverageQueueLength
            // 
            this.tbAverageQueueLength.Location = new System.Drawing.Point(137, 473);
            this.tbAverageQueueLength.Name = "tbAverageQueueLength";
            this.tbAverageQueueLength.ReadOnly = true;
            this.tbAverageQueueLength.Size = new System.Drawing.Size(82, 20);
            this.tbAverageQueueLength.TabIndex = 41;
            this.tbAverageQueueLength.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 477);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Avarage queue length";
            this.label7.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 441);
            this.Controls.Add(this.tbAverageQueueLength);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbInverseLink);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbAverageStayTime);
            this.Controls.Add(this.tbMaxQueueLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Service machine";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udRequestCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRight;
        private System.Windows.Forms.TextBox tbLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblθParam;
        private System.Windows.Forms.Label lblKParam;
        private System.Windows.Forms.TextBox tbθParam;
        private System.Windows.Forms.TextBox tbKParam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbAverageStayTime;
        private System.Windows.Forms.TextBox tbMaxQueueLength;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbCount;
        private System.Windows.Forms.RadioButton rbTime;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.NumericUpDown udRequestCount;
        private System.Windows.Forms.CheckBox cbInverseLink;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTimeStep;
        private System.Windows.Forms.TextBox tbAverageQueueLength;
        private System.Windows.Forms.Label label7;
    }
}

