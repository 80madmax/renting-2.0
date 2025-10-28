using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    //Bills / VAT / Tax / Commssion / Ensuarances / Rent
    public class Payment : BaseModel
    {
        public int PaymentTypeId { get; set; }

        [ValidateNever]
        public PaymentType PaymentType { get; set; }
    }
}
