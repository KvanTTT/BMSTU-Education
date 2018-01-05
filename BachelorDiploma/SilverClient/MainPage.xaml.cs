using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.IO.IsolatedStorage;
using SilverClient.Audio;
using KvantSound;
using System.Windows.Threading;
using System.Xml.Serialization;
using MusicNotationLib;

namespace SilverClient
{
	public partial class MainPage : UserControl
	{
		AudioRecorder Recorder;
		SoundProcessor Processor;
		DispatcherTimer NoteUpdater;
		bool Stop = false;
		Settings Settings;
		SaveFileDialog dialog;
		

		public MainPage()
		{
			InitializeComponent();
		}

		void  NoteUpdater_Tick(object sender, EventArgs e)
		{
			Dispatcher.BeginInvoke(() =>
				{
					instrumentBoard1.ReleaseAll();

					NoteViewer.Update();

					if (Processor.MusicNotation.Samples.Count == 0)
						return;

					Sample Note = Processor.MusicNotation.
						Samples[Processor.MusicNotation.Samples.Count - 1];

					if (!(Note == null || Note.ID == int.MinValue || Note.ID == int.MaxValue))
						instrumentBoard1.Press(Note.ID);
				});
		}
		 
		void OnSamples(object sender, SampleEventArgs e)
		{
			var Samples = Processor.MusicNotation.Samples;
			if (Samples.Count != 0)
				waveVisualizerManager1.OnSample(Samples[Samples.Count - 1]);
		}

		private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
		{
			Settings = SettingsHelper.Load();
			if (Settings.MusicNotationMode == MusicNotationMode.Create)
				rbRecognition.IsChecked = true;
			else
				rbFolow.IsChecked = true;

			//Settings = new Settings();
			Processor = new SoundProcessor(NoteViewer.UpdateMusicalSymbol, Settings);

			NoteUpdater = new DispatcherTimer();
			NoteUpdater.Tick += NoteUpdater_Tick;
			NoteUpdater.Interval = new TimeSpan(0, 0, 0, 0, 20);

			Recorder = new AudioRecorder(80);
			Recorder.Samples += OnSamples;
			Recorder.Samples += Processor.OnSamples;
		}

		private void waveVisualizerManager1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			//if (e.ClickCount == 2)
			//	Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
		}

		private void btnRecord_Click(object sender, RoutedEventArgs e)
		{
			if (!NoteUpdater.IsEnabled)
			{
				if (NoteViewer.Mode == MusicNotationMode.Follow)
					Processor.MusicalSymbols = NoteViewer.incipit;

				//Processor.Settings = Settings;
				//Processor.UpdateSettings();
				
				Recorder.Start();

				NoteUpdater.Start();

				btnRecord.Content = "Остановить";
			}
			else
			{
				NoteUpdater.Stop();

				Recorder.Stop();

				btnRecord.Content = "Начать";
			}
		}

		private void btnSettings_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow window = new SettingsWindow(Settings, () => Processor.UpdateSettings());
			window.Show();
		}

		private void rbRecognition_Checked(object sender, RoutedEventArgs e)
		{
			if (rbRecognition != null)
				if ((bool)rbRecognition.IsChecked)
				{
					btnSaveLoad.Content = "Сохранить";
					NoteViewer.Mode = MusicNotationMode.Create;
					Settings.MusicNotationMode = MusicNotationMode.Create;
				}
				else
				{
					btnSaveLoad.Content = "Загрузить";
					NoteViewer.Mode = MusicNotationMode.Follow;
					Settings.MusicNotationMode = MusicNotationMode.Follow;
				}
		}

		private void btnSaveLoad_Click(object sender, RoutedEventArgs e)
		{
			if (rbRecognition != null)
				if (NoteUpdater.IsEnabled)
				{
					var Dialog = new ModalDialog();
					Dialog.tbMessage.Text = "Остановите запись.";
					Dialog.Title = " ";
					Dialog.Show();
				}
				else
					if (NoteViewer.Mode == MusicNotationMode.Create)
					{
						dialog = new SaveFileDialog();
						dialog.DefaultExt = ".mid";
						dialog.Filter = "Midi Files|*.mid";

						bool? dialogResult = dialog.ShowDialog();

						if ((bool)dialogResult)
						{
							var buffer = NoteViewer.SaveToMidi();
							//var buffer = (Processor.MusicNotation as MusicNotationCreator).SaveToMidi();
							var File = dialog.OpenFile();
							File.Write(buffer, 0, buffer.Length);
							File.Close();
						}
					}
					else
					{
						OpenFileDialog OpenFileDialog = new OpenFileDialog();
						if (OpenFileDialog.ShowDialog() == true)
							NoteViewer.LoadFromXmlStream(OpenFileDialog.File.OpenRead());
					}
		}

		private void btnActivateMicrophone_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Media.CaptureDeviceConfiguration.RequestDeviceAccess();
		}
	}
}
