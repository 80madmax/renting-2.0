using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BO.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IActionResult Create()
        {
            return View(new CountryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = new Country { Id = model.Id, Name = model.Name };
                await _countryService.AddAsync(country);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await _countryService.GetPaginatedAsync(pageNumber, pageSize);

            var viewModel = new CountryListViewModel
            {
                Countries = pagedResult.Items.Select(c => new CountryViewModel
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
            var country = await _countryService.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var model = new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var country = new Country { Id = model.Id, Name = model.Name };
                await _countryService.UpdateAsync(country);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryService.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            var model = new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            await _countryService.DeleteAsync(Id);

            return RedirectToAction(nameof(Index));
        }

    }
}
