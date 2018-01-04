namespace Proj6
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
            this.zgc = new ZedGraph.ZedGraphControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.tbNodeCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbT0 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbF0 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRadius = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAlfa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLambdaN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLambda0 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // zgc
            // 
            this.zgc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgc.BackColor = System.Drawing.Color.PaleGreen;
            this.zgc.IsShowCursorValues = true;
            this.zgc.Location = new System.Drawing.Point(12, 12);
            this.zgc.Name = "zgc";
            this.zgc.ScrollGrace = 0;
            this.zgc.ScrollMaxX = 0;
            this.zgc.ScrollMaxY = 0;
            this.zgc.ScrollMaxY2 = 0;
            this.zgc.ScrollMinX = 0;
            this.zgc.ScrollMinY = 0;
            this.zgc.ScrollMinY2 = 0;
            this.zgc.Size = new System.Drawing.Size(752, 582);
            this.zgc.TabIndex = 25;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbNodeCount);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tbT0);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbF0);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbRadius);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbLength);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbAlfa);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbLambdaN);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbLambda0);
            this.panel1.Location = new System.Drawing.Point(770, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 582);
            this.panel1.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "N:";
            // 
            // tbNodeCount
            // 
            this.tbNodeCount.Location = new System.Drawing.Point(80, 206);
            this.tbNodeCount.Name = "tbNodeCount";
            this.tbNodeCount.Size = new System.Drawing.Size(103, 20);
            this.tbNodeCount.TabIndex = 32;
            this.tbNodeCount.Text = "30";
            this.tbNodeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "T0:";
            // 
            // tbT0
            // 
            this.tbT0.Location = new System.Drawing.Point(80, 180);
            this.tbT0.Name = "tbT0";
            this.tbT0.Size = new System.Drawing.Size(103, 20);
            this.tbT0.TabIndex = 30;
            this.tbT0.Text = "300";
            this.tbT0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "F0:";
            // 
            // tbF0
            // 
            this.tbF0.Location = new System.Drawing.Point(80, 154);
            this.tbF0.Name = "tbF0";
            this.tbF0.Size = new System.Drawing.Size(103, 20);
            this.tbF0.TabIndex = 28;
            this.tbF0.Text = "100";
            this.tbF0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "R:";
            // 
            // tbRadius
            // 
            this.tbRadius.Location = new System.Drawing.Point(80, 128);
            this.tbRadius.Name = "tbRadius";
            this.tbRadius.Size = new System.Drawing.Size(103, 20);
            this.tbRadius.TabIndex = 26;
            this.tbRadius.Text = "0,5";
            this.tbRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "L:";
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(80, 102);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(103, 20);
            this.tbLength.TabIndex = 24;
            this.tbLength.Text = "10";
            this.tbLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "α:";
            // 
            // tbAlfa
            // 
            this.tbAlfa.Location = new System.Drawing.Point(80, 76);
            this.tbAlfa.Name = "tbAlfa";
            this.tbAlfa.Size = new System.Drawing.Size(103, 20);
            this.tbAlfa.TabIndex = 22;
            this.tbAlfa.Text = "0,01";
            this.tbAlfa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "λN:";
            // 
            // tbLambdaN
            // 
            this.tbLambdaN.Location = new System.Drawing.Point(80, 50);
            this.tbLambdaN.Name = "tbLambdaN";
            this.tbLambdaN.Size = new System.Drawing.Size(103, 20);
            this.tbLambdaN.TabIndex = 20;
            this.tbLambdaN.Text = "0,05";
            this.tbLambdaN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "λ0:";
            // 
            // tbLambda0
            // 
            this.tbLambda0.Location = new System.Drawing.Point(80, 24);
            this.tbLambda0.Name = "tbLambda0";
            this.tbLambda0.Size = new System.Drawing.Size(103, 20);
            this.tbLambda0.TabIndex = 18;
            this.tbLambda0.Text = "0,1";
            this.tbLambda0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 33);
            this.button1.TabIndex = 34;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 602);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.zgc);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbNodeCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbT0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbF0;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRadius;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAlfa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLambdaN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLambda0;
        private System.Windows.Forms.Button button1;

    }
}

