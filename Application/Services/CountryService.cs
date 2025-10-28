using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public IQueryable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Country country)
        {
            await _countryRepository.AddAsync(country);
        }

        public async Task UpdateAsync(Country country)
        {
            await _countryRepository.UpdateAsync(country);
        }

        public async Task DeleteAsync(int id)
        {
            await _countryRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Country>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _countryRepository.GetPaginatedAsync(pageNumber, pageSize);
        }
    }
}
