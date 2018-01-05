using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverClient
{
	public interface IMusicInstrument
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="Volume">Sound volume between 0 and 1</param>
		void Press(int ID, double Volume = 1);
		void Release(int ID);
		void PressAll();
		void ReleaseAll();

		bool Interactive
		{
			get;
			set;
		}

		bool Mono
		{
			get;
			set;
		}

		int MinID
		{
			get;
			set;
		}

		int MaxID
		{
			get;
			set;
		}
	}
}
