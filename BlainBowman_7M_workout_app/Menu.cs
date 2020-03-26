using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
namespace BlainBowman_7M_workout_app
{
    class Menu
    {
        //Database connection instance
        private static Database dbCon = Database.Instance();
        //Current user date
        private static string currentUser = null;
        private static int currentUserId = 0;
        public static void SignUpAndLogInMenu()
        {
            Console.Clear();
            dbCon.DatabaseName = "Blain_Bowman_7M_Workout_App5";

            //Sign up and log in menu

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
            //Pulling the total body exercise name and printing the output to the user
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
            //Pulling the lower body exercise name and printing the output to the user
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
            //Pulling the upper body exercise name and printing the output to the user
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
            //Pulling the core exercise name and printing the output to the user
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
            //Adding workout by prompting the user of 3 total, 3 lower, 3 upper body exercises and a 3 core exercises, like in the official 7M workout app
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
                    //Prompting the user of the workout name, number of cycles, rest interval
                    Console.Clear();
                    string workoutname = Validation.ValidateString("Name your workout: ");
                    int numberOfCycles = Validation.GetInt(1, 10, "Pick the number of cycles (1-10): ");
                    int restInterval = Validation.GetInt(1, 60, "Pick the rest interval (1-60) seconds: ");

                    if (dbCon.IsConnect())
                    {
                        //Pushing workout data into database
                        string query = "INSERT INTO workouts(user_id, name_of_the_workout, rest_interval, number_of_cycles) VALUES(@user_id, @name_of_the_workout, @rest_interval, @number_of_cycles)";
                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        cmd.Parameters.AddWithValue("@user_id", currentUserId);
                        cmd.Parameters.AddWithValue("@name_of_the_workout", workoutname);
                        cmd.Parameters.AddWithValue("@rest_interval", restInterval);
                        cmd.Parameters.AddWithValue("@number_of_cycles", numberOfCycles);
                        cmd.ExecuteNonQuery();

                        //Pushing workout exercises data into database
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
            //Pulling the total body exercise name by the given workout id
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
            //Pulling the lower body exercise name by the given workout id
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
            //Pulling the upper body exercise name by the given workout id
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
            //Pulling the core exercise name by the given workout id
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
        public static void ShowWorkoutExercises(int workout_id)
        {
            //Displaying a chosen workout data
            Console.Clear();
            List<int> exercises = new List<int>();
            if (dbCon.IsConnect())
            {
                string query = "SELECT workout_id, user_id, name_of_the_workout, rest_interval, number_of_cycles FROM workouts";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (workout_id.ToString() == reader.GetString(0) && currentUserId.ToString() == reader.GetString(1))
                    {
                        Console.WriteLine("User: {0}", currentUser);
                        Console.WriteLine("Name of the workout: {0}", reader.GetString(2));
                        Console.WriteLine("Rest interval: {0}", reader.GetString(3));
                        Console.WriteLine("Number of cycles: {0}", reader.GetString(4));
                    }
                }
                reader.Close();
                Console.WriteLine("Exercises: ");
                query = "SELECT * FROM custom_workout_exercises WHERE custom_workout_exercises_id=" + workout_id + ";";
                cmd = new MySqlCommand(query, dbCon.Connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    exercises.Add(Convert.ToInt32(reader.GetString(2)));
                    exercises.Add(Convert.ToInt32(reader.GetString(3)));
                    exercises.Add(Convert.ToInt32(reader.GetString(4)));
                    exercises.Add(Convert.ToInt32(reader.GetString(5)));
                    exercises.Add(Convert.ToInt32(reader.GetString(6)));
                    exercises.Add(Convert.ToInt32(reader.GetString(7)));
                    exercises.Add(Convert.ToInt32(reader.GetString(8)));
                    exercises.Add(Convert.ToInt32(reader.GetString(9)));
                    exercises.Add(Convert.ToInt32(reader.GetString(10)));
                    exercises.Add(Convert.ToInt32(reader.GetString(11)));
                    exercises.Add(Convert.ToInt32(reader.GetString(12)));
                    exercises.Add(Convert.ToInt32(reader.GetString(13)));


                }
                reader.Close();
                Console.WriteLine("  1.{0}", GetTotalBodyExerciseName(exercises[0]));
                Console.WriteLine("  2.{0}", GetTotalBodyExerciseName(exercises[1]));
                Console.WriteLine("  3.{0}", GetTotalBodyExerciseName(exercises[2]));
                Console.WriteLine("  4.{0}", GetLowerBodyExerciseName(exercises[3]));
                Console.WriteLine("  5.{0}", GetLowerBodyExerciseName(exercises[4]));
                Console.WriteLine("  6.{0}", GetLowerBodyExerciseName(exercises[5]));
                Console.WriteLine("  7.{0}", GetUpperBodyExerciseName(exercises[6]));
                Console.WriteLine("  8.{0}", GetUpperBodyExerciseName(exercises[7]));
                Console.WriteLine("  9.{0}", GetUpperBodyExerciseName(exercises[8]));
                Console.WriteLine("  10.{0}", GetUpperBodyExerciseName(exercises[9]));
                Console.WriteLine("  11.{0}", GetUpperBodyExerciseName(exercises[10]));
                Console.WriteLine("  12.{0}", GetUpperBodyExerciseName(exercises[11]));
            }
        }
        public static void ShowCustomWorkouts()
        {
            //Displaying the list of workout and the option to add a new workout
            Console.Clear();
            int count = 1;
            if (dbCon.IsConnect())
            {
                string query = "SELECT name_of_the_workout FROM workouts";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                Console.WriteLine("{0}'s Workouts: ", currentUser);
                while (reader.Read())
                {
                    Console.WriteLine("\t[{0}] {1}", count, reader.GetString(0));
                    count++;
                }
                reader.Close();

            }
            if (count == 0)
            {
                Console.Clear();
                Console.WriteLine("There is no available workouts. Would you like to add one ?");
                Console.WriteLine("\t[1] Yes");
                Console.WriteLine("\t[2] No");
                int addWorkoutOptionNumber = Validation.GetInt(1, 2, "Please, choose an option: ");
                switch (addWorkoutOptionNumber)
                {
                    case 1:
                        AddWorkout();
                        break;
                    case 2:
                        MainMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\t[{0}] Add a new workout.", count++);
                Console.WriteLine("\t[{0}] Back to the main menu.", count);
                int userOptionNumber = Validation.GetInt(1, count, "Please, choose an option: ");
                if (userOptionNumber == count - 1) { AddWorkout(); }
                if (userOptionNumber == count) { MainMenu(); }
                ShowWorkoutExercises(userOptionNumber);
                Console.WriteLine("Would you like to start the workout ?");
                Console.WriteLine("\t[1] Yes");
                Console.WriteLine("\t[2] No");
                int startWorkoutOptionNumber = Validation.GetInt(1, 2, "Please, choose an option: ");
                switch (startWorkoutOptionNumber)
                {
                    case 1:
                        StartWorkout();
                        break;
                    case 2:
                        MainMenu();
                        break;
                }
            }
        }
        public static void StartWorkout()
        {
            //There is going to be a method that implements workout timer here
            MainMenu();
        }
        public static void MainMenu()
        {
            //Main menu for the 7M workout app 
            Console.Clear();
            Console.WriteLine("What do you want to do? ");
            Console.WriteLine("\t[1] Create or start your custom workout.");
            // Console.WriteLine("\t[2] Start pre-selected workout.");           
            // Console.WriteLine("\t[3] Check your progress.");
            // Console.WriteLine("\t[4] Go to the settings.");
            Console.WriteLine("\t[2] Exit the program.");
            int userOptionNumber = Validation.GetInt(1, 5, "Please, choose an option: ");
            switch (userOptionNumber)
            {

                case 1:
                    ShowCustomWorkouts();
                    break;
                case 2:
                    dbCon.Close();
                    Environment.Exit(1);
                    break;
            }
            dbCon.Close();
        }
        static void GetUserId()
        {
            //Sets the current user id
            if (dbCon.IsConnect())
            {
                int count = 1;
                string query = "SELECT user_id, username FROM users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (currentUser == reader.GetString(1)) { currentUserId = count; }
                    count++;
                }
                reader.Close();
            }
        }
        static int GetWorkoutId(string workoutname)
        {
            //Returns the workout id using workout name
            int count = 1;
            if (dbCon.IsConnect())
            {

                string query = "SELECT workout_id, name_of_the_workout FROM workouts";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (workoutname == reader.GetString(1)) { reader.Close(); return count; }
                    count++;
                }
                reader.Close();
            }
            return count;
        }
        static void LogIn()
        {
            //Application log in
            Console.Clear();
            //Username and email validation are located in the validation class
            string username = Validation.ValidateUsernameOrEmail("Select your username or email: ", dbCon);
            currentUser = username;
            GetUserId();
            string password = Validation.ValidatePassword("Enter your password: ", dbCon, currentUserId);

        }
        public static void SignUp()
        {
            //Application sign up
            Console.Clear();
            string name = Validation.ValidateString("Enter your name: ");
            //Username validation is located in the validation class
            string username = Validation.ValidateUsername("Select your username: ", dbCon);
            //Email validation is located in the validation class
            string email = Validation.ValidateEmail("Enter your email : ", dbCon);
            string password = Validation.ValidateString("Select a password: ");
            if (dbCon.IsConnect())
            {
                string query = "INSERT INTO users(name, username, email, user_password) VALUES(@name, @username, @email, @user_password)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@user_password", password);
                cmd.ExecuteNonQuery();
                Console.WriteLine("The user was entered successfully. Welcome {0}!", name);
            }

            currentUser = username;
            GetUserId();
            Console.WriteLine("Type any key to continue...");
            Console.ReadKey();
        }
    }
}
