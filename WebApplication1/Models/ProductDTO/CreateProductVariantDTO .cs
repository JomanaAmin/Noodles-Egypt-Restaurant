
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ProductDTO
{
    public class CreateProductVariantDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(typeof(decimal), "0.01", "10000",
        ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        //mapping to product
        public int ProductId { get; set; }
    }
}
