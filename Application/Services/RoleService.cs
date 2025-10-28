using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            await _roleRepository.UpdateAsync(role);
        }

        public async Task DeleteAsync(int id)
        {
            await _roleRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Role>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _roleRepository.GetPaginatedAsync(pageNumber, pageSize);
        }
    }
}
