using MusicNotationLib;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace KvantSound
{
	enum BitsPerSample
	{
		c8000,
		c11025,
		c22050,
		c32000,
		c44100,
		c48000,
		c96000
	}

	enum WaveFormat
	{
		Pcm
	}

	/// <summary>
	/// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
	/// Provides a method for performing a deep copy of an object.
	/// Binary Serialization is used to perform the copy.
	/// </summary>
	public static class ObjectCopier
	{
		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T Clone<T>(this T source)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (MemoryStream stream = new MemoryStream())
			{
				serializer.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)serializer.Deserialize(stream);
			}
		}
	}

	public class WindowTypeCaption
	{
		public WindowType Type
		{
			get;
			set;
		}

		public string Caption
		{
			get;
			set;
		}

		public WindowTypeCaption(WindowType Type, string Caption)
		{
			this.Type = Type;
			this.Caption = Caption;
		}
	}

	public class MusicSymbolDurationCaption
	{
		public MusicSymbolDuration Duration
		{
			get;
			set;
		}

		public string Caption
		{
			get;
			set;
		}

		public MusicSymbolDurationCaption(MusicSymbolDuration Duration, string Caption)
		{
			this.Duration = Duration;
			this.Caption = Caption;
		}
	}

	public class Settings
	{
		#region Private Fields

		private static ObservableCollection<WindowTypeCaption> windowTypeCaptions;
		private static ObservableCollection<MusicSymbolDurationCaption> musicSymbolDurationCaption;
		
		#endregion

		#region Signal

		public double InThreshold
		{
			get;
			set;
		}

		public double OutThreshold
		{
			get;
			set;
		}

		public double CompressParameter
		{
			get;
			set;
		}

		public int MinDuration
		{
			get;
			set;
		}

		public int WindowSize
		{
			get;
			set;
		}

		public WindowType WindowType
		{
			get;
			set;
		}

		public double OverlappedWindowRatio
		{
			get;
			set;
		}

		public double MinFreq
		{
			get;
			set;
		}

		public double MaxFreq
		{
			get;
			set;
		}

		public int PeaksCount
		{
			get;
			set;
		}

		#endregion

		#region Note

		public int Tempo
		{
			get;
			set;
		}

		public TimeSignature TimeSignature
		{
			get;
			set;
		}

		public MusicSymbolDuration MinSymbolDuration
		{
			get;
			set;
		}

		public MusicSymbolDuration MaxSymbolDuration
		{
			get;
			set;
		}

		public bool DottedNotes
		{
			get;
			set;
		}

		public FundFreqDetectionMode FundFreqDetectionMode
		{
			get;
			set;
		}

		public MusicNotationMode MusicNotationMode
		{
			get;
			set;
		}

		#endregion

		#region ViewModel

		public static ObservableCollection<WindowTypeCaption> WindowTypeCaptions
		{
			get
			{
				return windowTypeCaptions;
			}
		}

		public static ObservableCollection<MusicSymbolDurationCaption> MusicSymbolDurationCaptions
		{
			get
			{
				return musicSymbolDurationCaption;
			}
		}

		#endregion

		#region Contructors

		public Settings()
		{
			windowTypeCaptions = new ObservableCollection<WindowTypeCaption>()
			{
				new WindowTypeCaption(WindowType.Rectangle, "Прямоугольное"),
				new WindowTypeCaption(WindowType.Sin, "Синусоидальное"),
				new WindowTypeCaption(WindowType.Lanczos, "Lanczos"),
				new WindowTypeCaption(WindowType.Bartlett, "Барлета"),
				new WindowTypeCaption(WindowType.Hann, "Хана"),
				new WindowTypeCaption(WindowType.Hamming, "Хэмминга"),
				new WindowTypeCaption(WindowType.Blackman, "Блэкмана")
			};

			musicSymbolDurationCaption = new ObservableCollection<MusicSymbolDurationCaption>()
			{
				new MusicSymbolDurationCaption(MusicSymbolDuration.Whole, "Целая"),
				new MusicSymbolDurationCaption(MusicSymbolDuration.Half, "Половина"),
				new MusicSymbolDurationCaption(MusicSymbolDuration.Quarter, "Четверть"),
				new MusicSymbolDurationCaption(MusicSymbolDuration.Eighth, "Восьмая"),
				new MusicSymbolDurationCaption(MusicSymbolDuration.Sixteenth, "Шестнадцатая"),
				new MusicSymbolDurationCaption(MusicSymbolDuration.ThirtySecond, "Тридцатьвторая"),
			};

			WindowSize = 2048;
			OverlappedWindowRatio = 2;

			MinFreq = 100;
			MaxFreq = 2100;

			CompressParameter = 2.0;
			InThreshold = 0.015;
			OutThreshold = 0.010;

			WindowType = KvantSound.WindowType.Hamming;

			PeaksCount = 5;

			Tempo = 120;
			TimeSignature = new TimeSignature(4, 4);
			MinSymbolDuration = MusicSymbolDuration.Sixteenth;
			MaxSymbolDuration = MusicSymbolDuration.Whole;
			DottedNotes = true;
		}

		#endregion
	}
}
