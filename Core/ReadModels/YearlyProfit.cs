using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public class YearlyProfit
    {
        public int Year { get; set; }

        public decimal Profit { get; set; }

        public YearlyProfit(int year, decimal profit)
        {
            Year = year;
            Profit = profit;
        }
    }
}
