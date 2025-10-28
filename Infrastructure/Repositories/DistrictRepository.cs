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
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<District> _dbSet;

        public DistrictRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<District>();
        }

        public override async Task<District> GetByIdAsync(int id)
        {
            return await _context.Districts.Include(d => d.City).ThenInclude(c=>c.Country).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IPaginatedList<District>> GetPaginatedWithCitiesAndCountriesAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c => c.City).ThenInclude(c=>c.Country);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<District>> GetByCityId(int cityId)
        {
            return await _context.Districts.Where(d => d.CityId == cityId).ToListAsync();
        }
    }
}
