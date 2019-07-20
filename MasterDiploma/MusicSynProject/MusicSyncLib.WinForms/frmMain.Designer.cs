namespace MusicSyncLib.WinForms
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
			this.components = new System.ComponentModel.Container();
			this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
			this.btnDetectPitchs = new System.Windows.Forms.Button();
			this.btnLoadScore = new System.Windows.Forms.Button();
			this.tbThreshold = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPieceSize = new System.Windows.Forms.TextBox();
			this.btnLoadAudio = new System.Windows.Forms.Button();
			this.tbMusicNotes = new System.Windows.Forms.TextBox();
			this.tbInputAudio = new System.Windows.Forms.TextBox();
			this.tbInputScore = new System.Windows.Forms.TextBox();
			this.btnOpenAudio = new System.Windows.Forms.Button();
			this.btnOpenScore = new System.Windows.Forms.Button();
			this.openAudioDialog = new System.Windows.Forms.OpenFileDialog();
			this.openScoreDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnShowMatrices = new System.Windows.Forms.Button();
			this.timerPlayback = new System.Windows.Forms.Timer(this.components);
			this.btnRecord = new System.Windows.Forms.Button();
			this.tbMidiId = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPlay = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnStopRT = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbId13 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tbNote = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.dgvRecognizedPitchs = new System.Windows.Forms.DataGridView();
			this.columnMidiId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnId13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label6 = new System.Windows.Forms.Label();
			this.tbFrequency = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRecognizedPitchs)).BeginInit();
			this.SuspendLayout();
			// 
			// zedGraphControl1
			// 
			this.zedGraphControl1.Location = new System.Drawing.Point(224, 12);
			this.zedGraphControl1.Name = "zedGraphControl1";
			this.zedGraphControl1.ScrollGrace = 0D;
			this.zedGraphControl1.ScrollMaxX = 0D;
			this.zedGraphControl1.ScrollMaxY = 0D;
			this.zedGraphControl1.ScrollMaxY2 = 0D;
			this.zedGraphControl1.ScrollMinX = 0D;
			this.zedGraphControl1.ScrollMinY = 0D;
			this.zedGraphControl1.ScrollMinY2 = 0D;
			this.zedGraphControl1.Size = new System.Drawing.Size(431, 222);
			this.zedGraphControl1.TabIndex = 0;
			// 
			// btnDetectPitchs
			// 
			this.btnDetectPitchs.Location = new System.Drawing.Point(12, 155);
			this.btnDetectPitchs.Name = "btnDetectPitchs";
			this.btnDetectPitchs.Size = new System.Drawing.Size(189, 23);
			this.btnDetectPitchs.TabIndex = 2;
			this.btnDetectPitchs.Text = "Detect pitchs";
			this.btnDetectPitchs.UseVisualStyleBackColor = true;
			this.btnDetectPitchs.Click += new System.EventHandler(this.btnDetectPitchs_Click);
			// 
			// btnLoadScore
			// 
			this.btnLoadScore.Location = new System.Drawing.Point(12, 462);
			this.btnLoadScore.Name = "btnLoadScore";
			this.btnLoadScore.Size = new System.Drawing.Size(189, 23);
			this.btnLoadScore.TabIndex = 4;
			this.btnLoadScore.Text = "Load Score";
			this.btnLoadScore.UseVisualStyleBackColor = true;
			this.btnLoadScore.Click += new System.EventHandler(this.btnLoadScore_Click);
			// 
			// tbThreshold
			// 
			this.tbThreshold.Location = new System.Drawing.Point(111, 115);
			this.tbThreshold.Name = "tbThreshold";
			this.tbThreshold.Size = new System.Drawing.Size(90, 20);
			this.tbThreshold.TabIndex = 5;
			this.tbThreshold.Text = "0.01";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 118);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Threshold";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Piece size (sec)";
			// 
			// tbPieceSize
			// 
			this.tbPieceSize.Location = new System.Drawing.Point(111, 89);
			this.tbPieceSize.Name = "tbPieceSize";
			this.tbPieceSize.Size = new System.Drawing.Size(90, 20);
			this.tbPieceSize.TabIndex = 7;
			this.tbPieceSize.Text = "0.2";
			// 
			// btnLoadAudio
			// 
			this.btnLoadAudio.Location = new System.Drawing.Point(15, 47);
			this.btnLoadAudio.Name = "btnLoadAudio";
			this.btnLoadAudio.Size = new System.Drawing.Size(186, 23);
			this.btnLoadAudio.TabIndex = 9;
			this.btnLoadAudio.Text = "Load audio";
			this.btnLoadAudio.UseVisualStyleBackColor = true;
			this.btnLoadAudio.Click += new System.EventHandler(this.btnLoadAudio_Click);
			// 
			// tbMusicNotes
			// 
			this.tbMusicNotes.Location = new System.Drawing.Point(673, 371);
			this.tbMusicNotes.Multiline = true;
			this.tbMusicNotes.Name = "tbMusicNotes";
			this.tbMusicNotes.ReadOnly = true;
			this.tbMusicNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbMusicNotes.Size = new System.Drawing.Size(231, 142);
			this.tbMusicNotes.TabIndex = 10;
			// 
			// tbInputAudio
			// 
			this.tbInputAudio.Location = new System.Drawing.Point(15, 12);
			this.tbInputAudio.Name = "tbInputAudio";
			this.tbInputAudio.Size = new System.Drawing.Size(152, 20);
			this.tbInputAudio.TabIndex = 11;
			this.tbInputAudio.Text = "..\\..\\..\\Data\\test.mp3";
			// 
			// tbInputScore
			// 
			this.tbInputScore.Location = new System.Drawing.Point(15, 436);
			this.tbInputScore.Name = "tbInputScore";
			this.tbInputScore.Size = new System.Drawing.Size(152, 20);
			this.tbInputScore.TabIndex = 12;
			this.tbInputScore.Text = "..\\..\\..\\Data\\test.xml";
			// 
			// btnOpenAudio
			// 
			this.btnOpenAudio.Location = new System.Drawing.Point(173, 12);
			this.btnOpenAudio.Name = "btnOpenAudio";
			this.btnOpenAudio.Size = new System.Drawing.Size(28, 20);
			this.btnOpenAudio.TabIndex = 13;
			this.btnOpenAudio.Text = "...";
			this.btnOpenAudio.UseVisualStyleBackColor = true;
			this.btnOpenAudio.Click += new System.EventHandler(this.btnOpenAudio_Click);
			// 
			// btnOpenScore
			// 
			this.btnOpenScore.Location = new System.Drawing.Point(173, 434);
			this.btnOpenScore.Name = "btnOpenScore";
			this.btnOpenScore.Size = new System.Drawing.Size(28, 23);
			this.btnOpenScore.TabIndex = 14;
			this.btnOpenScore.Text = "...";
			this.btnOpenScore.UseVisualStyleBackColor = true;
			this.btnOpenScore.Click += new System.EventHandler(this.btnOpenScore_Click);
			// 
			// openAudioDialog
			// 
			this.openAudioDialog.FileName = "openFileDialog1";
			this.openAudioDialog.Filter = "Audio files|*.mp3;*.ogg;*.wav";
			// 
			// openScoreDialog
			// 
			this.openScoreDialog.FileName = "openFileDialog1";
			this.openScoreDialog.Filter = "MusicXML|*.xml";
			// 
			// btnShowMatrices
			// 
			this.btnShowMatrices.Location = new System.Drawing.Point(12, 491);
			this.btnShowMatrices.Name = "btnShowMatrices";
			this.btnShowMatrices.Size = new System.Drawing.Size(189, 22);
			this.btnShowMatrices.TabIndex = 18;
			this.btnShowMatrices.Text = "Show matrices";
			this.btnShowMatrices.UseVisualStyleBackColor = true;
			this.btnShowMatrices.Click += new System.EventHandler(this.btnShowMatrices_Click);
			// 
			// btnRecord
			// 
			this.btnRecord.Location = new System.Drawing.Point(22, 19);
			this.btnRecord.Name = "btnRecord";
			this.btnRecord.Size = new System.Drawing.Size(75, 23);
			this.btnRecord.TabIndex = 21;
			this.btnRecord.Text = "Record";
			this.btnRecord.UseVisualStyleBackColor = true;
			this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
			// 
			// tbMidiId
			// 
			this.tbMidiId.Location = new System.Drawing.Point(99, 65);
			this.tbMidiId.Name = "tbMidiId";
			this.tbMidiId.ReadOnly = true;
			this.tbMidiId.Size = new System.Drawing.Size(75, 20);
			this.tbMidiId.TabIndex = 22;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnStop);
			this.groupBox1.Controls.Add(this.btnPlay);
			this.groupBox1.Location = new System.Drawing.Point(224, 240);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(225, 61);
			this.groupBox1.TabIndex = 23;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "File";
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(103, 22);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 23);
			this.btnStop.TabIndex = 22;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			// 
			// btnPlay
			// 
			this.btnPlay.Location = new System.Drawing.Point(22, 22);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(75, 23);
			this.btnPlay.TabIndex = 21;
			this.btnPlay.Text = "Play";
			this.btnPlay.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnStopRT);
			this.groupBox2.Controls.Add(this.btnRecord);
			this.groupBox2.Location = new System.Drawing.Point(224, 307);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(225, 58);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Realtime";
			// 
			// btnStopRT
			// 
			this.btnStopRT.Location = new System.Drawing.Point(103, 19);
			this.btnStopRT.Name = "btnStopRT";
			this.btnStopRT.Size = new System.Drawing.Size(75, 23);
			this.btnStopRT.TabIndex = 23;
			this.btnStopRT.Text = "Stop";
			this.btnStopRT.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.tbFrequency);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.tbId13);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.tbNote);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.tbMidiId);
			this.groupBox3.Location = new System.Drawing.Point(455, 240);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 173);
			this.groupBox3.TabIndex = 25;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Output";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 27;
			this.label5.Text = "Id 13";
			// 
			// tbId13
			// 
			this.tbId13.Location = new System.Drawing.Point(99, 117);
			this.tbId13.Name = "tbId13";
			this.tbId13.ReadOnly = true;
			this.tbId13.Size = new System.Drawing.Size(75, 20);
			this.tbId13.TabIndex = 26;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 93);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 13);
			this.label4.TabIndex = 25;
			this.label4.Text = "Note";
			// 
			// tbNote
			// 
			this.tbNote.Location = new System.Drawing.Point(99, 91);
			this.tbNote.Name = "tbNote";
			this.tbNote.ReadOnly = true;
			this.tbNote.Size = new System.Drawing.Size(75, 20);
			this.tbNote.TabIndex = 24;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Midi Id";
			// 
			// dgvRecognizedPitchs
			// 
			this.dgvRecognizedPitchs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvRecognizedPitchs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnMidiId,
            this.columnNote,
            this.columnId13});
			this.dgvRecognizedPitchs.Location = new System.Drawing.Point(673, 12);
			this.dgvRecognizedPitchs.Name = "dgvRecognizedPitchs";
			this.dgvRecognizedPitchs.Size = new System.Drawing.Size(231, 353);
			this.dgvRecognizedPitchs.TabIndex = 26;
			// 
			// columnMidiId
			// 
			this.columnMidiId.HeaderText = "Midi Id";
			this.columnMidiId.Name = "columnMidiId";
			this.columnMidiId.Width = 21;
			// 
			// columnNote
			// 
			this.columnNote.HeaderText = "Note";
			this.columnNote.Name = "columnNote";
			this.columnNote.Width = 21;
			// 
			// columnId13
			// 
			this.columnId13.HeaderText = "Id13";
			this.columnId13.Name = "columnId13";
			this.columnId13.Width = 21;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 41);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 13);
			this.label6.TabIndex = 29;
			this.label6.Text = "Frequency";
			// 
			// tbFrequency
			// 
			this.tbFrequency.Location = new System.Drawing.Point(99, 39);
			this.tbFrequency.Name = "tbFrequency";
			this.tbFrequency.ReadOnly = true;
			this.tbFrequency.Size = new System.Drawing.Size(75, 20);
			this.tbFrequency.TabIndex = 28;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1094, 617);
			this.Controls.Add(this.dgvRecognizedPitchs);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnShowMatrices);
			this.Controls.Add(this.btnOpenScore);
			this.Controls.Add(this.btnOpenAudio);
			this.Controls.Add(this.tbInputScore);
			this.Controls.Add(this.tbInputAudio);
			this.Controls.Add(this.tbMusicNotes);
			this.Controls.Add(this.btnLoadAudio);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbPieceSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbThreshold);
			this.Controls.Add(this.btnLoadScore);
			this.Controls.Add(this.btnDetectPitchs);
			this.Controls.Add(this.zedGraphControl1);
			this.Name = "frmMain";
			this.Text = "Music sync";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRecognizedPitchs)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl zedGraphControl1;
		private System.Windows.Forms.Button btnDetectPitchs;
		private System.Windows.Forms.Button btnLoadScore;
		private System.Windows.Forms.TextBox tbThreshold;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbPieceSize;
		private System.Windows.Forms.Button btnLoadAudio;
		private System.Windows.Forms.TextBox tbMusicNotes;
		private System.Windows.Forms.TextBox tbInputAudio;
		private System.Windows.Forms.TextBox tbInputScore;
		private System.Windows.Forms.Button btnOpenAudio;
		private System.Windows.Forms.Button btnOpenScore;
		private System.Windows.Forms.OpenFileDialog openAudioDialog;
		private System.Windows.Forms.OpenFileDialog openScoreDialog;
		private System.Windows.Forms.Button btnShowMatrices;
		private System.Windows.Forms.Timer timerPlayback;
		private System.Windows.Forms.Button btnRecord;
		private System.Windows.Forms.TextBox tbMidiId;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnStopRT;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbId13;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbNote;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dgvRecognizedPitchs;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnMidiId;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnNote;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnId13;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbFrequency;
	}
}

