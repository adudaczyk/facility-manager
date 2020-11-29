using AutoMapper;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.EntityFramework.Models;
using FacilityManager.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public class SubfacilityService : ISubfacilityService
    {
        private readonly ISubfacilityRepository _subfacilityRepository;
        private readonly IMapper _mapper;

        public SubfacilityService(ISubfacilityRepository subfacilityRepository, IMapper mapper)
        {
            _subfacilityRepository = subfacilityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubfacilityDto>> GetSubfacilities()
        {
            var subfacilities = await _subfacilityRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SubfacilityDto>>(subfacilities);
        }

        public async Task<SubfacilityDto> GetSubfacility(string guid)
        {
            var subfacility = await _subfacilityRepository.GetByGuid(guid);

            return _mapper.Map<SubfacilityDto>(subfacility);
        }

        public async Task CreateSubfacility(SubfacilityDto subfacilityDto)
        {
            var subfacility = _mapper.Map<Subfacility>(subfacilityDto);

            subfacility.Guid = Guid.NewGuid();
            subfacility.CreationDate = DateTime.Now;

            _subfacilityRepository.Add(subfacility);
            await _subfacilityRepository.SaveChangesAsync();
        }

        public async Task UpdateSubfacility(SubfacilityDto facilityDto)
        {
            var subfacility = await _subfacilityRepository.GetByGuid(facilityDto.Guid.ToString());

            if (subfacility == null)
            {
                throw new KeyNotFoundException($"Cannot update Subfacility. Subfacility with guid {subfacility.Guid} does not exist");
            }

            _mapper.Map(facilityDto, subfacility);

            _subfacilityRepository.Update(subfacility);
            await _subfacilityRepository.SaveChangesAsync();
        }

        public async Task DeleteSubfacility(string guid)
        {
            var subfacility = await _subfacilityRepository.GetByGuid(guid);

            _subfacilityRepository.Delete(subfacility);
            await _subfacilityRepository.SaveChangesAsync();
        }
    }
}