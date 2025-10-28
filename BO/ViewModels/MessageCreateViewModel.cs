using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class MessageCreateViewModel
    {
        [Required] 
        public string Text { get; set; }

        [Required]
        [Display(Name = "Units")]
        public List<int> SelectedUnitIds { get; set; }

        public IEnumerable<SelectListItem>? Units { get; set; }
    }
}
