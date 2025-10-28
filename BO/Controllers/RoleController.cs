using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BO.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role { Id = model.Id, Name = model.Name };
                await _roleService.AddAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await _roleService.GetPaginatedAsync(pageNumber, pageSize);

            var viewModel = new RoleListViewModel
            {
                Roles = pagedResult.Items.Select(c => new RoleViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList(),
                PageIndex = pagedResult.PageIndex,
                TotalPages = pagedResult.TotalPages
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role { Id = model.Id, Name = model.Name };
                await _roleService.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = await _roleService.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _roleService.DeleteAsync(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
