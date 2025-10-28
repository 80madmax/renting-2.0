using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class DistrictViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "District Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "City")]
        [Required]
        public int CityId { get; set; }

        [Display(Name = "City")]
        public string? CityName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int? SelectedCountryId { get; set; }

        [Display(Name = "Country")]
        public string? CountryName { get; set; }

        public IEnumerable<SelectListItem>? Countries { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
    }
}
