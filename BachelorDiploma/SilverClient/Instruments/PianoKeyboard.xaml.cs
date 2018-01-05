using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace SilverClient
{
	public class PianoKey
	{
		public Border Visual;
		public bool White;		
		public Brush Brush;
		public Brush DownBrush;
		public Brush SelectBrush;
		public Brush DownSelectBrush;

		bool isSelected;
		public bool IsSelected
		{
			get
			{
				return isSelected;
			}
			set
			{
				isSelected = value;
				if (isSelected)
					Visual.Background = isDown ? DownSelectBrush : SelectBrush; 
				else
					Visual.Background = isDown ? DownBrush : Brush;			
			}
		}

		bool isDown;
		public bool IsDown
		{
			get
			{
				return isDown;
			}
			set
			{
				isDown = value;
				if (isDown)
					Visual.Background = isSelected ? DownSelectBrush : DownBrush;
				else
					Visual.Background = isSelected ? SelectBrush : Brush; 
			}
		}

		public void Press()
		{
			Visual.Background = DownBrush;
		}

		public void Release()
		{
			Visual.Background = Brush;
		}
	}

	public partial class PianoKeyboard : UserControl, IMusicInstrument
	{
		PianoKey[] Keys;
		List<int> PressedKeys;

		public class KeyboardKey
		{
			int ID;
			bool White;
		}		

		private bool interactive;

		int OctaveCount;

		int[] BlackKeyID = new int[] {1, 3, 6, 8, 10};
		bool IsMouseDown;

		SolidColorBrush WhiteBrush = new SolidColorBrush(Colors.White);
		SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
		SolidColorBrush GrayBrush = new SolidColorBrush(Colors.Gray);
		SolidColorBrush WhiteSelectBrush = new SolidColorBrush(Colors.LightGray);
		SolidColorBrush BlackSelectBrush = new SolidColorBrush(Colors.DarkGray);

		LinearGradientBrush BlackDownBrush;
		LinearGradientBrush WhiteDownBrush;

		void PrepareBurshes()
		{
			WhiteDownBrush = new LinearGradientBrush(
						new GradientStopCollection { 
							new GradientStop { Color = Colors.White, Offset = 0 }, 
							new GradientStop { Color = Colors.Black, Offset = 1 } },
						90);

			BlackDownBrush = new LinearGradientBrush(
						new GradientStopCollection { 
							new GradientStop { Color = Color.FromArgb(255, 220, 220, 220), Offset = 0 }, 
							new GradientStop { Color = Colors.Black, Offset = 1 } },
						90);
		}

	 /*   public PianoKeyboard(int MinID, int MaxID)
		{
		}*/
		public PianoKeyboard() : this(4)
		{

		}

		public PianoKeyboard(int OctaveCount = 4, bool Interactive = false)
		{
			PrepareBurshes();
			InitializeComponent();

			this.OctaveCount = OctaveCount;
			MinID = 60 - (OctaveCount - 1) / 2 * 12;
			MaxID = 72 +  OctaveCount / 2 * 12 - 1;

			Keys = new PianoKey[MaxID - MinID + 1];
			PressedKeys = new List<int>(Keys.Length);

			for (int i = 0; i < Keys.Length; i++)
			{
				Keys[i] = new PianoKey()
				{
					Visual = new Border()
					{
						Tag = i,
					}
				};

				if (BlackKeyID.Contains((MinID + i) % 12))
				{
					Keys[i].White = false;
					Keys[i].Visual.CornerRadius = new CornerRadius(0, 0, 3, 3);

					Keys[i].Visual.Background = BlackBrush;

					Canvas.SetZIndex(Keys[i].Visual, 1);

					Keys[i].Brush = BlackBrush;
					Keys[i].DownBrush = BlackDownBrush;
					Keys[i].SelectBrush = BlackSelectBrush;
					Keys[i].DownSelectBrush = BlackDownBrush;
				}
				else
				{
					Keys[i].White = true;
					Keys[i].Visual.CornerRadius = new CornerRadius(0, 0, 5, 5);

					Keys[i].Visual.Background = WhiteBrush;
					Keys[i].Visual.BorderBrush = GrayBrush;
					Keys[i].Visual.BorderThickness = new Thickness(1);

					Keys[i].Brush = WhiteBrush;
					Keys[i].DownBrush = WhiteDownBrush;
					Keys[i].SelectBrush = WhiteSelectBrush;
					Keys[i].DownSelectBrush = WhiteDownBrush;
				}			  

				canvas1.Children.Add(Keys[i].Visual);
			}

			this.Interactive = Interactive;			
		}		

		public PianoKeyboard(int MinID, int MaxID)
		{
			InitializeComponent();

			this.MinID = MinID;
			this.MaxID = MaxID;
		}

		void canvas1_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			int WhiteCount = OctaveCount * 7;
			int BlackCount = OctaveCount * 5;
			double WhiteWidth = canvas1.ActualWidth / WhiteCount;
			double BlackWidth = WhiteWidth / 2.2;
			double WhiteHeight = canvas1.ActualHeight;
			double BlackHeight = WhiteHeight * 2.0 / 3.0;

			double X = 0;
			for (int ID = MinID; ID <= MaxID; ID++)
			{
				int i = ID - MinID;
				
				if (!Keys[i].White)
				{
					Canvas.SetLeft(Keys[i].Visual, X - BlackWidth / 2);
					Keys[i].Visual.Width = BlackWidth;
					Keys[i].Visual.Height = BlackHeight;
				}
				else
				{
					Canvas.SetLeft(Keys[i].Visual, X);
					Keys[i].Visual.Width = WhiteWidth;
					Keys[i].Visual.Height = WhiteHeight;
					X += WhiteWidth;
				}
			}
		}

		void canvas1_MouseLeave(object sender, MouseEventArgs e)
		{			
			IsMouseDown = false;
		}

		void PianoKeyboard_KeyMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsMouseDown = true;
			Border Key = (Border)sender;
			Press(MinID + (int)Key.Tag);
		}

		void PianoKeyboard_KeyMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			IsMouseDown = false;
			Border Key = (Border)sender;
			Release(MinID + (int)Key.Tag);
		}

		void PianoKeyboard_KeyMouseEnter(object sender, MouseEventArgs e)
		{
			Border Key = (Border)sender;
			Keys[(int)Key.Tag].IsSelected = true;
		}

		void PianoKeyboard_KeyMouseLeave(object sender, MouseEventArgs e)
		{
			Border Key = (Border)sender;
			Keys[(int)Key.Tag].IsSelected = false;
		}

		public void Press(int ID, double Volume = 1)
		{
			if (ID >= MinID && ID <= MaxID)
			{				
				Keys[ID - MinID].IsDown = true;
				PressedKeys.Add(ID - MinID);
			}
		}

		public void Release(int ID)
		{
			if (ID >= MinID && ID <= MaxID)
			{				
				Keys[ID - MinID].IsDown = false;
				PressedKeys.Remove(ID - MinID);
			}
		}

		public void PressAll()
		{
			foreach (int Key in PressedKeys)
			{
				Keys[Key].IsDown = true;
				PressedKeys.Add(Key);
			}			
		}

		public void ReleaseAll()
		{
			foreach (int Key in PressedKeys)
				Keys[Key].IsDown = false;
			PressedKeys.Clear();
		}


		public bool Interactive
		{
			get
			{
				return interactive;
			}
			set
			{
				interactive = value;
				if (interactive)
				{
					for (int i = 0; i < Keys.Length; i++)
					{
						Keys[i].Visual.MouseLeftButtonDown += PianoKeyboard_KeyMouseLeftButtonDown;
						Keys[i].Visual.MouseLeftButtonUp += PianoKeyboard_KeyMouseLeftButtonUp;
						Keys[i].Visual.MouseEnter += PianoKeyboard_KeyMouseEnter;
						Keys[i].Visual.MouseLeave += PianoKeyboard_KeyMouseLeave;
						canvas1.MouseLeave += canvas1_MouseLeave;
					}
				}
				else
				{
					for (int i = 0; i < Keys.Length; i++)
					{
						Keys[i].Visual.MouseLeftButtonDown -= PianoKeyboard_KeyMouseLeftButtonDown;
						Keys[i].Visual.MouseLeftButtonUp -= PianoKeyboard_KeyMouseLeftButtonUp;
						Keys[i].Visual.MouseEnter -= PianoKeyboard_KeyMouseEnter;
						Keys[i].Visual.MouseLeave -= PianoKeyboard_KeyMouseLeave;
						canvas1.MouseLeave -= canvas1_MouseLeave;
					}
				}
			}
		}

		public bool Mono
		{
			get;
			set;
		}


		public int MinID
		{
			get;
			set;
		}

		public int MaxID
		{
			get;
			set;
		}
	}
}
