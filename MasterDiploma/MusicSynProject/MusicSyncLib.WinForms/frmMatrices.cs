using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using MusicSyncLib.WinForms.Properties;
using NAudio.Wave;

namespace MusicSyncLib.WinForms
{
	public partial class frmMatrices : Form
	{
		#region Constants

		private DataGridViewCellStyle DefaultItemCellStyle;
		private DataGridViewCellStyle CurrentItemCellStyle;
		private DataGridViewCellStyle GhostItemCellStyle;
		private DataGridViewCellStyle WindowCellStyle;
		private DataGridViewCellStyle NewMusicalEventItemCellStyle;

		#endregion

		#region Fields

		MusicalNotesDecoder _musicXmlDecoder;
		MusicalHmmData _musicHmmData;
		MusicalHmmOffline _musicHmmModelOffline;
		MusicalHmmOnline _musicHmmModelOnline;
		MusicalFollower _musicFollower;
		WaveMp3FileDecoder _audioDecoder;
		WaveIn _waveIn;
		Random _random;

		int _currentNoteInd = 0;
		int[] _inputSequence;
		int _currentBeat = 0;

		double _missedRatio;
		double _wrongRatio;
		double _extraRatio;
	
		#endregion

		#region Initialization

		public frmMatrices()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			InitializeComponent();

			GuiUtils.EnableDoubleBuffering(dgvTransitions);
			GuiUtils.EnableDoubleBuffering(dgvEmissions);
			GuiUtils.EnableDoubleBuffering(dgvTransitions);

			DefaultItemCellStyle = new DataGridViewCellStyle();
			DefaultItemCellStyle.Font = Font;

			CurrentItemCellStyle = new DataGridViewCellStyle();
			CurrentItemCellStyle.BackColor = Color.PaleTurquoise;
			CurrentItemCellStyle.Font = Font;

			GhostItemCellStyle = new DataGridViewCellStyle();
			GhostItemCellStyle.BackColor = Color.Beige;
			GhostItemCellStyle.ForeColor = Color.DarkGray;
			GhostItemCellStyle.Font = Font;
			
			WindowCellStyle = new DataGridViewCellStyle();
			WindowCellStyle.BackColor = Color.PaleGreen;

			NewMusicalEventItemCellStyle = new DataGridViewCellStyle();
			//NewMusicalEventItemCellStyle.ForeColor = Color.Red;
			NewMusicalEventItemCellStyle.Font = new Font(Font, FontStyle.Bold);

			tbGhostsCount.Text = Settings.Default.GhostsCount.ToString();
			cmbMinDuration.SelectedItem = Settings.Default.SplitCoef.ToString();
			nudWindowSize.Value = Settings.Default.WindowSize;
			nudObservsCount.Value = Settings.Default.ObservationsCount;
			nudTempo.Value = Settings.Default.TempoOnline;
			nudErrorPercent.Value = (int)(Settings.Default.ErrorRatio * 100);
			_missedRatio = Settings.Default.MissedErrorRatio;
			_wrongRatio = Settings.Default.WrongErrorRatio;
			_extraRatio = Settings.Default.ExtraErrorRatio;
			nudMissedErrorPercent.Value = (int)Math.Round(_missedRatio * 100);
			nudWrongErrorPercent.Value = (int)Math.Round(_wrongRatio * 100);
			nudExtraErrorPercent.Value = (int)Math.Round(_extraRatio * 100);
			tbSoundAmplitude.Value = Settings.Default.MinSoundAmplitude;
			cbUpdateMatrixes.Checked = Settings.Default.UpdateMatrixes;
			nudTempoErrorPercent.Value = (int)Math.Round(Settings.Default.TempoErrorRatio * 100);
			nudTempoVariationErrorPercent.Value = (int)Math.Round(Settings.Default.TempoVariationRatio * 100);
			cmbObservsCount.SelectedItem = Settings.Default.HmmObservsCount.ToString();
			cbParallel.Checked = Settings.Default.Parallel;
			nudBarCount.Value = Settings.Default.BarCount;

			string[] files = Directory.GetFiles(@"..\..\..\Data", "*.xml", SearchOption.TopDirectoryOnly);
			foreach (var file in files)
				cmbScore.Items.Add(Path.GetFileNameWithoutExtension(file));
			cmbScore.SelectedItem = Settings.Default.CompositionName;

			_random = new Random();
		}

		private void frmMatrices_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.CompositionName = cmbScore.SelectedItem.ToString();
			Settings.Default.GhostsCount = int.Parse(tbGhostsCount.Text);
			Settings.Default.SplitCoef = Convert.ToDouble(cmbMinDuration.SelectedItem);
			Settings.Default.WindowSize = (int)(nudWindowSize.Value);
			Settings.Default.ObservationsCount = (int)(nudObservsCount.Value);
			Settings.Default.TempoOnline = (int)(nudTempo.Value);
			Settings.Default.ErrorRatio = (double)nudErrorPercent.Value / 100.0;
			Settings.Default.MissedErrorRatio = _missedRatio;
			Settings.Default.WrongErrorRatio = _wrongRatio;
			Settings.Default.ExtraErrorRatio = _extraRatio;
			Settings.Default.UpdateMatrixes = cbUpdateMatrixes.Checked;
			Settings.Default.MinSoundAmplitude = tbSoundAmplitude.Value;
			Settings.Default.TempoErrorRatio = (double)nudTempoErrorPercent.Value / 100.0;
			Settings.Default.TempoVariationRatio = (double)nudTempoVariationErrorPercent.Value / 100.0;
			Settings.Default.Parallel = cbParallel.Checked;
			Settings.Default.ObservationsCount = Convert.ToInt32((int)nudObservsCount.Value);
			Settings.Default.BarCount = (int)nudBarCount.Value;
			Settings.Default.Save();
		}

		#endregion

		#region Offline

		private void cmbScore_SelectedIndexChanged(object sender, EventArgs e)
		{
			string compositionPath = @"..\..\..\Data\" + cmbScore.SelectedItem.ToString() + ".xml";
			_musicXmlDecoder = new MusicXmlDecoder(compositionPath);
			musicalNotesViewer.LoadFromXmlFile(compositionPath);
			GenerateHmmData();
			ClearMatrixesStyles();
			PerformViterbiOffline();
			ResetOnline();
		}

		private void btnGenerateMatricies_Click(object sender, EventArgs e)
		{
			ClearMatrixesStyles();
			GenerateHmmData();
		}

		private void btnViterbi_Click(object sender, EventArgs e)
		{
			ClearMatrixesStyles();
			PerformViterbiOffline();
		}

		private void btnClearInputSequence_Click(object sender, EventArgs e)
		{
			int minMidiId = _musicHmmData.EventKvants[0].MidiNote;
			int maxMidiId = minMidiId;
			for (int i = 1; i < _musicHmmData.EventKvants.Count; i++)
			{
				if (_musicHmmData.EventKvants[i].MidiNote < minMidiId)
					minMidiId = _musicHmmData.EventKvants[i].MidiNote;
				else if (_musicHmmData.EventKvants[i].MidiNote > maxMidiId)
					maxMidiId = _musicHmmData.EventKvants[i].MidiNote;
			}

			List<int> inputSequence = new List<int>(_musicHmmData.EventKvants.Count);
			Random rand = new Random();
			double errorRatio = (double)nudErrorPercent.Value / 100;
			for (int i = 0; i < _musicHmmData.EventKvants.Count; i++)
			{
				int note = _musicHmmData.EventKvants[i].MidiNote;
				if (errorRatio != 0)
				{
					double errorRand = rand.NextDouble();
					if (errorRand < errorRatio)
					{
						double errorTypeRand = rand.NextDouble();
						if (errorTypeRand < _missedRatio)
						{
							// missed
							continue;
						}
						else if (errorTypeRand >= _missedRatio && errorTypeRand < _missedRatio + _wrongRatio)
						{
							// wrong
							int wrongNote;
							do
							{
								wrongNote = rand.Next(minMidiId, maxMidiId + 1);
							}
							while (wrongNote == note);
							inputSequence.Add(wrongNote);
						}
						else if (errorTypeRand >= _missedRatio + _wrongRatio && errorTypeRand < _missedRatio + _wrongRatio + _extraRatio)
						{
							//extra
							int extraNote = rand.Next(minMidiId, maxMidiId + 1);
							inputSequence.Add(extraNote);
							inputSequence.Add(note);
						}
						else
						{
							inputSequence.Add(note);
						}
					}
					else
					{
						inputSequence.Add(note);
					}
				}
				else
				{
					inputSequence.Add(note);
				}
			}
			tbInputSequence.Text = string.Join(",", inputSequence);
		}

		private void nudMissedErrorPercent_ValueChanged(object sender, EventArgs e)
		{
			_missedRatio = (double)(sender as NumericUpDown).Value / 100.0;
			/*double newValue = (double)(sender as NumericUpDown).Value / 100.0;
			double diff = newValue - _missedRatio;
			_missedRatio = newValue;
			_wrongRatio -= diff / 2;
			_extraRatio = 1.0 - _missedRatio - _wrongRatio;
			nudWrongErrorPercent.Value = (int)Math.Round(_wrongRatio * 100);
			nudExtraErrorPercent.Value = (int)Math.Round(_extraRatio * 100);*/
		}

		private void nudWrongErrorPercent_ValueChanged(object sender, EventArgs e)
		{
			_wrongRatio = (double)(sender as NumericUpDown).Value / 100.0;
			/*double newValue = (double)(sender as NumericUpDown).Value / 100.0;
			double diff = newValue - _wrongRatio;
			_wrongRatio = newValue;
			_missedRatio -= diff / 2;
			_extraRatio = 1.0 - _missedRatio - _wrongRatio;
			nudMissedErrorPercent.Value = (int)Math.Round(_missedRatio * 100);
			nudExtraErrorPercent.Value = (int)Math.Round(_extraRatio * 100);*/
		}

		private void nudExtraErrorPercent_ValueChanged(object sender, EventArgs e)
		{
			_extraRatio = (double)(sender as NumericUpDown).Value / 100.0;
			/*double newValue = (double)(sender as NumericUpDown).Value / 100.0;
			double diff = newValue - _extraRatio;
			_extraRatio = newValue;
			_missedRatio -= diff / 2;
			_wrongRatio = 1.0 - _missedRatio - _extraRatio;
			nudMissedErrorPercent.Value = (int)Math.Round(_missedRatio * 100);
			nudWrongErrorPercent.Value = (int)Math.Round(_wrongRatio * 100);*/
		}

		#endregion

		#region Online

		private void btnReset_Click(object sender, EventArgs e)
		{
			ClearMatrixesStyles();
			ResetOnline();
		}

		private void btnFollowSequence_Click(object sender, EventArgs e)
		{
			tbInputSequence.Enabled = false;
			
			if (!timerFollowSequence.Enabled)
			{
				if (!_musicHmmModelOnline.Started)
				{
					_musicFollower = new MusicalFollower(_musicHmmModelOnline, (int)nudTempo.Value, _musicXmlDecoder.Beats, _musicXmlDecoder.BeatType);
					timerFollowSequence.Interval = _musicFollower.GetTimerInterval((int)nudTempo.Value);
					timerMetronome.Interval = _musicFollower.GetMetronomeInterval((int)nudTempo.Value);
					_inputSequence = tbInputSequence.Text.Split(',').Select(s => int.Parse(s)).ToArray();
				}

				timerFollowSequence.Start();
				timerMetronome.Start();
				btnFollowSequence.Text = "Stop";
			}
			else
			{
				timerFollowSequence.Stop();
				timerMetronome.Stop();
				btnFollowSequence.Text = "Follow the Sequence";
			}
		}

		private void btnPlayNextNote_Click(object sender, EventArgs e)
		{
			tbInputSequence.Enabled = false;
			FollowObservation(_inputSequence[_currentNoteInd++]);
		}

		private void btnPlayNextNoteWindow_Click(object sender, EventArgs e)
		{
			tbInputSequence.Enabled = false;
			FollowObservation(_inputSequence[_currentNoteInd++]);
		}

		private void btnPlayNote_Click(object sender, EventArgs e)
		{
			if (_currentNoteInd > 0 && _currentNoteInd < _inputSequence.Length)
			{
				tbInputSequence.Enabled = false;
				int note = Convert.ToInt32(((Button)sender).Tag);
				if (note == _inputSequence[_currentNoteInd])
					_currentNoteInd++;
				FollowObservation(note);
			}
		}

		private void timerFollowSequence_Tick(object sender, EventArgs e)
		{
			if (_currentNoteInd >= 0 && _currentNoteInd < _inputSequence.Length)
			{
				FollowObservation(_inputSequence[_currentNoteInd++]);

				double r = _random.NextDouble();
				if (r < (double)nudTempoErrorPercent.Value / 100.0)
				{
					int interval = _musicFollower.GetTimerInterval((int)nudTempo.Value);
					double variance = interval * (double)nudTempoVariationErrorPercent.Value / 100.0 * 2;
					timerFollowSequence.Interval = (int)Math.Round(interval - variance / 2 + _random.NextDouble() * variance);
				}
				else
				{
					timerFollowSequence.Interval = _musicFollower.GetTimerInterval((int)nudTempo.Value);
				}
			}
			else
			{
				timerFollowSequence.Stop();
				timerMetronome.Stop();
				btnFollowSequence.Text = "Ended";
			}
		}

		private void timerMetronome_Tick(object sender, EventArgs e)
		{
			if (_currentBeat == 1)
				tbMetronome.Font = new System.Drawing.Font(Font, FontStyle.Bold);
			else
				tbMetronome.Font = new System.Drawing.Font(Font, FontStyle.Regular);
			tbMetronome.Text = _currentBeat + "/" + _musicXmlDecoder.BeatType;
			_currentBeat = _currentBeat % _musicXmlDecoder.BeatType + 1;
		}

		private void cbUpdateMatrixes_CheckedChanged(object sender, EventArgs e)
		{
			if (cbUpdateMatrixes.Checked)
			{
				dgvTransitions.Enabled = true;
				dgvEmissions.Enabled = true;
				dgvInitial.Enabled = true;
			}
			else
			{
				ClearMatrixesStyles();
				dgvTransitions.Enabled = false;
				dgvEmissions.Enabled = false;
				dgvInitial.Enabled = false;
			}
		}

		private void btnMicFollowing_Click(object sender, EventArgs e)
		{
			_musicFollower = new MusicalFollower(_musicHmmModelOnline, (int)nudTempo.Value, _musicXmlDecoder.BeatType, _musicXmlDecoder.Beats);

			_audioDecoder = new WaveMp3FileDecoder();
			_audioDecoder.DetectLevelThreshold = AudioUtils.DecibelToPower(tbSoundAmplitude.Value);

			_waveIn = new WaveIn();
			_waveIn.WaveFormat = new WaveFormat(44100, 1);
			_waveIn.DataAvailable += OnDataAvailable;
			_waveIn.RecordingStopped += OnRecordingStopped;

			var stopwatch = new Stopwatch();
			TimeSpan oldTime = new TimeSpan();

			lblPitchsCount.Text = "0";
			_audioDecoder.InitRealtimeMode(_waveIn.WaveFormat.Channels, _waveIn.WaveFormat.BitsPerSample, _waveIn.WaveFormat.SampleRate,
				_musicFollower.GetTimerInterval((int)nudTempo.Value),
				(PitchTracker tracker, PitchRecord record) =>
				{
					lblPitchsCount.Text = (int.Parse(lblPitchsCount.Text) + 1).ToString();
					lblTime.Text = (stopwatch.Elapsed - oldTime).ToString();
					oldTime = stopwatch.Elapsed;

					tbCurNoteName.Text = MusicalTemperament.MidiNoteToNoteName(record.MidiNote);
					tbCurMidiNote.Text = record.MidiNote <= -1 ? "-" : record.MidiNote.ToString();
					tbCurHmmMidiNote.Text = _musicHmmData.MidiNoteToHmmMidiNote(record.MidiNote).ToString();
					FollowObservation(record.MidiNote);
				});
			_waveIn.StartRecording();
			stopwatch.Start();
			oldTime = stopwatch.Elapsed;

			timerMetronome.Interval = _musicFollower.GetMetronomeInterval((int)nudTempo.Value);
			timerMetronome.Start();

			btnMicFollowing.Text = "Stop";
		}

		private void OnDataAvailable(object sender, WaveInEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
			}
			else
			{
				_audioDecoder.AddDataInRealTime(AudioUtils.ConvertBuffer(e.Buffer, _waveIn.WaveFormat.Channels, _waveIn.WaveFormat.BitsPerSample));
			}
		}

		private void OnRecordingStopped(object sender, StoppedEventArgs e)
		{
		}

		private void tbSoundAmplitude_Scroll(object sender, EventArgs e)
		{
			if (_audioDecoder != null)
				_audioDecoder.DetectLevelThreshold = AudioUtils.DecibelToPower(tbSoundAmplitude.Value);
			lblCurAmpl.Text = tbSoundAmplitude.Value + " db";
		}

		private void btnSetDefaults_Click(object sender, EventArgs e)
		{
			nudObservsCount.Value = (int)Math.Round(
				_musicXmlDecoder.Beats / Convert.ToDouble(cmbMinDuration.SelectedItem) * (int)nudBarCount.Value * 4 / _musicXmlDecoder.BeatType);
			nudWindowSize.Value = nudObservsCount.Value * 4;
		}

		#endregion

		#region Utils

		private void GenerateHmmData()
		{
			_musicHmmData = new MusicalHmmData(_musicXmlDecoder.Events, Convert.ToDouble(cmbMinDuration.SelectedItem), int.Parse(tbGhostsCount.Text),
				Convert.ToInt32(cmbObservsCount.SelectedItem));
			_musicHmmData.GenerateEventsAndMatricies();

			musicalNotesViewer.InitMusicalHmm(_musicHmmData);

			if (cbUpdateMatrixes.Checked)
			{
				GuiUtils.BindMatrix(dgvTransitions, _musicHmmData.Transitions, "0.##");
				GuiUtils.BindMatrix(dgvEmissions, _musicHmmData.Emissions);
				GuiUtils.BindMatrix(dgvInitial, _musicHmmData.Initial);
			}

			btnClearInputSequence_Click(null, null);
		}

		private void PerformViterbiOffline()
		{
			double logLikehood;
			_musicHmmModelOffline = new MusicalHmmOffline(_musicHmmData);

			_inputSequence = tbInputSequence.Text.Split(',').Select(s => int.Parse(s)).ToArray();
			var stopwatch = new Stopwatch();
			stopwatch.Start(); 
			int[] outputArray = _musicHmmModelOffline.Decode(_inputSequence, out logLikehood, cbParallel.Checked);
			stopwatch.Stop();

			tbOutputSequence.Text = string.Join(",", outputArray);
			tbResultProbabOffline.Text = logLikehood.ToString("0.00000000");
			tbTimeOffline.Text = stopwatch.Elapsed.ToString();

			var outputSequence = tbOutputSequence.Text.Split(',').Select(s => int.Parse(s)).ToArray();

			if (cbUpdateMatrixes.Checked)
			{
				for (int i = 0; i < outputSequence.Length; i++)
				{
					if (i != 0)
						dgvTransitions[outputSequence[i], outputSequence[i - 1]].Style = CurrentItemCellStyle;
					else
						dgvTransitions[outputSequence[i], outputSequence[i]].Style = CurrentItemCellStyle;

					dgvEmissions[_musicHmmData.MidiNoteToHmmMidiNote(_inputSequence[i]), outputSequence[i]].Style = CurrentItemCellStyle;
				}
			}
		}

		private void ResetOnline()
		{
			tbInputSequence.Enabled = true;

			_musicHmmModelOnline = new MusicalHmmOnline(_musicHmmData, (int)nudWindowSize.Value, (int)nudObservsCount.Value);
			_musicFollower = new MusicalFollower(_musicHmmModelOnline, (int)nudTempo.Value, _musicXmlDecoder.Beats, _musicXmlDecoder.BeatType);
			_inputSequence = tbInputSequence.Text.Split(',').Select(s => int.Parse(s)).ToArray();
			_currentNoteInd = 0;
			_currentBeat = 1;

			timerFollowSequence.Stop();
			timerMetronome.Stop();

			if (_waveIn != null)
				_waveIn.StopRecording();

			if (cbUpdateMatrixes.Checked)
			{
				GuiUtils.NavigateToItem(dgvTransitions, 0, 0);
				GuiUtils.NavigateToItem(dgvEmissions, 0, 0);
				GuiUtils.NavigateToItem(dgvInitial, 0, 0);
			}

			tbObservations.Text = "";
			tbLastObservations.Text = "";
			tbPath.Text = "";
			tbLastPath.Text = "";
			tbCurNoteName.Text = "";
			tbCurMidiNote.Text = "";
			tbCurHmmMidiNote.Text = "";
			tbOnlineStepTime.Text = "";
			tbMetronome.Text = "";
			tbEventEstimation.Text = "";
			tbKvantEstimation.Text = "";
			tbTempoEstimation.Text = "";
			tbRating.Text = "";

			btnFollowSequence.Text = "Follow the Sequence";
			btnMicFollowing.Text = "Realtime mic following";

			musicalNotesViewer.NavigateToEvent(-1);
			pbNoteProgress.Minimum = 0;
			pbNoteProgress.Maximum = _musicHmmData.EventKvants.Count;
			pbNoteProgress.Value = 0;
			pnlNotesViewer.HorizontalScroll.Value = pnlNotesViewer.HorizontalScroll.Minimum;
		}

		private void ClearMatrixesStyles()
		{
			if (cbUpdateMatrixes.Checked)
			{
				GuiUtils.UpdateItemsStyle(dgvTransitions, DefaultItemCellStyle);
				GuiUtils.UpdateItemsStyle(dgvEmissions, GhostItemCellStyle);
				for (int i = _musicHmmData.GhostStatesCount; i < _musicHmmData.Emissions.GetLength(0); i += _musicHmmData.GhostStatesCount + 1)
					GuiUtils.UpdateItemsStyle(dgvEmissions.Rows[i], DefaultItemCellStyle);
				int ind = 0;
				for (int i = 0; i < _musicHmmData.Events.Count; i++)
				{
					GuiUtils.UpdateItemsStyle(dgvEmissions.Rows[ind * (_musicHmmData.GhostStatesCount + 1)], NewMusicalEventItemCellStyle);
					ind += _musicHmmData.Events[i].Kvants.Count;
				}
				GuiUtils.UpdateItemsStyle(dgvInitial, DefaultItemCellStyle);
			}
		}

		private void FollowObservation(int observation)
		{
			if (!_musicHmmModelOnline.IsEnded)
			{
				if (!_musicHmmModelOnline.Started)
				{
					ClearMatrixesStyles();
					_musicFollower.Start();
				}

				HmmWindow hmmBounds;
				if (_musicHmmModelOnline.Started)
				{
					//hmmBounds = _musicHmmModelOnline.Bounds;
					//GuiUtils.UpdateItemStyles(dgvTransitions, hmmBounds.LeftBound, hmmBounds.LeftBound, hmmBounds.RightBound, hmmBounds.RightBound, DefaultItemCellStyle);
				}

				Stopwatch stopwatch = new Stopwatch();
				double logLikehood;

				stopwatch.Start();
				_musicFollower.ProcessObservation(observation, out logLikehood);
				stopwatch.Stop();

				if (_musicHmmModelOnline.IsEnded)
					MessageBox.Show("Ended!");

				if (cbUpdateMatrixes.Checked)
				{
					if (_musicHmmModelOnline.OldPos == -1)
						GuiUtils.NavigateToItem(dgvTransitions, _musicHmmModelOnline.LastPos, _musicHmmModelOnline.LastPos, CurrentItemCellStyle);
					else
						GuiUtils.NavigateToItem(dgvTransitions, _musicHmmModelOnline.OldPos, _musicHmmModelOnline.LastPos, CurrentItemCellStyle);
					GuiUtils.NavigateToItem(dgvEmissions, _musicHmmModelOnline.LastPos, _musicHmmData.MidiNoteToHmmMidiNote(observation), CurrentItemCellStyle);
				}

				musicalNotesViewer.NavigateToEventKvant(_musicHmmModelOnline.LastEventNumber, _musicHmmModelOnline.LastLocalKvantNumber);
				ScrollToCurrentPos();

				hmmBounds = _musicHmmModelOnline.Bounds;
				//GuiUtils.UpdateItemStyles(dgvTransitions, hmmBounds.LeftBound, hmmBounds.LeftBound, hmmBounds.RightBound, hmmBounds.RightBound, WindowCellStyle);

				var observations = _musicHmmModelOnline.Observations;
				var lastObservations = _musicHmmModelOnline.LastObservations;
				var path = _musicHmmModelOnline.Path;
				var lastPath = _musicHmmModelOnline.LastPath;

				tbObservations.Text = "";
				tbLastObservations.Text = "";
				tbObservations.AppendText(string.Join(",", observations));
				tbLastObservations.AppendText(string.Join(",", lastObservations));

				tbLastPath.Text = "";
				tbPath.Text = string.Join(",", path);
				tbLastPath.Text = string.Join(",", lastPath);

				tbCurrentState.Text = _musicHmmModelOnline.LastPos.ToString();
				tbLocalEstimation.Text = logLikehood.ToString();
				pbNoteProgress.Value = _musicHmmModelOnline.LastGlobalKvantNumber;
				tbOnlineStepTime.Text = stopwatch.Elapsed.ToString();
				tbTempo.Text = _musicFollower.GetCurrentTempo().ToString();

				tbKvantEstimation.Text = _musicFollower.KvantPenalty.ToString();
				tbTempoEstimation.Text = _musicFollower.TempoPenalty.ToString();
				tbRating.Text = _musicFollower.Rating.ToString();
			}
		}

		private void ScrollToCurrentPos()
		{
			var horizScroll = pnlNotesViewer.HorizontalScroll;
			int newScroll =
				(int)Math.Round(
				(float)(musicalNotesViewer.CursorPixelPos - pnlNotesViewer.ClientSize.Width / 2.0) /
				(musicalNotesViewer.Width - pnlNotesViewer.ClientSize.Width) *
				(horizScroll.Maximum - horizScroll.LargeChange - horizScroll.Minimum));
			if (newScroll < pnlNotesViewer.HorizontalScroll.Minimum)
				newScroll = pnlNotesViewer.HorizontalScroll.Minimum;
			if (newScroll > pnlNotesViewer.HorizontalScroll.Maximum)
				newScroll = pnlNotesViewer.HorizontalScroll.Maximum;
			pnlNotesViewer.HorizontalScroll.Value = newScroll;
		}

		#endregion

		private void dgvTransitions_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
