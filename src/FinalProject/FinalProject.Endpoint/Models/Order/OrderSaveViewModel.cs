namespace FinalProject.Endpoint.Models
{
    public class OrderSaveViewModel
    {
        public DateTimeOffset DateRequired { get; set; }
        public string? Description { get; init; }
        public int AddressId { get; set; }
        public int ServiceId { get; set; }
        public string? CustomerId { get; init; } = null!;
    }
}
