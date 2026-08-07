using Microsoft.EntityFrameworkCore.Infrastructure;
using NoodlesEgypt.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private Lazy<IProductRepository> products;
        private Lazy<IBaseRepository<ProductVariant>> productVariants;
        private Lazy<IBaseRepository<Category>> categories;

        public UnitOfWork(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
            products = new Lazy<IProductRepository>(() => new ProductRepository(dbContext));
            productVariants = new Lazy<IBaseRepository<ProductVariant>>(() => new BaseRepository<ProductVariant>(dbContext));
            categories = new Lazy<IBaseRepository<Category>>(() => new BaseRepository<Category>(dbContext));
        }
        public IProductRepository Products => products.Value;

        public IBaseRepository<ProductVariant> ProductVariants => productVariants.Value;

        public IBaseRepository<Category> Categories => categories.Value;

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
