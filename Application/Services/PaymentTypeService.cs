using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository)
        {
            _paymentTypeRepository = paymentTypeRepository;
        }

        public IQueryable<PaymentType> GetAll()
        {
            return _paymentTypeRepository.GetAll();
        }

        public async Task<PaymentType> GetByIdAsync(int id)
        {
            return await _paymentTypeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(PaymentType paymentType)
        {
            await _paymentTypeRepository.AddAsync(paymentType);
        }

        public async Task UpdateAsync(PaymentType paymentType)
        {
            await _paymentTypeRepository.UpdateAsync(paymentType);
        }

        public async Task DeleteAsync(int id)
        {
            await _paymentTypeRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<PaymentType>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _paymentTypeRepository.GetPaginatedAsync(pageNumber, pageSize);
        }
    }
}
