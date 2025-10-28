using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class City : BaseModel
    {
        public ICollection<District> Districts { get; set; }

        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; }
    }
}
