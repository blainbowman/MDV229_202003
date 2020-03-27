using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace BlainBowman_TimeTrackerApp
{
    class Menu
    {
        
        public static string name;
        public static int id;
        static Database dbCon = Database.Instance();
        static void PickACategory()
        {
            Console.Clear();
            Console.WriteLine("Pick A Category Of Activity: ");
            int count = ShowCategories();
            Console.WriteLine("\t{0}. Back To Main Menu", count);
            int categoryId = Validation.GetInt(1, count, "Choose an option: ");
            if (categoryId == count) { MainMenu(name); }
            PickAnActivityDescription(categoryId);
        }
        static void PickAnActivityDescription(int categoryId)
        {
            Console.Clear();
            Console.WriteLine("Pick An Activity Description: ");
            int count = ShowDescription();
            Console.WriteLine("\t{0}. Back", count);
            int activityDescriptionId = Validation.GetInt(1, count, "Choose an option: ");
            if (activityDescriptionId == count) { PickACategory(); }
            PickADate(categoryId, activityDescriptionId);
        }
        static void PickADate(int categoryId, int activityDescriptionId)
        {
            Console.Clear();
            List<DateTime> dates = new List<DateTime>();
            Console.WriteLine("What Date Did You Perform Activity?");
            int count = 1;
            DateTime date = new DateTime();
            if (dbCon.IsConnect())
            {
                string query = "SELECT calendar_date_id, calendar_date FROM tracked_calendar_dates";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    date = Convert.ToDateTime(reader.GetString(1));
                    dates.Add(date);
                    Console.WriteLine("\t{0}. {1}-{2}-{3}.", count, date.Year, date.Month, date.Day);
                    count++;
                }
                reader.Close();
            }
            Console.WriteLine("\t{0}. Back", count);
            int dateNumber = Validation.GetInt(1, count, "Choose an option: ");
            var dayOfWeek = (int)dates[dateNumber-1].DayOfWeek;
            if (dayOfWeek == 0)
                dayOfWeek = 7;
            if (dateNumber == count) { PickAnActivityDescription(categoryId); }
            PickATime(categoryId, activityDescriptionId, dateNumber, dayOfWeek);

        }
        static void PickATime(int categoryId, int activityDescriptionId, int dateNumber, int dayOfWeek)
        {
            Console.Clear();
            Console.WriteLine("How Long Did You Perform That Activity?");
            int count = 1;
            if (dbCon.IsConnect())
            {
                string query = "SELECT activity_time_id, time_spent_on_activity FROM activity_times";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Console.WriteLine("\t{0}. {1}.", count, reader.GetString(1));
                    count++;
                }
                reader.Close();

            }
            Console.WriteLine("\t{0}. Back", count);
            int timeNumber = Validation.GetInt(1, count, "Choose an option: ");
            if (timeNumber == count) { PickADate(categoryId, activityDescriptionId); }
            SaveActivity(id, categoryId, activityDescriptionId, dateNumber, dayOfWeek, timeNumber);

        }
        static void SaveActivity(int id, int categoryId, int activityDescriptionId, int dateNumber, int dayOfWeek, int timeNumber)
        {
            Console.Clear();
            if (dbCon.IsConnect())
            {
                string query = "INSERT INTO activity_log(user_id, calendar_day, calendar_date, day_name, category_description, activity_description, time_spent_on_activity) VALUES(@user_id, @calendar_day, @calendar_date, @day_name, @category_description, @activity_description, @time_spent_on_activity)";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                cmd.Parameters.AddWithValue("@user_id", id);
                cmd.Parameters.AddWithValue("@calendar_day", dateNumber);
                cmd.Parameters.AddWithValue("@calendar_date", dateNumber);
                cmd.Parameters.AddWithValue("@day_name", dayOfWeek);
                cmd.Parameters.AddWithValue("@category_description", categoryId);
                cmd.Parameters.AddWithValue("@activity_description", activityDescriptionId);
                cmd.Parameters.AddWithValue("@time_spent_on_activity", timeNumber);
                cmd.ExecuteNonQuery();

            }
            Console.WriteLine("The activity was entered successfully. Press any key to continue...");
            Console.ReadKey();
            PromptForAnotherActivity();
        }
        static void PromptForAnotherActivity()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do next?");
            Console.WriteLine("\t1. Enter Another Activity");
            Console.WriteLine("\t2. Back To Main Menu");
            int userChoice = Validation.GetInt(1, 2, "Choose an option: ");
            if (userChoice == 1) { PickACategory(); }
            else { MainMenu(name); }
        }
        static void EnterActivity()
        {
            PickACategory();
        }
        static void ViewTrackedData()
        {
            Console.Clear();
            Console.WriteLine("View Tracked Data");
            Console.WriteLine("\t1. Select By Date");
            Console.WriteLine("\t2. Select By Category");
            Console.WriteLine("\t3. Select By Description");
            Console.WriteLine("\t4. Back");
            int userOptionNumber = Validation.GetInt(1, 4, "Choose an option: ");
            switch (userOptionNumber)
            {
                case 1:
                    SelectByDate();
                    break;
                case 2:
                    SelectByCategory();
                    break;
                case 3:
                    SelectByDescription();
                    break;
                case 4:
                    MainMenu(name);
                    break;

            }
        }
        static void SelectByDate()
        {
            Console.Clear();
            Console.WriteLine("Which Date Would You Like To View ?");
            List<DateTime> dates = new List<DateTime>();
            int count = 1;
            DateTime date = new DateTime();
            if (dbCon.IsConnect())
            {
                string query = "SELECT calendar_date_id, calendar_date FROM tracked_calendar_dates";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    date = Convert.ToDateTime(reader.GetString(1));
                    dates.Add(date);
                    Console.WriteLine("\t{0}. {1}-{2}-{3}.", count, date.Year, date.Month, date.Day);
                    count++;
                }
                reader.Close();
            }
            Console.WriteLine("\t{0}. Back", count);
            int dateNumber = Validation.GetInt(1, count, "Choose an option: ");
            var dayOfWeek = (int)dates[dateNumber - 1].DayOfWeek;
            if (dayOfWeek == 0)
                dayOfWeek = 7;
            if (dateNumber == count) {
                ViewTrackedData();
            }
            ViewAllActivitiesByDate(id, dateNumber, dayOfWeek);
        }
        static void SelectByCategory()
        {
            Console.Clear();
            Console.WriteLine("Which one would you like to view? ");
            int count = ShowCategories();
            Console.WriteLine("\t{0}. Back To Main Menu", count);
            int categoryId = Validation.GetInt(1, count, "Choose an option: ");
            if (categoryId == count) { ViewTrackedData(); }
            ViewAllActivitiesByCategory(id, categoryId);

        }
        static void SelectByDescription()
        {
            Console.Clear();
            Console.WriteLine("Which one would you like to view? ");
            int count = ShowDescription();
            Console.WriteLine("\t{0}. Back", count);
            int activityDescriptionId = Validation.GetInt(1, count, "Choose an option: ");
            if (activityDescriptionId == count) { ViewTrackedData(); }
            ViewAllActivitiesByDescription(id, activityDescriptionId);

        }

        static int ShowCategories()
        {
            int count = 1;
            if (dbCon.IsConnect())
            {
                string query = "SELECT activity_category_id, category_description FROM activity_categories";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}. {1}.", count, reader.GetString(1));
                    count++;
                }
                reader.Close();
            }
            return count;
        }
        static int ShowDescription()
        {
            int count = 1;
            if (dbCon.IsConnect())
            {
                string query = "SELECT activity_description_id, activity_description FROM activity_descriptions";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}. {1}.", count, reader.GetString(1));
                    count++;
                }
                reader.Close();
            }
            return count;
        }

        static void RunCalculations()
        {
            Console.Clear();
            Console.WriteLine("Look At All Of The Cool Data Collected Over 26 Days:");
            Console.WriteLine("\t1. Total Time Spent Working on Debugging: - {0} Hour(s)", TotalTime1(id) );
            Console.WriteLine("\t2. Total Time Spent on Reading/Planning : - {0} Hour(s)", TotalTime2(id));
            Console.WriteLine("\t3. Total Time Spent on Coding: - {0} Hour(s)", TotalTime3(id));
            Console.WriteLine("\t4. Total Time Spent on Reseach Apps: - {0} Hour(s)", TotalTime4(id));
            Console.WriteLine("\t5. Total Time Spent Doing '5 Star Rating': - {0} Hour(s)", TotalTime7(id));
            Console.WriteLine("\t6. Total Time Spent Doing 'Time Tracker': - {0} Hour(s)", TotalTime10(id));
            Console.WriteLine("\t7. Percentage of Time Spent Doing '5 Star Rating' vs 'Animated Bar Graphs': - {0}%", TotalTime5(id));
            Console.WriteLine("\t8. Percentage of Time Spent Custom App vs Work on Video: - {0}%", TotalTime6(id));
            Console.WriteLine("\t9. Percentage of Time Spent Read Course Material vs Finalize Everything/Turn in: - {0}%", TotalTime8(id));
            Console.WriteLine("\t10. Percentage of Time Spent Writing Script vs Research: - {0}%", TotalTime9(id));
            Console.WriteLine("");
            Console.WriteLine("Press the spacebar to return to main menu.");
            ConsoleKeyInfo cons = Console.ReadKey();
            while (cons.Key != ConsoleKey.Spacebar)  //space bar check
            {
                cons = Console.ReadKey();
            }
            Console.WriteLine("");
            MainMenu(name);
               

            
        }
        public static void MainMenu(string name)
        {
            Console.Clear();
            dbCon.DatabaseName = "blainbowman_mdv229_database_3";
            Console.WriteLine("Hello "+name+",");
            Console.WriteLine("What would you like to do today?");
            Console.WriteLine("\t1. Enter Activity");
            Console.WriteLine("\t2. View Tracked Data");
            Console.WriteLine("\t3. Run Calculations");
            Console.WriteLine("\t4. Exit");
            int userOptionNumber = Validation.GetInt(1, 4, "Choose an option: ");
            switch (userOptionNumber)
            {
                case 1:
                    EnterActivity();
                    break;
                case 2:
                    ViewTrackedData();

                    break;
                case 3:
                    RunCalculations();
                    break;
                case 4:
                    Environment.Exit(1);
                    break;
            }
            dbCon.Close();
        }
        public static void Login()
        {
            string login = "";
            string pass = "";
            Console.Write("Enter First Name: ");
            login = Console.ReadLine().ToLower();
            Console.Write("Enter password: ");
            pass = Console.ReadLine();
            dbCon.DatabaseName = "blainbowman_mdv229_database_3";
            
            if (dbCon.IsConnect())
            {
               
                string query = "SELECT user_firstname, user_id FROM time_tracker_users where user_password=\"" + pass + "\" and user_firstname=\"" + login + "\";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader[0].ToString();
                        id = int.Parse(reader[1].ToString());
                    }

                }
                else
                {
                    Console.WriteLine("Try again");
                    reader.Close();
                    Login();
                }
                reader.Close();
            }
            
        }
        public static void ViewAllActivitiesByDate(int id, int dateNumber, int dayOfWeek)
        {
            Console.Clear();
            int count = 1;
            Console.WriteLine("View All Activities By Date");
            List<string> rows = new List<string>();
            decimal tracked=0;
            decimal untracked;
            if (dbCon.IsConnect())
            {
                string query = "select t1.activity_description, IFNULL(t2.time_spent_on_activity, 0) from activity_descriptions as t1 left join(select activity_description, t5.time_spent_on_activity as time_spent_on_activity from activity_log inner join activity_times as t5 on activity_log.time_spent_on_activity=t5.activity_time_id where calendar_day = " + dateNumber + " and user_id = " + id + " ) as t2 on t1.activity_description_id = t2.activity_description;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    decimal track= decimal.Parse(reader.GetString(1));
                    tracked += track;
                    string a = count + ". " + reader.GetString(0) + ",  " + reader.GetString(1);
                    rows.Add(a);
                    count++;

                }
                reader.Close();

            }
            untracked = 24 - tracked;
            Console.WriteLine("\tTRACKED / USED: {0}, UNTRACKED / NOT: {1}", tracked, untracked);
            for (int iii = 0; iii < rows.Count; iii++)
            {
                Console.WriteLine("\t{0}", rows[iii]);
            }
            Console.WriteLine("\t{0}. Back", count);
            int userOptionNumber = Validation.GetInt(count, count, "Choose an option: ");
            AddAllActivitiesByDate(dateNumber, dayOfWeek);
            
        }

        public static void AddAllActivitiesByDate(int dateNumber, int dayOfWeek)
        {
            Console.Clear();
            Console.WriteLine("What do you want to do next?");
            Console.WriteLine("\t1. Enter Activity For This Day?");
            Console.WriteLine("\t2. Back To Main Menu");
            int userOptionNumber = Validation.GetInt(1, 2, "Choose an option: ");
            if (userOptionNumber == 1) { PickACategoryByDate(dateNumber, dayOfWeek); }
            else { MainMenu(name); }

        }
        public static void PickACategoryByDate(int dateNumber, int dayOfWeek)
        {

            Console.Clear();
            Console.WriteLine("Pick A Category Of Activity: ");
            int count = ShowCategories();
            Console.WriteLine("\t{0}. Back To Main Menu", count);
            int categoryId = Validation.GetInt(1, count, "Choose an option: ");
            if (categoryId == count) { MainMenu(name); }
            PickAnActivityDescriptionByDate(dateNumber, dayOfWeek, categoryId);
        }
        static void PickAnActivityDescriptionByDate(int dateNumber, int dayOfWeek, int categoryId)
        {
            Console.Clear();
            Console.WriteLine("Pick An Activity Description: ");
            int count = ShowDescription();
            Console.WriteLine("\t{0}. Back", count);
            int activityDescriptionId = Validation.GetInt(1, count, "Choose an option: ");
            if (activityDescriptionId == count) { PickACategory(); }
            PickATime(categoryId, activityDescriptionId, dateNumber, dayOfWeek);
        }

        public static void ViewAllActivitiesByCategory(int id, int categoryId)
        {
            Console.Clear();
            int count = 1;
            Console.WriteLine("View All Activities By Category");
            List<string> rows = new List<string>();
            decimal ii = 0;
            if (dbCon.IsConnect())
            {
                string query = "select t3.activity_description, round(IFNULL(t2.time_spent, 0), 2), t2.calendar_date from activity_categories as t1 inner join(select t2.category_description, t2.activity_description, SUM(t3.time_spent_on_activity) as time_spent, t1.calendar_date from activity_log as t2 inner join tracked_calendar_dates as t1 on t1.calendar_date_id = t2.calendar_date inner join activity_times as t3 on t3.activity_time_id = t2.time_spent_on_activity where t2.category_description = " + categoryId + "  and t2.user_id = " + id + " group by t1.calendar_date, t2.activity_description) as t2 on t1.activity_category_id = t2.category_description inner join activity_descriptions as t3 on t3.activity_description_id = t2.activity_description;";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = new DateTime();
                    date = Convert.ToDateTime(reader.GetString(2));
                    string a = count + ". " + reader.GetString(0) + ", Date: " + date.Year + "-" + date.Month + "-" + date.Day + ", X: " + reader.GetString(1);
                    rows.Add(a);
                    count++;
                    decimal i = decimal.Parse(reader.GetString(1));
                    ii += i;

                }
                reader.Close();
            }
            for (int iii = 0; iii <rows.Count; iii++)
            {
                Console.WriteLine("\t{0}, Y: {1}",  rows[iii], ii.ToString());
            }
            Console.WriteLine("\t{0}. Back", count);
            int userOptionNumber = Validation.GetInt(count, count, "Choose an option: ");
            ViewTrackedData();

        }
        public static void ViewAllActivitiesByDescription(int id, int activityDescriptionId)
        {
            Console.Clear();
            int count = 1;
            Console.WriteLine("View All Activities By Description");
            List<string> rows = new List<string>();
            decimal ii = 0;
            if (dbCon.IsConnect())
            {
                string query = "select t5.category_description,t3.calendar_date , t6.time_spent_on_activity from activity_log as t1 inner join activity_descriptions as t2 on t1.activity_description = t2.activity_description_id inner join tracked_calendar_dates as t3 on t1.calendar_date = t3.calendar_date_id inner join activity_times as t4 on t1.time_spent_on_activity = t4.activity_time_id inner join activity_categories as t5 on t1.category_description = t5.activity_category_id inner join activity_times as t6 on t6.activity_time_id = t1.time_spent_on_activity where t1.user_id = " + id + " and t1.activity_description = " + activityDescriptionId + ";";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = new DateTime();
                    date = Convert.ToDateTime(reader.GetString(1));
                    var dayOfWeek = (int)date.DayOfWeek;
                    string a = count + ". " + reader.GetString(0) + ", Date: " + date.Year + "-" + date.Month + "-" + date.Day+ ", X: "+ reader.GetString(2);
                    rows.Add(a);
                    decimal i = decimal.Parse(reader.GetString(2));
                    ii += i;
                    count++;
                }
                reader.Close();
            }
           for (int iii = 0; iii < rows.Count; iii++)
            {
                Console.WriteLine("\t{0}, Z: {1}", rows[iii], ii.ToString());
            }
            Console.WriteLine("\t{0}. Back", count);
            int userOptionNumber = Validation.GetInt(count, count, "Choose an option: ");
            ViewTrackedData();
        }

        public static decimal TotalTime(int id, string st)  
        {
            decimal total=0;
            if (dbCon.IsConnect())
            {
                string query = "select IFNULL(SUM(t4.time_spent_on_activity),0) from activity_log as t1 inner join activity_descriptions as t2 on t1.activity_description=t2.activity_description_id inner join tracked_calendar_dates as t3 on t1.calendar_date=t3.calendar_date_id inner join activity_times as t4 on t1.time_spent_on_activity=t4.activity_time_id inner join activity_categories as t5 on t1.category_description=t5.activity_category_id where t1.user_id=" + id + " and t5.category_description =\""+st+"\"; ";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total = decimal.Parse(reader.GetString(0));
                    }
                }
                else
                {
                    total = 0;
                }
                reader.Close();
            }
            return total;
        }

        public static decimal TotalTime01(int id, string st) 
        {
            decimal total = 0;
            if (dbCon.IsConnect())
            {
                string query = "select IFNULL(SUM(t4.time_spent_on_activity),0) from activity_log as t1 inner join activity_descriptions as t2 on t1.activity_description=t2.activity_description_id inner join tracked_calendar_dates as t3 on t1.calendar_date=t3.calendar_date_id inner join activity_times as t4 on t1.time_spent_on_activity=t4.activity_time_id inner join activity_categories as t5 on t1.category_description=t5.activity_category_id where t1.user_id=" + id + " and t2.activity_description =\"" + st + "\"; ";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total = decimal.Parse(reader.GetString(0));
                    }
                }
                else
                {
                    total = 0;
                }
                reader.Close();
            }
            return total;
        }
        public static decimal TotalTime1(int id)  
        {
            string st = "Debugging";
            return TotalTime(id, st); 
        }
        
        public static decimal TotalTime2(int id)  
        {
            string st = "Reading/Planning";
            return TotalTime(id, st);
        }
        public static decimal TotalTime3(int id)  
        {
            string st = "Coding";
            return TotalTime(id, st);
        }
        public static decimal TotalTime4(int id)  
        {
            string st = "Research Apps";
            return TotalTime(id, st);
        }

        
        public static decimal TotalTime5(int id)
        {
            decimal total = 0;
            decimal total1 = 0;
            decimal total2 = 0;
            total1 = TotalTime7(id);
            string st= "Animated Bar Graphs";
            total2 = TotalTime01(id, st);
            if (total1 <= 0)
            {
                total = 0;
            }
            else if (total2 <= 0)
            {
                total = 100;
            }
            else
            {
                total = Math.Round((total1 / total2) * 100, 2, MidpointRounding.AwayFromZero);
            }
            return total;
        }
        
        
        public static decimal TotalTime6(int id)
        {
            decimal total = 0;
            decimal total1 = 0;
            decimal total2 = 0;
            string st1 = "Custom App";
            total1 = TotalTime01(id,st1);
            string st2 = "Work on Video";
            total2 = TotalTime01(id, st2);
            if (total1 <= 0)
            {
                total = 0;
            }
            else if (total2 <= 0)
            {
                total = 100;
            }
            else
            {
                total = Math.Round((total1 / total2) * 100, 2, MidpointRounding.AwayFromZero);
            }
            return total;
        }
              
        public static decimal TotalTime7(int id) 
        {
            string st = "5 Star Rating";
            return TotalTime01(id, st);
        }
        
        public static decimal TotalTime8(int id)
        {
            decimal total = 0;
            decimal total1 = 0;
            decimal total2 = 0;
            string st1 = "Read Course Material";
            total1 = TotalTime01(id, st1);
            string st2 = "Finalize EverythingTurnin";
            total2 = TotalTime01(id, st2);
            if (total1 <= 0)
            {
                total = 0;
            }
            else if (total2 <= 0)
            {
                total = 100;
            }
            else
            {
                total = Math.Round((total1 / total2) * 100, 2, MidpointRounding.AwayFromZero);
            }
            return total;
        }
        
        public static decimal TotalTime9(int id)
        {
            decimal total = 0;
            decimal total1 = 0;
            decimal total2 = 0;
            string st1 = "Write Script";
            total1 = TotalTime01(id, st1);
            string st2 = "Research";
            total2 = TotalTime01(id, st2);
            if (total1 <= 0)
            {
                total = 0;
            }
            else if (total2 <= 0)
            {
                total = 100;
            }
            else
            {
                total = Math.Round((total1 / total2) * 100, 2, MidpointRounding.AwayFromZero);
            }
            return total;
        }
       
        public static decimal TotalTime10(int id)//+
        {
            decimal total = 0;
            string st1 = "Time Tracker EER & Database";
            total += TotalTime01(id, st1);
            string st2 = "Time Tracker Sprint 1";
            total += TotalTime01(id, st2);
            string st3 = "Time Tracker App";
            total  += TotalTime01(id, st3);
            string st4 = "Time Tracker Sprint 2";
            total += TotalTime01(id, st4);
            string st5 = "Time Tracker Sprint 3";
            total += TotalTime01(id, st5);
            string st6 = "Finish Time Tracker App";
            total += TotalTime01(id, st6);
            string st7 = "Time Tracker Project";
            total += TotalTime01(id, st7);
            return total;
        }
    }
}
