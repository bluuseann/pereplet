using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для katalog.xaml
    /// </summary>
    public partial class katalog : Window
    {
        int count = 0; 
        int columns = 0;
        int row = 0;
      
        public katalog()
        {
            
            InitializeComponent();
            var contex = new AppDbContext();
            var q = contex.Tovars.Count();
            var l = contex.Korzina.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.Count));
           
      
            var w = contex.Tovars.Where(x=>x.Id >0).ToList();
            while (count < q)
            {
                if (columns == 4)
                {
                    columns = 0; 
                    row += 3;
                    if (row == 2)
                    {
                        break;
                    }
                }
                Image image = new Image();
                string a = w[count].Image.ToString();
                image.Source = new BitmapImage(new Uri($"{a}", UriKind.RelativeOrAbsolute));
                TextBlock textBlock = new TextBlock();
                textBlock.Text = w[count].Names;
                textBlock.TextWrapping = TextWrapping.Wrap;
                Button button = new Button();
                button.Content = w[count].Price.ToString() + " руб."; 
                button.Width = 150;
               button.Height = 35;
                button.Template = (ControlTemplate)Resources["овальная кнопка"];
                button.CommandParameter = a;
                button.Click += Button_Click;
                counts.Text = ss.ToString();
                Grid.SetColumn(image, columns); 
                Grid.SetRow(image, row);
                Grid.SetColumn(textBlock, columns);
                Grid.SetRow(textBlock, row+1);
                Grid.SetColumn(button, columns);
                Grid.SetRow(button, row+2);
                dd.Children.Add(image);
                dd.Children.Add(textBlock);
                dd.Children.Add(button);
            
                columns++;
                count++;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var context = new AppDbContext();
            string par = button.CommandParameter as string;
            var q = context.Tovars.Where(x => x.Image == par).ToList();
            var r = context.Korzina.Where(x => x.Image == par).ToList();
            if (r.Count > 0)
            {
                if (q[0].Id == r[0].Id)
                {
                    string price = (Convert.ToInt32(r[0].Count) + 1).ToString();
                    var h = context.Korzina.Where(x => x.Id == r[0].Id).AsEnumerable().Select(x => { x.Count = price; return x; });
                    foreach (var x in h)
                    {
                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            else
            {
                var tov = new Korzinas { Id = q[0].Id, Image = q[0].Image, Names = q[0].Names, Count = "1", Price = q[0].Price };
                context.Korzina.Add(tov);
            }
            
            context.SaveChanges();
            var l = context.Korzina.Where(x => x.Id > 0).ToList();
            int ss = l.Sum(x => Convert.ToInt32(x.Count));
            counts.Text = ss.ToString();
        }

        private void bas_Click(object sender, RoutedEventArgs e)
        {
            Basket basket = new Basket();
            basket.Show();
            this.Close();
        }
    }

}
