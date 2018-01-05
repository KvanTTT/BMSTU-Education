namespace MetronomeSilver
{
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;

	public partial class Pendulum : UserControl, INotifyPropertyChanged
	{
		private int tempo;

		public Pendulum()
		{
			InitializeComponent();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public int Tempo
		{
			get
			{
				return ( int )this.PendulumSlider.Value;
			}
		}

		public void SetAngle( double angle )
		{
			this.PendulumRotatateTransofrm.Angle = angle;
		}

		private void PendulumSlider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e )
		{
			if ( this.PropertyChanged != null )
			{
				this.PropertyChanged( this, new PropertyChangedEventArgs( "Tempo" ) );
			}
		}
	}
}
