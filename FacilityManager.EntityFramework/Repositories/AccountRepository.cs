using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(FacilityManagerDbContext facilityManagerDbContext) : base(facilityManagerDbContext)
        {
        }

        public async Task<Account> GetByEmail(string email)
        {
            return await _dbSet.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Account> GetByRefreshToken(string token)
        {
            return await _dbSet.Where(u => u.RefreshTokens.Any(t => t.Token == token)).SingleOrDefaultAsync();
        }
    }
}