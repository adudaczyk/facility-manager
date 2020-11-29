using System;

namespace FacilityManager.EntityFramework.Models
{
    public class Occupancy : Entity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}