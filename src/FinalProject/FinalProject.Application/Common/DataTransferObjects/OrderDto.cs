using FinalProject.Domain.Enums;


namespace FinalProject.Application.Common.DataTransferObjects;

public record OrderDto
{
    public int Id { get; init; }
    public DateTimeOffset? DateCompleted { get; init; }
    public string? Description { get; init; }
    public OrderState State { get; init; }
    public int AddressId { get; init; }
    public int ServiceId { get; init; }
    public string CustomerId { get; init; } = null!;
    public string? ExpertId { get; set; }
    public decimal ServiceBasePrice { get; set; }
    public decimal? CompletedPrice { get; set; }
}