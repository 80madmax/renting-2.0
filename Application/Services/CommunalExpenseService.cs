using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommunalExpenseService : ICommunalExpenseService
    {
        private ICommunalExpenseRepository _commonExpenseRespositoy;

        public CommunalExpenseService(ICommunalExpenseRepository commonExpenseRespositoy)
        {
            _commonExpenseRespositoy = commonExpenseRespositoy;
        }

        public async Task<List<CommunalExpense>> GetAllOrdered()
        {
            return await _commonExpenseRespositoy.GetAllOrdered();
        }
    }
}
