using EMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMS.Data.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmsDbContext _emsDbContext;
        private readonly DbSet<T> _entitiySet;
        public GenericRepository(EmsDbContext emsDbContext)
        {
            _emsDbContext = emsDbContext;
            _entitiySet = _emsDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _entitiySet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _entitiySet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entitiySet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? result = await _entitiySet.FindAsync(id);
            if (result == null) return null;
            return result;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }

            //_emsDbContext.Entry(entity).State = EntityState.Modified;

            _entitiySet.Update(entity);
        }
    }
}
