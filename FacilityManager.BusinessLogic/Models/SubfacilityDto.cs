using System.Collections.Generic;

namespace FacilityManager.BusinessLogic.Models
{
    public class SubfacilityDto : EntityDto
    {
        public string Name { get; set; }
        public List<OccupancyDto> Occupancies { get; set; }
    }
}