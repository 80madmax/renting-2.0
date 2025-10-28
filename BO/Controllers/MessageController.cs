using Application.Services;
using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BO.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUnitService _unitService;
        private readonly IMessageService _messageService;

        public MessageController(IUnitService unitService,
                                IMessageService messageService,
                                IDistrictService districtService)
        {
            _unitService = unitService;
            _messageService = messageService;
        }

        public async Task<IActionResult> Create()
        {
            var units = _unitService.GetAll();

            var model = new MessageCreateViewModel
            {
                Units = units.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var units = _unitService.GetAll();
                model.Units = units.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(model);
            }

            foreach(var unitId in model.SelectedUnitIds)
            {
                var message = new Message
                {
                    Name = model.Text,
                    UnitId = unitId
                };

                await _messageService.AddAsync(message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? unitId, int pageNumber = 1, int pageSize = 2)
        {
            // Start with base query and include Unit navigation property
            var query = _messageService.GetAllWithUnits();

            // Apply filter if a Unit ID was selected
            if (unitId.HasValue)
            {
                query = query.Where(m => m.UnitId == unitId.Value);
            }

            // Apply pagination
            var paginated = await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);

            // Map to view model
            var model = new MessageListViewModel
            {
                SelectedUnitId = unitId,
                PageIndex = paginated.PageIndex,
                TotalPages = paginated.TotalPages,
                Messages = paginated.Items.Select(m => new MessageViewModel
                {
                    Id = m.Id,
                    Text = m.Name,
                    UnitName = m.Unit?.Name ?? "(No Unit)"
                }).ToList(),
                Units = (_unitService.GetAll()).Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                })
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null) return NotFound();

            var model = new MessageViewModel
            {
                Id = message.Id,
                Text = message.Name,
                UnitName = message.Unit.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null) return NotFound();

            var units = _unitService.GetAll();
          
            var model = new MessageEditViewModel
            {
                Id = message.Id,
                Text = message.Name,
                UnitId = message.Unit.Id,
                Units = units.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })              
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MessageEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var message = new Message
            {
                Id = model.Id,
                Name = model.Text,
                UnitId = model.UnitId
            };

            await _messageService.UpdateAsync(message);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
