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
    public class CommunalBillRepository : BaseRepository<CommunalBill>, ICommunalBillRepository
    {

        private readonly RentingDbContext _context;

        public CommunalBillRepository(RentingDbContext context)
         : base(context)
        {
            _context = context;
        }

        public async Task<IPaginatedList<CommunalBill>> GetPaginatedWithFiltersAsync(CommunalBillFilter filter, int pageNumber, int pageSize)
        {
            IQueryable<CommunalBill> query = _context.CommunalBills.Include(t => t.Unit)
                                             .Include(t => t.Unit.District)
                                             .Include(t => t.Unit.Floor)
                                             .Include(t => t.CommunalExpense);

            if (filter.UnitId.HasValue)
                query = query.Where(q => q.UnitId == filter.UnitId.Value);

            if (filter.CommunalExpenseId.HasValue)
                query = query.Where(q => q.CommunalExpenseId == filter.CommunalExpenseId.Value);

            if (filter.MonthId.HasValue)
                query = query.Where(q => q.Month == filter.MonthId.Value);

            if (filter.YearId.HasValue)
                query = query.Where(q => q.Year == filter.YearId.Value);


            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }
    }
}
