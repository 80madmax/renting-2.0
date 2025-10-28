using Core.Filters;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IPaginatedList<Transaction>> GetPaginatedWithFiltersAsync(TransactionFilter filter, int pageNumber, int pageSize);

        Task<IEnumerable<Transaction>> GetFilteredTransactions(TransactionFilter filter);
    }
}
