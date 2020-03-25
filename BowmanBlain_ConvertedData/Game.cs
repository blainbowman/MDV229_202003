using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading;

namespace BowmanBlain_ConvertedData
{
    class Game
    {
        public static List<Card>[] pld = new List<Card>[4]; //list cards handed out to players
        Deck d = new Deck(); //class instance creation 
        List<Card> pl1 = new List<Card>(); //list cards handed out to one player
        List<Card> pl2 = new List<Card>(); //list cards handed out to one player
        List<Card> pl3 = new List<Card>(); //list cards handed out to one player
        List<Card> pl4 = new List<Card>(); //list cards handed out to one player

        public void getcards() //cards handed out to players
        {

            d.shuffle(); //shuffle
            for (int i = 0; i < 13; i++) //cards handed out to one player at a time
            {
                pl1.Add(d.F[i]); //result list 
            }
            for (int i = 13; i < 26; i++) //cards handed out to one player at a time
            {
                pl2.Add(d.F[i]); //result list 
            }
            for (int i = 26; i < 39; i++) // cards handed out to one player at a time
            {
                pl3.Add(d.F[i]); //result list 
            }
            for (int i = 39; i < 52; i++) // cards handed out to one player at a time
            {
                pl4.Add(d.F[i]); //result list 
            }


        }

    }
}
