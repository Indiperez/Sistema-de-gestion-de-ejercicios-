// DataAccess/DatabaseHelper.cs
using System;
using System.Data.SQLite;

namespace ExerciseTrackingApp.DataAccess
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Exercises (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Type TEXT NOT NULL,
                        Duration INTEGER NOT NULL,
                        Intensity TEXT NOT NULL,
                        Date TEXT NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS Goals (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Frequency INTEGER NOT NULL,
                        Duration INTEGER NOT NULL
                    );";
                command.ExecuteNonQuery();
            }
        }
    }
}

