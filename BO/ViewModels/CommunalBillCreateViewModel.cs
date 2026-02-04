using Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CommunalBillCreateViewModel
    {
        [Required]
        [Display(Name = "Units")]
        public List<int> SelectedUnitIds { get; set; } = new();

        [Required]
        [Display(Name = "Communal Expenses")]
        public List<int> SelectedCommunalExpenseIds { get; set; }

        [Required]
        [Display(Name = "Months")]
        public int Month { get; set; }

        [Required]
        [Display(Name = "Years")]
        public int Year { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CommunalExpenses { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Months { get; set; } = new List<SelectListItem>();
    }
}
