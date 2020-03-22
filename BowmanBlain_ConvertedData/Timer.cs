using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BowmanBlain_ConvertedData
{
    class Timer
    {

        //--------------------
        //Animated Bar Graph
        //--------------------
        //Think, the following examples:
        //...showing a loading proression bar
        //...showing the distance from the start to a goal
        //...showing amount of percentage downloaded


        //To animate the bar graph, we need to know the value of the graph, the size of the graph, the speed we want it to animate, and for how long we want to animate it.
        //To animate it in the console, we can just choose random numbers and draw the graph over and over again.
        //--------------------

        //Timer Variable
        //This creates the actual timer
        //Note this is outside of the Main()
        //Note that we added in "using System.Timers;" at the top
        public static System.Timers.Timer myAnimationTimer;

        //Timer Counter
        //This counter is used to stop the timer when we want it to
        //So, it helps to make the animation go longer/shorter, faster/slower
        //Note this is outside of the Main()
        //static int myTimerCounter = 0;
        //Timer Method that runs every time the timer elapses
        public static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            try
            {
                //define each key value from dictionary
                foreach (var key in Graphs.graphs)
                {
                    //display the key value on the screen

                    Console.Write(key.Key);
                    Console.Write(" ");

                    //We determine the sum of all rating values ​​for key = restaurant
                    int a = 0;
                    decimal b = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        a = key.Value[i];
                        b += a;

                    }
                    Console.Write(" ");
                    //we find the average rating for the restaurant
                    int mid = Decimal.ToInt32(Math.Round(b / 100, MidpointRounding.AwayFromZero));

                    //define each value from dictionary
                    foreach (int val in Graphs.graphs[key.Key])

                    {
                        var myBackgroundColor = ConsoleColor.White; //Setting the bar graph colors
                        Graphs g = new Graphs();
                        var myBarGraphColor = g.Color(val);  //Setting the bar graph colors
                        var theRating = val;
                        Console.BackgroundColor = myBarGraphColor;

                        //Move the cursor back to the left, where it started to draw the animation to begin with.
                        //Once we redraw, and redraw, and redraw, it will look animated.
                        //We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
                        Console.CursorLeft = 40;

                        //Create bar graph, not the bar graph background
                        //We create the bar we want first, and the background second.
                        for (int i = 0; i < theRating; i++)
                        {
                            //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                            //Console.BackgroundColor = myBarGraphColor; 
                            //This creates a colored bar graph of spaces, so if you have a 5/10, you will get 5 colored spaces.
                            Console.Write(" ");
                            Thread.Sleep(0);

                        }
                        //Console.Write(" ");
                        //Set total number for the length of the bar graph, which is also the background
                        int myTotalNumber = 10;

                        //Set bar graph background color
                        //We can set the color once here, or set it over and over again in the loop below.
                        Console.BackgroundColor = myBackgroundColor;


                        //Draw bar graph background
                        //The background is not seen around the edges of the bar, or also with the foreground color of the bar. We only see one color at a time.
                        //So, we just start drawing the background after the final drawing of the bar graph based on the data.
                        //For Example...if we have 5/10, then the bar graph is 1-5 and the background is 6-10.
                        for (int ii = theRating; ii < myTotalNumber; ii++)
                        {
                            //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                            //Console.BackgroundColor = myBackgroundColor;
                            //This creates a colored background of spaces, so if you have a 5/10, you will get 5 colored spaces to make the background after the 5 colored spaces that made up the bar.
                            Console.Write(" ");

                        }




                    }

                    // display in the graph the total / average rating value for the restaurant
                    Graphs v = new Graphs();
                    var myBarGraphColor1 = v.Color(mid);  //Setting the bar graph colors 
                    Console.BackgroundColor = myBarGraphColor1;
                    //Move the cursor back to the left, where it started to draw the animation to begin with.
                    //Once we redraw, and redraw, and redraw, it will look animated.
                    //We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
                    Console.CursorLeft = 40;
                    for (int i = 0; i < mid; i++)// 
                    {
                        //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                        //Console.BackgroundColor = myBarGraphColor; 
                        //This creates a colored bar graph of spaces, so if you have a 5/10, you will get 5 colored spaces.
                        Console.Write(" ");

                    }
                    var myBackgroundColor1 = ConsoleColor.White;
                    Console.BackgroundColor = myBackgroundColor1;


                    //Draw bar graph background
                    //The background is not seen around the edges of the bar, or also with the foreground color of the bar. We only see one color at a time.
                    //So, we just start drawing the background after the final drawing of the bar graph based on the data.
                    //For Example...if we have 5/10, then the bar graph is 1-5 and the background is 6-10.
                    for (int ii = mid; ii < 10; ii++)//
                    {
                        //We could run the below code here if we wanted, but we just set it once above instead of each time here.
                        //Console.BackgroundColor = myBackgroundColor;
                        //This creates a colored background of spaces, so if you have a 5/10, you will get 5 colored spaces to make the background after the 5 colored spaces that made up the bar.
                        Console.Write(" ");
                    }

                    Console.ResetColor(); //Reset Color
                    //Move the cursor back to the left, where it started to draw the animation to begin with.
                    //Once we redraw, and redraw, and redraw, it will look animated.
                    //We are doing this because we are uisng a Console.Write to stay on the same line, so we move back, and redraw over top of the old one.
                    Console.CursorLeft = 60;

                    Console.Write(mid); //Display the average rating of the restaurant
                    Console.WriteLine("");

                }

            }

            finally { }



        }
        public static void SetTimer()
        {

            //Set time to happen really fast 1,000 = 1 second
            //Start the function every 50/1000 seconds
            myAnimationTimer = new System.Timers.Timer(10000);

            //At 50/1000, run this method "OnTimedEvent"
            //Every time it elapses, do it
            myAnimationTimer.Elapsed += OnTimedEvent;

            //Reset timer again after 50/1000, over and over again
            //False means to only run the timer one time
            myAnimationTimer.AutoReset = false;

            //The timer is enabled so it will work
            //False means the timer will not work
            myAnimationTimer.Enabled = true;


        }

        /*public static void RunFunc(int value, string key)
        {

           
        }*/

    }
}
