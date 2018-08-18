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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tbNickName.Text))
                    throw new Exception("Пустое имя пользователя");

                if (string.IsNullOrWhiteSpace(passwordBox1.Password))
                    throw new Exception("Пустой пароль");

                if (passwordBox1.Password != passwordBox2.Password)
                    throw new Exception("Пароли не совпадают");

                CinemaDataContext DatabaseContext = new CinemaDataContext();

                var UserQueue = from U in DatabaseContext.tblUsers
                                where U.Username == tbNickName.Text
                                select U;

                if (UserQueue.Count() != 0)
                    throw new Exception(" Пользователь с таким именем уже существует");

                tblUser User = new tblUser();
                User.Username = tbNickName.Text;
                User.Password = passwordBox1.Password;
                User.Rights = "E";

                DatabaseContext.tblUsers.InsertOnSubmit(User);
                DatabaseContext.SubmitChanges();

                MessageBox.Show("Вы успешно зарегестрированы");

                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show("Ошибка: " + E.Message);
            }
        }
    }
}
