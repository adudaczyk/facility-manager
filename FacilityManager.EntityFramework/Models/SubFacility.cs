using System.Collections.Generic;

namespace FacilityManager.EntityFramework.Models
{
    public class Subfacility : Entity
    {
        public string Name { get; set; }
        public List<Occupancy> Occupancies {get; set; }
    }
}