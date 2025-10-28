using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Payment Type")]
        [Required]
        public int PaymentTypeId { get; set; }

        public string? PaymentTypeName { get; set; }

        public IEnumerable<SelectListItem> PaymentTypes { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
