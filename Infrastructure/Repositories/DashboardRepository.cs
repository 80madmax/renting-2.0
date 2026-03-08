using Core.Interfaces;
using Core.ReadModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly RentingDbContext _context;       

        public DashboardRepository(RentingDbContext context)
        {
            _context = context;         
        }

        public async Task<IReadOnlyList<MonthlyProfit>> GetProfitPerMonth(int year)
        {
            var rows = await _context.Transactions
                                    .AsNoTracking()
                                    .Where(t => t.Year == year)
                                    .GroupBy(t => t.Month)
                                    .Select(g => new
                                    {
                                        Month = g.Key,
                                        Profit = g.Sum(x => x.Amount)
                                    })
                                    .OrderBy(x => x.Month)
                                    .ToListAsync();

            return rows.Select(x => new MonthlyProfit(x.Month, x.Profit)).ToList();

        }

        public async Task<IReadOnlyList<YearlyProfit>> GetProfitPerYear()
        {
            var rows = await _context.Transactions
                                                .AsNoTracking()
                                                .GroupBy(t => t.Year)
                                                .Select(g => new
                                                {
                                                    Year = g.Key,
                                                    Profit = g.Sum(x => x.Amount)
                                                })
                                                .OrderBy(x => x.Year)
                                                .ToListAsync();

            return rows.Select(x => new YearlyProfit(x.Year, x.Profit)).ToList();
        }

        public async Task<IReadOnlyList<UnitMonthlyBalance>> GetUnitMonthlyBalance(int month, int year)
        {            
            var rows = await _context.Units
            .AsNoTracking()
            .Select(u => new
            {
                u.Name,
                FloorName = u.Floor.Name,
                u.Address,
                DistrictName = u.District.Name,
                u.RentPrice,
                Amount = u.Transactions
                    .Where(t => t.Month == month && t.Year == year)
                    .Sum(t => (decimal?)t.Amount) ?? 0m
            })           
            .ToListAsync();

            var result = rows
                            .Select(x => new UnitMonthlyBalance(
                                apartment: $"{x.Name} - {x.FloorName} - {x.Address} - {x.DistrictName}",
                                rentPrice: x.RentPrice,
                                month: month,
                                year: year,
                                amount: x.Amount
                            ))
                            .OrderBy(x => x.Apartment) // safe in memory
                            .ToList();

            return result;
        }

        public async Task<IReadOnlyList<UnitROI>> GetUnitsROI()
        {
            var rows = await _context.Units
            .AsNoTracking()
            .Select(u => new
            {             
                u.Name,
                u.Cost,
                Returned = u.Transactions
                    .Sum(t => (decimal?)t.Amount) ?? 0m
            })
            .ToListAsync();

            // C#: compute ROI + map + sort
            return rows
                .Select(x =>
                {
                    var roi = x.Cost <= 0m ? 0m : (x.Returned / x.Cost) * 100m;
                    return new UnitROI(x.Name, roi);
                })
                .OrderByDescending(x => x.RoiPercent)
                .ToList();
        }

        public async Task<PortfolioROI> GetPortfolioRoi()
        {
            // Total cost of all properties
            var totalCost = await _context.Units
                .AsNoTracking()
                .SumAsync(u => (decimal?)u.Cost) ?? 0m;

            // Total net of all transactions (positive + negative)
            var totalNet = await _context.Transactions
                .AsNoTracking()
                .SumAsync(t => (decimal?)t.Amount) ?? 0m;

            return new PortfolioROI(totalCost, totalNet);
        }
    }
}
