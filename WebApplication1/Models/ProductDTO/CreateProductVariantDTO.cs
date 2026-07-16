
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ProductDTO
{
    public class CreateProductVariantDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }

        //mapping to product
        public int ProductId { get; set; }
    }
}
