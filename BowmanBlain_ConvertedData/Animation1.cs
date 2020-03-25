using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace BowmanBlain_ConvertedData
{
    class Animation1
    {
        

        //Timer Variable
        //This creates the actual timer
        //Note this is outside of the Main()
        //Note that we added in "using System.Timers;" at the top
        public static System.Timers.Timer myAnimationTimer;
        //Timer Counter
        //This counter is used to stop the timer when we want it to
        //So, it helps to make the animation go longer/shorter, faster/slower
        //Note this is outside of the Main()
        private static int myTimerCounter = 0;

        //Counter for card
        private static int iiii = 0;

        //Counter for card
        private static int ii = 0;

        //Set Timer Properties
        public static void SetTimer()
        {
            //Set time to happen really fast 1,000 = 1 second
            //Start the function every 50/1000 seconds
            myAnimationTimer = new System.Timers.Timer(500);

            //At 50/1000, run this method "OnTimedEvent"
            //Every time it elapses, do it
            myAnimationTimer.Elapsed += OnTimedEvent;

            //Reset timer again after 50/1000, over and over again
            //False means to only run the timer one time
            myAnimationTimer.AutoReset = true;

            //The timer is enabled so it will work
            //False means the timer will not work
            myAnimationTimer.Enabled = true;

        }

        //Timer Method that runs every time the timer elapses
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //when 4 players get cards ii=0
            if (ii >= 4) 
            {
                ii = 0;
            }
            
            //Setting the background colors
            var myBackgroundColor = ConsoleColor.White;

            myTimerCounter++;

            // We assign card values ​​to get animation
            char theCard = Card(iiii);
            
            //Move the cursor back to the left, where it started to draw the animation to begin with.
            //Once we redraw, and redraw, and redraw, it will look animated.
            //We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
            Console.CursorLeft = 20;
            Console.CursorLeft += iiii;
            Console.CursorLeft += iiii;
            Console.CursorLeft += iiii;
            Console.CursorLeft += iiii;

            //Setting the cards colors
            var myBarGraphColor = Color(theCard);
            Console.ForegroundColor = myBarGraphColor;

            //Create card animation
            Console.BackgroundColor = myBackgroundColor;
            Console.Write(Game.pld[ii][iiii].realwhide()); //rank and suit of card
            if (Game.pld[ii][iiii].realwhide().Length==2)
            Console.Write(" "); // " " between cards
            Console.ResetColor();

            //increase iii by 1 to get the next card value
            iiii++;

            //After a bit of time, stop the animation
            //We change this as well for longer/shorter, faster/slower
            //Right now, the animation, or resetting/redrawing of the graph will happen 50 times, once every 50/1000 seconds.
            if (myTimerCounter == 13 || iiii > 13)
            {
                //Stop Timer
                myAnimationTimer.Stop();

                //Reset myTimerCounter to 0 for another restaurant 
                myTimerCounter = 0;

                //Reset iii to 0 for another restaurant
                iiii = 0;
                ii++;

                //Reset color
                Console.ResetColor();

                //Show the cursor again so the user can do what you need them to do.
                Console.CursorVisible = true;

            }
        }
        private static char Card(int iiii) //return this card suits
        {
            char a;
            a = Game.pld[ii][iiii].realwhide2();
            return a;
        }
        private static  System.ConsoleColor Color(char ball) //get color of the card
        {
            System.ConsoleColor color;
            if (ball =='\u2660'|| ball == '\u2663')
            {
                color = ConsoleColor.Black;
            }
            else
            {
                color = ConsoleColor.Red;
            }
            return color;
        }
    }
}


