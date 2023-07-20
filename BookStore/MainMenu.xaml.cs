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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Schema;
using static System.Resources.ResXFileRef;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        SolidColorBrush selectedColor = new SolidColorBrush(Color.FromRgb(255, 207, 0)); // assign color from rgb
        public MainMenu()
        {
            InitializeComponent();
            BookData.InitializeDatabase();
            CustomerData.InitializeDatabase();
            OrderData.InitializeDatabase();
            BookData.MockUpData();
            CustomerData.MockUpData();
            CustomerPage customerPage = new CustomerPage();
            customerPage.Height = 540;
            content.Children.Add(customerPage);
            changeColor(1);
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            txtCurrentMenu.Text = "Customer";
            content.Children.RemoveAt(0);
            CustomerPage customerPage = new CustomerPage();
            customerPage.Height = 540;
            content.Children.Add(customerPage);
            changeColor(1);
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            txtCurrentMenu.Text = "Book";
            content.Children.RemoveAt(0);
            BookPage bookPage = new BookPage();
            bookPage.Height = 540;
            content.Children.Add(bookPage);
            changeColor(2);
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            txtCurrentMenu.Text = "Order";
            content.Children.RemoveAt(0);
            OrderPage orderPage = new OrderPage();
            orderPage.Height = 540;
            content.Children.Add(orderPage);
            changeColor(3);
        }

        private void changeColor(int orderButton)
        {
            Button[] buttons = { btnCustomer, btnBook, btnOrder };
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == (orderButton - 1))
                {
                    buttons[i].Background = selectedColor;
                    buttons[i].BorderBrush = selectedColor;
                }
                else
                {
                    buttons[i].Background = Brushes.White;
                    buttons[i].BorderBrush = Brushes.White;
                }
            }
        }
    }
}
