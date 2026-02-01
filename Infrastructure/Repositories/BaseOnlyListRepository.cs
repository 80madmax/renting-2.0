using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseOnlyListRepository<T> : IBaseOnlyListRepository<T> where T : class
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseOnlyListRepository(RentingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
    }
}
