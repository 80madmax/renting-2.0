using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UnitMonthlyBalanceDTO
    {
        public IReadOnlyList<UnitMonthlyBalance> UnitsMonthlyBalances { get; init; }
    }
}
