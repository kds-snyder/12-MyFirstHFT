using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstHFT.Classes
{
    // Information about a specific stock
    public class StockQuote
    {
        public string companyName { get; set; }
        public string symbol { get; set; }
        public decimal price { get; set; }
        public decimal dayLowPrice { get; set; }
        public decimal dayHighPrice { get; set; }
    }
}
