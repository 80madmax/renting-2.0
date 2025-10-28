using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictService(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public IQueryable<District> GetAll()
        {
            return _districtRepository.GetAll();
        }

        public async Task<District> GetByIdAsync(int id)
        {
            return await _districtRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(District district)
        {
            await _districtRepository.AddAsync(district);
        }

        public async Task UpdateAsync(District district)
        {
            await _districtRepository.UpdateAsync(district);
        }

        public async Task DeleteAsync(int id)
        {
            await _districtRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<District>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _districtRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<District>> GetPaginatedWithCitiesAndCountriesAsync(int pageNumber, int pageSize)
        {
            return await _districtRepository.GetPaginatedWithCitiesAndCountriesAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<District>> GetByCityId(int cityId)
        {
            return await _districtRepository.GetByCityId(cityId);
        }
    }
}
