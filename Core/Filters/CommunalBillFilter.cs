using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filters
{
    public class CommunalBillFilter
    {
        public int? UnitId { get; set; }
        public int? CommunalExpenseId { get; set; }
        public int? MonthId { get; set; }
        public int? YearId { get; set; }
    }
}
