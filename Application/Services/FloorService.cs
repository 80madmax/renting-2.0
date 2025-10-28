using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public IQueryable<Floor> GetAll()
        {
            return _floorRepository.GetAll();
        }

        public async Task<Floor> GetByIdAsync(int id)
        {
            return await _floorRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Floor floor)
        {
            await _floorRepository.AddAsync(floor);
        }

        public async Task UpdateAsync(Floor floor)
        {
            await _floorRepository.UpdateAsync(floor);
        }

        public async Task DeleteAsync(int id)
        {
            await _floorRepository.DeleteAsync(id);
        }

        public async Task<IPaginatedList<Floor>> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _floorRepository.GetPaginatedAsync(pageNumber, pageSize);
        }
    }
}
