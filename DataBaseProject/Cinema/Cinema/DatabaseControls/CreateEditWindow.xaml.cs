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
using System.Reflection;

namespace HospitalClient.AddingControls
{
    /// <summary>
    /// Interaction logic for CreateEditWindow.xaml
    /// </summary>
    public partial class CreateEditWindow : Window
    {
        System.Data.Linq.DataContext DatabaseContext;

        const int MarginLeft = 8, MarginTop = 6, MarginRight = 8, MarginBottom = 6;
        const int ValueItemWidth = 210, ValueItemHeight = 23;
        bool Creating = false;

        Dictionary<string, Control> ValueItems;
        Button ButtonOk, ButtonCancel;

        Dictionary<string, PropertyInfo> Fields;

        Type TableType;
        object Result;

        public bool CancelClick = true;
        
        Control CreateValueItem(Type ItemType)
        {
            Control Result;
            if (ItemType == typeof(bool) || ItemType == typeof(System.Nullable<bool>))
                Result = new CheckBox();
            else
                if (ItemType == typeof(DateTime) || ItemType == typeof(System.Nullable<DateTime>))
                    Result = new DatePicker();
                else
                    Result = new TextBox();
            return Result; 
        }

        void EditValueItem(Control ValueItem, object Value)
        {
            if (Value != null)
            if (ValueItem is CheckBox)
                (ValueItem as CheckBox).IsChecked = Convert.ToBoolean(Value);
            else
                if (ValueItem is DatePicker)
                    (ValueItem as DatePicker).DisplayDate = (DateTime)Value;
                else
                    (ValueItem as TextBox).Text = Convert.ToString(Value);
        }

        void SetValues(Object O)
        {
            foreach (KeyValuePair<string, PropertyInfo> Pair in Fields)
            {
                EditValueItem(ValueItems["item" + Pair.Value.Name], Pair.Value.GetValue(O, new object[0])); 
            }
        }

        object GetValue(Control ValueItem, Type ResultType)
        {
            if (ValueItem is CheckBox)
                return (ValueItem as CheckBox).IsChecked;
            else
                if (ValueItem is DatePicker)
                    return (ValueItem as DatePicker).DisplayDate;
                else
                {
                    if ((ValueItem as TextBox).Text == "")
                        return null;
                    if (ResultType == typeof(Int64) || ResultType == typeof(Int64?))
                        return Convert.ToInt64((ValueItem as TextBox).Text);
                    if (ResultType == typeof(int) || ResultType == typeof(int?))
                        return Convert.ToInt32((ValueItem as TextBox).Text);
                    else
                        if (ResultType == typeof(short) || ResultType == typeof(short?))
                            return Convert.ToInt16((ValueItem as TextBox).Text);
                    if (ResultType == typeof(byte) || ResultType == typeof(byte?))
                        return Convert.ToByte((ValueItem as TextBox).Text);
                    else
                        if (ResultType == typeof(double) || ResultType == typeof(double?))
                            return Convert.ToDouble((ValueItem as TextBox).Text);
                        else
                            if (ResultType == typeof(char) || ResultType == typeof(char?))
                                return Convert.ToChar((ValueItem as TextBox).Text);
                            else
                                return (ValueItem as TextBox).Text;
                }
        }

        public void RemakeWindow(Dictionary<string, PropertyInfo> Fields)
        {
            int i = 0;
            int MaxLabelWidth = 0;
            ValueItems = new Dictionary<string,Control>(Fields.Count);

            foreach (KeyValuePair<string, PropertyInfo> Pair in Fields)
                if (Pair.Key.Length > MaxLabelWidth)
                    MaxLabelWidth = Pair.Key.Length;
            MaxLabelWidth *= 8;

            wrapPanelMain.Width = MarginLeft + MaxLabelWidth + MarginRight + MarginLeft + ValueItemWidth + MarginRight;
            wrapPanelMain.Height = (MarginTop + MarginBottom + ValueItemHeight) * (Fields.Count + 1);
            windowMain.Width = wrapPanelMain.Width + 22;
            windowMain.Height = wrapPanelMain.Height + 39;

            foreach (KeyValuePair<string, PropertyInfo> Pair in Fields)
            {
                Label L = new Label();
                L.Content = Pair.Key;
                L.Width = MaxLabelWidth;
                L.Height = ValueItemHeight;                
                L.Margin = new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);
                L.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                L.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                L.Name = "lbl" + Pair.Value.Name;

                
                Control C = CreateValueItem(Pair.Value.GetType());
                C.Width = ValueItemWidth;
                C.Height = ValueItemHeight;
                C.Margin = new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);
                C.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                C.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                C.Name = "item" + Pair.Value.Name;
                if (Creating && Pair.Value.Name == "ID")
                    (C as TextBox).Text = ((int)DatabaseContext.ExecuteQuery<int>("select max(ID) from " + TableType.Name + "s").First() + 1).ToString();
                

                ValueItems.Add(C.Name, C);

                wrapPanelMain.Children.Add(L);
                wrapPanelMain.Children.Add(C);
            }

            ButtonOk = new Button();
            ButtonOk.Content = "ОК";
            ButtonOk.Width = MaxLabelWidth;
            ButtonOk.Height = ValueItemHeight;                
            ButtonOk.Margin = new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);
            ButtonOk.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ButtonOk.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ButtonOk.Name = "btnOk";
            ButtonOk.Click += new RoutedEventHandler(ButtonOk_Click);

            ButtonCancel = new Button();
            ButtonCancel.Content = "Отмена";
            ButtonCancel.Width = ValueItemWidth;
            ButtonCancel.Height = ValueItemHeight;                
            ButtonCancel.Margin = new Thickness(MarginLeft, MarginTop, MarginRight, MarginBottom);
            ButtonCancel.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ButtonCancel.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            ButtonCancel.Name = "btnCancel";
            ButtonCancel.Click += new RoutedEventHandler(ButtonCancel_Click);
            
            wrapPanelMain.Children.Add(ButtonOk);
            wrapPanelMain.Children.Add(ButtonCancel);          
        }        

        void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Table = DatabaseContext.GetTable(TableType);

                if (Creating)
                    Result = Activator.CreateInstance(TableType);

                foreach (KeyValuePair<string, PropertyInfo> Pair in Fields)
                {
                    object Value = GetValue(ValueItems["item" + Pair.Value.Name], Pair.Value.PropertyType);
                    if (Pair.Value.Name == "ID" && Value == null)
                        Value = ((int)DatabaseContext.ExecuteQuery<int>("select max(ID) from " + TableType.Name + "s").First() + 1).ToString();
                    Pair.Value.SetValue(Result, Value, new object[0]);
                }

                if (Creating)
                    Table.InsertOnSubmit(Result);
                DatabaseContext.SubmitChanges();

                CancelClick = false;

                Close();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }

        void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public CreateEditWindow(System.Data.Linq.DataContext DatabaseContext, Type Type, Dictionary<string, PropertyInfo> Fields)
        {
            InitializeComponent();

            TableType = Type;
            windowMain.Title = "Добавление";
            this.DatabaseContext = DatabaseContext;
            Creating = true;
            this.Fields = Fields;
            RemakeWindow(Fields);            
        }

        public CreateEditWindow(System.Data.Linq.DataContext DatabaseContext, Object O, Dictionary<string, PropertyInfo> Fields)
        {
            InitializeComponent();

            TableType = O.GetType();
            windowMain.Title = "Редактирование";
            this.DatabaseContext = DatabaseContext;
            Creating = false;
            this.Fields = Fields;
            RemakeWindow(Fields);
            SetValues(O);
            Result = O;            
        }

        private void numericUpDown1_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            
        }


        private void windowMain_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
