using Application.DTOs;
using Application.UseCases;
using BO.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BO.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardUseCase _dashboardUseCase;

        public DashboardController(IDashboardUseCase dashboardUseCase)
        {
            _dashboardUseCase = dashboardUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profitPerMonth = await _dashboardUseCase.GetProfitPerMonthDashboard();

            var profitPerYear = await _dashboardUseCase.GetProfitPerYearDashboard();

            var unitsMonthlyBalances = await _dashboardUseCase.GetUnitsMonthlyBalanceDashboard();

            var vm = new DashboardViewModel
            {
                ProfitPerMonth = profitPerMonth,
                ProfitPerYear = profitPerYear,
                UnitsMonthlyBalances = unitsMonthlyBalances

            };

            return View(vm);
        }
    }
}
