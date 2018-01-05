namespace Task2UI
{
	partial class form1
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
			this.ndaViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			this.lblInputGrammar = new System.Windows.Forms.Label();
			this.tbInputGrammar = new System.Windows.Forms.TextBox();
			this.tbOutputGrammar = new System.Windows.Forms.TextBox();
			this.lblOutputGrammar = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tbSolution = new System.Windows.Forms.TextBox();
			this.btnSolve = new System.Windows.Forms.Button();
			this.tbCoefficients = new System.Windows.Forms.TextBox();
			this.btnLoadGrammar = new System.Windows.Forms.Button();
			this.ofgOpenGrammar = new System.Windows.Forms.OpenFileDialog();
			this.tbInputString = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnCheckAccessory = new System.Windows.Forms.Button();
			this.lblAnswer = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ndaViewer
			// 
			this.ndaViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ndaViewer.AsyncLayout = false;
			this.ndaViewer.AutoScroll = true;
			this.ndaViewer.BackColor = System.Drawing.Color.Lavender;
			this.ndaViewer.BackwardEnabled = false;
			this.ndaViewer.ForwardEnabled = false;
			this.ndaViewer.Graph = null;
			this.ndaViewer.Location = new System.Drawing.Point(525, 32);
			this.ndaViewer.Margin = new System.Windows.Forms.Padding(10);
			this.ndaViewer.MouseHitDistance = 0.05D;
			this.ndaViewer.Name = "ndaViewer";
			this.ndaViewer.NavigationVisible = true;
			this.ndaViewer.PanButtonPressed = false;
			this.ndaViewer.SaveButtonVisible = true;
			this.ndaViewer.Size = new System.Drawing.Size(536, 588);
			this.ndaViewer.TabIndex = 0;
			this.ndaViewer.ZoomF = 1D;
			this.ndaViewer.ZoomWindowThreshold = 0.05D;
			// 
			// lblInputGrammar
			// 
			this.lblInputGrammar.AutoSize = true;
			this.lblInputGrammar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblInputGrammar.Location = new System.Drawing.Point(12, 9);
			this.lblInputGrammar.Name = "lblInputGrammar";
			this.lblInputGrammar.Size = new System.Drawing.Size(103, 18);
			this.lblInputGrammar.TabIndex = 1;
			this.lblInputGrammar.Text = "Input grammar";
			// 
			// tbInputGrammar
			// 
			this.tbInputGrammar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbInputGrammar.Location = new System.Drawing.Point(12, 32);
			this.tbInputGrammar.Multiline = true;
			this.tbInputGrammar.Name = "tbInputGrammar";
			this.tbInputGrammar.Size = new System.Drawing.Size(241, 153);
			this.tbInputGrammar.TabIndex = 2;
			this.tbInputGrammar.Text = "0, 1;\r\nS, A, B;\r\nS → 0∙A|1∙S|λ,\r\nA → 0∙B|1∙A,\r\nB → 0∙S|1∙B;\r\nS";
			// 
			// tbOutputGrammar
			// 
			this.tbOutputGrammar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbOutputGrammar.Location = new System.Drawing.Point(15, 217);
			this.tbOutputGrammar.Multiline = true;
			this.tbOutputGrammar.Name = "tbOutputGrammar";
			this.tbOutputGrammar.ReadOnly = true;
			this.tbOutputGrammar.Size = new System.Drawing.Size(238, 153);
			this.tbOutputGrammar.TabIndex = 4;
			// 
			// lblOutputGrammar
			// 
			this.lblOutputGrammar.AutoSize = true;
			this.lblOutputGrammar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOutputGrammar.Location = new System.Drawing.Point(12, 196);
			this.lblOutputGrammar.Name = "lblOutputGrammar";
			this.lblOutputGrammar.Size = new System.Drawing.Size(116, 18);
			this.lblOutputGrammar.TabIndex = 3;
			this.lblOutputGrammar.Text = "Output grammar";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(278, 196);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 18);
			this.label1.TabIndex = 5;
			this.label1.Text = "Solution";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(278, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(153, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Coefficients in system";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(522, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 18);
			this.label3.TabIndex = 9;
			this.label3.Text = "NFA";
			// 
			// tbSolution
			// 
			this.tbSolution.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbSolution.Location = new System.Drawing.Point(278, 217);
			this.tbSolution.Multiline = true;
			this.tbSolution.Name = "tbSolution";
			this.tbSolution.ReadOnly = true;
			this.tbSolution.Size = new System.Drawing.Size(231, 153);
			this.tbSolution.TabIndex = 10;
			// 
			// btnSolve
			// 
			this.btnSolve.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSolve.Location = new System.Drawing.Point(12, 388);
			this.btnSolve.Name = "btnSolve";
			this.btnSolve.Size = new System.Drawing.Size(241, 39);
			this.btnSolve.TabIndex = 12;
			this.btnSolve.Text = "Solve System && Build NFA";
			this.btnSolve.UseVisualStyleBackColor = true;
			this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
			// 
			// tbCoefficients
			// 
			this.tbCoefficients.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbCoefficients.Location = new System.Drawing.Point(278, 32);
			this.tbCoefficients.Multiline = true;
			this.tbCoefficients.Name = "tbCoefficients";
			this.tbCoefficients.ReadOnly = true;
			this.tbCoefficients.Size = new System.Drawing.Size(231, 153);
			this.tbCoefficients.TabIndex = 13;
			// 
			// btnLoadGrammar
			// 
			this.btnLoadGrammar.Location = new System.Drawing.Point(121, 8);
			this.btnLoadGrammar.Name = "btnLoadGrammar";
			this.btnLoadGrammar.Size = new System.Drawing.Size(42, 23);
			this.btnLoadGrammar.TabIndex = 14;
			this.btnLoadGrammar.Text = "Load";
			this.btnLoadGrammar.UseVisualStyleBackColor = true;
			this.btnLoadGrammar.Click += new System.EventHandler(this.btnLoadGrammar_Click);
			// 
			// tbInputString
			// 
			this.tbInputString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbInputString.Location = new System.Drawing.Point(110, 453);
			this.tbInputString.Name = "tbInputString";
			this.tbInputString.Size = new System.Drawing.Size(399, 26);
			this.tbInputString.TabIndex = 15;
			this.tbInputString.Text = "0.1.1.0.0.1.1.1.0.0.0.0.1.0.0.1";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(12, 457);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 18);
			this.label5.TabIndex = 17;
			this.label5.Text = "Expression";
			// 
			// btnCheckAccessory
			// 
			this.btnCheckAccessory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCheckAccessory.Location = new System.Drawing.Point(12, 499);
			this.btnCheckAccessory.Name = "btnCheckAccessory";
			this.btnCheckAccessory.Size = new System.Drawing.Size(241, 37);
			this.btnCheckAccessory.TabIndex = 18;
			this.btnCheckAccessory.Text = "Check Matching";
			this.btnCheckAccessory.UseVisualStyleBackColor = true;
			this.btnCheckAccessory.Click += new System.EventHandler(this.btnCheckAccessory_Click);
			// 
			// lblAnswer
			// 
			this.lblAnswer.AutoSize = true;
			this.lblAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblAnswer.Location = new System.Drawing.Point(12, 553);
			this.lblAnswer.Name = "lblAnswer";
			this.lblAnswer.Size = new System.Drawing.Size(86, 20);
			this.lblAnswer.TabIndex = 19;
			this.lblAnswer.Text = "lblAnswer";
			this.lblAnswer.Visible = false;
			// 
			// form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1074, 633);
			this.Controls.Add(this.lblAnswer);
			this.Controls.Add(this.btnCheckAccessory);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbInputString);
			this.Controls.Add(this.btnLoadGrammar);
			this.Controls.Add(this.tbCoefficients);
			this.Controls.Add(this.btnSolve);
			this.Controls.Add(this.tbSolution);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbOutputGrammar);
			this.Controls.Add(this.lblOutputGrammar);
			this.Controls.Add(this.tbInputGrammar);
			this.Controls.Add(this.lblInputGrammar);
			this.Controls.Add(this.ndaViewer);
			this.Name = "form1";
			this.Text = "Task2. Regular Expressions";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Microsoft.Msagl.GraphViewerGdi.GViewer ndaViewer;
		private System.Windows.Forms.Label lblInputGrammar;
		private System.Windows.Forms.TextBox tbInputGrammar;
		private System.Windows.Forms.TextBox tbOutputGrammar;
		private System.Windows.Forms.Label lblOutputGrammar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbSolution;
		private System.Windows.Forms.Button btnSolve;
		private System.Windows.Forms.TextBox tbCoefficients;
		private System.Windows.Forms.Button btnLoadGrammar;
		private System.Windows.Forms.OpenFileDialog ofgOpenGrammar;
		private System.Windows.Forms.TextBox tbInputString;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnCheckAccessory;
		private System.Windows.Forms.Label lblAnswer;
	}
}

