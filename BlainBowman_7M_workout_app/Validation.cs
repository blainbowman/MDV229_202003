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

    }
}
