using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Country : BaseModel
    {
        [ValidateNever]
        public ICollection<City> Cities { get; set; }
    }
}
