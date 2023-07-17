using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class BookData
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Books (ISBN NVARCHAR(2048) PRIMARY KEY, " +
                    "Title NVARCHAR(2048), Description NVARCHAR(2048), Price INTEGER)";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void MockUpData()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();

                for (int i = 1; i < 6; i++)
                {
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT INTO Books VALUES (@ISBN, @Title, @Description, @Price);";
                    insertCommand.Parameters.AddWithValue("@ISBN", $"123456{i}");
                    insertCommand.Parameters.AddWithValue("@Title", $"test{i}");
                    insertCommand.Parameters.AddWithValue("@Description", $"test{i} Description");
                    insertCommand.Parameters.AddWithValue("@Price", i * 10);
                    insertCommand.ExecuteReader();
                }
            }

        }
        public static void AddData(string ISBN, string title, string description, int price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Books VALUES (@ISBN, @Title, @Description, @Price);";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.ExecuteReader();
            }
        }
        public static ObservableCollection<Book> GetData(string input, string typeOfInput)
        {
            ObservableCollection<Book> customers = new ObservableCollection<Book>();
            bool inputCanParse = int.TryParse(input, out int inputParsed);
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();
                string command;
                if (typeOfInput)
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
