using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookStore
{
    class OrderData
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=TransactionsTable"))
            {
                db.Open();

                string tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (ISBN NVARCHAR(2048), " +
                    "Customer_Id INTEGER, Quatity INTEGER, Total_Price INTEGER)";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
        public static void AddData(string ISBN, int customerId, int quatity, int price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=TransactionsTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO Transactions VALUES (@ISBN, @CustomerId, @Quatity, @Price);";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.Parameters.AddWithValue("@CustomerId", customerId);
                insertCommand.Parameters.AddWithValue("@Quatity", quatity);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.ExecuteReader();
            }
        }

        public static ObservableCollection<Order> GetHistory(string input)
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            using (SqliteConnection db = new SqliteConnection($"Filename=TransactionsTable"))
            {
                db.Open();
                bool inputCanParse = int.TryParse(input, out int inputParsed);
                string command = "SELECT * from Transactions GROUP BY Customer_Id";
                if (inputCanParse)
                {
                    command = $"SELECT * from Transactions WHERE Customer_Id = {inputParsed}";
                }
                SqliteCommand selectCommand = new SqliteCommand
                    (command, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Order order = new Order();
                    order.iSBN = query.GetString(0);
                    order.customerId = query.GetInt32(1);
                    order.Quatity = query.GetInt32(2);
                    order.Price = query.GetInt32(3);
                    orders.Add(order);
                }
                db.Close();
            }
            return orders;
        }
        public static List<int> GetHistoryAllId()
        {
            List<int> allId = new List<int>();
            using (SqliteConnection db = new SqliteConnection($"Filename=TransactionsTable"))
            {

                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Customer_Id from Transactions GROUP BY Customer_Id", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    allId.Add(query.GetInt32(0));
                }
                db.Close();
            }
            return allId;
        }

    }
}
