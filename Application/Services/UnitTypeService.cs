using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly IUnitTypeRepository _unitTypeRepository;

        public UnitTypeService(IUnitTypeRepository unitTypeRepository)
        {
            _unitTypeRepository = unitTypeRepository;
        }

        public IQueryable<UnitType> GetAll()
        {
            return _unitTypeRepository.GetAll();
        }

        public async Task<UnitType> GetByIdAsync(int id)
        {
            return await _unitTypeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(UnitType unitType)
        {
            await _unitTypeRepository.AddAsync(unitType);
        }

        public async Task UpdateAsync(UnitType unitType)
        {
            await _unitTypeRepository.UpdateAsync(unitType);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitTypeRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<UnitType>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _unitTypeRepository.GetPaginatedAsync(pageNumber, pageSize);
        }
    }
}
