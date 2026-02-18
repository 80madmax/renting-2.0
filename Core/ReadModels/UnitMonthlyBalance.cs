using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public class UnitMonthlyBalance
    {
        public string Apartment { get; }
        public decimal RentPrice { get; }
        public int Month { get; }
        public int Year { get; }
        public decimal Amount { get; }

        public UnitMonthlyBalance(string apartment, decimal rentPrice, int month, int year, decimal amount)
        {
            Apartment = apartment;
            RentPrice = rentPrice;
            Month = month;
            Year = year;
            Amount = amount;
        }

    }
}
