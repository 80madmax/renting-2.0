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

            // Convert anonymous projection to Core.ReadModels.MonthlyProfit
            return rows.Select(x => new MonthlyProfit(x.Month, x.Profit)).ToList();
        }
    }
}
