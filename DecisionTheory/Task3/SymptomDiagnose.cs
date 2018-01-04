using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
	public class SymptomDiagnose
	{
		public int SymptomId
		{
			get;
			set;
		}

		public int DiagnoseId
		{
			get;
			set;
		}

		public double YesProbability
		{
			get;
			set;
		}

		public double NoProbability
		{
			get;
			set;
		}

		public SymptomDiagnose()
		{
		}

		public SymptomDiagnose(int symptomId, int diagnoseId, 
			double yesProbability, double noProbability)
		{
			SymptomId = symptomId;
			DiagnoseId = diagnoseId;
			YesProbability = yesProbability;
			NoProbability = noProbability;
		}

		public override string ToString()
		{
			return String.Format("SymptomId={0}; DiagnoseId={1}; YesProbability={2}; NoProbability={3}", 
				SymptomId, DiagnoseId, YesProbability, NoProbability);
		}
	}
}
