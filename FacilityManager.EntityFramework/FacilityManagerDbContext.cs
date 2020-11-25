using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.EntityFramework
{
    public class FacilityManagerDbContext : DbContext
    {
        public DbSet<Facility> Facilities { get; set; }

        public FacilityManagerDbContext(DbContextOptions<FacilityManagerDbContext> options)
            : base(options)
        {
        }
    }
}