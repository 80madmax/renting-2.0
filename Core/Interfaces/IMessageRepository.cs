using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IPaginatedList<Message>> GetPaginatedWithUnitsAsync(int pageNumber, int pageSize);

        Task<IEnumerable<Message>> GetByUnitId(int unitId);

        IQueryable<Message> GetAllWithUnits();
    }
}
