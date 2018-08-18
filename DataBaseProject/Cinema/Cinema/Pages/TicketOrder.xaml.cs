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
    /// Interaction logic for TicketOrder.xaml
    /// </summary>
    public partial class TicketOrder : Page
    {
        List<ShowFilmsAfterCurrentTime> FutureFilms;
        tblShow Show;
        tblRoom Room;
        string MovieName;
        List<DateTime> Times;

        public TicketOrder()
        {
            InitializeComponent();                        

            try
            {
                CinemaDataContext Context = new CinemaDataContext();

                FutureFilms = Context.ShowFilmsAfterCurrentTimes.ToList();
                foreach (var F in FutureFilms)
                    cmbFilms.Items.Add(F.Name);
            }
            catch (Exception E)
            {
                MessageBox.Show("Ошибка: " + E.Message);
            }

            cmbFilms.SelectedIndex = 0;

            borderFree.Background = new SolidColorBrush(Colors.PowderBlue);
            borderBusy.Background = new SolidColorBrush(Colors.MediumVioletRed);
            borderVIP.Background = new SolidColorBrush(Colors.Gold);
            borderSelected.Background = new SolidColorBrush(Colors.Green);
        }

        private void cmbFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFilms.SelectedIndex != -1)
            {
                try
                {
                    CinemaDataContext Context = new CinemaDataContext();
                    MovieName = (string)cmbFilms.SelectedItem;

                    var DateQuery = (from S in Context.tblShows
                                     join M in Context.tblMovies
                                     on S.MovieID equals M.MovieID
                                     where M.MovieName == MovieName
                                     select S.Date).Distinct();

                    cmbDate.Items.Clear();
                    foreach (DateTime DT in DateQuery)
                        cmbDate.Items.Add(DT);
                }
                catch (Exception E)
                {
                    MessageBox.Show("Ошибка: " + E.Message);
                }
            }
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDate.SelectedIndex != -1)
            {
                try
                {
                    CinemaDataContext Context = new CinemaDataContext();
                    string MovieName = (string)cmbFilms.SelectedItem;
                    DateTime Date = (DateTime)cmbDate.SelectedItem;

                    var ShowsQuery = (from S in Context.tblShows
                                      join M in Context.tblMovies
                                      on S.MovieID equals M.MovieID
                                      where M.MovieName == MovieName &&
                                      S.Date == Date
                                      select S.Session).Distinct();

                    cmbShow.Items.Clear();
                    Times = new List<DateTime>(ShowsQuery.Count());
                    foreach (DateTime DT in ShowsQuery)
                    {
                        cmbShow.Items.Add(DT.ToShortTimeString());
                        Times.Add(DT);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Ошибка: " + E.Message);
                }
            }                        
        }

        private void cmbShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbShow.SelectedIndex != -1)
            {
                try
                {
                    gridBook.Visibility = System.Windows.Visibility.Visible;

                    CinemaDataContext Context = new CinemaDataContext();

                    string MovieName = (string)cmbFilms.SelectedItem;
                    DateTime Date = (DateTime)cmbDate.SelectedItem;

                    var ShowQuery = (from S in Context.tblShows
                                     join M in Context.tblMovies
                                     on S.MovieID equals M.MovieID
                                     where M.MovieName == MovieName && S.Date == Date && S.Session == Times[cmbShow.SelectedIndex]
                                     select S).Distinct();

                    Show = ShowQuery.First();

                    lblNotVip.Content = " - Эконом (" + Show.NotVipPrice.ToString() + ")";
                    lblVip.Content = " - VIP (" + Show.VipPrice.ToString() + ")";

                    RedrawRoom((from R in Context.tblRooms where R.RoomID == Show.RoomID select R).First());
                }
                catch (Exception E)
                {
                    MessageBox.Show("Ошибка: " + E.Message);
                }
            }
            else
                gridBook.Visibility = System.Windows.Visibility.Collapsed;
        }

        struct Seat
        {
            public bool IsFree;
            public bool IsSelected;
            public bool IsVip;
        }

        Seat[,] Seats;

        void RedrawRoom(tblRoom Room)
        {
            CinemaDataContext Context = new CinemaDataContext();

            tbRoomID.Text = Room.RoomID.ToString();

            wrapPanel1.Children.Clear();
            double SeatWidth = wrapPanel1.ActualWidth / (Room.SeatsNumber + 1);
            double SeatHeight = wrapPanel1.ActualHeight / (Room.RowNumber + 1);

            Seats = new Seat[Room.RowNumber, Room.SeatsNumber];

            TextBlock TB = new TextBlock();
            TB.Text = "";
            TB.Width = SeatWidth;
            TB.Height = SeatHeight;
            TB.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            TB.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            TB.Background = new SolidColorBrush(Colors.AntiqueWhite);
            wrapPanel1.Children.Add(TB);
            for (int j = 0; j < Room.SeatsNumber; j++)
            {
                TB = new TextBlock();
                TB.Text = (j + 1).ToString();
                TB.Width = SeatWidth;
                TB.Height = SeatHeight;
                TB.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                TB.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                TB.Background = new SolidColorBrush(Colors.AntiqueWhite);
                wrapPanel1.Children.Add(TB);
            }
            for (int i = 0; i < Room.RowNumber; i++)
            {
                TB = new TextBlock();
                TB.Text = (i + 1).ToString();
                TB.Width = SeatWidth;
                TB.Height = SeatHeight;
                TB.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                TB.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                TB.Background = new SolidColorBrush(Colors.AntiqueWhite);
                wrapPanel1.Children.Add(TB);

                for (int j = 0; j < Room.SeatsNumber; j++)
                {
                    Button B = new Button();
                    SolidColorBrush Brush = new SolidColorBrush();

                    int Vip = (from S in Context.tblSeats
                         where S.RoomID == Room.RoomID &&
                               S.Row == i + 1 && S.Seat == j + 1
                         select S.Vip).ToArray()[0];
                    if (Vip == 0)
                    {
                        Brush = (SolidColorBrush)borderVIP.Background;
                        Seats[i, j].IsVip = true;
                    }
                    else
                    {
                        Brush = (SolidColorBrush)borderFree.Background;
                        Seats[i, j].IsVip = false;
                    }


                    if ((from T in Context.tblTickets
                         where T.ShowID == Show.ShowID && T.Row == i + 1 && T.Seat == j + 1 && T.Status == 1
                         select T).Count() != 0)
                    {
                        Brush = (SolidColorBrush)borderBusy.Background;
                        Seats[i, j].IsFree = false;
                    }
                    else
                    {
                        Seats[i, j].IsFree = true;
                    }

                    Seats[i, j].IsSelected = false;
                    
                    B.Tag = i.ToString() + " " + j.ToString();
                    B.Background = Brush;
                    B.Width = SeatWidth;
                    B.Height = SeatHeight;
                    B.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    B.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    B.Click += btnSeat_Click;

                    wrapPanel1.Children.Add(B);
                }
            }
        }



        private void btnSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button B = sender as Button;
                string[] Strs = (B.Tag as string).Split();
                int i = Convert.ToInt32(Strs[0]);
                int j = Convert.ToInt32(Strs[1]);
                if (Seats[i, j].IsFree == false)
                {
                    MessageBox.Show("Это место занято");
                }
                else
                {
                    if (Seats[i, j].IsSelected)
                    {
                        Seats[i, j].IsSelected = false;
                        if (Seats[i, j].IsVip)
                            B.Background = borderVIP.Background;
                        else
                            B.Background = borderFree.Background;
                    }
                    else
                    {
                        Seats[i, j].IsSelected = true;
                        B.Background = borderSelected.Background;
                    }
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Ошибка: " + E.Message);
            }
        }


        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CinemaDataContext Context = new CinemaDataContext();
                List<tblTicket> TempSeats = new List<tblTicket>();
                
                int Sum = 0;
                for (int i = 0; i < Seats.GetLength(0); i++)
                    for (int j = 0; j < Seats.GetLength(1); j++)
                    {
                        if (Seats[i, j].IsSelected)
                        {
                            if ((from T in Context.tblTickets
                                 where T.ShowID == Show.ShowID && T.Row == i + 1 && T.Seat == j + 1 && T.Status == 1
                                 select T).Count() != 0)
                            {                                
                                throw new Exception("Место " + (j + 1).ToString() + " в ряду " + (i + 1).ToString() + " уже занято" +
                                    Environment.NewLine + "Заказ будет отменен");
                            }

                            tblTicket Ticket = (from T in Context.tblTickets
                                                where T.Row == i + 1 && T.Seat == j + 1 && T.ShowID == Show.ShowID
                                                select T).First();
                            TempSeats.Add(Ticket); 
                
                            if (Seats[i, j].IsVip)
                                Sum += Show.VipPrice;
                            else
                                Sum += Show.NotVipPrice;
                        }
                    }

                foreach (var T in TempSeats)
                {
                    tblSoldTicket SoldTicket = new tblSoldTicket();
                    SoldTicket.UserID = (int)Settings.UserID;
                    SoldTicket.TicketID = T.TicketID;

                    T.Status = 1;
                    Context.tblSoldTickets.InsertOnSubmit(SoldTicket);
                }

                try
                {
                    Context.SubmitChanges();

                    MessageBox.Show("Вы заказали " + TempSeats.Count + " билет(а/ов)" + Environment.NewLine +
                                    "на сумму " + Sum + " руб.");
                    foreach (var T in TempSeats)
                    {
                        Ticket TicketWindow = new Ticket(MovieName, Show.Date, Show.Session, Show.RoomID, (int)T.Row, (int)T.Seat,
                                            Seats[(int)T.Row - 1, (int)T.Seat - 1].IsVip ? Show.VipPrice : Show.NotVipPrice);
                        TicketWindow.ShowDialog();
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Невозможно сделать заказ: " + E.Message);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("Ошибка: " + E.Message);
            }
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            cmbShow_SelectionChanged(sender, null);            
        }

        
    }
}
