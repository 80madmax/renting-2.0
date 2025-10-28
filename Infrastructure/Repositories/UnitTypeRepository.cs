using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitTypeRepository : BaseRepository<UnitType>, IUnitTypeRepository
    {
        public UnitTypeRepository(RentingDbContext context) : base(context)
        {

        }
    }
}
