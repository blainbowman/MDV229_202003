using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BlainBowman_7M_workout_app
{
    class Menu
    {
        private static Database dbCon = Database.Instance();
        private static string currentUser = null;
        private static int currentUserId = 0;
        public static void SignUpAndLogInMenu()
        {
            Console.Clear();
            dbCon.DatabaseName = "Blain_Bowman_7M_Workout_App";
            Console.WriteLine("Welcome!!!");
            Console.WriteLine("Let's get started.");
            Console.WriteLine("What do you want to do? ");
            Console.WriteLine("\t[1] Log in");
            Console.WriteLine("\t[2] Sign up");
            Console.WriteLine("\t[3] Exit");
            int userOptionNumber = Validation.GetInt(1, 3, "Please, choose an option: ");
            switch (userOptionNumber)
            {
                case 1:
                    LogIn();
                    break;
                case 2:
                    SignUp();
                    break;
                case 3:
                    dbCon.Close();
                    Environment.Exit(1);
                    break;
            }
            MainMenu();
        }
        public static void ShowTotalBodyExercises()
        {
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_id, exercise_name FROM total_body_exercises";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t[{0}] {1}", reader.GetString(0), reader.GetString(1));
                }
                reader.Close();
            }
        }
        public static void ShowLowerBodyExercises()
        {
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_id, exercise_name FROM lower_body_exercises";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t[{0}] {1}", reader.GetString(0), reader.GetString(1));
                }
                reader.Close();
            }
        }
        public static void ShowUpperBodyExercises()
        {
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_id, exercise_name FROM upper_body_exercises";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t[{0}] {1}", reader.GetString(0), reader.GetString(1));
                }
                reader.Close();
            }
        }
        public static void ShowCoreExercises()
        {
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_id, exercise_name FROM core_exercises";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t[{0}] {1}", reader.GetString(0), reader.GetString(1));
                }
                reader.Close();
            }
        }
        public static void AddWorkout()
        {
            Console.Clear();
            Console.WriteLine("Pick 3 total body exercises (0/3): ");
            ShowTotalBodyExercises();
            int first_total_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 total body exercises (1/3): ");
            ShowTotalBodyExercises();
            int second_total_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 total body exercises (2/3): ");
            ShowTotalBodyExercises();
            int third_total_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 lower body exercises (0/3): ");
            ShowLowerBodyExercises();
            int first_lower_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 lower body exercises (1/3): ");
            ShowLowerBodyExercises();
            int second_lower_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 lower body exercises (2/3): ");
            ShowLowerBodyExercises();
            int third_lower_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 upper body exercises (0/3): ");
            ShowUpperBodyExercises();
            int first_upper_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 upper body exercises (1/3): ");
            ShowUpperBodyExercises();
            int second_upper_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 upper body exercises (2/3): ");
            ShowUpperBodyExercises();
            int third_upper_body_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 core exercises (0/3): ");
            ShowCoreExercises();
            int first_core_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 core exercises (1/3): ");
            ShowCoreExercises();
            int second_core_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Pick 3 core exercises (2/3): ");
            ShowCoreExercises();
            int third_core_exercise_number = Validation.GetInt(1, 15, "Please, choose an option: ");
            Console.Clear();
            Console.WriteLine("Confirm the workout order?");
            Console.WriteLine("\t[1] Yes");
            Console.WriteLine("\t[2] No");
            int userOptionNumber = Validation.GetInt(1, 2, "Please, choose an option: ");
            switch (userOptionNumber)
            {
                case 1:
                    Console.Clear();
                    string workoutname = Validation.ValidateString("Name your workout: ");
                    int numberOfCycles = Validation.GetInt(1, 10, "Pick the number of cycles (1-10): ");
                    int restInterval = Validation.GetInt(1, 60, "Pick the rest interval (1-60) seconds: ");

                    if (dbCon.IsConnect())
                    {
                        string query = "INSERT INTO workouts(user_id, name_of_the_workout, rest_interval, number_of_cycles) VALUES(@user_id, @name_of_the_workout, @rest_interval, @number_of_cycles)";
                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@user_id", currentUserId);
                        cmd.Parameters.AddWithValue("@name_of_the_workout", workoutname);
                        cmd.Parameters.AddWithValue("@rest_interval", restInterval);
                        cmd.Parameters.AddWithValue("@number_of_cycles", numberOfCycles);
                        cmd.ExecuteNonQuery();

                        query = "INSERT INTO custom_workout_exercises(workout_id, first_total_body_exercise, second_total_body_exercise, third_total_body_exercise, first_lower_body_exercise, second_lower_body_exercise, third_lower_body_exercise, first_upper_body_exercise, second_upper_body_exercise, third_upper_body_exercise, first_core_exercise, second_core_exercise, third_core_exercise) VALUES(@workout_id, @first_total_body_exercise, @second_total_body_exercise, @third_total_body_exercise, @first_lower_body_exercise, @second_lower_body_exercise, @third_lower_body_exercise, @first_upper_body_exercise, @second_upper_body_exercise, @third_upper_body_exercise, @first_core_exercise, @second_core_exercise, @third_core_exercise)";
                        cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@workout_id", GetWorkoutId(workoutname));
                        cmd.Parameters.AddWithValue("@first_total_body_exercise", first_total_body_exercise_number);
                        cmd.Parameters.AddWithValue("@second_total_body_exercise", second_total_body_exercise_number);
                        cmd.Parameters.AddWithValue("@third_total_body_exercise", third_total_body_exercise_number);
                        cmd.Parameters.AddWithValue("@first_lower_body_exercise", first_lower_body_exercise_number);
                        cmd.Parameters.AddWithValue("@second_lower_body_exercise", second_lower_body_exercise_number);
                        cmd.Parameters.AddWithValue("@third_lower_body_exercise", third_lower_body_exercise_number);
                        cmd.Parameters.AddWithValue("@first_upper_body_exercise", first_upper_body_exercise_number);
                        cmd.Parameters.AddWithValue("@second_upper_body_exercise", second_upper_body_exercise_number);
                        cmd.Parameters.AddWithValue("@third_upper_body_exercise", third_upper_body_exercise_number);
                        cmd.Parameters.AddWithValue("@first_core_exercise", first_core_exercise_number);
                        cmd.Parameters.AddWithValue("@second_core_exercise", second_core_exercise_number);
                        cmd.Parameters.AddWithValue("@third_core_exercise", third_core_exercise_number);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("The workout was added successfully.");
                        Console.WriteLine("Type any key to continue...");
                        Console.ReadKey();
                        ShowCustomWorkouts();
                    }

                    break;
                case 2:
                    MainMenu();
                    break;
            }
        }
        public static string GetTotalBodyExerciseName(int id)
        {
            string result = "";
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_name FROM total_body_exercises WHERE exercise_id=" + id + ";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var newReader = cmd.ExecuteReader();
                while (newReader.Read())
                {
                    result = newReader.GetString(0);
                }
                newReader.Close();

            }
            return result;
        }
        public static string GetLowerBodyExerciseName(int id)
        {
            string result = "";
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_name FROM lower_body_exercises WHERE exercise_id=" + id + ";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var newReader = cmd.ExecuteReader();
                while (newReader.Read())
                {
                    result = newReader.GetString(0);
                }
                newReader.Close();

            }
            return result;
        }
        public static string GetUpperBodyExerciseName(int id)
        {
            string result = "";
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_name FROM upper_body_exercises WHERE exercise_id=" + id + ";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var newReader = cmd.ExecuteReader();
                while (newReader.Read())
                {
                    result = newReader.GetString(0);
                }
                newReader.Close();

            }
            return result;
        }
        public static string GetCoreExerciseName(int id)
        {
            string result = "";
            if (dbCon.IsConnect())
            {
                string query = "SELECT exercise_name FROM core_exercises WHERE exercise_id=" + id + ";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var newReader = cmd.ExecuteReader();
                while (newReader.Read())
                {
                    result = newReader.GetString(0);
                }
                newReader.Close();

            }
            return result;
        }
    }
}
