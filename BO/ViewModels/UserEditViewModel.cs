using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BO.ViewModels
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Role")]
        [Required]
        public int RoleId { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public string? Password { get; set; }

        [Required]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
