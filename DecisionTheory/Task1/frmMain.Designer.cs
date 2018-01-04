namespace Task1
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tbFormula = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tbMinimum = new System.Windows.Forms.TextBox();
			this.tbMinimumVariables = new System.Windows.Forms.TextBox();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.tbEpsilon = new System.Windows.Forms.TextBox();
			this.tbStartPoint = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.clmnVariables = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.clmnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tbDeriveStep = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.udVariableCount = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udVariableCount)).BeginInit();
			this.SuspendLayout();
			// 
			// tbFormula
			// 
			this.tbFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbFormula.Location = new System.Drawing.Point(138, 46);
			this.tbFormula.Multiline = true;
			this.tbFormula.Name = "tbFormula";
			this.tbFormula.Size = new System.Drawing.Size(192, 112);
			this.tbFormula.TabIndex = 0;
			this.tbFormula.Text = "X[0]*X[0] + X[1]*X[1] + X[2]*X[2] + X[3]*X[3] + X[4]*X[4] + X[5]*X[5]";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Formula";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 303);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Minimum";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 329);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Minimum variables";
			// 
			// tbMinimum
			// 
			this.tbMinimum.Location = new System.Drawing.Point(138, 300);
			this.tbMinimum.Name = "tbMinimum";
			this.tbMinimum.ReadOnly = true;
			this.tbMinimum.Size = new System.Drawing.Size(192, 20);
			this.tbMinimum.TabIndex = 6;
			// 
			// tbMinimumVariables
			// 
			this.tbMinimumVariables.Location = new System.Drawing.Point(138, 326);
			this.tbMinimumVariables.Name = "tbMinimumVariables";
			this.tbMinimumVariables.ReadOnly = true;
			this.tbMinimumVariables.Size = new System.Drawing.Size(192, 20);
			this.tbMinimumVariables.TabIndex = 7;
			// 
			// btnCalculate
			// 
			this.btnCalculate.Location = new System.Drawing.Point(138, 365);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(192, 33);
			this.btnCalculate.TabIndex = 8;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// tbEpsilon
			// 
			this.tbEpsilon.Location = new System.Drawing.Point(138, 237);
			this.tbEpsilon.Name = "tbEpsilon";
			this.tbEpsilon.Size = new System.Drawing.Size(192, 20);
			this.tbEpsilon.TabIndex = 12;
			this.tbEpsilon.Text = "0,001";
			// 
			// tbStartPoint
			// 
			this.tbStartPoint.Location = new System.Drawing.Point(138, 174);
			this.tbStartPoint.Name = "tbStartPoint";
			this.tbStartPoint.Size = new System.Drawing.Size(192, 20);
			this.tbStartPoint.TabIndex = 11;
			this.tbStartPoint.Text = "5; 5; 5; 5; 5; 5";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 240);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Epsilon";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 177);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Start Point";
			// 
			// dataGridView1
			// 
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnVariables,
            this.clmnValue});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView1.Location = new System.Drawing.Point(366, 22);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView1.Size = new System.Drawing.Size(299, 326);
			this.dataGridView1.TabIndex = 13;
			this.dataGridView1.Visible = false;
			// 
			// clmnVariables
			// 
			this.clmnVariables.HeaderText = "Variables";
			this.clmnVariables.Name = "clmnVariables";
			this.clmnVariables.ReadOnly = true;
			this.clmnVariables.Width = 150;
			// 
			// clmnValue
			// 
			this.clmnValue.HeaderText = "Value";
			this.clmnValue.Name = "clmnValue";
			this.clmnValue.ReadOnly = true;
			// 
			// tbDeriveStep
			// 
			this.tbDeriveStep.Location = new System.Drawing.Point(138, 263);
			this.tbDeriveStep.Name = "tbDeriveStep";
			this.tbDeriveStep.Size = new System.Drawing.Size(192, 20);
			this.tbDeriveStep.TabIndex = 15;
			this.tbDeriveStep.Text = "0,0001";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 266);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Derive Step";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Variable Count";
			// 
			// udVariableCount
			// 
			this.udVariableCount.Location = new System.Drawing.Point(138, 20);
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
			this.udVariableCount.TabIndex = 1;
			this.udVariableCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(350, 414);
			this.Controls.Add(this.tbDeriveStep);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.tbEpsilon);
			this.Controls.Add(this.tbStartPoint);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnCalculate);
			this.Controls.Add(this.tbMinimumVariables);
			this.Controls.Add(this.tbMinimum);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.udVariableCount);
			this.Controls.Add(this.tbFormula);
			this.Name = "frmMain";
			this.Text = "Polydimensional optimization";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udVariableCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbFormula;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbMinimum;
		private System.Windows.Forms.TextBox tbMinimumVariables;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.TextBox tbEpsilon;
		private System.Windows.Forms.TextBox tbStartPoint;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn clmnVariables;
		private System.Windows.Forms.DataGridViewTextBoxColumn clmnValue;
		private System.Windows.Forms.TextBox tbDeriveStep;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown udVariableCount;
	}
}

