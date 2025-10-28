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
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<Payment> _dbSet;

        public PaymentRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Payment>();
        }

        public override async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.Include(c => c.PaymentType).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Payment>> GetAllOrderedByType()
        {
            return await _context.Payments
                                 .Include(p=>p.PaymentType)
                                 .OrderBy(p=>p.PaymentType.Name)
                                 .ThenBy(p=>p.Name)
                                 .ToListAsync();
        }

        public async Task<IPaginatedList<Payment>> GetPaginatedWithPaymentTypeAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Include(c => c.PaymentType);

            return await PaginationHelper.ToPaginatedListAsync(query, pageNumber, pageSize);
        }

    }
}
