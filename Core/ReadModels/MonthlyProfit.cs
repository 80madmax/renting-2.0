using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public class MonthlyProfit
    {
        public int Month { get; }
        public decimal Profit { get; }

        public MonthlyProfit(int month, decimal profit)
        {
            Month = month;
            Profit = profit;
        }
    }
}
