using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class UnitViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Unit Type")]
        [Required]
        public int UnitTypeId { get; set; }

        [Display(Name = "Unit Type")]
        public string? UnitTypeName { get; set; }

        [Display(Name = "Floor")]
        [Required]
        public int FloorId { get; set; }

        [Display(Name = "Floor")]
        public string? FloorName { get; set; }

        public string Address { get; set; } = "";

        [Display(Name = "District")]
        [Required]
        public int DistrictId { get; set; }

        [Display(Name = "District")]
        public string? DistrictName { get; set; }

        [Display(Name = "City")]
        [Required]
        public int? SelectedCityId { get; set; }

        [Display(Name = "City")]
        public string? CityName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int? SelectedCountryId { get; set; }

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        public string Note { get; set; } = "";

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Total Cost")]
        public decimal Cost { get; set; } = 0;

        [Display(Name = "Renting Net Price")]
        public decimal RentPrice { get; set; } = 0;

        public int UserId { get; set; } = 2;

        public IEnumerable<SelectListItem>? Countries { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
        public IEnumerable<SelectListItem>? Districts { get; set; }
        public IEnumerable<SelectListItem>? Floors { get; set; }
        public IEnumerable<SelectListItem>? UnitTypes { get; set; }
    }
}
