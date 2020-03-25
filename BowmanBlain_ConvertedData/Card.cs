using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowmanBlain_ConvertedData
{
    class Card
    {

        #region
        static Dictionary<mast, char> mastdictionary = new Dictionary<mast, char>() //cards suits
        {
        { mast.Spades, '\u2660'},
        { mast.Clubs,'\u2663'},
        { mast.Diamonds,'\u2666'},
        { mast.Hearts, '\u2665'}
        };
        public static Dictionary<number, int> numberdictionary = new Dictionary<number, int>() //cards with a ranks and values
        {

        {number.Two, 2},
        {number.Three, 3},
        {number.Four, 4},
        {number.Five, 5},
        {number.Six, 6},
        {number.Seven, 7},
        {number.Eight, 8},
        {number.Nine, 9},
        {number.Ten, 10},
        {number.Jack, 12},
        {number.Queen, 12},
        {number.King, 12},
        {number.Ace, 15}
        };
        #endregion
        private mast mast;
        private number number;

        public mast Mast //get cards suit
        {
            get { return mast; }
        }

        public number Number //get cards value 
        {
            get { return number; }
        }

        public Card(mast Mast, number number)
        {
            this.mast = Mast;
            this.number = number;
        }

        public string realwhide() //return result in the format
        {
            return realwhide3() + mastdictionary[Mast];
        }
        public string realwhide1() //return result in the format
        {
            return numberdictionary[Number].ToString();
        }
        public char realwhide2() //return result in the format
        {
            return mastdictionary[Mast];

        }
        public string realwhide3() //return result in the format
        {
            string key = "";
            var myKey = numberdictionary.FirstOrDefault(x => x.Value == numberdictionary[number]).Key;
            switch (myKey)
            {
                case number.Two:
                    {
                        key = "2";
                    }
                    break;
                case number.Three:
                    {
                        key = "3";
                    }
                    break;
                case number.Four:
                    {
                        key = "4";
                    }
                    break;
                case number.Five:
                    {
                        key = "5";
                    }
                    break;
                case number.Six:
                    {
                        key = "6";
                    }
                    break;
                case number.Seven:
                    {
                        key = "7";
                    }
                    break;
                case number.Eight:
                    {
                        key = "8";
                    }
                    break;
                case number.Nine:
                    {
                        key = "9";
                    }
                    break;
                case number.Ten:
                    {
                        key = "10";
                    }
                    break;
                case number.Jack:
                    {
                        key = "J";
                    }
                    break;

                case number.Queen:
                    {
                        key = "Q";
                    }
                    break;

                case number.King:
                    {
                        key = "K";
                    }
                    break;

                case number.Ace:
                    {
                        key = "A";
                    }
                    break;

            }
            return key;
        }
        public string realwhide4() //return result in the format
        {
            return "Total: " + realwhide3() + " Mast:" + mastdictionary[Mast];
        }
    }
}
