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

namespace Cinema
{
    /// <summary>
    /// Interaction logic for SelectCountries.xaml
    /// </summary>
    public partial class SelectCountries : Window
    {
        public List<string> Selected = null;

        public SelectCountries(Settings.SelectionMode SelMode)
        {
            InitializeComponent();

            CinemaDataContext Context = new CinemaDataContext();
            List<string> Strs;
            if (SelMode == Settings.SelectionMode.Countries)
            {
                Title = "Выберите страны";
                Strs = new List<string>(Context.tblCountries.Count());
                Context.tblCountries.ToList().ForEach(A => Strs.Add(A.CountryName));
            }
            else
                if (SelMode == Settings.SelectionMode.Genres)
                {
                    Title = "Выберите жанры";
                    Strs = new List<string>(Context.tblGenres.Count());
                    Context.tblGenres.ToList().ForEach(A => Strs.Add(A.GenreName));
                }
                else
                {
                    Title = "Выберите фильмы";
                    Strs = new List<string>(Context.tblMovies.Count());
                    Context.tblMovies.ToList().ForEach(A => Strs.Add(A.MovieName));
                }
            Strs.ForEach(A => { var LVI = new ListViewItem(); LVI.Content = A; listView1.Items.Add(LVI); });
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Selected = null;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                Selected = new List<string>(listView1.SelectedItems.Count);
                foreach (var S in listView1.SelectedItems)
                    Selected.Add((string)(S as ListViewItem).Content);
            }
            //if (Selected.Count == 0)
            //    Selected = null;
            Close();
        }


    }
}
