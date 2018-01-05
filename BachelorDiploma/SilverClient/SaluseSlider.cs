using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;
using Saluse.LiquidPlayer.Controls;
using Saluse.MediaKit.Sample;
using Utilities = Saluse.MediaKit.Sample.Utilities;

namespace Saluse.LiquidPlayer.Controls
{
	/// <summary>
	/// 	A quick slider control I threw together better suited for seeking in media
	/// </summary>
	public class SaluseSlider : Slider
	{
		#region public events

		public event EventHandler Changed;
		public event EventHandler SeekingStart;
		public event EventHandler SeekingEnd;

		#endregion

		#region public static dependency properties

		public static DependencyProperty ThumbBrushProperty =
			DependencyProperty.Register(
				"ThumbBrush",
				typeof(Brush),
				typeof(SaluseSlider),
				null);

		public static DependencyProperty FocusBrushProperty =
			DependencyProperty.Register(
				"FocusBrush",
				typeof(Brush),
				typeof(SaluseSlider),
				null);

		#endregion

		#region private variables

		private double _previousValue = 0;
		private LinearGradientBrush _linearGradientBrush;
		Thumb _thumb = null;
		private Popup _popup = null;
		private readonly double _popupWidthOffset = 15;
		private bool _popupIsPercentage = false;

		#endregion

		#region private methods

		private void FireChangedEvent()
		{
			if (Changed != null)
			{
				Changed(this, new EventArgs());
			}
		}

		#endregion

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_thumb = (this.GetTemplateChild("HorizontalThumb") as Thumb);
			if (_thumb != null)
			{
				_thumb.DragStarted += new DragStartedEventHandler(thumb_DragStarted);
				_thumb.MouseMove += new System.Windows.Input.MouseEventHandler(thumb_MouseMove);
				_thumb.DragCompleted += new DragCompletedEventHandler(thumb_DragCompleted);
			}

			Rectangle horizontalRectangle = (this.GetTemplateChild("TrackRectangle") as Rectangle);
			_linearGradientBrush = (horizontalRectangle.Fill as LinearGradientBrush);

			_popup = (this.GetTemplateChild("SliderPopup") as Popup);
			_popup.DataContext = this;

			// If the Pop Text is set to display Percentage, then dynamically change the ValueConvertor for the Slider
			if (_popupIsPercentage)
			{
				TextBlock popupTextBlock = (this.GetTemplateChild("SliderPopupTextBlock") as TextBlock);
				Binding binding = new Binding("Value");
				binding.Converter = new Saluse.LiquidPlayer.Controls.Formatters.PercentageConverter();
				popupTextBlock.SetBinding(TextBlock.TextProperty, binding);
			}
		}

		void thumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (SeekingEnd != null)
			{
				SeekingEnd(this, new EventArgs());
			}

			if (!(e.Canceled))
			{
				if (this.Value != _previousValue)
				{
					FireChangedEvent();
				}
			}

			if (_popup != null)
			{
				_popup.IsOpen = false;
			}
		}

		void thumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			_previousValue = this.Value;
			if (SeekingStart != null)
			{
				SeekingStart(this, new EventArgs());
			}

			if (_popup != null)
			{
				_popup.IsOpen = true;
			}
		}

		void thumb_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if (_popup != null)
			{
				if (_thumb.IsDragging)
				{
					Point point = Utilities.UI.GetPosition(_thumb, this);
					_popup.HorizontalOffset = (point.X - _popupWidthOffset);
				}
			}
		}

		#region public properties

		public double ProgressPercentage
		{
			set
			{
				if (_linearGradientBrush != null)
				{
					_linearGradientBrush.GradientStops[0].Offset = value;
					_linearGradientBrush.GradientStops[1].Offset = value;
				}
			}
			get
			{
				return 0;
			}
		}

		public Brush ThumbBrush
		{
			get
			{
				return (Brush)GetValue(ThumbBrushProperty);
			}
			set
			{
				SetValue(ThumbBrushProperty, value);
			}
		}

		public Brush FocusBrush
		{
			get
			{
				return (Brush)GetValue(FocusBrushProperty);
			}
			set
			{
				SetValue(FocusBrushProperty, value);
			}
		}

		public bool PopupIsPercentage
		{
			get
			{
				return _popupIsPercentage;
			}
			set
			{
				_popupIsPercentage = value;
			}
		}

		#endregion
	}
}
