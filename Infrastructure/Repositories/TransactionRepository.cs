using Core.Filters;
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
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<Transaction> _dbSet;

        public TransactionRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Transaction>();
        }

        public async Task<IPaginatedList<Transaction>> GetPaginatedWithFiltersAsync(TransactionFilter filter, int pageNumber, int pageSize)
        {
            IQueryable<Transaction> query = _context.Transactions.Include(t => t.Unit)
                                             .Include(t => t.Unit.District)                                     
                                             .Include(t => t.Unit.Floor)
                                             .Include(t => t.Payment)
                                             .Include(t => t.Payment.PaymentType);

            if (filter.UnitId.HasValue)
                query = query.Where(q => q.UnitId == filter.UnitId.Value);

            if (filter.PaymentId.HasValue)
                query = query.Where(q => q.PaymentId == filter.PaymentId.Value);

            if (filter.MonthId.HasValue)
                query = query.Where(q => q.Month == filter.MonthId.Value);

            if (filter.YearId.HasValue)
                query = query.Where(q => q.Year == filter.YearId.Value);


            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactions(TransactionFilter filter)
        {
            var query = _context.Transactions.Include(t => t.Unit).Include(t => t.Payment).Include(t => t.Payment.PaymentType).AsQueryable();

            if (filter.UnitId.HasValue)
                query = query.Where(q => q.UnitId == filter.UnitId.Value);

            if (filter.PaymentId.HasValue)
                query = query.Where(q => q.PaymentId == filter.PaymentId.Value);

            if (filter.MonthId.HasValue)
                query = query.Where(q => q.Month == filter.MonthId.Value);

            if (filter.YearId.HasValue)
                query = query.Where(q => q.Year == filter.YearId.Value);


            return await query.ToListAsync();
        }

        public async Task<Transaction> GetByIdWithDetails(int id)
        {
            return await _context.Transactions.Include(t => t.Unit)
                                              .Include(t => t.Unit.Floor)
                                              .Include(t => t.Unit.District)
                                              .Include(t => t.Payment)
                                              .Include(t => t.Payment.PaymentType).FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
