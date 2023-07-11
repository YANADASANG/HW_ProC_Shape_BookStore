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

namespace BookStore
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "Admin" && txtPassword.Password == "admin123")
            {
                MessageBox.Show("Welcome Admin");
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsername.Text = "";
                txtPassword.Password = "";
            }
        }
    }
}
