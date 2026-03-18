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
        Task<IReadOnlyList<MonthlyProfit>> GetProfitPerMonth(int year, int loggedUserId);
        Task<IReadOnlyList<YearlyProfit>> GetProfitPerYear(int loggedUserId);
        Task<IReadOnlyList<UnitMonthlyBalance>> GetUnitMonthlyBalance(int month, int year, int loggedUserId);
        Task<IReadOnlyList<UnitROI>> GetUnitsROI(int loggedUserId);
        Task<PortfolioROI> GetPortfolioRoi(int loggedUserId);
    }
}
