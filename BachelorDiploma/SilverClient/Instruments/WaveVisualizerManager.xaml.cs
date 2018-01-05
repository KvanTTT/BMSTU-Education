using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using KvantSound;

namespace SilverClient.Instruments
{
	public partial class WaveVisualizerManager : UserControl
	{
		private ObservableCollection<VisualDescription> VisualDescriptions;

		public WaveVisualizerManager()
		{
			VisualDescriptions = new ObservableCollection<VisualDescription>();
			VisualDescriptions.Add(new VisualDescription() { Description = "Отключено", VisualType = VisualTypeEnumeration.None });
			VisualDescriptions.Add(new VisualDescription() { Description = "Измеритель пиков", VisualType = VisualTypeEnumeration.PeakMeter });
			VisualDescriptions.Add(new VisualDescription() { Description = "Осциллограф", VisualType = VisualTypeEnumeration.Oscilloscope });
			VisualDescriptions.Add(new VisualDescription() { Description = "Диапазон", VisualType = VisualTypeEnumeration.Scope });
			VisualDescriptions.Add(new VisualDescription() { Description = "Анализ", VisualType = VisualTypeEnumeration.Scan });
			VisualDescriptions.Add(new VisualDescription() { Description = "Band Scan", VisualType = VisualTypeEnumeration.FFTScan });
			VisualDescriptions.Add(new VisualDescription() { Description = "Volume", VisualType = VisualTypeEnumeration.Volume });
			VisualDescriptions.Add(new VisualDescription() { Description = "FFT", VisualType = VisualTypeEnumeration.FFT });

			InitializeComponent();
		}

		private void cbBlur_Checked(object sender, RoutedEventArgs e)
		{
			waveVisualizer1.Blur = true;
		}

		private void cbBlur_Unchecked(object sender, RoutedEventArgs e)
		{
			waveVisualizer1.Blur = false;
		}

		private void cmbVisualStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cmbVisualStyle != null)
			waveVisualizer1.VisualType = (VisualTypeEnumeration)cmbVisualStyle.SelectedIndex;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
		   // this.DataContext = this;
			cmbVisualStyle.SelectedIndex = 1;
		}

		public void OnSample(Sample SampleData)
		{
			waveVisualizer1.OnSample(SampleData);
		}
	}
}
