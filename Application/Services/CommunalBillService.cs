using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommunalBillService : ICommunalBillService
    {
        private readonly ICommunalBillRepository _communalBillRepository;

        public CommunalBillService(ICommunalBillRepository communalBillRepository) { 
            _communalBillRepository = communalBillRepository; 
        }

        public IQueryable<CommunalBill> GetAll()
        {
            return _communalBillRepository.GetAll();
        }

        public async Task<CommunalBill> GetByIdAsync(int id)
        {
            return await _communalBillRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CommunalBill communalBill)
        {
            await _communalBillRepository.AddAsync(communalBill);
        }

        public async Task UpdateAsync(CommunalBill communalBill)
        {
            await _communalBillRepository.UpdateAsync(communalBill);
        }

        public async Task DeleteAsync(int id)
        {
            await _communalBillRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<CommunalBill>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _communalBillRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

    }
}
