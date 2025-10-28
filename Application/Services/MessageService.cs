using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public IQueryable<Message> GetAll()
        {
            return _messageRepository.GetAll();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _messageRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Message message)
        {
            await _messageRepository.AddAsync(message);
        }

        public async Task UpdateAsync(Message message)
        {
            await _messageRepository.UpdateAsync(message);
        }

        public async Task DeleteAsync(int id)
        {
            await _messageRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Message>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _messageRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<Message>> GetPaginatedWithUnitsAsync(int pageNumber, int pageSize)
        {
            return await _messageRepository.GetPaginatedWithUnitsAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<Message>> GetByUnitId(int unitId)
        {
            return await _messageRepository.GetByUnitId(unitId);
        }

        public IQueryable<Message> GetAllWithUnits()
        {
            return _messageRepository.GetAllWithUnits();
        }
    }
}
