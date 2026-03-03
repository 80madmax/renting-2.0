using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITelegramMessageRepository
    {
        Task<UnitExpenseSummary?> GetUnitExpenseSummaryAsync(int itemId, int month, int year, CancellationToken ct = default);
    }
}
