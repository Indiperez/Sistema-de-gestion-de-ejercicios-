// BusinessLogic/ExerciseManager.cs
// BusinessLogic/ExerciseManager.cs
using System;
using System.Data.SQLite;
using ExerciseTrackingApp.DataAccess;

namespace ExerciseTrackingApp.BusinessLogic
{
    public class ExerciseManager
    {
        private readonly DatabaseHelper _dbHelper;

        public ExerciseManager(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void AddExercise(string type, int duration, string intensity)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Exercises (Type, Duration, Intensity, Date)
                    VALUES (@type, @duration, @intensity, @date)";
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@duration", duration);
                command.Parameters.AddWithValue("@intensity", intensity);
                command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }
        }

        public void ListExercises()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Exercises";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]}, Tipo: {reader["Type"]}, Duración: {reader["Duration"]} mins, Intensidad: {reader["Intensity"]}, Fecha: {reader["Date"]}");
                    }
                }
            }
        }

        public void DeleteAllExercises()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                // Eliminar todos los registros
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Exercises";
                command.ExecuteNonQuery();

                // Reiniciar el contador de IDs
                command.CommandText = "DELETE FROM sqlite_sequence WHERE name='Exercises'";
                command.ExecuteNonQuery();
            }
        }

        public void UpdateExercise(int id, string newType, int newDuration, string newIntensity)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Exercises 
                    SET Type = @type, Duration = @duration, Intensity = @intensity 
                    WHERE Id = @id";
                command.Parameters.AddWithValue("@type", newType);
                command.Parameters.AddWithValue("@duration", newDuration);
                command.Parameters.AddWithValue("@intensity", newIntensity);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}


