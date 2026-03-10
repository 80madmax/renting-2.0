using Application.UseCases;
using BO.ViewModels.Reports;
using Core.Filters;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;

namespace BO.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitService _unitService;
        private readonly SendUnitExpenseTelegramMessage _sendTelegram;

        public ReportsController(ITransactionService transactionService, IUnitService unitService, SendUnitExpenseTelegramMessage sendTelegram)
        {
            _transactionService = transactionService;
            _unitService = unitService;
            _sendTelegram = sendTelegram;
        }

        // GET: Reports/ExpensesIncome
        // Accepts optional query params that match the view names: SelectedMonth, SelectedYear, SelectedUnitId
        // and isInitialLoad so the controller preserves user selections after the first load (matches Transaction/Index behavior).
        public async Task<IActionResult> ExpensesIncome(int? SelectedMonth, int? SelectedYear, int? SelectedUnitId, bool isInitialLoad = true)
        {
            var now = DateTime.Now;

            // Only apply defaults on the initial load
            if (isInitialLoad)
            {
                SelectedMonth ??= now.Month;
                SelectedYear ??= now.Year;
            }

            // Build select lists (style and defaults like Transaction/Index)
            var months = Enumerable.Range(1, 12).Select(m => new SelectListItem
            {
                Value = m.ToString(),
                Text = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(m),
                Selected = (m == SelectedMonth)
            });

            var years = Enumerable.Range(now.Year - 5, 11).Select(y => new SelectListItem
            {
                Value = y.ToString(),
                Text = y.ToString(),
                Selected = (y == SelectedYear)
            });

            var unitsSource = await _unitService.GetAllWithDistrictCityFloor();
            var units = unitsSource.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Name} - {u.Floor?.Name} - {u.Address} - {u.District?.Name}",
                Selected = (u.Id == SelectedUnitId)
            }).ToList();

            // insert All option to match Transaction/Index semantics
            units.Insert(0, new SelectListItem { Value = "", Text = "-- All Units --", Selected = !SelectedUnitId.HasValue });

            var model = new ExpensesIncomeViewModel
            {
                SelectedMonth = SelectedMonth,
                SelectedYear = SelectedYear,
                SelectedUnitId = SelectedUnitId,
                Months = months,
                Years = years,
                Units = units
            };

            // Build real report by aggregating transactions for the selected period
            model.Rows = await BuildExpensesIncomeReportAsync(SelectedMonth, SelectedYear, SelectedUnitId);

            return View(model);
        }

        // Taxes report
        // GET: Reports/TaxesReport
        // Same filters as ExpensesIncome
        public async Task<IActionResult> TaxesReport(int? SelectedMonth, int? SelectedYear, int? SelectedUnitId, bool isInitialLoad = true)
        {
            var now = DateTime.Now;

            // only apply defaults on initial load (preserve user selection after postback)
            if (isInitialLoad)
            {
                SelectedMonth ??= now.Month;
                SelectedYear ??= now.Year;
            }

            var months = Enumerable.Range(1, 12).Select(m => new SelectListItem
            {
                Value = m.ToString(),
                Text = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(m),
                Selected = (m == SelectedMonth)
            });

            var years = Enumerable.Range(now.Year - 5, 11).Select(y => new SelectListItem
            {
                Value = y.ToString(),
                Text = y.ToString(),
                Selected = (y == SelectedYear)
            });

            var unitsSource = await _unitService.GetAllWithDistrictCityFloor();
            var units = unitsSource.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Name} - {u.Floor?.Name} - {u.Address} - {u.District?.Name}",
                Selected = (u.Id == SelectedUnitId)
            }).ToList();

            units.Insert(0, new SelectListItem { Value = "", Text = "-- All Units --", Selected = !SelectedUnitId.HasValue });

            var model = new TaxesReportViewModel
            {
                SelectedMonth = SelectedMonth,
                SelectedYear = SelectedYear,
                SelectedUnitId = SelectedUnitId,
                Months = months,
                Years = years,
                Units = units
            };

            model.Rows = await BuildTaxesReportAsync(SelectedMonth, SelectedYear, SelectedUnitId);

            return View(model);
        }

        // Build report using real transactions:
        // - Income: transactions whose Payment.PaymentType.Id == 2
        // - Expenses: transactions whose Payment.PaymentType.Id == 1
        // - Taxes: transactions whose Payment.PaymentType.Id == 3
        // Sums are reported as positive values (Abs).
        private async Task<List<ExpensesIncomeRowViewModel>> BuildExpensesIncomeReportAsync(int? month, int? year, int? unitId)
        {
            // prepare filter for transactions
            var filter = new TransactionFilter
            {
                UnitId = unitId,
                MonthId = month,
                YearId = year
            };

            var transactions = (await _transactionService.GetFilteredTransactions(filter)).ToList();

            // If you want all units to appear (even those with zero transactions), left-join against all units here.
            if (!transactions.Any())
            {
                return new List<ExpensesIncomeRowViewModel>();
            }

            // group by UnitId
            var groups = transactions.GroupBy(t => t.UnitId);

            // fetch unit details to read RentPrice
            var allUnitsList = await _unitService.GetAllWithDistrictCityFloor();
            var unitsById = allUnitsList.ToDictionary(u => u.Id);

            var rows = groups.Select(g =>
            {
                var unitIdKey = g.Key;
                var unitName = g.FirstOrDefault()?.Unit?.Name ?? (unitsById.ContainsKey(unitIdKey) ? unitsById[unitIdKey].Name : $"Unit {unitIdKey}");
                var unitRent = unitsById.ContainsKey(unitIdKey) ? unitsById[unitIdKey].RentPrice : 0m;

                // Sum positive values for presentation (stored amounts may be negative)
                decimal income = g.Where(t => t.Payment?.PaymentType?.Id == 2).Sum(t => Math.Abs(t.Amount));
                decimal expenses = g.Where(t => t.Payment?.PaymentType?.Id == 1).Sum(t => Math.Abs(t.Amount));
                decimal taxes = g.Where(t => t.Payment?.PaymentType?.Id == 3).Sum(t => Math.Abs(t.Amount));

                var balance = income - taxes - expenses;

                return new ExpensesIncomeRowViewModel
                {
                    Month = month,
                    Year = year,
                    UnitId = unitIdKey,
                    UnitName = unitName,
                    UnitRent = unitRent,
                    Expenses = expenses,
                    Taxes = taxes,
                    Income = income,
                    Balance = balance
                };
            }).ToList();

            // Order: Year DESC, Month DESC, Unit ASC
            rows = rows
                .OrderByDescending(r => r.Year ?? int.MinValue)
                .ThenByDescending(r => r.Month ?? int.MinValue)
                .ThenBy(r => r.UnitName)
                .ToList();

            return rows;
        }

        // Build taxes report grouped by Month+Year (and optionally filtered by unit)
        private async Task<List<TaxesReportRowViewModel>> BuildTaxesReportAsync(int? month, int? year, int? unitId)
        {
            var filter = new TransactionFilter
            {
                UnitId = unitId,
                MonthId = month,
                YearId = year
            };

            var transactions = (await _transactionService.GetFilteredTransactions(filter)).ToList();

            // Only taxes PaymentType.Id == 3
            var taxTransactions = transactions.Where(t => t.Payment?.PaymentType?.Id == 3).ToList();

            var rows = taxTransactions
                .GroupBy(t => new { Year = t.Year, Month = t.Month })
                .Select(g => new TaxesReportRowViewModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(t => Math.Abs(t.Amount))
                })
                .ToList();

            // Order: Year DESC, Month DESC
            rows = rows
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToList();

            return rows;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendTelegram(int unitId, int month, int year, string? returnUrl = null)
        {
            var result = await _sendTelegram.ExecuteAsync(unitId, month, year);

            if (!result.Success)
            {
                TempData["Error"] = result.Error;
            }
            else
            {
                TempData["Success"] = "Telegram message sent successfully.";
            }

            // If returnUrl is provided and local, redirect back to it (keeps user on the report).
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}