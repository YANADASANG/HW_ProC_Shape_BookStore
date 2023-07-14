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
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : UserControl
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private void txtSearch_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Customer> customer = CustomerData.GetData(txtSearch.Text);
            if (customer.Count() == 0)
            {
                MessageBox.Show("Not found this customer");
            }
            else
            {
                txtId.Text = customer[0].Customer_Id.ToString();
                txtName.Text = customer[0].Customer_Name;
                txtEmail.Text = customer[0].Customer_Email;
                txtAddress.Text = customer[0].Customer_Address;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (btnEdit.Content.ToString() == "Edit")
            {
                txtName.IsReadOnly = false;
                txtEmail.IsReadOnly = false;
                txtAddress.IsReadOnly = false;
                btnEdit.Content = "Save";
            }
            else if (btnEdit.Content == "Save") {
                btnEdit.Content = "Edit";
                MessageBox.Show("Saved");
            }

        }
    }
}
