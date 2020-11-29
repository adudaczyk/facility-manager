using FacilityManager.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.BusinessLogic.Services
{
    public interface ISubfacilityService
    {
        Task<IEnumerable<SubfacilityDto>> GetSubfacilities();
        Task<SubfacilityDto> GetSubfacility(string guid);
        Task CreateSubfacility(SubfacilityDto subfacilityDto);
        Task UpdateSubfacility(SubfacilityDto subfacilityDto);
        Task DeleteSubfacility(string guid);
    }
}