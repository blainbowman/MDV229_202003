using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace BlainBowman_TimeTrackerApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Menu.Login();
            
            Menu.MainMenu(Menu.name);
            Console.ReadKey();
        }
    }
}
