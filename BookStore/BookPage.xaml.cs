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
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : UserControl
    {
        public BookPage()
        {
            InitializeComponent();
        }
        private void txtSearch_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (btnEdit.Content.ToString() == "Edit")
            {
                txtISBN.IsReadOnly = false;
                txtTitle.IsReadOnly = false;
                txtDescription.IsReadOnly = false;
                txtPrice.IsReadOnly = false;
                btnEdit.Content = "Save";
            }
            else if (btnEdit.Content.ToString() == "Save")
            {
                BookData.UpdateData(txtISBN.Text, txtTitle.Text, txtDescription.Text, int.Parse(txtPrice.Text));
                txtISBN.IsReadOnly = true;
                txtTitle.IsReadOnly = true;
                txtDescription.IsReadOnly = true;
                txtPrice.IsReadOnly = true;
                btnEdit.Content = "Edit";
                MessageBox.Show("Saved");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this book", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result.ToString() == "Yes")
            {
                BookData.DeleteData(txtISBN.Text);
                txtISBN.Text = "";
                txtTitle.Text = "";
                txtDescription.Text = "";
                txtPrice.Text = "";
                MessageBox.Show("Deleted");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAdd.Content.ToString() == "Add")
            {
                txtAddISBN.IsReadOnly = false;
                txtTitle.IsReadOnly = false;
                txtDescription.IsReadOnly = false;
                txtPrice.IsReadOnly = false;
                btnAdd.Content = "Submit";
            }
            else if (btnAdd.Content.ToString() == "Submit")
            {
                txtAddISBN.IsReadOnly = true;
                txtTitle.IsReadOnly = true;
                txtDescription.IsReadOnly = true;
                txtPrice.IsReadOnly = true;
                BookData.AddData(txtAddISBN.Text, txtAddTitle.Text, txtAddDescription.Text, int.Parse(txtAddPrice.Text));
                MessageBox.Show("Add");
            }
        }
    }
}
