using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pereplet
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void auto_Click(object sender, RoutedEventArgs e)
        {
            if (pass.Text== pass2.Text)
            {
                var login = log.Text;
                var password = pass.Text;
                var context = new AppDbContext();
                var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
                if (user_exists != null)
                {
                    MessageBox.Show("Такой пользователь уже существует");
                    return;
                }
                var user = new User { Login = login, Password = password };
                context.Users.Add(user);
                context.SaveChanges();
            }
            else MessageBox.Show("Пароли не совпадают");

        }
    }
}
