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
    public class CommunalExpenseRepository : BaseOnlyListRepository<CommunalExpense>, ICommunalExpenseRepository
    {
        public CommunalExpenseRepository(RentingDbContext context)
       : base(context)
        {
        }

        public async Task<List<CommunalExpense>> GetAllOrdered()
        {
            return await GetAll().OrderBy(ce => ce.Name).ToListAsync();
        }
    }
}
