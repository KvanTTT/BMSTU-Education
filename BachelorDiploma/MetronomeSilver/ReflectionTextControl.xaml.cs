namespace MetronomeSilver
{
	using System.Windows.Controls;

	public partial class ReflectionTextControl : UserControl
	{
		public ReflectionTextControl()
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
				this.ReflectedText.Text = value;
			}
		}
	}
}
