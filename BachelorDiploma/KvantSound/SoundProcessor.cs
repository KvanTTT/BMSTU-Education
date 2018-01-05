using MusicNotationLib;
using System.Collections.Generic;

namespace KvantSound
{
	public class SoundProcessor
	{
		#region Private Fields

		private Sample Sample;
		private Settings settings;
		private bool settingsChanged;

		#endregion

		#region Properties

		public Preprocessor Preprocessor;
		public Splitter Splitter;
		public Equalizer Equalizer;
		public Normalizer Normalizer;
		public Spectrometer Spectrometer;
		public FundFreqDetector FundFreqDetector;
		public PitchDetector PitchDetector;
		public MusicNotation MusicNotation;

		public UpdateMuscialSymbolDelegate UpdateMuscialSymbolDelegate;
		public List<MusicalSymbol> MusicalSymbols;

		public Settings Settings
		{
			get
			{
				return settings;
			}
			set
			{
				if (settings == null || !settings.Equals(value))
				{
					settings = value;
					settingsChanged = true;
				}
			}
		}

		#region Private && Protected methods

		protected void ApplySettings()
		{
			Splitter.WindowSize = settings.WindowSize;
			Splitter.OverlappedWindowRatio = settings.OverlappedWindowRatio;

			Equalizer.MinFreq = settings.MinFreq;
			Equalizer.MaxFreq = settings.MaxFreq;

			Normalizer.CompressParameter = settings.CompressParameter;
			Normalizer.InThreshold = settings.InThreshold;
			Normalizer.OutThreshold = settings.OutThreshold;

			Spectrometer.WindowType = settings.WindowType;

			switch (settings.FundFreqDetectionMode)
			{
				default:
				case FundFreqDetectionMode.MaximumLikehood:
					FundFreqDetector = new MaximumLikehoodDetector(settings.PeaksCount);
					break;
				case FundFreqDetectionMode.Autocorrelation:
					FundFreqDetector = new AutocorrelationDetector();
					break;
			}

			switch (settings.MusicNotationMode)
			{
				default:
				case MusicNotationMode.Create:
					MusicNotation = new MusicNotationCreator(UpdateMuscialSymbolDelegate,
						settings.TimeSignature,
						settings.Tempo,
						settings.MinSymbolDuration,
						settings.MaxSymbolDuration,
						settings.DottedNotes);
					break;
				case MusicNotationMode.Follow:
					MusicNotation = new MusicNotationFollower(UpdateMuscialSymbolDelegate,
						MusicalSymbols);
					break;
			}

			settingsChanged = false;
		}

		#endregion

		#region Public methods

		public void UpdateSettings()
		{
			settingsChanged = true;
		}

		#endregion

		#endregion

		#region Event Handlers

		public void OnSamples(object sender, SampleEventArgs e)
		{
			Sample = e.Sample;
			Preprocessor.Process(Sample);
			Splitter.Process(Sample);
		}

		public void OnSplittedSamples(Sample SplittedSample)
		{
			if (settingsChanged)
				ApplySettings();

			Equalizer.Process(Sample);
			Normalizer.Process(Sample);
			Spectrometer.Process(Sample);
			FundFreqDetector.Process(Sample);
			PitchDetector.Process(Sample);
			MusicNotation.Process(Sample);
		}


		public SoundProcessor(UpdateMuscialSymbolDelegate UpdateMuscialSymbolDelegate, Settings settings)
		{
			Preprocessor = new Preprocessor();
			Splitter = new KvantSound.Splitter(OnSplittedSamples);
			Equalizer = new KvantSound.Equalizer();
			Normalizer = new KvantSound.Normalizer();
			Spectrometer = new KvantSound.Spectrometer();
			PitchDetector = new PitchDetector();
			this.UpdateMuscialSymbolDelegate = UpdateMuscialSymbolDelegate;

			this.settings = settings ?? new Settings();
			ApplySettings();
		}

		#endregion
	}
}
