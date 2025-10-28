using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentController(IPaymentService paymentService, IPaymentTypeService paymentTypeService)
        {
            _paymentService = paymentService;
            _paymentTypeService = paymentTypeService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
        {
            var paged = await _paymentService.GetPaginatedWithPaymentTypeAsync(pageNumber, pageSize);

            var model = new PaymentListViewModel
            {
                Payments = paged.Select(c => new PaymentViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    PaymentTypeId = c.PaymentTypeId,
                    PaymentTypeName = c.PaymentType.Name
                }).ToList(),
                PageIndex = paged.PageIndex,
                TotalPages = paged.TotalPages
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var paymentTypes = _paymentTypeService.GetAll();

            var model = new PaymentViewModel
            {
                PaymentTypes = paymentTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulatePaymentTypesAsync(model);
                return View(model);
            }

            var payment = new Payment
            {
                Name = model.Name,
                PaymentTypeId = model.PaymentTypeId
            };

            await _paymentService.AddAsync(payment);
            return RedirectToAction(nameof(Index));
        }

        private void PopulatePaymentTypesAsync(PaymentViewModel model)
        {
            var paymentTypes = _paymentTypeService.GetAll();
            model.PaymentTypes = paymentTypes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();

            var paymentTypes = _paymentTypeService.GetAll();

            var model = new PaymentViewModel
            {
                Id = payment.Id,
                Name = payment.Name,
                PaymentTypeId = payment.PaymentTypeId,
                PaymentTypes = paymentTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulatePaymentTypesAsync(model);

                return View(model);
            }

            var payment = new Payment
            {
                Id = model.Id,
                Name = model.Name,
                PaymentTypeId = model.PaymentTypeId
            };

            await _paymentService.UpdateAsync(payment);
            return RedirectToAction(nameof(Index));
        }
        

        public async Task<IActionResult> Details(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound();

            var model = new PaymentViewModel
            {
                Id = payment.Id,
                Name = payment.Name,
                PaymentTypeId = payment.PaymentTypeId,
                PaymentTypeName = payment.PaymentType.Name
            };

            return View(model);
        }

        

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
