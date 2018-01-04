using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.IO;

namespace Task3
{
	public partial class MainWindow : Window
	{
		Queue<SymptomImportance> SymptomImportances;
		Dictionary<int, double> SymptomDegrees;
		Dictionary<int, double> DiagnoseProbabilities;

		public MainWindow()
		{
			InitializeComponent();

			Helper.LoadKnownBase();
		}

		private void btnAnswer_Click(object sender, RoutedEventArgs e)
		{
			if (SymptomImportances == null || SymptomImportances.Count == 0)
			{
				MessageBox.Show("Начните новый сеанс");
				return;
			}

			var currentSymptomId = SymptomImportances.Dequeue().SymptomId;
			var degree = Convert.ToDouble((sender as Button).Tag) / 4.0;
			SymptomDegrees.Add(currentSymptomId, degree);

			var relatedSymptomsDiagnoses = Helper.SymptomsDiagnoses.Where(symptomDiagnose => symptomDiagnose.SymptomId == currentSymptomId);
			foreach (var symptomsDiagnose in relatedSymptomsDiagnoses)
			{
				DiagnoseProbabilities[symptomsDiagnose.DiagnoseId] =
					CalculateNewProbabilities(DiagnoseProbabilities[symptomsDiagnose.DiagnoseId],
					symptomsDiagnose.YesProbability, symptomsDiagnose.NoProbability, degree);
			}

			var symptomsQueue = from s in Helper.Symptoms
								join sd in SymptomDegrees
								on s.Id equals sd.Key
								select new { Symptom = s.Name, Degree = sd.Value };
			symptomsDataGrid.ItemsSource = symptomsQueue;

			var diagnosesQueue = from d in Helper.Diagnoses
								 join dd in DiagnoseProbabilities
								 on d.Id equals dd.Key
								 orderby dd.Value descending
								 select new { Diagnose = d.Name, Probability = dd.Value };
			diagnosesDataGrid.ItemsSource = diagnosesQueue;

			if (SymptomImportances.Count == 0)
			{
				tbMostProbabilityDiagnose.Content = "Наиболее вероятный диагноз: " + diagnosesQueue.First().Diagnose;
				MessageBox.Show("Наиболее вероятный диагноз: " + diagnosesQueue.First().Diagnose);
				tbQuestion.Text = string.Empty;
			}
			else
				tbQuestion.Text = Helper.Symptoms[SymptomImportances.Peek().SymptomId - 1].Question;
		}

		private static double CalculateNewProbabilities(double p, double yesP, double noP, double degree)
		{
			yesP = (1 - yesP) + degree * (2 * yesP - 1);
			noP = (1 - noP) + degree * (2 * noP - 1);
			return yesP * p / (p * yesP + (1 - p) * noP);
		}

		private void btnViewKnownBase_Click(object sender, RoutedEventArgs e)
		{
			var window = new KnownBaseWindow();
			window.Show();
		}

		private void btnNewSession_Click(object sender, RoutedEventArgs e)
		{
			SymptomDegrees = new Dictionary<int, double>(Helper.Symptoms.Count);
			DiagnoseProbabilities = new Dictionary<int, double>(Helper.Diagnoses.Count);
			Helper.Diagnoses.ForEach(diagnose => DiagnoseProbabilities.Add(diagnose.Id, diagnose.Probability));
			
			SymptomImportances = new Queue<SymptomImportance>(Helper.SymptomImportances);
			
			tbQuestion.Text = Helper.Symptoms[SymptomImportances.Peek().SymptomId - 1].Question;
			tbMostProbabilityDiagnose.Content = "";
		}

		private void diagnosesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (diagnosesDataGrid.SelectedItem != null)
			{
				tbDescription.DataContext = diagnosesDataGrid.SelectedItem;
				var selectedItem = diagnosesDataGrid.SelectedItem;
				string DiagnoseName = (string)selectedItem.GetType().GetProperty("Diagnose").GetValue(selectedItem, new object[0]);

				TextRange range;
				FileStream fStream;

				if (!string.IsNullOrWhiteSpace(DiagnoseName))
				{
					if (File.Exists(String.Format(@"Data\DiagnosesDescriptions\{0}.rtf", DiagnoseName)))
					{

						range = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd);
						fStream = new FileStream(String.Format(@"Data\DiagnosesDescriptions\{0}.rtf", DiagnoseName), FileMode.Open);
						range.Load(fStream, DataFormats.Rtf);
						fStream.Close();
					}
					else
					{
						range = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd);
						range.Text = "Нет информации";
					}
				}
				else
				{
					range = new TextRange(tbDescription.Document.ContentStart, tbDescription.Document.ContentEnd);
					range.Text = "Нет информации";
				}
			}
		}
	}
}
