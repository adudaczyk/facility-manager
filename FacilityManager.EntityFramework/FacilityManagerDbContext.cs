using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace FacilityManager.EntityFramework
{
    public class FacilityManagerDbContext : DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Facility> Facility { get; set; }

        public FacilityManagerDbContext(DbContextOptions<FacilityManagerDbContext> options)
            : base(options)
        {
        }
    }
}