namespace FinalProject.Endpoint.Models
{
    public class CategorySaveViewModel
    {
        public string Name { get; init; } = null!;
        public string Description { get; set; } = null!;
        public int? ParentCategoryId { get; init; }
        public IFormFile? Picture { get; set; }
    }
}
