using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BO.Controllers
{
    public class DistrictController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;

        public DistrictController(ICountryService countryService,
                                  ICityService cityService,
                                  IDistrictService districtService)
        {
            _countryService = countryService;
            _cityService = cityService;
            _districtService = districtService;
        }

        public async Task<IActionResult> Create()
        {
            var countries = _countryService.GetAll();

            var model = new DistrictViewModel
            {
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Cities = new List<SelectListItem>(), // initially empty
                SelectedCountryId = null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DistrictViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var countries = _countryService.GetAll();
                model.Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                if (model.SelectedCountryId.HasValue)
                {
                    var cities = await _cityService.GetByCountryId(model.SelectedCountryId.Value);
                    model.Cities = cities.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
                }

                return View(model);
            }

            var district = new District
            {
                CityId = model.CityId,
                Name = model.Name
            };

            await _districtService.AddAsync(district);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesByCountry(int countryId)
        {
            var cities = await _cityService.GetByCountryId(countryId);

            var cityList = cities.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return Json(cityList);
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedDistricts = await _districtService.GetPaginatedWithCitiesAndCountriesAsync(pageNumber, pageSize);

            var model = new DistrictListViewModel
            {
                Districts = paginatedDistricts.Items.Select(d => new DistrictViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    CityName = d.City?.Name ?? "",
                    CountryName = d.City?.Country?.Name ?? ""
                }).ToList(),
                PageIndex = paginatedDistricts.PageIndex,
                TotalPages = paginatedDistricts.TotalPages
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _districtService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            if (district == null) return NotFound();

            var model = new DistrictViewModel
            {
                Id = district.Id,
                Name = district.Name,
                CityName = district.City.Name,
                CountryName = district.City.Country.Name
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            if (district == null) return NotFound();

            var countries = _countryService.GetAll();
            var cities = _cityService.GetAll().Where(c=>c.CountryId == district.City.CountryId).ToList();
            var selectedCountryId = district.City?.CountryId;

            var model = new DistrictViewModel
            {
                Id = district.Id,
                Name = district.Name,
                SelectedCountryId = selectedCountryId,
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == selectedCountryId
                }),
                CityId = district.CityId,
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == district.CityId
                }),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DistrictViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCitiesCountriesAsync(model);

                return View(model);
            }

            var district = new District
            {
                Id = model.Id,
                Name = model.Name,
                CityId = model.CityId
            };

            await _districtService.UpdateAsync(district);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCitiesCountriesAsync(DistrictViewModel model)
        {
            var countries = _countryService.GetAll();
            var cities = _cityService.GetAll();

            model.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

          model.Cities = cities
         .Where(c => model.SelectedCountryId == null || c.CountryId == model.SelectedCountryId)
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),
             Text = c.Name
         });
        }

    }
}
