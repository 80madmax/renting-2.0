using Application.Services;
using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.Controllers
{
    public class CityController : Controller
    {       
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 2)
        {
            var paged = await _cityService.GetPaginatedWithCountriesAsync(pageNumber, pageSize);

            var model = new CityListViewModel
            {
                Cities = paged.Select(c => new CityViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId,
                    CountryName = c.Country.Name
                }).ToList(),
                PageIndex = paged.PageIndex,
                TotalPages = paged.TotalPages
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var countries = _countryService.GetAll();

            var model = new CityViewModel
            {
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCountriesAsync(model);
                return View(model);
            }

            var city = new City
            {
                Name = model.Name,
                CountryId = model.CountryId
            };

            await _cityService.AddAsync(city);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateCountriesAsync(CityViewModel model)
        {
            var countries = _countryService.GetAll();
            model.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null) return NotFound();

            var countries =  _countryService.GetAll();

            var model = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                Countries = countries.Select(c=> new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCountriesAsync(model);

                return View(model);
            }

            var city = new City
            {
                Id = model.Id,
                Name = model.Name,
                CountryId = model.CountryId
            };

            await _cityService.UpdateAsync(city);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null) return NotFound();

            var model = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = city.Country.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
