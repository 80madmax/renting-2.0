using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public IQueryable<Payment> GetAll()
        {
            return _paymentRepository.GetAll();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Payment payment)
        {
            await _paymentRepository.AddAsync(payment);
        }

        public async Task UpdateAsync(Payment payment)
        {
            await _paymentRepository.UpdateAsync(payment);
        }

        public async Task DeleteAsync(int id)
        {
            await _paymentRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Payment>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _paymentRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<Payment>> GetPaginatedWithPaymentTypeAsync(int pageNumber, int pageSize)
        {
            return await _paymentRepository.GetPaginatedWithPaymentTypeAsync(pageNumber, pageSize);
        }

        public async Task<List<Payment>> GetAllOrderedByType()
        {
            return await _paymentRepository.GetAllOrderedByType();
        }
    }
}
