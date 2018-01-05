using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverClient
{
	public partial class GuitarSoundBoard : UserControl, IMusicInstrument
	{
		public GuitarSoundBoard()
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
