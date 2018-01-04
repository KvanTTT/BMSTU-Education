namespace Project3_Distributions
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.graphDistribFunc = new ZedGraph.ZedGraphControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.graphDensityFunc = new ZedGraph.ZedGraphControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblExperimVariance = new System.Windows.Forms.Label();
            this.lblTheorVariance = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblExperimMean = new System.Windows.Forms.Label();
            this.lblTheorMean = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.udCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblθParam = new System.Windows.Forms.Label();
            this.lblKParam = new System.Windows.Forms.Label();
            this.tbθParam = new System.Windows.Forms.TextBox();
            this.tbKParam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRight = new System.Windows.Forms.TextBox();
            this.tbLeft = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbErlang = new System.Windows.Forms.RadioButton();
            this.rbUniform = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.graphDistribFunc);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.graphDensityFunc);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 530);
            this.panel1.TabIndex = 5;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(809, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(107, 59);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // graphDistribFunc
            // 
            this.graphDistribFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphDistribFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.graphDistribFunc.IsAntiAlias = true;
            this.graphDistribFunc.Location = new System.Drawing.Point(395, 0);
            this.graphDistribFunc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphDistribFunc.Name = "graphDistribFunc";
            this.graphDistribFunc.ScrollGrace = 0D;
            this.graphDistribFunc.ScrollMaxX = 0D;
            this.graphDistribFunc.ScrollMaxY = 0D;
            this.graphDistribFunc.ScrollMaxY2 = 0D;
            this.graphDistribFunc.ScrollMinX = 0D;
            this.graphDistribFunc.ScrollMinY = 0D;
            this.graphDistribFunc.ScrollMinY2 = 0D;
            this.graphDistribFunc.Size = new System.Drawing.Size(414, 530);
            this.graphDistribFunc.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(393, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 530);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // graphDensityFunc
            // 
            this.graphDensityFunc.Dock = System.Windows.Forms.DockStyle.Left;
            this.graphDensityFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.graphDensityFunc.IsAntiAlias = true;
            this.graphDensityFunc.Location = new System.Drawing.Point(0, 0);
            this.graphDensityFunc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphDensityFunc.Name = "graphDensityFunc";
            this.graphDensityFunc.ScrollGrace = 0D;
            this.graphDensityFunc.ScrollMaxX = 0D;
            this.graphDensityFunc.ScrollMaxY = 0D;
            this.graphDensityFunc.ScrollMaxY2 = 0D;
            this.graphDensityFunc.ScrollMinX = 0D;
            this.graphDensityFunc.ScrollMinY = 0D;
            this.graphDensityFunc.ScrollMinY2 = 0D;
            this.graphDensityFunc.Size = new System.Drawing.Size(393, 530);
            this.graphDensityFunc.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(809, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(117, 530);
            this.panel2.TabIndex = 18;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblExperimVariance);
            this.groupBox5.Controls.Add(this.lblTheorVariance);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Location = new System.Drawing.Point(3, 311);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(104, 65);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Variance";
            // 
            // lblExperimVariance
            // 
            this.lblExperimVariance.AutoSize = true;
            this.lblExperimVariance.Location = new System.Drawing.Point(53, 41);
            this.lblExperimVariance.Name = "lblExperimVariance";
            this.lblExperimVariance.Size = new System.Drawing.Size(44, 13);
            this.lblExperimVariance.TabIndex = 23;
            this.lblExperimVariance.Text = "Experim";
            // 
            // lblTheorVariance
            // 
            this.lblTheorVariance.AutoSize = true;
            this.lblTheorVariance.Location = new System.Drawing.Point(53, 21);
            this.lblTheorVariance.Name = "lblTheorVariance";
            this.lblTheorVariance.Size = new System.Drawing.Size(35, 13);
            this.lblTheorVariance.TabIndex = 22;
            this.lblTheorVariance.Text = "Theor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Experim";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Theor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblExperimMean);
            this.groupBox4.Controls.Add(this.lblTheorMean);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(3, 246);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(104, 59);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mean";
            // 
            // lblExperimMean
            // 
            this.lblExperimMean.AutoSize = true;
            this.lblExperimMean.Location = new System.Drawing.Point(53, 40);
            this.lblExperimMean.Name = "lblExperimMean";
            this.lblExperimMean.Size = new System.Drawing.Size(44, 13);
            this.lblExperimMean.TabIndex = 21;
            this.lblExperimMean.Text = "Experim";
            // 
            // lblTheorMean
            // 
            this.lblTheorMean.AutoSize = true;
            this.lblTheorMean.Location = new System.Drawing.Point(53, 20);
            this.lblTheorMean.Name = "lblTheorMean";
            this.lblTheorMean.Size = new System.Drawing.Size(35, 13);
            this.lblTheorMean.TabIndex = 20;
            this.lblTheorMean.Text = "Theor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Experim";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Theor";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 30);
            this.button1.TabIndex = 20;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.udCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblθParam);
            this.groupBox1.Controls.Add(this.lblKParam);
            this.groupBox1.Controls.Add(this.tbθParam);
            this.groupBox1.Controls.Add(this.tbKParam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbRight);
            this.groupBox1.Controls.Add(this.tbLeft);
            this.groupBox1.Location = new System.Drawing.Point(3, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 157);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Params";
            // 
            // udCount
            // 
            this.udCount.Location = new System.Drawing.Point(37, 71);
            this.udCount.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.udCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udCount.Name = "udCount";
            this.udCount.Size = new System.Drawing.Size(61, 20);
            this.udCount.TabIndex = 21;
            this.udCount.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.udCount.Validated += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Count";
            // 
            // lblθParam
            // 
            this.lblθParam.AutoSize = true;
            this.lblθParam.Location = new System.Drawing.Point(3, 126);
            this.lblθParam.Name = "lblθParam";
            this.lblθParam.Size = new System.Drawing.Size(13, 13);
            this.lblθParam.TabIndex = 21;
            this.lblθParam.Text = "θ";
            // 
            // lblKParam
            // 
            this.lblKParam.AutoSize = true;
            this.lblKParam.Location = new System.Drawing.Point(3, 100);
            this.lblKParam.Name = "lblKParam";
            this.lblKParam.Size = new System.Drawing.Size(13, 13);
            this.lblKParam.TabIndex = 20;
            this.lblKParam.Text = "k";
            // 
            // tbθParam
            // 
            this.tbθParam.Location = new System.Drawing.Point(37, 123);
            this.tbθParam.Name = "tbθParam";
            this.tbθParam.Size = new System.Drawing.Size(61, 20);
            this.tbθParam.TabIndex = 19;
            this.tbθParam.Text = "1";
            // 
            // tbKParam
            // 
            this.tbKParam.Location = new System.Drawing.Point(37, 97);
            this.tbKParam.Name = "tbKParam";
            this.tbKParam.Size = new System.Drawing.Size(61, 20);
            this.tbKParam.TabIndex = 18;
            this.tbKParam.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Right";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Left";
            // 
            // tbRight
            // 
            this.tbRight.Location = new System.Drawing.Point(37, 45);
            this.tbRight.Name = "tbRight";
            this.tbRight.Size = new System.Drawing.Size(61, 20);
            this.tbRight.TabIndex = 15;
            this.tbRight.Text = "1";
            // 
            // tbLeft
            // 
            this.tbLeft.Location = new System.Drawing.Point(37, 19);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.Size = new System.Drawing.Size(61, 20);
            this.tbLeft.TabIndex = 14;
            this.tbLeft.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbErlang);
            this.groupBox2.Controls.Add(this.rbUniform);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(104, 74);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Distribution";
            // 
            // rbErlang
            // 
            this.rbErlang.AutoSize = true;
            this.rbErlang.Location = new System.Drawing.Point(6, 42);
            this.rbErlang.Name = "rbErlang";
            this.rbErlang.Size = new System.Drawing.Size(55, 17);
            this.rbErlang.TabIndex = 1;
            this.rbErlang.TabStop = true;
            this.rbErlang.Text = "Erlang";
            this.rbErlang.UseVisualStyleBackColor = true;
            // 
            // rbUniform
            // 
            this.rbUniform.AutoSize = true;
            this.rbUniform.Location = new System.Drawing.Point(6, 19);
            this.rbUniform.Name = "rbUniform";
            this.rbUniform.Size = new System.Drawing.Size(64, 17);
            this.rbUniform.TabIndex = 0;
            this.rbUniform.TabStop = true;
            this.rbUniform.Text = "Unitform";
            this.rbUniform.UseVisualStyleBackColor = true;
            this.rbUniform.CheckedChanged += new System.EventHandler(this.rbUniform_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 530);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Distributions";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private ZedGraph.ZedGraphControl graphDensityFunc;
        private ZedGraph.ZedGraphControl graphDistribFunc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRight;
        private System.Windows.Forms.TextBox tbLeft;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbErlang;
        private System.Windows.Forms.RadioButton rbUniform;
        private System.Windows.Forms.Label lblθParam;
        private System.Windows.Forms.Label lblKParam;
        private System.Windows.Forms.TextBox tbθParam;
        private System.Windows.Forms.TextBox tbKParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown udCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblExperimVariance;
        private System.Windows.Forms.Label lblTheorVariance;
        private System.Windows.Forms.Label lblExperimMean;
        private System.Windows.Forms.Label lblTheorMean;
    }
}

