using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using KvantSound;

namespace SilverClient
{
	public delegate void OnSettingsUpdate();

	public partial class SettingsWindow : ChildWindow
	{
		Settings Settings;
		Settings OldSettings;
		OnSettingsUpdate OnSettingsUpdate;

		public SettingsWindow(Settings settings, OnSettingsUpdate onSettingsUpdate)
		{
			InitializeComponent();

			OldSettings = settings.Clone();
			Settings = settings;
			OnSettingsUpdate = onSettingsUpdate;
			settingsControl.DataContext = Settings;
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsHelper.Save(Settings);
			DialogResult = true;
			OnSettingsUpdate();
			//Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			//Close();
		}

		private void ChildWindow_Closed(object sender, EventArgs e)
		{
			if (DialogResult == false)
				Settings = OldSettings;
		}
	}
}

