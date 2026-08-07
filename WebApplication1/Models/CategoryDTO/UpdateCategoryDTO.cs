namespace WebApplication1.Models.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImageFileName{ get; set; }

    }

}
