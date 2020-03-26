using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace BlainBowman_7M_workout_app
{
    class Validation
    {
        public static string ValidateString(string s)
        {
            Console.Write(s);
            string response = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(response))
            {
                Console.Write("\rPlease do not leave this empty. Try again: ");
                response = Console.ReadLine();
            }
            return response;
        }

        public static int GetInt(int min, int max, string message = "Enter an integer: ")
        {
            int validatedInt;
            string input = null;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
                if (!(Int32.TryParse(input, out validatedInt))) { Console.WriteLine("Not an Integer. Try again..."); continue; }
                if (!(validatedInt >= min && validatedInt <= max)) { Console.WriteLine("Out of Range ({0}-{1}). Try again...", min, max); }
            } while (!(Int32.TryParse(input, out validatedInt) && (validatedInt >= min && validatedInt <= max)));


            return validatedInt;
        }
        public static string ValidateUsername(string message, Database dbCon)
        {
            List<string> usernames = new List<string>();
            if (dbCon.IsConnect())
            {
                string query = "SELECT username FROM users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    usernames.Add(reader.GetString(0));
                }
                reader.Close();
            }
            Console.Write(message);
            string response = Console.ReadLine();
            while (usernames.Contains(response))
            {
                Console.Write("\rUsername is already taken. Try again or type \"Exit\": ");
                response = Console.ReadLine();
                if (response.ToUpper() == "EXIT") { Menu.MainMenu(); }
            }
            return response;
        }
        public static string ValidateEmail(string message, Database dbCon)
        {
            List<string> emails = new List<string>();
            if (dbCon.IsConnect())
            {
                string query = "SELECT email FROM users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    emails.Add(reader.GetString(0));
                }
                reader.Close();
            }
            Console.Write(message);
            string response = Console.ReadLine();
            while (emails.Contains(response))
            {
                Console.Write("\rEmail is already connected to another account. Try again or type \"Exit\": ");
                response = Console.ReadLine();
                if (response.ToUpper() == "EXIT") { Menu.MainMenu(); }
            }
            return response;
        }
        public static string ValidatePassword(string message, Database dbCon, int user_id)
        {
            string actualPassword = "";
            if (dbCon.IsConnect())
            {
                string query = "SELECT user_id, user_password FROM users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader.GetString(0)) == user_id) { actualPassword = reader.GetString(1); }
                }
                reader.Close();
            }
            Console.Write(message);
            string response = Console.ReadLine();
            while (response != actualPassword)
            {
                Console.Write("\rThe password is incorrect. Try again or type \"Exit\": ");
                response = Console.ReadLine();
                if (response.ToUpper() == "EXIT") { Menu.SignUpAndLogInMenu(); }
            }
            return response;
        }
        public static string ValidateUsernameOrEmail(string message, Database dbCon)
        {
            Dictionary<string, string> userData = new Dictionary<string, string>();
            List<string> names = new List<string>();
            if (dbCon.IsConnect())
            {
                string query = "SELECT name, username, email FROM users";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    names.Add(reader.GetString(0));
                    userData.Add(reader.GetString(1), reader.GetString(2));
                }
                reader.Close();
            }
            Console.Write(message);
            string response = Console.ReadLine();
            while (!userData.ContainsKey(response) && !userData.ContainsValue(response))
            {
                Console.Write("\rThere is no such email or username in the system. Try again or type \"Exit\": ");
                response = Console.ReadLine();
                if (response.ToUpper() == "EXIT") { Menu.MainMenu(); }
            }
            int count = 0;
            Console.Clear();
            foreach (KeyValuePair<string, string> pair in userData)
            {
                if (pair.Key == response)
                {
                    Console.WriteLine("Hello {0}!", names[count]);
                }
                if (pair.Value == response)
                {
                    Console.WriteLine("Hello {0}!", names[count]);
                    response = pair.Key;
                }
                count++;
            }
            return response;
        }
    }
}
