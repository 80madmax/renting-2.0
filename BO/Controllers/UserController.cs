using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace BO.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
     
        public UserController(IUserService userService,
                                  IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Authorize(Roles = "Super Admin")]
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
        [Authorize(Roles = "Super Admin")]
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

        [Authorize(Roles = "Super Admin")]
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
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                TempData["Success"] = "User deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Unable to delete user: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var isSuperAdmin = User.IsInRole("Super Admin");

            if (!isSuperAdmin && id != LoggedUserIdAsInt)
            {
                return Forbid();
            }

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

            ViewBag.IsSuperAdmin = isSuperAdmin;

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var isSuperAdmin = User.IsInRole("Super Admin");

            // Non-super-admins can only edit their own profile
            if (!isSuperAdmin && id != LoggedUserIdAsInt)
            {
                return Forbid();
            }

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
                IsActive = user.IsActive,
                IsSuperAdmin = isSuperAdmin
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var isSuperAdmin = User.IsInRole("Super Admin");

            // Non-super-admins can only edit their own profile
            if (!isSuperAdmin && model.Id != LoggedUserIdAsInt)
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                var roles = _roleService.GetAll();
                model.Roles = roles.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
                model.IsSuperAdmin = isSuperAdmin;

                return View(model);
            }

            var user = await _userService.GetByIdAsync(model.Id);
            user.Name = model.Name;
            user.Phone = model.Phone;
            user.Email = model.Email;

            // Only Super Admin can change role and active status
            if (isSuperAdmin)
            {
                user.RoleId = model.RoleId;
                user.IsActive = model.IsActive;
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
                user.Password = model.Password;

            await _userService.UpdateAsync(user);

            TempData["Success"] = "Profile updated successfully.";

            if (isSuperAdmin)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
    }
}
