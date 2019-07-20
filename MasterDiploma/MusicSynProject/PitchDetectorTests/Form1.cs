using System;
using System.Windows.Forms;
using MusicSyncLib;
using System.Threading;
using System.Globalization;
using ZedGraph;
using System.Diagnostics;

namespace PitchDetectorTests
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			int samplingRate = Convert.ToInt32(nudSamplingRate.Value);
			float freq = (float)nudFreq.Value;

			int pcmDataLength = (int)nudSampleCount.Value / 2;
			float[] pcm = new float[pcmDataLength];
			var wave = PitchUtils.CreateSineWave(pcm, pcmDataLength, samplingRate, freq, 1.0f, 0);

			var stopwatch = new Stopwatch();

			float[] fft = new FFT(WindowType.Rectangle, cbParallel.Checked).Calculate(pcm);
			var zcrDetector = new ZCR(samplingRate, cbParallel.Checked);

			stopwatch.Restart();
			float zcr = zcrDetector.Detect(pcm, fft);
			stopwatch.Stop();
			tbZCR.Text = zcr.ToString();
			tbZCRTime.Text = stopwatch.ElapsedTicks.ToString();

			var autocorrelDetector = new AutocorrelationDetector(samplingRate, cbParallel.Checked);
			stopwatch.Restart();
			float autocorrel = autocorrelDetector.Detect(pcm, fft);
			stopwatch.Stop();
			tbAutocorrel.Text = autocorrel.ToString();
			tbAutocorrelationTime.Text = stopwatch.ElapsedTicks.ToString();

			var tracker = new PitchTracker();
			tracker.SampleRate = samplingRate;
			stopwatch.Restart();
			tracker.ProcessBuffer(pcm);
			float autocorrelNew = tracker.CurrentPitchRecord.Pitch;
			stopwatch.Stop();
			tbAutocorrelNew.Text = autocorrelNew.ToString();
			tbAutocorrelationNewTime.Text = stopwatch.ElapsedTicks.ToString();

			var hpsDetector = new HPS(samplingRate, cbParallel.Checked, 1);
			stopwatch.Restart();
			float hps = hpsDetector.Detect(pcm, fft);
			stopwatch.Stop();
			tbHPS.Text = hps.ToString();
			tbHPSTime.Text = stopwatch.ElapsedTicks.ToString();

			var maxLikehoodDetector = new MaximumLikehoodDetector(samplingRate, cbParallel.Checked);
			stopwatch.Restart();
			fft = new FFT(WindowType.Rectangle, cbParallel.Checked).Calculate(pcm);
			float maxLikehood = maxLikehoodDetector.Detect(pcm, fft);
			stopwatch.Stop();
			tbMaxLikehood.Text = maxLikehood.ToString();
			tbMaxLikehoodTime.Text = stopwatch.ElapsedTicks.ToString();
		}

		private void btnPlot_Click(object sender, EventArgs e)
		{
			int minFreq = (int)nudMinFreq.Value;
			int maxFreq = (int)nudMaxFreq.Value;
			int stepFreq = (int)nudFreqStep.Value;

			int samplingRate = (int)nudSamplingRate.Value;
			int pcmDataLength = (int)nudSampleCount.Value / 2;
			float[] pcm = new float[samplingRate];

			var zcr = new ZCR(samplingRate, cbParallel.Checked);
			var autocorrel = new AutocorrelationDetector(samplingRate, cbParallel.Checked);
			var tracker = new PitchTracker();
			tracker.SampleRate = samplingRate;
			var maxLikehood = new MaximumLikehoodDetector(samplingRate, cbParallel.Checked);

			var zcrPairList = new PointPairList();
			var autocorrelPairList = new PointPairList();
			var autocorrelNewPairList = new PointPairList();
			var maxLikehoodPairList = new PointPairList();

			for (int i = minFreq; i < maxFreq; i += stepFreq)
			{
				var wave = PitchUtils.CreateSineWave(pcm, samplingRate, samplingRate, i, 1.0f, 0/*, (float)i / 10, 0.05f*/);
				float[] fft = new FFT(WindowType.Rectangle, cbParallel.Checked).Calculate(pcm);

				zcrPairList.Add(i, Math.Abs(i - zcr.Detect(pcm, fft)));
				autocorrelPairList.Add(i,  Math.Abs(i - autocorrel.Detect(pcm, fft)));
				tracker.ProcessBuffer(pcm);
				autocorrelNewPairList.Add(i, Math.Abs(i - tracker.CurrentPitchRecord.Pitch));
				maxLikehoodPairList.Add(i, Math.Abs(i - maxLikehood.Detect(pcm, fft)));
			}

			zedGraphControl1.GraphPane.Title.Text = "Абсолютная ошибка";
			var graphPane = zedGraphControl1.GraphPane;
			graphPane.CurveList.Clear();
			graphPane.XAxis.Title.Text = "Частота (Hz)";
			graphPane.YAxis.Title.Text = "Ошибка (Hz)";

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Пересечение с нулем", zcrPairList, System.Drawing.Color.Sienna, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Автокорреляция", autocorrelPairList, System.Drawing.Color.DarkGreen, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Автокорреляция модифицированная", autocorrelNewPairList, System.Drawing.Color.OrangeRed, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Гармоническое перемножение спектров", maxLikehoodPairList, System.Drawing.Color.SkyBlue, SymbolType.None);

			graphPane.AxisChange();
			zedGraphControl1.Refresh();
		}

		private void btnPlotTimes_Click(object sender, EventArgs e)
		{
			int minSamples = (int)nudMinSamples.Value;
			int maxSamples = (int)nudMaxSamples.Value;
			int samplesStep = (int)nudSamplesStep.Value;

			var zcrPairList = new PointPairList();
			var autocorrelPairList = new PointPairList();
			var autocorrelNewPairList = new PointPairList();
			var maxLikehoodPairList = new PointPairList();

			for (int i = minSamples; i < maxSamples; i += samplesStep)
			{
				float[] pcm = new float[i];
				var wave = PitchUtils.CreateSineWave(pcm, i, i, 400, 1.0f, 0);
				float[] fft = new FFT(WindowType.Rectangle, cbParallel.Checked).Calculate(pcm);

				var zcr = new ZCR(i, cbParallel.Checked);
				var autocorrel = new AutocorrelationDetector(i, cbParallel.Checked);
				var tracker = new PitchTracker();
				tracker.SampleRate = i;
				var maxLikehood = new MaximumLikehoodDetector(i, cbParallel.Checked);

				var stopwatch = new Stopwatch();
				stopwatch.Restart();
				zcr.Detect(pcm, fft);
				stopwatch.Stop();
				zcrPairList.Add(i, stopwatch.ElapsedMilliseconds);

				stopwatch.Restart();
				autocorrel.Detect(pcm, fft);
				stopwatch.Stop();
				autocorrelPairList.Add(i, stopwatch.ElapsedMilliseconds);

				stopwatch.Restart();
				tracker.ProcessBuffer(pcm);
				stopwatch.Stop();
				autocorrelNewPairList.Add(i, stopwatch.ElapsedMilliseconds);

				stopwatch.Restart();
				fft = new FFT(WindowType.Rectangle, cbParallel.Checked).Calculate(pcm);
				maxLikehood.Detect(pcm, fft);
				stopwatch.Stop();
				maxLikehoodPairList.Add(i, stopwatch.ElapsedMilliseconds);
			}

			zedGraphControl2.GraphPane.Title.Text = "Время вычисления";
			var graphPane = zedGraphControl2.GraphPane;
			graphPane.CurveList.Clear();
			graphPane.XAxis.Title.Text = "Размер фрагмента (кол-во отсчетов)";
			graphPane.YAxis.Title.Text = "Время вычисления (мсек)";

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Пересечение с нулем", zcrPairList, System.Drawing.Color.Sienna, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Автокорреляция", autocorrelPairList, System.Drawing.Color.DarkGreen, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Автокорреляция модифицированная", autocorrelNewPairList, System.Drawing.Color.OrangeRed, SymbolType.None);

			graphPane.GraphObjList.Clear();
			graphPane.AddCurve("Гармоническое перемножение спектров", maxLikehoodPairList, System.Drawing.Color.SkyBlue, SymbolType.None);

			graphPane.AxisChange();
			zedGraphControl2.Refresh();
		}
	}
}
