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
    public class CommunalBillController : Controller
    {
        private readonly ICommunalBillService _communalBillService;
        private readonly IUnitService _unitService;
        private readonly ICommunalExpenseService _communalExpenseService;

        public CommunalBillController(ICommunalBillService communalBillService, IUnitService unitService, ICommunalExpenseService communalExpenseService)
        {
            _communalBillService = communalBillService;
            _unitService = unitService;
            _communalExpenseService = communalExpenseService;
        }

        public async Task<IActionResult> Create()
        {
            var now = DateTime.Now;
            var communalExpenses = await _communalExpenseService.GetAllOrdered();

            var model = new CommunalBillCreateViewModel
            {
                Year = now.Year,
                Month = now.Month,

                Units = _unitService.GetAll().Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                }),

                CommunalExpenses = communalExpenses.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
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
        public async Task<IActionResult> Create(CommunalBillCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns
                model.Units = _unitService.GetAll().Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name });

                var communalExpenses = await _communalExpenseService.GetAllOrdered();

                model.CommunalExpenses = communalExpenses.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });

                model.Years = Enumerable.Range(DateTime.Now.Year - 5, 11).Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() });
                model.Months = Enumerable.Range(1, 12).Select(m => new SelectListItem
                {
                    Value = m.ToString(),
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
                });

                return View(model);
            }

            //var payment = await _paymentService.GetByIdAsync(model.PaymentId);


            foreach (var unitId in model.SelectedUnitIds)
            {
                foreach (var communalExpenseId in model.SelectedCommunalExpenseIds)
                {
                    var communalBill = new CommunalBill
                    {
                        UnitId = unitId,
                        CommunalExpenseId = communalExpenseId,
                        Year = model.Year,
                        Month = model.Month                      
                    };

                    await _communalBillService.AddAsync(communalBill);
                }              
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(CommunalBillFilterViewModel filter, int pageNumber = 1, int pageSize = 10)
        {
            if (filter.IsInitialLoad)
            {
                filter.SelectedMonth ??= DateTime.Now.Month;

                filter.SelectedYear ??= DateTime.Now.Year;

            }

            // Map the filter to domain filter
            var filterEntity = new CommunalBillFilter
            {
                UnitId = filter.SelectedUnitId,
                MonthId = filter.SelectedMonth,
                YearId = filter.SelectedYear,
                CommunalExpenseId = filter.SelectedCommunalExpenseId
            };

            // Get paginated, filtered result
            var paginated = await _communalBillService.GetPaginatedWithFiltersAsync(filterEntity, pageNumber, pageSize);

            // Fetch dropdown sources
            var units = await _unitService.GetAllWithDistrictCityFloor();
            var communalExpense = await _communalExpenseService.GetAllOrdered();

            // Build view model
            var viewModel = new CommunalBillListViewModel
            {
                Filter = new CommunalBillFilterViewModel
                {
                    SelectedUnitId = filter.SelectedUnitId,
                    SelectedMonth = filter.SelectedMonth,
                    SelectedYear = filter.SelectedYear,
                    SelectedCommunalExpenseId = filter.SelectedCommunalExpenseId,
                    IsInitialLoad = filter.IsInitialLoad,

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

                    CommunalExpenses = communalExpense.Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name,
                        Selected = (p.Id == filter.SelectedCommunalExpenseId)
                    })
                },

                CommunalBills = paginated.Items.Select(t => new CommunalBillViewModel
                {
                    Id = t.Id,                  
                    Month = t.Month,
                    Year = t.Year,
                    CommunalExpense = t.CommunalExpense.Name,
                    Unit = $"{ t.Unit.Name + "," + t.Unit.Floor.Name + "," + t.Unit.Address + "," + t.Unit.District.Name}"

                }).ToList(),

                PageIndex = paginated.PageIndex,
                TotalPages = paginated.TotalPages
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _communalBillService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var communalBill = await _communalBillService.GetByIdWithDetails(id);
            if (communalBill == null) return NotFound();

            var model = new CommunalBillViewModel
            {
                Id = communalBill.Id,
                Unit = $"{communalBill.Unit.Name + "," + communalBill.Unit.Floor.Name + "," + communalBill.Unit.Address + "," + communalBill.Unit.District.Name}",
                CommunalExpense = communalBill.CommunalExpense.Name,
                Month = communalBill.Month,
                Year = communalBill.Year
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var communalBill = await _communalBillService.GetByIdAsync(id);

            // Fetch dropdown sources
            var units = await _unitService.GetAllWithDistrictCityFloor();
            var communalExpenses = await _communalExpenseService.GetAllOrdered();

            var model = new CommunalBillEditViewModel
            {
                CommunalBillId = communalBill.Id,
                UnitId = communalBill.UnitId,
                CommunalExpenseId = communalBill.CommunalExpenseId,
                Month = communalBill.Month,
                Year = communalBill.Year,          
                Units = units.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Name} - {u.Floor.Name} - {u.Address} - {u.District.Name}"
                }),

                CommunalExpenses = communalExpenses.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
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
        public async Task<IActionResult> Edit(CommunalBillEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var communalBill = new CommunalBill
            {
                Id = model.CommunalBillId,              
                Month = model.Month,
                Year = model.Year,
                CommunalExpenseId = model.CommunalExpenseId,
                UnitId = model.UnitId
            };

            await _communalBillService.UpdateAsync(communalBill);
            return RedirectToAction(nameof(Index));
        }
    }

}
