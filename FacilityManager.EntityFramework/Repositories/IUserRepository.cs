using FacilityManager.EntityFramework.Models;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByRefreshToken(string token);
    }
}