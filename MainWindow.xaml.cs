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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pereplet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void auto_Click(object sender, RoutedEventArgs e)
        {
            var login = log.Text;
            var password = pass.Text;
            var context = new AppDbContext();
            var user_exists =context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exists != null) {
                MessageBox.Show("Такой пользователь уже существует");
                return;
                    }
            var user = new User { Login = login, Password = password };
            context.Users.Add(user);
            context.SaveChanges();

        }
    }
}
