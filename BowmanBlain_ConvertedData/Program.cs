using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Timers;
using System.Threading;

namespace BowmanBlain_ConvertedData
{
    enum mast
    {
        Spades, Clubs, Diamonds, Hearts
    }
    enum number
    {
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }

    class Program
    {
        public static List<string> names = new List<string>();
        static string _directory = @"../../../output/"; //directory to create file
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode; //changing encoding to unicode
            MainMenu();

        }
        public static void Json()
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

        public static void AnimatedBarGraph()
        {

            bool running = true;
            string input = "";
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Hello <user>, How would you like to sort the data:");
                Console.WriteLine("1.Show Average of Reviews for Restaurants");
                Console.WriteLine("2.Dinner Spinner (Selects Random Restaurant)");
                Console.WriteLine("3.Top 10 Restaurants");
                Console.WriteLine("4.Back To Main Menu");
                int number;
                do
                {
                    Console.Write("Choose an action:  ");
                    input = Console.ReadLine().ToLower();

                }
                while (!(int.TryParse(input, out number) && (number >= 1 && number <= 4)) && !(input.ToString().ToLower() == "show average of reviews for restaurants") && !(input.ToString().ToLower() == "dinner spinner (selects random restaurant)") && !(input.ToString().ToLower() == "top 10 restaurants") && !(input.ToString().ToLower() == "back to main menu"));

                Console.WriteLine();

                switch (input)
                {

                    case "1":
                    case "show average of reviews for restaurants":
                        {
                            //select to database   
                            string select = "SELECT DISTINCT (t2.restaurantname) FROM restaurantreviews as t1 inner join restaurantprofiles as t2 on t1.restaurantid =t2.id order by t2.restaurantname";
                            Select1(select);
                            Select3();
                            Animat();
                        }
                        break;
                    case "2":
                    case "dinner spinner (selects random restaurant)":
                        {
                            //select to database   
                            string select = "SELECT DISTINCT (t2.restaurantname) FROM restaurantreviews as t1 inner join restaurantprofiles as t2 on t1.restaurantid =t2.id order by t2.restaurantname";
                            Select1(select);

                            //We determine the length of the names list. We generate a random number in terms of the length of the list.Assign a random number to the variable index. 
                            //We determine the value of an element from the list of names by index. Save the value from the list to a variable.We clear the current list of names.
                            //We form a new list of names and add the value from the variable to it.
                            var random = new Random();
                            int index = random.Next(names.Count);
                            string namesi = names[index];
                            names.Clear();
                            names.Add(namesi);
                            Select3();
                            Animat();
                        }
                        break;
                    case "3":
                    case "top 10 restaurants":
                        {
                            //select to database   
                            string select = "SELECT t2.restaurantname FROM restaurantreviews as t1 inner join restaurantprofiles as t2 on t1.restaurantid =t2.id group by t2.restaurantname  order by AVG(reviewScore) DESC limit 10";
                            Select1(select);
                            Select3();
                            Animat();
                        }
                        break;
                    case "4":
                    case "back to main menu":
                        {
                            running = false;
                        }
                        break;
                    default:
                        return;

                }
                Console.WriteLine("");
                Console.WriteLine("Press The Space Bar To See More Restaurant Reviews...");
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
                            Json(); //"1.0 Data Visualization Practice"
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
                            AnimatedBarGraph(); //"3.0  Data Visualization Practice"
                        }
                        break;
                    case "4":
                    case "play a card game":
                        {
                            Console.WriteLine("• The game consists on one deck of cards, minus the jokers...52 cards.");
                            Console.WriteLine("• It is a 4 player game.Each time the game is loaded, it will pull RANDOM four players from the database of restaurant reviewers.");
                            Console.WriteLine("• The cards will be shuffled and handed out, one player at a time...13 cards to each player.");
                            Console.WriteLine("• Once the entire deck has been handed out, all card values will be tallied up to see the winners of the game...");
                            Console.WriteLine(" ");
                            Console.WriteLine("Press Enter key to show next player's cards ...");
                            Console.WriteLine(" ");
                            Game();//"4.0  Data Visualization Practice"
                            Console.WriteLine("Press The Space Bar To Play The Game Again or any key to back...");
                            ConsoleKeyInfo cons = Console.ReadKey();
                            if (cons.Key == ConsoleKey.Spacebar)
                            {
                                Game();
                            }
                            else break;
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

        private static void Select(string select)
        {
            DataBase db1 = new DataBase();    //class connect to database instance creation 
            db1.OpenConnection(); //connect to database
            MySqlDataReader reader = db1.DataReader(select); //select to database

            while (reader.Read()) //reading data from database
            {
                int point = Decimal.ToInt32(Math.Round(decimal.Parse(reader[1].ToString()), MidpointRounding.AwayFromZero)); //round
                Stars s = new Stars(); //class instance creation
                Console.Write(reader[0].ToString() + ": "); //to format the information in an organized way, and to look more like a table
                // Move the cursor back to the left, where it started to draw the animation to begin with.
                // Once we redraw, and redraw, and redraw, it will look animated.
                // We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
                Console.CursorLeft = 40;
                Console.Write("*****");
                Console.ForegroundColor = s.Color(point); // star colors
                // Move the cursor back to the left, where it started to draw the animation to begin with.
                // Once we redraw, and redraw, and redraw, it will look animated.
                // We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
                Console.CursorLeft = 40;
                Console.Write(s.Star(point)); //to format the information in an organized way, and to look more like a table
                Console.ResetColor(); // reset color
                Console.WriteLine("");

            }

            reader.Close();
            db1.CloseConnection();
        }

        private static void Select1(string select)
        {

            DataBase db1 = new DataBase();    //class connect to database instance creation 
            db1.OpenConnection(); //connect to database
            MySqlDataReader reader = db1.DataReader(select); //select to database

            while (reader.Read()) //reading data from database
            {
                //We write the data received from the database into the names list.
                string name = reader[0].ToString();
                names.Add(name.ToString());


            }
            reader.Close();
            db1.CloseConnection();
        }
        private static void Select3()
        {
            //If directory graphs are not empty, then clear it before writing.
            if (Graphs.graphs.Count > 0)
                Graphs.graphs.Clear();
            //For each value from the names list, we form a query into the database.
            //The resulting values ​​are stored in the list of balls.
            for (int i = 0; i < names.Count; i++)
            {
                DataBase db1 = new DataBase();    //class connect to database instance creation 
                db1.OpenConnection(); //connect to database
                string select1 = "SELECT    IFNULL(round(t1.reviewScore/10,0),0) FROM restaurantreviews as t1 inner join restaurantprofiles as t2 on t1.restaurantid =t2.id where t2.restaurantname=\"" + names[i] + "\"order by t2.restaurantname;"; //select to database
                MySqlDataReader reader = db1.DataReader(select1);
                //Console.Write(names[i]);
                List<int> balls = new List<int>();
                while (reader.Read()) //reading data from database
                {
                    int ball = int.Parse(reader[0].ToString());
                    balls.Add(ball);
                }
                //Save data in dictonary graphs. Key = restaurant name from the names list. 
                //Value = list of balls with rating values ​​from the base.
                Graphs.graphs.Add(names[i].ToString(), balls);

                reader.Close();
                db1.CloseConnection();
            }
            names.Clear();

        }
        private static void Animat()
        {
            ReaderWriterLock rw = new ReaderWriterLock();

            Console.WriteLine("Press any key after animation to continue...");
            Console.WriteLine(" ");
            foreach (var key in Graphs.graphs)
            {
                //display the key value on the screen
                Console.Write(key.Key);
                Console.Write(" ");

                //We determine the sum of all rating values ​​for key = restaurant
                int a = 0;
                decimal b = 0;
                for (int i = 0; i < 100; i++)
                {
                    a = key.Value[i];
                    b += a;

                }
                Console.Write(" ");

                //we find the average rating for the restaurant
                Animation.mid = Decimal.ToInt32(Math.Round(b / 100, MidpointRounding.AwayFromZero));
                Console.CursorLeft = 60;
                Console.Write("Average rating: " + Animation.mid);
                Animation.res_name = key.Key;
                //Create Timer
                Animation.SetTimer();

                //If you like, hide the cursor so it does not get in the way of the graph, but be careful...it's invisible so do not make the user do something where they need the curso after you make it invisible.
                Console.CursorVisible = true;

                //This is needed for the timer to work
                //The code needs to stop running, or wait for a response in order to play the animation
                //Thread.Sleep faster animation = 1000/second
                Thread.Sleep(10000);
                Console.ReadLine();
                Animation.myAnimationTimer.Stop();

                Console.ResetColor();


            }

        }
        private static void Game()
        {
            Game a = new Game();  //class instance creation 
            a.getcards();  //void cards shuffled 
            a.game(); //start the game

        }
    }

}
