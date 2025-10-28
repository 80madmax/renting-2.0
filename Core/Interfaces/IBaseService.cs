using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T country);
        Task UpdateAsync(T league);
        Task DeleteAsync(int id);

        // For pagination, if needed:
        Task<IPaginatedList<T>> GetPaginatedAsync(int pageNumber, int pageSize);


    }
}
