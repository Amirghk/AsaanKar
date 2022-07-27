namespace FinalProject.Endpoint.Models
{
    public class OrderSaveViewModel
    {
        public int Id { get; init; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset DateRequired { get; set; }
        public DateTimeOffset? DateCompleted { get; set; }
        public string? Description { get; init; }
        public int AddressId { get; init; }
        public int ServiceId { get; init; }
        public string CustomerId { get; init; } = null!;
        public decimal ServiceBasePrice { get; set; }
        public decimal? CompletedPrice { get; set; }
    }
}
