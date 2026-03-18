using Application.DTOs;
using Core.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public interface IDashboardUseCase
    {
        Task<ProfitPerMonthDashboardDTO> GetProfitPerMonthDashboard(int loggedUserId);
        Task<ProfitPerYearDashboardDTO> GetProfitPerYearDashboard(int loggedUserId);
        Task<UnitMonthlyBalanceDTO> GetUnitsMonthlyBalanceDashboard(int loggedUserId);
        Task<UnitsROIDTO> GetUnitsROI(int loggedUserId);
        Task<PortfolioROIDTO> GetPortfolioRoiDashboard(int loggedUserId);
    }
}
