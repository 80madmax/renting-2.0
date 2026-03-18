using Application.DTOs;
using Application.UseCases;
using BO.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BO.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IDashboardUseCase _dashboardUseCase;

        public DashboardController(IDashboardUseCase dashboardUseCase)
        {
            _dashboardUseCase = dashboardUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profitPerMonth = await _dashboardUseCase.GetProfitPerMonthDashboard(LoggedUserIdAsInt);

            var profitPerYear = await _dashboardUseCase.GetProfitPerYearDashboard(LoggedUserIdAsInt);

            var unitsMonthlyBalances = await _dashboardUseCase.GetUnitsMonthlyBalanceDashboard(LoggedUserIdAsInt);

            var unitsROI = await _dashboardUseCase.GetUnitsROI(LoggedUserIdAsInt);

            var portfolioRoi = await _dashboardUseCase.GetPortfolioRoiDashboard(LoggedUserIdAsInt);

            var vm = new DashboardViewModel
            {
                ProfitPerMonth = profitPerMonth,
                ProfitPerYear = profitPerYear,
                UnitsMonthlyBalances = unitsMonthlyBalances,
                UnitsROI = unitsROI,
                PortfolioRoi = portfolioRoi
            };

            return View(vm);
        }
    }
}
