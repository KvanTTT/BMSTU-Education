using System;
using System.Windows.Data;

// Custom Formatters for SaluseSlider
namespace Saluse.LiquidPlayer.Controls.Formatters
{
	public class MillisecondsToTimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string formatted = string.Empty;
			if (value != null)
			{
				if (value is double)
				{
					{
						TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)value);
						formatted = string.Format(
							"{0:00}:{1:00}",
							timeSpan.Minutes,
							timeSpan.Seconds);
					}
				}
			}

			return formatted;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class PercentageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return string.Format("{0:0}%", value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
