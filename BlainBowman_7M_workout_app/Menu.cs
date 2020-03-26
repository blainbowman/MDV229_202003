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
    }
}
