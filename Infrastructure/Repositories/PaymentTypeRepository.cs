using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>, IPaymentTypeRepository
    {
        private readonly RentingDbContext _context;
        private readonly DbSet<PaymentType> _dbSet;

        public PaymentTypeRepository(RentingDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PaymentType>();
        }
    }
}
