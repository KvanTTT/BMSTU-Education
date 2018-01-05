using System;
using System.Windows.Controls;

namespace SilverClient
{
	public partial class BassSoundBoard : UserControl, IMusicInstrument
	{
		public BassSoundBoard()
		{
			InitializeComponent();
		}

		public void Press(int ID, double Volume = 1)
		{
			
		}

		public void Release(int ID)
		{
			
		}

		public void PressAll()
		{
			
		}

		public void ReleaseAll()
		{
			
		}

		public bool Interactive
		{
			get;
			set;
		}

		public bool Mono
		{
			get;
			set;
		}


		public int MinID
		{
			get;
			set;
		}

		public int MaxID
		{
			get;
			set;
		}
	}
}
