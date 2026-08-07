using WebApplication1.Data.Entities;
using WebApplication1.Models.ProductDTO;

namespace WebApplication1.Data.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);

    }
}
