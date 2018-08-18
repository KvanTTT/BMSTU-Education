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
    /// Interaction logic for Films.xaml
    /// </summary>
    public partial class Films : Page
    {
        public Films()
        {
            InitializeComponent();

            button3_Click(null, null);
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

                button3_Click(sender, e);
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

                button3_Click(sender, e);
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
                List<int> GenresID;
                List<int> CountriesID;
                GetID(tbGenres.Text, tbCountries.Text, out GenresID, out CountriesID);
                if (GenresID.Count == 0)
                    GenresID.AddRange((from MG in Context.tblGenres select MG.GenreID).ToArray());
                if (CountriesID.Count == 0)
                    CountriesID.AddRange((from MG in Context.tblCountries select MG.CountryID).ToArray());


                string findstr = textBox1.Text.Trim();
                tblMovieDataGrid.ItemsSource = (from M in Context.tblMovies
                                                join MG in Context.tblMovieGenres
                                                on M.MovieID equals MG.MovieID
                                                join MC in Context.tblMovieCountries
                                                on M.MovieID equals MC.MovieID
                                                where (M.MovieName.Contains(findstr) ||
                                                      M.MovieDirector.Contains(findstr) ||
                                                      M.MainActor.Contains(findstr) ||
                                                      M.MovieDuration.ToString().Contains(findstr) ||
                                                      M.MovieYear.ToString().Contains(findstr)) &&
                                                      (GenresID.Contains(MG.GenreID)) &&
                                                      (CountriesID.Contains(MC.CountryID))
                                                select new
                                                {
                                                    Название = M.MovieName,
                                                    Жанр = Context.ConcatGenres(M.MovieID),
                                                    Страна = Context.ConcatCountries(M.MovieID),
                                                    Режиссер = M.MovieDirector,
                                                    Время = M.MovieDuration,
                                                    Год = M.MovieYear,
                                                    Мин_возраст = M.MinAge,
                                                    Главный_актер = M.MainActor
                                                }).Distinct();
            }
            catch
            {
                MessageBox.Show("Ошибка формирования таблицы");
            }

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            button3_Click(sender, e);
        }
    }
}
