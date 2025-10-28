using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<IPaginatedList<City>> GetPaginatedWithCountriesAsync(int pageNumber, int pageSize);

        Task<IEnumerable<City>> GetByCountryId(int countryId);
    }
}
