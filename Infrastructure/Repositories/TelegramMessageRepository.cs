using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ReadModels;

namespace Infrastructure.Repositories
{
    //what message should I send?
    public class TelegramMessageRepository : ITelegramMessageRepository
    {
        private readonly RentingDbContext _context;
        public TelegramMessageRepository(RentingDbContext context)
        {
            _context = context;
        }

        public async Task<UnitExpenseSummary?> GetUnitExpenseSummaryAsync(
        int unitId, int month, int year, CancellationToken ct = default)
        {
            const int EXPENSE_TYPE_ID = 1;

            var rows = await _context.Transactions
                .Where(t =>
                    t.UnitId == unitId &&
                    t.Month == month &&
                    t.Year == year &&
                    t.Payment.PaymentTypeId == EXPENSE_TYPE_ID)
                .Select(t => new
                {
                    UnitName = t.Unit.Name,
                    PaymentName = t.Payment.Name,
                    t.Amount
                })
                .ToListAsync(ct);

            if (rows.Count == 0)
                return null;

            var unitName = rows[0].UnitName;

            var expenses = rows
                .GroupBy(x => x.PaymentName)
                .Select(g => new ExpenseItem(g.Key, g.Sum(x => x.Amount)))
                .ToList();

            return new UnitExpenseSummary(unitName, month, year, expenses);
        }

    }
}
