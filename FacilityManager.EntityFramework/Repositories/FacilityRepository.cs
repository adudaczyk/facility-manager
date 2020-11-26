using FacilityManager.EntityFramework.Models;
namespace FacilityManager.EntityFramework.Repositories
{
    public class FacilityRepository : GenericRepository<Facility>, IFacilityRepository
    {
        public FacilityRepository(FacilityManagerDbContext facilityManagerDbContext) : base(facilityManagerDbContext)
        {
        }
    }
}