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

    class DataBase
    {

        string connStr = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889"; //connection string
        MySqlConnection conn;
        public void OpenConnection()
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error:{0}", e.ToString());
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public MySqlDataReader DataReader(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }

    }
}
