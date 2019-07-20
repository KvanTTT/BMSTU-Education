namespace MusicSyncLib.WinForms
{
	partial class frmMatrices
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			this.timerFollowSequence = new System.Windows.Forms.Timer(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dgvTransitions = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dgvEmissions = new System.Windows.Forms.DataGridView();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dgvInitial = new System.Windows.Forms.DataGridView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.pnlNotesViewer = new System.Windows.Forms.Panel();
			this.musicalNotesViewer = new MusicNotesRendererLib.MusicalNotesViewer();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.tbInputSequence = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.cmbObservsCount = new System.Windows.Forms.ComboBox();
			this.label34 = new System.Windows.Forms.Label();
			this.cmbMinDuration = new System.Windows.Forms.ComboBox();
			this.cbParallel = new System.Windows.Forms.CheckBox();
			this.nudTempoVariationErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.label32 = new System.Windows.Forms.Label();
			this.nudTempoErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.label31 = new System.Windows.Forms.Label();
			this.nudExtraErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.label25 = new System.Windows.Forms.Label();
			this.nudWrongErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.label24 = new System.Windows.Forms.Label();
			this.nudMissedErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.label23 = new System.Windows.Forms.Label();
			this.nudErrorPercent = new System.Windows.Forms.NumericUpDown();
			this.btnClearInputSequence = new System.Windows.Forms.Button();
			this.label22 = new System.Windows.Forms.Label();
			this.tbGhostsCount = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.cmbScore = new System.Windows.Forms.ComboBox();
			this.btnGenerateMatricies = new System.Windows.Forms.Button();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.tbTimeOffline = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.tbResultProbabOffline = new System.Windows.Forms.TextBox();
			this.tbOutputSequence = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnViterbi = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblPitchsCount = new System.Windows.Forms.Label();
			this.nudBarCount = new System.Windows.Forms.NumericUpDown();
			this.label35 = new System.Windows.Forms.Label();
			this.btnSetDefaults = new System.Windows.Forms.Button();
			this.tbMetronome = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.tbCurHmmMidiNote = new System.Windows.Forms.TextBox();
			this.tbCurMidiNote = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.lblCurAmpl = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.tbSoundAmplitude = new System.Windows.Forms.TrackBar();
			this.tbCurNoteName = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.btnMicFollowing = new System.Windows.Forms.Button();
			this.nudTempo = new System.Windows.Forms.NumericUpDown();
			this.label21 = new System.Windows.Forms.Label();
			this.nudObservsCount = new System.Windows.Forms.NumericUpDown();
			this.nudWindowSize = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnNextNoteOpt = new System.Windows.Forms.Button();
			this.btnPlay12 = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.btnPlay11 = new System.Windows.Forms.Button();
			this.btnFollowSequence = new System.Windows.Forms.Button();
			this.btnPlay10 = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnPlay9 = new System.Windows.Forms.Button();
			this.btnPlay0 = new System.Windows.Forms.Button();
			this.btnPlay8 = new System.Windows.Forms.Button();
			this.btnPlay1 = new System.Windows.Forms.Button();
			this.btnPlay7 = new System.Windows.Forms.Button();
			this.btnPlay2 = new System.Windows.Forms.Button();
			this.btnPlay6 = new System.Windows.Forms.Button();
			this.btnPlay3 = new System.Windows.Forms.Button();
			this.btnPlay5 = new System.Windows.Forms.Button();
			this.btnPlay4 = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.tbTempoEstimation = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.tbKvantEstimation = new System.Windows.Forms.TextBox();
			this.label37 = new System.Windows.Forms.Label();
			this.tbEventEstimation = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.tbLastPath = new System.Windows.Forms.TextBox();
			this.tbPath = new System.Windows.Forms.TextBox();
			this.tbLastObservations = new System.Windows.Forms.TextBox();
			this.tbObservations = new System.Windows.Forms.TextBox();
			this.cbUpdateMatrixes = new System.Windows.Forms.CheckBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.tbRating = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.tbTempo = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.tbCurrentState = new System.Windows.Forms.TextBox();
			this.pbNoteProgress = new System.Windows.Forms.ProgressBar();
			this.tbOnlineStepTime = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.tbLocalEstimation = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.timerMetronome = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvEmissions)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvInitial)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.pnlNotesViewer.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTempoVariationErrorPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTempoErrorPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudExtraErrorPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWrongErrorPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMissedErrorPercent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudErrorPercent)).BeginInit();
			this.groupBox8.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBarCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSoundAmplitude)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTempo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudObservsCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWindowSize)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// timerFollowSequence
			// 
			this.timerFollowSequence.Interval = 600;
			this.timerFollowSequence.Tick += new System.EventHandler(this.timerFollowSequence_Tick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2.Controls.Add(this.label2);
			this.splitContainer1.Size = new System.Drawing.Size(1410, 376);
			this.splitContainer1.SplitterDistance = 799;
			this.splitContainer1.TabIndex = 61;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dgvTransitions);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(799, 376);
			this.groupBox1.TabIndex = 63;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "State transitions";
			// 
			// dgvTransitions
			// 
			this.dgvTransitions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvTransitions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvTransitions.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvTransitions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvTransitions.Location = new System.Drawing.Point(3, 16);
			this.dgvTransitions.Name = "dgvTransitions";
			this.dgvTransitions.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvTransitions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgvTransitions.RowHeadersWidth = 50;
			this.dgvTransitions.Size = new System.Drawing.Size(793, 357);
			this.dgvTransitions.TabIndex = 63;
			this.dgvTransitions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTransitions_CellContentClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dgvEmissions);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(607, 376);
			this.groupBox2.TabIndex = 64;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Emission probabilities";
			// 
			// dgvEmissions
			// 
			this.dgvEmissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvEmissions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgvEmissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvEmissions.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgvEmissions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvEmissions.Location = new System.Drawing.Point(3, 16);
			this.dgvEmissions.Name = "dgvEmissions";
			this.dgvEmissions.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvEmissions.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgvEmissions.RowHeadersWidth = 60;
			this.dgvEmissions.Size = new System.Drawing.Size(601, 357);
			this.dgvEmissions.TabIndex = 24;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 25;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.dgvInitial);
			this.groupBox3.Location = new System.Drawing.Point(12, 394);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(1410, 78);
			this.groupBox3.TabIndex = 64;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Initial probabilities";
			// 
			// dgvInitial
			// 
			this.dgvInitial.AccessibleDescription = "";
			this.dgvInitial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvInitial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgvInitial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvInitial.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgvInitial.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvInitial.Location = new System.Drawing.Point(3, 16);
			this.dgvInitial.Name = "dgvInitial";
			this.dgvInitial.ReadOnly = true;
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvInitial.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgvInitial.RowHeadersWidth = 50;
			this.dgvInitial.Size = new System.Drawing.Size(1404, 59);
			this.dgvInitial.TabIndex = 61;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.groupBox4);
			this.panel1.Controls.Add(this.groupBox9);
			this.panel1.Controls.Add(this.groupBox7);
			this.panel1.Controls.Add(this.groupBox8);
			this.panel1.Controls.Add(this.groupBox6);
			this.panel1.Controls.Add(this.groupBox5);
			this.panel1.Location = new System.Drawing.Point(12, 478);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1410, 414);
			this.panel1.TabIndex = 75;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.pnlNotesViewer);
			this.groupBox4.Location = new System.Drawing.Point(3, 277);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(532, 101);
			this.groupBox4.TabIndex = 80;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Graphics";
			// 
			// pnlNotesViewer
			// 
			this.pnlNotesViewer.AutoScroll = true;
			this.pnlNotesViewer.Controls.Add(this.musicalNotesViewer);
			this.pnlNotesViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlNotesViewer.Location = new System.Drawing.Point(3, 16);
			this.pnlNotesViewer.Name = "pnlNotesViewer";
			this.pnlNotesViewer.Size = new System.Drawing.Size(526, 82);
			this.pnlNotesViewer.TabIndex = 81;
			// 
			// musicalNotesViewer
			// 
			this.musicalNotesViewer.AutoScroll = true;
			this.musicalNotesViewer.AutoSize = true;
			this.musicalNotesViewer.BackColor = System.Drawing.Color.Transparent;
			this.musicalNotesViewer.DrawButtons = false;
			this.musicalNotesViewer.DrawOnlySelectionAndButtons = false;
			this.musicalNotesViewer.DrawOnParentControl = false;
			this.musicalNotesViewer.IncipitID = 0;
			this.musicalNotesViewer.IsSelected = false;
			this.musicalNotesViewer.Location = new System.Drawing.Point(3, 3);
			this.musicalNotesViewer.Name = "musicalNotesViewer";
			this.musicalNotesViewer.ShortIncipit = null;
			this.musicalNotesViewer.Size = new System.Drawing.Size(520, 78);
			this.musicalNotesViewer.TabIndex = 0;
			// 
			// groupBox9
			// 
			this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox9.Controls.Add(this.tbInputSequence);
			this.groupBox9.Controls.Add(this.label4);
			this.groupBox9.Location = new System.Drawing.Point(4, 144);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(532, 50);
			this.groupBox9.TabIndex = 79;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Input offline";
			// 
			// tbInputSequence
			// 
			this.tbInputSequence.Location = new System.Drawing.Point(113, 19);
			this.tbInputSequence.Name = "tbInputSequence";
			this.tbInputSequence.Size = new System.Drawing.Size(406, 20);
			this.tbInputSequence.TabIndex = 26;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 13);
			this.label4.TabIndex = 25;
			this.label4.Text = "Input Sequence";
			// 
			// groupBox7
			// 
			this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox7.Controls.Add(this.cmbObservsCount);
			this.groupBox7.Controls.Add(this.label34);
			this.groupBox7.Controls.Add(this.cmbMinDuration);
			this.groupBox7.Controls.Add(this.cbParallel);
			this.groupBox7.Controls.Add(this.nudTempoVariationErrorPercent);
			this.groupBox7.Controls.Add(this.label32);
			this.groupBox7.Controls.Add(this.nudTempoErrorPercent);
			this.groupBox7.Controls.Add(this.label31);
			this.groupBox7.Controls.Add(this.nudExtraErrorPercent);
			this.groupBox7.Controls.Add(this.label25);
			this.groupBox7.Controls.Add(this.nudWrongErrorPercent);
			this.groupBox7.Controls.Add(this.label24);
			this.groupBox7.Controls.Add(this.nudMissedErrorPercent);
			this.groupBox7.Controls.Add(this.label23);
			this.groupBox7.Controls.Add(this.nudErrorPercent);
			this.groupBox7.Controls.Add(this.btnClearInputSequence);
			this.groupBox7.Controls.Add(this.label22);
			this.groupBox7.Controls.Add(this.tbGhostsCount);
			this.groupBox7.Controls.Add(this.label13);
			this.groupBox7.Controls.Add(this.label6);
			this.groupBox7.Controls.Add(this.label12);
			this.groupBox7.Controls.Add(this.cmbScore);
			this.groupBox7.Controls.Add(this.btnGenerateMatricies);
			this.groupBox7.Location = new System.Drawing.Point(4, 3);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(532, 137);
			this.groupBox7.TabIndex = 78;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Generation";
			// 
			// cmbObservsCount
			// 
			this.cmbObservsCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbObservsCount.FormattingEnabled = true;
			this.cmbObservsCount.Items.AddRange(new object[] {
            "12",
            "24",
            "36",
            "48"});
			this.cmbObservsCount.Location = new System.Drawing.Point(272, 69);
			this.cmbObservsCount.Name = "cmbObservsCount";
			this.cmbObservsCount.Size = new System.Drawing.Size(62, 21);
			this.cmbObservsCount.TabIndex = 96;
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(187, 72);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(77, 13);
			this.label34.TabIndex = 95;
			this.label34.Text = "Observs Count";
			// 
			// cmbMinDuration
			// 
			this.cmbMinDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMinDuration.FormattingEnabled = true;
			this.cmbMinDuration.Items.AddRange(new object[] {
            "2",
            "1",
            "0.5",
            "0.25",
            "0.125",
            "0.0625"});
			this.cmbMinDuration.Location = new System.Drawing.Point(272, 42);
			this.cmbMinDuration.Name = "cmbMinDuration";
			this.cmbMinDuration.Size = new System.Drawing.Size(62, 21);
			this.cmbMinDuration.TabIndex = 94;
			// 
			// cbParallel
			// 
			this.cbParallel.AutoSize = true;
			this.cbParallel.Location = new System.Drawing.Point(457, 73);
			this.cbParallel.Name = "cbParallel";
			this.cbParallel.Size = new System.Drawing.Size(60, 17);
			this.cbParallel.TabIndex = 92;
			this.cbParallel.Text = "Parallel";
			this.cbParallel.UseVisualStyleBackColor = true;
			// 
			// nudTempoVariationErrorPercent
			// 
			this.nudTempoVariationErrorPercent.Location = new System.Drawing.Point(485, 110);
			this.nudTempoVariationErrorPercent.Name = "nudTempoVariationErrorPercent";
			this.nudTempoVariationErrorPercent.Size = new System.Drawing.Size(41, 20);
			this.nudTempoVariationErrorPercent.TabIndex = 91;
			this.nudTempoVariationErrorPercent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(395, 114);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(84, 13);
			this.label32.TabIndex = 90;
			this.label32.Text = "Tempo Variation";
			// 
			// nudTempoErrorPercent
			// 
			this.nudTempoErrorPercent.Location = new System.Drawing.Point(342, 110);
			this.nudTempoErrorPercent.Name = "nudTempoErrorPercent";
			this.nudTempoErrorPercent.Size = new System.Drawing.Size(44, 20);
			this.nudTempoErrorPercent.TabIndex = 89;
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(285, 112);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(51, 13);
			this.label31.TabIndex = 88;
			this.label31.Text = "Tempo %";
			// 
			// nudExtraErrorPercent
			// 
			this.nudExtraErrorPercent.Location = new System.Drawing.Point(238, 110);
			this.nudExtraErrorPercent.Name = "nudExtraErrorPercent";
			this.nudExtraErrorPercent.Size = new System.Drawing.Size(39, 20);
			this.nudExtraErrorPercent.TabIndex = 87;
			this.nudExtraErrorPercent.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
			this.nudExtraErrorPercent.ValueChanged += new System.EventHandler(this.nudExtraErrorPercent_ValueChanged);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(201, 112);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(31, 13);
			this.label25.TabIndex = 86;
			this.label25.Text = "Extra";
			// 
			// nudWrongErrorPercent
			// 
			this.nudWrongErrorPercent.Location = new System.Drawing.Point(154, 110);
			this.nudWrongErrorPercent.Name = "nudWrongErrorPercent";
			this.nudWrongErrorPercent.Size = new System.Drawing.Size(41, 20);
			this.nudWrongErrorPercent.TabIndex = 85;
			this.nudWrongErrorPercent.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
			this.nudWrongErrorPercent.ValueChanged += new System.EventHandler(this.nudWrongErrorPercent_ValueChanged);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(109, 112);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(39, 13);
			this.label24.TabIndex = 84;
			this.label24.Text = "Wrong";
			// 
			// nudMissedErrorPercent
			// 
			this.nudMissedErrorPercent.Location = new System.Drawing.Point(60, 109);
			this.nudMissedErrorPercent.Name = "nudMissedErrorPercent";
			this.nudMissedErrorPercent.Size = new System.Drawing.Size(43, 20);
			this.nudMissedErrorPercent.TabIndex = 83;
			this.nudMissedErrorPercent.Value = new decimal(new int[] {
            33,
            0,
            0,
            0});
			this.nudMissedErrorPercent.ValueChanged += new System.EventHandler(this.nudMissedErrorPercent_ValueChanged);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(15, 111);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(40, 13);
			this.label23.TabIndex = 82;
			this.label23.Text = "Missed";
			// 
			// nudErrorPercent
			// 
			this.nudErrorPercent.Location = new System.Drawing.Point(60, 84);
			this.nudErrorPercent.Name = "nudErrorPercent";
			this.nudErrorPercent.Size = new System.Drawing.Size(44, 20);
			this.nudErrorPercent.TabIndex = 81;
			// 
			// btnClearInputSequence
			// 
			this.btnClearInputSequence.Location = new System.Drawing.Point(342, 39);
			this.btnClearInputSequence.Name = "btnClearInputSequence";
			this.btnClearInputSequence.Size = new System.Drawing.Size(177, 24);
			this.btnClearInputSequence.TabIndex = 79;
			this.btnClearInputSequence.Text = "Refresh Input Sequence";
			this.btnClearInputSequence.UseVisualStyleBackColor = true;
			this.btnClearInputSequence.Click += new System.EventHandler(this.btnClearInputSequence_Click);
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(15, 86);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(40, 13);
			this.label22.TabIndex = 80;
			this.label22.Text = "Error %";
			// 
			// tbGhostsCount
			// 
			this.tbGhostsCount.Location = new System.Drawing.Point(272, 16);
			this.tbGhostsCount.Name = "tbGhostsCount";
			this.tbGhostsCount.Size = new System.Drawing.Size(62, 20);
			this.tbGhostsCount.TabIndex = 78;
			this.tbGhostsCount.Text = "1";
			this.tbGhostsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(187, 19);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(66, 13);
			this.label13.TabIndex = 77;
			this.label13.Text = "Ghost Count";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(187, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 13);
			this.label6.TabIndex = 75;
			this.label6.Text = "Min Duration";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(7, 16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(64, 13);
			this.label12.TabIndex = 73;
			this.label12.Text = "Composition";
			// 
			// cmbScore
			// 
			this.cmbScore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScore.FormattingEnabled = true;
			this.cmbScore.Location = new System.Drawing.Point(10, 37);
			this.cmbScore.Name = "cmbScore";
			this.cmbScore.Size = new System.Drawing.Size(164, 21);
			this.cmbScore.TabIndex = 72;
			this.cmbScore.SelectedIndexChanged += new System.EventHandler(this.cmbScore_SelectedIndexChanged);
			// 
			// btnGenerateMatricies
			// 
			this.btnGenerateMatricies.Location = new System.Drawing.Point(342, 13);
			this.btnGenerateMatricies.Name = "btnGenerateMatricies";
			this.btnGenerateMatricies.Size = new System.Drawing.Size(177, 24);
			this.btnGenerateMatricies.TabIndex = 71;
			this.btnGenerateMatricies.Text = "Generate Sequence and Matricies";
			this.btnGenerateMatricies.UseVisualStyleBackColor = true;
			this.btnGenerateMatricies.Click += new System.EventHandler(this.btnGenerateMatricies_Click);
			// 
			// groupBox8
			// 
			this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox8.Controls.Add(this.label8);
			this.groupBox8.Controls.Add(this.tbTimeOffline);
			this.groupBox8.Controls.Add(this.label11);
			this.groupBox8.Controls.Add(this.tbResultProbabOffline);
			this.groupBox8.Controls.Add(this.tbOutputSequence);
			this.groupBox8.Controls.Add(this.label5);
			this.groupBox8.Controls.Add(this.btnViterbi);
			this.groupBox8.Location = new System.Drawing.Point(4, 193);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(532, 78);
			this.groupBox8.TabIndex = 77;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Output offline";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(223, 53);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(30, 13);
			this.label8.TabIndex = 72;
			this.label8.Text = "Time";
			// 
			// tbTimeOffline
			// 
			this.tbTimeOffline.Location = new System.Drawing.Point(259, 49);
			this.tbTimeOffline.Name = "tbTimeOffline";
			this.tbTimeOffline.ReadOnly = true;
			this.tbTimeOffline.Size = new System.Drawing.Size(95, 20);
			this.tbTimeOffline.TabIndex = 71;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(387, 53);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(55, 13);
			this.label11.TabIndex = 69;
			this.label11.Text = "Estimation";
			// 
			// tbResultProbabOffline
			// 
			this.tbResultProbabOffline.Location = new System.Drawing.Point(457, 49);
			this.tbResultProbabOffline.Name = "tbResultProbabOffline";
			this.tbResultProbabOffline.ReadOnly = true;
			this.tbResultProbabOffline.Size = new System.Drawing.Size(61, 20);
			this.tbResultProbabOffline.TabIndex = 68;
			// 
			// tbOutputSequence
			// 
			this.tbOutputSequence.Location = new System.Drawing.Point(111, 17);
			this.tbOutputSequence.Name = "tbOutputSequence";
			this.tbOutputSequence.ReadOnly = true;
			this.tbOutputSequence.Size = new System.Drawing.Size(407, 20);
			this.tbOutputSequence.TabIndex = 24;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "Output Sequence";
			// 
			// btnViterbi
			// 
			this.btnViterbi.Location = new System.Drawing.Point(111, 47);
			this.btnViterbi.Name = "btnViterbi";
			this.btnViterbi.Size = new System.Drawing.Size(86, 23);
			this.btnViterbi.TabIndex = 23;
			this.btnViterbi.Text = "Viterbi Offline";
			this.btnViterbi.UseVisualStyleBackColor = true;
			this.btnViterbi.Click += new System.EventHandler(this.btnViterbi_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox6.Controls.Add(this.lblTime);
			this.groupBox6.Controls.Add(this.lblPitchsCount);
			this.groupBox6.Controls.Add(this.nudBarCount);
			this.groupBox6.Controls.Add(this.label35);
			this.groupBox6.Controls.Add(this.btnSetDefaults);
			this.groupBox6.Controls.Add(this.tbMetronome);
			this.groupBox6.Controls.Add(this.label33);
			this.groupBox6.Controls.Add(this.tbCurHmmMidiNote);
			this.groupBox6.Controls.Add(this.tbCurMidiNote);
			this.groupBox6.Controls.Add(this.label30);
			this.groupBox6.Controls.Add(this.label29);
			this.groupBox6.Controls.Add(this.lblCurAmpl);
			this.groupBox6.Controls.Add(this.label28);
			this.groupBox6.Controls.Add(this.label27);
			this.groupBox6.Controls.Add(this.tbSoundAmplitude);
			this.groupBox6.Controls.Add(this.tbCurNoteName);
			this.groupBox6.Controls.Add(this.label26);
			this.groupBox6.Controls.Add(this.btnMicFollowing);
			this.groupBox6.Controls.Add(this.nudTempo);
			this.groupBox6.Controls.Add(this.label21);
			this.groupBox6.Controls.Add(this.nudObservsCount);
			this.groupBox6.Controls.Add(this.nudWindowSize);
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.Controls.Add(this.label1);
			this.groupBox6.Controls.Add(this.btnNextNoteOpt);
			this.groupBox6.Controls.Add(this.btnPlay12);
			this.groupBox6.Controls.Add(this.label14);
			this.groupBox6.Controls.Add(this.btnPlay11);
			this.groupBox6.Controls.Add(this.btnFollowSequence);
			this.groupBox6.Controls.Add(this.btnPlay10);
			this.groupBox6.Controls.Add(this.btnReset);
			this.groupBox6.Controls.Add(this.btnPlay9);
			this.groupBox6.Controls.Add(this.btnPlay0);
			this.groupBox6.Controls.Add(this.btnPlay8);
			this.groupBox6.Controls.Add(this.btnPlay1);
			this.groupBox6.Controls.Add(this.btnPlay7);
			this.groupBox6.Controls.Add(this.btnPlay2);
			this.groupBox6.Controls.Add(this.btnPlay6);
			this.groupBox6.Controls.Add(this.btnPlay3);
			this.groupBox6.Controls.Add(this.btnPlay5);
			this.groupBox6.Controls.Add(this.btnPlay4);
			this.groupBox6.Location = new System.Drawing.Point(542, 3);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(721, 168);
			this.groupBox6.TabIndex = 76;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Input Online";
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(577, 109);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(13, 13);
			this.lblTime.TabIndex = 97;
			this.lblTime.Text = "0";
			// 
			// lblPitchsCount
			// 
			this.lblPitchsCount.AutoSize = true;
			this.lblPitchsCount.Location = new System.Drawing.Point(577, 89);
			this.lblPitchsCount.Name = "lblPitchsCount";
			this.lblPitchsCount.Size = new System.Drawing.Size(13, 13);
			this.lblPitchsCount.TabIndex = 96;
			this.lblPitchsCount.Text = "0";
			// 
			// nudBarCount
			// 
			this.nudBarCount.Location = new System.Drawing.Point(88, 22);
			this.nudBarCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudBarCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudBarCount.Name = "nudBarCount";
			this.nudBarCount.Size = new System.Drawing.Size(46, 20);
			this.nudBarCount.TabIndex = 95;
			this.nudBarCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(16, 24);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(54, 13);
			this.label35.TabIndex = 94;
			this.label35.Text = "Bar Count";
			// 
			// btnSetDefaults
			// 
			this.btnSetDefaults.Location = new System.Drawing.Point(416, 16);
			this.btnSetDefaults.Name = "btnSetDefaults";
			this.btnSetDefaults.Size = new System.Drawing.Size(100, 27);
			this.btnSetDefaults.TabIndex = 93;
			this.btnSetDefaults.Text = "Set Defaults";
			this.btnSetDefaults.UseVisualStyleBackColor = true;
			this.btnSetDefaults.Click += new System.EventHandler(this.btnSetDefaults_Click);
			// 
			// tbMetronome
			// 
			this.tbMetronome.Location = new System.Drawing.Point(636, 138);
			this.tbMetronome.Name = "tbMetronome";
			this.tbMetronome.ReadOnly = true;
			this.tbMetronome.Size = new System.Drawing.Size(63, 20);
			this.tbMetronome.TabIndex = 92;
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(570, 141);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(60, 13);
			this.label33.TabIndex = 91;
			this.label33.Text = "Metronome";
			// 
			// tbCurHmmMidiNote
			// 
			this.tbCurHmmMidiNote.Location = new System.Drawing.Point(484, 138);
			this.tbCurHmmMidiNote.Name = "tbCurHmmMidiNote";
			this.tbCurHmmMidiNote.ReadOnly = true;
			this.tbCurHmmMidiNote.Size = new System.Drawing.Size(63, 20);
			this.tbCurHmmMidiNote.TabIndex = 90;
			// 
			// tbCurMidiNote
			// 
			this.tbCurMidiNote.Location = new System.Drawing.Point(484, 112);
			this.tbCurMidiNote.Name = "tbCurMidiNote";
			this.tbCurMidiNote.ReadOnly = true;
			this.tbCurMidiNote.Size = new System.Drawing.Size(63, 20);
			this.tbCurMidiNote.TabIndex = 89;
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(399, 141);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(82, 13);
			this.label30.TabIndex = 88;
			this.label30.Text = "Hmm Midi Note ";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(429, 115);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(52, 13);
			this.label29.TabIndex = 87;
			this.label29.Text = "Midi Note";
			// 
			// lblCurAmpl
			// 
			this.lblCurAmpl.AutoSize = true;
			this.lblCurAmpl.Location = new System.Drawing.Point(364, 124);
			this.lblCurAmpl.Name = "lblCurAmpl";
			this.lblCurAmpl.Size = new System.Drawing.Size(19, 13);
			this.lblCurAmpl.TabIndex = 86;
			this.lblCurAmpl.Text = "db";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(325, 150);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(28, 13);
			this.label28.TabIndex = 85;
			this.label28.Text = "0 db";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(179, 150);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(37, 13);
			this.label27.TabIndex = 84;
			this.label27.Text = "-60 db";
			// 
			// tbSoundAmplitude
			// 
			this.tbSoundAmplitude.Location = new System.Drawing.Point(196, 118);
			this.tbSoundAmplitude.Maximum = 0;
			this.tbSoundAmplitude.Minimum = -60;
			this.tbSoundAmplitude.Name = "tbSoundAmplitude";
			this.tbSoundAmplitude.Size = new System.Drawing.Size(162, 45);
			this.tbSoundAmplitude.TabIndex = 83;
			this.tbSoundAmplitude.Scroll += new System.EventHandler(this.tbSoundAmplitude_Scroll);
			// 
			// tbCurNoteName
			// 
			this.tbCurNoteName.Location = new System.Drawing.Point(484, 86);
			this.tbCurNoteName.Name = "tbCurNoteName";
			this.tbCurNoteName.ReadOnly = true;
			this.tbCurNoteName.Size = new System.Drawing.Size(63, 20);
			this.tbCurNoteName.TabIndex = 81;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(420, 91);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(61, 13);
			this.label26.TabIndex = 82;
			this.label26.Text = "Note Name";
			// 
			// btnMicFollowing
			// 
			this.btnMicFollowing.Location = new System.Drawing.Point(14, 118);
			this.btnMicFollowing.Name = "btnMicFollowing";
			this.btnMicFollowing.Size = new System.Drawing.Size(152, 23);
			this.btnMicFollowing.TabIndex = 80;
			this.btnMicFollowing.Text = "Realtime mic following";
			this.btnMicFollowing.UseVisualStyleBackColor = true;
			this.btnMicFollowing.Click += new System.EventHandler(this.btnMicFollowing_Click);
			// 
			// nudTempo
			// 
			this.nudTempo.Location = new System.Drawing.Point(220, 92);
			this.nudTempo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudTempo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudTempo.Name = "nudTempo";
			this.nudTempo.Size = new System.Drawing.Size(66, 20);
			this.nudTempo.TabIndex = 79;
			this.nudTempo.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(174, 94);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(40, 13);
			this.label21.TabIndex = 78;
			this.label21.Text = "Tempo";
			// 
			// nudObservsCount
			// 
			this.nudObservsCount.Location = new System.Drawing.Point(352, 22);
			this.nudObservsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudObservsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudObservsCount.Name = "nudObservsCount";
			this.nudObservsCount.Size = new System.Drawing.Size(42, 20);
			this.nudObservsCount.TabIndex = 77;
			this.nudObservsCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			// 
			// nudWindowSize
			// 
			this.nudWindowSize.Location = new System.Drawing.Point(217, 22);
			this.nudWindowSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudWindowSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nudWindowSize.Name = "nudWindowSize";
			this.nudWindowSize.Size = new System.Drawing.Size(46, 20);
			this.nudWindowSize.TabIndex = 76;
			this.nudWindowSize.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(179, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 75;
			this.label3.Text = "Notes";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(269, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 73;
			this.label1.Text = "Observs Count";
			// 
			// btnNextNoteOpt
			// 
			this.btnNextNoteOpt.Location = new System.Drawing.Point(14, 56);
			this.btnNextNoteOpt.Name = "btnNextNoteOpt";
			this.btnNextNoteOpt.Size = new System.Drawing.Size(153, 23);
			this.btnNextNoteOpt.TabIndex = 63;
			this.btnNextNoteOpt.Text = "Manual following";
			this.btnNextNoteOpt.UseVisualStyleBackColor = true;
			this.btnNextNoteOpt.Click += new System.EventHandler(this.btnPlayNextNoteWindow_Click);
			// 
			// btnPlay12
			// 
			this.btnPlay12.BackColor = System.Drawing.Color.Azure;
			this.btnPlay12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnPlay12.Location = new System.Drawing.Point(652, 56);
			this.btnPlay12.Name = "btnPlay12";
			this.btnPlay12.Size = new System.Drawing.Size(30, 22);
			this.btnPlay12.TabIndex = 54;
			this.btnPlay12.Tag = "12";
			this.btnPlay12.Text = "-";
			this.btnPlay12.UseVisualStyleBackColor = false;
			this.btnPlay12.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(145, 24);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(69, 13);
			this.label14.TabIndex = 71;
			this.label14.Text = "Window Size";
			// 
			// btnPlay11
			// 
			this.btnPlay11.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay11.Location = new System.Drawing.Point(616, 56);
			this.btnPlay11.Name = "btnPlay11";
			this.btnPlay11.Size = new System.Drawing.Size(30, 22);
			this.btnPlay11.TabIndex = 53;
			this.btnPlay11.Tag = "11";
			this.btnPlay11.Text = "B";
			this.btnPlay11.UseVisualStyleBackColor = false;
			this.btnPlay11.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnFollowSequence
			// 
			this.btnFollowSequence.Location = new System.Drawing.Point(14, 89);
			this.btnFollowSequence.Name = "btnFollowSequence";
			this.btnFollowSequence.Size = new System.Drawing.Size(152, 23);
			this.btnFollowSequence.TabIndex = 39;
			this.btnFollowSequence.Text = "Realtime test following";
			this.btnFollowSequence.UseVisualStyleBackColor = true;
			this.btnFollowSequence.Click += new System.EventHandler(this.btnFollowSequence_Click);
			// 
			// btnPlay10
			// 
			this.btnPlay10.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay10.Location = new System.Drawing.Point(580, 56);
			this.btnPlay10.Name = "btnPlay10";
			this.btnPlay10.Size = new System.Drawing.Size(30, 22);
			this.btnPlay10.TabIndex = 52;
			this.btnPlay10.Tag = "10";
			this.btnPlay10.Text = "A#";
			this.btnPlay10.UseVisualStyleBackColor = false;
			this.btnPlay10.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(609, 17);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(73, 26);
			this.btnReset.TabIndex = 40;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnPlay9
			// 
			this.btnPlay9.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay9.Location = new System.Drawing.Point(544, 56);
			this.btnPlay9.Name = "btnPlay9";
			this.btnPlay9.Size = new System.Drawing.Size(30, 22);
			this.btnPlay9.TabIndex = 51;
			this.btnPlay9.Tag = "9";
			this.btnPlay9.Text = "A";
			this.btnPlay9.UseVisualStyleBackColor = false;
			this.btnPlay9.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay0
			// 
			this.btnPlay0.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay0.Location = new System.Drawing.Point(220, 56);
			this.btnPlay0.Name = "btnPlay0";
			this.btnPlay0.Size = new System.Drawing.Size(30, 22);
			this.btnPlay0.TabIndex = 42;
			this.btnPlay0.Tag = "0";
			this.btnPlay0.Text = "C";
			this.btnPlay0.UseVisualStyleBackColor = false;
			this.btnPlay0.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay8
			// 
			this.btnPlay8.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay8.Location = new System.Drawing.Point(508, 56);
			this.btnPlay8.Name = "btnPlay8";
			this.btnPlay8.Size = new System.Drawing.Size(30, 22);
			this.btnPlay8.TabIndex = 50;
			this.btnPlay8.Tag = "8";
			this.btnPlay8.Text = "G#";
			this.btnPlay8.UseVisualStyleBackColor = false;
			this.btnPlay8.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay1
			// 
			this.btnPlay1.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay1.Location = new System.Drawing.Point(256, 56);
			this.btnPlay1.Name = "btnPlay1";
			this.btnPlay1.Size = new System.Drawing.Size(30, 22);
			this.btnPlay1.TabIndex = 43;
			this.btnPlay1.Tag = "1";
			this.btnPlay1.Text = "C#";
			this.btnPlay1.UseVisualStyleBackColor = false;
			this.btnPlay1.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay7
			// 
			this.btnPlay7.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay7.Location = new System.Drawing.Point(472, 56);
			this.btnPlay7.Name = "btnPlay7";
			this.btnPlay7.Size = new System.Drawing.Size(30, 22);
			this.btnPlay7.TabIndex = 49;
			this.btnPlay7.Tag = "7";
			this.btnPlay7.Text = "G";
			this.btnPlay7.UseVisualStyleBackColor = false;
			this.btnPlay7.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay2
			// 
			this.btnPlay2.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay2.Location = new System.Drawing.Point(292, 56);
			this.btnPlay2.Name = "btnPlay2";
			this.btnPlay2.Size = new System.Drawing.Size(30, 22);
			this.btnPlay2.TabIndex = 44;
			this.btnPlay2.Tag = "2";
			this.btnPlay2.Text = "D";
			this.btnPlay2.UseVisualStyleBackColor = false;
			this.btnPlay2.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay6
			// 
			this.btnPlay6.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay6.Location = new System.Drawing.Point(436, 56);
			this.btnPlay6.Name = "btnPlay6";
			this.btnPlay6.Size = new System.Drawing.Size(30, 22);
			this.btnPlay6.TabIndex = 48;
			this.btnPlay6.Tag = "6";
			this.btnPlay6.Text = "F#";
			this.btnPlay6.UseVisualStyleBackColor = false;
			this.btnPlay6.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay3
			// 
			this.btnPlay3.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay3.Location = new System.Drawing.Point(328, 56);
			this.btnPlay3.Name = "btnPlay3";
			this.btnPlay3.Size = new System.Drawing.Size(30, 22);
			this.btnPlay3.TabIndex = 45;
			this.btnPlay3.Tag = "3";
			this.btnPlay3.Text = "D#";
			this.btnPlay3.UseVisualStyleBackColor = false;
			this.btnPlay3.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay5
			// 
			this.btnPlay5.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay5.Location = new System.Drawing.Point(400, 56);
			this.btnPlay5.Name = "btnPlay5";
			this.btnPlay5.Size = new System.Drawing.Size(30, 22);
			this.btnPlay5.TabIndex = 47;
			this.btnPlay5.Tag = "5";
			this.btnPlay5.Text = "F";
			this.btnPlay5.UseVisualStyleBackColor = false;
			this.btnPlay5.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// btnPlay4
			// 
			this.btnPlay4.BackColor = System.Drawing.Color.PapayaWhip;
			this.btnPlay4.Location = new System.Drawing.Point(364, 56);
			this.btnPlay4.Name = "btnPlay4";
			this.btnPlay4.Size = new System.Drawing.Size(30, 22);
			this.btnPlay4.TabIndex = 46;
			this.btnPlay4.Tag = "4";
			this.btnPlay4.Text = "E";
			this.btnPlay4.UseVisualStyleBackColor = false;
			this.btnPlay4.Click += new System.EventHandler(this.btnPlayNote_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox5.Controls.Add(this.tbTempoEstimation);
			this.groupBox5.Controls.Add(this.label38);
			this.groupBox5.Controls.Add(this.tbKvantEstimation);
			this.groupBox5.Controls.Add(this.label37);
			this.groupBox5.Controls.Add(this.tbEventEstimation);
			this.groupBox5.Controls.Add(this.label36);
			this.groupBox5.Controls.Add(this.tbLastPath);
			this.groupBox5.Controls.Add(this.tbPath);
			this.groupBox5.Controls.Add(this.tbLastObservations);
			this.groupBox5.Controls.Add(this.tbObservations);
			this.groupBox5.Controls.Add(this.cbUpdateMatrixes);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Controls.Add(this.label19);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.tbRating);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Controls.Add(this.tbTempo);
			this.groupBox5.Controls.Add(this.label16);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.tbCurrentState);
			this.groupBox5.Controls.Add(this.pbNoteProgress);
			this.groupBox5.Controls.Add(this.tbOnlineStepTime);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.tbLocalEstimation);
			this.groupBox5.Controls.Add(this.label7);
			this.groupBox5.Location = new System.Drawing.Point(542, 177);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(721, 223);
			this.groupBox5.TabIndex = 75;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Output Online";
			// 
			// tbTempoEstimation
			// 
			this.tbTempoEstimation.Location = new System.Drawing.Point(273, 186);
			this.tbTempoEstimation.Name = "tbTempoEstimation";
			this.tbTempoEstimation.ReadOnly = true;
			this.tbTempoEstimation.Size = new System.Drawing.Size(100, 20);
			this.tbTempoEstimation.TabIndex = 100;
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(182, 189);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(90, 13);
			this.label38.TabIndex = 101;
			this.label38.Text = "Tempo estimation";
			// 
			// tbKvantEstimation
			// 
			this.tbKvantEstimation.Location = new System.Drawing.Point(273, 160);
			this.tbKvantEstimation.Name = "tbKvantEstimation";
			this.tbKvantEstimation.ReadOnly = true;
			this.tbKvantEstimation.Size = new System.Drawing.Size(100, 20);
			this.tbKvantEstimation.TabIndex = 98;
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(182, 163);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(85, 13);
			this.label37.TabIndex = 99;
			this.label37.Text = "Kvant estimation";
			// 
			// tbEventEstimation
			// 
			this.tbEventEstimation.Location = new System.Drawing.Point(273, 134);
			this.tbEventEstimation.Name = "tbEventEstimation";
			this.tbEventEstimation.ReadOnly = true;
			this.tbEventEstimation.Size = new System.Drawing.Size(100, 20);
			this.tbEventEstimation.TabIndex = 96;
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(182, 137);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(85, 13);
			this.label36.TabIndex = 97;
			this.label36.Text = "Event estimation";
			// 
			// tbLastPath
			// 
			this.tbLastPath.Location = new System.Drawing.Point(421, 65);
			this.tbLastPath.Name = "tbLastPath";
			this.tbLastPath.ReadOnly = true;
			this.tbLastPath.Size = new System.Drawing.Size(278, 20);
			this.tbLastPath.TabIndex = 95;
			// 
			// tbPath
			// 
			this.tbPath.Location = new System.Drawing.Point(97, 65);
			this.tbPath.Name = "tbPath";
			this.tbPath.ReadOnly = true;
			this.tbPath.Size = new System.Drawing.Size(318, 20);
			this.tbPath.TabIndex = 94;
			// 
			// tbLastObservations
			// 
			this.tbLastObservations.Location = new System.Drawing.Point(421, 36);
			this.tbLastObservations.Name = "tbLastObservations";
			this.tbLastObservations.ReadOnly = true;
			this.tbLastObservations.Size = new System.Drawing.Size(278, 20);
			this.tbLastObservations.TabIndex = 93;
			// 
			// tbObservations
			// 
			this.tbObservations.Location = new System.Drawing.Point(97, 36);
			this.tbObservations.Name = "tbObservations";
			this.tbObservations.ReadOnly = true;
			this.tbObservations.Size = new System.Drawing.Size(318, 20);
			this.tbObservations.TabIndex = 92;
			// 
			// cbUpdateMatrixes
			// 
			this.cbUpdateMatrixes.AutoSize = true;
			this.cbUpdateMatrixes.Location = new System.Drawing.Point(612, 191);
			this.cbUpdateMatrixes.Name = "cbUpdateMatrixes";
			this.cbUpdateMatrixes.Size = new System.Drawing.Size(103, 17);
			this.cbUpdateMatrixes.TabIndex = 91;
			this.cbUpdateMatrixes.Text = "Update Matrixes";
			this.cbUpdateMatrixes.UseVisualStyleBackColor = true;
			this.cbUpdateMatrixes.CheckedChanged += new System.EventHandler(this.cbUpdateMatrixes_CheckedChanged);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(413, 11);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(27, 13);
			this.label20.TabIndex = 90;
			this.label20.Text = "Last";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(26, 65);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(29, 13);
			this.label19.TabIndex = 85;
			this.label19.Text = "Path";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(26, 39);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(69, 13);
			this.label18.TabIndex = 83;
			this.label18.Text = "Observations";
			// 
			// tbRating
			// 
			this.tbRating.Location = new System.Drawing.Point(474, 186);
			this.tbRating.Name = "tbRating";
			this.tbRating.ReadOnly = true;
			this.tbRating.Size = new System.Drawing.Size(100, 20);
			this.tbRating.TabIndex = 69;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(422, 187);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(38, 13);
			this.label17.TabIndex = 70;
			this.label17.Text = "Rating";
			// 
			// tbTempo
			// 
			this.tbTempo.Location = new System.Drawing.Point(97, 184);
			this.tbTempo.Name = "tbTempo";
			this.tbTempo.ReadOnly = true;
			this.tbTempo.Size = new System.Drawing.Size(61, 20);
			this.tbTempo.TabIndex = 67;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(51, 188);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(40, 13);
			this.label16.TabIndex = 68;
			this.label16.Text = "Tempo";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(429, 101);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(69, 13);
			this.label15.TabIndex = 66;
			this.label15.Text = "Current State";
			// 
			// tbCurrentState
			// 
			this.tbCurrentState.Location = new System.Drawing.Point(504, 98);
			this.tbCurrentState.Name = "tbCurrentState";
			this.tbCurrentState.ReadOnly = true;
			this.tbCurrentState.Size = new System.Drawing.Size(61, 20);
			this.tbCurrentState.TabIndex = 63;
			// 
			// pbNoteProgress
			// 
			this.pbNoteProgress.Location = new System.Drawing.Point(97, 98);
			this.pbNoteProgress.Name = "pbNoteProgress";
			this.pbNoteProgress.Size = new System.Drawing.Size(318, 20);
			this.pbNoteProgress.TabIndex = 62;
			// 
			// tbOnlineStepTime
			// 
			this.tbOnlineStepTime.Location = new System.Drawing.Point(97, 133);
			this.tbOnlineStepTime.Name = "tbOnlineStepTime";
			this.tbOnlineStepTime.ReadOnly = true;
			this.tbOnlineStepTime.Size = new System.Drawing.Size(61, 20);
			this.tbOnlineStepTime.TabIndex = 60;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(36, 136);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(55, 13);
			this.label9.TabIndex = 59;
			this.label9.Text = "Step Time";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(25, 95);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(44, 13);
			this.label10.TabIndex = 61;
			this.label10.Text = "Position";
			// 
			// tbLocalEstimation
			// 
			this.tbLocalEstimation.Location = new System.Drawing.Point(97, 158);
			this.tbLocalEstimation.Name = "tbLocalEstimation";
			this.tbLocalEstimation.ReadOnly = true;
			this.tbLocalEstimation.Size = new System.Drawing.Size(61, 20);
			this.tbLocalEstimation.TabIndex = 25;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(7, 161);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 13);
			this.label7.TabIndex = 33;
			this.label7.Text = "Viterbi Estimation";
			// 
			// timerMetronome
			// 
			this.timerMetronome.Tick += new System.EventHandler(this.timerMetronome_Tick);
			// 
			// frmMatrices
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1434, 882);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmMatrices";
			this.Text = "Matrices";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMatrices_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvTransitions)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvEmissions)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvInitial)).EndInit();
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.pnlNotesViewer.ResumeLayout(false);
			this.pnlNotesViewer.PerformLayout();
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudTempoVariationErrorPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTempoErrorPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudExtraErrorPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWrongErrorPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMissedErrorPercent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudErrorPercent)).EndInit();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBarCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tbSoundAmplitude)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTempo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudObservsCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWindowSize)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timerFollowSequence;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridView dgvEmissions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridView dgvTransitions;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DataGridView dgvInitial;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.TextBox tbInputSequence;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Button btnClearInputSequence;
		private System.Windows.Forms.TextBox tbGhostsCount;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cmbScore;
		private System.Windows.Forms.Button btnGenerateMatricies;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbTimeOffline;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbResultProbabOffline;
		private System.Windows.Forms.TextBox tbOutputSequence;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnViterbi;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.NumericUpDown nudObservsCount;
		private System.Windows.Forms.NumericUpDown nudWindowSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNextNoteOpt;
		private System.Windows.Forms.Button btnPlay12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button btnPlay11;
		private System.Windows.Forms.Button btnFollowSequence;
		private System.Windows.Forms.Button btnPlay10;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnPlay9;
		private System.Windows.Forms.Button btnPlay0;
		private System.Windows.Forms.Button btnPlay8;
		private System.Windows.Forms.Button btnPlay1;
		private System.Windows.Forms.Button btnPlay7;
		private System.Windows.Forms.Button btnPlay2;
		private System.Windows.Forms.Button btnPlay6;
		private System.Windows.Forms.Button btnPlay3;
		private System.Windows.Forms.Button btnPlay5;
		private System.Windows.Forms.Button btnPlay4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Panel pnlNotesViewer;
		private MusicNotesRendererLib.MusicalNotesViewer musicalNotesViewer;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox tbRating;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox tbTempo;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox tbCurrentState;
		private System.Windows.Forms.ProgressBar pbNoteProgress;
		private System.Windows.Forms.TextBox tbOnlineStepTime;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbLocalEstimation;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudTempo;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.NumericUpDown nudErrorPercent;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.CheckBox cbUpdateMatrixes;
		private System.Windows.Forms.NumericUpDown nudExtraErrorPercent;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.NumericUpDown nudWrongErrorPercent;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.NumericUpDown nudMissedErrorPercent;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox tbCurNoteName;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Button btnMicFollowing;
		private System.Windows.Forms.TextBox tbLastPath;
		private System.Windows.Forms.TextBox tbPath;
		private System.Windows.Forms.TextBox tbLastObservations;
		private System.Windows.Forms.TextBox tbObservations;
		private System.Windows.Forms.Label lblCurAmpl;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TrackBar tbSoundAmplitude;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox tbCurHmmMidiNote;
		private System.Windows.Forms.TextBox tbCurMidiNote;
		private System.Windows.Forms.NumericUpDown nudTempoVariationErrorPercent;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.NumericUpDown nudTempoErrorPercent;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.CheckBox cbParallel;
		private System.Windows.Forms.TextBox tbMetronome;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Timer timerMetronome;
		private System.Windows.Forms.ComboBox cmbMinDuration;
		private System.Windows.Forms.Button btnSetDefaults;
		private System.Windows.Forms.ComboBox cmbObservsCount;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.NumericUpDown nudBarCount;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label lblPitchsCount;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.TextBox tbEventEstimation;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox tbTempoEstimation;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.TextBox tbKvantEstimation;
		private System.Windows.Forms.Label label37;
	}
}