namespace MetronomeSilver
{
	using System;
	using System.Windows;
	using System.Windows.Browser;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;

	public partial class LogoTextControl : UserControl
	{
		public LogoTextControl()
		{
			InitializeComponent();
		}

		public string Text
		{
			get
			{
				return this.NormalText.Text;
			}

			set
			{
				this.NormalText.Text = value;
				this.BorderText.Text = value;
			}
		}

		private void NormalText_MouseEnter( object sender, System.Windows.Input.MouseEventArgs e )
		{
			this.NormalText.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xF7, 0xF7, 0xF7));
			this.NormalText.Cursor = Cursors.Hand;
		}

		private void NormalText_MouseLeave( object sender, System.Windows.Input.MouseEventArgs e )
		{
			this.NormalText.Foreground = new SolidColorBrush( Color.FromArgb( 255, 136, 136, 136 ) );
			this.NormalText.Cursor = Cursors.Arrow;
		}

		private void NormalText_MouseLeftButtonUp( object sender, System.Windows.Input.MouseButtonEventArgs e )
		{
			//HtmlPage.Window.Navigate( new Uri( "http://www.silverlightshow.net", UriKind.Absolute ), "_blank" );
		}
	}
}
