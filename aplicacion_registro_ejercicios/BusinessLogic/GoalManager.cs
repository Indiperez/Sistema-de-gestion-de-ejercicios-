// BusinessLogic/GoalManager.cs
using System;
using System.Data.SQLite;
using ExerciseTrackingApp.DataAccess;

namespace ExerciseTrackingApp.BusinessLogic
{
    public class GoalManager
    {
        private readonly DatabaseHelper _dbHelper;

        public GoalManager(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void SetGoal(int frequency, int duration)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Goals (Frequency, Duration)
                    VALUES (@frequency, @duration)";
                command.Parameters.AddWithValue("@frequency", frequency);
                command.Parameters.AddWithValue("@duration", duration);
                command.ExecuteNonQuery();
            }
        }

        public void ListGoals()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Goals";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Frecuencia: {reader["Frequency"]} veces/semana, Duración: {reader["Duration"]} mins/semana");
                    }
                }
            }
        }

        public void DeleteAllGoals()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                // Eliminar todos los registros
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Goals";
                command.ExecuteNonQuery();

                // Reiniciar el contador de IDs
                command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Goals'";
                command.ExecuteNonQuery();
            }
        }

        public void UpdateGoal(int id, int newFrequency, int newDuration)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Goals 
                    SET Frequency = @frequency, Duration = @duration 
                    WHERE Id = @id";
                command.Parameters.AddWithValue("@frequency", newFrequency);
                command.Parameters.AddWithValue("@duration", newDuration);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}

