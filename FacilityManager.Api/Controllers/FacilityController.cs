using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.BusinessLogic.Services;

namespace FacilityManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("get-facilities")]
        public async Task<IEnumerable<FacilityDto>> GetFacilities()
        {
            var allFacilities = await _facilityService.GetFacilities();

            return allFacilities;
        }

        [HttpGet("{guid}")]
        public async Task<FacilityDto> GetFacility(string guid)
        {
            var facility = await _facilityService.GetFacility(guid);

            return facility;
        }

        [HttpPost]
        public async Task AddFacility([FromBody] FacilityDto facilityDto)
        {
            await _facilityService.AddFacility(facilityDto);
        }

        [HttpPut("{guid}")]
        public async Task UpdateFacility(string guid, [FromBody] FacilityDto facilityDto)
        {
            facilityDto.Guid = new Guid(guid);
            await _facilityService.UpdateFacility(facilityDto);
        }

        [HttpDelete("{guid}")]
        public async Task DeleteFacility(string guid)
        {
            await _facilityService.DeleteFacility(guid);
        }
    }
}