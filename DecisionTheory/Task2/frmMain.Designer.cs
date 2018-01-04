namespace Task2
{
	partial class frmMain
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.udVariableCount = new System.Windows.Forms.NumericUpDown();
			this.tbFormula = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.tbMinimumVariables = new System.Windows.Forms.TextBox();
			this.tbMinimum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.udRepeatCount = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.tbMutationProbability = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.udPopulationSize = new System.Windows.Forms.NumericUpDown();
			this.tbStartPoint = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tbAreaSemiwidth = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.tbChildrenRatio = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbMutationOffset = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.udVariableCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udRepeatCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udPopulationSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Formula";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Variable Count";
			// 
			// udVariableCount
			// 
			this.udVariableCount.Location = new System.Drawing.Point(140, 21);
			this.udVariableCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.udVariableCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.udVariableCount.Name = "udVariableCount";
			this.udVariableCount.Size = new System.Drawing.Size(192, 20);
			this.udVariableCount.TabIndex = 5;
			this.udVariableCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			// 
			// tbFormula
			// 
			this.tbFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbFormula.Location = new System.Drawing.Point(140, 47);
			this.tbFormula.Multiline = true;
			this.tbFormula.Name = "tbFormula";
			this.tbFormula.Size = new System.Drawing.Size(192, 112);
			this.tbFormula.TabIndex = 4;
			this.tbFormula.Text = "X[0]*X[0] + X[1]*X[1] + X[2]*X[2] + X[3]*X[3] + X[4]*X[4] + X[5]*X[5]";
			this.tbFormula.Leave += new System.EventHandler(this.tbFormula_Leave);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(14, 232);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Population Size";
			// 
			// btnCalculate
			// 
			this.btnCalculate.Location = new System.Drawing.Point(140, 420);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(192, 33);
			this.btnCalculate.TabIndex = 14;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// tbMinimumVariables
			// 
			this.tbMinimumVariables.Location = new System.Drawing.Point(140, 394);
			this.tbMinimumVariables.Name = "tbMinimumVariables";
			this.tbMinimumVariables.ReadOnly = true;
			this.tbMinimumVariables.Size = new System.Drawing.Size(192, 20);
			this.tbMinimumVariables.TabIndex = 18;
			// 
			// tbMinimum
			// 
			this.tbMinimum.Location = new System.Drawing.Point(140, 368);
			this.tbMinimum.Name = "tbMinimum";
			this.tbMinimum.ReadOnly = true;
			this.tbMinimum.Size = new System.Drawing.Size(192, 20);
			this.tbMinimum.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 397);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Minimum variables";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 371);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 15;
			this.label3.Text = "Minimum";
			// 
			// udRepeatCount
			// 
			this.udRepeatCount.Location = new System.Drawing.Point(140, 342);
			this.udRepeatCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.udRepeatCount.Name = "udRepeatCount";
			this.udRepeatCount.Size = new System.Drawing.Size(192, 20);
			this.udRepeatCount.TabIndex = 22;
			this.udRepeatCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.udRepeatCount.ValueChanged += new System.EventHandler(this.udRepeatCount_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 344);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Max Repeat Count";
			// 
			// tbMutationProbability
			// 
			this.tbMutationProbability.Location = new System.Drawing.Point(140, 284);
			this.tbMutationProbability.Name = "tbMutationProbability";
			this.tbMutationProbability.Size = new System.Drawing.Size(192, 20);
			this.tbMutationProbability.TabIndex = 19;
			this.tbMutationProbability.Text = "0,4";
			this.tbMutationProbability.Leave += new System.EventHandler(this.tbMutationProbability_Leave);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(14, 287);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(98, 13);
			this.label7.TabIndex = 20;
			this.label7.Text = "Mutation probability";
			// 
			// udPopulationSize
			// 
			this.udPopulationSize.Location = new System.Drawing.Point(140, 232);
			this.udPopulationSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.udPopulationSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.udPopulationSize.Name = "udPopulationSize";
			this.udPopulationSize.Size = new System.Drawing.Size(192, 20);
			this.udPopulationSize.TabIndex = 23;
			this.udPopulationSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// tbStartPoint
			// 
			this.tbStartPoint.Location = new System.Drawing.Point(140, 180);
			this.tbStartPoint.Name = "tbStartPoint";
			this.tbStartPoint.Size = new System.Drawing.Size(192, 20);
			this.tbStartPoint.TabIndex = 25;
			this.tbStartPoint.Text = "5; 5; 5; 5; 5; 5";
			this.tbStartPoint.Leave += new System.EventHandler(this.tbStartPoint_Leave);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(14, 183);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 13);
			this.label8.TabIndex = 24;
			this.label8.Text = "Start Point";
			// 
			// tbAreaSemiwidth
			// 
			this.tbAreaSemiwidth.Location = new System.Drawing.Point(140, 206);
			this.tbAreaSemiwidth.Name = "tbAreaSemiwidth";
			this.tbAreaSemiwidth.Size = new System.Drawing.Size(192, 20);
			this.tbAreaSemiwidth.TabIndex = 27;
			this.tbAreaSemiwidth.Text = "50";
			this.tbAreaSemiwidth.Leave += new System.EventHandler(this.tbAreaSemiwidth_Leave);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(14, 209);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 13);
			this.label9.TabIndex = 26;
			this.label9.Text = "Area Semiwidth";
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(367, 21);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(341, 495);
			this.dataGridView1.TabIndex = 28;
			this.dataGridView1.Visible = false;
			// 
			// tbChildrenRatio
			// 
			this.tbChildrenRatio.Location = new System.Drawing.Point(140, 258);
			this.tbChildrenRatio.Name = "tbChildrenRatio";
			this.tbChildrenRatio.Size = new System.Drawing.Size(192, 20);
			this.tbChildrenRatio.TabIndex = 29;
			this.tbChildrenRatio.Text = "1,5";
			this.tbChildrenRatio.Leave += new System.EventHandler(this.tbChildrenRatio_Leave);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(14, 261);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(68, 13);
			this.label10.TabIndex = 30;
			this.label10.Text = "Children ratio";
			// 
			// tbMutationOffset
			// 
			this.tbMutationOffset.Location = new System.Drawing.Point(140, 310);
			this.tbMutationOffset.Name = "tbMutationOffset";
			this.tbMutationOffset.Size = new System.Drawing.Size(192, 20);
			this.tbMutationOffset.TabIndex = 31;
			this.tbMutationOffset.Text = "0,5";
			this.tbMutationOffset.Leave += new System.EventHandler(this.tbMutationOffset_Leave);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(14, 313);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(77, 13);
			this.label11.TabIndex = 32;
			this.label11.Text = "Mutation offset";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(354, 538);
			this.Controls.Add(this.tbMutationOffset);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.tbChildrenRatio);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.tbAreaSemiwidth);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.tbStartPoint);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.udPopulationSize);
			this.Controls.Add(this.udRepeatCount);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbMutationProbability);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.tbMinimumVariables);
			this.Controls.Add(this.tbMinimum);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnCalculate);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.udVariableCount);
			this.Controls.Add(this.tbFormula);
			this.Name = "frmMain";
			this.Text = "Task2";
			this.Load += new System.EventHandler(this.frmMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.udVariableCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udRepeatCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udPopulationSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown udVariableCount;
		private System.Windows.Forms.TextBox tbFormula;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.TextBox tbMinimumVariables;
		private System.Windows.Forms.TextBox tbMinimum;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udRepeatCount;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbMutationProbability;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown udPopulationSize;
		private System.Windows.Forms.TextBox tbStartPoint;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbAreaSemiwidth;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox tbChildrenRatio;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbMutationOffset;
		private System.Windows.Forms.Label label11;
	}
}

