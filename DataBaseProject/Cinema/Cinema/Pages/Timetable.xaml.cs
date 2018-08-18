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

namespace Cinema.Pages
{
    /// <summary>
    /// Interaction logic for Timetable.xaml
    /// </summary>
    public partial class Timetable : Page
    {
        public Timetable()
        {
            InitializeComponent();
            CinemaDataContext Context;

            try
            {
                Context = new CinemaDataContext();
            }
            catch
            {
                MessageBox.Show("Невозможно подсоединиться к базе данных");
                return;
            }

            try
            {
                var Films = Context.tblMovies.ToList();
                foreach (var F in Films)
                {
                    comboBox1.Items.Add(F.MovieName);
                }
                comboBox1.SelectedIndex = 0;

                if (Settings.Editing)
                    btnDelete.Visibility = System.Windows.Visibility.Visible;
            }
            catch
            {
                MessageBox.Show(@"Таблица ""Movies"" не найдена");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            comboBox1_SelectionChanged(null, null);
        }

        void GetID(CinemaDataContext Context, string FilmsStr, out List<int> FilmsID)
        {
            FilmsID = new List<int>();
            string[] Strs = FilmsStr.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string S in Strs)
                FilmsID.Add((from M in Context.tblMovies where M.MovieName == S select M.MovieID).ToArray()[0]);
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CinemaDataContext Context;
            try
            {
                Context = new CinemaDataContext();
            }
            catch
            {
                MessageBox.Show("Невозможно подсоединиться к базе данных");
                return;
            }

            try
            {
                DateTime DT1, DT2;
                if (datePicker1.SelectedDate == null)
                    DT1 = new DateTime(DateTime.Now.Year, 01, 01);
                else
                    DT1 = (DateTime)datePicker1.SelectedDate;
                if (datePicker2.SelectedDate == null)
                    DT2 = new DateTime(2012, 01, 01);
                else
                    DT2 = (DateTime)datePicker2.SelectedDate;

                int MovieID = (from M in Context.tblMovies where M.MovieName == (string)comboBox1.SelectedItem select M.MovieID).ToArray()[0];

                List<int> FilmsID;
                GetID(Context, tbFilms.Text, out FilmsID);
                if (FilmsID.Count == 0)
                    FilmsID.AddRange((from M in Context.tblMovies select M.MovieID).ToArray());

                dataGrid1.ItemsSource = from S in Context.tblShows
                                        join M in Context.tblMovies
                                        on S.MovieID equals M.MovieID
                                        where S.Date >= DT1 && S.Date <= DT2 &&
                                              FilmsID.Contains(M.MovieID)
                                        select new
                                        {
                                            ID = S.ShowID,
                                            Дата = S.Date.ToShortDateString(),
                                            Время = S.Session.ToShortTimeString(),
                                            Фильм = M.MovieName,
                                            Зал = S.RoomID,
                                            Свободных_мест = Context.FreePlaces(S.ShowID)
                                        };
            }
            catch
            {
                MessageBox.Show("Ошибка формирования запроса");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SelectCountries SC = new SelectCountries(Settings.SelectionMode.Films);
            SC.ShowDialog();            
            if (SC.Selected != null)
            {
                tbFilms.Text = "";
                SC.Selected.ForEach(A => tbFilms.Text += A + ", ");
                if (SC.Selected.Count != 0)
                    tbFilms.Text = tbFilms.Text.Remove(tbFilms.Text.Length - 2);
                comboBox1_SelectionChanged(null, null);
            }            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count != 0 && 
                MessageBox.Show("Вы действительно хотите удалить выбранные сеансы?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (object Item in dataGrid1.SelectedItems)
                {
                    try
                    {
                        int ShowID = (int)Item.GetType().GetProperty("ID").GetValue(Item, new object[0]);
                        CinemaDataContext Context = new CinemaDataContext();
                        tblShow Show = ((from S in Context.tblShows where S.ShowID == ShowID select S).ToArray().First());
                        Context.tblShows.DeleteOnSubmit(Show);
                        try
                        {
                            Context.SubmitChanges();                            
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить сеанс с ID = " + ShowID.ToString() + ". На него уже куплены билеты");
                        }
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Ошибка удаления: " + E.Message);
                    }
                }
                comboBox1_SelectionChanged(sender, null);
            }
        }


    }
}
