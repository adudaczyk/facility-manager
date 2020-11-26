using FacilityManager.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityManager.EntityFramework.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly FacilityManagerDbContext _dbContext;

        public GenericRepository(FacilityManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> GetByGuid(string guid)
        {
            return await _dbSet.Where(x => x.Guid.ToString() == guid).FirstOrDefaultAsync();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}