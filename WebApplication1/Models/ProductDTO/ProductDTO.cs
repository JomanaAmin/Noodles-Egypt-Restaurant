using System.ComponentModel.DataAnnotations;
using WebApplication1.Data.Entities;

namespace WebApplication1.Models.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)]
        public string ImageFileName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<ProductVariantDTO> Variants { get; set; }
        
    }
}
