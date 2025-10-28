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
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<Unit> _dbSet;

        public UnitRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Unit>();
        }

        public override async Task<Unit> GetByIdAsync(int id)
        {
            return await _context.Units.Include(c => c.Floor).Include(c => c.UnitType).Include(c => c.District).ThenInclude(c => c.City).ThenInclude(c=>c.Country).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IPaginatedList<Unit>> GetPaginatedWithDistrictCityCountryFloorUnitTypeAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c=>c.UnitType).Include(c=>c.Floor).Include(c => c.District).ThenInclude(c => c.City).ThenInclude(c => c.Country);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }
    }
}
