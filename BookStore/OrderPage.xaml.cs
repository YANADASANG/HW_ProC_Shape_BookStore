using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : UserControl
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> customerId = CustomerData.GetCustomerId();
            foreach (var id in customerId)
            {
                cmbCustomerID.Items.Add(id);
            }
            cmbCustomerHistory.Items.Add("All");
            List<int> allId = OrderData.GetHistoryAllId();
            foreach (var id in allId)
            {
                cmbCustomerHistory.Items.Add(id);
            }

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            int total_Price = int.Parse(txtPrice.Text) * int.Parse(txtAmount.Text);
            string text = $"Order of Customer ID {cmbCustomerID.SelectedItem}" + Environment.NewLine 
                + $"Book ISBN {txtISBN.Text}" + Environment.NewLine
                + $"Book Title {txtTitle.Text}" + Environment.NewLine
                + $"Quatity {txtAmount.Text}" + Environment.NewLine
                + $"Total Price {total_Price}";
            MessageBoxResult result = MessageBox.Show(text, "", MessageBoxButton.YesNo);
            if (result.ToString() == "Yes")
            {
                OrderData.AddData(txtISBN.Text, int.Parse(cmbCustomerID.SelectedItem.ToString()), int.Parse(txtAmount.Text), total_Price);
                txtISBN.Text = string.Empty;
                txtTitle.Text = string.Empty;
                txtDescription.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtAmount.Text = string.Empty;
                cmbCustomerID.SelectedIndex = 0;
            }
        }

        private void txtSearch_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Book> book = BookData.GetData(txtSearch.Text);
            if (book.Count() == 0)
            {
                MessageBox.Show("Not found this book");
            }
            else
            {
                txtISBN.Text = book[0].ISBN.ToString();
                txtTitle.Text = book[0].Title.ToString();
                txtDescription.Text = book[0].Description.ToString();
                txtPrice.Text = book[0].Price.ToString();
            }
        }

        private void cmbCustomerHistory_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Order> orders = OrderData.GetHistory(cmbCustomerHistory.SelectedItem.ToString());
            MessageBox.Show(orders.Count.ToString());
        }
    }
}
