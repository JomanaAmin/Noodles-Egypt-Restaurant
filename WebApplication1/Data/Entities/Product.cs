using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [MaxLength(100)]
        public string ImageFileName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductVariant> Variants { get; set; }
    }
}
