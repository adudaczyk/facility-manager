using FacilityManager.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public interface IFacilityService
    {
        Task<IEnumerable<FacilityDto>> GetFacilities();
        Task<FacilityDto> GetFacility(string guid);
        Task AddFacility(FacilityDto facilityDto);
        Task UpdateFacility(FacilityDto facilityDto);
        Task DeleteFacility(string guid);
    }
}