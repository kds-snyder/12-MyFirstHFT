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
        // Set timer for input duration
        public static void setTimer(int duration)
        {
            // Set the timer to the input duration,
            // set event handler for timer elapsed to OnTimedEvent,
            // set timer to reset automatically
            Timer timer = new Timer();
            timer.Interval = duration;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;

            // Start the timer
            timer.Enabled = true;
        }

        // Handle timer elapsed event:
        // 
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            StockService.GetNewCurrentStockInfo();

        }
    }
}
