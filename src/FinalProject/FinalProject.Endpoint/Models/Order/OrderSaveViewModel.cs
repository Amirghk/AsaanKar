namespace FinalProject.Endpoint.Models
{
    public class OrderSaveViewModel
    {
        public DateTimeOffset DateRequired { get; set; }
        public string? Description { get; init; }
        public int AddressId { get; init; }
        public int ServiceId { get; init; }
        public string CustomerId { get; init; } = null!;
    }
}
