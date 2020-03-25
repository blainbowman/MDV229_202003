using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowmanBlain_ConvertedData
{
    class User
    {
        public int player { get; set; } //number of player
        public string first_name { get; set; }
        public string last_name { get; set; }

        public int total { get; set; } //Score

        public override string ToString() //return result in the format
        {
            return "Player " + player + " - " + first_name + " " + last_name;
        }
        public string ToString1() //return result in the format
        {
            return "Score: " + total;
        }
    }

}
