using System.ComponentModel.DataAnnotations;

namespace FacilityManager.EntityFramework.Models
{
    public class Facility : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}