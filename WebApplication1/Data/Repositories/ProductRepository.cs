using Microsoft.EntityFrameworkCore;
using NoodlesEgypt.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Models.ProductDTO;

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
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
                return await dbSet.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,

                    CategoryName = p.Category.Name,
                    ImageFileName=p.ImageFileName,
                    CategoryId=p.CategoryId,
                    Variants = p.Variants.Select(v => new ProductVariantDTO
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Description = v.Description,
                        Price = v.Price,
                    }).ToList()
                })
            .AsNoTracking()
            .ToListAsync();
            //return await dbSet.Include(p=>p.Variants)
            //.ToListAsync();
        }
        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            return await dbSet.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImageFileName = p.ImageFileName,
                CategoryId = p.CategoryId,
                Variants = p.Variants.Select(v => new ProductVariantDTO
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    Price = v.Price,
                }).ToList()
            }).Where(p=>p.Id==id)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        }
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbSet
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
