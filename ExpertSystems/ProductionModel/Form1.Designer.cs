namespace ProductModel
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbGoal = new System.Windows.Forms.TextBox();
            this.lblGoal = new System.Windows.Forms.Label();
            this.btnGoal = new System.Windows.Forms.Button();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.gbSolveMethod = new System.Windows.Forms.GroupBox();
            this.rbStraight = new System.Windows.Forms.RadioButton();
            this.rbReverse = new System.Windows.Forms.RadioButton();
            this.chFacts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRools = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvFactBase = new System.Windows.Forms.ListView();
            this.chRules = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRuleBase = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelSelFacts = new System.Windows.Forms.Button();
            this.btnClearFactBase = new System.Windows.Forms.Button();
            this.btnAddFact = new System.Windows.Forms.Button();
            this.tbFact = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbRuleThen = new System.Windows.Forms.TextBox();
            this.lblRuleThen = new System.Windows.Forms.Label();
            this.tbRuleIf = new System.Windows.Forms.TextBox();
            this.lblRuleIf = new System.Windows.Forms.Label();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnDelSelRules = new System.Windows.Forms.Button();
            this.btnClearRuleBase = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.msMain.SuspendLayout();
            this.gbSolveMethod.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGoal
            // 
            this.tbGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbGoal.Location = new System.Drawing.Point(55, 517);
            this.tbGoal.Name = "tbGoal";
            this.tbGoal.Size = new System.Drawing.Size(391, 20);
            this.tbGoal.TabIndex = 14;
            this.tbGoal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGoal_KeyPress);
            // 
            // lblGoal
            // 
            this.lblGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGoal.AutoSize = true;
            this.lblGoal.Location = new System.Drawing.Point(12, 520);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(32, 13);
            this.lblGoal.TabIndex = 15;
            this.lblGoal.Text = "Goal:";
            // 
            // btnGoal
            // 
            this.btnGoal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGoal.Location = new System.Drawing.Point(460, 517);
            this.btnGoal.Name = "btnGoal";
            this.btnGoal.Size = new System.Drawing.Size(75, 23);
            this.btnGoal.TabIndex = 16;
            this.btnGoal.Text = "Solve";
            this.btnGoal.UseVisualStyleBackColor = true;
            this.btnGoal.Click += new System.EventHandler(this.btnGoal_Click);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(901, 24);
            this.msMain.TabIndex = 17;
            this.msMain.Text = "menuStrip1";
            this.msMain.Visible = false;
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile_Open,
            this.tsmiFile_SaveAs,
            this.tsmiFile_Exit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(48, 20);
            this.tsmiFile.Text = "Файл";
            // 
            // tsmiFile_Open
            // 
            this.tsmiFile_Open.Name = "tsmiFile_Open";
            this.tsmiFile_Open.Size = new System.Drawing.Size(162, 22);
            this.tsmiFile_Open.Text = "Открыть...";
            this.tsmiFile_Open.Click += new System.EventHandler(this.tsmiFile_Open_Click);
            // 
            // tsmiFile_SaveAs
            // 
            this.tsmiFile_SaveAs.Name = "tsmiFile_SaveAs";
            this.tsmiFile_SaveAs.Size = new System.Drawing.Size(162, 22);
            this.tsmiFile_SaveAs.Text = "Сохранить как...";
            this.tsmiFile_SaveAs.Click += new System.EventHandler(this.tsmiFile_SaveAs_Click);
            // 
            // tsmiFile_Exit
            // 
            this.tsmiFile_Exit.Name = "tsmiFile_Exit";
            this.tsmiFile_Exit.Size = new System.Drawing.Size(162, 22);
            this.tsmiFile_Exit.Text = "Выход";
            this.tsmiFile_Exit.Click += new System.EventHandler(this.tsmiFile_Exit_Click);
            // 
            // ofdOpen
            // 
            this.ofdOpen.Filter = "Текстовые файлы|*.txt";
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "Текстовые файлы|*.txt";
            // 
            // gbSolveMethod
            // 
            this.gbSolveMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbSolveMethod.Controls.Add(this.rbStraight);
            this.gbSolveMethod.Controls.Add(this.rbReverse);
            this.gbSolveMethod.Location = new System.Drawing.Point(725, 498);
            this.gbSolveMethod.Name = "gbSolveMethod";
            this.gbSolveMethod.Size = new System.Drawing.Size(156, 42);
            this.gbSolveMethod.TabIndex = 18;
            this.gbSolveMethod.TabStop = false;
            this.gbSolveMethod.Text = "Method";
            // 
            // rbStraight
            // 
            this.rbStraight.AutoSize = true;
            this.rbStraight.Location = new System.Drawing.Point(92, 19);
            this.rbStraight.Name = "rbStraight";
            this.rbStraight.Size = new System.Drawing.Size(53, 17);
            this.rbStraight.TabIndex = 0;
            this.rbStraight.Text = "Direct";
            this.rbStraight.UseVisualStyleBackColor = true;
            this.rbStraight.CheckedChanged += new System.EventHandler(this.rbReverse_CheckedChanged);
            // 
            // rbReverse
            // 
            this.rbReverse.AutoSize = true;
            this.rbReverse.Checked = true;
            this.rbReverse.Location = new System.Drawing.Point(21, 19);
            this.rbReverse.Name = "rbReverse";
            this.rbReverse.Size = new System.Drawing.Size(65, 17);
            this.rbReverse.TabIndex = 0;
            this.rbReverse.TabStop = true;
            this.rbReverse.Text = "Reverse";
            this.rbReverse.UseVisualStyleBackColor = true;
            this.rbReverse.CheckedChanged += new System.EventHandler(this.rbReverse_CheckedChanged);
            // 
            // chFacts
            // 
            this.chFacts.Text = "Facts";
            this.chFacts.Width = 413;
            // 
            // chRools
            // 
            this.chRools.Text = "";
            // 
            // lvFactBase
            // 
            this.lvFactBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvFactBase.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFacts,
            this.chRools});
            this.lvFactBase.Location = new System.Drawing.Point(4, 32);
            this.lvFactBase.Name = "lvFactBase";
            this.lvFactBase.ShowItemToolTips = true;
            this.lvFactBase.Size = new System.Drawing.Size(268, 398);
            this.lvFactBase.TabIndex = 3;
            this.lvFactBase.UseCompatibleStateImageBehavior = false;
            this.lvFactBase.View = System.Windows.Forms.View.Details;
            // 
            // chRules
            // 
            this.chRules.Text = "Rools";
            this.chRules.Width = 411;
            // 
            // lvRuleBase
            // 
            this.lvRuleBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRuleBase.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRules});
            this.lvRuleBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvRuleBase.Location = new System.Drawing.Point(283, 32);
            this.lvRuleBase.Name = "lvRuleBase";
            this.lvRuleBase.ShowItemToolTips = true;
            this.lvRuleBase.Size = new System.Drawing.Size(720, 398);
            this.lvRuleBase.TabIndex = 9;
            this.lvRuleBase.UseCompatibleStateImageBehavior = false;
            this.lvRuleBase.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnDelSelFacts);
            this.panel1.Controls.Add(this.btnClearFactBase);
            this.panel1.Controls.Add(this.btnAddFact);
            this.panel1.Controls.Add(this.tbFact);
            this.panel1.Location = new System.Drawing.Point(3, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 55);
            this.panel1.TabIndex = 20;
            // 
            // btnDelSelFacts
            // 
            this.btnDelSelFacts.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDelSelFacts.Location = new System.Drawing.Point(109, 27);
            this.btnDelSelFacts.Name = "btnDelSelFacts";
            this.btnDelSelFacts.Size = new System.Drawing.Size(76, 23);
            this.btnDelSelFacts.TabIndex = 9;
            this.btnDelSelFacts.Text = "Remove selected";
            this.btnDelSelFacts.UseVisualStyleBackColor = true;
            // 
            // btnClearFactBase
            // 
            this.btnClearFactBase.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClearFactBase.Location = new System.Drawing.Point(191, 27);
            this.btnClearFactBase.Name = "btnClearFactBase";
            this.btnClearFactBase.Size = new System.Drawing.Size(76, 23);
            this.btnClearFactBase.TabIndex = 8;
            this.btnClearFactBase.Text = "Clear";
            this.btnClearFactBase.UseVisualStyleBackColor = true;
            // 
            // btnAddFact
            // 
            this.btnAddFact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFact.Location = new System.Drawing.Point(191, 3);
            this.btnAddFact.Name = "btnAddFact";
            this.btnAddFact.Size = new System.Drawing.Size(76, 22);
            this.btnAddFact.TabIndex = 7;
            this.btnAddFact.Text = "Add";
            this.btnAddFact.UseVisualStyleBackColor = true;
            // 
            // tbFact
            // 
            this.tbFact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFact.Location = new System.Drawing.Point(3, 3);
            this.tbFact.Name = "tbFact";
            this.tbFact.Size = new System.Drawing.Size(100, 20);
            this.tbFact.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.tbRuleThen);
            this.panel2.Controls.Add(this.lblRuleThen);
            this.panel2.Controls.Add(this.tbRuleIf);
            this.panel2.Controls.Add(this.lblRuleIf);
            this.panel2.Controls.Add(this.btnAddRule);
            this.panel2.Controls.Add(this.btnDelSelRules);
            this.panel2.Controls.Add(this.btnClearRuleBase);
            this.panel2.Location = new System.Drawing.Point(281, 436);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(726, 55);
            this.panel2.TabIndex = 21;
            // 
            // tbRuleThen
            // 
            this.tbRuleThen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRuleThen.Location = new System.Drawing.Point(39, 29);
            this.tbRuleThen.Name = "tbRuleThen";
            this.tbRuleThen.Size = new System.Drawing.Size(513, 20);
            this.tbRuleThen.TabIndex = 20;
            // 
            // lblRuleThen
            // 
            this.lblRuleThen.AutoSize = true;
            this.lblRuleThen.Location = new System.Drawing.Point(3, 32);
            this.lblRuleThen.Name = "lblRuleThen";
            this.lblRuleThen.Size = new System.Drawing.Size(32, 13);
            this.lblRuleThen.TabIndex = 21;
            this.lblRuleThen.Text = "Then";
            // 
            // tbRuleIf
            // 
            this.tbRuleIf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRuleIf.Location = new System.Drawing.Point(39, 3);
            this.tbRuleIf.Name = "tbRuleIf";
            this.tbRuleIf.Size = new System.Drawing.Size(513, 20);
            this.tbRuleIf.TabIndex = 15;
            // 
            // lblRuleIf
            // 
            this.lblRuleIf.AutoSize = true;
            this.lblRuleIf.Location = new System.Drawing.Point(3, 10);
            this.lblRuleIf.Name = "lblRuleIf";
            this.lblRuleIf.Size = new System.Drawing.Size(13, 13);
            this.lblRuleIf.TabIndex = 19;
            this.lblRuleIf.Text = "If";
            // 
            // btnAddRule
            // 
            this.btnAddRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRule.Location = new System.Drawing.Point(639, 1);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(75, 23);
            this.btnAddRule.TabIndex = 16;
            this.btnAddRule.Text = "Add";
            this.btnAddRule.UseVisualStyleBackColor = true;
            // 
            // btnDelSelRules
            // 
            this.btnDelSelRules.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDelSelRules.Location = new System.Drawing.Point(558, 27);
            this.btnDelSelRules.Name = "btnDelSelRules";
            this.btnDelSelRules.Size = new System.Drawing.Size(75, 23);
            this.btnDelSelRules.TabIndex = 18;
            this.btnDelSelRules.Text = "Remove";
            this.btnDelSelRules.UseVisualStyleBackColor = true;
            // 
            // btnClearRuleBase
            // 
            this.btnClearRuleBase.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClearRuleBase.Location = new System.Drawing.Point(639, 27);
            this.btnClearRuleBase.Name = "btnClearRuleBase";
            this.btnClearRuleBase.Size = new System.Drawing.Size(75, 23);
            this.btnClearRuleBase.TabIndex = 17;
            this.btnClearRuleBase.Text = "Clear";
            this.btnClearRuleBase.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(109, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.tsmiFile_Open_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Location = new System.Drawing.Point(558, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.tsmiFile_Open_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 551);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbSolveMethod);
            this.Controls.Add(this.btnGoal);
            this.Controls.Add(this.lblGoal);
            this.Controls.Add(this.tbGoal);
            this.Controls.Add(this.lvRuleBase);
            this.Controls.Add(this.lvFactBase);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "Form1";
            this.Text = "Production Model";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.gbSolveMethod.ResumeLayout(false);
            this.gbSolveMethod.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbGoal;
        private System.Windows.Forms.Label lblGoal;
        private System.Windows.Forms.Button btnGoal;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Open;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_SaveAs;
        private System.Windows.Forms.OpenFileDialog ofdOpen;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_Exit;
        private System.Windows.Forms.GroupBox gbSolveMethod;
        private System.Windows.Forms.RadioButton rbStraight;
        private System.Windows.Forms.RadioButton rbReverse;
        private System.Windows.Forms.ColumnHeader chFacts;
        private System.Windows.Forms.ColumnHeader chRools;
        private System.Windows.Forms.ListView lvFactBase;
        private System.Windows.Forms.ColumnHeader chRules;
        private System.Windows.Forms.ListView lvRuleBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDelSelFacts;
        private System.Windows.Forms.Button btnClearFactBase;
        private System.Windows.Forms.Button btnAddFact;
        private System.Windows.Forms.TextBox tbFact;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbRuleThen;
        private System.Windows.Forms.Label lblRuleThen;
        private System.Windows.Forms.TextBox tbRuleIf;
        private System.Windows.Forms.Label lblRuleIf;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnDelSelRules;
        private System.Windows.Forms.Button btnClearRuleBase;
    }
}

