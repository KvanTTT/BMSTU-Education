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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;

namespace HospitalClient
{
    /// <summary>
    /// Interaction logic for DatabaseInterfaceControl.xaml
    /// </summary>
    ///     
    
    public partial class DatabaseInterfaceControl : UserControl
    {
        System.Data.Linq.DataContext DatabaseContext;
        Type TableType;
        DataGrid DataGrid;
        Dictionary<string, PropertyInfo> Fields;
        string ConnectionString;
        bool Loaded = false;

        Visibility ToVisibility(bool Visible)
        {
            return Visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public DatabaseInterfaceControl()
        {
            InitializeComponent();
        }

        public DatabaseInterfaceControl(string ConnectionString, Type TableType, DataGrid DataGrid,
            Dictionary<string, PropertyInfo> Fields,
            bool Add = false, bool Edit = false, bool Delete = false, bool Search = true)
        {
            InitializeComponent();

            SetParams(ConnectionString, TableType, DataGrid, Fields, Add, Edit, Delete, Search);
        }

        public void SetParams(string ConnectionString, Type TableType, DataGrid DataGrid,
            Dictionary<string, PropertyInfo> Fields,
            bool? Add = null, bool? Edit = null, bool? Delete = null, bool Search = true)
        {
            if (Add != null)
            btnAdd.Visibility = ToVisibility((bool)Add);
            if (Edit != null)
            btnEdit.Visibility = ToVisibility((bool)Edit);
            if (Delete != null)
            btnDelete.Visibility = ToVisibility((bool)Delete);

            this.TableType = TableType;
            this.DataGrid = DataGrid;
            this.Fields = Fields;

            this.ConnectionString = ConnectionString; 
            btnRefresh_Click(null, null);
        }

        public void ChangeEditing(bool Editing)
        {
            if (Editing)
            {
                btnAdd.Visibility = System.Windows.Visibility.Visible;
                btnEdit.Visibility = System.Windows.Visibility.Visible;
                btnDelete.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnAdd.Visibility = System.Windows.Visibility.Hidden;
                btnEdit.Visibility = System.Windows.Visibility.Hidden;
                btnDelete.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DatabaseContext = new System.Data.Linq.DataContext(ConnectionString);
            AddingControls.CreateEditWindow W;
           /* if (TableType == typeof(SymptomsDiagnose))
                W = new HospitalClient.DatabaseControls.SymptomDiagnoseAddEdit(DatabaseContext);
            else*/
                W = new AddingControls.CreateEditWindow(DatabaseContext, TableType, Fields);
            W.ShowDialog();

            if (!W.CancelClick)
            {                
                DatabaseContext = new System.Data.Linq.DataContext(ConnectionString);
                if (DataGrid != null)
                    DataGrid.DataContext = DatabaseContext.GetTable(TableType);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                //DatabaseContext = new System.Data.Linq.DataContext(ConnectionString); 
                AddingControls.CreateEditWindow W = new AddingControls.CreateEditWindow(DatabaseContext, DataGrid.SelectedItem, Fields);
                W.ShowDialog();

                if (!W.CancelClick)
                {
                    DatabaseContext = new System.Data.Linq.DataContext(ConnectionString);
                    if (DataGrid != null)
                        DataGrid.DataContext = DatabaseContext.GetTable(TableType);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid != null)
                if (DataGrid.SelectedItem != null)
                {
                    if (MessageBox.Show(@"Вы действительно хотите удалить эту запись", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {                          
                        var Table = DatabaseContext.GetTable(TableType);
                        Table.DeleteOnSubmit(DataGrid.SelectedItem);
                        DatabaseContext.SubmitChanges();
                        DatabaseContext = new System.Data.Linq.DataContext(ConnectionString);
                        DataGrid.DataContext = DatabaseContext.GetTable(TableType);
                    }
                }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearchString.Text == "")
            {
                btnRefresh_Click(sender, e);
                return;
            }
            else
            {
                string SqlCommand;
                if (TableType.Name[TableType.Name.Length-1] == 'y')
                    SqlCommand = "select * from " + TableType.Name.Substring(0, TableType.Name.Length - 1) + "ies where ";
                else
                    SqlCommand = "select * from " + TableType.Name + "s where ";
                string[] SearchValues = tbSearchString.Text.Split();
                foreach (string Str in SearchValues)
                {
                    try
                    {
                        int I = Convert.ToInt32(Str);
                        foreach (var F in Fields)
                            if (F.Value.PropertyType == typeof(int))
                                SqlCommand += F.Value.Name + " = " + I.ToString() + " or ";
                    }
                    catch { }
                    try
                    {
                        double I = Convert.ToDouble(Str);
                        foreach (var F in Fields)
                            if (F.Value.PropertyType == typeof(double))
                                SqlCommand += F.Value.Name + " = " + I.ToString() + " or ";
                    }
                    catch { }

                    foreach (var F in Fields)
                        if (F.Value.PropertyType == typeof(string))
                            SqlCommand += F.Value.Name + " like '%" + Str + "%' or ";
                }
                SqlCommand = SqlCommand.Substring(0, SqlCommand.Length - 4);

                DatabaseContext = new System.Data.Linq.DataContext(ConnectionString);
                DataGrid.DataContext = DatabaseContext.ExecuteQuery(TableType, SqlCommand);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DatabaseContext = new System.Data.Linq.DataContext(ConnectionString); 
            DataGrid.DataContext = DatabaseContext.GetTable(TableType);
        }
    }
}
