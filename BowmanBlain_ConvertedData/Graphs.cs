using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BowmanBlain_ConvertedData
{

    class Graphs
    {

        //Creating a dictionary for graphs
        public static Dictionary<string, List<int>> graphs = new Dictionary<string, List<int>>();


        public System.ConsoleColor Color(int ball)
        {
            System.ConsoleColor color;
            //Setting color for greater then 7 to green
            if (ball >= 7)
            {

                color = ConsoleColor.Green;
            }
            else
            {
                //Setting color equal to 3 or less then to red
                if (ball <= 3)
                {
                    color = ConsoleColor.Red;
                }
                //Setting all other color to yellow
                else
                {
                    color = ConsoleColor.Yellow;
                }
            }
            return color;
        }
    }
}
