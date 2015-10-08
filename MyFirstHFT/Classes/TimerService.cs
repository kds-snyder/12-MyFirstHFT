using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MyFirstHFT.Classes
{
    class TimerService
    {
        // Indicate if timer stop method was called,
        //  to prevent timer elapsed event from changing data
        //  if timer elapses after it is stopped 
        public static bool timerStopped = false;

        // Timer object 
        public static Timer timer = new Timer();

        // Set timer for input duration
        public static void setTimer(int duration)
        {
            // Set the timer to the input duration,
            // set event handler for timer elapsed to OnTimedEvent,
            // set timer to reset automatically           
            timer.Interval = duration;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        // Timer elapsed: update current stock info
        // Check timer stopped flag to allow for possibility that
        //  elapsed event occurred after timer was stopped
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Update current stock info only if timer stop is not indicated
            if (!timerStopped)
            {
                try
                {
                    StockService.UpdateCurrentStockInfo();
                }
                catch (Exception except)
                {
                    Console.WriteLine("An error occurred: {0}", except.Message);
                }
            }
            
            // Timer stopped indication can be reset now that elapsed event occurred
            else
            {
                timerStopped = false;
            }
        }

        // Stop the timer
        public static void stopTimer()
        {
            timer.Stop();
            timer.Dispose();
            timerStopped = true;
        }
    }
}
