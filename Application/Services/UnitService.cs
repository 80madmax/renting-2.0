using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public IQueryable<Unit> GetAll()
        {
            return _unitRepository.GetAll();
        }

        public async Task<Unit> GetByIdAsync(int id)
        {
            return await _unitRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Unit unit)
        {
            await _unitRepository.AddAsync(unit);
        }

        public async Task UpdateAsync(Unit unit)
        {
            await _unitRepository.UpdateAsync(unit);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Unit>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _unitRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<Unit>> GetPaginatedWithDistrictCityCountryFloorUnitTypeAsync(int pageNumber, int pageSize)
        {
            return await _unitRepository.GetPaginatedWithDistrictCityCountryFloorUnitTypeAsync(pageNumber, pageSize);
        }

      
    }
}
