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
    }
}
