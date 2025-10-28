using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Role : BaseModel
    {
        [ValidateNever]
        public List<User> Users { get; set; }
    }
}
