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
using System.IO;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //miAbout.che
        }

        private void GoToPageExecuteHandler(object sender, ExecutedRoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri((string)e.Parameter, UriKind.Relative));
        }

        private void GoToPageCanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.UserID == null)
            {
                LoginWindow LW = new LoginWindow();
                LW.ShowDialog();

                if (Settings.UserID == null)
                    return;

                if (Settings.Editing)
                    lblAdmin.Visibility = System.Windows.Visibility.Visible;
                if (Settings.Booking)
                    lblUser.Visibility = System.Windows.Visibility.Visible;
                button1.Content = "Выход";
                lblUserName.Visibility = System.Windows.Visibility.Visible;
                lblUserName.Content = Settings.UserName;
            }
            else
            {
                Settings.UserID = null;
                Settings.Booking = false;
                Settings.Editing = false;
                lblAdmin.Visibility = System.Windows.Visibility.Collapsed;
                lblUser.Visibility = System.Windows.Visibility.Collapsed;
                button1.Content = "Авторизация";
                frmContent.Content = null;
                lblUserName.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CinemaDataContext Context = new CinemaDataContext();
            Context.ExecuteCommand("delete from tblSeat");
            List<tblRoom> Rooms = Context.tblRooms.ToList();
            int c = 0;
            foreach (tblRoom R in Rooms)
            {
                for (int i = 0; i < R.RowNumber; i++)
                    for (int j = 0; j < R.SeatsNumber; j++)                    
                    {
                        tblSeat Seat = new tblSeat();
                        Seat.RoomID = R.RoomID;
                        Seat.Row = i + 1;
                        Seat.Seat = j + 1;

                        if (j > R.SeatsNumber / 5 && j < R.SeatsNumber * 4 / 5 &&
                            i > R.RowNumber / 3 && i < R.RowNumber * 4 / 5)
                            Seat.Vip = 0;
                        else
                            Seat.Vip = 1;
                       // Seat.Vip = 0;
                        c++;
                        Context.tblSeats.InsertOnSubmit(Seat);
                    }
            }
            Context.SubmitChanges();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            CinemaDataContext Context = new CinemaDataContext();

            int i = 0;
            string[] Strs = File.ReadAllLines("Films.txt");
            Context.ExecuteCommand("delete from tblMovie");
            do
            {
                tblMovie Movie = new tblMovie();
                Movie.MovieName = Strs[i++];
                Movie.MovieDirector = Strs[i++];
                Movie.MovieDuration = Convert.ToInt32(Strs[i++]);
                Movie.MovieYear = Convert.ToInt32(Strs[i++]);
                Movie.MinAge = Convert.ToInt32(Strs[i++]);
                Movie.MainActor = Strs[i++];

                Context.tblMovies.InsertOnSubmit(Movie);
                Context.SubmitChanges();               

                int MovieID = (from M in Context.tblMovies
                                      where M.MovieName == Movie.MovieName
                                      select M.MovieID).First();

                string[] GenresStrs = Strs[i++].Split();
                foreach (string S in GenresStrs)
                {
                    tblMovieGenre MovieGenre = new tblMovieGenre();
                    MovieGenre.MovieID = MovieID;
                    MovieGenre.GenreID = (from G in Context.tblGenres
                                          where G.GenreName == S
                                          select G.GenreID).First();
                    Context.tblMovieGenres.InsertOnSubmit(MovieGenre);
                }

                string[] CountriesStrs = Strs[i++].Split();
                foreach (string S in CountriesStrs)
                {
                    tblMovieCountry MovieCountry = new tblMovieCountry();
                    MovieCountry.MovieID = MovieID;
                    MovieCountry.CountryID = (from G in Context.tblCountries
                                          where G.CountryName == S
                                          select G.CountryID).First();
                    Context.tblMovieCountries.InsertOnSubmit(MovieCountry);
                }

                i++;
            }
            while (i < Strs.Length);


            Context.SubmitChanges();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
