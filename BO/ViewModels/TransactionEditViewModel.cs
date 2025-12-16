using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class TransactionEditViewModel
    {
        [Required]
        public int TransactionId { get; set; } 
     
        [Required]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public int PaymentId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string? Note { get; set; }

        public IEnumerable<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Payments { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Months { get; set; } = new List<SelectListItem>();
    }
}
