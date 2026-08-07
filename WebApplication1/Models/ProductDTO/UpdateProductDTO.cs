
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ProductDTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
        public string ImageFileName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<ProductVariantDTO> Variants { get; set; } = new();
    }
}
