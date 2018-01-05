using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Saluse.MediaKit.Delegates;
using System.Collections.Generic;
using System.Windows.Shapes;
using Alias = System.Windows;
using KvantSound;
using Saluse.MediaKit.Sample;
using System.Linq;
using Utilities;

namespace SilverClient
{  
	public partial class WaveVisualizer : UserControl
	{
		#region private delegates

		private delegate void RenderVisualsDelegate(Sample sample);

		#endregion		

		/// <summary>
		///  Describes an Audio Effect type
		/// </summary>
		public class EffectDescription
		{
			public enum EffectTypeEnumeration
			{
				None,
				Pan,
				Noise,
				Echo,
				PitchShift,
				Duet
			}

			public string Description
			{
				get;
				set;
			}

			public EffectTypeEnumeration EffectType
			{
				get;
				set;
			}
		}

		private WriteableBitmap _outputWriteableBitmap;
		private DispatcherTimer _timer = null;
		private int[] _clearBuffer;
		private int _primaryColour = Utilities.Colour.BuildColour(0xff, 0x00, 0x7f, 0xff);
		private int _alternatePrimaryColour = Utilities.Colour.BuildColour(0xff, 0x00, 0x6f, 0xcf);
		private int _secondaryColour = Utilities.Colour.BuildColour(0xff, 0x00, 0xff, 0x00);
		private int _clearColour = Utilities.Colour.BuildColour(0xff, 0x00, 0x00, 0x00);
		private int _pixelStride;
		private OpenFileDialog _openFileDialog;
		private int[] _lastPeaks = null;
		private int[] _maximumPeaks = null;
		private RenderVisualsDelegate _currentRenderVisuals = null;
		private AudioPreProcessorDelegate _currentAudioPreProcessor = null;
		private ObservableCollection<VisualDescription> visualDescriptions;
		private int _xRunningScanCounter = 0;
		private int _marginLeft = 29;
		private int _widthWithoutMarginRight = 0;
		private double _effectAffectValue = 0;
		private object _visualLock = new object();
		private Random _randomGenerator;
		private Queue<byte[]> _echoBuffer = new Queue<byte[]>();
		private int _echoCounter = 1;
		private int _echoThreshold = 5;
		private int _SamplePerSecond = 0;
		private Rectangle _clearShape;
		private bool _clearEffectIsEnabled = false;
		private bool _scanIsCleared;
		private bool _fftScanIsCleared;
		private VisualTypeEnumeration visualType;

		private int[] _peakMeterFrequencies = new int[19] { 30, 55, 80, 120, 155, 195, 250, 375, 500, 750, 1000, 1500, 2000, 3000, 4000, 6000, 8000, 12000, 16000 };
		private int[] _fftScanFrequencies = new int[10] { 30, 80, 155, 250, 375, 500, 750, 1000, 4000, 8000 };

		#region private methods

		private void InitialiseDescriptions()
		{
			visualDescriptions = new ObservableCollection<VisualDescription>();
			visualDescriptions.Add(new VisualDescription() { Description = "Отключено", VisualType = VisualTypeEnumeration.None });
			visualDescriptions.Add(new VisualDescription() { Description = "Измеритель пиков", VisualType = VisualTypeEnumeration.PeakMeter });
			visualDescriptions.Add(new VisualDescription() { Description = "Осциллограф", VisualType = VisualTypeEnumeration.Oscilloscope });
			visualDescriptions.Add(new VisualDescription() { Description = "Диапазон", VisualType = VisualTypeEnumeration.Scope });
			visualDescriptions.Add(new VisualDescription() { Description = "Анализ", VisualType = VisualTypeEnumeration.Scan });
			visualDescriptions.Add(new VisualDescription() { Description = "Band Scan", VisualType = VisualTypeEnumeration.FFTScan });
			visualDescriptions.Add(new VisualDescription() { Description = "Volume", VisualType = VisualTypeEnumeration.Volume });
			visualDescriptions.Add(new VisualDescription() { Description = "FFT", VisualType = VisualTypeEnumeration.FFT });
		}

		private int[] InitialiseArray(int size, byte initialValue)
		{
			int[] newArray = new int[size];
			for (int index = 0; index < size; index++)
			{
				newArray[index] = initialValue;
			}

			return newArray;
		}

		private void InitBitmap()
		{
			_outputWriteableBitmap = new WriteableBitmap((int)LayoutRoot.ActualWidth, (int)LayoutRoot.ActualHeight);
			outputImage.Source = _outputWriteableBitmap;
			_pixelStride = _outputWriteableBitmap.PixelWidth;

			_clearShape = new Rectangle()
			{
				Width = _outputWriteableBitmap.PixelWidth,
				Height = _outputWriteableBitmap.PixelHeight,
				Fill = new SolidColorBrush(Color.FromArgb(32, 0, 0, 0)) // the less opaque it is, the longer the blur effects lasts
			};

			_clearBuffer = Utilities.Array.Instantiate(_outputWriteableBitmap.Pixels.Length, _clearColour);
			_lastPeaks = InitialiseArray(FourierTransform.FREQUENCYSLOTCOUNT, byte.MaxValue);
			_maximumPeaks = InitialiseArray(FourierTransform.FREQUENCYSLOTCOUNT, byte.MaxValue);

			_widthWithoutMarginRight = Convert.ToInt32(outputImage.ActualWidth - _marginLeft);

			// Set the defaults on the UI
			this.DataContext = this;
		}

		private void Initialise()
		{
			_randomGenerator = new Random();

			// Set default visuals
		   // _currentRenderVisuals = new RenderVisualsDelegate(RenderPeakMeter);
			// Set default audio effect
		   // _currentAudioPreProcessor = new AudioPreProcessorDelegate(PanAudioPreProcessor);

			_openFileDialog = new OpenFileDialog { Filter = "MP3 files (*.mp3)|*.mp3" };

			InitBitmap();

			SetVisualType(VisualDescriptions[2]);
			//cmbVisualDescriptions.SelectedItem = VisualDescriptions[1]; // Peak Meter
		}

		private void ClearOutputBitmap()
		{
			// Clear bitmap
			_clearBuffer.CopyTo(_outputWriteableBitmap.Pixels, 0);
		}

		private void RunClearEffect()
		{
			_outputWriteableBitmap.Render(_clearShape, new MatrixTransform());
		}

		private void FlushOutputBitmap()
		{
			_outputWriteableBitmap.Invalidate();
		}



		#region Visual Rendering

		public struct OffsetColor
		{
			public double Offset;
			public int Color;
		}

		private void DrawFilledRectangle(WriteableBitmap bmp, OffsetColor[] Colors)
		{
		   /* int[] IntOffsets = new int[Colors.Length + 1];
			for (int i = 0; i < Colors.Length; i++)
				IntOffsets[i] = (int)Math.Round(Colors[i].Offset);
			IntOffsets[Colors.Length] = Colors.Length;

			for (int i = 0; i < Colors.Length; i++)
			{
				for (int j = IntOffsets[i]; j < IntOffsets[i + 1]; j++)
				{

				}
			}*/
		}

		private void RenderVolume(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				//short MeanVolume = VolumeSample.GetAvgVolume(Sample);
			   // double MeanVolumeDouble = (double)MeanVolume / 32768;				

				if (!(_clearEffectIsEnabled))
				{
					ClearOutputBitmap();
				}

				int Height = _outputWriteableBitmap.PixelHeight;
				int Width = _outputWriteableBitmap.PixelWidth;
				//_outputWriteableBitmap.FillRectangle(0, (int)Math.Round(Height * (1 - MeanVolumeDouble)),
				//	Width, Height, Utilities.Colour.BuildColour(0xff, 0x00, 0x7f, 0x00));

				if (_clearEffectIsEnabled)
				{
					RunClearEffect();
				}

				FlushOutputBitmap();
			});
		}

		private void RenderOscilloscope(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				int y = 0;
				int x = 0;
				const int leftOffset = 10;
				int sampleWidth = Sample.PreparedData.Length;
				int previousLeftX = leftOffset;
				int previousLeftY = 128;

				if (!(_clearEffectIsEnabled))
				{
					ClearOutputBitmap();
				}

				for (int sampleIndex = 0; sampleIndex < sampleWidth; sampleIndex++)
				{
					x = sampleIndex + leftOffset;

					y = ((Sample.PreparedData[sampleIndex] >> 8) + 192);
					_outputWriteableBitmap.DrawLine(previousLeftX, previousLeftY, x, y, _primaryColour);
					previousLeftX = x;
					previousLeftY = y;
				}

				if (_clearEffectIsEnabled)
				{
					RunClearEffect();
				}

				FlushOutputBitmap();
			});
		}

		private void RenderScope(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				int y = 0;
				int x = 10;
				int sampleWidth = Sample.PreparedData.Length;

				if (!(_clearEffectIsEnabled))
				{
					ClearOutputBitmap();
				}

				for (int sampleIndex = 0; sampleIndex < sampleWidth; sampleIndex++)
				{
					y = (Math.Abs(Sample.PreparedData[sampleIndex] >> 8));
					_outputWriteableBitmap.DrawLine(x, (128 - y), x, (y + 128), _primaryColour);

					//y = (Math.Abs(Sample.PreparedData[sampleIndex + 1] >> 8));
					//_outputWriteableBitmap.AddLine(x, (256 - y), x, (y + 256), _secondaryColour);

					x++;
				}

				if (_clearEffectIsEnabled)
				{
					RunClearEffect();
				}

				FlushOutputBitmap();
			});
		}

		private void RenderScan(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				byte strength = 0;
				int y = 0;
				int sampleWidth = Sample.PreparedData.Length;
				int height = (int)LayoutRoot.ActualHeight;

				if (_scanIsCleared)
				{
					ClearOutputBitmap();
					_xRunningScanCounter = _marginLeft;
					_scanIsCleared = false;
				}

				for (int sampleIndex = 0; sampleIndex < sampleWidth; sampleIndex++)
				{
					strength = (byte)(Math.Abs(Sample.PreparedData[sampleIndex] >> 7));
					//strength = (byte)((strength + Math.Abs(Sample.PreparedData[sampleIndex + 1] >> 7)) >> 1);

					_outputWriteableBitmap.SetPixel(_xRunningScanCounter, y, 0, strength, 0);
					y++;
					if (y >= height)
					{
						break;
					}
				}

				_outputWriteableBitmap.DrawLine((_xRunningScanCounter + 1), 0, (_xRunningScanCounter + 1), height, _primaryColour);

				_xRunningScanCounter++;
				if (_xRunningScanCounter >= _widthWithoutMarginRight)
				{
					_outputWriteableBitmap.DrawLine(_xRunningScanCounter, 0, _xRunningScanCounter, height, _clearColour);
					_xRunningScanCounter = _marginLeft;
				}

				FlushOutputBitmap();
			});
		}

		/// <summary>
		/// 	Calculates the next position for the slowing flowing animation on the
		/// 	peak meter
		/// </summary>
		/// <param name="currentValue"></param>
		/// <param name="comparisonValue"></param>
		/// <param name="speed"></param>
		/// <param name="cutOff"></param>
		/// <returns></returns>
		private int CalculatePeakDelay(int currentValue, int comparisonValue, int speed, int cutOff)
		{
			int combinedValue = ((currentValue + comparisonValue) >> 1);
			int runningValue = Math.Min(currentValue, combinedValue);
			if (runningValue < combinedValue)
			{
				runningValue += speed; // return to empty bar quicker
				if (runningValue > cutOff)
				{
					runningValue = cutOff;
				}
			}
			else
			{
				runningValue = combinedValue;
			}

			return runningValue;
		}

		private void RenderPeakMeter(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				try
				{
					int barSpacing = 25;
					int barWidth = (barSpacing - 8);
					int lineWidth = (barWidth + 1);
					int leftOffset = 60;
					byte[] frequencyPeaks = PeakMeter.CalculateFrequencies(Sample.Spectrogram, 44100);

					if (!(_clearEffectIsEnabled))
					{
						ClearOutputBitmap();
					}

					int baseY = 255;
					int offsetX = 0;
					int offsetY = 0;
					for (int x = 0; x < frequencyPeaks.Length; x++)
					{
						offsetX = (x * barSpacing) + leftOffset;
						offsetY = (baseY - frequencyPeaks[x]);

						_lastPeaks[x] = CalculatePeakDelay(_lastPeaks[x], offsetY, 2, baseY);
						_maximumPeaks[x] = CalculatePeakDelay(_maximumPeaks[x], offsetY, 1, baseY);

						_outputWriteableBitmap.FillRectangle(offsetX, _lastPeaks[x], (offsetX + barWidth), baseY, _primaryColour);
						// Only draw the maximum peak if it is above the empty bar level (cutoff)
						if (_lastPeaks[x] < baseY)
						{
							_outputWriteableBitmap.DrawLine(offsetX, _maximumPeaks[x], (offsetX + lineWidth), _maximumPeaks[x], _secondaryColour);
						}
					}

					if (_clearEffectIsEnabled)
					{
						RunClearEffect();
					}

					FlushOutputBitmap();
				}
				catch
				{ }
			});
		}

		private void RenderFFTScan(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				try
				{
					int height = (int)_outputWriteableBitmap.PixelHeight;
					int width = (int)_outputWriteableBitmap.PixelWidth;
					byte[] frequencyPeaks = PeakMeter.CalculateFrequencies(Sample.Spectrogram, 44100);

					if (_fftScanIsCleared)
					{
						ClearOutputBitmap();
						_xRunningScanCounter = _marginLeft;
						_fftScanIsCleared = false;
					}

					int offsetY = 0;
					int baseOffsetY = 0;
					int maximumPeak = 0;
					int bandTotal = frequencyPeaks.Length;
					int drawingColour = _primaryColour;
					bool isAlternateColour = false;

					// Clear previous scan line
					_outputWriteableBitmap.DrawLine(_xRunningScanCounter, 0, _xRunningScanCounter, height, _clearColour);

					for (int x = 0; x < bandTotal; x++)
					{
						maximumPeak = (frequencyPeaks[x] >> 1);
						baseOffsetY = ((bandTotal - x) * 39);
						_maximumPeaks[x] = ((_maximumPeaks[x] + maximumPeak) >> 1);
						offsetY = (baseOffsetY - _maximumPeaks[x]);

						if (isAlternateColour)
						{
							drawingColour = _alternatePrimaryColour;
						}
						else
						{
							drawingColour = _primaryColour;
						}
						isAlternateColour = !(isAlternateColour);

						if (offsetY == baseOffsetY)
						{
							_outputWriteableBitmap.SetPixel(_xRunningScanCounter, baseOffsetY, drawingColour);
						}
						else
						{
							_outputWriteableBitmap.DrawLine(_xRunningScanCounter, offsetY, _xRunningScanCounter, baseOffsetY, drawingColour);
						}
					}

					// Current Scanline
					_outputWriteableBitmap.DrawLine((_xRunningScanCounter + 1), 0, (_xRunningScanCounter + 1), height, _secondaryColour);

					_xRunningScanCounter++;
					if (_xRunningScanCounter >= _widthWithoutMarginRight)
					{
						_outputWriteableBitmap.DrawLine(_xRunningScanCounter, 0, _xRunningScanCounter, height, _clearColour);
						_xRunningScanCounter = _marginLeft;
					}

					FlushOutputBitmap();
				}
				catch
				{

				}
			});
		}

		private void RenderFFT(Sample Sample)
		{
			Dispatcher.BeginInvoke(() =>
			{
				int y = 0;
				int x = 0;
				const int leftOffset = 10;
				int sampleWidth = Sample.Spectrogram.Length;
				int previousLeftX = leftOffset;
				int previousLeftY = 128;
				//int previousRightX = leftOffset;
				//int previousRightY = 256;

				double Coef = 256.0 / Sample.Spectrogram.Max(X => X.Real);
				double Step = _outputWriteableBitmap.PixelWidth;

				if (!(_clearEffectIsEnabled))
				{
					ClearOutputBitmap();
				}

				for (int sampleIndex = 0; sampleIndex < sampleWidth; sampleIndex++)
				{
					x = sampleIndex + leftOffset;

					y = (int)Math.Round(Sample.Spectrogram[sampleIndex].Real * Coef) + 192;

					_outputWriteableBitmap.DrawLine(previousLeftX, previousLeftY, x, y, _primaryColour);
					previousLeftX = x;
					previousLeftY = y;
				}

				if (_clearEffectIsEnabled)
				{
					RunClearEffect();
				}

				FlushOutputBitmap();
			});
		}

		/// <summary>
		/// 	Calculates the next position for the slowing flowing animation on the
		/// 	peak meter
		/// </summary>
		/// <param name="currentValue"></param>
		/// <param name="comparisonValue"></param>
		/// <param name="speed"></param>
		/// <param name="cutOff"></param>
		/// <returns></returns>
		

		#endregion

		#region Audio PreProcessor Effects

		private void SetVisualType(VisualDescription visualDescription)
		{
			lock (_visualLock)
			{
				// Certain Visuals require more information on the display
			   /* if (visualDescription.VisualType == VisualTypeEnumeration.FFTScan)
				{
					fftScanHUD.Visibility = Alias.Visibility.Visible;
				}
				else
				{
					fftScanHUD.Visibility = Alias.Visibility.Collapsed;
				}

				if (visualDescription.VisualType == VisualTypeEnumeration.PeakMeter)
				{
					peakMeterHUD.Visibility = Alias.Visibility.Visible;
				}
				else
				{
					peakMeterHUD.Visibility = Alias.Visibility.Collapsed;
				}*/
				InitBitmap();

				switch (visualDescription.VisualType)
				{

					case VisualTypeEnumeration.None:
						_currentRenderVisuals = null;
						ClearOutputBitmap();
						FlushOutputBitmap();
						break;

					case VisualTypeEnumeration.PeakMeter:
						  FourierTransform.SetMeterFrequencies(_peakMeterFrequencies);
						  _lastPeaks = InitialiseArray(FourierTransform.FREQUENCYSLOTCOUNT, byte.MaxValue);
						  _maximumPeaks = InitialiseArray(FourierTransform.FREQUENCYSLOTCOUNT, byte.MaxValue);

						  _currentRenderVisuals = new RenderVisualsDelegate(RenderPeakMeter);
						break;

					case VisualTypeEnumeration.Oscilloscope:
						_currentRenderVisuals = RenderOscilloscope;
						break;

					case VisualTypeEnumeration.Scope:
						_currentRenderVisuals = RenderScope;
						break;

					case VisualTypeEnumeration.Scan:
						_scanIsCleared = true;
						_currentRenderVisuals = RenderScan;
						break;

					case VisualTypeEnumeration.FFTScan:
						  FourierTransform.SetMeterFrequencies(_fftScanFrequencies);
						  _maximumPeaks = InitialiseArray(_maximumPeaks.Length, 0);

						  _fftScanIsCleared = true;
						  _currentRenderVisuals = new RenderVisualsDelegate(RenderFFTScan);
						break;

					case VisualTypeEnumeration.Volume:
						_currentRenderVisuals = RenderVolume;
						break;

					case VisualTypeEnumeration.FFT:
						_currentRenderVisuals = RenderFFT;
						break;
				}
			}
		}

		#endregion
		#endregion

		#region public constructors

		public WaveVisualizer()
		{
			InitialiseDescriptions();
			InitializeComponent();
		}

		#endregion

		#region private event handlers

		private void cbBlur_Checked(object sender, RoutedEventArgs e)
		{
		   _clearEffectIsEnabled = true;
		}

		private void cbBlur_Unchecked(object sender, RoutedEventArgs e)
		{
			_clearEffectIsEnabled = false;
		}

		private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Initialise();
		}

		#endregion

		#region public event handlers

		public void OnSample(Sample SampleData)
		{
			Dispatcher.BeginInvoke(() =>
			{
				lock (_visualLock)
				{
					if (_currentRenderVisuals != null)
					{
						_currentRenderVisuals(SampleData);
					}
				}
			});
		}	   

		#endregion

		#region public properties

		public bool Blur
		{
			get
			{
				return _clearEffectIsEnabled;
			}
			set
			{
				_clearEffectIsEnabled = value;
			}
		}

		
		public VisualTypeEnumeration VisualType
		{
			get
			{
				return visualType;
			}
			set
			{
				visualType = value;
				SetVisualType((from VD in VisualDescriptions
							   where VD.VisualType == visualType
							   select VD).Single());
			}
		}

		public ObservableCollection<VisualDescription> VisualDescriptions
		{
			get
			{
				return visualDescriptions;
			}
		}

		#endregion
	}

	public enum VisualTypeEnumeration
	{
		None,
		PeakMeter,
		Oscilloscope,
		Scope,
		Scan,
		FFTScan,
		Volume,
		FFT
	}

	/// <summary>
	/// 	Describes a Visual rendering type
	/// </summary>
	public class VisualDescription
	{
		public string Description
		{
			get;
			set;
		}

		public VisualTypeEnumeration VisualType
		{
			get;
			set;
		}
	}
}
