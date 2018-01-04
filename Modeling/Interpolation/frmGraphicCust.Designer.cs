namespace Interpolation
{
    partial class frmGraphicCust
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
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button3 = new System.Windows.Forms.Button();
            this.udGridXNumber = new System.Windows.Forms.NumericUpDown();
            this.udGridYNumber = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbAxisStyle = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.cbConstAxis = new System.Windows.Forms.CheckBox();
            this.cbSaveProp = new System.Windows.Forms.CheckBox();
            this.btnAxisColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbGridStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbShowGrid = new System.Windows.Forms.CheckBox();
            this.btnGridColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbGraph2 = new System.Windows.Forms.ComboBox();
            this.cmbGraph1 = new System.Windows.Forms.ComboBox();
            this.btnGraph2Color = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGraph1Color = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridXNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridYNumber)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(177, 186);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 80;
            this.dataGridView1.Size = new System.Drawing.Size(171, 40);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.Visible = false;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Firebrick;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column1.HeaderText = "Цвет";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 38;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Location = new System.Drawing.Point(379, 186);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 40);
            this.button3.TabIndex = 51;
            this.button3.Text = "ОК";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // udGridXNumber
            // 
            this.udGridXNumber.Location = new System.Drawing.Point(123, 85);
            this.udGridXNumber.Name = "udGridXNumber";
            this.udGridXNumber.Size = new System.Drawing.Size(48, 20);
            this.udGridXNumber.TabIndex = 57;
            this.udGridXNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // udGridYNumber
            // 
            this.udGridYNumber.Location = new System.Drawing.Point(123, 113);
            this.udGridYNumber.Name = "udGridYNumber";
            this.udGridYNumber.Size = new System.Drawing.Size(48, 20);
            this.udGridYNumber.TabIndex = 59;
            this.udGridYNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbAxisStyle);
            this.groupBox1.Controls.Add(this.checkBox3);
            this.groupBox1.Controls.Add(this.cbConstAxis);
            this.groupBox1.Controls.Add(this.cbSaveProp);
            this.groupBox1.Controls.Add(this.btnAxisColor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(202, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 173);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оси";
            // 
            // cmbAxisStyle
            // 
            this.cmbAxisStyle.DisplayMember = "0";
            this.cmbAxisStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAxisStyle.FormattingEnabled = true;
            this.cmbAxisStyle.Items.AddRange(new object[] {
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot",
            "Solid"});
            this.cmbAxisStyle.Location = new System.Drawing.Point(98, 22);
            this.cmbAxisStyle.Name = "cmbAxisStyle";
            this.cmbAxisStyle.Size = new System.Drawing.Size(62, 21);
            this.cmbAxisStyle.Sorted = true;
            this.cmbAxisStyle.TabIndex = 61;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(9, 60);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(95, 17);
            this.checkBox3.TabIndex = 58;
            this.checkBox3.Text = "Рисовать оси";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // cbConstAxis
            // 
            this.cbConstAxis.AutoSize = true;
            this.cbConstAxis.Location = new System.Drawing.Point(9, 86);
            this.cbConstAxis.Name = "cbConstAxis";
            this.cbConstAxis.Size = new System.Drawing.Size(110, 17);
            this.cbConstAxis.TabIndex = 57;
            this.cbConstAxis.Text = "Постоянные оси";
            this.cbConstAxis.UseVisualStyleBackColor = true;
            // 
            // cbSaveProp
            // 
            this.cbSaveProp.AutoSize = true;
            this.cbSaveProp.Location = new System.Drawing.Point(9, 112);
            this.cbSaveProp.Name = "cbSaveProp";
            this.cbSaveProp.Size = new System.Drawing.Size(136, 17);
            this.cbSaveProp.TabIndex = 56;
            this.cbSaveProp.Text = "Сохранять пропорции";
            this.cbSaveProp.UseVisualStyleBackColor = true;
            // 
            // btnAxisColor
            // 
            this.btnAxisColor.BackColor = System.Drawing.Color.DimGray;
            this.btnAxisColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAxisColor.Location = new System.Drawing.Point(71, 21);
            this.btnAxisColor.Name = "btnAxisColor";
            this.btnAxisColor.Size = new System.Drawing.Size(21, 21);
            this.btnAxisColor.TabIndex = 55;
            this.btnAxisColor.UseVisualStyleBackColor = false;
            this.btnAxisColor.Click += new System.EventHandler(this.btnGraph1Color_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Цвет осей";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbGridStyle);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbShowGrid);
            this.groupBox2.Controls.Add(this.btnGridColor);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.udGridYNumber);
            this.groupBox2.Controls.Add(this.udGridXNumber);
            this.groupBox2.Location = new System.Drawing.Point(379, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 173);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сетка";
            // 
            // cmbGridStyle
            // 
            this.cmbGridStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGridStyle.FormattingEnabled = true;
            this.cmbGridStyle.Items.AddRange(new object[] {
            "Dash",
            "DashDotDot",
            "DashDot",
            "Dot",
            "Solid"});
            this.cmbGridStyle.Location = new System.Drawing.Point(109, 21);
            this.cmbGridStyle.Name = "cmbGridStyle";
            this.cmbGridStyle.Size = new System.Drawing.Size(62, 21);
            this.cmbGridStyle.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 65;
            this.label5.Text = "Кол-во шагов по Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 64;
            this.label4.Text = "Кол-во шагов по Х";
            // 
            // cbShowGrid
            // 
            this.cbShowGrid.AutoSize = true;
            this.cbShowGrid.Location = new System.Drawing.Point(9, 60);
            this.cbShowGrid.Name = "cbShowGrid";
            this.cbShowGrid.Size = new System.Drawing.Size(105, 17);
            this.cbShowGrid.TabIndex = 63;
            this.cbShowGrid.Text = "Рисовать сетку";
            this.cbShowGrid.UseVisualStyleBackColor = true;
            // 
            // btnGridColor
            // 
            this.btnGridColor.BackColor = System.Drawing.Color.LightGray;
            this.btnGridColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGridColor.Location = new System.Drawing.Point(77, 21);
            this.btnGridColor.Name = "btnGridColor";
            this.btnGridColor.Size = new System.Drawing.Size(21, 21);
            this.btnGridColor.TabIndex = 62;
            this.btnGridColor.UseVisualStyleBackColor = false;
            this.btnGridColor.Click += new System.EventHandler(this.btnGraph1Color_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Цвет сетки";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbGraph2);
            this.groupBox3.Controls.Add(this.cmbGraph1);
            this.groupBox3.Controls.Add(this.btnGraph2Color);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnGraph1Color);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(12, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 173);
            this.groupBox3.TabIndex = 63;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Стили";
            // 
            // cmbGraph2
            // 
            this.cmbGraph2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGraph2.FormattingEnabled = true;
            this.cmbGraph2.Items.AddRange(new object[] {
            "Dash",
            "DashDotDot",
            "DashDot",
            "Dot",
            "Solid"});
            this.cmbGraph2.Location = new System.Drawing.Point(107, 58);
            this.cmbGraph2.Name = "cmbGraph2";
            this.cmbGraph2.Size = new System.Drawing.Size(62, 21);
            this.cmbGraph2.TabIndex = 61;
            // 
            // cmbGraph1
            // 
            this.cmbGraph1.DisplayMember = "0";
            this.cmbGraph1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGraph1.FormattingEnabled = true;
            this.cmbGraph1.Items.AddRange(new object[] {
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot",
            "Solid"});
            this.cmbGraph1.Location = new System.Drawing.Point(107, 22);
            this.cmbGraph1.Name = "cmbGraph1";
            this.cmbGraph1.Size = new System.Drawing.Size(62, 21);
            this.cmbGraph1.Sorted = true;
            this.cmbGraph1.TabIndex = 60;
            // 
            // btnGraph2Color
            // 
            this.btnGraph2Color.BackColor = System.Drawing.Color.Teal;
            this.btnGraph2Color.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGraph2Color.Location = new System.Drawing.Point(66, 57);
            this.btnGraph2Color.Name = "btnGraph2Color";
            this.btnGraph2Color.Size = new System.Drawing.Size(21, 21);
            this.btnGraph2Color.TabIndex = 59;
            this.btnGraph2Color.UseVisualStyleBackColor = false;
            this.btnGraph2Color.Click += new System.EventHandler(this.btnGraph1Color_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 58;
            this.label6.Text = "График 2";
            // 
            // btnGraph1Color
            // 
            this.btnGraph1Color.BackColor = System.Drawing.Color.Firebrick;
            this.btnGraph1Color.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGraph1Color.Location = new System.Drawing.Point(66, 22);
            this.btnGraph1Color.Name = "btnGraph1Color";
            this.btnGraph1Color.Size = new System.Drawing.Size(21, 21);
            this.btnGraph1Color.TabIndex = 57;
            this.btnGraph1Color.UseVisualStyleBackColor = false;
            this.btnGraph1Color.Click += new System.EventHandler(this.btnGraph1Color_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "График 1";
            // 
            // frmGraphicCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 237);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGraphicCust";
            this.Text = "Настройка графика";
            this.Load += new System.EventHandler(this.frmGraphicCust_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGraphicCust_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridXNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udGridYNumber)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown udGridXNumber;
        private System.Windows.Forms.NumericUpDown udGridYNumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox cbConstAxis;
        private System.Windows.Forms.CheckBox cbSaveProp;
        private System.Windows.Forms.Button btnAxisColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbShowGrid;
        private System.Windows.Forms.Button btnGridColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbGraph1;
        private System.Windows.Forms.Button btnGraph2Color;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGraph1Color;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGraph2;
        private System.Windows.Forms.ComboBox cmbAxisStyle;
        private System.Windows.Forms.ComboBox cmbGridStyle;
    }
}