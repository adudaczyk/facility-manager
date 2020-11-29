using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.EntityFramework
{
    public class FacilityManagerDbContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Subfacility> Subfacility { get; set; }
        public DbSet<Occupancy> Occupancy { get; set; }

        public FacilityManagerDbContext(DbContextOptions<FacilityManagerDbContext> options)
            : base(options)
        {
        }
    }
}