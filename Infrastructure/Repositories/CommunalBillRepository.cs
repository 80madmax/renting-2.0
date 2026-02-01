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
    public class CommunalBillRepository : BaseOnlyListRepository<CommunalBill>, ICommunalBillRepository
    {
        public CommunalBillRepository(RentingDbContext context)
         : base(context)
        {
        }

        public  async Task<List<CommunalBill>> GetAllOrdered()
        {
            return await GetAll().OrderByDescending(cb => cb.Year).ThenByDescending(cb => cb.Month).ToListAsync();
        }
    }
}
