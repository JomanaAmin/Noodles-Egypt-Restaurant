namespace WebApplication1.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
