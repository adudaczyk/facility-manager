using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityManager.EntityFramework.Models
{

    [Table("Facilities", Schema = "dbo")]
    public class Facility : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}