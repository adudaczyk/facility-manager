using System;

namespace FacilityManager.BusinessLogic.Models
{
    public class OccupancyDto : EntityDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}