﻿using System.Collections.Generic;

namespace FacilityManager.EntityFramework.Models
{
    public class Facility : Entity
    {
        public string Name { get; set; }
        public List<Subfacility> Subfacilities { get; set; }
    }
}