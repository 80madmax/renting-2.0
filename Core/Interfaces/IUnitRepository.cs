using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        Task<IPaginatedList<Unit>> GetPaginatedWithDistrictCityCountryFloorUnitTypeAsync(int pageNumber, int pageSize);
        Task<List<Unit>> GetAllWithDistrictCityFloor();
    }
}
