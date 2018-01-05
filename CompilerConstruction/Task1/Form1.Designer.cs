namespace Task1
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
			this.btnLoadGrammar = new System.Windows.Forms.Button();
			this.tbInputGrammar = new System.Windows.Forms.TextBox();
			this.lblInputGrammar = new System.Windows.Forms.Label();
			this.btnSplit = new System.Windows.Forms.Button();
			this.tbResult = new System.Windows.Forms.TextBox();
			this.lblOutputGrammar = new System.Windows.Forms.Label();
			this.tbSplittingSymbols = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ofgOpenGrammar = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btnLoadGrammar
			// 
			this.btnLoadGrammar.Location = new System.Drawing.Point(121, 16);
			this.btnLoadGrammar.Name = "btnLoadGrammar";
			this.btnLoadGrammar.Size = new System.Drawing.Size(42, 23);
			this.btnLoadGrammar.TabIndex = 17;
			this.btnLoadGrammar.Text = "Load";
			this.btnLoadGrammar.UseVisualStyleBackColor = true;
			this.btnLoadGrammar.Click += new System.EventHandler(this.btnLoadGrammar_Click);
			// 
			// tbInputGrammar
			// 
			this.tbInputGrammar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tbInputGrammar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbInputGrammar.Location = new System.Drawing.Point(12, 40);
			this.tbInputGrammar.Multiline = true;
			this.tbInputGrammar.Name = "tbInputGrammar";
			this.tbInputGrammar.Size = new System.Drawing.Size(241, 207);
			this.tbInputGrammar.TabIndex = 16;
			this.tbInputGrammar.Text = "a, +, *, (, );\r\nE, T, F;\r\nE → E∙+∙T|T,\r\nT → T∙*∙F|F,\r\nF → (∙E∙)|a;\r\nE";
			// 
			// lblInputGrammar
			// 
			this.lblInputGrammar.AutoSize = true;
			this.lblInputGrammar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblInputGrammar.Location = new System.Drawing.Point(12, 17);
			this.lblInputGrammar.Name = "lblInputGrammar";
			this.lblInputGrammar.Size = new System.Drawing.Size(107, 18);
			this.lblInputGrammar.TabIndex = 15;
			this.lblInputGrammar.Text = "Input Grammar";
			// 
			// btnSplit
			// 
			this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSplit.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSplit.Location = new System.Drawing.Point(15, 299);
			this.btnSplit.Name = "btnSplit";
			this.btnSplit.Size = new System.Drawing.Size(241, 39);
			this.btnSplit.TabIndex = 18;
			this.btnSplit.Text = "Split!";
			this.btnSplit.UseVisualStyleBackColor = true;
			this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
			// 
			// tbResult
			// 
			this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbResult.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbResult.Location = new System.Drawing.Point(263, 40);
			this.tbResult.Multiline = true;
			this.tbResult.Name = "tbResult";
			this.tbResult.ReadOnly = true;
			this.tbResult.Size = new System.Drawing.Size(260, 298);
			this.tbResult.TabIndex = 20;
			// 
			// lblOutputGrammar
			// 
			this.lblOutputGrammar.AutoSize = true;
			this.lblOutputGrammar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblOutputGrammar.Location = new System.Drawing.Point(260, 19);
			this.lblOutputGrammar.Name = "lblOutputGrammar";
			this.lblOutputGrammar.Size = new System.Drawing.Size(50, 18);
			this.lblOutputGrammar.TabIndex = 19;
			this.lblOutputGrammar.Text = "Result";
			// 
			// tbSplittingSymbols
			// 
			this.tbSplittingSymbols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tbSplittingSymbols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbSplittingSymbols.Location = new System.Drawing.Point(15, 271);
			this.tbSplittingSymbols.Name = "tbSplittingSymbols";
			this.tbSplittingSymbols.Size = new System.Drawing.Size(241, 22);
			this.tbSplittingSymbols.TabIndex = 21;
			this.tbSplittingSymbols.Text = "E, T";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(15, 250);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151, 18);
			this.label1.TabIndex = 22;
			this.label1.Text = "Splitting Nonterminals";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 353);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbSplittingSymbols);
			this.Controls.Add(this.tbResult);
			this.Controls.Add(this.lblOutputGrammar);
			this.Controls.Add(this.btnSplit);
			this.Controls.Add(this.btnLoadGrammar);
			this.Controls.Add(this.tbInputGrammar);
			this.Controls.Add(this.lblInputGrammar);
			this.Name = "Form1";
			this.Text = "Task1 - Grammar Splitting";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadGrammar;
		private System.Windows.Forms.TextBox tbInputGrammar;
		private System.Windows.Forms.Label lblInputGrammar;
		private System.Windows.Forms.Button btnSplit;
		private System.Windows.Forms.TextBox tbResult;
		private System.Windows.Forms.Label lblOutputGrammar;
		private System.Windows.Forms.TextBox tbSplittingSymbols;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog ofgOpenGrammar;
	}
}

