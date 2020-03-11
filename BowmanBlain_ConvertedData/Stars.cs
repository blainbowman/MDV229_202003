using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowmanBlain_ConvertedData
{
    class Stars
    {

        System.ConsoleColor color;
        public System.ConsoleColor Color(int point)
        {
            if (point >= 0 && point <= 2)
            {
                color = ConsoleColor.Red;
            }
            else
            {
                if (point == 3)
                {
                    color = ConsoleColor.Yellow;
                }
                else
                {
                    color = ConsoleColor.Green;
                }
            }
            return color;
        }
        public string Star(int point)
        {

            string b = "";
            switch (point)
            {
                case 0:
                    b = "NO RATING";
                    break;
                case 1:
                    b = "*";
                    break;
                case 2:
                    b = "**";
                    break;
                case 3:
                    b = "***";
                    break;
                case 4:
                    b = "****";
                    break;
                case 5:
                    b = "*****";
                    break;
                default:
                    break;
            }
            return b;
        }
    }

}
