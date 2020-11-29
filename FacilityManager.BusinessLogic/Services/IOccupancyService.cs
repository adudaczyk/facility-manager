using FacilityManager.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public interface IOccupancyService
    {
        Task<IEnumerable<OccupancyDto>> GetOccupancies();
        Task<OccupancyDto> GetOccupancy(string guid);
        Task CreateOccupancy(OccupancyDto occupancyDto);
        Task UpdateOccupancy(OccupancyDto occupancyDto);
        Task DeleteOccupancy(string guid);
    }
}