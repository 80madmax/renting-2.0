using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class CommunalBill
    {
        public int ID { set; get; }
        public int UnitId {  set; get; }        
        public int Year { set; get; }
        public int Month { set; get; }
        public int CommunalExpenseId { set; get; }
        [ValidateNever]
        public Unit Unit { get; set; }
        [ValidateNever]
        public CommunalExpense CommunalExpense { get; set; }

    }
}
