using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Entities;

namespace WebApplication1.Models.ProductDTO
{
    public class CreateProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile ImageFile { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<CreateProductVariantDTO> Variants { get; set; } = new();
    }
}
