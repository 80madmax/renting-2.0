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
    }
}
