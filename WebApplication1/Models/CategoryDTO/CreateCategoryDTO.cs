namespace WebApplication1.Models.CategoryDTO
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
