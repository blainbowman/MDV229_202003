using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlainBowman_TimeTrackerApp
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
        public static int GetInt(string message = "Enter an Integer: ")
        {
            int validatedInt;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (!Int32.TryParse(input, out validatedInt));

            return validatedInt;
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

        public static bool GetBool(string message = "Enter Yes or No: ")
        {
            bool answer = false;
            string input = null;

            bool needAValidREspone = true; ;

            while (needAValidREspone)
            {
                Console.WriteLine(message);
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "yes":
                    case "y":
                    case "true":
                    case "t":
                        {
                            answer = true;
                            needAValidREspone = false;

                        }
                        break;

                    case "no":
                    case "n":
                    case "false":
                    case "f":
                        {
                            needAValidREspone = false;
                        }
                        break;
                }
            }

            return answer;
        }

        public static double GetDouble(string message = "Enter an number: ")
        {
            double validatedDouble;
            string input = null;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (!Double.TryParse(input, out validatedDouble));

            return validatedDouble;
        }

        public static double GetDouble(int min, int max, string message = "Enter a number: ")
        {
            double validatedDouble;
            string input = null;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (!(Double.TryParse(input, out validatedDouble) && (validatedDouble >= min && validatedDouble <= max)));


            return validatedDouble;
        }
    }
}
