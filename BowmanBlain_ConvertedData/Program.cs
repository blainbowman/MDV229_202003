using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace BowmanBlain_ConvertedData
{
    class Program
    {
        static string _directory = @"../../../output/"; //directory to create file
        static void Main(string[] args)
        {
            MainMenu();
        }
        public static void SqlToJson()
        {

            Restaurants_DB rest = new Restaurants_DB(); //class instance creation 
            rest.restaurantprofiles = new List<Restaurantprofiles>(); //create sheet instance
            DataBase db1 = new DataBase();  //class instance creation 
            db1.OpenConnection();              //connect to database
            MySqlDataReader reader = db1.DataReader("SELECT RestaurantName, IFNULL(Address, 0), IFNULL(Phone, 0), IFNULL(HoursOfOperation, 0), IFNULL(Price, 0), USACityLocation, Cuisine,  IFNULL(FoodRating, 0), IFNULL(ServiceRating, 0), IFNULL(AmbienceRating, 0), IFNULL(ValueRating, 0), IFNULL(OverallRating, 0), IFNULL(OverallPossibleRating, 0) FROM restaurantprofiles;");
            while (reader.Read())              //taking data from database
            {

                Restaurantprofiles restaurants_list = new Restaurantprofiles(); //class instance creation 

                restaurants_list.Name = reader.GetString(0);
                restaurants_list.Address = reader.GetString(1);
                restaurants_list.Phone = reader.GetString(2);
                restaurants_list.Time = reader.GetString(3);
                restaurants_list.Price = reader.GetString(4);
                restaurants_list.Location = reader.GetString(5);
                restaurants_list.Cuisine = reader.GetString(6);
                restaurants_list.FoodRating = float.Parse(reader.GetString(7));
                restaurants_list.ServiceRating = float.Parse(reader.GetString(8));
                restaurants_list.AmbienceRating = float.Parse(reader.GetString(9));
                restaurants_list.ValueRating = float.Parse(reader.GetString(10));
                restaurants_list.OverallRating = float.Parse(reader.GetString(11));
                restaurants_list.OverallPossible = float.Parse(reader.GetString(12));
                rest.restaurantprofiles.Add(restaurants_list);
            }

            reader.Close(); //close reading
            db1.CloseConnection();                    //close connect to database
            string serialized = JsonConvert.SerializeObject(rest, Formatting.Indented); //serialize data to json
            string a = serialized;
            if (!Directory.Exists(_directory)) Directory.CreateDirectory(_directory);   //check and create folder

            string writePath = _directory + "BowmanBlain_ConvertedDate.json";               //route to file
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))           //writing into the file
            {
                sw.WriteLine(a);
            }
            Console.WriteLine("File has created in " + _directory + "BowmanBlain_ConvertedDate.json");            //print info about saving file
        }
        public static void Rating()
        {

            bool running = true;
            string input = "";
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Hello <user>, How would you like to sort the data:");
                Console.WriteLine("1.List Restaurants Alphabetically(Show Rating Next To Name)");
                Console.WriteLine("2.List Restaurants in Reverse Alphabetical(Show Rating Next To Name)");
                Console.WriteLine("3.Sort Restaurants From Best to Worst(Show Rating Next To Name)");
                Console.WriteLine("4.Sort Restaurants From Worst to Best(Show Rating Next To Name)");
                Console.WriteLine("5.Show Only X and Up");
                Console.WriteLine("6.Exit");
                int number;
                do
                {
                    Console.Write("Choose an action:  ");
                    input = Console.ReadLine().ToLower();

                }
                while (!(int.TryParse(input, out number) && (number >= 1 && number <= 6)) && !(input.ToString().ToLower() == "list restaurants alphabetically") && !(input.ToString().ToLower() == "list restaurants in reverse alphabetical") && !(input.ToString().ToLower() == "sort restaurants from best to worst") && !(input.ToString().ToLower() == "sort restaurants from worst to best") && !(input.ToString().ToLower() == "show only x and up") && !(input.ToString().ToLower() == "exit"));

                Console.WriteLine();

                switch (input)
                {

                    case "1":
                    case "list restaurants alphabetically":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles order by RestaurantName;"; //select to database    
                            Select(select);
                        }
                        break;
                    case "2":
                    case "list restaurants in reverse alphabetical":
                        {
                            string select = "SELECT RestaurantName,  IFNULL(OverallRating, 0) FROM restaurantprofiles order by RestaurantName DESC;"; //select to database    
                            Select(select);
                        }
                        break;
                    case "3":
                    case "sort restaurants from best to worst":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles order by OverallRating DESC;";//select to database    
                            Select(select);
                        }
                        break;
                    case "4":
                    case "sort restaurants from worst to best":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles order by OverallRating ASC;";//select to database    
                            Select(select);
                        }
                        break;
                    case "5":
                    case "show only x and up":
                        {
                            SubMenu();
                        }
                        break;
                    case "6":
                    case "exit":
                        {
                            running = false;
                        }
                        break;

                    default:
                        return;

                }
                Console.WriteLine("");
                Console.WriteLine("Press The Space Bar To Resort The Data, Go Back To The Menu, etc...");
                ConsoleKeyInfo cons = Console.ReadKey();
                while (cons.Key != ConsoleKey.Spacebar)  //space bar check
                {
                    cons = Console.ReadKey();
                }
                Console.WriteLine("");
            }

        }
        private static void MainMenu()
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
                while (!(int.TryParse(input, out number) && (number >= 1 && number <= 5)) && !(input.ToString().ToLower() == "convert") && !(input.ToString().ToLower() == "showcase our 5 star rating system") && !(input.ToString().ToLower() == "showcase our animated bar graph review system") && !(input.ToString().ToLower() == "play a card game") && !(input.ToString().ToLower() == "exit"));

                Console.WriteLine();

                switch (input)
                {

                    case "1":
                    case "convert":
                        {

                            SqlToJson(); //"1.0 Data Visualization Practice"
                        }
                        break;
                    case "2":
                    case "showcase our 5 star rating system":
                        {
                            Rating(); //"2.0 Data Visualization Practice"
                        }
                        break;
                    case "3":
                    case "showcase our animated bar graph review system":
                        {
                            Console.WriteLine("3.0  Data Visualization Practice");

                        }
                        break;
                    case "4":
                    case "play a card game":
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
        private static void SubMenu()
        {

            bool running = true;
            string input = "";
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Sub - Menu");
                Console.WriteLine("1.Show the Best(5 Stars)");
                Console.WriteLine("2.Show 4 Stars and Up");
                Console.WriteLine("3.Show 3 Stars and Up");
                Console.WriteLine("4.Show the Worst(1 Stars)");
                Console.WriteLine("5.Show Unrated");
                Console.WriteLine("6.Back");
                int number;
                do
                {
                    Console.Write("Choose an action:  ");
                    input = Console.ReadLine().ToLower();

                }
                while (!(int.TryParse(input, out number) && (number >= 1 && number <= 6)) && !(input.ToString().ToLower() == "show the best") && !(input.ToString().ToLower() == "show 4 stars and up") && !(input.ToString().ToLower() == "show 3 stars and up") && !(input.ToString().ToLower() == "show the worst") && !(input.ToString().ToLower() == "show unrated") && !(input.ToString().ToLower() == "back"));

                Console.WriteLine();

                switch (input)
                {
                    case "1":
                    case "show the best":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles where OverallRating>=4.5;";//select to database    
                            Select(select);
                        }
                        break;

                    case "2":
                    case "show 4 stars and up":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles where OverallRating>=3.5;";//select to database    
                            Select(select);
                        }
                        break;

                    case "3":
                    case "show 3 stars and up":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles where OverallRating>=2.5;";//select to database    
                            Select(select);
                        }
                        break;

                    case "4":
                    case "show the worst":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles where OverallRating>=0.5 and  OverallRating<1.5;";//select to database    
                            Select(select);
                        }
                        break;

                    case "5":
                    case "show unrated":
                        {
                            string select = "SELECT RestaurantName, IFNULL(OverallRating, 0) FROM restaurantprofiles where OverallRating<0.5 or OverallRating is NULL;";//select to database    
                            Select(select);
                        }
                        break;
                    case "6":
                    case "back":
                        {
                            running = false;
                        }
                        break;

                    default:
                        return;

                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}


