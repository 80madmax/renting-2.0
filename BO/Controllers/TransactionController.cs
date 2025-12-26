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

            var payment = await _paymentService.GetByIdAsync(model.PaymentId);


            foreach (var unitId in model.SelectedUnitIds)
            {
                var transaction = new Transaction
                {
                    UnitId = unitId,
                    PaymentId = model.PaymentId,
                    Year = model.Year,
                    Month = model.Month,
                    Amount = (payment.PaymentType?.Id == 1) ? -model.Amount : model.Amount,                    
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
                        Text = $"{u.Name} -  {u.Floor.Name} - {u.Address} - {u.District.Name}",
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
                    UnitName = t.Unit.Name,
                    Floor = t.Unit.Floor.Name,
                    Address = t.Unit.Address,
                    District = t.Unit.District.Name
                    
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

        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionService.GetByIdWithDetails(id);
            if (transaction == null) return NotFound();

            var model = new TransactionViewModel
            {
                Id = transaction.Id,
                Name = transaction.Name,      
                UnitName = $"{transaction.Unit.Name} - {transaction.Unit.Floor.Name} - {transaction.Unit.Address} - {transaction.Unit.District.Name}",
                PaymentName = $"{transaction.Payment.PaymentType.Name} - {transaction.Payment.Name}",
                Amount = transaction.Amount,
                Month = transaction.Month,
                MonthName = CultureInfo
                                .GetCultureInfo("en-US")
                                .DateTimeFormat
                                .GetMonthName(transaction.Month),
                Year = transaction.Year
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            // Fetch dropdown sources
            var units = await _unitService.GetAllWithDistrictCityFloor();
            var payments = await _paymentService.GetAllOrderedByType();

            var model = new TransactionEditViewModel
            {
                TransactionId = transaction.Id,
                UnitId = transaction.UnitId,
                PaymentId = transaction.PaymentId,
                Month = transaction.Month,
                Year = transaction.Year,
                Amount = transaction.Amount,
                Note = transaction.Name,
                Units = units.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Name} - {u.Floor.Name} - {u.Address} - {u.District.Name}"
                }),

                Payments = payments.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.PaymentType.Name} - {p.Name}"
                }),

                Years = Enumerable.Range(DateTime.Now.Year - 5, 11).Select(y => new SelectListItem
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
        public async Task<IActionResult> Edit(TransactionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {              
                return View(model);
            }

            var transaction = new Transaction
            {
                Id = model.TransactionId,
                Name = model.Note,
                Month = model.Month,
                Year = model.Year,
                PaymentId = model.PaymentId,
                UnitId = model.UnitId,
                Amount = model.Amount
            };

            await _transactionService.UpdateAsync(transaction);
            return RedirectToAction(nameof(Index));
        }
    }
}
