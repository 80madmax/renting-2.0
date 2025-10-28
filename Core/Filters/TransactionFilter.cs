using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Filters
{
    public class TransactionFilter
    {
        //int? unitId, int? paymentId, int? MonthId, int? yearId
        public int? UnitId { get; set; }
        public int? PaymentId {  get; set; }
        public int? MonthId {  get; set; }
        public int? YearId { get; set; }
    }
}
