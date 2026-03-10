using Application.UseCases;
using BO.ViewModels;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace BO.Controllers
{
    public class UnitController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IDistrictService _districtService;
        private readonly IUnitService _unitService;
        private readonly IFloorService _floorService;
        private readonly IUnitTypeService _unitTypeService;

        public UnitController(ICountryService countryService,
                                  ICityService cityService,
                                  IDistrictService districtService,
                                  IUnitService unitService,
                                  IFloorService floorService,
                                  IUnitTypeService unitTypeService)
        {
            _countryService = countryService;
            _cityService = cityService;
            _districtService = districtService;
            _unitService = unitService;
            _floorService = floorService;
            _unitTypeService = unitTypeService;
        }

        public async Task<IActionResult> Create()
        {
            var countries = _countryService.GetAll();
            var unitTypes = _unitTypeService.GetAll();
            var floors = _floorService.GetAll();

            var model = new UnitViewModel
            {
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                UnitTypes = unitTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Floors = floors.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),

                Cities = new List<SelectListItem>(),
                Districts = new List<SelectListItem>(),
                SelectedCountryId = null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitViewModel model)
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

                if (model.SelectedCityId.HasValue)
                {
                    var districts = await _districtService.GetByCityId(model.SelectedCityId.Value);
                    model.Districts = districts.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
                }

                return View(model);
            }

            var unit = new Unit
            {
                Name = model.Name,
                Address = model.Address,
                TelegramChatId = model.Chat,             
                DistrictID = model.DistrictId,
                FloorID = model.FloorId,
                UnitTypeId = model.UnitTypeId,
                Note = model.Note,
                IsAvailable = model.IsAvailable,
                Cost = model.Cost,
                RentPrice = model.RentPrice,
                CorporateTax = model.CorporateTax,
                VatTax = model.VatTax,
                UserId = model.UserId
            };


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

        [HttpGet]
        public async Task<IActionResult> GetDistrictsByCity(int cityId)
        {
            var districts = await _districtService.GetByCityId(cityId);

            var districtList = districts.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });

            return Json(districtList);
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var paginatedUnits = await _unitService.GetPaginatedWithDistrictCityCountryFloorUnitTypeAsync(pageNumber, pageSize);

            var model = new UnitListViewModel
            {
                Units = paginatedUnits.Items.Select(d => new UnitViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    DistrictName = d.District?.Name ?? "",
                    Address = d.Address,
                    FloorName = d.Floor.Name,
                    UnitTypeName = d.UnitType.Name,
                    Cost = d.Cost,
                    RentPrice = d.RentPrice,
                    IsAvailable = d.IsAvailable
                }).ToList(),
                PageIndex = paginatedUnits.PageIndex,
                TotalPages = paginatedUnits.TotalPages
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitService.DeleteAsync(id);
                TempData["Success"] = "Unit deleted successfully.";
            }
            catch (Exception ex)
            {
                // consider logging in real code
                TempData["Error"] = $"Unable to delete unit: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            var model = new UnitViewModel
            {
                Id = unit.Id,
                Name = unit.Name,
                FloorName = unit.Floor.Name,
                UnitTypeName = unit.UnitType.Name,
                Address = unit.Address,
                DistrictName=unit.District?.Name ?? string.Empty,
                CityName = unit.District?.City.Name ?? string.Empty,
                CountryName = unit.District?.City.Country.Name ?? string.Empty,
                Cost = unit.Cost,
                RentPrice = unit.RentPrice,  
                CorporateTax = unit.CorporateTax,
                VatTax = unit.VatTax,
                Chat = unit.TelegramChatId,
                IsAvailable = unit.IsAvailable
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var unit = await _unitService.GetByIdAsync(id);
            if (unit == null) return NotFound();

            var countries = _countryService.GetAll();
            var cities = _cityService.GetAll().Where(c=>c.CountryId == unit.District.City.CountryId).ToList();
            var districts = _districtService.GetAll().Where(d=>d.CityId == unit.District.CityId).ToList();
            var floors = _floorService.GetAll();
            var unitTypes = _unitTypeService.GetAll();
            
            var model = new UnitViewModel
            {
                Id = unit.Id,
                Name = unit.Name,
                Address = unit.Address,
                FloorId = unit.FloorID,
                Floors = floors.Select(f=> new SelectListItem
                { 
                    Value = f.Id.ToString(),
                    Text = f.Name
                }),
                UnitTypeId = unit.UnitTypeId,
                UnitTypes = unitTypes.Select(ut => new SelectListItem
                {
                    Value = ut.Id.ToString(),
                    Text = ut.Name
                }),
                SelectedCountryId = unit.District.City?.CountryId,
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                SelectedCityId = unit.District.CityId,
                Cities = cities.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                DistrictId = unit.DistrictID,
                Districts = districts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }),
                Cost = unit.Cost,
                RentPrice = unit.RentPrice,
                CorporateTax = unit.CorporateTax,
                VatTax = unit.VatTax,
                Chat = unit.TelegramChatId,
                IsAvailable = unit.IsAvailable
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitViewModel model)
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

                if (model.SelectedCityId.HasValue)
                {
                    var districts = await _districtService.GetByCityId(model.SelectedCityId.Value);
                    model.Districts = districts.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    });
                }

                return View(model);
            }

            var unit = new Unit
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                TelegramChatId = model.Chat,
                DistrictID = model.DistrictId,
                FloorID = model.FloorId,
                UnitTypeId = model.UnitTypeId,
                Note = model.Note,
                IsAvailable = model.IsAvailable,
                Cost = model.Cost,
                RentPrice = model.RentPrice,
                CorporateTax = model.CorporateTax,
                VatTax = model.VatTax,
                UserId = model.UserId
            };


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
