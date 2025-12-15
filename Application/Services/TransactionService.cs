using Core.Filters;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public IQueryable<Transaction> GetAll()
        {
            return _transactionRepository.GetAll();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _transactionRepository.UpdateAsync(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Transaction>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _transactionRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<Transaction>> GetPaginatedWithFiltersAsync(TransactionFilter filter, int pageNumber, int pageSize)
        {
            return await _transactionRepository.GetPaginatedWithFiltersAsync(filter, pageNumber, pageSize);
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactions(TransactionFilter filter)
        {
            return await _transactionRepository.GetFilteredTransactions(filter);
        }

        public async Task<Transaction> GetByIdWithDetails(int id)
        {
            return await _transactionRepository.GetByIdWithDetails(id);
        }
    }
}
