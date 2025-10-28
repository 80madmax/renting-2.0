using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDistrictService : IBaseService<District>
    {
        Task<IPaginatedList<District>> GetPaginatedWithCitiesAndCountriesAsync(int pageNumber, int pageSize);
        Task<IEnumerable<District>> GetByCityId(int cityId);
    }
}
