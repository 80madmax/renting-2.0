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

        public async Task<ProfitPerMonthDashboardDTO> GetProfitPerMonthDashboard(int loggedUserId)
        {
            var year = DateTime.Now.Year;

            var rows = await _dashboardRepository.GetProfitPerMonth(year, loggedUserId);

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

        public async Task<ProfitPerYearDashboardDTO> GetProfitPerYearDashboard(int loggedUserId)
        {        
            var yearlyProfits = await _dashboardRepository.GetProfitPerYear(loggedUserId);

            return new ProfitPerYearDashboardDTO
            {                
                YearlyProfits = yearlyProfits
            };

        }

        public async Task<UnitMonthlyBalanceDTO> GetUnitsMonthlyBalanceDashboard(int loggedUserId)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            var unitsMonthlyBalances = await _dashboardRepository.GetUnitMonthlyBalance(month, year, loggedUserId);

            return new UnitMonthlyBalanceDTO
            {
                UnitsMonthlyBalances = unitsMonthlyBalances
            };
        }

        public async Task<UnitsROIDTO> GetUnitsROI(int loggedUserId)
        {
            var items = await _dashboardRepository.GetUnitsROI(loggedUserId);

            return new UnitsROIDTO
            {
                Items = items
            };
        }

        public async Task<PortfolioROIDTO> GetPortfolioRoiDashboard(int loggedUserId)
        {
            var data = await _dashboardRepository.GetPortfolioRoi(loggedUserId);

            return new PortfolioROIDTO
            {
                Data = data
            };
        }
    }
}
