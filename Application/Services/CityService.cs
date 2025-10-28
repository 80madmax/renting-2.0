using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IQueryable<City> GetAll()
        {
            return _cityRepository.GetAll();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(City city)
        {
            await _cityRepository.AddAsync(city);
        }

        public async Task UpdateAsync(City city)
        {
            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<City>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _cityRepository.GetPaginatedAsync(pageNumber, pageSize);
        }

        public async Task<IPaginatedList<City>> GetPaginatedWithCountriesAsync(int pageNumber, int pageSize)
        {
            return await  _cityRepository.GetPaginatedWithCountriesAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<City>> GetByCountryId(int countryId)
        {
            return await _cityRepository.GetByCountryId(countryId);
        }
    }
}
