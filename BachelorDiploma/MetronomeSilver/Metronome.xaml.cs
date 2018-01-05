namespace MetronomeSilver
{
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;

	public partial class Metronome : UserControl, INotifyPropertyChanged
	{
		private const double G = 9.81;

		private double pendulumLenght;
		private double theta0 = 45;
		private double thetaN = 0;
		private double alpha0 = 0;
		private double[] xx = new double[ 2 ];
		private double time = 0;
		private double dt;
		private TimeSpan date;
		private int tempo;
		private double period;

		public Metronome()
		{
			InitializeComponent();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public event EventHandler MetronomeReady;
		public event EventHandler MetronomeFailed;

		public int MetronomeTempo
		{
			get
			{
				return this.Pendulum.Tempo;
			}
		}

		public void SetMetronomeVolume( double volume )
		{
			this.TickPlayer.Volume = volume;
		}

		public void StartMetronome()
		{
			this.Pendulum.IsEnabled = false;
			this.tempo = this.MetronomeTempo;
			this.theta0 = Math.PI * 45 / 180;
			this.thetaN = this.theta0;
			this.alpha0 = Math.PI * 0 / 180;
			this.period = ( double )116.12 / ( double )this.tempo;
			this.pendulumLenght = this.CalculatePendulumLenght();
			this.Pendulum.SetAngle( this.theta0 );
			this.date = DateTime.Now.TimeOfDay;

			this.time = 0;
			this.xx = new double[] { this.theta0, this.alpha0 };
			CompositionTarget.Rendering += new EventHandler( this.CompositionTarget_Rendering );
		}

		public void StopMetronome()
		{
			CompositionTarget.Rendering -= new EventHandler( this.CompositionTarget_Rendering );
			this.Pendulum.SetAngle( 0 );
			this.TickPlayer.Stop();
			this.Pendulum.IsEnabled = true;
		}

		private double CalculatePendulumLenght()
		{
			return ( Math.Pow( this.period, 2 ) * G ) / ( 4 * Math.Pow( Math.PI, 2 ) );
		}

		private void CompositionTarget_Rendering( object sender, EventArgs e )
		{
			TimeSpan now = DateTime.Now.TimeOfDay;

			TimeSpan diff = now - this.date;
			this.dt = ( double )diff.Milliseconds / 1000;
			this.date = now;

			ODESolver.Function[] f = new ODESolver.Function[ 2 ] { this.F1, this.F2 };
			double[] result = ODESolver.RungeKutta4( f, this.xx, this.time, this.dt );

			this.Pendulum.SetAngle( 180 * result[ 0 ] / Math.PI );

			if ( ( this.thetaN > 0 && result[ 0 ] < 0 ) || ( this.thetaN < 0 && result[ 0 ] > 0 ) )
			{
				this.TickPlayer.Position = TimeSpan.FromTicks( 0 );
				this.TickPlayer.Play();
			}

			this.thetaN = result[ 0 ];
			this.xx = result;
			this.time += this.dt;
			this.time = Math.Round( this.time, 3 );
		}

		private double F1( double[] xx, double t )
		{
			return xx[ 1 ];
		}

		private double F2( double[] xx, double t )
		{
			double l = this.pendulumLenght;
			return -G * Math.Sin( xx[ 0 ] ) / l;
		}

		private void TickPlayer_MediaOpened( object sender, RoutedEventArgs e )
		{
			if ( this.MetronomeReady != null )
			{
				this.MetronomeReady( this, new EventArgs() );
			}
		}

		private void TickPlayer_MediaFailed( object sender, ExceptionRoutedEventArgs e )
		{
			if ( this.MetronomeFailed != null )
			{
				this.MetronomeFailed( this, new EventArgs() );
			}
		}

		private void Pendulum_PropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			if ( this.PropertyChanged != null )
			{
				this.PropertyChanged( this, new PropertyChangedEventArgs( e.PropertyName ) );
			}
		}
	}
}
