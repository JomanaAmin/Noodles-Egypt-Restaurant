using WebApplication1.Data.Entities;
using WebApplication1.Data.Repositories;

namespace WebApplication1.Data
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IBaseRepository<ProductVariant> ProductVariants { get; }
        IBaseRepository<Category> Categories { get; }
        Task<int> SaveChangesAsync();
    }
}
