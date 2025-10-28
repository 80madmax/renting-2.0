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
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Include(c => c.Role).FirstOrDefaultAsync(c => c.Id == c.Id);
        }


        public async Task<IPaginatedList<User>> GetPaginatedWithRolesAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c => c.Role);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }
    }
}
