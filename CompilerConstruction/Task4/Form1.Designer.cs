namespace Task4
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.label1 = new System.Windows.Forms.Label();
			this.tbExpression = new System.Windows.Forms.TextBox();
			this.Viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			this.label2 = new System.Windows.Forms.Label();
			this.lblAnswer = new System.Windows.Forms.Label();
			this.btnCheckAccessory = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 463);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Expression";
			// 
			// tbExpression
			// 
			this.tbExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbExpression.Location = new System.Drawing.Point(13, 486);
			this.tbExpression.Multiline = true;
			this.tbExpression.Name = "tbExpression";
			this.tbExpression.Size = new System.Drawing.Size(317, 68);
			this.tbExpression.TabIndex = 1;
			this.tbExpression.Text = "+ a * 5 <= not ( 15 + b )";
			// 
			// Viewer
			// 
			this.Viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Viewer.AsyncLayout = false;
			this.Viewer.AutoScroll = true;
			this.Viewer.BackColor = System.Drawing.Color.FloralWhite;
			this.Viewer.BackwardEnabled = false;
			this.Viewer.ForwardEnabled = false;
			this.Viewer.Graph = null;
			this.Viewer.Location = new System.Drawing.Point(336, 32);
			this.Viewer.MouseHitDistance = 0.05D;
			this.Viewer.Name = "Viewer";
			this.Viewer.NavigationVisible = true;
			this.Viewer.PanButtonPressed = false;
			this.Viewer.SaveButtonVisible = true;
			this.Viewer.Size = new System.Drawing.Size(549, 609);
			this.Viewer.TabIndex = 2;
			this.Viewer.ZoomF = 1D;
			this.Viewer.ZoomWindowThreshold = 0.05D;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(332, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "Parsing Tree";
			// 
			// lblAnswer
			// 
			this.lblAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblAnswer.AutoSize = true;
			this.lblAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblAnswer.Location = new System.Drawing.Point(12, 611);
			this.lblAnswer.Name = "lblAnswer";
			this.lblAnswer.Size = new System.Drawing.Size(86, 20);
			this.lblAnswer.TabIndex = 21;
			this.lblAnswer.Text = "lblAnswer";
			this.lblAnswer.Visible = false;
			// 
			// btnCheckAccessory
			// 
			this.btnCheckAccessory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCheckAccessory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCheckAccessory.Location = new System.Drawing.Point(13, 560);
			this.btnCheckAccessory.Name = "btnCheckAccessory";
			this.btnCheckAccessory.Size = new System.Drawing.Size(317, 35);
			this.btnCheckAccessory.TabIndex = 20;
			this.btnCheckAccessory.Text = "Check derivability";
			this.btnCheckAccessory.UseVisualStyleBackColor = true;
			this.btnCheckAccessory.Click += new System.EventHandler(this.btnCheckAccessory_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 20);
			this.label3.TabIndex = 22;
			this.label3.Text = "Grammar";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(12, 32);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(318, 412);
			this.textBox1.TabIndex = 23;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(897, 653);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblAnswer);
			this.Controls.Add(this.btnCheckAccessory);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Viewer);
			this.Controls.Add(this.tbExpression);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Task4. Recursive Descent Algorithm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbExpression;
		private Microsoft.Msagl.GraphViewerGdi.GViewer Viewer;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblAnswer;
		private System.Windows.Forms.Button btnCheckAccessory;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
	}
}

