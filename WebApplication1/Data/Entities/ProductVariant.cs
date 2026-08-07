using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Precision(6,2)]
        public decimal Price { get; set; }

        //mapping to product
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
