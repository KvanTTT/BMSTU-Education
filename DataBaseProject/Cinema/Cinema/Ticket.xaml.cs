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
    /// Interaction logic for Ticket.xaml
    /// </summary>
    public partial class Ticket : Window
    {
        public Ticket(string MovieName, DateTime Date, DateTime Session, int RoomID, int Row, int Seat, int Price)
        {
            InitializeComponent();

            tbMovieName.Text = MovieName;
            tbDate.Text = Date.ToShortDateString();
            tbSession.Text = Session.ToShortTimeString();
            tbRoom.Text = RoomID.ToString();
            tbRow.Text = Row.ToString();
            tbSeat.Text = Seat.ToString();
            tbPrice.Text = Price.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
