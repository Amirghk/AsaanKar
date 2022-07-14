using FinalProject.Domain.Enums;

namespace FinalProject.Application.Common.Dtos;

public record OrderDto
{
    public int Id { get; init; }
    public DateTimeOffset DateAdded { get; init; }
    public DateTimeOffset? DateCompleted { get; init; }
    public string? Description { get; init; }
    public OrderState State { get; init; }
    public int AddressId { get; init; }
    public int ServiceId { get; init; }
    public int CustomerId { get; init; }
    public int? ExpertId { get; init; }
}