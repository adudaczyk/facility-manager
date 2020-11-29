using FacilityManager.EntityFramework.Models;
namespace FacilityManager.EntityFramework.Repositories
{
    public class OccupancyRepository : GenericRepository<Occupancy>, IOccupancyRepository
    {
        public OccupancyRepository(FacilityManagerDbContext facilityManagerDbContext) : base(facilityManagerDbContext)
        {
        }
    }
}