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
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<Message> _dbSet;

        public MessageRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Message>();
        }

        public override async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.Include(c => c.Unit).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Message>> GetByUnitId(int unitId)
        {
            return await _context.Messages.Where(m=>m.UnitId == unitId).ToListAsync();
        }

        public async Task<IPaginatedList<Message>> GetPaginatedWithUnitsAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c => c.Unit);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }

        public IQueryable<Message> GetAllWithUnits()
        {
            return _dbSet.Include(m=>m.Unit).AsQueryable();
        }

    }
}
