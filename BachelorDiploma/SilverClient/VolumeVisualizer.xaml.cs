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
	public partial class VolumeVisualizer : UserControl
	{
		private bool inverted;

		public bool Inverted
		{
			get
			{
				return Inverted;
			}
			set
			{
				inverted = value;
				if (inverted)
				{
					brdrCover.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
					(LayoutRoot.Background as LinearGradientBrush).StartPoint = new Point(0, 1);
					(LayoutRoot.Background as LinearGradientBrush).EndPoint = new Point(0, 0);
				}
				else
				{
					brdrCover.VerticalAlignment = System.Windows.VerticalAlignment.Top;
					(LayoutRoot.Background as LinearGradientBrush).StartPoint = new Point(0, 0);
					(LayoutRoot.Background as LinearGradientBrush).EndPoint = new Point(0, 1);
				}
			}
		}

		public VolumeVisualizer()
		{
			InitializeComponent();
		}


		public void SetVolume(double Volume)
		{
			brdrCover.Height = ucVolumeVisualizer.ActualHeight * Volume;
		}

		private void ucVolumeVisualizer_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
