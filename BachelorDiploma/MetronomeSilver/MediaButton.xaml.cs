namespace MetronomeSilver
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	public enum ButtonTypes
	{
		Play,
		Stop
	}

	public partial class MediaButton : UserControl
	{
		private ButtonTypes buttonType;
		private string buttonTooltip;

		public MediaButton()
		{
			InitializeComponent();
		}

		public event EventHandler ButtonClicked;

		public ButtonTypes ButtonType
		{
			get
			{
				return this.buttonType;
			}

			set
			{
				this.buttonType = value;
				this.DrawButton( value );
			}
		}

		public string ButtonTooltip
		{
			get
			{
				return this.buttonTooltip;
			}

			set
			{
				this.buttonTooltip = value;
				ToolTipService.SetToolTip( this.ButtonFigure, value );
			}
		}

		private void DrawButton( ButtonTypes type )
		{
			switch ( type )
			{
				case ButtonTypes.Play:
					this.DrawPlayButton();
					break;
				case ButtonTypes.Stop:
					this.DrawStopButton();
					break;
				default:
					break;
			}
		}

		private void DrawStopButton()
		{
			this.ButtonFigureReflection.Points.Add( new Point( 0, 0 ) );
			this.ButtonFigureReflection.Points.Add( new Point( 20, 0 ) );
			this.ButtonFigureReflection.Points.Add( new Point( 20, 20 ) );
			this.ButtonFigureReflection.Points.Add( new Point( 0, 20 ) );

			this.ButtonFigure.Points.Add( new Point( 0, 0 ) );
			this.ButtonFigure.Points.Add( new Point( 20, 0 ) );
			this.ButtonFigure.Points.Add( new Point( 20, 20 ) );
			this.ButtonFigure.Points.Add( new Point( 0, 20 ) );
		}

		private void DrawPlayButton()
		{
			this.ButtonFigureReflection.Points.Add( new Point( 0, 0 ) );
			this.ButtonFigureReflection.Points.Add( new Point( 20, 10 ) );
			this.ButtonFigureReflection.Points.Add( new Point( 0, 20 ) );

			this.ButtonFigure.Points.Add( new Point( 0, 0 ) );
			this.ButtonFigure.Points.Add( new Point( 20, 10 ) );
			this.ButtonFigure.Points.Add( new Point( 0, 20 ) );
		}

		private void ButtonFigure_MouseEnter( object sender, MouseEventArgs e )
		{
			VisualStateManager.GoToState( this, "MouseOver", false );
			this.ButtonFigure.Cursor = Cursors.Hand;
		}

		private void ButtonFigure_MouseLeave( object sender, MouseEventArgs e )
		{
			VisualStateManager.GoToState( this, "Normal", false );
			this.ButtonFigure.Cursor = Cursors.Arrow;
		}

		private void ButtonFigure_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
		{
			VisualStateManager.GoToState( this, "Pressed", false );
		}

		private void ButtonFigure_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
		{
			VisualStateManager.GoToState( this, "MouseOver", false );

			if ( this.ButtonClicked != null )
			{
				this.ButtonClicked( this, new EventArgs() );
			}
		}
	}
}
