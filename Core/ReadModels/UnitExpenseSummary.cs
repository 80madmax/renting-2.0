using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ReadModels
{
    public sealed record ExpenseItem(
         string Name,
         decimal Amount 
    );

    public sealed record UnitExpenseSummary(
        string UnitName,
        int Month,
        int Year,
        IReadOnlyList<ExpenseItem> Expenses
    );
}
