using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                    "EXISTS Books (ISBN INTEGER PRIMARY KEY, " +
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
                int ISBN;
                for (int i = 1; i < 6; i++)
                {
                    ISBN = int.Parse($"123456{i}");
                    SqliteCommand insertCommand = new SqliteCommand();
                    insertCommand.Connection = db;
                    insertCommand.CommandText = "INSERT or IGNORE INTO Books VALUES (@ISBN, @Title, @Description, @Price);";
                    insertCommand.Parameters.AddWithValue("@ISBN", $"{ISBN}");
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
                insertCommand.CommandText = "INSERT OR IGNORE INTO Books VALUES (@ISBN, @Title, @Description, @Price);";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.ExecuteReader();
            }
        }
        public static ObservableCollection<Book> GetData(string input)
        {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();
                string command;
                string typeOfInput = "Title";
                bool inputCanParse = int.TryParse(input, out int inputParsed);
                if (inputCanParse)
                {
                    typeOfInput = "ISBN";
                }
                command = $" WHERE {typeOfInput} = \"{input}\"";
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from Books" + command, db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    Book book = new Book();
                    book.ISBN = query.GetString(0);
                    book.Title = query.GetString(1);
                    book.Description = query.GetString(2);
                    book.Price = query.GetInt32(3);
                    books.Add(book);
                }
            }

            return books;
        }

        public static void UpdateData(string ISBN, string title, string description, int price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "UPDATE Books\r\nSET Title = @Title, Description = @Description, Price = @Price \r\nWHERE ISBN = @ISBN;";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.Parameters.AddWithValue("@Title", title);
                insertCommand.Parameters.AddWithValue("@Description", description);
                insertCommand.Parameters.AddWithValue("@Price", price);
                insertCommand.ExecuteReader();

            }
        }

        public static void DeleteData(string ISBN)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=BooksTable"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "DELETE FROM Books WHERE ISBN = @ISBN ;";
                insertCommand.Parameters.AddWithValue("@ISBN", ISBN);
                insertCommand.ExecuteReader();
            }
        }
    }
}
