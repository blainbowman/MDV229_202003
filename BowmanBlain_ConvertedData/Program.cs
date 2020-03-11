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
        static string _directory = @"../../../output/";
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
                            SqlToJson();
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
    }
}

