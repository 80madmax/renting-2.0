using Application.DTOs;

namespace BO.ViewModels
{
    public class DashboardViewModel
    {
        public ProfitPerMonthDashboardDTO ProfitPerMonth { get; set; } = default!;
        public ProfitPerYearDashboardDTO ProfitPerYear { get; set; } = default!;
        public UnitMonthlyBalanceDTO UnitsMonthlyBalances { get; set; } = default!;
        public UnitsROIDTO UnitsROI { get; set; } = default!;
        public PortfolioROIDTO PortfolioRoi { get; set; } = default!;
    }
}
