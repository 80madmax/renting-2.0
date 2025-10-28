using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<City> _dbSet;

        public CityRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<City>();
        }

        public override async Task<City> GetByIdAsync(int id)
        {
            return await _context.Cities.Include(c => c.Country).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IPaginatedList<City>> GetPaginatedWithCountriesAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c => c.Country);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<City>> GetByCountryId(int countryId)
        {
            return await _context.Cities.Where(c => c.CountryId == countryId).ToListAsync();
        }
    }
}
