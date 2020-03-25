using System;
using System.Collections.Generic;
using System.Text;

namespace BowmanBlain_ConvertedData
{
    class Deck
    {
        static Random n = new Random(); //value to shuffle cadrs
        private Card[] kalod = new Card[52];  //deck of cards
        public Deck()
        {
            for (mast mast = mast.Spades; mast <= mast.Hearts; mast++) // for all cards suits
            {
                for (number number = number.Two; number <= number.Ace; number++) //get all rank for each suit
                {
                    deck[((int)mast) * 13 + (int)number] = new Card(mast, number); //get deck with 52 cards
                }
            }
        }
        public void shuffle() //shuffle cadrs
        {

            for (int i = 0; i < 1000; i++)
            {
                int k = n.Next(0, 51); //Random number
                int k1 = n.Next(0, 51); //Random number
                Card d;
                d = deck[k];
                deck[k] = deck[k1];
                deck[k1] = d;
            }
        }
    }
}
