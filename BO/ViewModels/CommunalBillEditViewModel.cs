using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class CommunalBillEditViewModel
    {
        [Required]
        public int CommunalBillId { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }

        [Required]
        [Display(Name = "Communal Expense")]
        public int CommunalExpenseId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CommunalExpenses { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Months { get; set; } = new List<SelectListItem>();
    }
}
