using FinalProject.Domain.Enums;


namespace FinalProject.Application.Common.DataTransferObjects;

public record OrderDto
{
    public int Id { get; init; }
    public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset DateRequired { get; set; }
    public DateTimeOffset? DateCompleted { get; set; }
    public string? Description { get; init; }
    public OrderState State { get; set; }
    public int AddressId { get; init; }
    public AddressDto Address { get; set; } = null!;
    public int ServiceId { get; init; }
    public ServiceDto Service { get; init; } = null!;
    public string CustomerId { get; set; } = null!;
    public CustomerDto Customer { get; set; } = null!;
    public string? ExpertId { get; set; }
    public ExpertDto? Expert { get; set; }
    public decimal ServiceBasePrice { get; set; }
    public decimal? CompletedPrice { get; set; }
    public ICollection<BidDto> Bids { get; set; } = new List<BidDto>();
}