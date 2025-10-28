using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    //One bed room, two bedroom...
    public class UnitType : BaseModel
    {
        [ValidateNever]
        public ICollection<Unit> Units { get; set; }
    }
}
