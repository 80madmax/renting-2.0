using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class MessageEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        public IEnumerable<SelectListItem>? Units { get; set; }
    }
}
