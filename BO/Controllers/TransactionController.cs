using Application.Services;
using BO.ViewModels;
using Core.Filters;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace BO.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitService _unitService;
        private readonly IPaymentService _paymentService;

        public TransactionController(ITransactionService transactionService, IUnitService unitService, IPaymentService paymentService)
        {
            _transactionService = transactionService;
            _unitService = unitService;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> Create()
        {
            var now = DateTime.Now;
            var payments = await _paymentService.GetAllOrderedByType();

            var model = new TransactionCreateViewModel
            {
                Year = now.Year,
                Month = now.Month,

                Units = _unitService.GetAll().Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }),

                Payments = payments.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{ p.PaymentType.Name } - {p.Name}"
                }),

                Years = Enumerable.Range(now.Year - 5, 11).Select(y => new SelectListItem
                {
                    Value = y.ToString(),
                    Text = y.ToString()
                }),

                Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
                {
                    Value = m.ToString(),
                    Text = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(m)
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns
                model.Units = _unitService.GetAll().Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name });
                model.Payments = _paymentService.GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
                model.Years = Enumerable.Range(DateTime.Now.Year - 5, 11).Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() });
                model.Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
                {
                    Value = m.ToString(),
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
                });

                return View(model);
            }

            foreach (var unitId in model.SelectedUnitIds)
            {
                var transaction = new Transaction
                {
                    UnitId = unitId,
                    PaymentId = model.PaymentId,
                    Year = model.Year,
                    Month = model.Month,
                    Amount = model.Amount,
                    Name = model.Note
                };

                await _transactionService.AddAsync(transaction);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(TransactionFilterViewModel filter, int pageNumber = 1, int pageSize = 10)
        {
            // Default current month/year if not selected
            filter.SelectedMonth ??= DateTime.Now.Month;
            filter.SelectedYear ??= DateTime.Now.Year;

            // Map the filter to domain filter
            var filterEntity = new TransactionFilter
            {
                UnitId = filter.SelectedUnitId,
                MonthId = filter.SelectedMonth,
                YearId = filter.SelectedYear,
                PaymentId = filter.SelectedPaymentId
            };

            // Get paginated, filtered result
            var paginated = await _transactionService.GetPaginatedWithFiltersAsync(filterEntity, pageNumber, pageSize);

            // Fetch dropdown sources
            var units = await _unitService.GetAllWithDistrictCityFloor();
            var payments = await _paymentService.GetAllOrderedByType();

            // Build view model
            var viewModel = new TransactionListViewModel
            {
                Filter = new TransactionFilterViewModel
                {
                    SelectedUnitId = filter.SelectedUnitId,
                    SelectedMonth = filter.SelectedMonth,
                    SelectedYear = filter.SelectedYear,
                    SelectedPaymentId = filter.SelectedPaymentId,

                    Units = units.Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = $"{u.District.Name} - {u.Address} - {u.Floor.Name} - {u.Name}",
                        Selected = (u.Id == filter.SelectedUnitId)
                    }),

                    Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
                    {
                        Value = m.ToString(),
                        Text = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(m),
                        Selected = (m == filter.SelectedMonth)
                    }),

                    Years = Enumerable.Range(DateTime.Now.Year - 5, 10).Select(y => new SelectListItem
                    {
                        Value = y.ToString(),
                        Text = y.ToString(),
                        Selected = (y == filter.SelectedYear)
                    }),

                    Payments = payments.Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.PaymentType.Name} - {p.Name}",
                        Selected = (p.Id == filter.SelectedPaymentId)
                    })
                },

                Transactions = paginated.Items.Select(t => new TransactionViewModel
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Month = t.Month,
                    Year = t.Year,
                    PaymentName = t.Payment.Name,
                    UnitName = t.Unit.Name
                }).ToList(),

                PageIndex = paginated.PageIndex,
                TotalPages = paginated.TotalPages
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _transactionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
