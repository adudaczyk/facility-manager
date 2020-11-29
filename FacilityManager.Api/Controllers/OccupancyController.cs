using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FacilityManager.BusinessLogic.Models;
using FacilityManager.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;

namespace FacilityManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupancyController : ControllerBase
    {
        private readonly IOccupancyService _occupancyService;

        public OccupancyController(IOccupancyService occupancyService)
        {
            _occupancyService = occupancyService;
        }

        [AllowAnonymous]
        [HttpGet("get-occupancies")]
        public async Task<IEnumerable<OccupancyDto>> GetOccupancies()
        {
            var allOccupancies = await _occupancyService.GetOccupancies();

            return allOccupancies;
        }

        [HttpGet("{guid}")]
        public async Task<OccupancyDto> GetOccupancy(string guid)
        {
            var occupancy = await _occupancyService.GetOccupancy(guid);

            return occupancy;
        }

        [HttpPost]
        public async Task CreateOccupancy([FromBody] OccupancyDto occupancyDto)
        {
            await _occupancyService.CreateOccupancy(occupancyDto);
        }

        [HttpPut("{guid}")]
        public async Task UpdateOccupancy(string guid, [FromBody] OccupancyDto occupancyDto)
        {
            occupancyDto.Guid = new Guid(guid);
            await _occupancyService.UpdateOccupancy(occupancyDto);
        }

        [HttpDelete("{guid}")]
        public async Task DeleteOccupancy(string guid)
        {
            await _occupancyService.DeleteOccupancy(guid);
        }
    }
}