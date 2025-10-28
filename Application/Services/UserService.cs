using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<User>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _userRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<User>> GetPaginatedWithRolesAsync(int pageNumber, int pageSize)
        {
            return await _userRepository.GetPaginatedWithRolesAsync(pageNumber, pageSize);
        }
    }
}
