namespace FinalProject.Endpoint.Areas.Administration.Models
{
    public class BidViewModel
    {
        public int Id { get; init; }
        public string? Notes { get; init; }
        public decimal Price { get; init; }
        public int OrderId { get; init; }
        public string? ExpertId { get; init; }
    }
}
