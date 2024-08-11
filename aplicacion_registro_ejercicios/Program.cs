// Presentación/Program.cs
using System;
using ExerciseTrackingApp.DataAccess;
using ExerciseTrackingApp.BusinessLogic;

namespace ExerciseTrackingApp.Presentación
{
    class Program
    {
        static void Main(string[] args)
        {
            // Título de la aplicación
            Console.Title = "Aplicación de Seguimiento de Ejercicio";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=========================================");
            Console.WriteLine("     Bienvenido a la Aplicación de");
            Console.WriteLine("     Seguimiento de Ejercicio");
            Console.WriteLine("=========================================");
            Console.ResetColor();

            string connectionString = "Data Source=exercise.db";
            var dbHelper = new DatabaseHelper(connectionString);

            dbHelper.InitializeDatabase();

            var ejercicioManager = new ExerciseManager(dbHelper);
            var metaManager = new GoalManager(dbHelper);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Menú Principal ===");
                Console.ResetColor();
                Console.WriteLine("1. Agregar Ejercicio");
                Console.WriteLine("2. Listar Ejercicios");
                Console.WriteLine("3. Establecer Meta");
                Console.WriteLine("4. Listar Metas");
                Console.WriteLine("5. Eliminar Todos los Ejercicios");
                Console.WriteLine("6. Eliminar Todas las Metas");
                Console.WriteLine("7. Modificar Ejercicio");
                Console.WriteLine("8. Modificar Meta");
                Console.WriteLine("9. Salir");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Elige una opción: ");
                Console.ResetColor();

                var opción = Console.ReadLine();

                switch (opción)
                {
                    case "1":
                        Console.Write("Ingresa el Tipo de Ejercicio: ");
                        string tipo = Console.ReadLine();
                        Console.Write("Ingresa la Duración (minutos): ");
                        int duracion = int.Parse(Console.ReadLine());
                        Console.Write("Ingresa la Intensidad (Baja/Media/Alta): ");
                        string intensidad = Console.ReadLine();
                        ejercicioManager.AddExercise(tipo, duracion, intensidad);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ejercicio agregado exitosamente.");
                        Console.ResetColor();
                        break;
                    case "2":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n=== Lista de Ejercicios ===");
                        Console.ResetColor();
                        ejercicioManager.ListExercises();
                        break;
                    case "3":
                        Console.Write("Establece la Frecuencia (veces/semana): ");
                        int frecuencia = int.Parse(Console.ReadLine());
                        Console.Write("Establece la Duración (minutos/semana): ");
                        int duracionMeta = int.Parse(Console.ReadLine());
                        metaManager.SetGoal(frecuencia, duracionMeta);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Meta establecida exitosamente.");
                        Console.ResetColor();
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n=== Lista de Metas ===");
                        Console.ResetColor();
                        metaManager.ListGoals();
                        break;
                    case "5":
                        ejercicioManager.DeleteAllExercises();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Todos los ejercicios han sido eliminados.");
                        Console.ResetColor();
                        break;
                    case "6":
                        metaManager.DeleteAllGoals();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Todas las metas han sido eliminadas.");
                        Console.ResetColor();
                        break;
                    case "7":
                        Console.Write("Ingresa el ID del Ejercicio a Modificar: ");
                        int idEjercicio = int.Parse(Console.ReadLine());
                        Console.Write("Nuevo Tipo de Ejercicio: ");
                        string nuevoTipo = Console.ReadLine();
                        Console.Write("Nueva Duración (minutos): ");
                        int nuevaDuracion = int.Parse(Console.ReadLine());
                        Console.Write("Nueva Intensidad (Baja/Media/Alta): ");
                        string nuevaIntensidad = Console.ReadLine();
                        ejercicioManager.UpdateExercise(idEjercicio, nuevoTipo, nuevaDuracion, nuevaIntensidad);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("El ejercicio ha sido actualizado.");
                        Console.ResetColor();
                        break;
                    case "8":
                        Console.Write("Ingresa el ID de la Meta a Modificar: ");
                        int idMeta = int.Parse(Console.ReadLine());
                        Console.Write("Nueva Frecuencia (veces/semana): ");
                        int nuevaFrecuencia = int.Parse(Console.ReadLine());
                        Console.Write("Nueva Duración (minutos/semana): ");
                        int nuevaDuracionMeta = int.Parse(Console.ReadLine());
                        metaManager.UpdateGoal(idMeta, nuevaFrecuencia, nuevaDuracionMeta);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("La meta ha sido actualizada.");
                        Console.ResetColor();
                        break;
                    case "9":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Gracias por usar la Aplicación de Seguimiento de Ejercicio.");
                        Console.WriteLine("¡Hasta luego!");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida. Por favor, intenta de nuevo.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}




