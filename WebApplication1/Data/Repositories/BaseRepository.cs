
using Microsoft.EntityFrameworkCore;
using NoodlesEgypt.Data;

namespace WebApplication1.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T:class
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
   
            dbSet.Remove(entity);
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
