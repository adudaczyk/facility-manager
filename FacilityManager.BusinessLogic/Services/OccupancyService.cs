using AutoMapper;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.EntityFramework.Models;
using FacilityManager.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public class OccupancyService : IOccupancyService
    {
        private readonly IOccupancyRepository _occupancyRepository;
        private readonly IMapper _mapper;

        public OccupancyService(IOccupancyRepository occupancyRepository, IMapper mapper)
        {
            _occupancyRepository = occupancyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OccupancyDto>> GetOccupancies()
        {
            var occupancies = await _occupancyRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<OccupancyDto>>(occupancies);
        }

        public async Task<OccupancyDto> GetOccupancy(string guid)
        {
            var occupancy = await _occupancyRepository.GetByGuid(guid);

            return _mapper.Map<OccupancyDto>(occupancy);
        }

        public async Task CreateOccupancy(OccupancyDto occupancyDto)
        {
            var occupancy = _mapper.Map<Occupancy>(occupancyDto);

            occupancy.Guid = Guid.NewGuid();
            occupancy.CreationDate = DateTime.Now;

            _occupancyRepository.Add(occupancy);
            await _occupancyRepository.SaveChangesAsync();
        }

        public async Task UpdateOccupancy(OccupancyDto occupancyDto)
        {
            var occupancy = await _occupancyRepository.GetByGuid(occupancyDto.Guid.ToString());

            if (occupancy == null)
            {
                throw new KeyNotFoundException($"Cannot update Occupancy. Occupancy with guid {occupancy.Guid} does not exist");
            }

            _mapper.Map(occupancyDto, occupancy);

            _occupancyRepository.Update(occupancy);
            await _occupancyRepository.SaveChangesAsync();
        }

        public async Task DeleteOccupancy(string guid)
        {
            var occupancy = await _occupancyRepository.GetByGuid(guid);

            _occupancyRepository.Delete(occupancy);
            await _occupancyRepository.SaveChangesAsync();
        }
    }
}