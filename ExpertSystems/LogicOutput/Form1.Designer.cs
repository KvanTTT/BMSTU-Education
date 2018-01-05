namespace Lab1
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
            this.gbxRules = new System.Windows.Forms.GroupBox();
            this.btnRuleFromFile = new System.Windows.Forms.Button();
            this.lvwRules = new System.Windows.Forms.ListBox();
            this.tbxRules = new System.Windows.Forms.TextBox();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.gbxFacts = new System.Windows.Forms.GroupBox();
            this.lvwFacts = new System.Windows.Forms.ListBox();
            this.btnFactFromFile = new System.Windows.Forms.Button();
            this.btnAddFact = new System.Windows.Forms.Button();
            this.tbxFacts = new System.Windows.Forms.TextBox();
            this.ofdFacts = new System.Windows.Forms.OpenFileDialog();
            this.ofdRules = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvwResults = new System.Windows.Forms.ListBox();
            this.btnCheckGoals = new System.Windows.Forms.Button();
            this.tbxGoal = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLoadRequests = new System.Windows.Forms.Button();
            this.lbRequests = new System.Windows.Forms.ListBox();
            this.ofdRequests = new System.Windows.Forms.OpenFileDialog();
            this.gbxRules.SuspendLayout();
            this.gbxFacts.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxRules
            // 
            this.gbxRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbxRules.Controls.Add(this.btnRuleFromFile);
            this.gbxRules.Controls.Add(this.lvwRules);
            this.gbxRules.Controls.Add(this.tbxRules);
            this.gbxRules.Controls.Add(this.btnAddRule);
            this.gbxRules.Location = new System.Drawing.Point(12, 250);
            this.gbxRules.Name = "gbxRules";
            this.gbxRules.Size = new System.Drawing.Size(216, 226);
            this.gbxRules.TabIndex = 0;
            this.gbxRules.TabStop = false;
            this.gbxRules.Text = "Rools";
            // 
            // btnRuleFromFile
            // 
            this.btnRuleFromFile.Location = new System.Drawing.Point(6, 189);
            this.btnRuleFromFile.Name = "btnRuleFromFile";
            this.btnRuleFromFile.Size = new System.Drawing.Size(201, 25);
            this.btnRuleFromFile.TabIndex = 3;
            this.btnRuleFromFile.Text = "Load rools";
            this.btnRuleFromFile.UseVisualStyleBackColor = true;
            this.btnRuleFromFile.Click += new System.EventHandler(this.btnRuleFromFile_Click);
            // 
            // lvwRules
            // 
            this.lvwRules.FormattingEnabled = true;
            this.lvwRules.HorizontalScrollbar = true;
            this.lvwRules.Location = new System.Drawing.Point(6, 18);
            this.lvwRules.Name = "lvwRules";
            this.lvwRules.Size = new System.Drawing.Size(201, 134);
            this.lvwRules.TabIndex = 0;
            // 
            // tbxRules
            // 
            this.tbxRules.Location = new System.Drawing.Point(6, 163);
            this.tbxRules.Name = "tbxRules";
            this.tbxRules.Size = new System.Drawing.Size(155, 20);
            this.tbxRules.TabIndex = 1;
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(167, 163);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(40, 20);
            this.btnAddRule.TabIndex = 2;
            this.btnAddRule.Text = "Add";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // gbxFacts
            // 
            this.gbxFacts.Controls.Add(this.lvwFacts);
            this.gbxFacts.Controls.Add(this.btnFactFromFile);
            this.gbxFacts.Controls.Add(this.btnAddFact);
            this.gbxFacts.Controls.Add(this.tbxFacts);
            this.gbxFacts.Location = new System.Drawing.Point(12, 12);
            this.gbxFacts.Name = "gbxFacts";
            this.gbxFacts.Size = new System.Drawing.Size(217, 226);
            this.gbxFacts.TabIndex = 1;
            this.gbxFacts.TabStop = false;
            this.gbxFacts.Text = "Facts";
            // 
            // lvwFacts
            // 
            this.lvwFacts.FormattingEnabled = true;
            this.lvwFacts.HorizontalScrollbar = true;
            this.lvwFacts.Location = new System.Drawing.Point(6, 19);
            this.lvwFacts.Name = "lvwFacts";
            this.lvwFacts.Size = new System.Drawing.Size(205, 134);
            this.lvwFacts.TabIndex = 4;
            // 
            // btnFactFromFile
            // 
            this.btnFactFromFile.Location = new System.Drawing.Point(6, 189);
            this.btnFactFromFile.Name = "btnFactFromFile";
            this.btnFactFromFile.Size = new System.Drawing.Size(205, 25);
            this.btnFactFromFile.TabIndex = 3;
            this.btnFactFromFile.Text = "Load facts";
            this.btnFactFromFile.UseVisualStyleBackColor = true;
            this.btnFactFromFile.Click += new System.EventHandler(this.btnFactFromFile_Click);
            // 
            // btnAddFact
            // 
            this.btnAddFact.Location = new System.Drawing.Point(175, 161);
            this.btnAddFact.Name = "btnAddFact";
            this.btnAddFact.Size = new System.Drawing.Size(36, 23);
            this.btnAddFact.TabIndex = 2;
            this.btnAddFact.Text = "Add";
            this.btnAddFact.UseVisualStyleBackColor = true;
            this.btnAddFact.Click += new System.EventHandler(this.btnAddFact_Click);
            // 
            // tbxFacts
            // 
            this.tbxFacts.Location = new System.Drawing.Point(6, 163);
            this.tbxFacts.Name = "tbxFacts";
            this.tbxFacts.Size = new System.Drawing.Size(163, 20);
            this.tbxFacts.TabIndex = 1;
            // 
            // ofdFacts
            // 
            this.ofdFacts.Filter = "Facts|*.fct";
            this.ofdFacts.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdFacts_FileOk);
            // 
            // ofdRules
            // 
            this.ofdRules.Filter = "Rools|*.rul";
            this.ofdRules.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdRules_FileOk);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvwResults);
            this.groupBox1.Controls.Add(this.btnCheckGoals);
            this.groupBox1.Location = new System.Drawing.Point(237, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 226);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Results";
            // 
            // lvwResults
            // 
            this.lvwResults.FormattingEnabled = true;
            this.lvwResults.HorizontalScrollbar = true;
            this.lvwResults.Location = new System.Drawing.Point(6, 19);
            this.lvwResults.Name = "lvwResults";
            this.lvwResults.Size = new System.Drawing.Size(205, 134);
            this.lvwResults.TabIndex = 0;
            // 
            // btnCheckGoals
            // 
            this.btnCheckGoals.Location = new System.Drawing.Point(6, 163);
            this.btnCheckGoals.Name = "btnCheckGoals";
            this.btnCheckGoals.Size = new System.Drawing.Size(205, 51);
            this.btnCheckGoals.TabIndex = 6;
            this.btnCheckGoals.Text = "Solve";
            this.btnCheckGoals.UseVisualStyleBackColor = true;
            this.btnCheckGoals.Click += new System.EventHandler(this.btnCheckGoal_Click);
            // 
            // tbxGoal
            // 
            this.tbxGoal.Location = new System.Drawing.Point(6, 163);
            this.tbxGoal.Name = "tbxGoal";
            this.tbxGoal.Size = new System.Drawing.Size(163, 20);
            this.tbxGoal.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnLoadRequests);
            this.groupBox2.Controls.Add(this.lbRequests);
            this.groupBox2.Controls.Add(this.tbxGoal);
            this.groupBox2.Location = new System.Drawing.Point(235, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 226);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Requests";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 25);
            this.button2.TabIndex = 6;
            this.button2.Text = "Load requests";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnLoadRequests
            // 
            this.btnLoadRequests.Location = new System.Drawing.Point(175, 161);
            this.btnLoadRequests.Name = "btnLoadRequests";
            this.btnLoadRequests.Size = new System.Drawing.Size(36, 23);
            this.btnLoadRequests.TabIndex = 5;
            this.btnLoadRequests.Text = "Add";
            this.btnLoadRequests.UseVisualStyleBackColor = true;
            this.btnLoadRequests.Click += new System.EventHandler(this.btnLoadRequests_Click);
            // 
            // lbRequests
            // 
            this.lbRequests.FormattingEnabled = true;
            this.lbRequests.HorizontalScrollbar = true;
            this.lbRequests.Location = new System.Drawing.Point(6, 20);
            this.lbRequests.Name = "lbRequests";
            this.lbRequests.Size = new System.Drawing.Size(205, 134);
            this.lbRequests.TabIndex = 0;
            // 
            // ofdRequests
            // 
            this.ofdRequests.Filter = "Requests|*.req";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 488);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbxFacts);
            this.Controls.Add(this.gbxRules);
            this.Name = "Form1";
            this.Text = "LogicOutput";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbxRules.ResumeLayout(false);
            this.gbxRules.PerformLayout();
            this.gbxFacts.ResumeLayout(false);
            this.gbxFacts.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRules;
        private System.Windows.Forms.GroupBox gbxFacts;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.TextBox tbxRules;
        private System.Windows.Forms.Button btnAddFact;
        private System.Windows.Forms.TextBox tbxFacts;
        private System.Windows.Forms.Button btnRuleFromFile;
        private System.Windows.Forms.OpenFileDialog ofdFacts;
        private System.Windows.Forms.OpenFileDialog ofdRules;
        private System.Windows.Forms.ListBox lvwRules;
        private System.Windows.Forms.ListBox lvwFacts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCheckGoals;
        private System.Windows.Forms.TextBox tbxGoal;
        private System.Windows.Forms.ListBox lvwResults;
        private System.Windows.Forms.Button btnFactFromFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLoadRequests;
        private System.Windows.Forms.ListBox lbRequests;
        private System.Windows.Forms.OpenFileDialog ofdRequests;
    }
}

