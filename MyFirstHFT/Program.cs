using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstHFT.Classes;

namespace MyFirstHFT
{
    class Program
    {
        const int TIMERDURATION = 5000; // Set timer to 5 seconds (5000 milliseconds)
        static void Main(string[] args)
        {
            try
            {               
                // Get the input stock symbol               
                string inputSymbol = 
                    getString("Enter a stock symbol, for example AAPL or GOOG or MSFT: ");

                // Get the current stock quote (using input symbol in upper case)
                // If found, display the current stock quote with indication to wait           
                if (StockService.GetCurrentStockInfo(inputSymbol.ToUpper()))               
                {                   
                   StockService.DisplayCurrentStockQuote("Wait for more information");

                    //  Set a timer to display more stock quotes                
                    TimerService.setTimer(TIMERDURATION);

                    Console.ReadLine();
                    TimerService.stopTimer();
                    Console.WriteLine("Press <enter> to exit...");
                }
                else
                {                    
                    Console.WriteLine("No stock information was found for {0}",
                                       inputSymbol);
                }                              

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
                Console.ReadLine();
            }            

            Console.ReadLine();
        }

        // Prompt user to enter string, using input prompt
        // Return the string that the user enters
        // Allow input of only <return> according to indicator AcceptEmpty
        static string getString(string prompt, bool AcceptEmpty=false)
        {
            string result = "";
            bool done = false;
            while (!done)
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (result.Length == 0)
                {
                    if (AcceptEmpty)
                    {
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
            } 

            return result;
        }

       

    }
}
