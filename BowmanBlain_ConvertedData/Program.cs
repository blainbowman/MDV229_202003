using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

namespace BowmanBlain_ConvertedData
{
    class Program
    {
        static List<string> restaurants = new List<string>();
        static void Main(string[] args)
        {
            bool running = true;
            string input = "";
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Hello Admin, What Would You Like To Do Today?");
                Console.WriteLine("1. Convert The Restaurant Profile Table From SQL To JSON");
                Console.WriteLine("2. Showcase Our 5 Star Rating System");
                Console.WriteLine("3. Showcase Our Animated Bar Graph Review System");
                Console.WriteLine("4. Play A Card Game");
                Console.WriteLine("5. Exit");
                int number;
                do
                {
                    Console.Write("Choose an action:  ");
                    input = Console.ReadLine().ToLower();

                }
                while (!(int.TryParse(input, out number) && (number >= 1 && number <= 5)) && !(input.ToString().ToLower() == "Convert") && !(input.ToString().ToLower() == "Showcase Our 5 Star Rating System") && !(input.ToString().ToLower() == "Showcase Our Animated Bar Graph Review System") && !(input.ToString().ToLower() == "Play A Card Game") && !(input.ToString().ToLower() == "exit"));

                switch (input)
                {

                    case "1":
                    case "Convert":
                        {

                        }
                        break;
                    case "2":
                    case "Showcase Our 5 Star Rating System":
                        {
                            Console.WriteLine("2.0 Data Visualization Practice");
                        }
                        break;
                    case "3":
                    case "Showcase Our Animated Bar Graph Review System":
                        {
                            Console.WriteLine("3.0  Data Visualization Practice");

                        }
                        break;
                    case "4":
                    case "Play A Card Game":
                        {
                            Console.WriteLine("4.0  Data Visualization Practice");
                        }
                        break;
                    case "5":
                    case "exit":
                        {

                            running = false;
                        }
                        break;

                    default:
                        return;

                }
                Console.WriteLine("Press The Return Key To Go Back To The Main Menu...");
                Console.ReadKey();
            }

        }
    }
}

