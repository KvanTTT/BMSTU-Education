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

namespace Cinema
{
    /// <summary>
    /// Interaction logic for Accounting.xaml
    /// </summary>
    public partial class Accounting : UserControl
    {
        public Accounting()
        {
            InitializeComponent();

            CinemaDataContext Context = new CinemaDataContext();
            var Films = Context.tblMovies.ToList();
            foreach (var F in Films)
                cmbFilms.Items.Add(F.MovieName);

            cmbFilms.SelectedIndex = 0;
            if (Settings.Booking)
            {
                tbUser.Visibility = System.Windows.Visibility.Visible;
                cmbUsers.Visibility = System.Windows.Visibility.Collapsed;

                tbUser.Text = Settings.UserName;
            }
            else 
            if (Settings.Editing)
            {
                tbUser.Visibility = System.Windows.Visibility.Collapsed;
                cmbUsers.Visibility = System.Windows.Visibility.Visible;

                var Users = from U in Context.tblUsers where U.Rights.Contains('B') select U;
                foreach (var U in Users)
                    cmbUsers.Items.Add(U.Username);
                cmbUsers.SelectedIndex = 0;
            }

        }

        void CalcululStat(int UserID)
        {
            CinemaDataContext Context = new CinemaDataContext();
            /*
            var SoldsQueue = from ST in Context.tblSoldTickets 
                            join 
                            
                            where ST.UserID == UserID*/
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CinemaDataContext Context = new CinemaDataContext();
            tbFilm.Text = Context.CountCashM((from M in Context.tblMovies 
                                              where M.MovieName == (string)cmbFilms.SelectedItem
                                              select M.MovieID).First()).ToString();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (datePicker1.SelectedDate == null || datePicker1.SelectedDate == null)
                    throw new Exception("Не введены даты");

                DateTime DT1 = (DateTime)datePicker1.SelectedDate;
                DateTime DT2 = (DateTime)datePicker2.SelectedDate;

                CinemaDataContext Context = new CinemaDataContext();
                tbPeriod.Text = Context.CountCashPeriod(DT1, DT2).ToString();
            }
            catch (Exception E)
            {
                
            }
        }

        private void cmbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



       
    }
}
