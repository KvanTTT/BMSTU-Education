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
using NAudio.Wave;
using System.IO;
using ZedGraph;

namespace MusicSyncLib.WinForms
{
	public partial class frmMain : Form
	{
		string _inputAudioFile = @"..\..\..\Data\test.mp3";
		string _inputScoreFile = @"..\..\..\Data\test.xml";

		#region Fields

		WaveMp3FileDecoder Decoder;

		#endregion

		public frmMain()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
			Decoder = new WaveMp3FileDecoder();
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			btnLoadAudio_Click(sender, e);

			GuiUtils.EnableDoubleBuffering(dgvRecognizedPitchs);
		}

		private void btnLoadAudio_Click(object sender, EventArgs e)
		{
			Decoder.LoadAudioFile(tbInputAudio.Text);

			PlotGraphics(zedGraphControl1, Decoder.PcmData);

			btnDetectPitchs_Click(sender, e);
		}

		private void btnDetectPitchs_Click(object sender, EventArgs e)
		{
			/*Decoder.PieceLengthInSec = double.Parse(tbPieceSize.Text);
			Decoder.DetectLevelThreshold = double.Parse(tbThreshold.Text);
			Decoder.RecognizePitchs();
			dgvRecognizedPitchs.Rows.Clear();
			foreach (var pitch in Decoder.Pitchs)
			{
				var noteName = pitch.MidiNote;
				dgvRecognizedPitchs.Rows.Add(pitch.MidiNote, 
					MusicalTemperament.MidiNoteToNoteName(pitch.MidiNote),
					MusicalHmmData.MidiNoteToHmmMidiNote(pitch.MidiNote));
			}*/
		}

		private void btnLoadScore_Click(object sender, EventArgs e)
		{
			/*if (!string.IsNullOrEmpty(tbInputScore.Text))
				_inputScoreFile = tbInputScore.Text;

			var xmlDecoder = new MusicXmlDecoder(_inputScoreFile);
			//notesViewer.LoadFromXmlFile(_inputScoreFile);

			var musicalHmmData = new MusicalHmmData(xmlDecoder.Events);

			var builder = new StringBuilder();
			musicalHmmData.GenerateAlignedEvents();
			int[] events = musicalHmmData.EventKvants;
			foreach (var ev in events)
				builder.AppendLine(ev.ToString());
			tbMusicNotes.Text = builder.ToString();*/
		}

		private void btnOpenAudio_Click(object sender, EventArgs e)
		{
			if (openAudioDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				tbInputAudio.Text = openAudioDialog.FileName;
			}
		}

		private void btnOpenScore_Click(object sender, EventArgs e)
		{
			if (openScoreDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				tbInputScore.Text = openScoreDialog.FileName;
			}
		}

		private void btnShowMatrices_Click(object sender, EventArgs e)
		{
			var form = new frmMatrices();
			form.Show();
		}

		private void btnPlay_Click(object sender, EventArgs e)
		{
			WaveOut waveOutDevice = new WaveOut();
			var mainOutputStream = CreateInputStream(_inputAudioFile);
			//mainOutputStream.Position
			
			waveOutDevice.Init(mainOutputStream);
			waveOutDevice.Play();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
		}

		private void btnRecord_Click(object sender, EventArgs e)
		{
			var waveIn = new WaveIn();
			waveIn.WaveFormat = new WaveFormat();
			waveIn.DataAvailable += OnDataAvailable;
			waveIn.RecordingStopped += OnRecordingStopped;

			Decoder = new MusicSyncLib.WaveMp3FileDecoder();
			Decoder.DetectLevelThreshold = 0;
			Decoder.InitRealtimeMode(waveIn.WaveFormat.Channels, waveIn.WaveFormat.BitsPerSample, waveIn.WaveFormat.SampleRate, 200,
				(PitchTracker tracker, PitchRecord record) =>
				{
					tbFrequency.Text = record.Pitch.ToString();
					tbMidiId.Text = record.MidiNote.ToString();
					tbNote.Text = MusicalTemperament.MidiNoteToNoteName(record.MidiNote);
					//tbId13.Text = MusicalHmmData.MidiNoteToHmmMidiNote(record.MidiNote).ToString();
				});

			waveIn.StartRecording();
		}

		void OnDataAvailable(object sender, WaveInEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
			}
			else
			{
				Decoder.AddDataInRealTime(AudioUtils.ConvertBuffer(e.Buffer, 1, 16));
			}
		}

		void OnRecordingStopped(object sender, StoppedEventArgs e)
		{

		}

		#region Utils

		private static void PlotGraphics(ZedGraphControl zedGraphControl, float[] data)
		{
			var graphPane = zedGraphControl.GraphPane;
			graphPane.CurveList.Clear();
			graphPane.Title.Text = "";
			graphPane.XAxis.Title.Text = "";
			graphPane.YAxis.Title.Text = "";
			var list = new PointPairList(GenerateXData(data.Length), data.Select(v => (double)v).ToArray());
			graphPane.AddCurve("PCM data", list, Color.OrangeRed, SymbolType.None);

			zedGraphControl.AxisChange();

			Navigate(zedGraphControl, data.Length / 2);

			zedGraphControl.Refresh();
		}

		private WaveStream CreateInputStream(string fileName)
		{
			WaveChannel32 inputStream;
			if (fileName.EndsWith(".mp3"))
			{
				WaveStream mp3Reader = new Mp3FileReader(fileName);
				inputStream = new WaveChannel32(mp3Reader);
			}
			else
			{
				throw new InvalidOperationException("Unsupported extension");
			}
			return inputStream;
		}

		private static double[] GenerateXData(int length)
		{
			double[] result = new double[length];
			for (int i = 0; i < length; i++)
				result[i] = i;
			return result;
		}

		private static void Navigate(ZedGraphControl graphControl, double pos)
		{
			LineObj threshHoldLine = new LineObj(Color.Blue, pos, graphControl.GraphPane.YAxis.Scale.Min, pos, graphControl.GraphPane.YAxis.Scale.Max);
			graphControl.GraphPane.GraphObjList.Add(threshHoldLine);
		}

		#endregion

		
	}
}
