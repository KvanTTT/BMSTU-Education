using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace Task3
{
	public class Helper
	{
		public static List<Symptom> Symptoms;
		public static List<Diagnose> Diagnoses;
		public static List<SymptomDiagnose> SymptomsDiagnoses;
		public static List<SymptomImportance> SymptomImportances;
		public static void LoadKnownBase()
		{
			LoadSymptoms();
			LoadDiagnoses();
			LoadSymptomsDiagnoses();
		}

		public static void LoadSymptoms()
		{
			var reader = new StreamReader("Data/Symptoms.xml");
			var symptomsSerializer = new XmlSerializer(typeof(List<Symptom>));
			Symptoms = (List<Symptom>)symptomsSerializer.Deserialize(reader);
			reader.Close();
		}

		public static void LoadDiagnoses()
		{
			var reader = new StreamReader("Data/Diagnoses.xml");
			var diagnosesSerializer = new XmlSerializer(typeof(List<Diagnose>));
			Diagnoses = (List<Diagnose>)diagnosesSerializer.Deserialize(reader);
			reader.Close();
		}

		public static void LoadSymptomsDiagnoses()
		{
			var reader = new StreamReader("Data/SymptomsDiagnoses.xml");
			var symptomsDiagnosesSerializer = new XmlSerializer(typeof(List<SymptomDiagnose>));
			SymptomsDiagnoses = (List<SymptomDiagnose>)symptomsDiagnosesSerializer.Deserialize(reader);
			reader.Close();

			SymptomImportances = new List<SymptomImportance>(Symptoms.Count);
			Symptoms.ForEach(symptom => SymptomImportances.Add(new SymptomImportance(symptom.Id, 0)));
			foreach (var symptomDiagnose in SymptomsDiagnoses)
			{
				var symptomImportances = SymptomImportances.Where(answerImportance => answerImportance.SymptomId == symptomDiagnose.SymptomId);
				foreach (var symptomImportance in symptomImportances)
					symptomImportance.Importance += Math.Abs(symptomDiagnose.YesProbability - symptomDiagnose.NoProbability);
			}

			SymptomImportances = SymptomImportances.OrderByDescending(answerImportance => answerImportance.Importance).ToList();
		}

		public static void SaveKnownBase()
		{
			SaveSymptoms();
			SaveDiagnoses();
			SaveSymptomsDiagnoses();
		}

		public static void SaveSymptoms()
		{
			var writer = new StreamWriter("Data/Symptoms.xml");
			var symptomsSerializer = new XmlSerializer(typeof(List<Symptom>));
			symptomsSerializer.Serialize(writer, Symptoms);
			writer.Close();
		}

		public static void SaveDiagnoses()
		{
			var writer = new StreamWriter("Data/Diagnoses.xml");
			var diagnosesSerializer = new XmlSerializer(typeof(List<Diagnose>));
			diagnosesSerializer.Serialize(writer, Diagnoses);
			writer.Close();
		}

		public static void SaveSymptomsDiagnoses()
		{
			var writer = new StreamWriter("Data/SymptomsDiagnoses.xml");
			var symptomsDiagnosesSerializer = new XmlSerializer(typeof(List<SymptomDiagnose>));
			symptomsDiagnosesSerializer.Serialize(writer, SymptomsDiagnoses);
			writer.Close();
		}
	}
}
