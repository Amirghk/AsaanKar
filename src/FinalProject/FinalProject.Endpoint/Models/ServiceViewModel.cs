namespace FinalProject.Endpoint.Models
{
    public class ServiceViewModel
    {
        public int Id { get; init; }
        public string Description { get; init; } = null!;
        public decimal? BasePrice { get; set; }
        public int CategoryId { get; init; }
    }
}
