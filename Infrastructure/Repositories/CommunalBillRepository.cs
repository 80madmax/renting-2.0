using Core.Filters;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
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
                                             .Include(t => t.CommunalExpense)
                                             .OrderBy(t => t.Unit.Name)
                                             .ThenBy(t => t.Unit.Address)
                                             .ThenBy(t => t.Unit.District.Name)
                                             .ThenBy(t => t.Unit.District.City.Name)
                                             .ThenBy(t => t.CommunalExpense.Name);


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

        public async Task<CommunalBill> GetByIdWithDetails(int id)
        {
            return await _context.CommunalBills
                                .Include(t=>t.Unit)
                                    .ThenInclude(u => u.District)
                                .Include(t => t.Unit)
                                    .ThenInclude(u => u.Floor)
                                .Include(t => t.CommunalExpense)
                                .FirstOrDefaultAsync(t => t.Id == id);
        }

      
    }
}
