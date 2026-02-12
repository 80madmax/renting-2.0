using Application.DTOs;
using Core.Interfaces;
using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class DashboardUseCase : IDashboardUseCase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardUseCase(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<ProfitPerMonthDashboardDTO> GetProfitPerMonthDashboard()
        {
            var year = DateTime.Now.Year;

            var rows = await _dashboardRepository.GetProfitPerMonth(year);

            var map = rows.ToDictionary(x => x.Month, x => x.Profit);

            var fullYear = Enumerable.Range(1, 12)
                .Select(m => new MonthlyProfit(
                    m,
                    map.TryGetValue(m, out var profit) ? profit : 0m
                ))
                .ToList();

            return new ProfitPerMonthDashboardDTO
            {
                Year = year,
                MonthlyProfits = fullYear
            };

        }
    }
}
