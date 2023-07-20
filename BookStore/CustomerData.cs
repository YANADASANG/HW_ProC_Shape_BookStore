using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class CustomerData
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (Customer_Id INTEGER PRIMARY KEY, " +
                    "Customer_Name NVARCHAR(2048), Address NVARCHAR(2048), Email NVARCHAR(2048))";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void MockUpData()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();

                for (int i = 1; i < 6; i++)
                {
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT OR IGNORE INTO Customers VALUES (@Id, @Name, @Address, @Email);";
                    insertCommand.Parameters.AddWithValue("@Id", $"{i}");
                    insertCommand.Parameters.AddWithValue("@Name", $"test{i}");
                    insertCommand.Parameters.AddWithValue("@Address", $"test{i} address");
                    insertCommand.Parameters.AddWithValue("@Email", $"test{i}@gmail.com");
                    insertCommand.ExecuteReader();
                }
            }

        }
        public static void AddData(string name, string address, string email)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Customers VALUES (NULL, @Name, @Address, @Email);";
                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.Parameters.AddWithValue("@Address", address);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.ExecuteReader();

            }
        }
        public static ObservableCollection<Customer> GetData(string input)
        {
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            bool inputCanParse = int.TryParse(input, out int inputParsed);
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();
                string command;
                if (inputCanParse)
                {
                    command = $" WHERE Customer_Id = {inputParsed}";
                }
                else
                {
                    command = $" WHERE Customer_Name = \"{input}\"";
                }
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Customers" + command, db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Customer customer = new Customer();
                    customer.Customer_Id = query.GetInt32(0);
                    customer.Customer_Name = query.GetString(1);
                    customer.Customer_Address = query.GetString(2);
                    customer.Customer_Email = query.GetString(3);
                    customers.Add(customer);
                }
            }
            return customers;
        }

        public static List<int> GetCustomerId()
        {
            List<int> customerName = new List<int>();
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Id from Customers" , db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    customerName.Add(query.GetInt32(0));
                }
                db.Close();
            }
            return customerName;
        }

        public static void UpdateData(int id, string name, string address, string email)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE Customers\r\nSET Customer_Name = @Name, Address = @Address, Email = @Email \r\nWHERE Customer_Id = @Id;";
                insertCommand.Parameters.AddWithValue("@Id", id);
                insertCommand.Parameters.AddWithValue("@Name", name);
                insertCommand.Parameters.AddWithValue("@Address", address);
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.ExecuteReader();
            }
        }

        public static void DeleteData(int id)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=CustomersTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM Customers WHERE Customer_id = @Id ;";
                insertCommand.Parameters.AddWithValue("@Id", id);
                insertCommand.ExecuteReader();
            }
        }
    }
}
