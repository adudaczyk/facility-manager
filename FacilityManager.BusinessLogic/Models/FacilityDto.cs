﻿using System.Collections.Generic;

namespace FacilityManager.BusinessLogic.Models
{
    public class FacilityDto : EntityDto
    {
        public string Name { get; set; }
        public List<SubfacilityDto> Subfacilities { get; set; }
    }
}