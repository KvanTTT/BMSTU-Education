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
    /// Interaction logic for FilmShowEditing.xaml
    /// </summary>
    public partial class FilmShowEditing : UserControl
    {
        public FilmShowEditing()
        {
            InitializeComponent();

            CinemaDataContext Context = new CinemaDataContext();

            ReloadFilmsRooms();
            //cmbFilm2.SelectedIndex = 0;

            udDuration.Value = 120;
            udYear.Value = 2011;
            udMinAge.Value = 0;
            udHour.Value = 15;
        }

        void ReloadFilmsRooms()
        {
            try
            {
                CinemaDataContext Context = new CinemaDataContext();
                var Films = Context.tblMovies.ToList();
                var Rooms = Context.tblRooms.ToList();
                cmbFilm2.Items.Clear();
                cmbFilm3.Items.Clear();
                cmbRoom.Items.Clear();
                foreach (var F in Films)
                {
                    cmbFilm2.Items.Add(F.MovieName);
                    cmbFilm3.Items.Add(F.MovieName);
                }
                foreach (var R in Rooms)
                {
                    cmbRoom.Items.Add(R.RoomID);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Невозможно обновить списки");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SelectCountries SC = new SelectCountries(Settings.SelectionMode.Genres);
            SC.ShowDialog();            
            if (SC.Selected != null)
            {
                tbGenres.Text = "";
                SC.Selected.ForEach(A => tbGenres.Text += A + ", ");
                if (SC.Selected.Count != 0)
                    tbGenres.Text = tbGenres.Text.Remove(tbGenres.Text.Length - 2);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SelectCountries SC = new SelectCountries(Settings.SelectionMode.Countries);
            SC.ShowDialog();            
            if (SC.Selected != null)
            {
                tbCountries.Text = "";
                SC.Selected.ForEach(A => tbCountries.Text += A + ", ");
                if (SC.Selected.Count != 0)
                    tbCountries.Text = tbCountries.Text.Remove(tbCountries.Text.Length - 2);
            }
        }

        void GetID(string GenreStr, string CountryStr, out List<int> GenresID, out List<int> CountriesID)
        {
            CinemaDataContext Context = new CinemaDataContext();
            
            GenresID = new List<int>();
            string[] Strs = GenreStr.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string S in Strs)
                GenresID.Add((from MG in Context.tblGenres where MG.GenreName == S select MG.GenreID).ToArray()[0]);

            CountriesID = new List<int>();
            Strs = CountryStr.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string S in Strs)
                CountriesID.Add((from MG in Context.tblCountries where MG.CountryName == S select MG.CountryID).ToArray()[0]);
        }



        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tblMovie Movie = new tblMovie();
                if (string.IsNullOrWhiteSpace(tbFilmName.Text)) throw new Exception("Не введено название фильма");
                Movie.MovieName = tbFilmName.Text;

                if (string.IsNullOrWhiteSpace(tbDirector.Text)) throw new Exception("Не введено имя режиссера");
                Movie.MovieDirector = tbDirector.Text;
                Movie.MovieDuration = Convert.ToInt32(udDuration.Value);
                Movie.MovieYear = Convert.ToInt32(udYear.Value);
                Movie.MinAge = Convert.ToInt32(udMinAge.Value);
                if (string.IsNullOrWhiteSpace(tbMainHero.Text)) throw new Exception("Не введено имя главного актера");
                Movie.MainActor = tbMainHero.Text;

                if (string.IsNullOrWhiteSpace(tbGenres.Text)) throw new Exception("Не выбран хотя бы один жанр");
                if (string.IsNullOrWhiteSpace(tbCountries.Text)) throw new Exception("Не выбран хотя бы одна страна");

                CinemaDataContext Context = new CinemaDataContext();
                Context.tblMovies.InsertOnSubmit(Movie);
                Context.SubmitChanges();

                int MovieID = (from M in Context.tblMovies where M.MovieName == tbFilmName.Text select M.MovieID).ToArray()[0];

                List<int> GenresID;
                List<int> CountriesID;
                GetID(tbGenres.Text, tbCountries.Text, out GenresID, out CountriesID);
                
                foreach (int ID in GenresID)
                {
                    tblMovieGenre MovieGenre = new tblMovieGenre();
                    MovieGenre.MovieID = MovieID;
                    MovieGenre.GenreID = ID;
                    Context.tblMovieGenres.InsertOnSubmit(MovieGenre);                    
                }

                foreach (int ID in CountriesID)
                {
                    tblMovieCountry MovieCountry = new tblMovieCountry();
                    MovieCountry.MovieID = MovieID;
                    MovieCountry.CountryID = ID;
                    Context.tblMovieCountries.InsertOnSubmit(MovieCountry);
                }

                Context.SubmitChanges();

                ReloadFilmsRooms();

                MessageBox.Show("Фильм " + Movie.MovieName + " успешно добавлен");
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка: " + Ex.Message);
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbFilm2.SelectedValue == null) throw new Exception("Не выбран фильм");
                if (cmbRoom.SelectedValue == null) throw new Exception("Не выбран зал");
                if (datePicker1.SelectedDate == null || datePicker2.SelectedDate == null)
                    throw new Exception("Не выбрана дата");                

                DateTime DT1 = (DateTime)datePicker1.SelectedDate;
                DateTime DT2 = (DateTime)datePicker2.SelectedDate;


                if (DT2.DayOfYear - DT1.DayOfYear > 7)
                    throw new Exception("Максимальная количество дней - 7");

                CinemaDataContext Context = new CinemaDataContext();
                int MovieID = (from M in Context.tblMovies where M.MovieName == (string)cmbFilm2.SelectedValue select M.MovieID).ToArray()[0];
                int RoomID = (int)cmbRoom.SelectedValue;

                for (DateTime DT = DT1; DT <= DT2; DT = DT.Date.AddDays(1))
                {
                    DateTime NewSession = new DateTime(2011, 01, 01, Convert.ToInt32(udHour.Value), Convert.ToInt32(udMinute.Value), 0);
                    int? S = Context.IsShowCorrect(RoomID, MovieID, DT, NewSession);
                    if (DT <= DateTime.Now)
                    {
                        MessageBox.Show("Сеанс " + DT.ToShortDateString() + " " + NewSession.Hour.ToString() + ":" +
                            NewSession.Minute.ToString() + " раньше текущего времени");
                        continue;
                    }
                    if (S == 0 || S == null)
                    {
                        tblShow Show = new tblShow();
                        Show.MovieID = (from M in Context.tblMovies where M.MovieName == (string)cmbFilm2.SelectedItem select M.MovieID).ToArray()[0];
                        Show.RoomID = (int)cmbRoom.SelectedItem;
                        Show.Date = DT;
                        Show.Session = NewSession;
                        Show.VipPrice = 0;
                        Show.NotVipPrice = 0;

                        Context.tblShows.InsertOnSubmit(Show);
                        Context.SubmitChanges();
                    }
                    else
                    {
                        MessageBox.Show("Сеанс с ID = " + S.ToString() + " пересекается с уже имеющимся сеансом в этом зале");
                    }
                }

                MessageBox.Show("Сеансы успешно добавлены");
            }
            catch (Exception E)
            {
                    MessageBox.Show("Ошибка: " + E.Message);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFilm3.SelectedItem != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить этот фиильм?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CinemaDataContext Context = new CinemaDataContext();
                        int MovieID = (from M in Context.tblMovies where M.MovieName == (string)cmbFilm3.SelectedItem select M.MovieID).ToArray()[0];
                        if (Context.HasShows(MovieID) == 1)
                            throw new Exception("На данный фильм есть сеансы");
                        Context.ExecuteCommand("delete from tblMovie where MovieID = " + MovieID.ToString());
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Ошибка: " + E.Message);
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ReloadFilmsRooms();
        }
    }
}
