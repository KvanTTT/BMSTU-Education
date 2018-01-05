using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Data;

namespace SilverClient
{
	public partial class FluteSoundBoard : UserControl, IMusicInstrument
	{
		public readonly bool[][] Fingering;
		public readonly FluteKeys FluteKeys;
		public readonly int minID;
		public readonly int maxID;

		void SetupNote(bool[][] Fingering, int NoteID, List<FluteKey> FluteKeysOn)
		{
			foreach (FluteKey Key in FluteKeysOn)
				Fingering[NoteID][(int)Key] = true;
		}

		public FluteSoundBoard()
		{
			minID = 60;
			maxID = minID + 37;

			Fingering = new bool[37][];
			for (int i = 0; i < Fingering.Length; i++)
				Fingering[i] = new bool[15];

			int CurNoteId = -1;
			
			#region First Octave
			// Note C
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey, FluteKey.LowCKey
				});

			// Note C#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey, FluteKey.CSharpKey
				});

			// Note D
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey
				});

			// Note D#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey, FluteKey.EbKey
				});

			// Note E
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.EbKey
				});

			// Note F
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{

					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EbKey
				});

			// Note F#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.DKey, FluteKey.EbKey
				});

			// Note G
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note G#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey, FluteKey.GSharpKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note A
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note A#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BbLeverKey, FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note B
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});
			#endregion

			#region Second Octave
			// Note C
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note C#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					// Right Hand
					FluteKey.EbKey
				});

			// Note D
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey
				});

			// Note D#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey, FluteKey.EbKey
				});

			// Note E
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.FKey, FluteKey.EKey, FluteKey.EbKey
				});

			// Note F
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.AKey, FluteKey.CKey, FluteKey.GKey,
					// Right Hand
					FluteKey.FKey, FluteKey.EbKey
				});

			// Note F#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.DKey, FluteKey.EbKey
				});

			// Note G
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note G#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey, FluteKey.GSharpKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note A
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note A#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BbLeverKey, FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note B
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});
			#endregion

			#region Third Octave
			// Note C
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.CKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note C#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					// Right Hand
					FluteKey.EbKey
				});

			// Note D
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.AKey, FluteKey.GKey,
					FluteKey.EbKey
				});

			// Note D#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey, FluteKey.GKey, FluteKey.GSharpKey,
					FluteKey.FKey, FluteKey.EKey, FluteKey.DKey, FluteKey.EbKey
				});

			// Note E
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.AKey,
					FluteKey.FKey, FluteKey.EKey
				});

			// Note F
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{

					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.GKey,
					FluteKey.FKey, FluteKey.EbKey
				});

			// Note F#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.GKey,
					FluteKey.DKey, FluteKey.EbKey
				});

			// Note G
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.CKey, FluteKey.AKey, FluteKey.GKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note G#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.AKey, FluteKey.GKey, FluteKey.GSharpKey,
					// Right Hand
					FluteKey.EbKey
				});

			// Note A
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.AKey,
					// Right Hand
					FluteKey.FKey, FluteKey.EbKey
				});

			// Note A#
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey,
					// Right Hand
					FluteKey.FKey, FluteKey.CSharpTrillKey
				});

			// Note B
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.BLeverKey, FluteKey.CKey, FluteKey.GKey,
					// Right Hand
					FluteKey.DSharpTrillKey
				});

			// Note C
			SetupNote(Fingering, ++CurNoteId, new List<FluteKey>()
				{
					// Left Hand
					FluteKey.CKey, FluteKey.AKey, FluteKey.GKey, FluteKey.GSharpKey,
					// Right Hand
					FluteKey.FKey
				});
			#endregion

			InitializeComponent();

			FluteKeys = new FluteKeys();
			this.DataContext = FluteKeys;
		}


		public void Press(int ID, double Volume = 1)
		{
			if (ID >= minID && ID <= maxID)
				FluteKeys.SetPressed(Fingering[ID - MinID]);
		}

		public void Release(int ID)
		{
			ReleaseAll();
		}

		public void PressAll()
		{
			FluteKeys.SetPressed();
		}

		public void ReleaseAll()
		{
			FluteKeys.SetUnpressed();
		}


		public bool Interactive
		{
			get
			{
				return false;
			}
			set
			{

			}
		}

		public bool Mono
		{
			get
			{
				return true;
			}
			set
			{
				//throw new Exception("Flute support only Mono mode");
			}
		}


		public int MinID
		{
			get
			{
				return minID;
			}
			set
			{
			}
		}

		public int MaxID
		{
			get
			{
				return maxID;
			}
			set
			{
			}
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			FluteKeys.SetUnpressed();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			FluteKeys.SetPressed();
		}
	}

	public enum FluteKey
	{
		BLeverKey = 0,
		BbKey = 1,
		CKey = 2,
		AKey = 3,
		GKey = 4,
		GSharpKey = 5,

		BbLeverKey = 6,
		FKey = 7,
		CSharpTrillKey = 8,
		EKey = 9,
		DSharpTrillKey = 10,
		DKey = 11,
		EbKey = 12,
		CSharpKey = 13,
		LowCKey = 14
	}

	public class FluteKeys : INotifyPropertyChanged
	{
		Visibility ToVisibility(bool Vis)
		{
			return Vis ? Visibility.Visible : Visibility.Collapsed;
		}

		bool ToBool(Visibility Vis)
		{
			return Vis == Visibility.Visible ? true : false;
		}

		private Visibility _BLeverKey;
		public Visibility BLeverKey
		{
			get
			{
				return _BLeverKey;
			}
			set
			{
				_BLeverKey = value;
				NotifyPropertyChanged("BLeverKey");
			}
		}

		private Visibility _BbKey;
		public Visibility BbKey
		{
			get
			{
				return _BbKey;
			}
			set
			{
				_BbKey = value;
				if (value == Visibility.Visible)
					BLeverKey = Visibility.Visible;
				NotifyPropertyChanged("BbKey");
			}
		}

		private Visibility _CKey;
		public Visibility CKey
		{
			get
			{
				return _CKey;
			}
			set
			{
				_CKey = value;
				NotifyPropertyChanged("CKey");
			}
		}

		private Visibility _AKey;
		public Visibility AKey
		{
			get
			{
				return _AKey;
			}
			set
			{
				_AKey = value;
				NotifyPropertyChanged("AKey");
			}
		}

		private Visibility _GKey;
		public Visibility GKey
		{
			get
			{
				return _GKey;
			}
			set
			{
				_GKey = value;
				NotifyPropertyChanged("GKey");
			}
		}

		private Visibility _GSharpKey;
		public Visibility GSharpKey
		{
			get
			{
				return _GSharpKey;
			}
			set
			{
				_GSharpKey = value;
				NotifyPropertyChanged("GSharpKey");
			}
		}

		private Visibility _BbLeverKey;
		public Visibility BbLeverKey
		{
			get
			{
				return _BbLeverKey;
			}
			set
			{
				_BbLeverKey = value;
				NotifyPropertyChanged("BbLeverKey");
			}
		}

		private Visibility _FKey;
		public Visibility FKey
		{
			get
			{
				return _FKey;
			}
			set
			{
				_FKey = value;
				NotifyPropertyChanged("FKey");
			}
		}

		private Visibility _CSharpTrillKey;
		public Visibility CSharpTrillKey
		{
			get
			{
				return _CSharpTrillKey;
			}
			set
			{
				_CSharpTrillKey = value;
				NotifyPropertyChanged("CSharpTrillKey");
			}
		}

		private Visibility _EKey;
		public Visibility EKey
		{
			get
			{
				return _EKey;
			}
			set
			{
				_EKey = value;
				NotifyPropertyChanged("EKey");
			}
		}

		private Visibility _DSharpTrillKey;
		public Visibility DSharpTrillKey
		{
			get
			{
				return _DSharpTrillKey;
			}
			set
			{
				_DSharpTrillKey = value;
				NotifyPropertyChanged("DSharpTrillKey");
			}
		}

		private Visibility _DKey;
		public Visibility DKey
		{
			get
			{
				return _DKey;
			}
			set
			{
				_DKey = value;
				NotifyPropertyChanged("DKey");
			}
		}

		private Visibility _EbKey;
		public Visibility EbKey
		{
			get
			{
				return _EbKey;
			}
			set
			{
				_EbKey = value;
				NotifyPropertyChanged("EbKey");
			}
		}

		private Visibility _CSharpKey;
		public Visibility CSharpKey
		{
			get
			{
				return _CSharpKey;
			}
			set
			{
				_CSharpKey = value;
				NotifyPropertyChanged("CSharpKey");
			}
		}

		private Visibility _LowCKey;
		public Visibility LowCKey
		{
			get
			{
				return _LowCKey;
			}
			set
			{
				_LowCKey = value;
				if (value == Visibility.Visible)
					CSharpKey = Visibility.Visible;
				NotifyPropertyChanged("LowCKey");
			}
		}

		public void SetPressed(bool[] Keys)
		{
			BLeverKey = ToVisibility(Keys[(int)FluteKey.BLeverKey]);
			BbKey = ToVisibility(Keys[(int)FluteKey.BbKey]);
			CKey = ToVisibility(Keys[(int)FluteKey.CKey]);
			AKey = ToVisibility(Keys[(int)FluteKey.AKey]);
			GKey = ToVisibility(Keys[(int)FluteKey.GKey]);
			GSharpKey = ToVisibility(Keys[(int)FluteKey.GSharpKey]);

			BbLeverKey = ToVisibility(Keys[(int)FluteKey.BbLeverKey]);
			FKey = ToVisibility(Keys[(int)FluteKey.FKey]);
			CSharpTrillKey = ToVisibility(Keys[(int)FluteKey.CSharpTrillKey]);
			EKey = ToVisibility(Keys[(int)FluteKey.EKey]);
			DSharpTrillKey = ToVisibility(Keys[(int)FluteKey.DSharpTrillKey]);
			DKey = ToVisibility(Keys[(int)FluteKey.DKey]);
			EbKey = ToVisibility(Keys[(int)FluteKey.EbKey]);
			CSharpKey = ToVisibility(Keys[(int)FluteKey.CSharpKey]);
			LowCKey = ToVisibility(Keys[(int)FluteKey.LowCKey]);
		}

		public void SetUnpressed()
		{
			BLeverKey = Visibility.Collapsed;
			BbKey = Visibility.Collapsed;
			CKey = Visibility.Collapsed;
			AKey = Visibility.Collapsed;
			GKey = Visibility.Collapsed;
			GSharpKey = Visibility.Collapsed;

			BbLeverKey = Visibility.Collapsed;
			FKey = Visibility.Collapsed;
			CSharpTrillKey = Visibility.Collapsed;
			EKey = Visibility.Collapsed;
			DSharpTrillKey = Visibility.Collapsed;
			DKey = Visibility.Collapsed;
			EbKey = Visibility.Collapsed;
			CSharpKey = Visibility.Collapsed;
			LowCKey = Visibility.Collapsed;
		}

		public void SetPressed()
		{
			BLeverKey = Visibility.Visible;
			BbKey = Visibility.Visible;
			CKey = Visibility.Visible;
			AKey = Visibility.Visible;
			GKey = Visibility.Visible;
			GSharpKey = Visibility.Visible;

			BbLeverKey = Visibility.Visible;
			FKey = Visibility.Visible;
			CSharpTrillKey = Visibility.Visible;
			EKey = Visibility.Visible;
			DSharpTrillKey = Visibility.Visible;
			DKey = Visibility.Visible;
			EbKey = Visibility.Visible;
			CSharpKey = Visibility.Visible;
			LowCKey = Visibility.Visible;
		}


		// Declare the PropertyChanged event
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public FluteKeys()
		{
			BLeverKey = Visibility.Collapsed;
			BbKey = Visibility.Collapsed;
			CKey = Visibility.Collapsed;
			AKey = Visibility.Collapsed;
			GKey = Visibility.Collapsed;
			GSharpKey = Visibility.Collapsed;

			BbLeverKey = Visibility.Collapsed;
			FKey = Visibility.Collapsed;
			CSharpTrillKey = Visibility.Collapsed;
			EKey = Visibility.Collapsed;
			DSharpTrillKey = Visibility.Collapsed;
			DKey = Visibility.Collapsed;
			EbKey = Visibility.Collapsed;
			CSharpKey = Visibility.Collapsed;
			LowCKey = Visibility.Collapsed;
		}
	}
}
