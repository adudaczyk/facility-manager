using FacilityManager.EntityFramework.Models;
namespace FacilityManager.EntityFramework.Repositories
{
    public class SubfacilityRepository : GenericRepository<Subfacility>, ISubfacilityRepository
    {
        public SubfacilityRepository(FacilityManagerDbContext facilityManagerDbContext) : base(facilityManagerDbContext)
        {
        }
    }
}