namespace Project1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.udStateCount = new System.Windows.Forms.NumericUpDown();
            this.btnCalcul = new System.Windows.Forms.Button();
            this.dgvTransits = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udLinkCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLinkDensity = new System.Windows.Forms.TextBox();
            this.cbSelfPoint = new System.Windows.Forms.CheckBox();
            this.btnLockVertexCount = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnLockLinkCount = new System.Windows.Forms.Button();
            this.btnLockSelfPoint = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dgvAnswer = new System.Windows.Forms.DataGridView();
            this.columnProbab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gViewer1 = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.lblConnect = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.udStateCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLinkCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswer)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // udStateCount
            // 
            this.udStateCount.Location = new System.Drawing.Point(119, 260);
            this.udStateCount.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.udStateCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udStateCount.Name = "udStateCount";
            this.udStateCount.Size = new System.Drawing.Size(70, 20);
            this.udStateCount.TabIndex = 1;
            this.udStateCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.udStateCount.ValueChanged += new System.EventHandler(this.udStateCount_ValueChanged);
            // 
            // btnCalcul
            // 
            this.btnCalcul.Location = new System.Drawing.Point(295, 250);
            this.btnCalcul.Name = "btnCalcul";
            this.btnCalcul.Size = new System.Drawing.Size(127, 25);
            this.btnCalcul.TabIndex = 2;
            this.btnCalcul.Text = "Calculate";
            this.btnCalcul.UseVisualStyleBackColor = true;
            this.btnCalcul.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvTransits
            // 
            this.dgvTransits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransits.Location = new System.Drawing.Point(12, 12);
            this.dgvTransits.Name = "dgvTransits";
            this.dgvTransits.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTransits.Size = new System.Drawing.Size(277, 232);
            this.dgvTransits.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(295, 312);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "Randomize";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "State count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Transition count";
            // 
            // udLinkCount
            // 
            this.udLinkCount.Location = new System.Drawing.Point(119, 286);
            this.udLinkCount.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.udLinkCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udLinkCount.Name = "udLinkCount";
            this.udLinkCount.Size = new System.Drawing.Size(70, 20);
            this.udLinkCount.TabIndex = 9;
            this.udLinkCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.udLinkCount.ValueChanged += new System.EventHandler(this.cbSelfPoint_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Transition density";
            // 
            // tbLinkDensity
            // 
            this.tbLinkDensity.Location = new System.Drawing.Point(119, 312);
            this.tbLinkDensity.Name = "tbLinkDensity";
            this.tbLinkDensity.ReadOnly = true;
            this.tbLinkDensity.Size = new System.Drawing.Size(70, 20);
            this.tbLinkDensity.TabIndex = 11;
            // 
            // cbSelfPoint
            // 
            this.cbSelfPoint.AutoSize = true;
            this.cbSelfPoint.Location = new System.Drawing.Point(82, 384);
            this.cbSelfPoint.Name = "cbSelfPoint";
            this.cbSelfPoint.Size = new System.Drawing.Size(75, 17);
            this.cbSelfPoint.TabIndex = 12;
            this.cbSelfPoint.Text = "Self transit";
            this.cbSelfPoint.UseVisualStyleBackColor = true;
            this.cbSelfPoint.Visible = false;
            this.cbSelfPoint.CheckedChanged += new System.EventHandler(this.cbSelfPoint_CheckedChanged);
            // 
            // btnLockVertexCount
            // 
            this.btnLockVertexCount.ImageIndex = 0;
            this.btnLockVertexCount.ImageList = this.imageList1;
            this.btnLockVertexCount.Location = new System.Drawing.Point(194, 258);
            this.btnLockVertexCount.Name = "btnLockVertexCount";
            this.btnLockVertexCount.Size = new System.Drawing.Size(40, 20);
            this.btnLockVertexCount.TabIndex = 13;
            this.btnLockVertexCount.UseVisualStyleBackColor = true;
            this.btnLockVertexCount.Click += new System.EventHandler(this.btnLockVertexCount_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lock.png");
            this.imageList1.Images.SetKeyName(1, "unlock.png");
            // 
            // btnLockLinkCount
            // 
            this.btnLockLinkCount.ImageIndex = 0;
            this.btnLockLinkCount.ImageList = this.imageList1;
            this.btnLockLinkCount.Location = new System.Drawing.Point(194, 284);
            this.btnLockLinkCount.Name = "btnLockLinkCount";
            this.btnLockLinkCount.Size = new System.Drawing.Size(40, 20);
            this.btnLockLinkCount.TabIndex = 14;
            this.btnLockLinkCount.UseVisualStyleBackColor = true;
            this.btnLockLinkCount.Click += new System.EventHandler(this.btnLockVertexCount_Click);
            // 
            // btnLockSelfPoint
            // 
            this.btnLockSelfPoint.ImageIndex = 0;
            this.btnLockSelfPoint.ImageList = this.imageList1;
            this.btnLockSelfPoint.Location = new System.Drawing.Point(157, 381);
            this.btnLockSelfPoint.Name = "btnLockSelfPoint";
            this.btnLockSelfPoint.Size = new System.Drawing.Size(40, 20);
            this.btnLockSelfPoint.TabIndex = 15;
            this.btnLockSelfPoint.UseVisualStyleBackColor = true;
            this.btnLockSelfPoint.Visible = false;
            this.btnLockSelfPoint.Click += new System.EventHandler(this.btnLockVertexCount_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(128, 423);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "0,5";
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(128, 448);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(70, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.Text = "16";
            this.textBox2.Visible = false;
            // 
            // dgvAnswer
            // 
            this.dgvAnswer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnswer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProbab});
            this.dgvAnswer.Location = new System.Drawing.Point(295, 12);
            this.dgvAnswer.Name = "dgvAnswer";
            this.dgvAnswer.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvAnswer.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAnswer.Size = new System.Drawing.Size(127, 232);
            this.dgvAnswer.TabIndex = 18;
            // 
            // columnProbab
            // 
            this.columnProbab.HeaderText = "Probab";
            this.columnProbab.Name = "columnProbab";
            this.columnProbab.ReadOnly = true;
            this.columnProbab.Width = 50;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer1.Location = new System.Drawing.Point(438, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gViewer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblConnect);
            this.splitContainer1.Size = new System.Drawing.Size(505, 496);
            this.splitContainer1.SplitterDistance = 461;
            this.splitContainer1.TabIndex = 20;
            // 
            // gViewer1
            // 
            this.gViewer1.AsyncLayout = false;
            this.gViewer1.AutoScroll = true;
            this.gViewer1.BackwardEnabled = false;
            this.gViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gViewer1.ForwardEnabled = false;
            this.gViewer1.Graph = null;
            this.gViewer1.Location = new System.Drawing.Point(0, 0);
            this.gViewer1.MouseHitDistance = 0.05;
            this.gViewer1.Name = "gViewer1";
            this.gViewer1.NavigationVisible = true;
            this.gViewer1.PanButtonPressed = false;
            this.gViewer1.SaveButtonVisible = true;
            this.gViewer1.Size = new System.Drawing.Size(505, 461);
            this.gViewer1.TabIndex = 7;
            this.gViewer1.ZoomF = 1;
            this.gViewer1.ZoomFraction = 0.5;
            this.gViewer1.ZoomWindowThreshold = 0.05;
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConnect.Location = new System.Drawing.Point(12, 7);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(92, 16);
            this.lblConnect.TabIndex = 20;
            this.lblConnect.Text = "Connectivity";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(293, 366);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 120);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(15, 85);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(64, 17);
            this.checkBox4.TabIndex = 25;
            this.checkBox4.Text = "Strongly";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(15, 62);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 17);
            this.checkBox3.TabIndex = 24;
            this.checkBox3.Text = "Conneted";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(15, 39);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(62, 17);
            this.checkBox2.TabIndex = 23;
            this.checkBox2.Text = "Weakly";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(15, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 17);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Not conneted";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 496);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.dgvAnswer);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnLockSelfPoint);
            this.Controls.Add(this.btnLockLinkCount);
            this.Controls.Add(this.btnLockVertexCount);
            this.Controls.Add(this.cbSelfPoint);
            this.Controls.Add(this.tbLinkDensity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.udLinkCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCalcul);
            this.Controls.Add(this.udStateCount);
            this.Controls.Add(this.dgvTransits);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udStateCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udLinkCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnswer)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown udStateCount;
        private System.Windows.Forms.Button btnCalcul;
        private System.Windows.Forms.DataGridView dgvTransits;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udLinkCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLinkDensity;
        private System.Windows.Forms.CheckBox cbSelfPoint;
        private System.Windows.Forms.Button btnLockVertexCount;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnLockLinkCount;
        private System.Windows.Forms.Button btnLockSelfPoint;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dgvAnswer;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProbab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Glee.GraphViewerGdi.GViewer gViewer1;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

