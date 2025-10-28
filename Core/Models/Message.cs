using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Message : BaseModel
    {
        public int UnitId { get; set; }

        [ValidateNever]
        public Unit Unit { get; set; }
    }
}
