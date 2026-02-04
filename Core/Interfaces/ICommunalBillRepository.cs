using Core.Filters;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICommunalBillRepository : IBaseRepository<CommunalBill>
    {
        Task<IPaginatedList<CommunalBill>> GetPaginatedWithFiltersAsync(CommunalBillFilter filter, int pageNumber, int pageSize);
    }
}
