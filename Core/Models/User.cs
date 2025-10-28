using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Models
{
    public class User : BaseModel
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [ValidateNever]
        public string Password { get; set; }
        [ValidateNever]
        public string Phone { get; set; }
        public int RoleId { get; set; }
        [ValidateNever]
        public Role Role { get; set; }
        public bool IsActive { get; set; }
      
    }
}
