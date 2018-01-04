using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
	public class Diagnose
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public double Probability
		{
			get;
			set;
		}

		public Diagnose()
		{
		}

		public Diagnose(int id, string name, double probability)
		{
			Id = id;
			Name = name;
			Probability = probability;
		}

		public override string ToString()
		{
			return String.Format("Id={0}; Name={1}; Probability={2}", Id, Name, Probability);
		}
	}
}
