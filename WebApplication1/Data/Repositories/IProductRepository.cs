using WebApplication1.Data.Entities;

namespace WebApplication1.Data.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
