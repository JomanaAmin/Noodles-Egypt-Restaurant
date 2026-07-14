namespace WebApplication1.Data.Entities
{
    public class ProductVariant
    {
        public int ProductVariantId { get; set; }
        public string VariantName { get; set; } = string.Empty;
        public string VariantDescription { get; set; } = string.Empty;
        public decimal VariantPrice { get; set; }

        //mapping to product
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
