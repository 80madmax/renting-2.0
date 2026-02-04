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
    public class CommunalBillRepository : BaseRepository<CommunalBill>, ICommunalBillRepository
    {
        public CommunalBillRepository(RentingDbContext context)
         : base(context)
        {
        }


    }
}
