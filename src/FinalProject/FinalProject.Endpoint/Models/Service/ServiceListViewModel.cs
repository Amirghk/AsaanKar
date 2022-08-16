namespace FinalProject.Endpoint.Models
{
    public class ServiceListViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public decimal? BasePrice { get; set; }
        public int CategoryId { get; init; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
