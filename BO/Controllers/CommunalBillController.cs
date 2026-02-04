using Application.Services;
using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace BO.Controllers
{
    public class CommunalBillController : Controller
    {
        private readonly ICommunalBillService _transactionService;
        private readonly IUnitService _unitService;
        private readonly ICommunalExpenseService _communalExpenseService;

        public CommunalBillController(ICommunalBillService communalBillService, IUnitService unitService, ICommunalExpenseService communalExpenseService)
        {
            _transactionService = communalBillService;
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

                    await _transactionService.AddAsync(communalBill);
                }              
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
