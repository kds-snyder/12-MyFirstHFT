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
                }
                else
                {                    
                    Console.WriteLine("No stock information was found for {0}",
                                       inputSymbol);
                }
                
                //  Set a timer to display more stock quotes                
                TimerService.setTimer(TIMERDURATION);

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message); 
            }            

            Console.ReadLine();
        }

        // Prompt user to enter string, using input prompt
        // Return the string that the user enters
        static string getString(string prompt)
        {
            string result = "";
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
            } while (result == "");

            return result;
        }

       

    }
}
