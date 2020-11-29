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
    public class SubfacilityController : ControllerBase
    {
        private readonly ISubfacilityService _subfacilityService;

        public SubfacilityController(ISubfacilityService subfacilityService)
        {
            _subfacilityService = subfacilityService;
        }

        [AllowAnonymous]
        [HttpGet("get-subfacilities")]
        public async Task<IEnumerable<SubfacilityDto>> GetSubfacilities()
        {
            var allSubfacilities = await _subfacilityService.GetSubfacilities();

            return allSubfacilities;
        }

        [HttpGet("{guid}")]
        public async Task<SubfacilityDto> GetSubfacility(string guid)
        {
            var subfacility = await _subfacilityService.GetSubfacility(guid);

            return subfacility;
        }

        [HttpPost]
        public async Task CreateSubfacility([FromBody] SubfacilityDto subfacilityDto)
        {
            await _subfacilityService.CreateSubfacility(subfacilityDto);
        }

        [HttpPut("{guid}")]
        public async Task UpdateSubfacility(string guid, [FromBody] SubfacilityDto subfacilityDto)
        {
            subfacilityDto.Guid = new Guid(guid);
            await _subfacilityService.UpdateSubfacility(subfacilityDto);
        }

        [HttpDelete("{guid}")]
        public async Task DeleteSubfacility(string guid)
        {
            await _subfacilityService.DeleteSubfacility(guid);
        }
    }
}