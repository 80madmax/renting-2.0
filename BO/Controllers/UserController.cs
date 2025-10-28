using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace BO.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
     
        public UserController(IUserService userService,
                                  IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Create()
        {
            var roles = _roleService.GetAll();

            var model = new UserCreateViewModel
            {
                Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = _roleService.GetAll();
                model.Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(model);
            }

            var user = new User
            {
                RoleId = model.RoleId,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                IsActive = model.IsActive
            };

            await _userService.AddAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedUsers = await _userService.GetPaginatedWithRolesAsync(pageNumber, pageSize);

            var model = new UserListViewModel
            {
                Users = paginatedUsers.Items.Select(u => new UserCreateViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name,
                    Phone = u.Phone?? "",
                    IsActive = u.IsActive
                }).ToList(),
                PageIndex = paginatedUsers.PageIndex,
                TotalPages = paginatedUsers.TotalPages
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var model = new UserCreateViewModel
            {
                Id = user.Id,
                Name = user.Name,
                RoleName = user.Role.Name,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var roles = _roleService.GetAll();

            var model = new UserEditViewModel
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = _roleService.GetAll();
                model.Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(model);
            }

            var user = await _userService.GetByIdAsync(model.Id);
            user.Name = model.Name;
            user.RoleId = model.RoleId;
            user.Phone = model.Phone;
            user.Email = model.Email;
            user.IsActive = model.IsActive;

            if(!string.IsNullOrWhiteSpace(model.Password)) 
                user.Password = model.Password;

            await _userService.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
