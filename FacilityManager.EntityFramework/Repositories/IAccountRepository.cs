using FacilityManager.EntityFramework.Models;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetByEmail(string email);
        Task<Account> GetByRefreshToken(string token);
    }
}