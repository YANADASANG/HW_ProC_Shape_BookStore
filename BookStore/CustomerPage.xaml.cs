﻿using System;
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            if (btnEdit.Content.ToString() == "Edit")
            {
                txtId.IsReadOnly = false;
                txtName.IsReadOnly = false;
                txtEmail.IsReadOnly = false;
                txtAddress.IsReadOnly = false;
                btnEdit.Content = "Save";
            }
            else if (btnEdit.Content.ToString() == "Save")
            {
                btnEdit.Content = "Edit";
                txtId.IsReadOnly = true;
                txtName.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
                txtAddress.IsReadOnly = true;
                if (txtId.Text == "" && txtName.Text == "" && txtEmail.Text == "" && txtAddress.Text == "")
                {
                    MessageBox.Show("Input can not be blank", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    CustomerData.UpdateData(int.Parse(txtId.Text), txtName.Text, txtAddress.Text, txtEmail.Text);
                    MessageBox.Show("Saved");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "" && txtName.Text == "" && txtEmail.Text == "" && txtAddress.Text == "")
            {
                MessageBox.Show("Input can not be blank", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBoxResult Result = MessageBox.Show("Are you sure to delete this customer", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result.ToString() == "Yes")
                {
                    CustomerData.DeleteData(int.Parse(txtId.Text));
                    txtId.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";
                    MessageBox.Show("Deleted");
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (btnAdd.Content.ToString() == "Add")
            {
                txtAddName.IsReadOnly = false;
                txtAddEmail.IsReadOnly = false;
                txtAddAddress.IsReadOnly = false;
                btnAdd.Content = "Submit";
            }
            else if (btnAdd.Content.ToString() == "Submit")
            {
                btnAdd.Content = "Add";
                txtAddName.IsReadOnly = true;
                txtAddEmail.IsReadOnly = true;
                txtAddAddress.IsReadOnly = true;
                if (txtAddName.Text != "" && txtAddEmail.Text != "" && txtAddAddress.Text != "")
                {
                    MessageBox.Show("Added");
                    CustomerData.AddData(txtAddName.Text, txtAddAddress.Text, txtAddEmail.Text);
                }
                else
                {
                    MessageBox.Show("Input can not be blank", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
