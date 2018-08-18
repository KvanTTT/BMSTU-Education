using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HospitalClient.DatabaseControls
{
    /// <summary>
    /// Interaction logic for SymptomDiagnoseAddEdit.xaml
    /// </summary>
    public partial class SymptomDiagnoseAddEdit : Window
    {
        System.Data.Linq.DataContext DatabaseContext;
        string ConnectionString;
        SymptomsDiagnose Result;

        public SymptomDiagnoseAddEdit(System.Data.Linq.DataContext DatabaseContext)
        {
            InitializeComponent();

            this.DatabaseContext = DatabaseContext;

            var Queue = from S in DatabaseContext.GetTable<Symptom>() select S;
            List<string> Symptoms = new List<string>(Queue.Count());
            foreach (var Q in Queue)
                Symptoms.Add(Q.ID + " " + Q.Name);
            cmbSymptoms.ItemsSource = Symptoms;

            var Queue1 = from S in DatabaseContext.GetTable<Diagnose>() select S;
            List<string> Diagnoses = new List<string>(Queue1.Count());
            foreach (var Q in Queue1)
                Diagnoses.Add(Q.ID + " " + Q.Name);
            cmbDiagnoses.ItemsSource = Diagnoses;
        }

        public string UpdateOrInsert()
        {
            try
            {
                var Table = DatabaseContext.GetTable<SymptomsDiagnose>();

                SymptomsDiagnose SD = new SymptomsDiagnose();
                string SymptomsSelectedItem = (string)cmbSymptoms.SelectedItem;
                string DiagnosesSelectedItem = (string)cmbDiagnoses.SelectedItem;
                SD.SymptomID = Convert.ToInt32(SymptomsSelectedItem.Substring(0, SymptomsSelectedItem.IndexOf(' ')));
                SD.DiagnoseID = Convert.ToInt32(DiagnosesSelectedItem.Substring(0, DiagnosesSelectedItem.IndexOf(' ')));
                SD.ProbabYes = (double)nudYes.Value;
                SD.ProbabNo = (double)nudNo.Value;

                var Queue = from T in Table
                            where (T.SymptomID == SD.SymptomID && T.DiagnoseID == SD.DiagnoseID)
                            select T;
                if (Queue.Count() != 0)
                    return "Такое правило уже существует. Пара Симптом-Диагноз должна быть уникальной";

                Table.InsertOnSubmit(SD);
            }
            catch (Exception Ex)
            {
                return "Неизвестная ошибка: " + Ex.Message;
            }
            return null;            
        }

        public SymptomDiagnoseAddEdit(System.Data.Linq.DataContext DatabaseContext, SymptomsDiagnose SD) : this(DatabaseContext)
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrInsert();
        }
    }
}
