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

namespace SilverClient
{
	public partial class MidiBoard : UserControl
	{
		int OctaveCount;
		int MinID, MaxID;
		Border[] Tracks;

		SolidColorBrush TrackBrush = new SolidColorBrush(Colors.Blue);
		SolidColorBrush BorderBrush = new SolidColorBrush(Colors.Gray);

		public MidiBoard(int OctaveCount = 4)
		{
			this.OctaveCount = OctaveCount;
			MinID = 60 - (OctaveCount - 1) / 2 * 12;
			MaxID = 72 + OctaveCount / 2 * 12 - 1;

			Tracks = new Border[MaxID - MinID + 1];

			for (int i = 0; i < Tracks.Length; i++)
			{
				Tracks[i] = new Border();
				Tracks[i].Tag = MinID + i;
				Tracks[i].Background = TrackBrush;
				Tracks[i].BorderBrush = BorderBrush;

				canvasMain.Children.Add(Tracks[i]);
			}
		}

		public MidiBoard()
		{
			InitializeComponent();
		}

		private void canvasMain_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			int WhiteCount = OctaveCount * 7;
			int BlackCount = OctaveCount * 5;
			double WhiteWidth = canvasMain.ActualWidth / WhiteCount;
			double BlackWidth = WhiteWidth / 2.2;
			WhiteWidth = WhiteWidth - BlackWidth / 2;
			
			for (int i = 0; i < Tracks.Length; i++)
			{

			}
		}

		
	}
}
