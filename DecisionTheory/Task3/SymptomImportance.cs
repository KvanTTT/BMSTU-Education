using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
	public class SymptomImportance
	{
		public int SymptomId
		{
			get;
			set;
		}

		public double Importance
		{
			get;
			set;
		}

		public SymptomImportance(int symptomId, double importance)
		{
			SymptomId = symptomId;
			Importance = importance;
		}
	}
}
