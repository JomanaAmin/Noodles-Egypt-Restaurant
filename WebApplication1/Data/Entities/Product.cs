namespace WebApplication1.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
