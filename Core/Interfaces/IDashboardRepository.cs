using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IReadOnlyList<MonthlyProfit>> GetProfitPerMonth(int year);
        Task<IReadOnlyList<YearlyProfit>> GetProfitPerYear();
        Task<IReadOnlyList<UnitMonthlyBalance>> GetUnitMonthlyBalance(int month, int year);

    }
}
