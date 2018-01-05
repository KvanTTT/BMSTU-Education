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
	public partial class InstrumentBoard : UserControl
	{
		IMusicInstrument SelectedInstrument;

		public InstrumentBoard()
		{
			InitializeComponent();
		}

		private void cmbSelectedInstrument_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBoxItem SelItem = e.AddedItems[0] as ComboBoxItem;
			if (SelItem == null)
				return;
			if (LayoutRoot != null)
				LayoutRoot.Children.Remove(SelectedInstrument as UserControl);
			switch (SelItem.Name)
			{
				default:
				case "cmbItemPiano":
					SelectedInstrument = new PianoKeyboard();
					break;
				case "cmbItemFlute":
					SelectedInstrument = new FluteSoundBoard();
					break;
				case "cmbItemGuitar":
					SelectedInstrument = new GuitarSoundBoard();
					break;
				case "cmbItemBass":
					SelectedInstrument = new BassSoundBoard();
					break;
			}
			Grid.SetRow(SelectedInstrument as UserControl, 1);
			if (LayoutRoot != null)
				LayoutRoot.Children.Add(SelectedInstrument as UserControl);
		}

		public void Press(int ID, double Volume = 1)
		{
			SelectedInstrument.Press(ID, Volume);
		}

		public void Release(int ID)
		{
			SelectedInstrument.Release(ID);
		}

		public void PressAll()
		{
			SelectedInstrument.PressAll();
		}

		public void ReleaseAll()
		{
			SelectedInstrument.ReleaseAll();
		}

		private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
		{
			LayoutRoot.Children.Add(SelectedInstrument as UserControl);
		}
	}
}
