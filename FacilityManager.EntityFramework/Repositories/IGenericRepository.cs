using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByGuid(string guid);
        Task SaveChangesAsync();
    }
}
