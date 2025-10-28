using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Transaction : BaseModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int PaymentId { get; set; }
        public int UnitId { get; set; } 
        public decimal Amount { get; set; } 
        public Payment Payment { get; set; }
        public Unit Unit { get; set; }
    }
}
