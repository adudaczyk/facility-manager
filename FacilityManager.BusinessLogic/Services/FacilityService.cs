using AutoMapper;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.EntityFramework.Models;
using FacilityManager.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMapper _mapper;

        public FacilityService(IFacilityRepository facilityRepository, IMapper mapper)
        {
            _facilityRepository = facilityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacilityDto>> GetFacilities()
        {
            var facilities = await _facilityRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<FacilityDto>>(facilities);
        }

        public async Task<FacilityDto> GetFacility(string guid)
        {
            var facility = await _facilityRepository.GetByGuid(guid);

            return _mapper.Map<FacilityDto>(facility);
        }

        public async Task AddFacility(FacilityDto facilityDto)
        {
            var facility = _mapper.Map<Facility>(facilityDto);

            facility.Guid = Guid.NewGuid();
            facility.CreationDate = DateTime.Now;

            _facilityRepository.Add(facility);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task UpdateFacility(FacilityDto facilityDto)
        {
            var facility = await _facilityRepository.GetByGuid(facilityDto.Guid.ToString());

            if (facility == null)
            {
                throw new KeyNotFoundException($"Cannot update Facility. Facility with guid {facility.Guid} does not exist");
            }

            _mapper.Map(facilityDto, facility);

            _facilityRepository.Update(facility);
            await _facilityRepository.SaveChangesAsync();
        }

        public async Task DeleteFacility(string guid)
        {
            var facility = await _facilityRepository.GetByGuid(guid);

            _facilityRepository.Delete(facility);
            await _facilityRepository.SaveChangesAsync();
        }
    }
}