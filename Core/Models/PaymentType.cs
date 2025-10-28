using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PaymentType : BaseModel
    {
        public ICollection<Payment> Payments { get; set; }
    }
}
