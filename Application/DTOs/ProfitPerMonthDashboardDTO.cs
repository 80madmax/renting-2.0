using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProfitPerMonthDashboardDTO
    {
        public int Year {  get; set; }
        public IReadOnlyList<MonthlyProfit> MonthlyProfits { get; init; }
    }
}
