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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CinemaDataContext DatabaseContext = new CinemaDataContext();

            var UserQueue = from U in DatabaseContext.tblUsers
                        where U.Username == tbNickName.Text && U.Password == passwordBox1.Password
                        select U;

            if (UserQueue.Count() != 0)
            {
                tblUser User = UserQueue.First();

                if (User.Rights.Contains('B'))
                    Settings.Booking = true;
                if (User.Rights.Contains('E'))
                    Settings.Editing = true;
                Settings.UserID = User.UserID;
                Settings.UserName = User.Username;

                MessageBox.Show("Вы успешно вошли в систему");
                Close();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Registration R = new Registration();
            R.ShowDialog();
        }
    }
}
