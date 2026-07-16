using Microsoft.EntityFrameworkCore;
using NoodlesEgypt.Data;
using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<Product> dbSet;
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet=dbContext.Set<Product>();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await dbSet.Include(p=>p.Variants).ToListAsync();
        }
    }
}
