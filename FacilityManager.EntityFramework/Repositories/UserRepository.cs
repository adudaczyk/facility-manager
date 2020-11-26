using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FacilityManagerDbContext facilityManagerDbContext) : base(facilityManagerDbContext)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetByRefreshToken(string token)
        {
            return await _dbSet.Where(u => u.RefreshTokens.Any(t => t.Token == token)).SingleOrDefaultAsync();
        }
    }
}