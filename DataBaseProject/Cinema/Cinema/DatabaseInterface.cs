using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
using System.Windows;

namespace HospitalClient
{
    class DatabaseInterface
    {
        System.Data.Linq.DataContext DatabaseContext;
        Dictionary<string, PropertyInfo> Fields;
        Type TableType;
        DataGrid DataGrid;

        public DatabaseInterface(System.Data.Linq.DataContext DatabaseContext, Type TableType, Dictionary<string, PropertyInfo> Fields, DataGrid DataGrid)
        {
            this.DatabaseContext = DatabaseContext;
            this.TableType = TableType;
            this.Fields = Fields;
            this.DataGrid = DataGrid;
        }

        public void Add()
        {
            AddingControls.CreateEditWindow W = new AddingControls.CreateEditWindow(DatabaseContext, TableType, Fields);
            W.ShowDialog();

            DataGrid.DataContext = DatabaseContext.GetTable(TableType);
        }

        public void Edit()
        {
            if (DataGrid.SelectedItem != null)
            {
                AddingControls.CreateEditWindow W = new AddingControls.CreateEditWindow(DatabaseContext, DataGrid.SelectedItem, Fields);
                W.ShowDialog();

                DataGrid.DataContext = DatabaseContext.GetTable(TableType);
            }
        }

        public void Delete()
        {
            if (DataGrid.SelectedItem != null)
            {
                if (MessageBox.Show(@"Вы действительно хотите удалить эту запись", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var Table = DatabaseContext.GetTable(TableType);
                    Table.DeleteOnSubmit(DataGrid.SelectedItem);
                    DatabaseContext.SubmitChanges();
                    DataGrid.DataContext = Table;
                }
            }
        }

        public void OnButtonClick(object sender, RoutedEventArgs Args)
        {
            Button B = sender as Button;
            if (B.Name.Contains("Add"))
                Add();
            else
                if (B.Name.Contains("Edit"))
                    Edit();
                else
                    if (B.Name.Contains("Delete"))
                        Delete();
        }
    }
}
