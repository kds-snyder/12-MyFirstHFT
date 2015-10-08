using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstHFT.Classes
{
    public class StockService
    {
        // Current stock quote
        public static StockQuote currentStockQuote = new StockQuote();

        //Previous stock quote
        public static StockQuote previousStockQuote = new StockQuote();

        // Temporary counter to change prices
        public static int counter = 0;

        // Get stock information for the input symbol,
        // and store it in the current stock quote
        // Return true if successful, otherwise return false        
        public static bool GetCurrentStockInfo(string inputSymbol)
        {
            // Use Yahoo finance API to get stock quote; 
            // URL to get stock quote including current, low, & high price for
            // Apple (symbol AAPL) returned in JSON format would be:
            // http://finance.yahoo.com/webservice/v1/symbols/AAPL/quote?format=json&view=detail

            string url = @"http://finance.yahoo.com/webservice/v1/symbols/"
                            + inputSymbol + @"/quote?format=json&view=detail";

            using (WebClient webClient = new WebClient())
            {
                string json = webClient.DownloadString(url);
                var o = JObject.Parse(json);
                var oList = o["list"];
                var oResources = oList["resources"].ToArray();

                // If no stock information was found for the symbol, 
                // then the resources array is empty                
                if (oResources.Length == 0)
                {
                    return false;
                }              

                var oResource = oResources[0]["resource"];               
                var oFields = oResource["fields"];

                // Assign the stock info to the StockQuote object properties
                currentStockQuote.companyName = oFields["issuer_name"].ToString();
                currentStockQuote.symbol = oFields["symbol"].ToString();

                string stringPrice = oFields["price"].ToString();
                currentStockQuote.price = Decimal.Parse(stringPrice);

                string stringDayLowPrice = oFields["day_low"].ToString();
                currentStockQuote.dayLowPrice = Decimal.Parse(stringDayLowPrice);

                string stringDayHighPrice = oFields["day_high"].ToString();
                currentStockQuote.dayHighPrice = Decimal.Parse(stringDayHighPrice);
               
                return true;
                /*
                // temporary test unable to update
                if (counter == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                */               
            }
        }


        // Get a new current quote (after copying current to previous),
        //  and display with appropriate indication
        //  according to comparison with previous
        public static void UpdateCurrentStockInfo()
        {            
           CopyCurrentToPrevious();       
           if (GetCurrentStockInfo(currentStockQuote.symbol))
           {
                
                // Temporary: change prices            
                if ((counter % 2) == 0)
                {
                    currentStockQuote.price = previousStockQuote.price + 1;
                }
                else if (counter %3 == 0)
                {
                    currentStockQuote.price = previousStockQuote.price - 1;
                }
                else
                {
                    currentStockQuote.price = previousStockQuote.price;
                }                
                ++counter;
                // End temporary price change                               

                if (currentStockQuote.price > previousStockQuote.price)
                {
                    DisplayCurrentStockQuote("The price is going up: Sell!");
                }
                else if (currentStockQuote.price < previousStockQuote.price)
                {
                    DisplayCurrentStockQuote("The price is going down: Buy!");
                }
                else
                {
                    DisplayCurrentStockQuote("The price hasn't changed");
                }
           }
           else
            { // If unable to get the new quote, copy previous back to current,
              // and display with indication that info could not be updated
                CopyPreviousToCurrent();
                DisplayCurrentStockQuote("Unable to update price information");
            }
        }

        //Display the current stock quote information,
        // and append input string
        public static void DisplayCurrentStockQuote(string infoString)
        {
            Console.WriteLine
                ("Company: {0} - Current: {1:C} Low: {2:C} High: {3:C} - {4}",
                  currentStockQuote.companyName, currentStockQuote.price,
                  currentStockQuote.dayLowPrice, currentStockQuote.dayHighPrice,
                  infoString);
        }

        // Copy current stock quote to previous stock quote
        public static void CopyCurrentToPrevious()
        {
            previousStockQuote.companyName = currentStockQuote.companyName;
            previousStockQuote.symbol = currentStockQuote.symbol;
            previousStockQuote.price = currentStockQuote.price;
            previousStockQuote.dayLowPrice = currentStockQuote.dayLowPrice;
            previousStockQuote.dayHighPrice = currentStockQuote.dayHighPrice;
        }

        // Copy previous stock quote to current  stock quote
        public static void CopyPreviousToCurrent()
        {
            currentStockQuote.companyName = previousStockQuote.companyName;
            currentStockQuote.symbol = previousStockQuote.symbol;
            currentStockQuote.price = previousStockQuote.price;
            currentStockQuote.dayLowPrice = previousStockQuote.dayLowPrice;
            currentStockQuote.dayHighPrice = previousStockQuote.dayHighPrice;
        }

    }
}
