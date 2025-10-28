using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }

        public string? CountryName { get; set; } 

        public IEnumerable<SelectListItem> Countries { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
